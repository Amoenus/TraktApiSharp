﻿namespace TraktApiSharp.Objects.Get.Watched
{
    using Enums;
    using Newtonsoft.Json;
    using Seasons;
    using Shows;
    using System;
    using System.Collections.Generic;

    /// <summary>Contains information about a watched Trakt show.</summary>
    public class TraktWatchedShow : ITraktWatchedShow
    {
        /// <summary>Gets or sets the number of plays for the watched show.</summary>
        [JsonProperty(PropertyName = "plays")]
        public int? Plays { get; set; }

        /// <summary>Gets or sets the UTC datetime, when the show was last watched.</summary>
        [JsonProperty(PropertyName = "last_watched_at")]
        public DateTime? LastWatchedAt { get; set; }

        /// <summary>Gets or sets the Trakt show. See also <seealso cref="TraktShow" />.<para>Nullable</para></summary>
        [JsonProperty(PropertyName = "show")]
        public ITraktShow Show { get; set; }

        /// <summary>
        /// Gets or sets a list of watched seasons in the watched show.
        /// See also <seealso cref="TraktWatchedShowSeason" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "seasons")]
        public IEnumerable<TraktWatchedShowSeason> WatchedSeasons { get; set; }

        [JsonIgnore]
        public string Title
        {
            get { return Show?.Title; }

            set
            {
                if (Show != null)
                    Show.Title = value;
            }
        }

        [JsonIgnore]
        public int? Year
        {
            get { return Show?.Year; }

            set
            {
                if (Show != null)
                    Show.Year = value;
            }
        }

        [JsonIgnore]
        public TraktShowIds Ids
        {
            get { return Show?.Ids; }

            set
            {
                if (Show != null)
                    Show.Ids = value;
            }
        }

        [JsonIgnore]
        public string Overview
        {
            get { return Show?.Overview; }

            set
            {
                if (Show != null)
                    Show.Overview = value;
            }
        }

        [JsonIgnore]
        public DateTime? FirstAired
        {
            get { return Show?.FirstAired; }

            set
            {
                if (Show != null)
                    Show.FirstAired = value;
            }
        }

        [JsonIgnore]
        public TraktShowAirs Airs
        {
            get { return Show?.Airs; }

            set
            {
                if (Show != null)
                    Show.Airs = value;
            }
        }

        [JsonIgnore]
        public int? Runtime
        {
            get { return Show?.Runtime; }

            set
            {
                if (Show != null)
                    Show.Runtime = value;
            }
        }

        [JsonIgnore]
        public string Certification
        {
            get { return Show?.Certification; }

            set
            {
                if (Show != null)
                    Show.Certification = value;
            }
        }

        [JsonIgnore]
        public string Network
        {
            get { return Show?.Network; }

            set
            {
                if (Show != null)
                    Show.Network = value;
            }
        }

        [JsonIgnore]
        public string CountryCode
        {
            get { return Show?.CountryCode; }

            set
            {
                if (Show != null)
                    Show.CountryCode = value;
            }
        }

        [JsonIgnore]
        public string Trailer
        {
            get { return Show?.Trailer; }

            set
            {
                if (Show != null)
                    Show.Trailer = value;
            }
        }

        [JsonIgnore]
        public string Homepage
        {
            get { return Show?.Homepage; }

            set
            {
                if (Show != null)
                    Show.Homepage = value;
            }
        }

        [JsonIgnore]
        public TraktShowStatus Status
        {
            get { return Show?.Status; }

            set
            {
                if (Show != null)
                    Show.Status = value;
            }
        }

        [JsonIgnore]
        public float? Rating
        {
            get { return Show?.Rating; }

            set
            {
                if (Show != null)
                    Show.Rating = value;
            }
        }

        [JsonIgnore]
        public int? Votes
        {
            get { return Show?.Votes; }

            set
            {
                if (Show != null)
                    Show.Votes = value;
            }
        }

        [JsonIgnore]
        public DateTime? UpdatedAt
        {
            get { return Show?.UpdatedAt; }

            set
            {
                if (Show != null)
                    Show.UpdatedAt = value;
            }
        }

        [JsonIgnore]
        public string LanguageCode
        {
            get { return Show?.LanguageCode; }

            set
            {
                if (Show != null)
                    Show.LanguageCode = value;
            }
        }

        [JsonIgnore]
        public IEnumerable<string> AvailableTranslationLanguageCodes
        {
            get { return Show?.AvailableTranslationLanguageCodes; }

            set
            {
                if (Show != null)
                    Show.AvailableTranslationLanguageCodes = value;
            }
        }

        [JsonIgnore]
        public IEnumerable<string> Genres
        {
            get { return Show?.Genres; }

            set
            {
                if (Show != null)
                    Show.Genres = value;
            }
        }

        [JsonIgnore]
        public int? AiredEpisodes
        {
            get { return Show?.AiredEpisodes; }

            set
            {
                if (Show != null)
                    Show.AiredEpisodes = value;
            }
        }

        [JsonIgnore]
        public IEnumerable<TraktSeason> Seasons
        {
            get { return Show?.Seasons; }

            set
            {
                if (Show != null)
                    Show.Seasons = value;
            }
        }
    }
}
