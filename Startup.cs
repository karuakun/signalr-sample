using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NSwag.AspNetCore;
using signalsample.Configs;
using signalsample.Hubs;

namespace signalsample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // SignalR
            services.AddSignalR()
                .AddMessagePackProtocol()
                ;

            // CORS
            services.AddCors(o => o.AddPolicy("AllowSpaClient",
                builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(
                        "http://localhost:4200",
                        "http://localhost"
                    )
                ));

            var secion = Configuration.GetSection(JwtConfig.ConfigName);
            services.Configure<JwtConfig>(secion);
            var jwtConfig = secion.Get<JwtConfig>();
            if (jwtConfig == null)
            {
                throw new InvalidOperationException("jwt settings is not completed.");
            }


            // �F�؁A�F��
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = jwtConfig.Authority;
                options.Audience = jwtConfig.Audience;
                options.IncludeErrorDetails = true;
                options.SaveToken = true;

                // Token�̌��ؕ��@���w�肷��
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler
                {
                    InboundClaimTypeMap = new Dictionary<string, string>()
                });
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtConfig.ValidIssuer,
                    ValidAudience = jwtConfig.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.IssuerSigningKey)),    // SecretKey
                    NameClaimType = "name",
                    RoleClaimType = "role",
                }; 

                // QueryString����id token�����o���Č��؂����{����
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Path.Value.StartsWith("/hubs") &&
                            context.Request.Query.TryGetValue("idToken", out StringValues token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var te = context.Exception;
                        Console.WriteLine(te);
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSwaggerDocument(d =>
            {
                d.SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                d.DocumentName = "v1";
                d.ApiGroupNames = new[] {"1"};
            });

            services
                .AddMvc(options =>
                {
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // SignalR
            app.UseSignalR(routes =>
            {
                routes.MapHub<EchoHub>("/hubs/echo");
                routes.MapHub<AuthHub>("/hubs/auth-echo");
                routes.MapHub<ProcessHub>("/hubs/process");
            });

            // CORS
            app.UseCors("AllowSpaClient");

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUi3();
            //app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, settings =>
            //{
            //    settings.GeneratorSettings.DefaultPropertyNameHandling =
            //        PropertyNameHandling.CamelCase;
            //});

            // �F��
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
