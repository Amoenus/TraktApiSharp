﻿namespace TraktApiSharp.Tests.Requests.Users.OAuth
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using TraktApiSharp.Experimental.Requests.Base;
    using TraktApiSharp.Experimental.Requests.Interfaces;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Requests;
    using TraktApiSharp.Tests.Traits;
    using Xunit;

    [Category("Requests.Users.OAuth")]
    public class ATraktUsersDeleteByIdRequest_Tests
    {
        internal class TraktUsersDeleteByIdRequestMock : ATraktUsersDeleteByIdRequest
        {
            public override string UriTemplate { get { throw new NotImplementedException(); } }

            public override TraktRequestObjectType RequestObjectType { get { throw new NotImplementedException(); } }
        }

        [Fact]
        public void Test_ATraktUsersDeleteByIdRequest_Is_Abstract()
        {
            typeof(ATraktUsersDeleteByIdRequest).IsAbstract.Should().BeTrue();
        }

        [Fact]
        public void Test_ATraktUsersDeleteByIdRequest_Inherits_ATraktDeleteRequest()
        {
            typeof(ATraktUsersDeleteByIdRequest).IsSubclassOf(typeof(ATraktDeleteRequest)).Should().BeTrue();
        }

        [Fact]
        public void Test_ATraktUsersDeleteByIdRequest_Implements_ITraktHasId_Interface()
        {
            typeof(ATraktUsersDeleteByIdRequest).GetInterfaces().Should().Contain(typeof(ITraktHasId));
        }

        [Fact]
        public void Test_ATraktUsersDeleteByIdRequest_Has_AuthorizationRequirement_Required()
        {
            var request = new TraktUsersDeleteByIdRequestMock();
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.Required);
        }

        [Fact]
        public void Test_ATraktUsersDeleteByIdRequest_Returns_Valid_UriPathParameters()
        {
            var requestMock = new TraktUsersDeleteByIdRequestMock { Id = "123" };

            requestMock.GetUriPathParameters().Should().NotBeNull()
                                                       .And.HaveCount(1)
                                                       .And.Contain(new Dictionary<string, object>
                                                       {
                                                           ["id"] = "123"
                                                       });
        }

        [Fact]
        public void Test_ATraktUsersDeleteByIdRequest_Validate_Throws_Exceptions()
        {
            // id is null
            var requestMock = new TraktUsersDeleteByIdRequestMock();

            Action act = () => requestMock.Validate();
            act.ShouldThrow<ArgumentNullException>();

            // empty id
            requestMock = new TraktUsersDeleteByIdRequestMock { Id = string.Empty };

            act = () => requestMock.Validate();
            act.ShouldThrow<ArgumentException>();

            // id with spaces
            requestMock = new TraktUsersDeleteByIdRequestMock { Id = "invalid id" };

            act = () => requestMock.Validate();
            act.ShouldThrow<ArgumentException>();
        }
    }
}
