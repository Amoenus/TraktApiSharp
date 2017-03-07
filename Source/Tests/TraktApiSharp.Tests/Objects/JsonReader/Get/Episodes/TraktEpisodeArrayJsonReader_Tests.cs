﻿namespace TraktApiSharp.Tests.Objects.JsonReader.Get.Episodes
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using Traits;
    using TraktApiSharp.Objects.Get.Episodes;
    using TraktApiSharp.Objects.JsonReader;
    using TraktApiSharp.Objects.JsonReader.Get.Episodes;
    using Xunit;

    [Category("Objects.JsonReader.Get.Episodes")]
    public class TraktEpisodeArrayJsonReader_Tests
    {
        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_Implements_ITraktArrayJsonReader_Interface()
        {
            typeof(TraktEpisodeArrayJsonReader).GetInterfaces().Should().Contain(typeof(ITraktArrayJsonReader<TraktEpisode>));
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_Json_String_Empty_Array()
        {
            var jsonReader = new TraktEpisodeArrayJsonReader();

            var traktEpisodes = jsonReader.ReadArray(JSON_EMPTY_ARRAY);
            traktEpisodes.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_Json_String_Minimal_Complete()
        {
            var jsonReader = new TraktEpisodeArrayJsonReader();

            var traktEpisodes = jsonReader.ReadArray(MINIMAL_JSON_COMPLETE);
            traktEpisodes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);

            var episodes = traktEpisodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].SeasonNumber.Should().Be(1);
            episodes[0].Number.Should().Be(1);
            episodes[0].Title.Should().Be("Winter Is Coming");
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(73640U);
            episodes[0].Ids.Tvdb.Should().Be(3254641U);
            episodes[0].Ids.Imdb.Should().Be("tt1480055");
            episodes[0].Ids.Tmdb.Should().Be(63056U);
            episodes[0].Ids.TvRage.Should().Be(1065008299U);
            episodes[0].NumberAbsolute.Should().NotHaveValue();
            episodes[0].Overview.Should().BeNullOrEmpty();
            episodes[0].Runtime.Should().NotHaveValue();
            episodes[0].Rating.Should().NotHaveValue();
            episodes[0].Votes.Should().NotHaveValue();
            episodes[0].FirstAired.Should().NotHaveValue();
            episodes[0].UpdatedAt.Should().NotHaveValue();
            episodes[0].AvailableTranslationLanguageCodes.Should().BeNull();
            episodes[0].Translations.Should().BeNull();

            episodes[1].Should().NotBeNull();
            episodes[1].SeasonNumber.Should().Be(1);
            episodes[1].Number.Should().Be(2);
            episodes[1].Title.Should().Be("The Kingsroad");
            episodes[1].Ids.Should().NotBeNull();
            episodes[1].Ids.Trakt.Should().Be(73641U);
            episodes[1].Ids.Tvdb.Should().Be(3436411U);
            episodes[1].Ids.Imdb.Should().Be("tt1668746");
            episodes[1].Ids.Tmdb.Should().Be(63057U);
            episodes[1].Ids.TvRage.Should().Be(1065023912U);
            episodes[1].NumberAbsolute.Should().NotHaveValue();
            episodes[1].Overview.Should().BeNullOrEmpty();
            episodes[1].Runtime.Should().NotHaveValue();
            episodes[1].Rating.Should().NotHaveValue();
            episodes[1].Votes.Should().NotHaveValue();
            episodes[1].FirstAired.Should().NotHaveValue();
            episodes[1].UpdatedAt.Should().NotHaveValue();
            episodes[1].AvailableTranslationLanguageCodes.Should().BeNull();
            episodes[1].Translations.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_Json_String_Full_Complete()
        {
            var jsonReader = new TraktEpisodeArrayJsonReader();

            var traktEpisodes = jsonReader.ReadArray(FULL_JSON_COMPLETE);
            traktEpisodes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);

            var episodes = traktEpisodes.ToArray();

            episodes[0].Should().NotBeNull();
            episodes[0].SeasonNumber.Should().Be(1);
            episodes[0].Number.Should().Be(1);
            episodes[0].Title.Should().Be("Winter Is Coming");
            episodes[0].Ids.Should().NotBeNull();
            episodes[0].Ids.Trakt.Should().Be(73640U);
            episodes[0].Ids.Tvdb.Should().Be(3254641U);
            episodes[0].Ids.Imdb.Should().Be("tt1480055");
            episodes[0].Ids.Tmdb.Should().Be(63056U);
            episodes[0].Ids.TvRage.Should().Be(1065008299U);
            episodes[0].NumberAbsolute.Should().Be(1);
            episodes[0].Overview.Should().Be("Ned Stark, Lord of Winterfell learns that his mentor, Jon Arryn, has died and that King Robert is on his way north to offer Ned Arryn’s position as the King’s Hand. Across the Narrow Sea in Pentos, Viserys Targaryen plans to wed his sister Daenerys to the nomadic Dothraki warrior leader, Khal Drogo to forge an alliance to take the throne.");
            episodes[0].Runtime.Should().Be(55);
            episodes[0].Rating.Should().Be(9.0f);
            episodes[0].Votes.Should().Be(111);
            episodes[0].FirstAired.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
            episodes[0].UpdatedAt.Should().Be(DateTime.Parse("2014-08-29T23:16:39.000Z").ToUniversalTime());
            episodes[0].AvailableTranslationLanguageCodes.Should().NotBeNull().And.HaveCount(2).And.Contain("en", "es");
            episodes[0].Translations.Should().NotBeNull().And.HaveCount(2);

            var translations = episodes[0].Translations.ToArray();

            translations[0].Should().NotBeNull();
            translations[0].Title.Should().Be("Winter Is Coming");
            translations[0].Overview.Should().Be("Jon Arryn, the Hand of the King, is dead. King Robert Baratheon plans to ask his oldest friend, Eddard Stark, to take Jon's place. Across the sea, Viserys Targaryen plans to wed his sister to a nomadic warlord in exchange for an army.");
            translations[0].LanguageCode.Should().Be("en");

            translations[1].Should().NotBeNull();
            translations[1].Title.Should().Be("Se acerca el invierno");
            translations[1].Overview.Should().Be("El Lord Ned Stark está preocupado por los perturbantes reportes de un desertor del Nights Watch; El Rey Robert y los Lannisters llegan a Winterfell; el exiliado Viserys Targaryen forja una nueva y poderosa alianza.");
            translations[1].LanguageCode.Should().Be("es");

            episodes[1].Should().NotBeNull();
            episodes[1].SeasonNumber.Should().Be(1);
            episodes[1].Number.Should().Be(2);
            episodes[1].Title.Should().Be("The Kingsroad");
            episodes[1].Ids.Should().NotBeNull();
            episodes[1].Ids.Trakt.Should().Be(73641U);
            episodes[1].Ids.Tvdb.Should().Be(3436411U);
            episodes[1].Ids.Imdb.Should().Be("tt1668746");
            episodes[1].Ids.Tmdb.Should().Be(63057U);
            episodes[1].Ids.TvRage.Should().Be(1065023912U);
            episodes[1].NumberAbsolute.Should().Be(2);
            episodes[1].Overview.Should().Be("Having agreed to become the King’s Hand, Ned leaves Winterfell with daughters Sansa and Arya, while Catelyn stays behind in Winterfell. Jon Snow heads north to join the brotherhood of the Night’s Watch. Tyrion decides to forego the trip south with his family, instead joining Jon in the entourage heading to the Wall. Viserys bides his time in hopes of winning back the throne, while Daenerys focuses her attention on learning how to please her new husband, Drogo.");
            episodes[1].Runtime.Should().Be(55);
            episodes[1].Rating.Should().Be(8.22255f);
            episodes[1].Votes.Should().Be(6183);
            episodes[1].FirstAired.Should().Be(DateTime.Parse("2011-04-25T01:00:00.000Z").ToUniversalTime());
            episodes[1].UpdatedAt.Should().Be(DateTime.Parse("2017-03-05T14:47:14.000Z").ToUniversalTime());
            episodes[1].AvailableTranslationLanguageCodes.Should().NotBeNull().And.HaveCount(2).And.Contain("en", "es");
            episodes[1].Translations.Should().NotBeNull().And.HaveCount(2);

            translations = episodes[1].Translations.ToArray();

            translations[0].Should().NotBeNull();
            translations[0].Title.Should().Be("The Kingsroad");
            translations[0].Overview.Should().Be("While Bran recovers from his fall, Ned takes only his daughters to Kings Landing. Jon Snow goes with his uncle Benjen to The Wall. Tyrion joins them.");
            translations[0].LanguageCode.Should().Be("en");

            translations[1].Should().NotBeNull();
            translations[1].Title.Should().Be("El Camino Real");
            translations[1].Overview.Should().Be("Cuando Bran sobrevive milagrosamente a su caída de la torre, Cersei y Jaime conspiran para asegurar su silencio; Jon Snow y Tyrion se dirigen a El Muro; al convertirse en la mano derecha del rey, Ned deja Winterfell con sus hijas Sansa y Arya.");
            translations[1].LanguageCode.Should().Be("es");
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_Json_String_Null()
        {
            var jsonReader = new TraktEpisodeArrayJsonReader();

            var traktEpisodes = jsonReader.ReadArray(default(string));
            traktEpisodes.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_Json_String_Empty()
        {
            var jsonReader = new TraktEpisodeArrayJsonReader();

            var traktEpisodes = jsonReader.ReadArray(string.Empty);
            traktEpisodes.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_JsonReader_Empty_Array()
        {
            var traktJsonReader = new TraktEpisodeArrayJsonReader();

            using (var reader = new StringReader(JSON_EMPTY_ARRAY))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodes = traktJsonReader.ReadArray(jsonReader);
                traktEpisodes.Should().NotBeNull().And.BeEmpty();
            }
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_JsonReader_Minimal_Complete()
        {
            var traktJsonReader = new TraktEpisodeArrayJsonReader();

            using (var reader = new StringReader(MINIMAL_JSON_COMPLETE))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodes = traktJsonReader.ReadArray(jsonReader);
                traktEpisodes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);

                var episodes = traktEpisodes.ToArray();

                episodes[0].Should().NotBeNull();
                episodes[0].SeasonNumber.Should().Be(1);
                episodes[0].Number.Should().Be(1);
                episodes[0].Title.Should().Be("Winter Is Coming");
                episodes[0].Ids.Should().NotBeNull();
                episodes[0].Ids.Trakt.Should().Be(73640U);
                episodes[0].Ids.Tvdb.Should().Be(3254641U);
                episodes[0].Ids.Imdb.Should().Be("tt1480055");
                episodes[0].Ids.Tmdb.Should().Be(63056U);
                episodes[0].Ids.TvRage.Should().Be(1065008299U);
                episodes[0].NumberAbsolute.Should().NotHaveValue();
                episodes[0].Overview.Should().BeNullOrEmpty();
                episodes[0].Runtime.Should().NotHaveValue();
                episodes[0].Rating.Should().NotHaveValue();
                episodes[0].Votes.Should().NotHaveValue();
                episodes[0].FirstAired.Should().NotHaveValue();
                episodes[0].UpdatedAt.Should().NotHaveValue();
                episodes[0].AvailableTranslationLanguageCodes.Should().BeNull();
                episodes[0].Translations.Should().BeNull();

                episodes[1].Should().NotBeNull();
                episodes[1].SeasonNumber.Should().Be(1);
                episodes[1].Number.Should().Be(2);
                episodes[1].Title.Should().Be("The Kingsroad");
                episodes[1].Ids.Should().NotBeNull();
                episodes[1].Ids.Trakt.Should().Be(73641U);
                episodes[1].Ids.Tvdb.Should().Be(3436411U);
                episodes[1].Ids.Imdb.Should().Be("tt1668746");
                episodes[1].Ids.Tmdb.Should().Be(63057U);
                episodes[1].Ids.TvRage.Should().Be(1065023912U);
                episodes[1].NumberAbsolute.Should().NotHaveValue();
                episodes[1].Overview.Should().BeNullOrEmpty();
                episodes[1].Runtime.Should().NotHaveValue();
                episodes[1].Rating.Should().NotHaveValue();
                episodes[1].Votes.Should().NotHaveValue();
                episodes[1].FirstAired.Should().NotHaveValue();
                episodes[1].UpdatedAt.Should().NotHaveValue();
                episodes[1].AvailableTranslationLanguageCodes.Should().BeNull();
                episodes[1].Translations.Should().BeNull();
            }
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_JsonReader_Full_Complete()
        {
            var traktJsonReader = new TraktEpisodeArrayJsonReader();

            using (var reader = new StringReader(FULL_JSON_COMPLETE))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodes = traktJsonReader.ReadArray(jsonReader);
                traktEpisodes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);

                var episodes = traktEpisodes.ToArray();

                episodes[0].Should().NotBeNull();
                episodes[0].SeasonNumber.Should().Be(1);
                episodes[0].Number.Should().Be(1);
                episodes[0].Title.Should().Be("Winter Is Coming");
                episodes[0].Ids.Should().NotBeNull();
                episodes[0].Ids.Trakt.Should().Be(73640U);
                episodes[0].Ids.Tvdb.Should().Be(3254641U);
                episodes[0].Ids.Imdb.Should().Be("tt1480055");
                episodes[0].Ids.Tmdb.Should().Be(63056U);
                episodes[0].Ids.TvRage.Should().Be(1065008299U);
                episodes[0].NumberAbsolute.Should().Be(1);
                episodes[0].Overview.Should().Be("Ned Stark, Lord of Winterfell learns that his mentor, Jon Arryn, has died and that King Robert is on his way north to offer Ned Arryn’s position as the King’s Hand. Across the Narrow Sea in Pentos, Viserys Targaryen plans to wed his sister Daenerys to the nomadic Dothraki warrior leader, Khal Drogo to forge an alliance to take the throne.");
                episodes[0].Runtime.Should().Be(55);
                episodes[0].Rating.Should().Be(9.0f);
                episodes[0].Votes.Should().Be(111);
                episodes[0].FirstAired.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
                episodes[0].UpdatedAt.Should().Be(DateTime.Parse("2014-08-29T23:16:39.000Z").ToUniversalTime());
                episodes[0].AvailableTranslationLanguageCodes.Should().NotBeNull().And.HaveCount(2).And.Contain("en", "es");
                episodes[0].Translations.Should().NotBeNull().And.HaveCount(2);

                var translations = episodes[0].Translations.ToArray();

                translations[0].Should().NotBeNull();
                translations[0].Title.Should().Be("Winter Is Coming");
                translations[0].Overview.Should().Be("Jon Arryn, the Hand of the King, is dead. King Robert Baratheon plans to ask his oldest friend, Eddard Stark, to take Jon's place. Across the sea, Viserys Targaryen plans to wed his sister to a nomadic warlord in exchange for an army.");
                translations[0].LanguageCode.Should().Be("en");

                translations[1].Should().NotBeNull();
                translations[1].Title.Should().Be("Se acerca el invierno");
                translations[1].Overview.Should().Be("El Lord Ned Stark está preocupado por los perturbantes reportes de un desertor del Nights Watch; El Rey Robert y los Lannisters llegan a Winterfell; el exiliado Viserys Targaryen forja una nueva y poderosa alianza.");
                translations[1].LanguageCode.Should().Be("es");

                episodes[1].Should().NotBeNull();
                episodes[1].SeasonNumber.Should().Be(1);
                episodes[1].Number.Should().Be(2);
                episodes[1].Title.Should().Be("The Kingsroad");
                episodes[1].Ids.Should().NotBeNull();
                episodes[1].Ids.Trakt.Should().Be(73641U);
                episodes[1].Ids.Tvdb.Should().Be(3436411U);
                episodes[1].Ids.Imdb.Should().Be("tt1668746");
                episodes[1].Ids.Tmdb.Should().Be(63057U);
                episodes[1].Ids.TvRage.Should().Be(1065023912U);
                episodes[1].NumberAbsolute.Should().Be(2);
                episodes[1].Overview.Should().Be("Having agreed to become the King’s Hand, Ned leaves Winterfell with daughters Sansa and Arya, while Catelyn stays behind in Winterfell. Jon Snow heads north to join the brotherhood of the Night’s Watch. Tyrion decides to forego the trip south with his family, instead joining Jon in the entourage heading to the Wall. Viserys bides his time in hopes of winning back the throne, while Daenerys focuses her attention on learning how to please her new husband, Drogo.");
                episodes[1].Runtime.Should().Be(55);
                episodes[1].Rating.Should().Be(8.22255f);
                episodes[1].Votes.Should().Be(6183);
                episodes[1].FirstAired.Should().Be(DateTime.Parse("2011-04-25T01:00:00.000Z").ToUniversalTime());
                episodes[1].UpdatedAt.Should().Be(DateTime.Parse("2017-03-05T14:47:14.000Z").ToUniversalTime());
                episodes[1].AvailableTranslationLanguageCodes.Should().NotBeNull().And.HaveCount(2).And.Contain("en", "es");
                episodes[1].Translations.Should().NotBeNull().And.HaveCount(2);

                translations = episodes[1].Translations.ToArray();

                translations[0].Should().NotBeNull();
                translations[0].Title.Should().Be("The Kingsroad");
                translations[0].Overview.Should().Be("While Bran recovers from his fall, Ned takes only his daughters to Kings Landing. Jon Snow goes with his uncle Benjen to The Wall. Tyrion joins them.");
                translations[0].LanguageCode.Should().Be("en");

                translations[1].Should().NotBeNull();
                translations[1].Title.Should().Be("El Camino Real");
                translations[1].Overview.Should().Be("Cuando Bran sobrevive milagrosamente a su caída de la torre, Cersei y Jaime conspiran para asegurar su silencio; Jon Snow y Tyrion se dirigen a El Muro; al convertirse en la mano derecha del rey, Ned deja Winterfell con sus hijas Sansa y Arya.");
                translations[1].LanguageCode.Should().Be("es");
            }
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_JsonReader_Null()
        {
            var jsonReader = new TraktEpisodeArrayJsonReader();

            var traktEpisodes = jsonReader.ReadArray(default(JsonTextReader));
            traktEpisodes.Should().BeNull();
        }

        [Fact]
        public void Test_TraktEpisodeArrayJsonReader_ReadArray_From_JsonReader_Empty()
        {
            var traktJsonReader = new TraktEpisodeArrayJsonReader();

            using (var reader = new StringReader(string.Empty))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktEpisodes = traktJsonReader.ReadArray(jsonReader);
                traktEpisodes.Should().BeNull();
            }
        }

        private const string JSON_EMPTY_ARRAY = @"[]";

        private const string MINIMAL_JSON_COMPLETE =
            @"[
                {
                  ""season"": 1,
                  ""number"": 1,
                  ""title"": ""Winter Is Coming"",
                  ""ids"": {
                    ""trakt"": 73640,
                    ""tvdb"": 3254641,
                    ""imdb"": ""tt1480055"",
                    ""tmdb"": 63056,
                    ""tvrage"": 1065008299
                  }
                },
                {
                  ""season"": 1,
                  ""number"": 2,
                  ""title"": ""The Kingsroad"",
                  ""ids"": {
                    ""trakt"": 73641,
                    ""tvdb"": 3436411,
                    ""imdb"": ""tt1668746"",
                    ""tmdb"": 63057,
                    ""tvrage"": 1065023912
                  }
                }
              ]";

        private const string FULL_JSON_COMPLETE =
            @"[
                {
                  ""season"": 1,
                  ""number"": 1,
                  ""title"": ""Winter Is Coming"",
                  ""ids"": {
                    ""trakt"": 73640,
                    ""tvdb"": 3254641,
                    ""imdb"": ""tt1480055"",
                    ""tmdb"": 63056,
                    ""tvrage"": 1065008299
                  },
                  ""number_abs"": 1,
                  ""overview"": ""Ned Stark, Lord of Winterfell learns that his mentor, Jon Arryn, has died and that King Robert is on his way north to offer Ned Arryn’s position as the King’s Hand. Across the Narrow Sea in Pentos, Viserys Targaryen plans to wed his sister Daenerys to the nomadic Dothraki warrior leader, Khal Drogo to forge an alliance to take the throne."",
                  ""first_aired"": ""2011-04-18T01:00:00.000Z"",
                  ""updated_at"": ""2014-08-29T23:16:39.000Z"",
                  ""rating"": 9,
                  ""votes"": 111,
                  ""available_translations"": [
                    ""en"",
                    ""es""
                  ],
                  ""runtime"": 55,
                  ""translations"": [
                    {
                      ""title"": ""Winter Is Coming"",
                      ""overview"": ""Jon Arryn, the Hand of the King, is dead. King Robert Baratheon plans to ask his oldest friend, Eddard Stark, to take Jon's place. Across the sea, Viserys Targaryen plans to wed his sister to a nomadic warlord in exchange for an army."",
                      ""language"": ""en""
                    },
                    {
                      ""title"": ""Se acerca el invierno"",
                      ""overview"": ""El Lord Ned Stark está preocupado por los perturbantes reportes de un desertor del Nights Watch; El Rey Robert y los Lannisters llegan a Winterfell; el exiliado Viserys Targaryen forja una nueva y poderosa alianza."",
                      ""language"": ""es""
                    }
                  ]
                },
                {
                  ""season"": 1,
                  ""number"": 2,
                  ""title"": ""The Kingsroad"",
                  ""ids"": {
                    ""trakt"": 73641,
                    ""tvdb"": 3436411,
                    ""imdb"": ""tt1668746"",
                    ""tmdb"": 63057,
                    ""tvrage"": 1065023912
                  },
                  ""number_abs"": 2,
                  ""overview"": ""Having agreed to become the King’s Hand, Ned leaves Winterfell with daughters Sansa and Arya, while Catelyn stays behind in Winterfell. Jon Snow heads north to join the brotherhood of the Night’s Watch. Tyrion decides to forego the trip south with his family, instead joining Jon in the entourage heading to the Wall. Viserys bides his time in hopes of winning back the throne, while Daenerys focuses her attention on learning how to please her new husband, Drogo."",
                  ""first_aired"": ""2011-04-25T01:00:00.000Z"",
                  ""updated_at"": ""2017-03-05T14:47:14.000Z"",
                  ""rating"": 8.22255,
                  ""votes"": 6183,
                  ""available_translations"": [
                    ""en"",
                    ""es""
                  ],
                  ""runtime"": 55,
                  ""translations"": [
                    {
                      ""title"": ""The Kingsroad"",
                      ""overview"": ""While Bran recovers from his fall, Ned takes only his daughters to Kings Landing. Jon Snow goes with his uncle Benjen to The Wall. Tyrion joins them."",
                      ""language"": ""en""
                    },
                    {
                      ""title"": ""El Camino Real"",
                      ""overview"": ""Cuando Bran sobrevive milagrosamente a su caída de la torre, Cersei y Jaime conspiran para asegurar su silencio; Jon Snow y Tyrion se dirigen a El Muro; al convertirse en la mano derecha del rey, Ned deja Winterfell con sus hijas Sansa y Arya."",
                      ""language"": ""es""
                    }
                  ]
                }
              ]";
    }
}