﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap01.Middlewares
{
    public class LogMiddleware
    {
        private RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableRewind();

            var request = await FormatRequest(context.Request);
            var log = new LoggerConfiguration()
            .WriteTo.Logentries("93044087-b400-478c-924d-cb8966e72372")
            //.WriteTo.Logentries("9b4c08d3-cd6d-4612-a0b5-68912c04c465")
            .CreateLogger();
            log.Information($"request {request}");

            context.Request.Body.Position = 0;

            await _next(context);

            #region Exemplo

            //TODO: Programar isso
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            //await _next.Invoke(context);
            //stopwatch.Stop();
            //Console.WriteLine($"A requisição demorou {stopwatch.Elapsed.ToString()}");
            #endregion
        }
        private static async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            var messageObjToLog = new { scheme = request.Scheme, host = request.Host, path = request.Path, queryString = request.Query, requestBody = bodyAsText };

            return JsonConvert.SerializeObject(messageObjToLog);
        }
    }


    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMeuLogPreza(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<LogMiddleware>();
        }
    }
}
