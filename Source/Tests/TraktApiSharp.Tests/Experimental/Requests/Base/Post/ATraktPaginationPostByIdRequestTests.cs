﻿namespace TraktApiSharp.Tests.Experimental.Requests.Base.Post
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Post;
    using TraktApiSharp.Experimental.Requests.Interfaces.Base;

    [TestClass]
    public class ATraktPaginationPostByIdRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktPaginationPostByIdRequestIsAbstract()
        {
            typeof(ATraktPaginationPostByIdRequest<,>).IsAbstract.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktPaginationPostByIdRequestIsSubclassOfATraktPaginationPostRequest()
        {
            typeof(ATraktPaginationPostByIdRequest<int, float>).IsSubclassOf(typeof(ATraktPaginationPostRequest<int, float>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktPaginationPostByIdRequestHasGenericTypeParameter()
        {
            typeof(ATraktPaginationPostByIdRequest<,>).ContainsGenericParameters.Should().BeTrue();
            typeof(ATraktPaginationPostByIdRequest<int, float>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(2);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktPaginationPostByIdRequestImplementsITraktHasRequestBodyInterface()
        {
            typeof(ATraktPaginationPostByIdRequest<int, float>).GetInterfaces().Should().Contain(typeof(ITraktHasRequestBody<float>));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post")]
        public void TestATraktPaginationPostByIdRequestImplementsITraktHasIdInterface()
        {
            typeof(ATraktPaginationPostByIdRequest<,>).GetInterfaces().Should().Contain(typeof(ITraktHasId));
        }
    }
}
