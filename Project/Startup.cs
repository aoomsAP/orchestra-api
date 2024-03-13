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
            services.AddScoped<ICountryData, InMemoryCountryData>();
            services.AddScoped<IMusicianData, InMemoryMusicianData>();
            services.AddScoped<IOrchestraData, InMemoryOrchestraData>();
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