﻿namespace TraktApiSharp.Requests.Shows
{
    using Objects.Basic;

    internal sealed class TraktShowStatisticsRequest : ATraktShowRequest<TraktStatistics>
    {
        public override string UriTemplate => "shows/{id}/stats";
    }
}
