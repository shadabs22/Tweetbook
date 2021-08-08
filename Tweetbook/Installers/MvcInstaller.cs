using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using Tweetbook.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Tweetbook.Authorizations.abcdDomainAuthorization;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.AspNetCore;
using Tweetbook.Filters;

namespace Tweetbook.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtsettings = new JwtSettings();
            configuration.Bind(nameof(jwtsettings), jwtsettings);
            services.AddSingleton(jwtsettings);
            services.AddMvc(options =>
            {
                //options.EnableEndpointRouting = false;
                //options.Filters.Add<ValidationFilter>();
                options.Filters.Add(typeof(ValidationFilter));
            })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.DisableDataAnnotationsValidation = true;
                    //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;  
                    //fv.ImplicitlyValidateChildProperties = true;  
                });

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsettings.Secrete)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = tokenValidationParameters;
            });

            //services.AddAuthorization();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TagViewer", builder => builder.RequireClaim("tags.view", "true"));
                options.AddPolicy("MustWorkForABCD.COM", policy =>
                {
                    policy.AddRequirements(new WorksForCompanyRequirement("abcd.com"));
                });
            });

            services.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Tweetbook API", Version = "v1" });

                x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                 }
                });
            });
        }
    }
}
