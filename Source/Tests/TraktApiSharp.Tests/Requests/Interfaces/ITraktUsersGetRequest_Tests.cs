﻿namespace TraktApiSharp.Tests.Requests.Interfaces
{
    using FluentAssertions;
    using Traits;
    using TraktApiSharp.Requests.Interfaces;
    using Xunit;

    [Category("Requests.Interfaces")]
    public class ITraktUsersGetRequest_Tests
    {
        [Fact]
        public void Test_ITraktUsersGetRequest_Is_Interface()
        {
            typeof(ITraktUsersGetRequest).IsInterface.Should().BeTrue();
        }
    }
}
