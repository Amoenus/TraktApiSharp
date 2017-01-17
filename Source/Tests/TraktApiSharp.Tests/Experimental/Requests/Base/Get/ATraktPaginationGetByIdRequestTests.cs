﻿namespace TraktApiSharp.Tests.Experimental.Requests.Base.Get
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Interfaces.Base.Get;

    [TestClass]
    public class ATraktPaginationGetByIdRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Get")]
        public void TestATraktPaginationGetByIdRequestIsAbstract()
        {
            typeof(ATraktPaginationGetByIdRequest<>).IsAbstract.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Get")]
        public void TestATraktPaginationGetByIdRequestIsSubclassOfATraktPaginationGetRequest()
        {
            typeof(ATraktPaginationGetByIdRequest<int>).IsSubclassOf(typeof(ATraktPaginationGetRequest<int>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Get")]
        public void TestATraktPaginationGetByIdRequestHasGenericTypeParameter()
        {
            typeof(ATraktPaginationGetByIdRequest<>).ContainsGenericParameters.Should().BeTrue();
            typeof(ATraktPaginationGetByIdRequest<int>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(1);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Get")]
        public void TestATraktPaginationGetByIdRequestImplementsITraktPaginationGetByIdRequestInterface()
        {
            typeof(ATraktPaginationGetByIdRequest<int>).GetInterfaces().Should().Contain(typeof(ITraktPaginationGetByIdRequest<int>));
        }
    }
}
