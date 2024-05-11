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

countryEndpoints.MapPost("", CreatedAtRoute<CountryDto> (
    DataContext dbContext,
    IMapper mapper,
    CountryCreationDto creationDto
    ) =>
{
    var country = mapper.Map<Country>(creationDto);
    dbContext.Countries.Add(country);
    dbContext.SaveChanges();

    var countryToReturn = mapper.Map<CountryDto>(country);
    return TypedResults.CreatedAtRoute(countryToReturn, "GetCountry", new { countrycode = countryToReturn.Code });
});

countryWithCodeEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    string countrycode,
    CountryUpdateDto updateDto
    ) =>
{
    var country = dbContext.Countries
        .FirstOrDefault(c => c.Code == countrycode);
    if (country == null)
    {
        return TypedResults.NotFound();
    }

    country.Name = updateDto.Name;
    dbContext.SaveChanges();

    return TypedResults.NoContent();
});

countryWithCodeEndpoints.MapDelete("", Results<NotFound, NoContent> (
    DataContext dbContext,
    string countrycode
    ) =>
{
    var country = dbContext.Countries
        .FirstOrDefault(c => c.Code == countrycode);
    if (country == null)
    {
        return TypedResults.NotFound();
    }

    dbContext.Countries.Remove(country);
    dbContext.SaveChanges();

    return TypedResults.NoContent();
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

orchestraEndpoints.MapPost("", CreatedAtRoute<OrchestraDto> (
    DataContext dbContext,
    IMapper mapper,
    OrchestraCreationDto creationDto
    ) =>
{
    var orchestra = mapper.Map<Orchestra>(creationDto);
    dbContext.Orchestras.Add(orchestra);
    dbContext.SaveChanges();

    var orchestraToReturn = mapper.Map<OrchestraDto>(orchestra);
    return TypedResults.CreatedAtRoute(orchestraToReturn, "GetOrchestra", new { orchestraid = orchestraToReturn.Id });
});

orchestraWithIdEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    int orchestraid,
    OrchestraUpdateDto updateDto
    ) =>
{
    var orchestra = dbContext.Orchestras
        .FirstOrDefault(o => o.Id == orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    orchestra.Name = updateDto.Name;
    orchestra.Conductor = updateDto.Conductor;
    dbContext.SaveChanges();

    return TypedResults.NoContent();
});

orchestraWithIdEndpoints.MapDelete("", Results<NotFound, NoContent> (
    DataContext dbContext,
    int orchestraid
    ) =>
{
    var orchestra = dbContext.Orchestras
        .FirstOrDefault(o => o.Id == orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    dbContext.Orchestras.Remove(orchestra);
    dbContext.SaveChanges();

    return TypedResults.NoContent();
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

musicianEndpoints.MapPost("", CreatedAtRoute<MusicianDto> (
    DataContext dbContext,
    IMapper mapper,
    MusicianCreationDto creationDto
    ) =>
{
    var musician = mapper.Map<Musician>(creationDto);
    dbContext.Musicians.Add(musician);
    dbContext.SaveChanges();

    var musicianToReturn = mapper.Map<MusicianDto>(musician);
    return TypedResults.CreatedAtRoute(musicianToReturn, "GetMusician", new { musicianid = musicianToReturn.Id });
});

musicianWithIdEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    int musicianid,
    MusicianUpdateDto updateDto
    ) =>
{
    var musician = dbContext.Musicians
        .FirstOrDefault(m => m.Id == musicianid);
    if (musician == null)
    {
        return TypedResults.NotFound();
    }

    musician.Name = updateDto.Name;
    musician.Instrument = updateDto.Instrument;
    dbContext.SaveChanges();

    return TypedResults.NoContent();
});

musicianWithIdEndpoints.MapDelete("", Results<NotFound, NoContent> (
    DataContext dbContext,
    int musicianid
    ) =>
{
    var musician = dbContext.Musicians
        .FirstOrDefault(m => m.Id == musicianid);
    if (musician == null)
    {
        return TypedResults.NotFound();
    }

    dbContext.Musicians.Remove(musician);
    dbContext.SaveChanges();

    return TypedResults.NoContent();
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