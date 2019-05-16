using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RotaGeek.Core.Interfaces;
using RotaGeek.Infrastructure.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace RotaGeek.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "AllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Registering Swagger Generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Swagger Demo",
                    Version = "v1",
                    Description = "TBD",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "John Doe", Email = "john@xyzmail.com", Url = "www.example.com" },
                    License = new License() { Name = "License Terms", Url = "www.example.com" }
                });
            });

            services.AddScoped<IRepository, EFRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            // Serves generated swagger document as JSON endpoint. 
            app.UseSwagger();

            // Serves the Swagger UI
            app.UseSwaggerUI(c =>
            {
                // specifying the Swagger JSON endpoint.
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            // logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }
    }
}
