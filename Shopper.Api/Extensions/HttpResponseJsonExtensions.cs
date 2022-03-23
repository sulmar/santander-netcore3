using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shopper.Api.Extensions
{
    public static class HttpResponseJsonExtensions
    {
        public static async Task WriteAsJsonAsync<T>(this HttpResponse response, T model)
        {
            string json = JsonSerializer.Serialize(model);
            response.ContentType = "application/json";
            await response.WriteAsync(json);
        }
    }
}
