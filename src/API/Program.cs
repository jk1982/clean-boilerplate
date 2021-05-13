using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(GetLogEventLevel())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("c:/logs/teste.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");

                CreateHostBuilder(args).Build().Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                Console.ReadKey();

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static LogEventLevel GetLogEventLevel()
        {
            LogEventLevel defaultLevel = LogEventLevel.Information;
            var logEventLevel = Environment.GetEnvironmentVariable("LOG_LEVEL");

            if (!string.IsNullOrEmpty(logEventLevel))
            {
                LogEventLevel level;
                if (!Enum.TryParse(logEventLevel, out level))
                {
                    Trace.TraceWarning("Error parsing Serilog.LogEventLevel. Defaulting to {0}", defaultLevel);
                    level = defaultLevel;
                }

                return level;
            }

            return defaultLevel;
        }
    }
}
