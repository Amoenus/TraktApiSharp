﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using System;
    using TraktApiSharp.Requests;

    internal sealed class TraktUserCustomListDeleteRequest : ATraktUsersDeleteByIdRequest
    {
        internal TraktUserCustomListDeleteRequest(TraktClient client) : base(client) { }

        public override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Lists;

        public override string UriTemplate
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
