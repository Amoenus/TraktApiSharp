﻿namespace TraktApiSharp.Experimental.Requests.Search
{
    using Base.Get;
    using Enums;
    using Interfaces;
    using Objects.Basic;
    using TraktApiSharp.Requests;
    using TraktApiSharp.Requests.Params;

    internal abstract class ATraktSearchRequest : ATraktPaginationGetRequest<TraktSearchResult>, ITraktSupportsExtendedInfo
    {
        internal ATraktSearchRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public TraktExtendedInfo ExtendedInfo { get; set; }

        internal TraktSearchResultType ResultTypes { get; set; }
    }
}
