﻿namespace TraktApiSharp.Tests.Experimental.Requests.Interfaces.Base
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;
    using TraktApiSharp.Experimental.Requests.Interfaces.Base;
    using TraktApiSharp.Experimental.Responses.Interfaces.Base;

    [TestClass]
    public class ITraktListRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktListRequestIsInterface()
        {
            typeof(ITraktListRequest<>).IsInterface.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktListRequestDerivesFromITraktRequestInterface()
        {
            typeof(ITraktListRequest<>).GetInterfaces().Should().Contain(typeof(ITraktRequest));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktListRequestHasGenericTypeParameter()
        {
            typeof(ITraktListRequest<>).ContainsGenericParameters.Should().BeTrue();
            typeof(ITraktListRequest<int>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(1);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktListRequestHasQueryAsyncMethod()
        {
            var methodInfo = typeof(ITraktListRequest<int>).GetMethods()
                                                           .Where(m => m.Name == "QueryAsync")
                                                           .FirstOrDefault();

            methodInfo.ReturnType.Should().Be(typeof(Task<ITraktListResponse<int>>));
            methodInfo.GetParameters().Should().BeEmpty();
        }
    }
}
