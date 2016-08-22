﻿namespace TraktApiSharp.Services
{
    using Authentication;
    using Enums;
    using Newtonsoft.Json;
    using System;
    using Utils;

    /// <summary>Provides helper methods for serializing and deserializing Trakt objects.</summary>
    public static class TraktSerializationService
    {
        /// <summary>Serializes an <see cref="TraktAuthorization" /> instance to a JSON string.</summary>
        /// <param name="authorization">The authorization information, which should be serialized.</param>
        /// <returns>A Json string, containing all properties of the given authorization.</returns>
        /// <exception cref="ArgumentNullException">Thrown, if the given authorization is null.</exception>
        public static string Serialize(TraktAuthorization authorization)
        {
            if (authorization == null)
                throw new ArgumentNullException(nameof(authorization), "authorization must not be null");

            var authorizationWrapper = new
            {
                AccessToken = authorization.AccessToken,
                RefreshToken = authorization.RefreshToken,
                ExpiresIn = authorization.ExpiresIn,
                Scope = authorization.AccessScope.ObjectName,
                TokenType = authorization.TokenType.ObjectName,
                CreatedAtTicks = authorization.Created.Ticks,
                IgnoreExpiration = authorization.IgnoreExpiration
            };

            return Json.Serialize(authorizationWrapper);
        }

        /// <summary>Serializes an <see cref="TraktDevice" /> instance to a JSON string.</summary>
        /// <param name="device">The device information, which should be serialized.</param>
        /// <returns>A Json string, containing all properties of the given device.</returns>
        /// <exception cref="ArgumentNullException">Thrown, if the given device is null.</exception>
        public static string Serialize(TraktDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "device must not be null");

            var deviceWrapper = new
            {
                UserCode = device.UserCode,
                DeviceCode = device.DeviceCode,
                VerificationUrl = device.VerificationUrl,
                ExpiresInSeconds = device.ExpiresInSeconds,
                IntervalInSeconds = device.IntervalInSeconds,
                CreatedAtTicks = device.Created.Ticks
            };

            return Json.Serialize(deviceWrapper);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="TraktAuthorization" /> instance.
        /// </summary>
        /// <param name="authorizationJson">The JSON string, which should be deserialized.</param>
        /// <returns>
        /// An <see cref="TraktAuthorization" /> instance, containing the information from the JSON string, if successful.
        /// If the JSON string could not be parsed, null will be returned.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown, if the given authorizationJson is null or empty.</exception>
        public static TraktAuthorization Deserialize(string authorizationJson)
        {
            if (string.IsNullOrEmpty(authorizationJson))
                throw new ArgumentException("authorization is invalid", nameof(authorizationJson));

            var authorizationWrapper = new
            {
                AccessToken = string.Empty,
                RefreshToken = string.Empty,
                ExpiresIn = 0,
                Scope = string.Empty,
                TokenType = string.Empty,
                CreatedAtTicks = 0L,
                IgnoreExpiration = false
            };

            var anonymousAuthorization = JsonConvert.DeserializeAnonymousType(authorizationJson, authorizationWrapper);

            if (anonymousAuthorization != null)
            {
                var accessToken = anonymousAuthorization.AccessToken;
                var refreshToken = anonymousAuthorization.RefreshToken;
                var expiresIn = anonymousAuthorization.ExpiresIn;
                var scope = anonymousAuthorization.Scope;
                var tokenType = anonymousAuthorization.TokenType;
                var createdAtTicks = anonymousAuthorization.CreatedAtTicks;
                var ignoreExpiration = anonymousAuthorization.IgnoreExpiration;

                var accessScope = !string.IsNullOrEmpty(scope) ? TraktEnumeration.FromObjectName<TraktAccessScope>(scope) : TraktAccessScope.Public;
                var accessTokenType = !string.IsNullOrEmpty(tokenType) ? TraktEnumeration.FromObjectName<TraktAccessTokenType>(tokenType) : TraktAccessTokenType.Bearer;
                var createdDateTime = new DateTime(createdAtTicks, DateTimeKind.Utc);

                var authorization = new TraktAuthorization
                {
                    AccessToken = accessToken ?? string.Empty,
                    RefreshToken = refreshToken ?? string.Empty,
                    ExpiresIn = expiresIn,
                    AccessScope = accessScope,
                    TokenType = accessTokenType,
                    IgnoreExpiration = ignoreExpiration,
                    Created = createdDateTime
                };

                return authorization;
            }

            return default(TraktAuthorization);
        }
    }
}
