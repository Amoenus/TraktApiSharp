﻿namespace TraktApiSharp.Requests.WithoutOAuth.Shows.Episodes
{
    using Enums;
    using Objects.Basic;
    using System.Collections.Generic;

    internal class TraktEpisodeCommentsRequest : TraktGetByIdEpisodeRequest<TraktPaginationListResult<TraktComment>, TraktComment>
    {
        internal TraktEpisodeCommentsRequest(TraktClient client) : base(client) { }

        internal TraktCommentSortOrder? Sorting { get; set; }

        protected override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();

            if (Sorting.HasValue && Sorting.Value != TraktCommentSortOrder.Unspecified)
                uriParams.Add("sorting", Sorting.Value.AsString());

            return uriParams;
        }

        protected override string UriTemplate => "shows/{id}/seasons/{season}/episodes/{episode}/comments{/sorting}{?page,limit}";

        protected override bool SupportsPagination => true;

        protected override bool IsListResult => true;
    }
}
