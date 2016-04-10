﻿namespace TraktApiSharp.Requests.Movies.Common
{
    using Base.Get;
    using Enums;
    using Objects;
    using Objects.Movies.Common;
    using System.Collections.Generic;

    internal class TraktMoviesMostCollectedRequest : TraktGetRequest<TraktPaginationListResult<TraktMoviesMostCollectedItem>, TraktMoviesMostCollectedItem>
    {
        internal TraktMoviesMostCollectedRequest(TraktClient client) : base(client) { Period = TraktPeriod.Weekly; }

        internal TraktPeriod Period { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetPathParameters()
        {
            return new Dictionary<string, string> { { "period", Period.AsString() } };
        }

        protected override string UriTemplate => "movies/collected/{period}";

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.NotRequired;

        protected override bool SupportsPagination => true;

        protected override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Movies;
    }
}