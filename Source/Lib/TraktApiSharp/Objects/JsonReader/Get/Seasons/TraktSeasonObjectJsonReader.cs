﻿namespace TraktApiSharp.Objects.JsonReader.Get.Seasons
{
    using Get.Episodes;
    using Newtonsoft.Json;
    using Objects.Get.Seasons.Implementations;
    using System;
    using System.IO;

    internal class TraktSeasonObjectJsonReader : ITraktObjectJsonReader<TraktSeason>
    {
        private const string PROPERTY_NAME_NUMBER = "number";
        private const string PROPERTY_NAME_IDS = "ids";
        private const string PROPERTY_NAME_RATING = "rating";
        private const string PROPERTY_NAME_VOTES = "votes";
        private const string PROPERTY_NAME_EPISODES_COUNT = "episode_count";
        private const string PROPERTY_NAME_AIRED_EPISODES = "aired_episodes";
        private const string PROPERTY_NAME_OVERVIEW = "overview";
        private const string PROPERTY_NAME_FIRST_AIRED = "first_aired";
        private const string PROPERTY_NAME_EPISODES = "episodes";

        public TraktSeason ReadObject(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return ReadObject(jsonReader);
            }
        }

        public TraktSeason ReadObject(JsonTextReader jsonReader)
        {
            if (jsonReader == null)
                return null;

            if (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartObject)
            {
                var idsObjectReader = new TraktSeasonIdsObjectJsonReader();
                var episodesArrayReader = new TraktEpisodeArrayJsonReader();
                var traktSeason = new TraktSeason();

                while (jsonReader.Read() && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case PROPERTY_NAME_NUMBER:
                            traktSeason.Number = jsonReader.ReadAsInt32();
                            break;
                        case PROPERTY_NAME_IDS:
                            traktSeason.Ids = idsObjectReader.ReadObject(jsonReader);
                            break;
                        case PROPERTY_NAME_RATING:
                            traktSeason.Rating = (float?)jsonReader.ReadAsDouble();
                            break;
                        case PROPERTY_NAME_VOTES:
                            traktSeason.Votes = jsonReader.ReadAsInt32();
                            break;
                        case PROPERTY_NAME_EPISODES_COUNT:
                            traktSeason.TotalEpisodesCount = jsonReader.ReadAsInt32();
                            break;
                        case PROPERTY_NAME_AIRED_EPISODES:
                            traktSeason.AiredEpisodesCount = jsonReader.ReadAsInt32();
                            break;
                        case PROPERTY_NAME_OVERVIEW:
                            traktSeason.Overview = jsonReader.ReadAsString();
                            break;
                        case PROPERTY_NAME_FIRST_AIRED:
                            {
                                DateTime dateTime;
                                if (JsonReaderHelper.ReadDateTimeValue(jsonReader, out dateTime))
                                    traktSeason.FirstAired = dateTime;

                                break;
                            }
                        case PROPERTY_NAME_EPISODES:
                            traktSeason.Episodes = episodesArrayReader.ReadArray(jsonReader);
                            break;
                        default:
                            JsonReaderHelper.OverreadInvalidContent(jsonReader);
                            break;
                    }
                }

                return traktSeason;
            }

            return null;
        }
    }
}
