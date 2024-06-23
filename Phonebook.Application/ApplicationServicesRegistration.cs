using Jose;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.Application.Identity.CheckToken;
using System.Reflection;


namespace Phonebook.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}
