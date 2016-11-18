﻿namespace TraktApiSharp.Tests.Experimental.Requests.Shows
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Shows;
    using TraktApiSharp.Objects.Basic;

    [TestClass]
    public class TraktShowPeopleRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowPeopleRequestIsNotAbstract()
        {
            typeof(TraktShowPeopleRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowPeopleRequestIsSealed()
        {
            typeof(TraktShowPeopleRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowPeopleRequestIsSubclassOfATraktSingleItemGetByIdRequest()
        {
            typeof(TraktShowPeopleRequest).IsSubclassOf(typeof(ATraktSingleItemGetByIdRequest<TraktCastAndCrew>)).Should().BeTrue();
        }
    }
}
