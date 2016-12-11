﻿namespace TraktApiSharp.Experimental.Requests.Interfaces
{
    using TraktApiSharp.Requests;

    internal interface ITraktPagination
    {
        int? Page { get; set; }

        int? Limit { get; set; }

        TraktPaginationOptions PaginationOptions { get; set; }
    }
}
