﻿namespace TraktApiSharp.Tests.Requests.Users.OAuth
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Objects.Get.Watched;
    using TraktApiSharp.Requests;
    using TraktApiSharp.Requests.Params;
    using TraktApiSharp.Tests.Traits;
    using Xunit;

    [Category("Requests.Users.OAuth")]
    public class TraktUserWatchedMoviesRequest_Tests
    {
        [Fact]
        public void Test_TraktUserWatchedMoviesRequest_Is_Not_Abstract()
        {
            typeof(TraktUserWatchedMoviesRequest).IsAbstract.Should().BeFalse();
        }

        [Fact]
        public void Test_TraktUserWatchedMoviesRequest_Is_Sealed()
        {
            typeof(TraktUserWatchedMoviesRequest).IsSealed.Should().BeTrue();
        }

        [Fact]
        public void Test_TraktUserWatchedMoviesRequest_Inherits_ATraktUsersGetRequest_1()
        {
            typeof(TraktUserWatchedMoviesRequest).IsSubclassOf(typeof(ATraktUsersGetRequest<TraktWatchedMovie>)).Should().BeTrue();
        }

        [Fact]
        public void Test_TraktUserWatchedMoviesRequest_Has_Username_Property()
        {
            var sortingPropertyInfo = typeof(TraktUserWatchedMoviesRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "Username")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(string));
        }

        [Fact]
        public void Test_TraktUserWatchedMoviesRequest_Has_AuthorizationRequirement_Optional()
        {
            var request = new TraktUserWatchedMoviesRequest();
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.Optional);
        }

        [Fact]
        public void Test_TraktUserWatchedMoviesRequest_Has_Valid_UriTemplate()
        {
            var request = new TraktUserWatchedMoviesRequest();
            request.UriTemplate.Should().Be("users/{username}/watched/movies{?extended}");
        }

        [Fact]
        public void Test_TraktUserWatchedMoviesRequest_Returns_Valid_UriPathParameters()
        {
            // without extended info
            var request = new TraktUserWatchedMoviesRequest { Username = "username" };

            request.GetUriPathParameters().Should().NotBeNull()
                                                   .And.HaveCount(1)
                                                   .And.Contain(new Dictionary<string, object>
                                                   {
                                                       ["username"] = "username"
                                                   });

            // with extended info
            var extendedInfo = new TraktExtendedInfo { Full = true };
            request = new TraktUserWatchedMoviesRequest { Username = "username", ExtendedInfo = extendedInfo };

            request.GetUriPathParameters().Should().NotBeNull()
                                                   .And.HaveCount(2)
                                                   .And.Contain(new Dictionary<string, object>
                                                   {
                                                       ["username"] = "username",
                                                       ["extended"] = extendedInfo.ToString()
                                                   });
        }

        [Fact]
        public void Test_TraktUserWatchedMoviesRequest_Validate_Throws_Exceptions()
        {
            // username is null
            var request = new TraktUserWatchedMoviesRequest();

            Action act = () => request.Validate();
            act.ShouldThrow<ArgumentNullException>();

            // empty username
            request = new TraktUserWatchedMoviesRequest { Username = string.Empty };

            act = () => request.Validate();
            act.ShouldThrow<ArgumentException>();

            // username with spaces
            request = new TraktUserWatchedMoviesRequest { Username = "invalid username" };

            act = () => request.Validate();
            act.ShouldThrow<ArgumentException>();
        }
    }
}
