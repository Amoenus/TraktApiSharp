﻿namespace TraktApiSharp.Experimental.Requests.Users
{
    using Base.Get;
    using Objects.Basic;
    using System;
    using TraktApiSharp.Requests;

    internal sealed class TraktUserListCommentsRequest : ATraktPaginationGetByIdRequest<TraktComment>
    {
        internal TraktUserListCommentsRequest(TraktClient client) : base(client) {}

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Lists;

        public override string UriTemplate => throw new NotImplementedException();
    }
}
