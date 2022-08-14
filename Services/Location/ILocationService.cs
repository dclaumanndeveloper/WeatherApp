using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;

namespace WeatherApp.Services.Location
{
    public interface ILocationService
    {
        Task GetCurrentLocationCoordinates();
        Task GetCurrentLocationName(double lat, double lon);
        Task GetLocation(string locationName);

    }
}
