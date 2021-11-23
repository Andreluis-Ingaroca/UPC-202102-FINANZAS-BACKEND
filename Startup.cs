using Finanzas.Domain.Persistence.Context;
using Finanzas.Domain.Persistence.Repositories;
using Finanzas.Domain.Services;
using Finanzas.Persistence.Repositories;
using Finanzas.Services;
using Finanzas.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas
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
            // Add CORS Support
            services.AddCors();

            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
                options.UseMySQL(Configuration.GetConnectionString("SmarterAspMySqlConnection"));
                
            });

            // AppSettings Section Reference
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSetings>(appSettingsSection);

            // JSON Web Token Authentication Configuration
            var appSettings = appSettingsSection.Get<AppSetings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // Authentication Service Configuration
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //Dependency Injection Configuration
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarteraRepository, CarteraRepository>();
            services.AddScoped<ICostoRepository, CostoRepository>();
            services.AddScoped<ICostosOperacionRepository, CostosOperacionRepository>();
            services.AddScoped<ILetraRepository, LetraRepository>();
            services.AddScoped<IOperacionCarteraRepository, OperacionCarteraRepository>();
            services.AddScoped<IOperacionLetraRepository, OperacionLetraRepository>();
            services.AddScoped<IOperacionRepository, OperacionRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<IPeriodoRepository, PeriodoRepository>();
            services.AddScoped<ITasaRepository, TasaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();


            services.AddScoped<ICarteraService, CarteraService>();
            services.AddScoped<ICostoService, CostoService>();
            services.AddScoped<ICostosOperacionService, CostosOperacionService>();
            services.AddScoped<ILetraService, LetraService>();
            services.AddScoped<IOperacionCarteraService, OperacionCarteraService>();
            services.AddScoped<IOperacionLetraService, OperacionLetraService>();
            services.AddScoped<IOperacionService, OperacionService>();
            services.AddScoped<IPerfilService, PerfilService>();
            services.AddScoped<IPeriodoService, PeriodoService>();
            services.AddScoped<ITasaService, TasaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            //Apply Endpoint Naming Convention
            services.AddRouting(options => options.LowercaseUrls = true);

            // AutoMapper Setup
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Finanzas", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Finanzas v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // CORS Configuration
            app.UseCors(x => x.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
