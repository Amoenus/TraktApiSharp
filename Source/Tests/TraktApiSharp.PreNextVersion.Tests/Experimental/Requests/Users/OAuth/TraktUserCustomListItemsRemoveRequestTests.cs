﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Objects.Post.Users.CustomListItems;
    using TraktApiSharp.Objects.Post.Users.CustomListItems.Responses;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktUserCustomListItemsRemoveRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRemoveRequestIsNotAbstract()
        {
            typeof(TraktUserCustomListItemsRemoveRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRemoveRequestIsSealed()
        {
            typeof(TraktUserCustomListItemsRemoveRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRemoveRequestIsSubclassOfATraktUsersPostByIdRequest()
        {
            typeof(TraktUserCustomListItemsRemoveRequest).IsSubclassOf(typeof(ATraktUsersPostByIdRequest<TraktUserCustomListItemsRemovePostResponse, TraktUserCustomListItemsPost>)).Should().BeTrue();
        }

        //[TestMethod, TestCategory("Requests"), TestCategory("Users")]
        //public void TestTraktUserCustomListItemsRemoveRequestHasAuthorizationRequired()
        //{
        //    var request = new TraktUserCustomListItemsRemoveRequest(null);
        //    request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.Required);
        //}

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRemoveRequestHasValidUriTemplate()
        {
            var request = new TraktUserCustomListItemsRemoveRequest(null);
            request.UriTemplate.Should().Be("users/{username}/lists/{id}/items/remove");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRemoveRequestHasValidRequestObjectType()
        {
            var request = new TraktUserCustomListItemsRemoveRequest(null);
            request.RequestObjectType.Should().Be(TraktRequestObjectType.Lists);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRemoveRequestHasUsernameProperty()
        {
            var sortingPropertyInfo = typeof(TraktUserCustomListItemsRemoveRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "Username")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(string));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRemoveRequestHasGetUriPathParametersMethod()
        {
            var methodInfo = typeof(TraktUserCustomListItemsRemoveRequest).GetMethods()
                                                                          .Where(m => m.Name == "GetUriPathParameters")
                                                                          .FirstOrDefault();

            methodInfo.ReturnType.Should().Be(typeof(IDictionary<string, object>));
            methodInfo.GetParameters().Should().BeEmpty();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRemoveRequestUriParamsWithUsername()
        {
            var username = "username";

            var request = new TraktUserCustomListItemsRemoveRequest(null)
            {
                Username = username
            };

            var uriParams = request.GetUriPathParameters();

            uriParams.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1);
            uriParams.Should().Contain("username", username);
        }
    }
}
