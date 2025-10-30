using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RedeSocial.Application.Interfaces;
using RedeSocial.Application.Mappings;
using RedeSocial.Application.Services;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Infra.Data.Context;
using RedeSocial.Infra.Data.Identity;
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

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthentication, AuthenticateService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<IPhostPhotosService, PhostPhotosService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<ILikesService, LikesService>();
            services.AddScoped<IFollowersService, FollowersService>();
            services.AddAutoMapper(typeof(DTOMappingProfile));

            return services;
        } 
    }
}
