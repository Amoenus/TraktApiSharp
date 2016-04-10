﻿namespace TraktApiSharp.Requests.Shows
{
    using Base.Get;
    using Objects;
    using Objects.Shows;

    internal class TraktShowTranslationsRequest : TraktGetByIdRequest<TraktListResult<TraktShowTranslation>, TraktShowTranslation>
    {
        internal TraktShowTranslationsRequest(TraktClient client) : base(client) { }

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.NotRequired;

        protected override string UriTemplate => "shows/{id}/translations";

        protected override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Shows;
    }
}
