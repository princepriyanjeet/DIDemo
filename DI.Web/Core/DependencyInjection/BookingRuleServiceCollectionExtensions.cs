using DI.Web.Domain.Rule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BookingRuleServiceCollectionExtensions
    {
        public static IServiceCollection AddRules(this IServiceCollection services)
        {
            //naive way
            //services.TryAddEnumerable(new[] {
            //    ServiceDescriptor.Singleton<ICourtBookingRule,ClubIsOpen>(),
            //    ServiceDescriptor.Singleton<ICourtBookingRule,MaxBookingLengthRule>()
            //}); ;
            //services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();

            //scrutor way
            services.Scan(scan => scan
            .FromAssemblyOf<ICourtBookingRule>()
            .AddClasses(c => c.AssignableTo<IScopedCourtBookingRule>())
            .As<ICourtBookingRule>()
            .WithScopedLifetime()
            .FromAssemblyOf<ICourtBookingRule>()
            .AddClasses(c => c.AssignableTo<ISingletonCourtBookingRule>())
            .As<ICourtBookingRule>()
            .WithSingletonLifetime());

            services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();
            return services;
        }
    }
}
