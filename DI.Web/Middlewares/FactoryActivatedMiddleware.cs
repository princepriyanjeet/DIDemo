using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Middlewares
{
    public class FactoryActivatedMiddleware : IMiddleware
    {
        private readonly IGuidService guidService;
        private readonly ILogger<FactoryActivatedMiddleware> logger;

        public FactoryActivatedMiddleware(IGuidService guidService,ILogger<FactoryActivatedMiddleware> logger)
        {
            this.guidService = guidService;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var logMessage = $"FactoryActivatedMiddleware:The guid from GuidService is {guidService.GetGuid()}";
            this.logger.LogInformation(logMessage);
            context.Items.Add("FactoryActivatedMiddleware", logMessage);
            await next(context);
        }
    }
}
