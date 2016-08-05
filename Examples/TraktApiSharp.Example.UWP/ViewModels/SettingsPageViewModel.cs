namespace TraktApiSharp.Example.UWP.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Template10.Mvvm;
    using Windows.UI.Xaml;

    public class SettingsPageViewModel : ViewModelBase
    {
        public TraktClientPartViewModel TraktClientPartViewModel { get; } = new TraktClientPartViewModel();
        public SettingsPartViewModel SettingsPartViewModel { get; } = new SettingsPartViewModel();
        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();
    }

    public class TraktClientPartViewModel : ViewModelBase
    {
        Services.SettingsServices.SettingsService _settings;

        public TraktClientPartViewModel()
        {
            _settings = Services.SettingsServices.SettingsService.Instance;
        }

        public string ClientId
        {
            get { return _settings.TraktClientId; }

            set
            {
                _settings.TraktClientId = value;
                base.RaisePropertyChanged();
            }
        }

        public string ClientSecret
        {
            get { return _settings.TraktClientSecret; }

            set
            {
                _settings.TraktClientSecret = value;
                base.RaisePropertyChanged();
            }
        }

        public string ClientAccessToken
        {
            get { return _settings.TraktClientAccessToken; }

            set
            {
                _settings.TraktClientAccessToken = value;
                base.RaisePropertyChanged();
            }
        }

        public bool UseStagingUrl
        {
            get { return _settings.TraktUseStagingUrl; }

            set
            {
                _settings.TraktUseStagingUrl = value;
                base.RaisePropertyChanged();
            }
        }

        public bool ForceAuthorization
        {
            get { return _settings.TraktForceAuthorization; }

            set
            {
                _settings.TraktForceAuthorization = value;
                base.RaisePropertyChanged();
            }
        }
    }

    public class SettingsPartViewModel : ViewModelBase
    {
        Services.SettingsServices.SettingsService _settings;

        public SettingsPartViewModel()
        {
            _settings = Services.SettingsServices.SettingsService.Instance;
        }

        public bool UseShellBackButton
        {
            get { return _settings.UseShellBackButton; }
            set { _settings.UseShellBackButton = value; base.RaisePropertyChanged(); }
        }

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set { _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; base.RaisePropertyChanged(); }
        }

        private string _BusyText = "Please wait...";
        public string BusyText
        {
            get { return _BusyText; }
            set
            {
                Set(ref _BusyText, value);
                _ShowBusyCommand.RaiseCanExecuteChanged();
            }
        }

        DelegateCommand _ShowBusyCommand;
        public DelegateCommand ShowBusyCommand
            => _ShowBusyCommand ?? (_ShowBusyCommand = new DelegateCommand(async () =>
            {
                Views.Busy.SetBusy(true, _BusyText);
                await Task.Delay(5000);
                Views.Busy.SetBusy(false);
            }, () => !string.IsNullOrEmpty(BusyText)));
    }

    public class AboutPartViewModel : ViewModelBase
    {
        public string Logo => Windows.ApplicationModel.Package.Current.Logo.AbsolutePath;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

        public Uri RateMe => new Uri("http://aka.ms/template10");
    }
}
