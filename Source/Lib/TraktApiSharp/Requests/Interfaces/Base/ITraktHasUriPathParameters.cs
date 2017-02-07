﻿namespace TraktApiSharp.Requests.Interfaces.Base
{
    using System.Collections.Generic;

    internal interface ITraktHasUriPathParameters
    {
        IDictionary<string, object> GetUriPathParameters();
    }
}
