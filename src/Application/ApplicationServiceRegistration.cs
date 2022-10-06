using Application.Features.Rules;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.Rules;
using Application.Features.Technologies.Rules;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Core.Security.JWT;
using Application.Services.Repositories;
using Core.Security.Entities;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ProgrammingLanguageBusinessRules>();
            services.AddScoped<TechnologyBusinessRules>();
            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<UserBusinessRules>();

            services.AddTransient<IAuthService, AuthManager>();
            services.AddTransient<ITokenHelper, JwtHelper>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}