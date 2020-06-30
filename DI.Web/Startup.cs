using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DI.Web.Domain.Rule;
using DI.Web.Middlewares;
using DI.Web.Services;
using DI.Web.Services.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace DI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddAppConfiguration(Configuration).AddRules();
            //step1
            //using service descriptor
            //var svDsc1 = new ServiceDescriptor(typeof(IWeatherForecaster), typeof(WeatherForecaster), ServiceLifetime.Singleton);
            //var svDsc2 = ServiceDescriptor.Describe(typeof(IWeatherForecaster), typeof(WeatherForecaster), ServiceLifetime.Singleton);
            //var svDsc3 = ServiceDescriptor.Singleton(typeof(IWeatherForecaster), typeof(WeatherForecaster));
            //var svDsc4 = ServiceDescriptor.Singleton<IWeatherForecaster, WeatherForecaster>();
            //services.Add(svDsc4);
            services.AddSingleton<IWeatherForecaster, WeatherForecaster>();
            //services.AddSingleton<IWeatherForecaster, AmazingWeatherForecaster>();
            //services.TryAddSingleton<IWeatherForecaster, AmazingWeatherForecaster>();
            services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, AmazingWeatherForecaster>());
            //services.AddTransient<IGuidService, GuidService>();
            //services.AddSingleton<IGuidService, GuidService>();
            //services.AddSingleton<ICourtBookingRule, ClubIsOpen>();
            //services.AddSingleton<ICourtBookingRule, MaxBookingLengthRule>();
            
            services.AddScoped<IGuidService, GuidService>();
            //composite pattern
            //Sms and email has to b registered against there own type and not against INotification
            services.AddSingleton<EmailNotificationService>();
            services.AddSingleton<SmsNotificationService>();
            services.AddSingleton<INotificationService>(sp => new CompositeNotificationService(new INotificationService[] { 
                sp.GetRequiredService<EmailNotificationService>(),sp.GetRequiredService<SmsNotificationService>()
            }));
            //services.AddScoped<FactoryActivatedMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<CustomeMiddleware>();
            //app.UseMiddleware<FactoryActivatedMiddleware>();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterType<GuidService>().As<IGuidService>().InstancePerLifetimeScope();

        }
    }
}
