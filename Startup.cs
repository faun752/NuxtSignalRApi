using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NuxtSignalRApi.Extensions;
using NuxtSignalRApi.Hubs;
using NuxtSignalRApi.Models;
using NuxtSignalRApi.Services;

namespace NuxtSignalRApi
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string AppAllowSpecificOrigins = "_appAllowSpecificOrigins";

        public void ConfigureServices (IServiceCollection services)
        {
            var settings = new Settings ();
            services.AddCors (o => o.AddPolicy (AppAllowSpecificOrigins, builder =>
            {
                builder.WithOrigins ("http://localhost:3000")
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .AllowCredentials ();
            }));

            services.AddControllers ();
            services.AddSwaggerGen (c =>
            {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "NuxtSignalRApi", Version = "v1" });
            });
            services.AddHostedService<VisitorService> ();
            services.AddSignalRConfiguration (Configuration, settings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
                app.UseSwagger ();
                app.UseSwaggerUI (c => c.SwaggerEndpoint ("/swagger/v1/swagger.json", "NuxtSignalRApi v1"));
            }

            // app.UseHttpsRedirection ();
            app.UseRouting ();
            app.UseCors (AppAllowSpecificOrigins);
            app.UseAuthorization ();

            app.UseEndpoints (endpoints =>
            {
                endpoints.MapControllers ();
                endpoints.MapHub<VisitorHub> ("/hub");
            });
        }
    }
}