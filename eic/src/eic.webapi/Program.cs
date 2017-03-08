using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Couchbase.Configuration.Client;

namespace eic.webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new LoggerFactory();
            factory.AddDebug();
            factory.AddNLog();
            factory.ConfigureNLog("nlog.config");

            //configure logging on the couchbase client
            var config = new ClientConfiguration
            {
                LoggerFactory = factory
            };
            
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
