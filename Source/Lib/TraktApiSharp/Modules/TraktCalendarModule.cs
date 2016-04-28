﻿namespace TraktApiSharp.Modules
{
    using Objects.Basic;
    using Objects.Get.Calendars;
    using Requests;
    using Requests.WithOAuth.Calendars;
    using Requests.WithoutOAuth.Calendars;
    using System;
    using System.Threading.Tasks;

    public class TraktCalendarModule : TraktBaseModule
    {
        public TraktCalendarModule(TraktClient client) : base(client) { }

        public async Task<TraktListResult<TraktCalendarShowItem>> GetUserShowsAsync(DateTime? startDate = null, int? days = null,
                                                                                    TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktCalendarUserShowsRequest(Client)
            {
                StartDate = startDate,
                Days = days,
                ExtendedOption = extended
            });
        }

        public async Task<TraktListResult<TraktCalendarShowItem>> GetUserNewShowsAsync(DateTime? startDate = null, int? days = null,
                                                                                       TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktCalendarUserNewShowsRequest(Client)
            {
                StartDate = startDate,
                Days = days,
                ExtendedOption = extended
            });
        }

        public async Task<TraktListResult<TraktCalendarShowItem>> GetUserSeasonPremieresAsync(DateTime? startDate = null, int? days = null,
                                                                                              TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktCalendarUserSeasonPremieresRequest(Client)
            {
                StartDate = startDate,
                Days = days,
                ExtendedOption = extended
            });
        }

        public async Task<TraktListResult<TraktCalendarMovieItem>> GetUserMoviesAsync(DateTime? startDate = null, int? days = null,
                                                                                      TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktCalendarUserMoviesRequest(Client)
            {
                StartDate = startDate,
                Days = days,
                ExtendedOption = extended
            });
        }

        public async Task<TraktListResult<TraktCalendarShowItem>> GetAllShowsAsync(DateTime? startDate = null, int? days = null,
                                                                                   TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktCalendarAllShowsRequest(Client)
            {
                StartDate = startDate,
                Days = days,
                ExtendedOption = extended
            });
        }

        public async Task<TraktListResult<TraktCalendarShowItem>> GetAllNewShowsAsync(DateTime? startDate = null, int? days = null,
                                                                                      TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktCalendarAllNewShowsRequest(Client)
            {
                StartDate = startDate,
                Days = days,
                ExtendedOption = extended
            });
        }

        public async Task<TraktListResult<TraktCalendarShowItem>> GetAllSeasonPremieresAsync(DateTime? startDate = null, int? days = null,
                                                                                             TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktCalendarAllSeasonPremieresRequest(Client)
            {
                StartDate = startDate,
                Days = days,
                ExtendedOption = extended
            });
        }

        public async Task<TraktListResult<TraktCalendarMovieItem>> GetAllMoviesAsync(DateTime? startDate = null, int? days = null,
                                                                                     TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktCalendarAllMoviesRequest(Client)
            {
                StartDate = startDate,
                Days = days,
                ExtendedOption = extended
            });
        }
    }
}
