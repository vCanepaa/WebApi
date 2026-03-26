using Serilog;

namespace WebApi.Configuration
{
    public static class LoggingConfig
    {
        public static void AddSeriLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext().WriteTo.Console().WriteTo.Debug().CreateLogger();
            builder.Host.UseSerilog();

        }

    }
}
