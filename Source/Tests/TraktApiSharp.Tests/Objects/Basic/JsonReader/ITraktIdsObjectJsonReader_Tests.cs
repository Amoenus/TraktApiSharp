﻿namespace TraktApiSharp.Tests.Objects.Basic.JsonReader
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.IO;
    using Traits;
    using TraktApiSharp.Objects.Basic;
    using TraktApiSharp.Objects.Basic.JsonReader;
    using TraktApiSharp.Objects.JsonReader;
    using Xunit;

    [Category("Objects.JsonReader.Basic")]
    public class ITraktIdsObjectJsonReader_Tests
    {
        [Fact]
        public void Test_ITraktIdsObjectJsonReader_Implements_ITraktObjectJsonReader_Interface()
        {
            typeof(ITraktIdsObjectJsonReader).GetInterfaces().Should().Contain(typeof(ITraktObjectJsonReader<ITraktIds>));
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Complete()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_COMPLETE);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_1()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_1);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(0);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_2()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_2);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().BeNull();
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_3()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_3);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().BeNull();
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_4()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_4);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().BeNull();
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_5()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_5);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().BeNull();
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_6()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_6);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_7()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_7);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().BeNull();
            traktIds.Tvdb.Should().BeNull();
            traktIds.Imdb.Should().BeNull();
            traktIds.Tmdb.Should().BeNull();
            traktIds.TvRage.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_8()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_8);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(0);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().BeNull();
            traktIds.Imdb.Should().BeNull();
            traktIds.Tmdb.Should().BeNull();
            traktIds.TvRage.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_9()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_9);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(0);
            traktIds.Slug.Should().BeNull();
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().BeNull();
            traktIds.Tmdb.Should().BeNull();
            traktIds.TvRage.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_10()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_10);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(0);
            traktIds.Slug.Should().BeNull();
            traktIds.Tvdb.Should().BeNull();
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().BeNull();
            traktIds.TvRage.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_11()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_11);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(0);
            traktIds.Slug.Should().BeNull();
            traktIds.Tvdb.Should().BeNull();
            traktIds.Imdb.Should().BeNull();
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Incomplete_12()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_INCOMPLETE_12);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(0);
            traktIds.Slug.Should().BeNull();
            traktIds.Tvdb.Should().BeNull();
            traktIds.Imdb.Should().BeNull();
            traktIds.Tmdb.Should().BeNull();
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Not_Valid_1()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_NOT_VALID_1);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(0);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Not_Valid_2()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_NOT_VALID_2);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().BeNull();
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Not_Valid_3()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_NOT_VALID_3);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().BeNull();
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Not_Valid_4()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_NOT_VALID_4);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().BeNull();
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Not_Valid_5()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_NOT_VALID_5);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().BeNull();
            traktIds.TvRage.Should().Be(24493U);
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Not_Valid_6()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_NOT_VALID_6);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(1390);
            traktIds.Slug.Should().Be("game-of-thrones");
            traktIds.Tvdb.Should().Be(121361U);
            traktIds.Imdb.Should().Be("tt0944947");
            traktIds.Tmdb.Should().Be(1399U);
            traktIds.TvRage.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Not_Valid_7()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(JSON_NOT_VALID_7);

            traktIds.Should().NotBeNull();
            traktIds.Trakt.Should().Be(0);
            traktIds.Slug.Should().BeNull();
            traktIds.Tvdb.Should().BeNull();
            traktIds.Imdb.Should().BeNull();
            traktIds.Tmdb.Should().BeNull();
            traktIds.TvRage.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Null()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(default(string));
            traktIds.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_Json_String_Empty()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(string.Empty);
            traktIds.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Complete()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_COMPLETE))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_1()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_1))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(0);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_2()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_2))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().BeNull();
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_3()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_3))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().BeNull();
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_4()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_4))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().BeNull();
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_5()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_5))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().BeNull();
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_6()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_6))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().BeNull();
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_7()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_7))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().BeNull();
                traktIds.Tvdb.Should().BeNull();
                traktIds.Imdb.Should().BeNull();
                traktIds.Tmdb.Should().BeNull();
                traktIds.TvRage.Should().BeNull();
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_8()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_8))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(0);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().BeNull();
                traktIds.Imdb.Should().BeNull();
                traktIds.Tmdb.Should().BeNull();
                traktIds.TvRage.Should().BeNull();
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_9()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_9))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(0);
                traktIds.Slug.Should().BeNull();
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().BeNull();
                traktIds.Tmdb.Should().BeNull();
                traktIds.TvRage.Should().BeNull();
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_10()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_10))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(0);
                traktIds.Slug.Should().BeNull();
                traktIds.Tvdb.Should().BeNull();
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().BeNull();
                traktIds.TvRage.Should().BeNull();
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_11()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_11))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(0);
                traktIds.Slug.Should().BeNull();
                traktIds.Tvdb.Should().BeNull();
                traktIds.Imdb.Should().BeNull();
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().BeNull();
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Incomplete_12()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_12))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(0);
                traktIds.Slug.Should().BeNull();
                traktIds.Tvdb.Should().BeNull();
                traktIds.Imdb.Should().BeNull();
                traktIds.Tmdb.Should().BeNull();
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_1()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_1))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(0);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_2()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_2))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().BeNull();
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_3()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_3))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().BeNull();
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_4()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_4))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().BeNull();
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_5()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_5))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().BeNull();
                traktIds.TvRage.Should().Be(24493U);
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_6()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_6))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(1390);
                traktIds.Slug.Should().Be("game-of-thrones");
                traktIds.Tvdb.Should().Be(121361U);
                traktIds.Imdb.Should().Be("tt0944947");
                traktIds.Tmdb.Should().Be(1399U);
                traktIds.TvRage.Should().BeNull();
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_7()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_7))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);

                traktIds.Should().NotBeNull();
                traktIds.Trakt.Should().Be(0);
                traktIds.Slug.Should().BeNull();
                traktIds.Tvdb.Should().BeNull();
                traktIds.Imdb.Should().BeNull();
                traktIds.Tmdb.Should().BeNull();
                traktIds.TvRage.Should().BeNull();
            }
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Null()
        {
            var jsonReader = new ITraktIdsObjectJsonReader();

            var traktIds = jsonReader.ReadObject(default(JsonTextReader));
            traktIds.Should().BeNull();
        }

        [Fact]
        public void Test_ITraktIdsObjectJsonReader_ReadObject_From_JsonReader_Empty()
        {
            var traktJsonReader = new ITraktIdsObjectJsonReader();

            using (var reader = new StringReader(string.Empty))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktIds = traktJsonReader.ReadObject(jsonReader);
                traktIds.Should().BeNull();
            }
        }

        private const string JSON_COMPLETE =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_INCOMPLETE_1 =
            @"{
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_INCOMPLETE_2 =
            @"{
                ""trakt"": 1390,
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_INCOMPLETE_3 =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_INCOMPLETE_4 =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_INCOMPLETE_5 =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tvrage"": 24493
              }";

        private const string JSON_INCOMPLETE_6 =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399
              }";

        private const string JSON_INCOMPLETE_7 =
            @"{
                ""trakt"": 1390
              }";

        private const string JSON_INCOMPLETE_8 =
            @"{
                ""slug"": ""game-of-thrones""
              }";

        private const string JSON_INCOMPLETE_9 =
            @"{
                ""tvdb"": 121361
              }";

        private const string JSON_INCOMPLETE_10 =
            @"{
                ""imdb"": ""tt0944947""
              }";

        private const string JSON_INCOMPLETE_11 =
            @"{
                ""tmdb"": 1399
              }";

        private const string JSON_INCOMPLETE_12 =
            @"{
                ""tvrage"": 24493
              }";

        private const string JSON_NOT_VALID_1 =
            @"{
                ""tr"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_NOT_VALID_2 =
            @"{
                ""trakt"": 1390,
                ""sl"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_NOT_VALID_3 =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tv"": 121361,
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_NOT_VALID_4 =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""im"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_NOT_VALID_5 =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tm"": 1399,
                ""tvrage"": 24493
              }";

        private const string JSON_NOT_VALID_6 =
            @"{
                ""trakt"": 1390,
                ""slug"": ""game-of-thrones"",
                ""tvdb"": 121361,
                ""imdb"": ""tt0944947"",
                ""tmdb"": 1399,
                ""tv"": 24493
              }";

        private const string JSON_NOT_VALID_7 =
            @"{
                ""tr"": 1390,
                ""sl"": ""game-of-thrones"",
                ""tv"": 121361,
                ""im"": ""tt0944947"",
                ""tm"": 1399,
                ""tvr"": 24493
              }";
    }
}
