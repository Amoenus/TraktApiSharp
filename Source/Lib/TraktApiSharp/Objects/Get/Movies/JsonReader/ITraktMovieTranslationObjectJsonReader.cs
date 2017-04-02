﻿namespace TraktApiSharp.Objects.Get.Movies.JsonReader
{
    using Implementations;
    using Newtonsoft.Json;
    using Objects.JsonReader;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ITraktMovieTranslationObjectJsonReader : ITraktObjectJsonReader<ITraktMovieTranslation>
    {
        private const string PROPERTY_NAME_TITLE = "title";
        private const string PROPERTY_NAME_OVERVIEW = "overview";
        private const string PROPERTY_NAME_LANGUAGE_CODE = "language";
        private const string PROPERTY_NAME_TAGLINE = "tagline";

        public Task<ITraktMovieTranslation> ReadObjectAsync(string json, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(json))
                return null;

            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return ReadObjectAsync(jsonReader, cancellationToken);
            }
        }

        public async Task<ITraktMovieTranslation> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jsonReader == null)
                return null;

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                ITraktMovieTranslation traktMovieTranslation = new TraktMovieTranslation();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case PROPERTY_NAME_TITLE:
                            traktMovieTranslation.Title = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case PROPERTY_NAME_OVERVIEW:
                            traktMovieTranslation.Overview = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case PROPERTY_NAME_LANGUAGE_CODE:
                            traktMovieTranslation.LanguageCode = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case PROPERTY_NAME_TAGLINE:
                            traktMovieTranslation.Tagline = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return traktMovieTranslation;
            }

            return null;
        }
    }
}
