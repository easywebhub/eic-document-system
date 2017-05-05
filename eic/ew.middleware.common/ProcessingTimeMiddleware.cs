using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace ew.middleware.common
{
    public class ProcessingTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ProcessingTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting((state) =>
            {
                context.Response.Headers.Add("X-Processing-Time-Milliseconds", new[] { watch.ElapsedMilliseconds.ToString() });
                //if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                //{
                //    if (context.Request.Path.Value.EndsWith(".map"))
                //    {
                //        context.Response.ContentType = "application/json";
                //    }
                //}
                return Task.FromResult(0);
            }, null);

            await _next(context);
        }
    }
}
