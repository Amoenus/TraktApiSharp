﻿namespace TraktApiSharp.Requests.Shows
{
    using Base.Get;
    using Objects;
    using Objects.Shows;

    internal class TraktShowCommentsRequest : TraktGetByIdRequest<TraktPaginationListResult<TraktShowComment>, TraktShowComment>
    {
        internal TraktShowCommentsRequest(TraktClient client) : base(client) { }

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.NotRequired;

        protected override string UriTemplate => "shows/{id}/comments";

        protected override bool SupportsPagination => true;

        protected override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Shows;
    }
}
