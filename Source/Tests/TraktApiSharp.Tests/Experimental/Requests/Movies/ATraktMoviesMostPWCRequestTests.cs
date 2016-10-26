﻿namespace TraktApiSharp.Tests.Experimental.Requests.Movies
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Movies;

    [TestClass]
    public class ATraktMoviesMostPWCRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestATraktMoviesMostPWCRequestIsAbstract()
        {
            typeof(ATraktMoviesMostPWCRequest<>).IsAbstract.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestATraktMoviesMostPWCRequestHasGenericTypeParameter()
        {
            typeof(ATraktMoviesMostPWCRequest<>).ContainsGenericParameters.Should().BeTrue();
            typeof(ATraktMoviesMostPWCRequest<int>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(1);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestATraktMoviesMostPWCRequestsSubclassOfATraktMoviesRequest()
        {
            typeof(ATraktMoviesMostPWCRequest<int>).IsSubclassOf(typeof(ATraktMoviesRequest<int>)).Should().BeTrue();
        }
    }
}
