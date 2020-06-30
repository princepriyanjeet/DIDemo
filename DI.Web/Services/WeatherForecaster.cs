using DI.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Services
{
    public class WeatherForecaster : IWeatherForecaster
    {
        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var result = new CurrentWeatherResult
            {
                Description = "Nice sunny weather"
            };

            return await Task.FromResult(result);
        }
    }

    public class AmazingWeatherForecaster : IWeatherForecaster
    {
        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var result = new CurrentWeatherResult
            {
                Description = "AmazingWeatherForecaster Nice sunny weather"
            };

            return await Task.FromResult(result);
        }
    }
}
