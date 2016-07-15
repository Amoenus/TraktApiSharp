﻿namespace TraktApiSharp.Tests.Objects.Post.Syncs.Collection
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using TraktApiSharp.Enums;
    using TraktApiSharp.Objects.Basic;
    using TraktApiSharp.Objects.Get.Movies;
    using TraktApiSharp.Objects.Get.Shows;
    using TraktApiSharp.Objects.Get.Shows.Episodes;
    using TraktApiSharp.Objects.Post.Syncs.Collection;
    using TraktApiSharp.Utils;

    [TestClass]
    public class TraktSyncCollectionPostBuilderTests
    {
        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderBuildExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            Action act = () => builder.Build();
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddMovie()
        {
            var movie1 = new TraktMovie
            {
                Title = "movie1",
                Year = 2016,
                Ids = new TraktMovieIds
                {
                    Trakt = 1,
                    Slug = "movie1",
                    Imdb = "imdb1",
                    Tmdb = 1234
                }
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddMovie(movie1);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            builder.AddMovie(movie1);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            movie1.Ids.Trakt = 2;

            builder.AddMovie(movie1);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            var movies = collectionPost.Movies.ToArray();

            movies[0].Should().NotBeNull();
            movies[0].Title.Should().Be("movie1");
            movies[0].Year.Should().Be(2016);
            movies[0].Ids.Should().NotBeNull();
            movies[0].Ids.Trakt.Should().Be(2);
            movies[0].Ids.Slug.Should().Be("movie1");
            movies[0].Ids.Imdb.Should().Be("imdb1");
            movies[0].Ids.Tmdb.Should().Be(1234);
            movies[0].CollectedAt.Should().NotHaveValue();
            movies[0].Metadata.Should().BeNull();

            var movie2 = new TraktMovie
            {
                Title = "movie2",
                Year = 2016,
                Ids = new TraktMovieIds
                {
                    Trakt = 3,
                    Slug = "movie2",
                    Imdb = "imdb2",
                    Tmdb = 12345
                }
            };

            builder.AddMovie(movie2);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(2);

            movies = collectionPost.Movies.ToArray();

            movies[0].Should().NotBeNull();
            movies[0].Title.Should().Be("movie1");
            movies[0].Year.Should().Be(2016);
            movies[0].Ids.Should().NotBeNull();
            movies[0].Ids.Trakt.Should().Be(2);
            movies[0].Ids.Slug.Should().Be("movie1");
            movies[0].Ids.Imdb.Should().Be("imdb1");
            movies[0].Ids.Tmdb.Should().Be(1234);
            movies[0].CollectedAt.Should().NotHaveValue();
            movies[0].Metadata.Should().BeNull();

            movies[1].Should().NotBeNull();
            movies[1].Title.Should().Be("movie2");
            movies[1].Year.Should().Be(2016);
            movies[1].Ids.Should().NotBeNull();
            movies[1].Ids.Trakt.Should().Be(3);
            movies[1].Ids.Slug.Should().Be("movie2");
            movies[1].Ids.Imdb.Should().Be("imdb2");
            movies[1].Ids.Tmdb.Should().Be(12345);
            movies[1].CollectedAt.Should().NotHaveValue();
            movies[1].Metadata.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddMovieWithMetadata()
        {
            var movie1 = new TraktMovie
            {
                Title = "movie1",
                Year = 2016,
                Ids = new TraktMovieIds
                {
                    Trakt = 1,
                    Slug = "movie1",
                    Imdb = "imdb1",
                    Tmdb = 1234
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddMovie(movie1, metadata);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            builder.AddMovie(movie1, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            movie1.Ids.Trakt = 2;

            builder.AddMovie(movie1, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            var movies = collectionPost.Movies.ToArray();

            movies[0].Should().NotBeNull();
            movies[0].Title.Should().Be("movie1");
            movies[0].Year.Should().Be(2016);
            movies[0].Ids.Should().NotBeNull();
            movies[0].Ids.Trakt.Should().Be(2);
            movies[0].Ids.Slug.Should().Be("movie1");
            movies[0].Ids.Imdb.Should().Be("imdb1");
            movies[0].Ids.Tmdb.Should().Be(1234);
            movies[0].CollectedAt.Should().NotHaveValue();
            movies[0].Metadata.Should().NotBeNull();

            var movie2 = new TraktMovie
            {
                Title = "movie2",
                Year = 2016,
                Ids = new TraktMovieIds
                {
                    Trakt = 3,
                    Slug = "movie2",
                    Imdb = "imdb2",
                    Tmdb = 12345
                }
            };

            builder.AddMovie(movie2, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(2);

            movies = collectionPost.Movies.ToArray();

            movies[0].Should().NotBeNull();
            movies[0].Title.Should().Be("movie1");
            movies[0].Year.Should().Be(2016);
            movies[0].Ids.Should().NotBeNull();
            movies[0].Ids.Trakt.Should().Be(2);
            movies[0].Ids.Slug.Should().Be("movie1");
            movies[0].Ids.Imdb.Should().Be("imdb1");
            movies[0].Ids.Tmdb.Should().Be(1234);
            movies[0].CollectedAt.Should().NotHaveValue();
            movies[0].Metadata.Should().NotBeNull();

            movies[1].Should().NotBeNull();
            movies[1].Title.Should().Be("movie2");
            movies[1].Year.Should().Be(2016);
            movies[1].Ids.Should().NotBeNull();
            movies[1].Ids.Trakt.Should().Be(3);
            movies[1].Ids.Slug.Should().Be("movie2");
            movies[1].Ids.Imdb.Should().Be("imdb2");
            movies[1].Ids.Tmdb.Should().Be(12345);
            movies[1].CollectedAt.Should().NotHaveValue();
            movies[1].Metadata.Should().NotBeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddMovieWithCollectedAt()
        {
            var movie1 = new TraktMovie
            {
                Title = "movie1",
                Year = 2016,
                Ids = new TraktMovieIds
                {
                    Trakt = 1,
                    Slug = "movie1",
                    Imdb = "imdb1",
                    Tmdb = 1234
                }
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddMovie(movie1, collectedAt);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            builder.AddMovie(movie1, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            movie1.Ids.Trakt = 2;

            builder.AddMovie(movie1, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            var movies = collectionPost.Movies.ToArray();

            movies[0].Should().NotBeNull();
            movies[0].Title.Should().Be("movie1");
            movies[0].Year.Should().Be(2016);
            movies[0].Ids.Should().NotBeNull();
            movies[0].Ids.Trakt.Should().Be(2);
            movies[0].Ids.Slug.Should().Be("movie1");
            movies[0].Ids.Imdb.Should().Be("imdb1");
            movies[0].Ids.Tmdb.Should().Be(1234);
            movies[0].CollectedAt.Should().HaveValue();
            movies[0].Metadata.Should().BeNull();

            var movie2 = new TraktMovie
            {
                Title = "movie2",
                Year = 2016,
                Ids = new TraktMovieIds
                {
                    Trakt = 3,
                    Slug = "movie2",
                    Imdb = "imdb2",
                    Tmdb = 12345
                }
            };

            builder.AddMovie(movie2, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(2);

            movies = collectionPost.Movies.ToArray();

            movies[0].Should().NotBeNull();
            movies[0].Title.Should().Be("movie1");
            movies[0].Year.Should().Be(2016);
            movies[0].Ids.Should().NotBeNull();
            movies[0].Ids.Trakt.Should().Be(2);
            movies[0].Ids.Slug.Should().Be("movie1");
            movies[0].Ids.Imdb.Should().Be("imdb1");
            movies[0].Ids.Tmdb.Should().Be(1234);
            movies[0].CollectedAt.Should().HaveValue();
            movies[0].Metadata.Should().BeNull();

            movies[1].Should().NotBeNull();
            movies[1].Title.Should().Be("movie2");
            movies[1].Year.Should().Be(2016);
            movies[1].Ids.Should().NotBeNull();
            movies[1].Ids.Trakt.Should().Be(3);
            movies[1].Ids.Slug.Should().Be("movie2");
            movies[1].Ids.Imdb.Should().Be("imdb2");
            movies[1].Ids.Tmdb.Should().Be(12345);
            movies[1].CollectedAt.Should().HaveValue();
            movies[1].Metadata.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddMovieWithMetadataAndCollectedAt()
        {
            var movie1 = new TraktMovie
            {
                Title = "movie1",
                Year = 2016,
                Ids = new TraktMovieIds
                {
                    Trakt = 1,
                    Slug = "movie1",
                    Imdb = "imdb1",
                    Tmdb = 1234
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddMovie(movie1, metadata, collectedAt);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            builder.AddMovie(movie1, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            movie1.Ids.Trakt = 2;

            builder.AddMovie(movie1, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(1);

            var movies = collectionPost.Movies.ToArray();

            movies[0].Should().NotBeNull();
            movies[0].Title.Should().Be("movie1");
            movies[0].Year.Should().Be(2016);
            movies[0].Ids.Should().NotBeNull();
            movies[0].Ids.Trakt.Should().Be(2);
            movies[0].Ids.Slug.Should().Be("movie1");
            movies[0].Ids.Imdb.Should().Be("imdb1");
            movies[0].Ids.Tmdb.Should().Be(1234);
            movies[0].CollectedAt.Should().HaveValue();
            movies[0].Metadata.Should().NotBeNull();

            var movie2 = new TraktMovie
            {
                Title = "movie2",
                Year = 2016,
                Ids = new TraktMovieIds
                {
                    Trakt = 3,
                    Slug = "movie2",
                    Imdb = "imdb2",
                    Tmdb = 12345
                }
            };

            builder.AddMovie(movie2, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().NotBeNull().And.HaveCount(2);

            movies = collectionPost.Movies.ToArray();

            movies[0].Should().NotBeNull();
            movies[0].Title.Should().Be("movie1");
            movies[0].Year.Should().Be(2016);
            movies[0].Ids.Should().NotBeNull();
            movies[0].Ids.Trakt.Should().Be(2);
            movies[0].Ids.Slug.Should().Be("movie1");
            movies[0].Ids.Imdb.Should().Be("imdb1");
            movies[0].Ids.Tmdb.Should().Be(1234);
            movies[0].CollectedAt.Should().HaveValue();
            movies[0].Metadata.Should().NotBeNull();

            movies[1].Should().NotBeNull();
            movies[1].Title.Should().Be("movie2");
            movies[1].Year.Should().Be(2016);
            movies[1].Ids.Should().NotBeNull();
            movies[1].Ids.Trakt.Should().Be(3);
            movies[1].Ids.Slug.Should().Be("movie2");
            movies[1].Ids.Imdb.Should().Be("imdb2");
            movies[1].Ids.Tmdb.Should().Be(12345);
            movies[1].CollectedAt.Should().HaveValue();
            movies[1].Metadata.Should().NotBeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddMovieArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            Action act = () => builder.AddMovie(null);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddMovie(new TraktMovie());
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds() });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = null });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = string.Empty });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie" });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 0 });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 123 });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 12345 });
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddMovieWithMetadataArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();

            Action act = () => builder.AddMovie(null, metadata);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddMovie(new TraktMovie(), metadata);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds() }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = null }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = string.Empty }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie" }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 0 }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 123 }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 12345 }, metadata);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddMovieWithCollectedAtArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddMovie(null, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddMovie(new TraktMovie(), collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds() }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = null }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = string.Empty }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie" }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 0 }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 123 }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 12345 }, collectedAt);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddMovieWithMetadataAndCollectedAtArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();
            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddMovie(null, metadata, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddMovie(new TraktMovie(), metadata, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds() }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = null }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = string.Empty }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie" }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 0 }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 123 }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddMovie(new TraktMovie { Ids = new TraktMovieIds { Trakt = 1 }, Title = "movie", Year = 12345 }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();
        }

        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddEpisode()
        {
            var episode1 = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 1,
                    Slug = "episode1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddEpisode(episode1);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            builder.AddEpisode(episode1);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            episode1.Ids.Trakt = 2;

            builder.AddEpisode(episode1);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            var episodes = collectionPost.Episodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(2);
            episodes[0].Ids.Slug.Should().Be("episode1");
            episodes[0].Ids.Imdb.Should().Be("imdb1");
            episodes[0].Ids.Tmdb.Should().Be(1234);
            episodes[0].Ids.Tvdb.Should().Be(12345);
            episodes[0].Ids.TvRage.Should().Be(123456);
            episodes[0].CollectedAt.Should().NotHaveValue();
            episodes[0].Metadata.Should().BeNull();

            var episode2 = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 3,
                    Slug = "episode2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddEpisode(episode2);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            episodes = collectionPost.Episodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(2);
            episodes[0].Ids.Slug.Should().Be("episode1");
            episodes[0].Ids.Imdb.Should().Be("imdb1");
            episodes[0].Ids.Tmdb.Should().Be(1234);
            episodes[0].Ids.Tvdb.Should().Be(12345);
            episodes[0].Ids.TvRage.Should().Be(123456);
            episodes[0].CollectedAt.Should().NotHaveValue();
            episodes[0].Metadata.Should().BeNull();

            episodes[1].Should().NotBeNull();
            episodes[1].Ids.Should().NotBeNull();
            episodes[1].Ids.Trakt.Should().Be(3);
            episodes[1].Ids.Slug.Should().Be("episode2");
            episodes[1].Ids.Imdb.Should().Be("imdb2");
            episodes[1].Ids.Tmdb.Should().Be(12345);
            episodes[1].Ids.Tvdb.Should().Be(123456);
            episodes[1].Ids.TvRage.Should().Be(1234567);
            episodes[1].CollectedAt.Should().NotHaveValue();
            episodes[1].Metadata.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddEpisodeWithMetadata()
        {
            var episode1 = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 1,
                    Slug = "episode1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddEpisode(episode1, metadata);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            builder.AddEpisode(episode1, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            episode1.Ids.Trakt = 2;

            builder.AddEpisode(episode1, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            var episodes = collectionPost.Episodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(2);
            episodes[0].Ids.Slug.Should().Be("episode1");
            episodes[0].Ids.Imdb.Should().Be("imdb1");
            episodes[0].Ids.Tmdb.Should().Be(1234);
            episodes[0].Ids.Tvdb.Should().Be(12345);
            episodes[0].Ids.TvRage.Should().Be(123456);
            episodes[0].CollectedAt.Should().NotHaveValue();
            episodes[0].Metadata.Should().NotBeNull();

            var episode2 = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 3,
                    Slug = "episode2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddEpisode(episode2, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            episodes = collectionPost.Episodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(2);
            episodes[0].Ids.Slug.Should().Be("episode1");
            episodes[0].Ids.Imdb.Should().Be("imdb1");
            episodes[0].Ids.Tmdb.Should().Be(1234);
            episodes[0].Ids.Tvdb.Should().Be(12345);
            episodes[0].Ids.TvRage.Should().Be(123456);
            episodes[0].CollectedAt.Should().NotHaveValue();
            episodes[0].Metadata.Should().NotBeNull();

            episodes[1].Should().NotBeNull();
            episodes[1].Ids.Should().NotBeNull();
            episodes[1].Ids.Trakt.Should().Be(3);
            episodes[1].Ids.Slug.Should().Be("episode2");
            episodes[1].Ids.Imdb.Should().Be("imdb2");
            episodes[1].Ids.Tmdb.Should().Be(12345);
            episodes[1].Ids.Tvdb.Should().Be(123456);
            episodes[1].Ids.TvRage.Should().Be(1234567);
            episodes[1].CollectedAt.Should().NotHaveValue();
            episodes[1].Metadata.Should().NotBeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddEpisodeWithCollectedAt()
        {
            var episode1 = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 1,
                    Slug = "episode1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddEpisode(episode1, collectedAt);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            builder.AddEpisode(episode1, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            episode1.Ids.Trakt = 2;

            builder.AddEpisode(episode1, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            var episodes = collectionPost.Episodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(2);
            episodes[0].Ids.Slug.Should().Be("episode1");
            episodes[0].Ids.Imdb.Should().Be("imdb1");
            episodes[0].Ids.Tmdb.Should().Be(1234);
            episodes[0].Ids.Tvdb.Should().Be(12345);
            episodes[0].Ids.TvRage.Should().Be(123456);
            episodes[0].CollectedAt.Should().HaveValue();
            episodes[0].Metadata.Should().BeNull();

            var episode2 = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 3,
                    Slug = "episode2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddEpisode(episode2, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            episodes = collectionPost.Episodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(2);
            episodes[0].Ids.Slug.Should().Be("episode1");
            episodes[0].Ids.Imdb.Should().Be("imdb1");
            episodes[0].Ids.Tmdb.Should().Be(1234);
            episodes[0].Ids.Tvdb.Should().Be(12345);
            episodes[0].Ids.TvRage.Should().Be(123456);
            episodes[0].CollectedAt.Should().HaveValue();
            episodes[0].Metadata.Should().BeNull();

            episodes[1].Should().NotBeNull();
            episodes[1].Ids.Should().NotBeNull();
            episodes[1].Ids.Trakt.Should().Be(3);
            episodes[1].Ids.Slug.Should().Be("episode2");
            episodes[1].Ids.Imdb.Should().Be("imdb2");
            episodes[1].Ids.Tmdb.Should().Be(12345);
            episodes[1].Ids.Tvdb.Should().Be(123456);
            episodes[1].Ids.TvRage.Should().Be(1234567);
            episodes[1].CollectedAt.Should().HaveValue();
            episodes[1].Metadata.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddEpisodeWithMetadataAndCollectedAt()
        {
            var episode1 = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 1,
                    Slug = "episode1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddEpisode(episode1, metadata, collectedAt);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            builder.AddEpisode(episode1, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            episode1.Ids.Trakt = 2;

            builder.AddEpisode(episode1, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            var episodes = collectionPost.Episodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(2);
            episodes[0].Ids.Slug.Should().Be("episode1");
            episodes[0].Ids.Imdb.Should().Be("imdb1");
            episodes[0].Ids.Tmdb.Should().Be(1234);
            episodes[0].Ids.Tvdb.Should().Be(12345);
            episodes[0].Ids.TvRage.Should().Be(123456);
            episodes[0].CollectedAt.Should().HaveValue();
            episodes[0].Metadata.Should().NotBeNull();

            var episode2 = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 3,
                    Slug = "episode2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddEpisode(episode2, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Shows.Should().BeNull();
            collectionPost.Movies.Should().BeNull();

            episodes = collectionPost.Episodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(2);
            episodes[0].Ids.Slug.Should().Be("episode1");
            episodes[0].Ids.Imdb.Should().Be("imdb1");
            episodes[0].Ids.Tmdb.Should().Be(1234);
            episodes[0].Ids.Tvdb.Should().Be(12345);
            episodes[0].Ids.TvRage.Should().Be(123456);
            episodes[0].CollectedAt.Should().HaveValue();
            episodes[0].Metadata.Should().NotBeNull();

            episodes[1].Should().NotBeNull();
            episodes[1].Ids.Should().NotBeNull();
            episodes[1].Ids.Trakt.Should().Be(3);
            episodes[1].Ids.Slug.Should().Be("episode2");
            episodes[1].Ids.Imdb.Should().Be("imdb2");
            episodes[1].Ids.Tmdb.Should().Be(12345);
            episodes[1].Ids.Tvdb.Should().Be(123456);
            episodes[1].Ids.TvRage.Should().Be(1234567);
            episodes[1].CollectedAt.Should().HaveValue();
            episodes[1].Metadata.Should().NotBeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddEpisodeArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            Action act = () => builder.AddEpisode(null);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddEpisode(new TraktEpisode());
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddEpisode(new TraktEpisode { Ids = new TraktEpisodeIds() });
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddEpisodeWithMetadataArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();

            Action act = () => builder.AddEpisode(null, metadata);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddEpisode(new TraktEpisode(), metadata);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddEpisode(new TraktEpisode { Ids = new TraktEpisodeIds() }, metadata);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddEpisodeWithCollectedAtArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddEpisode(null, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddEpisode(new TraktEpisode(), collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddEpisode(new TraktEpisode { Ids = new TraktEpisodeIds() }, collectedAt);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddEpisodeWithMetadataAndCollectedAtArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();
            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddEpisode(null, metadata, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddEpisode(new TraktEpisode(), metadata, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddEpisode(new TraktEpisode { Ids = new TraktEpisodeIds() }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();
        }

        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShow()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            builder.AddShow(show1);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            show1.Ids.Trakt = 2;

            builder.AddShow(show1);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().BeNull();

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().BeNull();

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().NotHaveValue();
            shows[1].Metadata.Should().BeNull();
            shows[1].Seasons.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithMetadata()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, metadata);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            builder.AddShow(show1, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().BeNull();

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, metadata);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().BeNull();

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().NotHaveValue();
            shows[1].Metadata.Should().NotBeNull();
            shows[1].Seasons.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithCollectedAt()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, collectedAt);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            builder.AddShow(show1, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().BeNull();

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().BeNull();

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().HaveValue();
            shows[1].Metadata.Should().BeNull();
            shows[1].Seasons.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithMetadataAndCollectedAt()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, metadata, collectedAt);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            builder.AddShow(show1, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().BeNull();

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, metadata, collectedAt);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().BeNull();

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().HaveValue();
            shows[1].Metadata.Should().NotBeNull();
            shows[1].Seasons.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            Action act = () => builder.AddShow(null);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow());
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 });
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 });
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithMetadataArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();

            Action act = () => builder.AddShow(null, metadata);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), metadata);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, metadata);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, metadata);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithCollectedAtArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddShow(null, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, collectedAt);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithMetadataAndCollectedAtArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();
            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddShow(null, metadata, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), metadata, collectedAt);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, metadata, collectedAt);
            act.ShouldThrow<ArgumentException>();
        }

        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasons()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, 1);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            var show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder.AddShow(show1, 1, 2);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder.AddShow(show1, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().NotHaveValue();
            shows[1].Metadata.Should().BeNull();
            shows[1].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            var show2Seasons = shows[1].Seasons.ToArray();

            show2Seasons[0].Number.Should().Be(1);
            show2Seasons[0].Episodes.Should().BeNull();

            show2Seasons[1].Number.Should().Be(2);
            show2Seasons[1].Episodes.Should().BeNull();

            show2Seasons[2].Number.Should().Be(3);
            show2Seasons[2].Episodes.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsAndMetadata()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, metadata, 1);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            var show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, 1, 2);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, metadata, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, metadata, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().NotHaveValue();
            shows[1].Metadata.Should().NotBeNull();
            shows[1].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            var show2Seasons = shows[1].Seasons.ToArray();

            show2Seasons[0].Number.Should().Be(1);
            show2Seasons[0].Episodes.Should().BeNull();

            show2Seasons[1].Number.Should().Be(2);
            show2Seasons[1].Episodes.Should().BeNull();

            show2Seasons[2].Number.Should().Be(3);
            show2Seasons[2].Episodes.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsAndCollectedAt()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, collectedAt, 1);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            var show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder.AddShow(show1, collectedAt, 1, 2);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder.AddShow(show1, collectedAt, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, collectedAt, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, collectedAt, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().HaveValue();
            shows[1].Metadata.Should().BeNull();
            shows[1].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            var show2Seasons = shows[1].Seasons.ToArray();

            show2Seasons[0].Number.Should().Be(1);
            show2Seasons[0].Episodes.Should().BeNull();

            show2Seasons[1].Number.Should().Be(2);
            show2Seasons[1].Episodes.Should().BeNull();

            show2Seasons[2].Number.Should().Be(3);
            show2Seasons[2].Episodes.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsAndMetadataAndCollectedAt()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, metadata, collectedAt, 1);

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            var show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, collectedAt, 1, 2);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, collectedAt, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, metadata, collectedAt, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, metadata, collectedAt, 1, 2, 3);

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(3);

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().HaveValue();
            shows[1].Metadata.Should().NotBeNull();
            shows[1].Seasons.Should().NotBeNull().And.HaveCount(3);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            show1Seasons[2].Number.Should().Be(3);
            show1Seasons[2].Episodes.Should().BeNull();

            var show2Seasons = shows[1].Seasons.ToArray();

            show2Seasons[0].Number.Should().Be(1);
            show2Seasons[0].Episodes.Should().BeNull();

            show2Seasons[1].Number.Should().Be(2);
            show2Seasons[1].Episodes.Should().BeNull();

            show2Seasons[2].Number.Should().Be(3);
            show2Seasons[2].Episodes.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            Action act = () => builder.AddShow(null, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), 1, 2, 3, 4);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, -1);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, 1, 2, -1);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithMetadataAndSeasonsArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();

            Action act = () => builder.AddShow(null, metadata, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), metadata, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, metadata, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, metadata, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, metadata, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, metadata, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, metadata, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, metadata, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, metadata, -1);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, metadata, 1, 2, -1);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithCollectedAtAndSeasonsArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddShow(null, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, collectedAt, -1);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, collectedAt, 1, 2, -1);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithMetadataAndCollectedAtAndSeasonsArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();
            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddShow(null, metadata, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), metadata, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, metadata, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, metadata, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, metadata, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, metadata, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, metadata, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, metadata, collectedAt, 1, 2, 3, 4);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, metadata, collectedAt, -1);
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, metadata, collectedAt, 1, 2, -1);
            act.ShouldThrow<ArgumentException>();
        }

        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsAndEpisodes()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, new SAE(1, new int[] { 1 })); // season 1 - episode 1

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            var show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(1);

            var show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);

            // ---------------------------------------------------------

            builder.AddShow(show1, new SAE(1, new int[] { 1, 2 })); // season 1 - episode 1, 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);

            // ---------------------------------------------------------

            builder.AddShow(show1, new SAE(1, new int[] { 1, 2, 3 })); // season 1 - episode 1, 2, 3

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            // ---------------------------------------------------------

            builder.AddShow(show1, new SAE(1, new int[] { 1, 2, 3 }), // season 1 - episode 1, 2, 3
                                   new SAE(2, new int[] { 4 }));      // season 2 - episode 4

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(1);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            var show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);

            // ---------------------------------------------------------

            builder.AddShow(show1, new SAE(1, new int[] { 1, 2, 3 }), // season 1 - episode 1, 2, 3
                                   new SAE(2, new int[] { 4, 5 }));   // season 2 - episode 4, 5

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);

            // ---------------------------------------------------------

            builder.AddShow(show1, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                   new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                   new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                   new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().NotHaveValue();
            shows[1].Metadata.Should().BeNull();
            shows[1].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            var show2Seasons = shows[1].Seasons.ToArray();

            show2Seasons[0].Number.Should().Be(1);
            show2Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show2Seasons[1].Number.Should().Be(2);
            show2Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            var show2Season1Episodes = show2Seasons[0].Episodes.ToArray();

            show2Season1Episodes[0].Number.Should().Be(1);
            show2Season1Episodes[1].Number.Should().Be(2);
            show2Season1Episodes[2].Number.Should().Be(3);

            var show2Season2Episodes = show2Seasons[1].Episodes.ToArray();

            show2Season2Episodes[0].Number.Should().Be(4);
            show2Season2Episodes[1].Number.Should().Be(5);
            show2Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, new SAE(1, null));  // season 1

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, new SAE(1, new int[] { }));  // season 1

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, new SAE(1, new int[] { 1, 2 }),  // season 1 - episodes 1, 2
                                   new SAE(2, null));               // season 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, new SAE(1, new int[] { 1, 2 }),  // season 1 - episodes 1, 2
                                   new SAE(2, new int[] { }));      // season 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsAndEpisodesAndMetadata()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, metadata, new SAE(1, new int[] { 1 })); // season 1 - episode 1

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            var show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(1);

            var show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, new SAE(1, new int[] { 1, 2 })); // season 1 - episode 1, 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, new SAE(1, new int[] { 1, 2, 3 })); // season 1 - episode 1, 2, 3

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, new SAE(1, new int[] { 1, 2, 3 }), // season 1 - episode 1, 2, 3
                                             new SAE(2, new int[] { 4 }));      // season 2 - episode 4

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(1);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            var show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, new SAE(1, new int[] { 1, 2, 3 }), // season 1 - episode 1, 2, 3
                                             new SAE(2, new int[] { 4, 5 }));   // season 2 - episode 4, 5

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                             new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, metadata, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                             new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, metadata, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                             new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().NotHaveValue();
            shows[1].Metadata.Should().NotBeNull();
            shows[1].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            var show2Seasons = shows[1].Seasons.ToArray();

            show2Seasons[0].Number.Should().Be(1);
            show2Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show2Seasons[1].Number.Should().Be(2);
            show2Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            var show2Season1Episodes = show2Seasons[0].Episodes.ToArray();

            show2Season1Episodes[0].Number.Should().Be(1);
            show2Season1Episodes[1].Number.Should().Be(2);
            show2Season1Episodes[2].Number.Should().Be(3);

            var show2Season2Episodes = show2Seasons[1].Episodes.ToArray();

            show2Season2Episodes[0].Number.Should().Be(4);
            show2Season2Episodes[1].Number.Should().Be(5);
            show2Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, metadata, new SAE(1, null));  // season 1

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, metadata, new SAE(1, new int[] { }));  // season 1

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, metadata, new SAE(1, new int[] { 1, 2 }),  // season 1 - episodes 1, 2
                                             new SAE(2, null));               // season 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, metadata, new SAE(1, new int[] { 1, 2 }),  // season 1 - episodes 1, 2
                                             new SAE(2, new int[] { }));      // season 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().NotHaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsAndEpisodesAndCollectedAt()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, collectedAt, new SAE(1, new int[] { 1 })); // season 1 - episode 1

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            var show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(1);

            var show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);

            // ---------------------------------------------------------

            builder.AddShow(show1, collectedAt, new SAE(1, new int[] { 1, 2 })); // season 1 - episode 1, 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);

            // ---------------------------------------------------------

            builder.AddShow(show1, collectedAt, new SAE(1, new int[] { 1, 2, 3 })); // season 1 - episode 1, 2, 3

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            // ---------------------------------------------------------

            builder.AddShow(show1, collectedAt, new SAE(1, new int[] { 1, 2, 3 }), // season 1 - episode 1, 2, 3
                                                new SAE(2, new int[] { 4 }));      // season 2 - episode 4

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(1);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            var show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);

            // ---------------------------------------------------------

            builder.AddShow(show1, collectedAt, new SAE(1, new int[] { 1, 2, 3 }), // season 1 - episode 1, 2, 3
                                                new SAE(2, new int[] { 4, 5 }));   // season 2 - episode 4, 5

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);

            // ---------------------------------------------------------

            builder.AddShow(show1, collectedAt, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                                new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, collectedAt, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                                new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, collectedAt, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                                new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().HaveValue();
            shows[1].Metadata.Should().BeNull();
            shows[1].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            var show2Seasons = shows[1].Seasons.ToArray();

            show2Seasons[0].Number.Should().Be(1);
            show2Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show2Seasons[1].Number.Should().Be(2);
            show2Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            var show2Season1Episodes = show2Seasons[0].Episodes.ToArray();

            show2Season1Episodes[0].Number.Should().Be(1);
            show2Season1Episodes[1].Number.Should().Be(2);
            show2Season1Episodes[2].Number.Should().Be(3);

            var show2Season2Episodes = show2Seasons[1].Episodes.ToArray();

            show2Season2Episodes[0].Number.Should().Be(4);
            show2Season2Episodes[1].Number.Should().Be(5);
            show2Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, collectedAt, new SAE(1, null));  // season 1

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, collectedAt, new SAE(1, new int[] { }));  // season 1

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, collectedAt, new SAE(1, new int[] { 1, 2 }),  // season 1 - episodes 1, 2
                                                new SAE(2, null));               // season 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, collectedAt, new SAE(1, new int[] { 1, 2 }),  // season 1 - episodes 1, 2
                                                new SAE(2, new int[] { }));      // season 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().BeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsAndEpisodesAndMetadataAndCollectedAt()
        {
            var show1 = new TraktShow
            {
                Title = "show1",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 1,
                    Slug = "show1",
                    Imdb = "imdb1",
                    Tmdb = 1234,
                    Tvdb = 12345,
                    TvRage = 123456
                }
            };

            var metadata = new TraktMetadata
            {
                Audio = TraktMediaAudio.AAC,
                AudioChannels = TraktMediaAudioChannel.Channels_5_1,
                MediaResolution = TraktMediaResolution.HD_720p,
                MediaType = TraktMediaType.DVD,
                ThreeDimensional = false
            };

            var collectedAt = DateTime.Now;

            var builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show1, metadata, collectedAt, new SAE(1, new int[] { 1 })); // season 1 - episode 1

            var collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            var shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            var show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(1);

            var show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, collectedAt, new SAE(1, new int[] { 1, 2 })); // season 1 - episode 1, 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 })); // season 1 - episode 1, 2, 3

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }), // season 1 - episode 1, 2, 3
                                                          new SAE(2, new int[] { 4 }));      // season 2 - episode 4

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(1);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            var show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }), // season 1 - episode 1, 2, 3
                                                          new SAE(2, new int[] { 4, 5 }));   // season 2 - episode 4, 5

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);

            // ---------------------------------------------------------

            builder.AddShow(show1, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                                          new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(1);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            show1.Ids.Trakt = 2;

            builder.AddShow(show1, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                                          new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            var show2 = new TraktShow
            {
                Title = "show2",
                Year = 2016,
                Ids = new TraktShowIds
                {
                    Trakt = 3,
                    Slug = "show2",
                    Imdb = "imdb2",
                    Tmdb = 12345,
                    Tvdb = 123456,
                    TvRage = 1234567
                }
            };

            builder.AddShow(show2, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }),  // season 1 - episode 1, 2, 3
                                                          new SAE(2, new int[] { 4, 5, 6 })); // season 2 - episode 4, 5, 6

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(2);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show1");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(2);
            shows[0].Ids.Slug.Should().Be("show1");
            shows[0].Ids.Imdb.Should().Be("imdb1");
            shows[0].Ids.Tmdb.Should().Be(1234);
            shows[0].Ids.Tvdb.Should().Be(12345);
            shows[0].Ids.TvRage.Should().Be(123456);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            shows[1].Should().NotBeNull();
            shows[1].Title.Should().Be("show2");
            shows[1].Year.Should().Be(2016);
            shows[1].Ids.Should().NotBeNull();
            shows[1].Ids.Trakt.Should().Be(3);
            shows[1].Ids.Slug.Should().Be("show2");
            shows[1].Ids.Imdb.Should().Be("imdb2");
            shows[1].Ids.Tmdb.Should().Be(12345);
            shows[1].Ids.Tvdb.Should().Be(123456);
            shows[1].Ids.TvRage.Should().Be(1234567);
            shows[1].CollectedAt.Should().HaveValue();
            shows[1].Metadata.Should().NotBeNull();
            shows[1].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            show1Season1Episodes = show1Seasons[0].Episodes.ToArray();

            show1Season1Episodes[0].Number.Should().Be(1);
            show1Season1Episodes[1].Number.Should().Be(2);
            show1Season1Episodes[2].Number.Should().Be(3);

            show1Season2Episodes = show1Seasons[1].Episodes.ToArray();

            show1Season2Episodes[0].Number.Should().Be(4);
            show1Season2Episodes[1].Number.Should().Be(5);
            show1Season2Episodes[2].Number.Should().Be(6);

            var show2Seasons = shows[1].Seasons.ToArray();

            show2Seasons[0].Number.Should().Be(1);
            show2Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(3);

            show2Seasons[1].Number.Should().Be(2);
            show2Seasons[1].Episodes.Should().NotBeNull().And.HaveCount(3);

            var show2Season1Episodes = show2Seasons[0].Episodes.ToArray();

            show2Season1Episodes[0].Number.Should().Be(1);
            show2Season1Episodes[1].Number.Should().Be(2);
            show2Season1Episodes[2].Number.Should().Be(3);

            var show2Season2Episodes = show2Seasons[1].Episodes.ToArray();

            show2Season2Episodes[0].Number.Should().Be(4);
            show2Season2Episodes[1].Number.Should().Be(5);
            show2Season2Episodes[2].Number.Should().Be(6);

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, metadata, collectedAt, new SAE(1, null));  // season 1

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, metadata, collectedAt, new SAE(1, new int[] { }));  // season 1

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(1);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, metadata, collectedAt, new SAE(1, new int[] { 1, 2 }),  // season 1 - episodes 1, 2
                                                          new SAE(2, null));               // season 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();

            // ---------------------------------------------------------

            builder = TraktSyncCollectionPost.Builder();

            builder.AddShow(show2, metadata, collectedAt, new SAE(1, new int[] { 1, 2 }),  // season 1 - episodes 1, 2
                                                          new SAE(2, new int[] { }));      // season 2

            collectionPost = builder.Build();

            collectionPost.Should().NotBeNull();
            collectionPost.Episodes.Should().BeNull();
            collectionPost.Shows.Should().NotBeNull().And.HaveCount(1);
            collectionPost.Movies.Should().BeNull();

            shows = collectionPost.Shows.ToArray();

            shows[0].Should().NotBeNull();
            shows[0].Title.Should().Be("show2");
            shows[0].Year.Should().Be(2016);
            shows[0].Ids.Should().NotBeNull();
            shows[0].Ids.Trakt.Should().Be(3);
            shows[0].Ids.Slug.Should().Be("show2");
            shows[0].Ids.Imdb.Should().Be("imdb2");
            shows[0].Ids.Tmdb.Should().Be(12345);
            shows[0].Ids.Tvdb.Should().Be(123456);
            shows[0].Ids.TvRage.Should().Be(1234567);
            shows[0].CollectedAt.Should().HaveValue();
            shows[0].Metadata.Should().NotBeNull();
            shows[0].Seasons.Should().NotBeNull().And.HaveCount(2);

            show1Seasons = shows[0].Seasons.ToArray();

            show1Seasons[0].Number.Should().Be(1);
            show1Seasons[0].Episodes.Should().NotBeNull().And.HaveCount(2);

            show1Seasons[1].Number.Should().Be(2);
            show1Seasons[1].Episodes.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithSeasonsAndEpisodesArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            Action act = () => builder.AddShow(null, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            var sae = default(SAE);

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, sae);
            act.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithMetadataAndSeasonsAndEpisodesArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();

            Action act = () => builder.AddShow(null, metadata, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), metadata, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, metadata, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, metadata, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, metadata, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, metadata, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, metadata, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, metadata, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            var sae = default(SAE);

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, metadata, sae);
            act.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithCollectedAtAndSeasonsAndEpisodesArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddShow(null, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            var sae = default(SAE);

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, collectedAt, sae);
            act.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void TestTraktSyncCollectionPostBuilderAddShowWithMetadataAndCollectedAtAndSeasonsAndEpisodesArgumentExceptions()
        {
            var builder = TraktSyncCollectionPost.Builder();

            var metadata = new TraktMetadata();
            var collectedAt = DateTime.UtcNow;

            Action act = () => builder.AddShow(null, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow(), metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentNullException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds() }, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = null }, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = string.Empty }, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 0 }, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 123 }, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 12345 }, metadata, collectedAt, new SAE(1, new int[] { 1, 2, 3 }));
            act.ShouldThrow<ArgumentException>();

            var sae = default(SAE);

            act = () => builder.AddShow(new TraktShow { Ids = new TraktShowIds { Trakt = 1 }, Title = "show", Year = 1234 }, metadata, collectedAt, sae);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
