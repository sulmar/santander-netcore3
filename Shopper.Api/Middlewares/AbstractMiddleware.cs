using Microsoft.AspNetCore.Http;

namespace Shopper.Api.Middlewares
{
    public abstract class AbstractMiddleware
    {
        protected readonly RequestDelegate next;

        public AbstractMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
    }
}
