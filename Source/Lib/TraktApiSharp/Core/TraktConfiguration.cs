﻿namespace TraktApiSharp.Core
{
    using System.Net.Http;

    public class TraktConfiguration
    {
        internal TraktConfiguration()
        {
            ApiVersion = 2;
            UseStagingUrl = false;
            ForceAuthorization = false;
        }

        internal static HttpClient HTTP_CLIENT = null;

        public int ApiVersion { get; set; }

        /// <summary>
        /// Gets or sets, whether the Trakt API staging environment should be used. This is disabled by default.
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#introduction/api-url">"Trakt API Doc - API URL"</a> for more information.
        /// </para>
        /// </summary>
        public bool UseStagingUrl { get; set; }

        /// <summary>Returns the Trakt API base URL based on, whether <see cref="UseStagingUrl" /> is false or true.</summary>
        public string BaseUrl => UseStagingUrl ? "https://api-staging.trakt.tv/" : "https://api.trakt.tv/";

        /// <summary>Gets or sets, whether authorization should be enforced, even if it is optional. This is disabled by default.</summary>
        public bool ForceAuthorization { get; set; }
    }
}
