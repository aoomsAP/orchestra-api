using Library.Contexts;
using Microsoft.EntityFrameworkCore;
using Project;

// Recreate & migrate the database on each run, for demo purposes

// Altered program class for doing this in MVC based on:
// https://medium.com/@ashishnimrot/extending-net-core-application-with-fake-data-seeding-in-development-environment-1c1a6eb21ff0

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build();

using (var serviceScope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

await host.RunAsync();