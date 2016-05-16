﻿namespace TraktApiSharp.Requests.WithOAuth.Users
{
    using Base.Get;
    using Enums;
    using Objects.Basic;
    using Objects.Get.Users;
    using System.Collections.Generic;

    internal class TraktUserHiddenItemsRequest : TraktGetRequest<TraktPaginationListResult<TraktUserHiddenItem>, TraktUserHiddenItem>
    {
        internal TraktUserHiddenItemsRequest(TraktClient client) : base(client) { }

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.Required;

        internal TraktHiddenItemsSection Section { get; set; }

        internal TraktHiddenItemType? Type { get; set; }

        protected override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();

            uriParams.Add("section", Section.AsString());

            if (Type.HasValue && Type.Value != TraktHiddenItemType.Unspecified)
                uriParams.Add("type", Type.Value.ToString().ToLower());

            return uriParams;
        }

        protected override string UriTemplate => "users/hidden/{section}{?type}";

        protected override bool SupportsPagination => true;

        protected override bool IsListResult => true;
    }
}
