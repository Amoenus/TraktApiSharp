﻿namespace TraktApiSharp.Objects.Get.Movies.JsonReader
{
    using Enums;
    using Implementations;
    using Newtonsoft.Json;
    using Objects.JsonReader;
    using System;
    using System.IO;

    internal class ITraktMovieReleaseObjectJsonReader : ITraktObjectJsonReader<ITraktMovieRelease>
    {
        private const string PROPERTY_NAME_COUNTRY = "country";
        private const string PROPERTY_NAME_CERTIFICATION = "certification";
        private const string PROPERTY_NAME_RELEASE_DATE = "release_date";
        private const string PROPERTY_NAME_RELEASE_TYPE = "release_type";
        private const string PROPERTY_NAME_NOTE = "note";

        public ITraktMovieRelease ReadObject(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return ReadObject(jsonReader);
            }
        }

        public ITraktMovieRelease ReadObject(JsonTextReader jsonReader)
        {
            if (jsonReader == null)
                return null;

            if (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartObject)
            {
                ITraktMovieRelease traktMovieRelease = new TraktMovieRelease();

                while (jsonReader.Read() && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case PROPERTY_NAME_COUNTRY:
                            traktMovieRelease.CountryCode = jsonReader.ReadAsString();
                            break;
                        case PROPERTY_NAME_CERTIFICATION:
                            traktMovieRelease.Certification = jsonReader.ReadAsString();
                            break;
                        case PROPERTY_NAME_RELEASE_DATE:
                            DateTime dateTime;
                            if (JsonReaderHelper.ReadDateTimeValue(jsonReader, out dateTime))
                                traktMovieRelease.ReleaseDate = dateTime;

                            break;
                        case PROPERTY_NAME_RELEASE_TYPE:
                            TraktReleaseType releaseType = null;
                            JsonReaderHelper.ReadEnumerationValue(jsonReader, out releaseType);
                            traktMovieRelease.ReleaseType = releaseType;
                            break;
                        case PROPERTY_NAME_NOTE:
                            traktMovieRelease.Note = jsonReader.ReadAsString();
                            break;
                        default:
                            JsonReaderHelper.OverreadInvalidContent(jsonReader);
                            break;
                    }
                }

                return traktMovieRelease;
            }

            return null;
        }
    }
}