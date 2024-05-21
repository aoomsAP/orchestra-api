﻿using Library.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Project.Services;

namespace Project
{
    public class Startup
    {
        // The below method configures the depedency containers
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            //services.AddScoped<IData, InMemoryData>();
            services.AddScoped<IData, EfData>();

            // Database connection

            var connection = "server=localhost; database=orchestra-db; user=root; password=password";

            // mySqlOptions / MigrationsAssembly necessary because DbContext is in class library
            // and this apparently causes migrations confusion
            services.AddDbContext<DataContext>(x => x.UseMySql(
                connection,
                ServerVersion.AutoDetect(connection),
                mySqlOptions =>
            {
                mySqlOptions.MigrationsAssembly("MVC");
            }));
        }

        // The below method gets called by runtime
        // It configures the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Development
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = String.Empty;
                });
            }
            // Production
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Something went wrong.")
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}