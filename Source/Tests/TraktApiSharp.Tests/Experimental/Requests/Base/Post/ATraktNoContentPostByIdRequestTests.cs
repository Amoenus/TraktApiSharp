﻿namespace TraktApiSharp.Tests.Experimental.Requests.Base.Post
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Post;
    using TraktApiSharp.Experimental.Requests.Interfaces;

    [TestClass]
    public class ATraktNoContentPostByIdRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktNoContentPostByIdRequestIsAbstract()
        {
            typeof(ATraktNoContentPostByIdRequest<>).IsAbstract.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktNoContentPostByIdRequestIsSubclassOfATraktNoContentPostRequest()
        {
            typeof(ATraktNoContentPostByIdRequest<float>).IsSubclassOf(typeof(ATraktNoContentPostRequest<float>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktNoContentPostByIdRequestHasGenericTypeParameter()
        {
            typeof(ATraktNoContentPostByIdRequest<>).ContainsGenericParameters.Should().BeTrue();
            typeof(ATraktNoContentPostByIdRequest<float>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(1);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktNoContentPostByIdRequestImplementsITraktHasRequestBodyInterface()
        {
            typeof(ATraktNoContentPostByIdRequest<float>).GetInterfaces().Should().Contain(typeof(ITraktHasRequestBody<float>));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktNoContentPostByIdRequestImplementsITraktHasIdInterface()
        {
            typeof(ATraktNoContentPostByIdRequest<>).GetInterfaces().Should().Contain(typeof(ITraktHasId));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktNoContentPostByIdRequestImplementsITraktObjectRequestInterface()
        {
            typeof(ATraktNoContentPostByIdRequest<>).GetInterfaces().Should().Contain(typeof(ITraktObjectRequest));
        }
    }
}
