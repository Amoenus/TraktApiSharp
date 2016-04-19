﻿namespace TraktApiSharp.Enums
{
    using System;

    public enum TraktPeriod
    {
        Unspecified,
        Weekly,
        Monthly,
        Yearly,
        All
    }

    public static class TraktPeriodExtensions
    {
        public static string AsString(this TraktPeriod period)
        {
            switch (period)
            {
                case TraktPeriod.Weekly: return "weekly";
                case TraktPeriod.Monthly: return "monthly";
                case TraktPeriod.Yearly: return "yearly";
                case TraktPeriod.All: return "all";
                case TraktPeriod.Unspecified: return string.Empty;
                default:
                    throw new ArgumentOutOfRangeException("Period");
            }
        }
    }
}
