using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.Infrastructure.AppDbContext;

namespace Phonebook.Infrastructure
{
    public static class InfrastructureServicesRegisteration
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services , IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });
            //return services;
        }
    }
}
