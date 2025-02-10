using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Million.Domain.Abstractions;
using Million.Domain.Owners;
using Million.Domain.Properties;
using Million.Domain.Users;
using Million.Infrastructure.Authentication;
using Million.Infrastructure.Repositories;
using Million.Repository.Database;

namespace Million.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MillionContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MillionContext>());

            

            return services;
        }

    }
}
