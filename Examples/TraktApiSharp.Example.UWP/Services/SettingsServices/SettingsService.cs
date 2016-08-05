using System;
using Template10.Common;
using Template10.Utils;
using TraktApiSharp.Example.UWP.Services.TraktService;
using Windows.UI.Xaml;

namespace TraktApiSharp.Example.UWP.Services.SettingsServices
{
    public class SettingsService
    {
        public static SettingsService Instance { get; } = new SettingsService();

        private Template10.Services.SettingsService.ISettingsHelper _helper;

        private SettingsService()
        {
            _helper = new Template10.Services.SettingsService.SettingsHelper();
        }

        public bool UseShellBackButton
        {
            get { return _helper.Read<bool>(nameof(UseShellBackButton), true); }

            set
            {
                _helper.Write(nameof(UseShellBackButton), value);

                BootStrapper.Current.NavigationService.Dispatcher.Dispatch(() =>
                {
                    BootStrapper.Current.ShowShellBackButton = value;
                    BootStrapper.Current.UpdateShellBackButton();
                    BootStrapper.Current.NavigationService.Refresh();
                });
            }
        }

        public static string DEFAULT_CLIENT_ID_VALUE { get; } = "Enter your Trakt Client Id here";

        public static string DEFAULT_CLIENT_SECRET_VALUE { get; } = "Enter your Trakt Client Secret here";

        public static string DEFAULT_CLIENT_ACCESS_TOKEN_VALUE { get; } = "Enter your Trakt Client Access Token here";

        public string TraktClientId
        {
            get { return _helper.Read<string>(nameof(TraktClientId), DEFAULT_CLIENT_ID_VALUE); }

            set
            {
                _helper.Write(nameof(TraktClientId), value);
                TraktServiceProvider.Instance.Client.ClientId = value;
            }
        }

        public string TraktClientSecret
        {
            get { return _helper.Read<string>(nameof(TraktClientSecret), DEFAULT_CLIENT_ID_VALUE); }

            set
            {
                _helper.Write(nameof(TraktClientSecret), value);
                TraktServiceProvider.Instance.Client.ClientSecret = value;
            }
        }

        public string TraktClientAccessToken
        {
            get { return _helper.Read<string>(nameof(TraktClientAccessToken), DEFAULT_CLIENT_ID_VALUE); }

            set
            {
                _helper.Write(nameof(TraktClientAccessToken), value);
                TraktServiceProvider.Instance.Client.AccessToken = value;
            }
        }

        public bool TraktUseStagingUrl
        {
            get { return _helper.Read<bool>(nameof(TraktUseStagingUrl), false); }

            set
            {
                _helper.Write(nameof(TraktUseStagingUrl), value);
                TraktServiceProvider.Instance.Client.Configuration.UseStagingUrl = value;
            }
        }

        public bool TraktForceAuthorization
        {
            get { return _helper.Read<bool>(nameof(TraktForceAuthorization), false); }

            set
            {
                _helper.Write(nameof(TraktForceAuthorization), value);
                TraktServiceProvider.Instance.Client.Configuration.ForceAuthorization = value;
            }
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                var theme = ApplicationTheme.Light;
                var value = _helper.Read<string>(nameof(AppTheme), theme.ToString());

                return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
            }

            set
            {
                _helper.Write(nameof(AppTheme), value.ToString());
                (Window.Current.Content as FrameworkElement).RequestedTheme = value.ToElementTheme();
                Views.Shell.HamburgerMenu.RefreshStyles(value);
            }
        }

        public TimeSpan CacheMaxDuration
        {
            get { return _helper.Read<TimeSpan>(nameof(CacheMaxDuration), TimeSpan.FromDays(2)); }

            set
            {
                _helper.Write(nameof(CacheMaxDuration), value);
                BootStrapper.Current.CacheMaxDuration = value;
            }
        }
    }
}
