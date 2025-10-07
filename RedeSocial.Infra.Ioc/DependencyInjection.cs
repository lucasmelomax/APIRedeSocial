using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Application.Interfaces;
using RedeSocial.Application.Mappings;
using RedeSocial.Application.Services;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Infra.Data.Context;
using RedeSocial.Infra.Data.Repositories;

namespace RedeSocial.Infra.Ioc {
    public static class DependencyInjection {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration) 
        {
            services.AddDbContext<RedeSocialContext>(options => {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                    b => b.MigrationsAssembly(typeof(RedeSocialContext).Assembly.FullName));
                    
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(DTOMappingProfile));

            return services;
        } 
    }
}
