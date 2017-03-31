﻿namespace TraktApiSharp.Tests.Objects.Basic.JsonReader
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using Traits;
    using TraktApiSharp.Objects.Basic.Implementations;
    using TraktApiSharp.Objects.Basic.JsonReader;
    using TraktApiSharp.Objects.JsonReader;
    using Xunit;

    [Category("Objects.JsonReader.Basic")]
    public class TraktRatingObjectJsonReader_Tests
    {
        [Fact]
        public void Test_TraktRatingObjectJsonReader_Implements_ITraktObjectJsonReader_Interface()
        {
            typeof(TraktRatingObjectJsonReader).GetInterfaces().Should().Contain(typeof(ITraktObjectJsonReader<TraktRating>));
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Complete()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_COMPLETE);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().Be(8.32715f);
            traktRating.Votes.Should().Be(9274);
            traktRating.Distribution.Should().NotBeNull();
            traktRating.Distribution.Should().NotBeEmpty();
            traktRating.Distribution.Should().HaveCount(10);
            traktRating.Distribution.Should().Contain(new Dictionary<string, int>
            {
                ["1"] = 78,
                ["2"] = 45,
                ["3"] = 55,
                ["4"] = 96,
                ["5"] = 183,
                ["6"] = 545,
                ["7"] = 1361,
                ["8"] = 2259,
                ["9"] = 1772,
                ["10"] = 2863
            });
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Incomplete_1()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_INCOMPLETE_1);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().BeNull();
            traktRating.Votes.Should().Be(9274);
            traktRating.Distribution.Should().NotBeNull();
            traktRating.Distribution.Should().NotBeEmpty();
            traktRating.Distribution.Should().HaveCount(10);
            traktRating.Distribution.Should().Contain(new Dictionary<string, int>
            {
                ["1"] = 78,
                ["2"] = 45,
                ["3"] = 55,
                ["4"] = 96,
                ["5"] = 183,
                ["6"] = 545,
                ["7"] = 1361,
                ["8"] = 2259,
                ["9"] = 1772,
                ["10"] = 2863
            });
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Incomplete_2()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_INCOMPLETE_2);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().Be(8.32715f);
            traktRating.Votes.Should().BeNull();
            traktRating.Distribution.Should().NotBeNull();
            traktRating.Distribution.Should().NotBeEmpty();
            traktRating.Distribution.Should().HaveCount(10);
            traktRating.Distribution.Should().Contain(new Dictionary<string, int>
            {
                ["1"] = 78,
                ["2"] = 45,
                ["3"] = 55,
                ["4"] = 96,
                ["5"] = 183,
                ["6"] = 545,
                ["7"] = 1361,
                ["8"] = 2259,
                ["9"] = 1772,
                ["10"] = 2863
            });
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Incomplete_3()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_INCOMPLETE_3);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().Be(8.32715f);
            traktRating.Votes.Should().Be(9274);
            traktRating.Distribution.Should().BeNull();
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Incomplete_4()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_INCOMPLETE_4);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().Be(8.32715f);
            traktRating.Votes.Should().Be(9274);
            traktRating.Distribution.Should().NotBeNull();
            traktRating.Distribution.Should().NotBeEmpty();
            traktRating.Distribution.Should().HaveCount(10);
            traktRating.Distribution.Should().Contain(new Dictionary<string, int>
            {
                ["1"] = 78,
                ["2"] = 45,
                ["3"] = 55,
                ["4"] = 0,
                ["5"] = 183,
                ["6"] = 545,
                ["7"] = 1361,
                ["8"] = 0,
                ["9"] = 1772,
                ["10"] = 2863
            });
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Not_Valid_1()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_NOT_VALID_1);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().BeNull();
            traktRating.Votes.Should().Be(9274);
            traktRating.Distribution.Should().NotBeNull();
            traktRating.Distribution.Should().NotBeEmpty();
            traktRating.Distribution.Should().HaveCount(10);
            traktRating.Distribution.Should().Contain(new Dictionary<string, int>
            {
                ["1"] = 78,
                ["2"] = 45,
                ["3"] = 55,
                ["4"] = 96,
                ["5"] = 183,
                ["6"] = 545,
                ["7"] = 1361,
                ["8"] = 2259,
                ["9"] = 1772,
                ["10"] = 2863
            });
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Not_Valid_2()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_NOT_VALID_2);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().Be(8.32715f);
            traktRating.Votes.Should().BeNull();
            traktRating.Distribution.Should().NotBeNull();
            traktRating.Distribution.Should().NotBeEmpty();
            traktRating.Distribution.Should().HaveCount(10);
            traktRating.Distribution.Should().Contain(new Dictionary<string, int>
            {
                ["1"] = 78,
                ["2"] = 45,
                ["3"] = 55,
                ["4"] = 96,
                ["5"] = 183,
                ["6"] = 545,
                ["7"] = 1361,
                ["8"] = 2259,
                ["9"] = 1772,
                ["10"] = 2863
            });
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Not_Valid_3()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_NOT_VALID_3);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().Be(8.32715f);
            traktRating.Votes.Should().Be(9274);
            traktRating.Distribution.Should().BeNull();
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Not_Valid_4()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_NOT_VALID_4);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().BeNull();
            traktRating.Votes.Should().BeNull();
            traktRating.Distribution.Should().BeNull();
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Not_Valid_5()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(JSON_NOT_VALID_5);

            traktRating.Should().NotBeNull();
            traktRating.Rating.Should().Be(8.32715f);
            traktRating.Votes.Should().Be(9274);
            traktRating.Distribution.Should().NotBeNull();
            traktRating.Distribution.Should().NotBeEmpty();
            traktRating.Distribution.Should().HaveCount(10);
            traktRating.Distribution.Should().Contain(new Dictionary<string, int>
            {
                ["1"] = 78,
                ["2"] = 45,
                ["3"] = 0,
                ["4"] = 96,
                ["5"] = 183,
                ["6"] = 0,
                ["7"] = 1361,
                ["8"] = 2259,
                ["9"] = 0,
                ["10"] = 2863
            });
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Null()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(default(string));
            traktRating.Should().BeNull();
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_Json_String_Empty()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(string.Empty);
            traktRating.Should().BeNull();
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Complete()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_COMPLETE))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().Be(8.32715f);
                traktRating.Votes.Should().Be(9274);
                traktRating.Distribution.Should().NotBeNull();
                traktRating.Distribution.Should().NotBeEmpty();
                traktRating.Distribution.Should().HaveCount(10);
                traktRating.Distribution.Should().Contain(new Dictionary<string, int>
                {
                    ["1"] = 78,
                    ["2"] = 45,
                    ["3"] = 55,
                    ["4"] = 96,
                    ["5"] = 183,
                    ["6"] = 545,
                    ["7"] = 1361,
                    ["8"] = 2259,
                    ["9"] = 1772,
                    ["10"] = 2863
                });
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Incomplete_1()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_1))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().BeNull();
                traktRating.Votes.Should().Be(9274);
                traktRating.Distribution.Should().NotBeNull();
                traktRating.Distribution.Should().NotBeEmpty();
                traktRating.Distribution.Should().HaveCount(10);
                traktRating.Distribution.Should().Contain(new Dictionary<string, int>
                {
                    ["1"] = 78,
                    ["2"] = 45,
                    ["3"] = 55,
                    ["4"] = 96,
                    ["5"] = 183,
                    ["6"] = 545,
                    ["7"] = 1361,
                    ["8"] = 2259,
                    ["9"] = 1772,
                    ["10"] = 2863
                });
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Incomplete_2()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_2))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().Be(8.32715f);
                traktRating.Votes.Should().BeNull();
                traktRating.Distribution.Should().NotBeNull();
                traktRating.Distribution.Should().NotBeEmpty();
                traktRating.Distribution.Should().HaveCount(10);
                traktRating.Distribution.Should().Contain(new Dictionary<string, int>
                {
                    ["1"] = 78,
                    ["2"] = 45,
                    ["3"] = 55,
                    ["4"] = 96,
                    ["5"] = 183,
                    ["6"] = 545,
                    ["7"] = 1361,
                    ["8"] = 2259,
                    ["9"] = 1772,
                    ["10"] = 2863
                });
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Incomplete_3()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_3))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().Be(8.32715f);
                traktRating.Votes.Should().Be(9274);
                traktRating.Distribution.Should().BeNull();
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Incomplete_4()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_4))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().Be(8.32715f);
                traktRating.Votes.Should().Be(9274);
                traktRating.Distribution.Should().NotBeNull();
                traktRating.Distribution.Should().NotBeEmpty();
                traktRating.Distribution.Should().HaveCount(10);
                traktRating.Distribution.Should().Contain(new Dictionary<string, int>
                {
                    ["1"] = 78,
                    ["2"] = 45,
                    ["3"] = 55,
                    ["4"] = 0,
                    ["5"] = 183,
                    ["6"] = 545,
                    ["7"] = 1361,
                    ["8"] = 0,
                    ["9"] = 1772,
                    ["10"] = 2863
                });
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_1()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_1))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().BeNull();
                traktRating.Votes.Should().Be(9274);
                traktRating.Distribution.Should().NotBeNull();
                traktRating.Distribution.Should().NotBeEmpty();
                traktRating.Distribution.Should().HaveCount(10);
                traktRating.Distribution.Should().Contain(new Dictionary<string, int>
                {
                    ["1"] = 78,
                    ["2"] = 45,
                    ["3"] = 55,
                    ["4"] = 96,
                    ["5"] = 183,
                    ["6"] = 545,
                    ["7"] = 1361,
                    ["8"] = 2259,
                    ["9"] = 1772,
                    ["10"] = 2863
                });
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_2()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_2))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().Be(8.32715f);
                traktRating.Votes.Should().BeNull();
                traktRating.Distribution.Should().NotBeNull();
                traktRating.Distribution.Should().NotBeEmpty();
                traktRating.Distribution.Should().HaveCount(10);
                traktRating.Distribution.Should().Contain(new Dictionary<string, int>
                {
                    ["1"] = 78,
                    ["2"] = 45,
                    ["3"] = 55,
                    ["4"] = 96,
                    ["5"] = 183,
                    ["6"] = 545,
                    ["7"] = 1361,
                    ["8"] = 2259,
                    ["9"] = 1772,
                    ["10"] = 2863
                });
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_3()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_3))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().Be(8.32715f);
                traktRating.Votes.Should().Be(9274);
                traktRating.Distribution.Should().BeNull();
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_4()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_4))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().BeNull();
                traktRating.Votes.Should().BeNull();
                traktRating.Distribution.Should().BeNull();
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Not_Valid_5()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_5))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);

                traktRating.Should().NotBeNull();
                traktRating.Rating.Should().Be(8.32715f);
                traktRating.Votes.Should().Be(9274);
                traktRating.Distribution.Should().NotBeNull();
                traktRating.Distribution.Should().NotBeEmpty();
                traktRating.Distribution.Should().HaveCount(10);
                traktRating.Distribution.Should().Contain(new Dictionary<string, int>
                {
                    ["1"] = 78,
                    ["2"] = 45,
                    ["3"] = 0,
                    ["4"] = 96,
                    ["5"] = 183,
                    ["6"] = 0,
                    ["7"] = 1361,
                    ["8"] = 2259,
                    ["9"] = 0,
                    ["10"] = 2863
                });
            }
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Null()
        {
            var jsonReader = new TraktRatingObjectJsonReader();

            var traktRating = jsonReader.ReadObject(default(JsonTextReader));
            traktRating.Should().BeNull();
        }

        [Fact]
        public void Test_TraktRatingObjectJsonReader_ReadObject_From_JsonReader_Empty()
        {
            var traktJsonReader = new TraktRatingObjectJsonReader();

            using (var reader = new StringReader(string.Empty))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktRating = traktJsonReader.ReadObject(jsonReader);
                traktRating.Should().BeNull();
            }
        }

        private const string JSON_COMPLETE =
            @"{
                ""rating"": 8.32715,
                ""votes"": 9274,
                ""distribution"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""3"": 55,
                  ""4"": 96,
                  ""5"": 183,
                  ""6"": 545,
                  ""7"": 1361,
                  ""8"": 2259,
                  ""9"": 1772,
                  ""10"": 2863
                }
              }";

        private const string JSON_INCOMPLETE_1 =
            @"{
                ""votes"": 9274,
                ""distribution"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""3"": 55,
                  ""4"": 96,
                  ""5"": 183,
                  ""6"": 545,
                  ""7"": 1361,
                  ""8"": 2259,
                  ""9"": 1772,
                  ""10"": 2863
                }
              }";

        private const string JSON_INCOMPLETE_2 =
            @"{
                ""rating"": 8.32715,
                ""distribution"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""3"": 55,
                  ""4"": 96,
                  ""5"": 183,
                  ""6"": 545,
                  ""7"": 1361,
                  ""8"": 2259,
                  ""9"": 1772,
                  ""10"": 2863
                }
              }";

        private const string JSON_INCOMPLETE_3 =
            @"{
                ""rating"": 8.32715,
                ""votes"": 9274
              }";

        private const string JSON_INCOMPLETE_4 =
            @"{
                ""rating"": 8.32715,
                ""votes"": 9274,
                ""distribution"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""3"": 55,
                  ""5"": 183,
                  ""6"": 545,
                  ""7"": 1361,
                  ""9"": 1772,
                  ""10"": 2863
                }
              }";

        private const string JSON_NOT_VALID_1 =
            @"{
                ""ra"": 8.32715,
                ""votes"": 9274,
                ""distribution"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""3"": 55,
                  ""4"": 96,
                  ""5"": 183,
                  ""6"": 545,
                  ""7"": 1361,
                  ""8"": 2259,
                  ""9"": 1772,
                  ""10"": 2863
                }
              }";

        private const string JSON_NOT_VALID_2 =
            @"{
                ""rating"": 8.32715,
                ""vo"": 9274,
                ""distribution"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""3"": 55,
                  ""4"": 96,
                  ""5"": 183,
                  ""6"": 545,
                  ""7"": 1361,
                  ""8"": 2259,
                  ""9"": 1772,
                  ""10"": 2863
                }
              }";

        private const string JSON_NOT_VALID_3 =
            @"{
                ""rating"": 8.32715,
                ""votes"": 9274,
                ""dist"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""3"": 55,
                  ""4"": 96,
                  ""5"": 183,
                  ""6"": 545,
                  ""7"": 1361,
                  ""8"": 2259,
                  ""9"": 1772,
                  ""10"": 2863
                }
              }";

        private const string JSON_NOT_VALID_4 =
            @"{
                ""ra"": 8.32715,
                ""vo"": 9274,
                ""dist"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""3"": 55,
                  ""4"": 96,
                  ""5"": 183,
                  ""6"": 545,
                  ""7"": 1361,
                  ""8"": 2259,
                  ""9"": 1772,
                  ""10"": 2863
                }
              }";

        private const string JSON_NOT_VALID_5 =
            @"{
                ""rating"": 8.32715,
                ""votes"": 9274,
                ""distribution"": {
                  ""1"": 78,
                  ""2"": 45,
                  ""33"": 55,
                  ""4"": 96,
                  ""5"": 183,
                  ""66"": 545,
                  ""7"": 1361,
                  ""8"": 2259,
                  ""str"": 1772,
                  ""10"": 2863
                }
              }";
    }
}
