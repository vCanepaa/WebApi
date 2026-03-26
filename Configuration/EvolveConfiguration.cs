using EvolveDb;
using Microsoft.Data.SqlClient;
using Serilog;

namespace WebApi.Configuration
{
    public static class EvolveConfiguration
    {

        public static IServiceCollection AddEvolveConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment()) {
                var connectionString = configuration["SqlConnectionStrings:ConnectionStringDev"];
                if (string.IsNullOrEmpty(connectionString))
                    throw new ArgumentNullException("Connection string is null");


                try
                {
                    using var evolveConnection = new SqlConnection(connectionString);

                    var evolve = new Evolve(evolveConnection, Log.Information)
                    {
                        Locations = new List<string> { "DataBase/migrations", "DataBase/dataset" },
                        IsEraseDisabled = true


                    };
                    evolve.Migrate();
                }
                catch(Exception ex) 
                {
                    Log.Error(ex, "An error occurred while migrating the database");
                }

            }

            return services;
        }
    }
}
