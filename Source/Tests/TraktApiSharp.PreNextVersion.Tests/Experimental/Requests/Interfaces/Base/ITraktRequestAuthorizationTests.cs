﻿namespace TraktApiSharp.Tests.Experimental.Requests.Interfaces.Base
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using TraktApiSharp.Experimental.Requests.Interfaces.Base;
    using TraktApiSharp.Requests;

    [TestClass]
    public class ITraktRequestAuthorizationTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktRequestAuthorizationIsInterface()
        {
            typeof(ITraktHasRequestAuthorization).IsInterface.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktRequestAuthorizationHasAuthorizationRequirementProperty()
        {
            var authorizationRequirementPropertyInfo = typeof(ITraktHasRequestAuthorization).GetProperties()
                                                                                         .Where(p => p.Name == "AuthorizationRequirement")
                                                                                         .FirstOrDefault();

            authorizationRequirementPropertyInfo.CanRead.Should().BeTrue();
            authorizationRequirementPropertyInfo.CanWrite.Should().BeFalse();
            authorizationRequirementPropertyInfo.PropertyType.Should().Be(typeof(TraktAuthorizationRequirement));
        }
    }
}
