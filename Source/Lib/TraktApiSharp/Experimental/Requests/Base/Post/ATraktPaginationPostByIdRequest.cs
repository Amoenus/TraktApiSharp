﻿namespace TraktApiSharp.Experimental.Requests.Base.Post
{
    using System;
    using System.Collections.Generic;

    internal abstract class ATraktPaginationPostByIdRequest<TItem, TRequestBody> : ATraktPaginationPostRequest<TItem, TRequestBody>
    {
        public ATraktPaginationPostByIdRequest(TraktClient client) : base(client) { }

        protected override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();
            uriParams.Add("id", Id);
            return uriParams;
        }

        protected override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(Id))
                throw new ArgumentException("id not valid");

            if (RequestBody == null)
                throw new ArgumentException("request body not valid");
        }
    }
}
