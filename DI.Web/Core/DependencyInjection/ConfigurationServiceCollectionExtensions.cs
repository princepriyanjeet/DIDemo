using DI.Web.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services,IConfiguration config)
        {
            services.Configure<ExternalServicesConfig>(config.GetSection("ExternalServices"));
            services.Configure<ClubConfiguration>(config.GetSection("ClubSettings"));
            services.Configure<BookingConfiguration>(config.GetSection("CourtBookings"));
            services.Configure<FeaturesConfiguration>(config.GetSection("Features"));
            services.Configure<MembershipConfiguration>(config.GetSection("Membership"));

            services.TryAddSingleton<IBookingConfiguration>(sp => sp.GetRequiredService<IOptions<BookingConfiguration>>().Value);//using implementatin factory
            services.TryAddSingleton<IClubConfiguration>(sp => sp.GetRequiredService<IOptions<ClubConfiguration>>().Value);//using implemenation factory

            //services.AddSingleton<IBookingConfiguration>(sp => sp.GetRequiredService<IOptions<BookingConfiguration>>().Value);
            return services;
        }
    }
}
