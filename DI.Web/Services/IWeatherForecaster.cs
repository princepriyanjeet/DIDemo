using DI.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Services
{
    public interface IWeatherForecaster
    {
        Task<CurrentWeatherResult> GetCurrentWeatherAsync();
    }
}
