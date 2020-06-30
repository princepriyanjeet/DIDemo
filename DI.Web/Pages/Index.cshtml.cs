using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Web.Data;
using DI.Web.Domain.Rule;
using DI.Web.Middlewares;
using DI.Web.Services;
using DI.Web.Services.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DI.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWeatherForecaster weatherForecasterService;
        private readonly IGuidService guidService;
        private readonly IBookingRuleProcessor bookingRuleProcessor;
        private readonly INotificationService notificationService;

        public IndexModel(ILogger<IndexModel> logger,
                          IWeatherForecaster weatherForecasterService, 
                          IGuidService guidService,
                          IBookingRuleProcessor bookingRuleProcessor,
                          INotificationService notificationService)
        {
            _logger = logger;
            this.weatherForecasterService = weatherForecasterService;
            this.guidService = guidService;
            this.bookingRuleProcessor = bookingRuleProcessor;
            this.notificationService = notificationService;
        }

        public async Task OnGetAsync()
        {
            List<string> obj = new List<string>();
            var guid = this.guidService.GetGuid();
            var logMessage = $"IndexModel:The guid from GuidService is {guid}";
            this._logger.LogInformation(logMessage);
            var result = await this.weatherForecasterService.GetCurrentWeatherAsync();
            this._logger.LogInformation(result.Description);
            CourtBooking courtBooking = new CourtBooking() { StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddHours(5) };
            var ruleOutComes = await bookingRuleProcessor.PassesAllRulesAsync(courtBooking);
            await this.notificationService.SendAsync("Booking succeeded", guid);
        }
    }
}
