﻿namespace TraktApiSharp.Experimental.Requests.Shows
{
    using Objects.Get.Shows.Common;

    internal sealed class TraktShowsMostCollectedRequest : ATraktShowsMostPWCRequest<TraktMostCollectedShow>
    {
        internal TraktShowsMostCollectedRequest(TraktClient client) : base(client) { }

        public string UriTemplate => "shows/collected{/period}{?extended,page,limit,query,years,genres,languages,countries,runtimes,ratings,certifications,networks,status}";
    }
}
