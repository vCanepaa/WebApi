using Microsoft.EntityFrameworkCore;
using WebApi.DataBase;

namespace WebApi.Configuration
{
    public static class DataBaseConfiguration
    {
        public static IServiceCollection AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration["SqlConnectionStrings:ConnectionStringDev"];
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Connection string is null");

            services.AddDbContext<SqlContext>(opt => opt.UseSqlServer(connectionString));

            return services;

        }

    }
}
