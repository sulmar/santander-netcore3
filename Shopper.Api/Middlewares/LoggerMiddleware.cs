using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.Api.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;

        public LoggerMiddleware(RequestDelegate next) // <- obowiązkowo!
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<LoggerMiddleware> logger)
        {
            logger.LogInformation("{0} {1}", context.Request.Method, context.Request.Path);

            await next(context);

            logger.LogInformation("StatusCode: {0}", context.Response.StatusCode);
        }
    }
}
