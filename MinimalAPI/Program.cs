using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;
using Project.Entities;

var builder = WebApplication.CreateBuilder(args);


// ADDING SERVICES TO CONTAINER
// ----------------------------

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Getting connection string & registering DbContext to container
var connection = builder.Configuration["ConnectionStrings:DbConnectionString"];
builder.Services.AddDbContext<DataContext>(o => o.UseMySql(connection, ServerVersion.AutoDetect(connection), b => b.MigrationsAssembly("MinimalAPI")));

// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Exception Handling
builder.Services.AddProblemDetails();

var app = builder.Build();


// HTTP REQUEST PIPELINE
// ---------------------

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler();
}


// ENDPOINTS
// ---------

// Endpoint groups

var countryEndpoints = app.MapGroup("/countries");
var countryWithCodeEndpoints = app.MapGroup("/countries/{countrycode}");
var countryOrchestraEndpoints = app.MapGroup("/countries/{countrycode}/orchestras");

var orchestraEndpoints = app.MapGroup("/orchestras");
var orchestraWithIdEndpoints = app.MapGroup("/orchestras/{orchestraid}");
var orchestraMusiciansEndpoints = app.MapGroup("/orchestras/{orchestraid}/musicians");

var musicianEndpoints = app.MapGroup("/musicians");
var musicianWithIdEndpoints = app.MapGroup("/musicians/{musicianid}");
var musicianOrchestrasEndpoints = app.MapGroup("/musicians/{musicianid}/orchestras");


// Country

countryEndpoints.MapGet("", Ok<IEnumerable<CountryDto>> (
    DataContext dbContext,
    IMapper mapper
    ) =>
{
    return TypedResults.Ok(mapper.Map<IEnumerable<CountryDto>>(dbContext.Countries));
});

countryWithCodeEndpoints.MapGet("", Results<Ok<CountryDto>, NotFound> (
    DataContext dbContext,
    string countrycode,
    IMapper mapper
    ) =>
{
    var country = dbContext.Countries.FirstOrDefault(x => x.Code == countrycode);

    if (country == null)
    {
        return TypedResults.NotFound();
    }

    return TypedResults.Ok(mapper.Map<CountryDto>(country));
}).WithName("GetCountry");

countryOrchestraEndpoints.MapGet("", Ok<IEnumerable<OrchestraDto>> (
    DataContext dbContext,
    string countrycode,
    IMapper mapper
    ) =>
{
    var orchestras = dbContext.Countries
        .Include(x => x.Orchestras)
        .FirstOrDefault(x => x.Code == countrycode)?.Orchestras;

    return TypedResults.Ok(mapper.Map<IEnumerable<OrchestraDto>>(orchestras));
});


// Orchestra

orchestraEndpoints.MapGet("", Ok<IEnumerable<OrchestraDto>> (
    DataContext dbContext,
    IMapper mapper
    ) =>
{
    return TypedResults.Ok(mapper.Map<IEnumerable<OrchestraDto>>(dbContext.Orchestras));
});

orchestraWithIdEndpoints.MapGet("", Results<Ok<OrchestraDto>, NotFound> (
    DataContext dbContext,
    int orchestraid,
    IMapper mapper
    ) =>
{
    var orchestra = dbContext.Orchestras.FirstOrDefault(x => x.Id == orchestraid);

    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    return TypedResults.Ok(mapper.Map<OrchestraDto>(orchestra));
}).WithName("GetOrchestra");

orchestraMusiciansEndpoints.MapGet("", Ok<IEnumerable<MusicianDto>> (
    DataContext dbContext,
    int orchestraid,
    IMapper mapper
    ) =>
{
    var musicians = dbContext.Orchestras
        .Include(x => x.Musicians)
        .FirstOrDefault(x => x.Id == orchestraid)?.Musicians;

    return TypedResults.Ok(mapper.Map<IEnumerable<MusicianDto>>(musicians));
});


// Musician

musicianEndpoints.MapGet("", Ok<IEnumerable<MusicianDto>> (
    DataContext dbContext,
    IMapper mapper
    ) =>
{
    return TypedResults.Ok(mapper.Map<IEnumerable<MusicianDto>>(dbContext.Musicians));
});

musicianWithIdEndpoints.MapGet("", Results<Ok<MusicianDto>, NotFound> (
    DataContext dbContext,
    int musicianid,
    IMapper mapper
    ) =>
{
    var musician = dbContext.Musicians.FirstOrDefault(x => x.Id == musicianid);

    if (musician == null)
    {
        return TypedResults.NotFound();
    }

    return TypedResults.Ok(mapper.Map<MusicianDto>(musician));
}).WithName("GetMusician");

musicianOrchestrasEndpoints.MapGet("", Ok<IEnumerable<OrchestraDto>> (
    DataContext dbContext,
    int musicianid,
    IMapper mapper
    ) =>
{
    var orchestras = dbContext.Musicians
        .Include(x => x.Orchestras)
        .FirstOrDefault(x => x.Id == musicianid)?.Orchestras;

    return TypedResults.Ok(mapper.Map<IEnumerable<OrchestraDto>>(orchestras));
});


// RUN
// ---

// Recreate & migrate the database on each run, for demo purposes

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();