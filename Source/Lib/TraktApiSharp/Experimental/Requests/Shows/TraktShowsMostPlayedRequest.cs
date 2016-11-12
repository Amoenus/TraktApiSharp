﻿namespace TraktApiSharp.Experimental.Requests.Shows
{
    using Objects.Get.Shows.Common;
    using System;
    using TraktApiSharp.Requests;

    internal sealed class TraktShowsMostPlayedRequest : ATraktShowsMostPWCRequest<TraktMostPlayedShow>
    {
        public TraktShowsMostPlayedRequest(TraktClient client) : base(client) { }

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
