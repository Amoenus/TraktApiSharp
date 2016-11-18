﻿namespace TraktApiSharp.Experimental.Requests.Shows
{
    using Objects.Get.Shows;

    internal sealed class TraktShowsPopularRequest : ATraktShowsRequest<TraktShow>
    {
        public TraktShowsPopularRequest(TraktClient client) : base(client) { }

        public override string UriTemplate => "shows/popular{?extended,page,limit,query,years,genres,languages,countries,runtimes,ratings,certifications,networks,status}";
    }
}