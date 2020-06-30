using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Middlewares
{
    public class CustomeMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomeMiddleware(RequestDelegate next,ILogger<CustomeMiddleware> logger)
        {
            this._next = next;
            Logger = logger;
        }

        public ILogger<CustomeMiddleware> Logger { get; }

        public async Task InvokeAsync(HttpContext context,IGuidService guidService)
        {
            var logMessage = $"Middleware:The guid from GuidService is {guidService.GetGuid()}";
            this.Logger.LogInformation(logMessage);
            context.Items.Add("MiddlewareGuid",logMessage);
            await _next(context);
        }
    }

   
}
