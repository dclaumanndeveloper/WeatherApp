using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services.Weather
{
    public interface IWeatherService
    {
        Task<CurrentWeatherModel> GetCurrentWeatherAndHourlyForecastByLatLon(double lat, double lon, string units);
    }
}
