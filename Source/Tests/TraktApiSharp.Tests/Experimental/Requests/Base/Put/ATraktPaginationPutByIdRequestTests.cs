﻿namespace TraktApiSharp.Tests.Experimental.Requests.Base.Put
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base;
    using TraktApiSharp.Experimental.Requests.Base.Put;

    [TestClass]
    public class ATraktPaginationPutByIdRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktPaginationPutByIdRequestIsAbstract()
        {
            typeof(ATraktPaginationPutByIdRequest<,>).IsAbstract.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktPaginationPutByIdRequestIsSubclassOfATraktPaginationPutRequest()
        {
            typeof(ATraktPaginationPutByIdRequest<int, float>).IsSubclassOf(typeof(ATraktPaginationPutRequest<int, float>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktPaginationPutByIdRequestHasGenericTypeParameter()
        {
            typeof(ATraktPaginationPutByIdRequest<,>).ContainsGenericParameters.Should().BeTrue();
            typeof(ATraktPaginationPutByIdRequest<int, float>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(2);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktPaginationPutByIdRequestImplementsITraktHasRequestBodyInterface()
        {
            typeof(ATraktPaginationPutByIdRequest<int, float>).GetInterfaces().Should().Contain(typeof(ITraktHasRequestBody<float>));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Put")]
        public void TestATraktPaginationPutByIdRequestImplementsITraktHasIdInterface()
        {
            typeof(ATraktPaginationPutByIdRequest<int, float>).GetInterfaces().Should().Contain(typeof(ITraktHasId));
        }
    }
}
