﻿namespace TraktApiSharp.Objects.Get.People.JsonReader
{
    using Implementations;
    using Newtonsoft.Json;
    using Objects.JsonReader;
    using System.IO;

    internal class ITraktPersonIdsObjectJsonReader : ITraktObjectJsonReader<ITraktPersonIds>
    {
        private const string PROPERTY_NAME_TRAKT = "trakt";
        private const string PROPERTY_NAME_SLUG = "slug";
        private const string PROPERTY_NAME_IMDB = "imdb";
        private const string PROPERTY_NAME_TMDB = "tmdb";
        private const string PROPERTY_NAME_TVRAGE = "tvrage";

        public ITraktPersonIds ReadObject(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return ReadObject(jsonReader);
            }
        }

        public ITraktPersonIds ReadObject(JsonTextReader jsonReader)
        {
            if (jsonReader == null)
                return null;

            if (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartObject)
            {
                ITraktPersonIds traktPersonIds = new TraktPersonIds();

                while (jsonReader.Read() && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case PROPERTY_NAME_TRAKT:
                            uint traktId;

                            if (JsonReaderHelper.ReadUnsignedIntegerValue(jsonReader, out traktId))
                                traktPersonIds.Trakt = traktId;

                            break;
                        case PROPERTY_NAME_SLUG:
                            traktPersonIds.Slug = jsonReader.ReadAsString();
                            break;
                        case PROPERTY_NAME_IMDB:
                            traktPersonIds.Imdb = jsonReader.ReadAsString();
                            break;
                        case PROPERTY_NAME_TMDB:
                            uint tmdbId;

                            if (JsonReaderHelper.ReadUnsignedIntegerValue(jsonReader, out tmdbId))
                                traktPersonIds.Tmdb = tmdbId;

                            break;
                        case PROPERTY_NAME_TVRAGE:
                            uint tvRageId;

                            if (JsonReaderHelper.ReadUnsignedIntegerValue(jsonReader, out tvRageId))
                                traktPersonIds.TvRage = tvRageId;

                            break;
                        default:
                            JsonReaderHelper.OverreadInvalidContent(jsonReader);
                            break;
                    }
                }

                return traktPersonIds;
            }

            return null;
        }
    }
}
