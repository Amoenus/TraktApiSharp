﻿namespace TraktApiSharp.Authentication
{
    using Core;
    using Enums;
    using Exceptions;
    using Extensions;
    using Newtonsoft.Json;
    using Objects.Basic;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class TraktOAuth
    {
        internal TraktOAuth(TraktClient client)
        {
            Client = client;
        }

        public TraktClient Client { get; private set; }

        public string CreateAuthorizationUrl()
        {
            var clientId = Client.ClientId;
            var redirectUri = Client.Authentication.RedirectUri;

            return CreateAuthorizationUrl(clientId, redirectUri);
        }

        public string CreateAuthorizationUrl(string clientId)
        {
            var redirectUri = Client.Authentication.RedirectUri;
            return CreateAuthorizationUrl(clientId, redirectUri);
        }

        public string CreateAuthorizationUrl(string clientId, string redirectUri)
        {
            ValidateAuthorizationUrlParameters(clientId, redirectUri);
            return BuildAuthorizationUrl(clientId, redirectUri);
        }

        public string CreateAuthorizationUrl(string clientId, string redirectUri, string state)
        {
            ValidateAuthorizationUrlParameters(clientId, redirectUri, state);
            return BuildAuthorizationUrl(clientId, redirectUri, state);
        }

        public string CreateAuthorizationUrlWithDefaultState()
        {
            var clientId = Client.ClientId;
            var redirectUri = Client.Authentication.RedirectUri;
            var state = Client.Authentication.AntiForgeryToken;

            return CreateAuthorizationUrl(clientId, redirectUri, state);
        }

        public string CreateAuthorizationUrlWithDefaultState(string clientId)
        {
            var redirectUri = Client.Authentication.RedirectUri;
            var state = Client.Authentication.AntiForgeryToken;
            return CreateAuthorizationUrl(clientId, redirectUri, state);
        }

        public string CreateAuthorizationUrlWithDefaultState(string clientId, string redirectUri)
        {
            var state = Client.Authentication.AntiForgeryToken;
            return CreateAuthorizationUrl(clientId, redirectUri, state);
        }

        public async Task<TraktAccessToken> GetAccessTokenAsync()
        {
            return await GetAccessTokenAsync(Client.Authentication.OAuthAuthorizationCode);
        }

        public async Task<TraktAccessToken> GetAccessTokenAsync(string code)
        {
            return await GetAccessTokenAsync(code, Client.ClientId, Client.ClientSecret, Client.Authentication.RedirectUri);
        }

        public async Task<TraktAccessToken> GetAccessTokenAsync(string code, string clientId)
        {
            return await GetAccessTokenAsync(code, clientId, Client.ClientSecret, Client.Authentication.RedirectUri);
        }

        public async Task<TraktAccessToken> GetAccessTokenAsync(string code, string clientId, string clientSecret)
        {
            return await GetAccessTokenAsync(code, clientId, clientSecret, Client.Authentication.RedirectUri);
        }

        public async Task<TraktAccessToken> GetAccessTokenAsync(string code, string clientId, string clientSecret, string redirectUri)
        {
            var grantType = TraktAccessTokenGrantType.AuthorizationCode.AsString();

            ValidateAccessTokenInput(code, clientId, clientSecret, redirectUri, grantType);

            var postContent = $"{{ \"code\": \"{code}\", \"client_id\": \"{clientId}\", " +
                              $"\"client_secret\": \"{clientSecret}\", \"redirect_uri\": " +
                              $"\"{redirectUri}\", \"grant_type\": \"{grantType}\" }}";

            var httpClient = TraktConfiguration.HTTP_CLIENT;

            if (httpClient == null)
                httpClient = new HttpClient();

            SetDefaultRequestHeaders(httpClient);

            var tokenUrl = $"{Client.Configuration.BaseUrl}{TraktConstants.OAuthTokenUri}";

            using (var content = new StringContent(postContent))
            using (var response = await httpClient.PostAsync(tokenUrl, content))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var token = await Task.Run(() => JsonConvert.DeserializeObject<TraktAccessToken>(data));

                    Client.Authentication.AccessToken = token;

                    return token;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized) // Invalid code
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var error = await Task.Run(() => JsonConvert.DeserializeObject<TraktError>(data));

                    var errorMessage = $"error on retrieving oauth access token\nerror: {error.Error}\n" +
                                       $"description: {error.Description}";

                    throw new TraktAuthenticationOAuthException(errorMessage)
                    {
                        StatusCode = response.StatusCode,
                        RequestUrl = tokenUrl,
                        RequestBody = postContent,
                        ServerReasonPhrase = response.ReasonPhrase
                    };
                }

                throw new TraktAuthenticationOAuthException("unknown exception") { ServerReasonPhrase = response.ReasonPhrase };
            }
        }

        public async Task<TraktAccessToken> RefreshAccessTokenAsync()
        {
            return await Client.Authentication.RefreshAccessTokenAsync();
        }

        public async Task<TraktAccessToken> RefreshAccessTokenAsync(string refreshToken, string clientId, string clientSecret, string redirectUri)
        {
            return await Client.Authentication.RefreshAccessTokenAsync(refreshToken, clientId, clientSecret, redirectUri);
        }

        public async Task<bool> RevokeAccessTokenAsync()
        {
            return await Client.Authentication.RevokeAccessTokenAsync();
        }

        public async Task<bool> RevokeAccessTokenAsync(string accessToken, string clientId)
        {
            return await Client.Authentication.RevokeAccessTokenAsync(accessToken, clientId);
        }

        private void SetDefaultRequestHeaders(HttpClient httpClient)
        {
            var appJsonHeader = new MediaTypeWithQualityHeaderValue("application/json");

            if (!httpClient.DefaultRequestHeaders.Accept.Contains(appJsonHeader))
                httpClient.DefaultRequestHeaders.Accept.Add(appJsonHeader);
        }

        private string CreateEncodedAuthorizationUri(string clientId, string redirectUri, string state = null)
        {
            var uriParams = new Dictionary<string, string>();

            uriParams["response_type"] = "code";
            uriParams["client_id"] = clientId;
            uriParams["redirect_uri"] = redirectUri;

            if (!string.IsNullOrEmpty(state))
                uriParams["state"] = state;

            var encodedUriContent = new FormUrlEncodedContent(uriParams);
            var encodedUri = encodedUriContent.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(encodedUri))
                throw new ArgumentException("authorization uri not valid");

            return $"?{encodedUri}";
        }

        private string BuildAuthorizationUrl(string clientId, string redirectUri, string state = null)
        {
            var encodedUriParams = CreateEncodedAuthorizationUri(clientId, redirectUri, state);
            var authorizationUrl = $"{TraktConstants.OAuthBaseAuthorizeUrl}/{TraktConstants.OAuthAuthorizeUri}{encodedUriParams}";

            return authorizationUrl;
        }

        private void ValidateAuthorizationUrlParameters(string clientId, string redirectUri)
        {
            if (string.IsNullOrEmpty(clientId) || clientId.ContainsSpace())
                throw new ArgumentException("client id not valid", "clientId");

            if (string.IsNullOrEmpty(redirectUri) || redirectUri.ContainsSpace())
                throw new ArgumentException("redirect uri not valid", "redirectUri");
        }

        private void ValidateAuthorizationUrlParameters(string clientId, string redirectUri, string state)
        {
            ValidateAuthorizationUrlParameters(clientId, redirectUri);

            if (string.IsNullOrEmpty(state) || state.ContainsSpace())
                throw new ArgumentException("state not valid", "state");
        }

        private void ValidateAccessTokenInput(string code, string clientId, string clientSecret, string redirectUri, string grantType)
        {
            if (string.IsNullOrEmpty(code) || code.ContainsSpace())
                throw new ArgumentException("code not valid", "code");

            ValidateAuthorizationUrlParameters(clientId, redirectUri);

            if (string.IsNullOrEmpty(clientSecret) || clientSecret.ContainsSpace())
                throw new ArgumentException("client secret not valid", "clientSecret");

            if (string.IsNullOrEmpty(grantType))
                throw new ArgumentException("grant type not valid", "grantType");
        }
    }
}
