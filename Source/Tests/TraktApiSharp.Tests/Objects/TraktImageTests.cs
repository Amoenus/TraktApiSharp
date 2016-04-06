﻿namespace TraktApiSharp.Tests.Objects
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using TraktApiSharp.Objects;
    using Utils;

    [TestClass]
    public class TraktImageTests
    {
        [TestMethod]
        public void TestTraktImageDefaultConstructor()
        {
            var image = new TraktImage();

            image.Full.Should().BeNullOrEmpty();
            image.FullUri.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktImageReadFromJson()
        {
            var jsonFile = TestUtility.ReadJsonData(@"Basic\Image.json");

            jsonFile.Should().NotBeNullOrEmpty();

            var image = JsonConvert.DeserializeObject<TraktImage>(jsonFile);

            image.Should().NotBeNull();
            image.Full.Should().Be("https://walter.trakt.us/images/shows/000/060/300/logos/original/ab151d1043.png");
            image.FullUri.OriginalString.Should().Be("https://walter.trakt.us/images/shows/000/060/300/logos/original/ab151d1043.png");
        }
    }
}
