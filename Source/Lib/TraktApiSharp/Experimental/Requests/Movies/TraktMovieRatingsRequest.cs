﻿namespace TraktApiSharp.Experimental.Requests.Movies
{
    using Base.Get;
    using Objects.Basic;
    using System;
    using TraktApiSharp.Requests;

    internal sealed class TraktMovieRatingsRequest : ATraktSingleItemGetByIdRequest<TraktRating>
    {
        internal TraktMovieRatingsRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override string UriTemplate
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
