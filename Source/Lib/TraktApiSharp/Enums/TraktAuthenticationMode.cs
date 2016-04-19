﻿namespace TraktApiSharp.Enums
{
    using Extensions;
    using Newtonsoft.Json;
    using System;

    public enum TraktAuthenticationMode
    {
        Unspecified,
        Device,
        OAuth
    }

    public static class TraktAuthenticationModeExtensions
    {
        public static string AsString(this TraktAuthenticationMode authenticationMode)
        {
            switch (authenticationMode)
            {
                case TraktAuthenticationMode.Device: return "Device";
                case TraktAuthenticationMode.OAuth: return "OAuth";
                case TraktAuthenticationMode.Unspecified: return string.Empty;
                default:
                    throw new ArgumentOutOfRangeException("AuthenticationMode");
            }
        }
    }

    public class TraktAuthenticationModeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = reader.Value as string;
            enumString = enumString.FirstToUpper();
            return Enum.Parse(typeof(TraktAuthenticationMode), enumString, true);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var authenticationMode = (TraktAuthenticationMode)value;
            writer.WriteValue(authenticationMode.AsString());
        }
    }
}
