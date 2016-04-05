﻿namespace TraktApiSharp.Tests.Authentication
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using TraktApiSharp.Authentication;
    using TraktApiSharp.Enums;
    using Utils;

    [TestClass]
    public class TraktAccessTokenTests
    {
        [TestMethod]
        public void TestDefaultConstructor()
        {
            var dtNowUtc = DateTime.UtcNow;

            var token = new TraktAccessToken();

            token.Created.Should().BeCloseTo(dtNowUtc);
        }

        [TestMethod]
        public void TestReadFromJson()
        {
            var jsonFile = TestUtility.ReadJsonData(@"Authentication\AccessToken.json");

            jsonFile.Should().NotBeNullOrEmpty();

            var token = JsonConvert.DeserializeObject<TraktAccessToken>(jsonFile);

            token.Should().NotBeNull();
            token.AccessToken.Should().Be("dbaf9757982a9e738f05d249b7b5b4a266b3a139049317c4909f2f263572c781");
            token.TokenType.Should().Be(TraktAccessTokenType.Bearer);
            token.ExpiresInSeconds.Should().Be(7200);
            token.RefreshToken.Should().Be("76ba4c5c75c96f6087f58a4de10be6c00b29ea1ddc3b2022ee2016d1363e3a7c");
            token.AccessScope.Should().Be(TraktAccessScope.Public);
            token.IsValid.Should().BeTrue();
        }
    }
}
