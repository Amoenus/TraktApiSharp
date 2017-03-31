﻿namespace TraktApiSharp.Tests.Objects.Get.Episodes.JsonReader
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using Traits;
    using TraktApiSharp.Objects.Get.Episodes.Implementations;
    using TraktApiSharp.Objects.Get.Episodes.JsonReader;
    using TraktApiSharp.Objects.JsonReader;
    using Xunit;

    [Category("Objects.JsonReader.Get.Episodes")]
    public class TraktEpisodeWatchedProgressArrayJsonReader_Tests
    {
        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_Implements_ITraktArrayJsonReader_Interface()
        {
            typeof(TraktEpisodeWatchedProgressArrayJsonReader).GetInterfaces().Should().Contain(typeof(ITraktArrayJsonReader<TraktEpisodeWatchedProgress>));
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Empty_Array()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_EMPTY_ARRAY);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Complete()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_COMPLETE);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().Be(1);
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeTrue();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Incomplete_1()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_INCOMPLETE_1);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().BeNull();
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeTrue();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Incomplete_2()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_INCOMPLETE_2);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().Be(1);
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeNull();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Incomplete_3()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_INCOMPLETE_3);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().Be(1);
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeTrue();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Incomplete_4()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_INCOMPLETE_4);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().Be(1);
            watchedProgress[0].Completed.Should().BeNull();
            watchedProgress[0].LastWatchedAt.Should().BeNull();

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeTrue();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Incomplete_5()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_INCOMPLETE_5);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().Be(1);
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().BeNull();
            watchedProgress[1].Completed.Should().BeTrue();
            watchedProgress[1].LastWatchedAt.Should().BeNull();

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Incomplete_6()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_INCOMPLETE_6);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().Be(1);
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeTrue();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().BeNull();
            watchedProgress[2].Completed.Should().BeNull();
            watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Not_Valid_1()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_NOT_VALID_1);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().BeNull();
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeTrue();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Not_Valid_2()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_NOT_VALID_2);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().Be(1);
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeNull();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Not_Valid_3()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_NOT_VALID_3);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().Be(1);
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeTrue();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Not_Valid_4()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgresses = jsonReader.ReadArray(JSON_NOT_VALID_4);
            traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

            var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

            watchedProgress[0].Number.Should().BeNull();
            watchedProgress[0].Completed.Should().BeTrue();
            watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[1].Number.Should().Be(2);
            watchedProgress[1].Completed.Should().BeNull();
            watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            watchedProgress[2].Number.Should().Be(3);
            watchedProgress[2].Completed.Should().BeTrue();
            watchedProgress[2].LastWatchedAt.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Null()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgress = jsonReader.ReadArray(default(string));
            traktEpisodeWatchedProgress.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_Json_String_Empty()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgress = jsonReader.ReadArray(string.Empty);
            traktEpisodeWatchedProgress.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Empty_Array()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_EMPTY_ARRAY))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.BeEmpty();
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Complete()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_COMPLETE))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().Be(1);
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeTrue();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Incomplete_1()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_1))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().BeNull();
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeTrue();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Incomplete_2()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_2))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().Be(1);
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeNull();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Incomplete_3()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_3))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().Be(1);
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeTrue();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().BeNull();
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Incomplete_4()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_4))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().Be(1);
                watchedProgress[0].Completed.Should().BeNull();
                watchedProgress[0].LastWatchedAt.Should().BeNull();

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeTrue();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Incomplete_5()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_5))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().Be(1);
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().BeNull();
                watchedProgress[1].Completed.Should().BeTrue();
                watchedProgress[1].LastWatchedAt.Should().BeNull();

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Incomplete_6()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_INCOMPLETE_6))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().Be(1);
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeTrue();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().BeNull();
                watchedProgress[2].Completed.Should().BeNull();
                watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Not_Valid_1()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_1))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().BeNull();
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeTrue();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Not_Valid_2()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_2))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().Be(1);
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeNull();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Not_Valid_3()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_3))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().Be(1);
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeTrue();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().BeNull();
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Not_Valid_4()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID_4))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgresses = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgresses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);

                var watchedProgress = traktEpisodeWatchedProgresses.ToArray();

                watchedProgress[0].Number.Should().BeNull();
                watchedProgress[0].Completed.Should().BeTrue();
                watchedProgress[0].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[1].Number.Should().Be(2);
                watchedProgress[1].Completed.Should().BeNull();
                watchedProgress[1].LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

                watchedProgress[2].Number.Should().Be(3);
                watchedProgress[2].Completed.Should().BeTrue();
                watchedProgress[2].LastWatchedAt.Should().BeNull();
            }
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Null()
        {
            var jsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            var traktEpisodeWatchedProgress = jsonReader.ReadArray(default(JsonTextReader));
            traktEpisodeWatchedProgress.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgressArrayJsonReader_ReadArray_From_JsonReader_Empty()
        {
            var traktJsonReader = new TraktEpisodeWatchedProgressArrayJsonReader();

            using (var reader = new StringReader(string.Empty))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodeWatchedProgress = traktJsonReader.ReadArray(jsonReader);
                traktEpisodeWatchedProgress.Should().BeNull();
            }
        }

        private const string JSON_EMPTY_ARRAY = @"[]";

        private const string JSON_COMPLETE =
            @"[
                {
                  ""number"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_INCOMPLETE_1 =
            @"[
                {
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_INCOMPLETE_2 =
            @"[
                {
                  ""number"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_INCOMPLETE_3 =
            @"[
                {
                  ""number"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true
                }
              ]";

        private const string JSON_INCOMPLETE_4 =
            @"[
                {
                  ""number"": 1
                },
                {
                  ""number"": 2,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_INCOMPLETE_5 =
            @"[
                {
                  ""number"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""completed"": true
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_INCOMPLETE_6 =
            @"[
                {
                  ""number"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_NOT_VALID_1 =
            @"[
                {
                  ""nu"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_NOT_VALID_2 =
            @"[
                {
                  ""number"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""com"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_NOT_VALID_3 =
            @"[
                {
                  ""number"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""watat"": ""2011-04-18T01:00:00.000Z""
                }
              ]";

        private const string JSON_NOT_VALID_4 =
            @"[
                {
                  ""nu"": 1,
                  ""completed"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 2,
                  ""com"": true,
                  ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
                },
                {
                  ""number"": 3,
                  ""completed"": true,
                  ""wat"": ""2011-04-18T01:00:00.000Z""
                }
              ]";
    }
}
