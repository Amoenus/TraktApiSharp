﻿namespace TraktApiSharp.Tests.Experimental.Requests.Interfaces.Base
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;
    using TraktApiSharp.Experimental.Requests.Interfaces.Base;
    using TraktApiSharp.Experimental.Responses;

    [TestClass]
    public class ITraktNoContentRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktNoContentRequestIsInterface()
        {
            typeof(ITraktNoContentRequest).IsInterface.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktNoContentRequestDerivesFromITraktRequestInterface()
        {
            typeof(ITraktNoContentRequest).GetInterfaces().Should().Contain(typeof(ITraktRequest));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Interfaces")]
        public void TestITraktNoContentRequestHasQueryAsyncMethod()
        {
            var methodInfo = typeof(ITraktNoContentRequest).GetMethods()
                                                           .Where(m => m.Name == "QueryAsync")
                                                           .FirstOrDefault();

            methodInfo.ReturnType.Should().Be(typeof(Task<TraktNoContentResponse>));
            methodInfo.GetParameters().Should().BeEmpty();
        }
    }
}
