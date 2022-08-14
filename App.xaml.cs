using WeatherApp.Core;
using WeatherApp.Services.LocalSettings;
using WeatherApp.Services.Location;
using WeatherApp.Services.Weather;
using WeatherApp.ViewModels.Dialogs;
using WeatherApp.ViewModels;
using WeatherApp.Views.Dialogs;
using WeatherApp.Views;

namespace WeatherApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
    public App(IPlatformInitializer initializer) : base(initializer) { }

    public new static App Current => Application.Current as App;

    protected override async void OnInitialized()
    {
        InitializeComponent();
        SetAppTheme();

        var locations = await SecureStorage.GetAsync("locations");
        if (locations != null)
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WeatherPage)}");
        }
        else
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WelcomePage)}");
        }
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterInstance(new HttpClientFactory());
        containerRegistry.Register<ILocationService, LocationService>();
        containerRegistry.Register<ILocalSettingsService, LocalSettingsService>();
        containerRegistry.Register<IWeatherService, WeatherService>();

        containerRegistry.RegisterForNavigation<NavigationPage>("NavigationPage");
        containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>("WelcomePage");
        containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>("WeatherPage");
        containerRegistry.RegisterForNavigation<YourLocationsPage, YourLocationsPageViewModel>("YourLocationsPage");
        containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>("SettingsPage");

        containerRegistry.RegisterDialog<AddLocationDialog, AddLocationDialogViewModel>();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }

    private void SetAppTheme()
    {
        var theme = Preferences.Get("theme", string.Empty);
        if (string.IsNullOrEmpty(theme) || theme == "light")
        {
            Application.Current.UserAppTheme = OSAppTheme.Light;
        }
        else
        {
            Application.Current.UserAppTheme = OSAppTheme.Dark;
        }
    }
}
