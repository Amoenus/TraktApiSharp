﻿namespace TraktApiSharp.Requests.WithOAuth.Calendars
{
    using Objects.Get.Calendars;

    internal class TraktCalendarUserMoviesRequest : TraktCalendarUserRequest<TraktCalendarMovieItem>
    {
        internal TraktCalendarUserMoviesRequest(TraktClient client) : base(client) { }

        protected override string UriTemplate => "calendars/my/movies/{start_date}/{days}";
    }
}
