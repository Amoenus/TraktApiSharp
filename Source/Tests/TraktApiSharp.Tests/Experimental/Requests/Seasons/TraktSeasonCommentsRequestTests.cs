﻿namespace TraktApiSharp.Tests.Experimental.Requests.Seasons
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Enums;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Interfaces;
    using TraktApiSharp.Experimental.Requests.Seasons;
    using TraktApiSharp.Objects.Basic;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktSeasonCommentsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonCommentsRequestIsNotAbstract()
        {
            typeof(TraktSeasonCommentsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonCommentsRequestIsSealed()
        {
            typeof(TraktSeasonCommentsRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonCommentsRequestIsSubclassOfATraktPaginationGetByIdRequest()
        {
            typeof(TraktSeasonCommentsRequest).IsSubclassOf(typeof(ATraktPaginationGetByIdRequest<TraktComment>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonCommentsRequestHasAuthorizationNotRequired()
        {
            var request = new TraktSeasonCommentsRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonCommentsRequestHasValidUriTemplate()
        {
            var request = new TraktSeasonCommentsRequest(null);
            request.UriTemplate.Should().Be("shows/{id}/seasons/{season}/comments{/sorting}{?page,limit}");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonCommentsRequestImplementsITraktObjectRequestInterface()
        {
            typeof(TraktSeasonCommentsRequest).GetInterfaces().Should().Contain(typeof(ITraktObjectRequest));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonCommentsRequestHasSeasonNumberProperty()
        {
            var sortingPropertyInfo = typeof(TraktSeasonCommentsRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "SeasonNumber")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(int));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonCommentsRequestHasSortingProperty()
        {
            var sortingPropertyInfo = typeof(TraktSeasonCommentsRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "Sorting")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(TraktCommentSortOrder));
        }
    }
}
