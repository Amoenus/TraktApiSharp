﻿namespace TraktApiSharp.Tests.Experimental.Requests.Shows
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Interfaces;
    using TraktApiSharp.Experimental.Requests.Shows;
    using TraktApiSharp.Objects.Get.Shows;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktShowTranslationsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowTranslationsRequestIsNotAbstract()
        {
            typeof(TraktShowTranslationsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowTranslationsRequestIsSealed()
        {
            typeof(TraktShowTranslationsRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowTranslationsRequestIsSubclassOfATraktListGetByIdRequest()
        {
            typeof(TraktShowTranslationsRequest).IsSubclassOf(typeof(ATraktListGetByIdRequest<TraktShowTranslation>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowTranslationsRequestHasAuthorizationNotRequired()
        {
            var request = new TraktShowTranslationsRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowTranslationsRequestHasValidUriTemplate()
        {
            var request = new TraktShowTranslationsRequest(null);
            request.UriTemplate.Should().Be("shows/{id}/translations");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowTranslationsRequestImplementsITraktObjectRequestInterface()
        {
            typeof(TraktShowTranslationsRequest).GetInterfaces().Should().Contain(typeof(ITraktObjectRequest));
        }
    }
}
