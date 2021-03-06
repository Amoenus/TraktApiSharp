﻿namespace TraktApiSharp.Tests.Requests.People
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using Traits;
    using TraktApiSharp.Objects.Get.People.Credits;
    using TraktApiSharp.Requests.Base;
    using TraktApiSharp.Requests.Parameters;
    using TraktApiSharp.Requests.People;
    using Xunit;

    [Category("Requests.People")]
    public class TraktPersonMovieCreditsRequest_Tests
    {
        [Fact]
        public void Test_TraktPersonMovieCreditsRequest_IsNotAbstract()
        {
            typeof(TraktPersonMovieCreditsRequest).IsAbstract.Should().BeFalse();
        }

        [Fact]
        public void Test_TraktPersonMovieCreditsRequest_IsSealed()
        {
            typeof(TraktPersonMovieCreditsRequest).IsSealed.Should().BeTrue();
        }

        [Fact]
        public void Test_TraktPersonMovieCreditsRequest_Inherits_ATraktPersonRequest_1()
        {
            typeof(TraktPersonMovieCreditsRequest).IsSubclassOf(typeof(ATraktPersonRequest<TraktPersonMovieCredits>)).Should().BeTrue();
        }

        [Fact]
        public void Test_TraktPersonMovieCreditsRequest_Has_AuthorizationRequirement_NotRequired()
        {
            var request = new TraktPersonMovieCreditsRequest();
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [Fact]
        public void Test_TraktPersonMovieCreditsRequest_Returns_Valid_RequestObjectType()
        {
            var request = new TraktPersonMovieCreditsRequest();
            request.RequestObjectType.Should().Be(TraktRequestObjectType.People);
        }

        [Fact]
        public void Test_TraktPersonMovieCreditsRequest_Has_Valid_UriTemplate()
        {
            var request = new TraktPersonMovieCreditsRequest();
            request.UriTemplate.Should().Be("people/{id}/movies{?extended}");
        }

        [Fact]
        public void Test_TraktPersonMovieCreditsRequest_Returns_Valid_UriPathParameters()
        {
            // only id
            var request = new TraktPersonMovieCreditsRequest { Id = "123" };

            request.GetUriPathParameters().Should().NotBeNull()
                                                   .And.HaveCount(1)
                                                   .And.Contain(new Dictionary<string, object>
                                                   {
                                                       ["id"] = "123"
                                                   });

            // id and extended info
            var extendedInfo = new TraktExtendedInfo { Full = true };
            request = new TraktPersonMovieCreditsRequest { Id = "123", ExtendedInfo = extendedInfo };

            request.GetUriPathParameters().Should().NotBeNull()
                                                   .And.HaveCount(2)
                                                   .And.Contain(new Dictionary<string, object>
                                                   {
                                                       ["id"] = "123",
                                                       ["extended"] = extendedInfo.ToString()
                                                   });
        }

        [Fact]
        public void Test_TraktPersonMovieCreditsRequest_Validate_Throws_Exceptions()
        {
            // id is null
            var request = new TraktPersonMovieCreditsRequest();

            Action act = () => request.Validate();
            act.ShouldThrow<ArgumentNullException>();

            // empty id
            request = new TraktPersonMovieCreditsRequest { Id = string.Empty };

            act = () => request.Validate();
            act.ShouldThrow<ArgumentException>();

            // id with spaces
            request = new TraktPersonMovieCreditsRequest { Id = "invalid id" };

            act = () => request.Validate();
            act.ShouldThrow<ArgumentException>();
        }
    }
}
