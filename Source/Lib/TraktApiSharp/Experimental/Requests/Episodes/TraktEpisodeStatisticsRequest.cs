﻿namespace TraktApiSharp.Experimental.Requests.Episodes
{
    using Base.Get;
    using Objects.Basic;
    using System;
    using TraktApiSharp.Requests;

    internal sealed class TraktEpisodeStatisticsRequest : ATraktSingleItemGetByIdRequest<TraktStatistics>
    {
        public TraktEpisodeStatisticsRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override string UriTemplate
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
