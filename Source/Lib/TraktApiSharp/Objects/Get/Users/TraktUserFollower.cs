﻿namespace TraktApiSharp.Objects.Get.Users
{
    using Newtonsoft.Json;
    using System;

    /// <summary>A Trakt user follower.</summary>
    public class TraktUserFollower : ITraktUserFollower
    {
        /// <summary>Gets or sets the UTC datetime, when the relationship began.</summary>
        [JsonProperty(PropertyName = "followed_at")]
        public DateTime? FollowedAt { get; set; }

        /// <summary>
        /// Gets or sets the following / followed Trakt user.
        /// See also <seealso cref="TraktUser" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public ITraktUser User { get; set; }

        [JsonIgnore]
        public string Username
        {
            get { return User?.Username; }

            set
            {
                if (User != null)
                    User.Username = value;
            }
        }

        [JsonIgnore]
        public bool? IsPrivate
        {
            get { return User?.IsPrivate; }

            set
            {
                if (User != null)
                    User.IsPrivate = value;
            }
        }

        [JsonIgnore]
        public TraktUserIds Ids
        {
            get { return User?.Ids; }

            set
            {
                if (User != null)
                    User.Ids = value;
            }
        }

        [JsonIgnore]
        public string Name
        {
            get { return User?.Name; }

            set
            {
                if (User != null)
                    User.Name = value;
            }
        }

        [JsonIgnore]
        public bool? IsVIP
        {
            get { return User?.IsVIP; }

            set
            {
                if (User != null)
                    User.IsVIP = value;
            }
        }

        [JsonIgnore]
        public bool? IsVIP_EP
        {
            get { return User?.IsVIP_EP; }

            set
            {
                if (User != null)
                    User.IsVIP_EP = value;
            }
        }

        [JsonIgnore]
        public DateTime? JoinedAt
        {
            get { return User?.JoinedAt; }

            set
            {
                if (User != null)
                    User.JoinedAt = value;
            }
        }

        [JsonIgnore]
        public string Location
        {
            get { return User?.Location; }

            set
            {
                if (User != null)
                    User.Location = value;
            }
        }

        [JsonIgnore]
        public string About
        {
            get { return User?.About; }

            set
            {
                if (User != null)
                    User.About = value;
            }
        }

        [JsonIgnore]
        public string Gender
        {
            get { return User?.Gender; }

            set
            {
                if (User != null)
                    User.Gender = value;
            }
        }

        [JsonIgnore]
        public int? Age
        {
            get { return User?.Age; }

            set
            {
                if (User != null)
                    User.Age = value;
            }
        }

        [JsonIgnore]
        public TraktUserImages Images
        {
            get { return User?.Images; }

            set
            {
                if (User != null)
                    User.Images = value;
            }
        }
    }
}
