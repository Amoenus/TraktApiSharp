﻿namespace TraktApiSharp.Experimental.Requests.Base.Get
{
    using System.Net.Http;

    internal abstract class ATraktPaginationGetRequest<TItem> : ATraktPaginationRequest<TItem, object>
    {
        protected override HttpMethod Method => HttpMethod.Get;
    }
}
