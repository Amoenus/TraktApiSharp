﻿namespace TraktApiSharp.Objects.Get.Episodes.JsonReader
{
    using Newtonsoft.Json;
    using Objects.JsonReader;
    using System.Collections.Generic;
    using System.IO;

    internal class ITraktEpisodeCollectionProgressArrayJsonReader : ITraktArrayJsonReader<ITraktEpisodeCollectionProgress>
    {
        public IEnumerable<ITraktEpisodeCollectionProgress> ReadArray(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return ReadArray(jsonReader);
            }
        }

        public IEnumerable<ITraktEpisodeCollectionProgress> ReadArray(JsonTextReader jsonReader)
        {
            if (jsonReader == null)
                return null;

            if (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartArray)
            {
                var episodeCollectionProgressReader = new ITraktEpisodeCollectionProgressObjectJsonReader();
                var traktEpisodeCollectionProgresses = new List<ITraktEpisodeCollectionProgress>();

                ITraktEpisodeCollectionProgress traktEpisodeCollectionProgress = episodeCollectionProgressReader.ReadObject(jsonReader);

                while (traktEpisodeCollectionProgress != null)
                {
                    traktEpisodeCollectionProgresses.Add(traktEpisodeCollectionProgress);
                    traktEpisodeCollectionProgress = episodeCollectionProgressReader.ReadObject(jsonReader);
                }

                return traktEpisodeCollectionProgresses;
            }

            return null;
        }
    }
}