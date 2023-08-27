using AutoMapper;
using ApiRest.Extensions;
using Business.BO;
using Business.Interfaces;
using Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TokenHandler.Export;
using Microsoft.OpenApi.Models;

namespace ApiRest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Automapper

            services.AddAutoMapper(typeof(Startup));

            #endregion

            #region ConnectionString

            services.AddDbContext<ProjectContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:ProjectContext"]);
            });

            #endregion

            #region Extension

            //Dependency Injection Business Objects
            services.AddServices();

            #endregion

            #region  Swagger 
            //***** Configuracion de servicios para Swagger *****
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo { Title = "Project Tesis API", Version = "V1.0" });
                // Autenticacion desde la pagina
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });

            #endregion

            #region TokenHandler JWT

            // JwtHanlder Configuration Libreria que se encarga de la autenticacion 
            JwtHanlder.JwtConfigutration(services, Configuration["ConnectionStrings:ProjectContext"]);
            services.AddHttpContextAccessor();

            #endregion

            services.AddCors();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region JWT
            
            //Configurando la aplicación para JWT
            app.UseAuthentication();

            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            #region  Swagger Config

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404
                    && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/V1/swagger.json", "Project Tesis.APIService");
                c.RoutePrefix = string.Empty;
            });
            #endregion

            #region TokenHandler Exceptions Middleware
            app.ConfigureExceptionHandler();
            #endregion


            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true).AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
