﻿namespace TraktApiSharp.Tests.Modules
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using TraktApiSharp.Exceptions;
    using TraktApiSharp.Modules;
    using TraktApiSharp.Objects.Basic;
    using TraktApiSharp.Objects.Get.People;
    using TraktApiSharp.Objects.Get.People.Credits;
    using TraktApiSharp.Requests;
    using Utils;

    [TestClass]
    public class TraktPeopleModuleTests
    {
        [TestMethod]
        public void TestTraktPeopleModuleIsModule()
        {
            typeof(TraktBaseModule).IsAssignableFrom(typeof(TraktPeopleModule)).Should().BeTrue();
        }

        [ClassInitialize]
        public static void InitializeTests(TestContext context)
        {
            TestUtility.SetupMockHttpClient();
        }

        [ClassCleanup]
        public static void CleanupTests()
        {
            TestUtility.ResetMockHttpClient();
        }

        [TestCleanup]
        public void CleanupSingleTest()
        {
            TestUtility.ClearMockHttpClient();
        }

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region SinglePerson

        [TestMethod]
        public void TestTraktPeopleModuleGetPerson()
        {
            var person = TestUtility.ReadFileContents(@"Objects\Get\People\PersonFullAndImages.json");
            person.Should().NotBeNullOrEmpty();

            var personId = "297737";

            TestUtility.SetupMockResponseWithoutOAuth($"people/{personId}", person);

            var response = TestUtility.MOCK_TEST_CLIENT.People.GetPersonAsync(personId).Result;

            response.Should().NotBeNull();
            response.Name.Should().Be("Bryan Cranston");
            response.Ids.Should().NotBeNull();
            response.Ids.Trakt.Should().Be(297737);
            response.Ids.Slug.Should().Be("bryan-cranston");
            response.Ids.Imdb.Should().Be("nm0186505");
            response.Ids.Tmdb.Should().Be(17419);
            response.Ids.TvRage.Should().Be(1797);
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonWithExtendedOption()
        {
            var person = TestUtility.ReadFileContents(@"Objects\Get\People\PersonFullAndImages.json");
            person.Should().NotBeNullOrEmpty();

            var personId = "297737";

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithoutOAuth($"people/{personId}?extended={extendedOption.ToString()}", person);

            var response = TestUtility.MOCK_TEST_CLIENT.People.GetPersonAsync(personId, extendedOption).Result;

            response.Should().NotBeNull();
            response.Name.Should().Be("Bryan Cranston");
            response.Ids.Should().NotBeNull();
            response.Ids.Trakt.Should().Be(297737);
            response.Ids.Slug.Should().Be("bryan-cranston");
            response.Ids.Imdb.Should().Be("nm0186505");
            response.Ids.Tmdb.Should().Be(17419);
            response.Ids.TvRage.Should().Be(1797);
            response.Images.Should().NotBeNull();
            response.Images.Headshot.Should().NotBeNull();
            response.Images.Headshot.Full.Should().Be("https://walter.trakt.us/images/people/000/297/737/headshots/original/47aebaace9.jpg");
            response.Images.Headshot.Medium.Should().Be("https://walter.trakt.us/images/people/000/297/737/headshots/medium/47aebaace9.jpg");
            response.Images.Headshot.Thumb.Should().Be("https://walter.trakt.us/images/people/000/297/737/headshots/thumb/47aebaace9.jpg");
            response.Images.FanArt.Should().NotBeNull();
            response.Images.FanArt.Full.Should().Be("https://walter.trakt.us/images/people/000/297/737/fanarts/original/0e436db5dd.jpg");
            response.Images.FanArt.Medium.Should().Be("https://walter.trakt.us/images/people/000/297/737/fanarts/medium/0e436db5dd.jpg");
            response.Images.FanArt.Thumb.Should().Be("https://walter.trakt.us/images/people/000/297/737/fanarts/thumb/0e436db5dd.jpg");
            response.Biography.Should().Be("Bryan Lee Cranston (born March 7, 1956) is an American actor, voice actor, writer and director.He is perhaps best known for his roles as Hal, the father in the Fox situation comedy \"Malcolm in the Middle\", and as Walter White in the AMC drama series Breaking Bad, for which he has won three consecutive Outstanding Lead Actor in a Drama Series Emmy Awards. Other notable roles include Dr. Tim Whatley on Seinfeld, Doug Heffernan's neighbor in The King of Queens, Astronaut Buzz Aldrin in From the Earth to the Moon, and Ted Mosby's boss on How I Met Your Mother. Description above from the Wikipedia article Bryan Cranston, licensed under CC-BY-SA, full list of contributors on Wikipedia.");
            response.Birthday.Should().Be(DateTime.Parse("1956-03-07T00:00:00Z").ToUniversalTime());
            response.Death.Should().Be(DateTime.Parse("2016-04-06T00:00:00Z").ToUniversalTime());
            response.Age.Should().Be(60);
            response.Birthplace.Should().Be("San Fernando Valley, California, USA");
            response.Homepage.Should().Be("http://www.bryancranston.com/");
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonExceptions()
        {
            var personId = "297737";
            var uri = $"people/{personId}";

            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktPerson>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonAsync(personId);
            act.ShouldThrow<TraktPersonNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonArgumentExceptions()
        {
            var person = TestUtility.ReadFileContents(@"Objects\Get\People\PersonFullAndImages.json");
            person.Should().NotBeNullOrEmpty();

            var personId = "297737";

            TestUtility.SetupMockResponseWithoutOAuth($"people/{personId}", person);

            Func<Task<TraktPerson>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MultiplePerson

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonsArgumentExceptions()
        {
            Func<Task<TraktListResult<TraktPerson>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonsAsync(new string[] { null });
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonsAsync(new string[] { string.Empty });
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonsAsync(new string[] { });
            act.ShouldNotThrow();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonsAsync(null);
            act.ShouldNotThrow();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region PersonMovieCredits

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonMovieCredits()
        {
            var personMovieCredits = TestUtility.ReadFileContents(@"Objects\Get\People\Credits\PersonMovieCredits.json");
            personMovieCredits.Should().NotBeNullOrEmpty();

            var personId = "297737";

            TestUtility.SetupMockResponseWithoutOAuth($"people/{personId}/movies", personMovieCredits);

            var response = TestUtility.MOCK_TEST_CLIENT.People.GetPersonMovieCreditsAsync(personId).Result;

            response.Should().NotBeNull();

            response.Cast.Should().NotBeNull().And.HaveCount(2);

            var cast = response.Cast.ToArray();

            cast[0].Character.Should().Be("Li (voice)");
            cast[0].Movie.Should().NotBeNull();
            cast[0].Movie.Title.Should().Be("Kung Fu Panda 3");
            cast[0].Movie.Year.Should().Be(2016);
            cast[0].Movie.Ids.Should().NotBeNull();
            cast[0].Movie.Ids.Trakt.Should().Be(93870);
            cast[0].Movie.Ids.Slug.Should().Be("kung-fu-panda-3-2016");
            cast[0].Movie.Ids.Imdb.Should().Be("tt2267968");
            cast[0].Movie.Ids.Tmdb.Should().Be(140300);

            cast[1].Character.Should().Be("Joe Brody");
            cast[1].Movie.Should().NotBeNull();
            cast[1].Movie.Title.Should().Be("Godzilla");
            cast[1].Movie.Year.Should().Be(2014);
            cast[1].Movie.Ids.Should().NotBeNull();
            cast[1].Movie.Ids.Trakt.Should().Be(24);
            cast[1].Movie.Ids.Slug.Should().Be("godzilla-2014");
            cast[1].Movie.Ids.Imdb.Should().Be("tt0831387");
            cast[1].Movie.Ids.Tmdb.Should().Be(124905);

            response.Crew.Should().NotBeNull();
            response.Crew.Art.Should().BeNull();
            response.Crew.Camera.Should().BeNull();
            response.Crew.CostumeAndMakeup.Should().BeNull();
            response.Crew.Crew.Should().BeNull();
            response.Crew.Directing.Should().NotBeNull().And.HaveCount(1);

            var directing = response.Crew.Directing.ToArray();

            directing[0].Job.Should().Be("Director");
            directing[0].Movie.Should().NotBeNull();
            directing[0].Movie.Title.Should().Be("Godzilla");
            directing[0].Movie.Year.Should().Be(2014);
            directing[0].Movie.Ids.Should().NotBeNull();
            directing[0].Movie.Ids.Trakt.Should().Be(24);
            directing[0].Movie.Ids.Slug.Should().Be("godzilla-2014");
            directing[0].Movie.Ids.Imdb.Should().Be("tt0831387");
            directing[0].Movie.Ids.Tmdb.Should().Be(124905);

            response.Crew.Editing.Should().BeNull();
            response.Crew.Lighting.Should().BeNull();
            response.Crew.Production.Should().NotBeNull().And.HaveCount(1);

            var production = response.Crew.Production.ToArray();

            production[0].Job.Should().Be("Producer");
            production[0].Movie.Should().NotBeNull();
            production[0].Movie.Title.Should().Be("Godzilla");
            production[0].Movie.Year.Should().Be(2014);
            production[0].Movie.Ids.Should().NotBeNull();
            production[0].Movie.Ids.Trakt.Should().Be(24);
            production[0].Movie.Ids.Slug.Should().Be("godzilla-2014");
            production[0].Movie.Ids.Imdb.Should().Be("tt0831387");
            production[0].Movie.Ids.Tmdb.Should().Be(124905);

            response.Crew.Sound.Should().BeNull();
            response.Crew.VisualEffects.Should().BeNull();
            response.Crew.Writing.Should().NotBeNull().And.HaveCount(1);

            var writing = response.Crew.Writing.ToArray();

            writing[0].Job.Should().Be("Screenplay");
            writing[0].Movie.Should().NotBeNull();
            writing[0].Movie.Title.Should().Be("Godzilla");
            writing[0].Movie.Year.Should().Be(2014);
            writing[0].Movie.Ids.Should().NotBeNull();
            writing[0].Movie.Ids.Trakt.Should().Be(24);
            writing[0].Movie.Ids.Slug.Should().Be("godzilla-2014");
            writing[0].Movie.Ids.Imdb.Should().Be("tt0831387");
            writing[0].Movie.Ids.Tmdb.Should().Be(124905);
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonMovieCreditsWithExtendedOption()
        {
            var personMovieCredits = TestUtility.ReadFileContents(@"Objects\Get\People\Credits\PersonMovieCredits.json");
            personMovieCredits.Should().NotBeNullOrEmpty();

            var personId = "297737";

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithoutOAuth($"people/{personId}/movies?extended={extendedOption.ToString()}",
                                                      personMovieCredits);

            var response = TestUtility.MOCK_TEST_CLIENT.People.GetPersonMovieCreditsAsync(personId, extendedOption).Result;

            response.Should().NotBeNull();

            response.Cast.Should().NotBeNull().And.HaveCount(2);

            var cast = response.Cast.ToArray();

            cast[0].Character.Should().Be("Li (voice)");
            cast[0].Movie.Should().NotBeNull();
            cast[0].Movie.Title.Should().Be("Kung Fu Panda 3");
            cast[0].Movie.Year.Should().Be(2016);
            cast[0].Movie.Ids.Should().NotBeNull();
            cast[0].Movie.Ids.Trakt.Should().Be(93870);
            cast[0].Movie.Ids.Slug.Should().Be("kung-fu-panda-3-2016");
            cast[0].Movie.Ids.Imdb.Should().Be("tt2267968");
            cast[0].Movie.Ids.Tmdb.Should().Be(140300);

            cast[1].Character.Should().Be("Joe Brody");
            cast[1].Movie.Should().NotBeNull();
            cast[1].Movie.Title.Should().Be("Godzilla");
            cast[1].Movie.Year.Should().Be(2014);
            cast[1].Movie.Ids.Should().NotBeNull();
            cast[1].Movie.Ids.Trakt.Should().Be(24);
            cast[1].Movie.Ids.Slug.Should().Be("godzilla-2014");
            cast[1].Movie.Ids.Imdb.Should().Be("tt0831387");
            cast[1].Movie.Ids.Tmdb.Should().Be(124905);

            response.Crew.Should().NotBeNull();
            response.Crew.Art.Should().BeNull();
            response.Crew.Camera.Should().BeNull();
            response.Crew.CostumeAndMakeup.Should().BeNull();
            response.Crew.Crew.Should().BeNull();
            response.Crew.Directing.Should().NotBeNull().And.HaveCount(1);

            var directing = response.Crew.Directing.ToArray();

            directing[0].Job.Should().Be("Director");
            directing[0].Movie.Should().NotBeNull();
            directing[0].Movie.Title.Should().Be("Godzilla");
            directing[0].Movie.Year.Should().Be(2014);
            directing[0].Movie.Ids.Should().NotBeNull();
            directing[0].Movie.Ids.Trakt.Should().Be(24);
            directing[0].Movie.Ids.Slug.Should().Be("godzilla-2014");
            directing[0].Movie.Ids.Imdb.Should().Be("tt0831387");
            directing[0].Movie.Ids.Tmdb.Should().Be(124905);

            response.Crew.Editing.Should().BeNull();
            response.Crew.Lighting.Should().BeNull();
            response.Crew.Production.Should().NotBeNull().And.HaveCount(1);

            var production = response.Crew.Production.ToArray();

            production[0].Job.Should().Be("Producer");
            production[0].Movie.Should().NotBeNull();
            production[0].Movie.Title.Should().Be("Godzilla");
            production[0].Movie.Year.Should().Be(2014);
            production[0].Movie.Ids.Should().NotBeNull();
            production[0].Movie.Ids.Trakt.Should().Be(24);
            production[0].Movie.Ids.Slug.Should().Be("godzilla-2014");
            production[0].Movie.Ids.Imdb.Should().Be("tt0831387");
            production[0].Movie.Ids.Tmdb.Should().Be(124905);

            response.Crew.Sound.Should().BeNull();
            response.Crew.VisualEffects.Should().BeNull();
            response.Crew.Writing.Should().NotBeNull().And.HaveCount(1);

            var writing = response.Crew.Writing.ToArray();

            writing[0].Job.Should().Be("Screenplay");
            writing[0].Movie.Should().NotBeNull();
            writing[0].Movie.Title.Should().Be("Godzilla");
            writing[0].Movie.Year.Should().Be(2014);
            writing[0].Movie.Ids.Should().NotBeNull();
            writing[0].Movie.Ids.Trakt.Should().Be(24);
            writing[0].Movie.Ids.Slug.Should().Be("godzilla-2014");
            writing[0].Movie.Ids.Imdb.Should().Be("tt0831387");
            writing[0].Movie.Ids.Tmdb.Should().Be(124905);
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonMovieCreditsExceptions()
        {
            var personId = "297737";
            var uri = $"people/{personId}/movies";

            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktPersonMovieCredits>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonMovieCreditsAsync(personId);
            act.ShouldThrow<TraktPersonNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonMovieCreditsArgumentExceptions()
        {
            var personMovieCredits = TestUtility.ReadFileContents(@"Objects\Get\People\Credits\PersonMovieCredits.json");
            personMovieCredits.Should().NotBeNullOrEmpty();

            var personId = "297737";

            TestUtility.SetupMockResponseWithoutOAuth($"people/{personId}/movies", personMovieCredits);

            Func<Task<TraktPersonMovieCredits>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonMovieCreditsAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.People.GetPersonMovieCreditsAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region PersonShowCredits

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonShowCredits()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonShowCreditsWithExtendedOption()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonShowCreditsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktPeopleModuleGetPersonShowCreditsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion
    }
}
