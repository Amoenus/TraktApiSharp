﻿namespace TraktApiSharp.Tests.Services
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TraktApiSharp.Authentication;
    using TraktApiSharp.Enums;
    using TraktApiSharp.Services;

    [TestClass]
    public class TraktSerializationServiceTests
    {
        private static readonly DateTime CREATED_AT = DateTime.UtcNow;

        private static readonly TraktAuthorization AUTHORIZATION = new TraktAuthorization
        {
            AccessScope = TraktAccessScope.Public,
            AccessToken = "accessToken",
            RefreshToken = "refreshToken",
            ExpiresInSeconds = 7200,
            TokenType = TraktAccessTokenType.Bearer,
            IgnoreExpiration = false,
            Created = CREATED_AT
        };

        private static readonly string AUTHORIZATION_JSON =
            "{" +
                $"\"AccessToken\":\"{AUTHORIZATION.AccessToken}\"," +
                $"\"RefreshToken\":\"{AUTHORIZATION.RefreshToken}\"," +
                $"\"ExpiresIn\":{AUTHORIZATION.ExpiresInSeconds}," +
                $"\"Scope\":\"{AUTHORIZATION.AccessScope.ObjectName}\"," +
                $"\"TokenType\":\"{AUTHORIZATION.TokenType.ObjectName}\"," +
                $"\"CreatedAtTicks\":{CREATED_AT.Ticks}," +
                $"\"IgnoreExpiration\":{AUTHORIZATION.IgnoreExpiration.ToString().ToLower()}" +
            "}";

        [TestMethod]
        public void TestTraktSerializationServiceSerializeTraktAuthorization()
        {
            var jsonAuthorization = TraktSerializationService.Serialize(AUTHORIZATION);

            jsonAuthorization.Should().NotBeNullOrEmpty();
            jsonAuthorization.Should().Be(AUTHORIZATION_JSON);
        }

        [TestMethod]
        public void TestTraktSerializationServiceSerializeEmptyTraktAuthorization()
        {
            var emptyAuthorization = new TraktAuthorization();

            string emptyAuthorizationJson =
            "{" +
                $"\"AccessToken\":\"\"," +
                $"\"RefreshToken\":\"\"," +
                $"\"ExpiresIn\":0," +
                $"\"Scope\":\"public\"," +
                $"\"TokenType\":\"bearer\"," +
                $"\"CreatedAtTicks\":{emptyAuthorization.Created.Ticks}," +
                $"\"IgnoreExpiration\":false" +
            "}";

            Action act = () => TraktSerializationService.Serialize(emptyAuthorization);
            act.ShouldNotThrow();

            var jsonAuthorization = TraktSerializationService.Serialize(emptyAuthorization);

            jsonAuthorization.Should().NotBeNullOrEmpty();
            jsonAuthorization.Should().Be(emptyAuthorizationJson);
        }

        [TestMethod]
        public void TestTraktSerializationServiceSerializeTraktAuthorizationArgumentExceptions()
        {
            Action act = () => TraktSerializationService.Serialize(default(TraktAuthorization));
            act.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void TestTraktSerializationServiceDeserializeTraktAuthorization()
        {
            var authorization = TraktSerializationService.DeserializeAuthorization(AUTHORIZATION_JSON);

            authorization.Should().NotBeNull();
            authorization.AccessToken.Should().Be(AUTHORIZATION.AccessToken);
            authorization.RefreshToken.Should().Be(AUTHORIZATION.RefreshToken);
            authorization.ExpiresInSeconds.Should().Be(AUTHORIZATION.ExpiresInSeconds);
            authorization.AccessScope.Should().Be(AUTHORIZATION.AccessScope);
            authorization.TokenType.Should().Be(AUTHORIZATION.TokenType);
            authorization.Created.Should().Be(AUTHORIZATION.Created);
            authorization.IgnoreExpiration.Should().Be(AUTHORIZATION.IgnoreExpiration);
        }

        [TestMethod]
        public void TestTraktSerializationServiceDeserializeTraktAuthorizationArgumentExceptions()
        {
            Action act = () => TraktSerializationService.DeserializeAuthorization(null);
            act.ShouldThrow<ArgumentException>();

            act = () => TraktSerializationService.DeserializeAuthorization(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSerializationServiceDeserializeTraktAuthorizationInvalidJson()
        {
            Action act = () => TraktSerializationService.DeserializeAuthorization("{ \"invalid\": \"json\" }");
            act.ShouldNotThrow();

            var result = TraktSerializationService.DeserializeAuthorization("{ \"invalid\": \"json\" }");
            result.Should().BeNull();

            act = () => TraktSerializationService.DeserializeAuthorization("invalid\": \"json\" }");
            act.ShouldThrow<ArgumentException>();

            string invalidAuthorizationJson =
            "{" +
                $"\"AccessToken\":\"\"," +
                $"\"RefreshToken\":\"\"," +
                $"\"ExpiresIn\":\"stringvalue\"," +
                $"\"Scope\":\"public\"," +
                $"\"TokenType\":\"bearer\"," +
                $"\"CreatedAtTicks\":0," +
                $"\"IgnoreExpiration\":false" +
            "}";

            act = () => TraktSerializationService.DeserializeAuthorization(invalidAuthorizationJson);
            act.ShouldThrow<ArgumentException>();

            invalidAuthorizationJson =
            "{" +
                $"\"AccessToken\":\"\"," +
                $"\"RefreshToken\":\"\"," +
                $"\"ExpiresIn\":0," +
                $"\"Scope\":\"public\"," +
                $"\"TokenType\":\"bearer\"," +
                $"\"CreatedAtTicks\":\"stringvalue\"," +
                $"\"IgnoreExpiration\":false" +
            "}";

            act = () => TraktSerializationService.DeserializeAuthorization(invalidAuthorizationJson);
            act.ShouldThrow<ArgumentException>();
        }
    }
}
