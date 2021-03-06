﻿namespace TraktApiSharp.Requests.Handler
{
    using Base;
    using Checkins.OAuth;
    using Core;
    using Exceptions;
    using Interfaces;
    using Interfaces.Base;
    using Objects.Basic;
    using Objects.Post.Checkins.Responses;
    using Responses;
    using Responses.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using UriTemplates;
    using Utils;

    internal sealed class TraktRequestHandler : ITraktRequestHandler
    {
        private const string HEADER_PAGINATION_PAGE_KEY = "X-Pagination-Page";
        private const string HEADER_PAGINATION_LIMIT_KEY = "X-Pagination-Limit";
        private const string HEADER_PAGINATION_PAGE_COUNT_KEY = "X-Pagination-Page-Count";
        private const string HEADER_PAGINATION_ITEM_COUNT_KEY = "X-Pagination-Item-Count";
        private const string HEADER_TRENDING_USER_COUNT_KEY = "X-Trending-User-Count";
        private const string HEADER_SORT_BY_KEY = "X-Sort-By";
        private const string HEADER_SORT_HOW_KEY = "X-Sort-How";
        private const string HEADER_STARTDATE_KEY = "X-Start-Date";
        private const string HEADER_ENDDATE_KEY = "X-End-Date";
        private const string HEADER_PRIVATE_USER_KEY = "X-Private-User";

        // Don't mark this field as readonly,
        // as it is manually set in unit tests
        internal static HttpClient s_httpClient;

        private readonly TraktClient _client;

        internal TraktRequestHandler(TraktClient client)
        {
            _client = client;
        }

        public Task<TraktNoContentResponse> ExecuteNoContentRequestAsync(ITraktRequest request)
        {
            PreExecuteRequest(request);
            return QueryNoContentAsync(SetupRequestMessage(request));
        }

        public Task<TraktResponse<TContentType>> ExecuteSingleItemRequestAsync<TContentType>(ITraktRequest<TContentType> request)
        {
            PreExecuteRequest(request);
            return QuerySingleItemAsync<TContentType>(SetupRequestMessage(request));
        }

        public Task<TraktListResponse<TContentType>> ExecuteListRequestAsync<TContentType>(ITraktRequest<TContentType> request)
        {
            PreExecuteRequest(request);
            return QueryListAsync<TContentType>(SetupRequestMessage(request));
        }

        public Task<TraktPagedResponse<TContentType>> ExecutePagedRequestAsync<TContentType>(ITraktRequest<TContentType> request)
        {
            PreExecuteRequest(request);
            return QueryPagedListAsync<TContentType>(SetupRequestMessage(request));
        }

        // post requests

        public Task<TraktNoContentResponse> ExecuteNoContentRequestAsync<TRequestBody>(ITraktPostRequest<TRequestBody> request)
        {
            PreExecuteRequest(request);
            return QueryNoContentAsync(SetupRequestMessage(request));
        }

        public Task<TraktResponse<TContentType>> ExecuteSingleItemRequestAsync<TContentType, TRequestBody>(ITraktPostRequest<TContentType, TRequestBody> request)
        {
            PreExecuteRequest(request);
            var isCheckinRequest = request is TraktCheckinRequest<TContentType, TRequestBody>;
            return QuerySingleItemAsync<TContentType>(SetupRequestMessage(request), isCheckinRequest);
        }

        public Task<TraktListResponse<TContentType>> ExecuteListRequestAsync<TContentType, TRequestBody>(ITraktPostRequest<TContentType, TRequestBody> request)
        {
            PreExecuteRequest(request);
            return QueryListAsync<TContentType>(SetupRequestMessage(request));
        }

        public Task<TraktPagedResponse<TContentType>> ExecutePagedRequestAsync<TContentType, TRequestBody>(ITraktPostRequest<TContentType, TRequestBody> request)
        {
            PreExecuteRequest(request);
            return QueryPagedListAsync<TContentType>(SetupRequestMessage(request));
        }

        // put requests

        public Task<TraktNoContentResponse> ExecuteNoContentRequestAsync<TRequestBody>(ITraktPutRequest<TRequestBody> request)
        {
            PreExecuteRequest(request);
            return QueryNoContentAsync(SetupRequestMessage(request));
        }

        public Task<TraktResponse<TContentType>> ExecuteSingleItemRequestAsync<TContentType, TRequestBody>(ITraktPutRequest<TContentType, TRequestBody> request)
        {
            PreExecuteRequest(request);
            return QuerySingleItemAsync<TContentType>(SetupRequestMessage(request));
        }

        public Task<TraktListResponse<TContentType>> ExecuteListRequestAsync<TContentType, TRequestBody>(ITraktPutRequest<TContentType, TRequestBody> request)
        {
            PreExecuteRequest(request);
            return QueryListAsync<TContentType>(SetupRequestMessage(request));
        }

        public Task<TraktPagedResponse<TContentType>> ExecutePagedRequestAsync<TContentType, TRequestBody>(ITraktPutRequest<TContentType, TRequestBody> request)
        {
            PreExecuteRequest(request);
            return QueryPagedListAsync<TContentType>(SetupRequestMessage(request));
        }

        // query response helper methods

        private async Task<TraktNoContentResponse> QueryNoContentAsync(TraktHttpRequestMessage requestMessage)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await ExecuteRequestAsync(requestMessage).ConfigureAwait(false);
                Debug.Assert(responseMessage?.StatusCode == HttpStatusCode.NoContent, "precondition for generating no content response failed");

                return new TraktNoContentResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                if (_client.Configuration.ThrowResponseExceptions)
                    throw;

                return new TraktNoContentResponse { IsSuccess = false, Exception = ex };
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        private async Task<TraktResponse<TContentType>> QuerySingleItemAsync<TContentType>(TraktHttpRequestMessage requestMessage, bool isCheckinRequest = false)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await ExecuteRequestAsync(requestMessage, isCheckinRequest).ConfigureAwait(false);
                Debug.Assert(responseMessage?.StatusCode != HttpStatusCode.NoContent, "precondition for generating single item response failed");

                var responseContent = await GetResponseContentAsync(responseMessage).ConfigureAwait(false);
                Debug.Assert(!string.IsNullOrEmpty(responseContent), "precondition for deserializing response content failed");

                var contentObject = Json.Deserialize<TContentType>(responseContent);
                var hasValue = !EqualityComparer<TContentType>.Default.Equals(contentObject, default(TContentType));

                var response = new TraktResponse<TContentType>
                {
                    IsSuccess = true,
                    HasValue = hasValue,
                    Value = contentObject
                };

                if (responseMessage.Headers != null)
                    ParseResponseHeaderValues(response, responseMessage.Headers);

                return response;
            }
            catch (Exception ex)
            {
                if (_client.Configuration.ThrowResponseExceptions)
                    throw;

                return new TraktResponse<TContentType> { IsSuccess = false, Exception = ex };
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        private async Task<TraktListResponse<TContentType>> QueryListAsync<TContentType>(TraktHttpRequestMessage requestMessage)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await ExecuteRequestAsync(requestMessage).ConfigureAwait(false);
                Debug.Assert(responseMessage?.StatusCode != HttpStatusCode.NoContent, "precondition for generating list response failed");

                var responseContent = await GetResponseContentAsync(responseMessage).ConfigureAwait(false);
                Debug.Assert(!string.IsNullOrEmpty(responseContent), "precondition for deserializing response content failed");

                var contentObject = Json.Deserialize<IEnumerable<TContentType>>(responseContent);

                var response = new TraktListResponse<TContentType>
                {
                    IsSuccess = true,
                    HasValue = contentObject != null,
                    Value = contentObject
                };

                if (responseMessage.Headers != null)
                    ParseResponseHeaderValues(response, responseMessage.Headers);

                return response;
            }
            catch (Exception ex)
            {
                if (_client.Configuration.ThrowResponseExceptions)
                    throw;

                return new TraktListResponse<TContentType> { IsSuccess = false, Exception = ex };
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        private async Task<TraktPagedResponse<TContentType>> QueryPagedListAsync<TContentType>(TraktHttpRequestMessage requestMessage)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await ExecuteRequestAsync(requestMessage).ConfigureAwait(false);
                Debug.Assert(responseMessage?.StatusCode != HttpStatusCode.NoContent, "precondition for generating paged list response failed");

                var responseContent = await GetResponseContentAsync(responseMessage).ConfigureAwait(false);
                Debug.Assert(!string.IsNullOrEmpty(responseContent), "precondition for deserializing response content failed");

                var contentObject = Json.Deserialize<IEnumerable<TContentType>>(responseContent);

                var response = new TraktPagedResponse<TContentType>
                {
                    IsSuccess = true,
                    HasValue = contentObject != null,
                    Value = contentObject
                };

                if (responseMessage.Headers != null)
                {
                    ParseResponseHeaderValues(response, responseMessage.Headers);
                    ParsePagedResponseHeaderValues(response, responseMessage.Headers);
                }

                return response;
            }
            catch (Exception ex)
            {
                if (_client.Configuration.ThrowResponseExceptions)
                    throw;

                return new TraktPagedResponse<TContentType> { IsSuccess = false, Exception = ex };
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        private async Task<HttpResponseMessage> ExecuteRequestAsync(TraktHttpRequestMessage requestMessage, bool isCheckinRequest = false)
        {
            var response = await s_httpClient.SendAsync(requestMessage).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                await ErrorHandlingAsync(response, requestMessage, isCheckinRequest).ConfigureAwait(false);

            return response;
        }

        private Task<string> GetResponseContentAsync(HttpResponseMessage response)
            => response.Content != null ? response.Content.ReadAsStringAsync() : Task.FromResult(string.Empty);

        private void PreExecuteRequest(ITraktRequest request)
        {
            ValidateRequest(request);
            SetupHttpClient();
        }

        private void ValidateRequest(ITraktRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            request.Validate();
        }

        private void SetupHttpClient()
        {
            var httpClient = s_httpClient ?? new HttpClient();
            SetDefaultRequestHeaders(httpClient);
        }

        private string BuildUrl(ITraktRequest request)
        {
            var uriTemplate = new UriTemplate(request.UriTemplate);
            var requestUriParameters = request.GetUriPathParameters();

            foreach (var parameter in requestUriParameters)
                uriTemplate.AddParameterFromKeyValuePair(parameter.Key, parameter.Value);

            var uri = uriTemplate.Resolve();
            return $"{_client.Configuration.BaseUrl}{uri}";
        }

        private TraktHttpRequestMessage SetupRequestMessage(ITraktRequest request)
        {
            var requestMessage = CreateRequestMessage(request);
            SetRequestMessageHeadersForAuthorization(requestMessage, request.AuthorizationRequirement);
            return requestMessage;
        }

        private TraktHttpRequestMessage SetupRequestMessage<TRequestBody>(ITraktPostRequest<TRequestBody> request)
        {
            var requestMessage = CreateRequestMessage(request);
            AddRequestBodyContent(requestMessage, request);
            SetRequestMessageHeadersForAuthorization(requestMessage, request.AuthorizationRequirement);
            return requestMessage;
        }

        private TraktHttpRequestMessage SetupRequestMessage<TContentType, TRequestBody>(ITraktPostRequest<TContentType, TRequestBody> request)
        {
            var requestMessage = CreateRequestMessage(request);
            AddRequestBodyContent(requestMessage, request);
            SetRequestMessageHeadersForAuthorization(requestMessage, request.AuthorizationRequirement);
            return requestMessage;
        }

        private TraktHttpRequestMessage SetupRequestMessage<TRequestBody>(ITraktPutRequest<TRequestBody> request)
        {
            var requestMessage = CreateRequestMessage(request);
            AddRequestBodyContent(requestMessage, request);
            SetRequestMessageHeadersForAuthorization(requestMessage, request.AuthorizationRequirement);
            return requestMessage;
        }

        private TraktHttpRequestMessage SetupRequestMessage<TContentType, TRequestBody>(ITraktPutRequest<TContentType, TRequestBody> request)
        {
            var requestMessage = CreateRequestMessage(request);
            AddRequestBodyContent(requestMessage, request);
            SetRequestMessageHeadersForAuthorization(requestMessage, request.AuthorizationRequirement);
            return requestMessage;
        }

        private TraktHttpRequestMessage CreateRequestMessage(ITraktRequest request)
        {
            const string seasonKey = "season";
            const string episodeKey = "episode";

            var url = BuildUrl(request);
            var requestMessage = new TraktHttpRequestMessage(request.Method, url) { Url = url };

            if (request is ITraktHasId)
            {
                var idRequest = request as ITraktHasId;

                requestMessage.ObjectId = idRequest?.Id;
                requestMessage.RequestObjectType = idRequest?.RequestObjectType;
            }

            var parameters = request.GetUriPathParameters();

            if (parameters.Count != 0)
            {
                if (parameters.ContainsKey(seasonKey))
                {
                    var strSeasonNumber = (string)parameters[seasonKey];
                    uint seasonNumber;

                    if (uint.TryParse(strSeasonNumber, out seasonNumber))
                        requestMessage.SeasonNumber = seasonNumber;
                }

                if (parameters.ContainsKey(episodeKey))
                {
                    var strEpisodeNumber = (string)parameters[episodeKey];
                    uint episodeNumber;

                    if (uint.TryParse(strEpisodeNumber, out episodeNumber))
                        requestMessage.EpisodeNumber = episodeNumber;
                }
            }

            return requestMessage;
        }

        private void AddRequestBodyContent<TRequestBody>(TraktHttpRequestMessage requestMessage, ITraktHasRequestBody<TRequestBody> request)
        {
            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            string requestBodyJson;
            requestMessage.Content = GetRequestBodyContent(request, out requestBodyJson);
            requestMessage.RequestBodyJson = requestBodyJson;
        }

        private HttpContent GetRequestBodyContent<TRequestBody>(ITraktHasRequestBody<TRequestBody> request, out string requestBodyJson)
        {
            var requestBody = request.RequestBody;
            var requestBodyIsNull = EqualityComparer<TRequestBody>.Default.Equals(requestBody, default(TRequestBody));

            if (requestBodyIsNull)
            {
                requestBodyJson = string.Empty;
                return null;
            }

            var json = Json.Serialize(requestBody);
            requestBodyJson = json;
            return !string.IsNullOrEmpty(json) ? new StringContent(json, Encoding.UTF8, "application/json") : null;
        }

        private void SetDefaultRequestHeaders(HttpClient httpClient)
        {
            var appJsonHeader = new MediaTypeWithQualityHeaderValue("application/json");

            if (!httpClient.DefaultRequestHeaders.Contains(TraktConstants.APIClientIdHeaderKey))
                httpClient.DefaultRequestHeaders.Add(TraktConstants.APIClientIdHeaderKey, _client.ClientId);

            if (!httpClient.DefaultRequestHeaders.Contains(TraktConstants.APIVersionHeaderKey))
                httpClient.DefaultRequestHeaders.Add(TraktConstants.APIVersionHeaderKey, $"{_client.Configuration.ApiVersion}");

            if (!httpClient.DefaultRequestHeaders.Accept.Contains(appJsonHeader))
                httpClient.DefaultRequestHeaders.Accept.Add(appJsonHeader);
        }

        private void SetRequestMessageHeadersForAuthorization(TraktHttpRequestMessage requestMessage, TraktAuthorizationRequirement authorizationRequirement)
        {
            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            if (authorizationRequirement == TraktAuthorizationRequirement.Required)
            {
                if (!_client.Authentication.IsAuthorized)
                    throw new TraktAuthorizationException("authorization is required for this request, but the current authorization parameters are invalid");

                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _client.Authentication.Authorization.AccessToken);
            }

            if (authorizationRequirement == TraktAuthorizationRequirement.Optional && _client.Configuration.ForceAuthorization && _client.Authentication.IsAuthorized)
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _client.Authentication.Authorization.AccessToken);
        }

        private void ParseResponseHeaderValues(ITraktResponseHeaders headerResults, HttpResponseHeaders responseHeaders)
        {
            IEnumerable<string> values;

            if (responseHeaders.TryGetValues(HEADER_PAGINATION_PAGE_KEY, out values))
            {
                var strPage = values.First();
                int page;

                if (int.TryParse(strPage, out page))
                    headerResults.Page = page;
            }

            if (responseHeaders.TryGetValues(HEADER_PAGINATION_LIMIT_KEY, out values))
            {
                var strLimit = values.First();
                int limit;

                if (int.TryParse(strLimit, out limit))
                    headerResults.Limit = limit;
            }

            if (responseHeaders.TryGetValues(HEADER_TRENDING_USER_COUNT_KEY, out values))
            {
                var strTrendingUserCount = values.First();
                int userCount;

                if (int.TryParse(strTrendingUserCount, out userCount))
                    headerResults.TrendingUserCount = userCount;
            }

            if (responseHeaders.TryGetValues(HEADER_SORT_BY_KEY, out values))
                headerResults.SortBy = values.First();

            if (responseHeaders.TryGetValues(HEADER_SORT_HOW_KEY, out values))
                headerResults.SortHow = values.First();

            if (responseHeaders.TryGetValues(HEADER_PRIVATE_USER_KEY, out values))
            {
                var strIsPrivateUser = values.First();
                bool isPrivateUser;

                if (bool.TryParse(strIsPrivateUser, out isPrivateUser))
                    headerResults.IsPrivateUser = isPrivateUser;
            }

            if (responseHeaders.TryGetValues(HEADER_STARTDATE_KEY, out values))
            {
                var strStartDate = values.First();
                DateTime startDate;

                if (DateTime.TryParse(strStartDate, out startDate))
                    headerResults.StartDate = startDate.ToUniversalTime();
            }

            if (responseHeaders.TryGetValues(HEADER_ENDDATE_KEY, out values))
            {
                var strEndDate = values.First();
                DateTime endDate;

                if (DateTime.TryParse(strEndDate, out endDate))
                    headerResults.EndDate = endDate.ToUniversalTime();
            }
        }

        private void ParsePagedResponseHeaderValues(ITraktPagedResponseHeaders headerResults, HttpResponseHeaders responseHeaders)
        {
            IEnumerable<string> values;

            if (responseHeaders.TryGetValues(HEADER_PAGINATION_PAGE_COUNT_KEY, out values))
            {
                var strPageCount = values.First();
                int pageCount;

                if (int.TryParse(strPageCount, out pageCount))
                    headerResults.PageCount = pageCount;
            }

            if (responseHeaders.TryGetValues(HEADER_PAGINATION_ITEM_COUNT_KEY, out values))
            {
                var strItemCount = values.First();
                int itemCount;

                if (int.TryParse(strItemCount, out itemCount))
                    headerResults.ItemCount = itemCount;
            }
        }

        private async Task ErrorHandlingAsync(HttpResponseMessage response, TraktHttpRequestMessage requestMessage, bool isCheckinRequest = false)
        {
            var responseContent = string.Empty;

            if (response.Content != null)
                responseContent = await response.Content.ReadAsStringAsync();

            var code = response.StatusCode;
            var url = requestMessage.Url;
            var requestBodyJson = requestMessage.RequestBodyJson;
            var reasonPhrase = response.ReasonPhrase;

            switch (code)
            {
                case HttpStatusCode.NotFound:
                    HandleNotFoundStatusCode(requestMessage, responseContent, url, requestBodyJson, reasonPhrase);
                    break;
                case HttpStatusCode.BadRequest:
                    throw new TraktBadRequestException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case HttpStatusCode.Unauthorized:
                    throw new TraktAuthorizationException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case HttpStatusCode.Forbidden:
                    throw new TraktForbiddenException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case HttpStatusCode.MethodNotAllowed:
                    throw new TraktMethodNotFoundException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case HttpStatusCode.Conflict:
                    HandleConflictStatusCode(isCheckinRequest, responseContent, url, requestBodyJson, reasonPhrase);
                    break;
                case HttpStatusCode.InternalServerError:
                    throw new TraktServerException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case HttpStatusCode.BadGateway:
                    throw new TraktBadGatewayException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case (HttpStatusCode)412:
                    throw new TraktPreconditionFailedException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case (HttpStatusCode)422:
                    throw new TraktValidationException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case (HttpStatusCode)429:
                    throw new TraktRateLimitException()
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case (HttpStatusCode)503:
                case (HttpStatusCode)504:
                    throw new TraktServerUnavailableException("Service Unavailable - server overloaded (try again in 30s)")
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        StatusCode = HttpStatusCode.ServiceUnavailable,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
                case (HttpStatusCode)520:
                case (HttpStatusCode)521:
                case (HttpStatusCode)522:
                    throw new TraktServerUnavailableException("Service Unavailable - Cloudflare error")
                    {
                        RequestUrl = url,
                        RequestBody = requestBodyJson,
                        StatusCode = HttpStatusCode.ServiceUnavailable,
                        Response = responseContent,
                        ServerReasonPhrase = reasonPhrase
                    };
            }

            HandleUnknownError(responseContent, code, url, requestBodyJson, reasonPhrase);
        }

        private static void HandleNotFoundStatusCode(TraktHttpRequestMessage requestMessage, string responseContent, string url, string requestBodyJson, string reasonPhrase)
        {
            var requestObjectType = requestMessage.RequestObjectType;

            if (requestObjectType.HasValue)
            {
                var objectId = requestMessage.ObjectId;
                uint seasonNr = requestMessage.SeasonNumber ?? 0;
                uint episodeNr = requestMessage.EpisodeNumber ?? 0;

                switch (requestObjectType.Value)
                {
                    case TraktRequestObjectType.Episodes:
                        throw new TraktEpisodeNotFoundException(objectId, seasonNr, episodeNr)
                        {
                            RequestUrl = url,
                            RequestBody = requestBodyJson,
                            Response = responseContent,
                            ServerReasonPhrase = reasonPhrase
                        };
                    case TraktRequestObjectType.Seasons:
                        throw new TraktSeasonNotFoundException(objectId, seasonNr)
                        {
                            RequestUrl = url,
                            RequestBody = requestBodyJson,
                            Response = responseContent,
                            ServerReasonPhrase = reasonPhrase
                        };
                    case TraktRequestObjectType.Shows:
                        throw new TraktShowNotFoundException(objectId)
                        {
                            RequestUrl = url,
                            RequestBody = requestBodyJson,
                            Response = responseContent,
                            ServerReasonPhrase = reasonPhrase
                        };
                    case TraktRequestObjectType.Movies:
                        throw new TraktMovieNotFoundException(objectId)
                        {
                            RequestUrl = url,
                            RequestBody = requestBodyJson,
                            Response = responseContent,
                            ServerReasonPhrase = reasonPhrase
                        };
                    case TraktRequestObjectType.People:
                        throw new TraktPersonNotFoundException(objectId)
                        {
                            RequestUrl = url,
                            RequestBody = requestBodyJson,
                            Response = responseContent,
                            ServerReasonPhrase = reasonPhrase
                        };
                    case TraktRequestObjectType.Comments:
                        throw new TraktCommentNotFoundException(objectId)
                        {
                            RequestUrl = url,
                            RequestBody = requestBodyJson,
                            Response = responseContent,
                            ServerReasonPhrase = reasonPhrase
                        };
                    case TraktRequestObjectType.Lists:
                        throw new TraktListNotFoundException(objectId)
                        {
                            RequestUrl = url,
                            RequestBody = requestBodyJson,
                            Response = responseContent,
                            ServerReasonPhrase = reasonPhrase
                        };
                    default:
                        throw new TraktObjectNotFoundException(objectId)
                        {
                            RequestUrl = url,
                            RequestBody = requestBodyJson,
                            Response = responseContent,
                            ServerReasonPhrase = reasonPhrase
                        };
                }
            }

            throw new TraktNotFoundException($"Resource not found - Reason Phrase: {reasonPhrase}");
        }

        private static void HandleConflictStatusCode(bool isCheckinRequest, string responseContent, string url, string requestBodyJson, string reasonPhrase)
        {
            if (isCheckinRequest)
            {
                TraktCheckinPostErrorResponse errorResponse = null;

                if (!string.IsNullOrEmpty(responseContent))
                    errorResponse = Json.Deserialize<TraktCheckinPostErrorResponse>(responseContent);

                throw new TraktCheckinException("checkin is already in progress")
                {
                    RequestUrl = url,
                    RequestBody = requestBodyJson,
                    Response = responseContent,
                    ServerReasonPhrase = reasonPhrase,
                    ExpiresAt = errorResponse?.ExpiresAt
                };
            }

            throw new TraktConflictException()
            {
                RequestUrl = url,
                RequestBody = requestBodyJson,
                Response = responseContent,
                ServerReasonPhrase = reasonPhrase
            };
        }

        private static void HandleUnknownError(string responseContent, HttpStatusCode code, string url, string requestBodyJson, string reasonPhrase)
        {
            TraktError error = null;

            try
            {
                error = Json.Deserialize<TraktError>(responseContent);
            }
            catch (Exception ex)
            {
                throw new TraktException("json convert exception", ex);
            }

            var errorMessage = (error == null || string.IsNullOrEmpty(error.Description))
                                    ? $"Trakt API error without content. Response status code was {(int)code}"
                                    : error.Description;

            throw new TraktException(errorMessage)
            {
                RequestUrl = url,
                RequestBody = requestBodyJson,
                Response = responseContent,
                ServerReasonPhrase = reasonPhrase
            };
        }
    }
}
