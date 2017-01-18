﻿namespace TraktApiSharp.Tests.Experimental.Requests.Base.Delete
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Delete;
    using TraktApiSharp.Experimental.Requests.Interfaces.Base.Delete;

    [TestClass]
    public class ATraktNoContentDeleteByIdRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Delete")]
        public void TestATraktNoContentDeleteByIdRequestIsAbstract()
        {
            typeof(ATraktNoContentDeleteByIdRequest).IsAbstract.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Delete")]
        public void TestATraktNoContentDeleteByIdRequestIsSubclassOfATraktNoContentDeleteRequest()
        {
            typeof(ATraktNoContentDeleteByIdRequest).IsSubclassOf(typeof(ATraktNoContentDeleteRequest)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Delete")]
        public void TestATraktNoContentDeleteByIdRequestImplementsITraktNoContentDeleteByIdRequestInterface()
        {
            typeof(ATraktNoContentDeleteByIdRequest).GetInterfaces().Should().Contain(typeof(ITraktNoContentDeleteByIdRequest));
        }
    }
}
