﻿namespace TraktApiSharp.Tests.Experimental.Requests.Base.Put
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base;
    using TraktApiSharp.Experimental.Requests.Base.Put;
    using TraktApiSharp.Experimental.Requests.Interfaces.Base.Put;

    [TestClass]
    public class ATraktSingleItemPutRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktSingleItemPutRequestIsAbstract()
        {
            typeof(ATraktSingleItemPutRequest<,>).IsAbstract.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktSingleItemPutRequestIsSubclassOfATraktSingleItemRequest()
        {
            typeof(ATraktSingleItemPutRequest<int, float>).IsSubclassOf(typeof(ATraktSingleItemRequest<int>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktSingleItemPutRequestHasGenericTypeParameter()
        {
            typeof(ATraktSingleItemPutRequest<,>).ContainsGenericParameters.Should().BeTrue();
            typeof(ATraktSingleItemPutRequest<int, float>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(2);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktSingleItemPutRequestImplementsITraktSingleItemPutRequestInterface()
        {
            typeof(ATraktSingleItemPutRequest<int, float>).GetInterfaces().Should().Contain(typeof(ITraktSingleItemPutRequest<int, float>));
        }
    }
}
