﻿namespace TraktApiSharp.Objects.Get.People.Credits
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>Contains all Trakt shows where a Trakt person is in the cast or crew.</summary>
    public class TraktPersonShowCredits
    {
        /// <summary>
        /// Gets or sets a list of cast positions, in which a person is.
        /// See also <seealso cref="TraktPersonShowCreditsCastItem" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "cast")]
        public IEnumerable<TraktPersonShowCreditsCastItem> Cast { get; set; }

        /// <summary>
        /// Gets or sets a collection of crew positions, which a person has.
        /// See also <seealso cref="TraktPersonShowCreditsCrew" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "crew")]
        public TraktPersonShowCreditsCrew Crew { get; set; }
    }
}
