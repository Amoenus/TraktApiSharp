﻿namespace TraktApiSharp.Experimental.Requests.Interfaces.Base
{
    internal interface ITraktUriBuildable : ITraktHasUriPathParameters
    {
        string UriTemplate { get; }

        string Url { get; }

        string BuildUrl();
    }
}
