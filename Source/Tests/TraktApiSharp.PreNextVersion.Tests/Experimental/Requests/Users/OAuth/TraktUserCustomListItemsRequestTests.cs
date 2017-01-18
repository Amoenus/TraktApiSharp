﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Enums;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Objects.Get.Users.Lists;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktUserCustomListItemsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestIsNotAbstract()
        {
            typeof(TraktUserCustomListItemsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestIsSealed()
        {
            typeof(TraktUserCustomListItemsRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestIsSubclassOfATraktListGetByIdRequest()
        {
            typeof(TraktUserCustomListItemsRequest).IsSubclassOf(typeof(ATraktListGetByIdRequest<TraktListItem>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestHasValidRequestObjectType()
        {
            var request = new TraktUserCustomListItemsRequest(null);
            request.RequestObjectType.Should().Be(TraktRequestObjectType.Lists);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestHasAuthorizationOptional()
        {
            var request = new TraktUserCustomListItemsRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.Optional);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestHasValidUriTemplate()
        {
            var request = new TraktUserCustomListItemsRequest(null);
            request.UriTemplate.Should().Be("users/{username}/lists/{id}/items{/type}{?extended}");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestHasUsernameProperty()
        {
            var sortingPropertyInfo = typeof(TraktUserCustomListItemsRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "Username")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(string));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestHasTypeProperty()
        {
            var sortingPropertyInfo = typeof(TraktUserCustomListItemsRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "Type")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(TraktListItemType));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestHasGetUriPathParametersMethod()
        {
            var methodInfo = typeof(TraktUserCustomListItemsRequest).GetMethods()
                                                                    .Where(m => m.Name == "GetUriPathParameters")
                                                                    .FirstOrDefault();

            methodInfo.ReturnType.Should().Be(typeof(IDictionary<string, object>));
            methodInfo.GetParameters().Should().BeEmpty();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestUriParamsWithUsernameAndWithoutType()
        {
            var username = "username";

            var request = new TraktUserCustomListItemsRequest(null)
            {
                Username = username
            };

            var uriParams = request.GetUriPathParameters();

            uriParams.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1);
            uriParams.Should().Contain("username", username);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestUriParamsWithUsernameAndUnspecifiedType()
        {
            var username = "username";
            var type = TraktListItemType.Unspecified;

            var request = new TraktUserCustomListItemsRequest(null)
            {
                Username = username,
                Type = type
            };

            var uriParams = request.GetUriPathParameters();

            uriParams.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1);
            uriParams.Should().Contain("username", username);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserCustomListItemsRequestUriParamsWithUsernameAndType()
        {
            var username = "username";
            var type = TraktListItemType.Person;

            var request = new TraktUserCustomListItemsRequest(null)
            {
                Username = username,
                Type = type
            };

            var uriParams = request.GetUriPathParameters();

            uriParams.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);
            uriParams.Should().Contain(new Dictionary<string, object>
            {
                ["username"] = username,
                ["type"] = type.UriName
            });
        }
    }
}
