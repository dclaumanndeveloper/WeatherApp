using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Services.Location
{
    public class LocationService : ILocationService
    {
        private CancellationTokenSource _cts;

        public async Task<Xamarin.Essentials.Location> GetCurrentLocationCoordinates()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                _cts = new CancellationTokenSource();
                Xamarin.Essentials.Location location = await Geolocation.GetLocationAsync(request, _cts.Token);

                return location;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Debug.WriteLine(fnsEx);
                return null;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Debug.WriteLine(fneEx);
                return null;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Debug.WriteLine(pEx);
                return null;
            }
            catch (Exception ex)
            {
                // Unable to get location
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<Placemark> GetCurrentLocationName(double lat, double lon)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks.FirstOrDefault();
                if (placemark != null)
                {
                    return placemark;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public async Task<Xamarin.Essentials.Location> GetLocation(string locationName)
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(locationName);

                var location = locations.FirstOrDefault();
                if (location != null)
                {
                    return location;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        Task ILocationService.GetCurrentLocationCoordinates()
        {
            throw new NotImplementedException();
        }

        Task ILocationService.GetCurrentLocationName(double lat, double lon)
        {
            throw new NotImplementedException();
        }

        Task ILocationService.GetLocation(string locationName)
        {
            throw new NotImplementedException();
        }
    }
}
