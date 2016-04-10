﻿namespace TraktApiSharp.Requests.Shows.Seasons
{
    using Objects;
    using Objects.Shows.Seasons;

    internal class TraktSeasonWatchingUsersRequest : TraktGetByIdSeasonRequest<TraktListResult<TraktSeasonWatchingUser>, TraktSeasonWatchingUser>
    {
        internal TraktSeasonWatchingUsersRequest(TraktClient client) : base(client) { }

        protected override string UriTemplate => "shows/{id}/seasons/{season}/watching";

        protected override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Seasons;
    }
}
