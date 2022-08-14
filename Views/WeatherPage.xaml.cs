using Xamarin.CommunityToolkit;
namespace WeatherApp.Views;

public partial class WeatherPage : ContentPage, IDestructible
{
    private MenuEvent _menuEvent;

    public WeatherPage(IEventAggregator eventAggregator)
    {
        InitializeComponent();

        _menuEvent = eventAggregator.GetEvent<MenuEvent>();
        _menuEvent.Subscribe(OpenCloseMenu);
    }

    public void Destroy()
    {
        _menuEvent.Unsubscribe(OpenCloseMenu);
    }

    private void OpenCloseMenu()
    { 
        if (menuView.State.Equals(SideMenuState.LeftMenuShown))
        {
            menuView.State = SideMenuState.MainViewShown;
        }
        else
        {
            menuView.State = SideMenuState.LeftMenuShown;
        }
    }
}