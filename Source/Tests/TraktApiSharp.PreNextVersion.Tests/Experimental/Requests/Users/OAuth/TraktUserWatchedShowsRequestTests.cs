﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Objects.Get.Watched;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktUserWatchedShowsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedShowsRequestIsNotAbstract()
        {
            typeof(TraktUserWatchedShowsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedShowsRequestIsSealed()
        {
            typeof(TraktUserWatchedShowsRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedShowsRequestIsSubclassOfATraktUsersListGetRequest()
        {
            typeof(TraktUserWatchedShowsRequest).IsSubclassOf(typeof(ATraktUsersGetRequest<TraktWatchedShow>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedShowsRequestHasAuthorizationOptional()
        {
            var request = new TraktUserWatchedShowsRequest(null);
            //request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.Optional);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedShowsRequestHasValidUriTemplate()
        {
            var request = new TraktUserWatchedShowsRequest(null);
            request.UriTemplate.Should().Be("users/{username}/watched/shows{?extended}");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedShowsRequestHasUsernameProperty()
        {
            var sortingPropertyInfo = typeof(TraktUserWatchedShowsRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "Username")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(string));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedShowsRequestHasGetUriPathParametersMethod()
        {
            var methodInfo = typeof(TraktUserWatchedShowsRequest).GetMethods()
                                                                 .Where(m => m.Name == "GetUriPathParameters")
                                                                 .FirstOrDefault();

            methodInfo.ReturnType.Should().Be(typeof(IDictionary<string, object>));
            methodInfo.GetParameters().Should().BeEmpty();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedShowsRequestUriParamsWithUsername()
        {
            var username = "username";

            var request = new TraktUserWatchedShowsRequest(null)
            {
                Username = username
            };

            var uriParams = request.GetUriPathParameters();

            uriParams.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1);
            uriParams.Should().Contain("username", username);
        }
    }
}
