﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Experimental.Requests.Base.Post.Bodyless;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktUserListLikeRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListLikeRequestIsNotAbstract()
        {
            typeof(TraktUserListLikeRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListLikeRequestIsSealed()
        {
            typeof(TraktUserListLikeRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListLikeRequestIsSubclassOfATraktNoContentBodylessPostByIdRequest()
        {
            typeof(TraktUserListLikeRequest).IsSubclassOf(typeof(ATraktNoContentBodylessPostByIdRequest)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListLikeRequestHasAuthorizationRequired()
        {
            var request = new TraktUserListLikeRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.Required);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListLikeRequestHasValidUriTemplate()
        {
            var request = new TraktUserListLikeRequest(null);
            request.UriTemplate.Should().Be("users/{username}/lists/{id}/like");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListLikeRequestHasValidRequestObjectType()
        {
            var request = new TraktUserListLikeRequest(null);
            request.RequestObjectType.Should().Be(TraktRequestObjectType.Lists);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListLikeRequestHasUsernameProperty()
        {
            var sortingPropertyInfo = typeof(TraktUserListLikeRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "Username")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(string));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListLikeRequestHasGetUriPathParametersMethod()
        {
            var methodInfo = typeof(TraktUserListLikeRequest).GetMethods()
                                                             .Where(m => m.Name == "GetUriPathParameters")
                                                             .FirstOrDefault();

            methodInfo.ReturnType.Should().Be(typeof(IDictionary<string, object>));
            methodInfo.GetParameters().Should().BeEmpty();
        }
    }
}
