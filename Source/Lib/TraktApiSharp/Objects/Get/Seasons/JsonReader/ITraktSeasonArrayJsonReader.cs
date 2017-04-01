﻿namespace TraktApiSharp.Objects.Get.Seasons.JsonReader
{
    using Newtonsoft.Json;
    using Objects.JsonReader;
    using System.Collections.Generic;
    using System.IO;

    internal class ITraktSeasonArrayJsonReader : ITraktArrayJsonReader<ITraktSeason>
    {
        public IEnumerable<ITraktSeason> ReadArray(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return ReadArray(jsonReader);
            }
        }

        public IEnumerable<ITraktSeason> ReadArray(JsonTextReader jsonReader)
        {
            if (jsonReader == null)
                return null;

            if (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartArray)
            {
                var seasonReader = new ITraktSeasonObjectJsonReader();
                var traktSeasons = new List<ITraktSeason>();

                ITraktSeason traktSeason = seasonReader.ReadObject(jsonReader);

                while (traktSeason != null)
                {
                    traktSeasons.Add(traktSeason);
                    traktSeason = seasonReader.ReadObject(jsonReader);
                }

                return traktSeasons;
            }

            return null;
        }
    }
}
