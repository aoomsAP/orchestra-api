using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;
using Project.Entities;
using System.ComponentModel.DataAnnotations;

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


// Country --------------------------------------------------------------------------------

// get

countryEndpoints.MapGet("", Ok<IEnumerable<CountryDto>> (
    DataContext dbContext,
    IMapper mapper
    ) =>
{
    return TypedResults.Ok(mapper.Map<IEnumerable<CountryDto>>(dbContext.Countries));
});

countryWithCodeEndpoints.MapGet("", Results<Ok<CountryDto>, NotFound> (
    DataContext dbContext,
    [RegularExpression("^[A-Z]{2}$")] string countrycode,
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

countryOrchestraEndpoints.MapGet("", Results<Ok<IEnumerable<OrchestraDto>>, NotFound> (
    DataContext dbContext,
    [RegularExpression("^[A-Z]{2}$")] string countrycode,
    IMapper mapper
    ) =>
{
    var country = dbContext.Countries.FirstOrDefault(x => x.Code == countrycode);

    if (country == null)
    {
        return TypedResults.NotFound();
    }

    var orchestras = dbContext.Countries
        .Include(x => x.Orchestras)
        .FirstOrDefault(x => x.Code == countrycode)?.Orchestras;

    return TypedResults.Ok(mapper.Map<IEnumerable<OrchestraDto>>(orchestras));
});

// post

countryEndpoints.MapPost("", CreatedAtRoute<CountryDto> (
    DataContext dbContext,
    IMapper mapper,
    CountryCreationDto creationDto
    ) =>
{
    // how to validate dto? and return BadRequest?

    var country = mapper.Map<Country>(creationDto);
    dbContext.Countries.Add(country);
    dbContext.SaveChanges();

    var countryToReturn = mapper.Map<CountryDto>(country);
    return TypedResults.CreatedAtRoute(countryToReturn, "GetCountry", new { countrycode = countryToReturn.Code });
});

// put

countryWithCodeEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    [RegularExpression("^[A-Z]{2}$")] string countrycode,
    CountryUpdateDto updateDto
    ) =>
{
    // how to validate dto? and return BadRequest?

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

countryOrchestraEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    [RegularExpression("^[A-Z]{2}$")] string countrycode,
    CountryOrchestrasUpdateDto updateDto
    ) =>
{
    // how to validate dto? and return BadRequest?

    var country = dbContext.Countries
        .Include(m => m.Orchestras)
        .FirstOrDefault(m => m.Code == countrycode);
    if (country == null)
    {
        return TypedResults.NotFound();
    }

    var newOrchestras = new List<Orchestra>();
    foreach (var orchestraid in updateDto.OrchestraIds)
    {
        var newOrchestra = dbContext.Orchestras.FirstOrDefault(o => o.Id == orchestraid);
        if (newOrchestra == null)
        {
            return TypedResults.NotFound();
        }
        newOrchestras.Add(newOrchestra);
    }

    country.Orchestras = newOrchestras;
    dbContext.SaveChanges();

    return TypedResults.NoContent();
});

// delete

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


// Orchestra --------------------------------------------------------------------------------

// get

orchestraEndpoints.MapGet("", Ok<IEnumerable<OrchestraDto>> (
    DataContext dbContext,
    IMapper mapper
    ) =>
{
    return TypedResults.Ok(mapper.Map<IEnumerable<OrchestraDto>>(dbContext.Orchestras.Include(o => o.Country)));
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

orchestraMusiciansEndpoints.MapGet("", Results<Ok<IEnumerable<MusicianDto>>, NotFound> (
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

    var musicians = dbContext.Orchestras
        .Include(x => x.Musicians)
        .FirstOrDefault(x => x.Id == orchestraid)?.Musicians;

    return TypedResults.Ok(mapper.Map<IEnumerable<MusicianDto>>(musicians));
});

// post

orchestraEndpoints.MapPost("", CreatedAtRoute<OrchestraDto> (
    DataContext dbContext,
    IMapper mapper,
    OrchestraCreationDto creationDto
    ) =>
{
    // how to validate dto? and return BadRequest?

    var orchestra = mapper.Map<Orchestra>(creationDto);
    // possible with mapping?
    var country = dbContext.Countries.FirstOrDefault(c => c.Code == creationDto.CountryCode);
    orchestra.Country = country;
    dbContext.Orchestras.Add(orchestra);
    dbContext.SaveChanges();

    var orchestraToReturn = mapper.Map<OrchestraDto>(orchestra);
    return TypedResults.CreatedAtRoute(orchestraToReturn, "GetOrchestra", new { orchestraid = orchestraToReturn.Id });
});

// put

orchestraWithIdEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    int orchestraid,
    OrchestraUpdateDto updateDto
    ) =>
{
    // how to validate dto? and return BadRequest?

    var orchestra = dbContext.Orchestras
        .FirstOrDefault(o => o.Id == orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    // possible with mapping?
    var newCountry = dbContext.Countries.FirstOrDefault(c => c.Code == updateDto.CountryCode);
    if (newCountry == null)
    {
        return TypedResults.NotFound();
    }

    orchestra.Name = updateDto.Name;
    orchestra.Conductor = updateDto.Conductor;
    orchestra.Country = newCountry;
    dbContext.SaveChanges();

    return TypedResults.NoContent();
});

orchestraMusiciansEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    int orchestraid,
    OrchestraMusiciansUpdateDto updateDto
    ) =>
{
    // how to validate dto? and return BadRequest?

    var orchestra = dbContext.Orchestras
        .Include (o => o.Musicians)
        .FirstOrDefault(o => o.Id == orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    var newMusicians = new List<Musician>();
    foreach (var musicianid in updateDto.MusicianIds)
    {
        var newMusician = dbContext.Musicians.FirstOrDefault(o => o.Id == musicianid);
        if (newMusician == null)
        {
            return TypedResults.NotFound();
        }
        newMusicians.Add(newMusician);
    }

    orchestra.Musicians = newMusicians;
    dbContext.SaveChanges();

    return TypedResults.NoContent();
});

// delete

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


// Musician --------------------------------------------------------------------------------

// get

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

musicianOrchestrasEndpoints.MapGet("", Results<Ok<IEnumerable<OrchestraDto>>, NotFound> (
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

    var orchestras = dbContext.Musicians
        .Include(x => x.Orchestras)
        .ThenInclude(x => x.Country)
        .FirstOrDefault(x => x.Id == musicianid)?.Orchestras;

    return TypedResults.Ok(mapper.Map<IEnumerable<OrchestraDto>>(orchestras));
});

// post

musicianEndpoints.MapPost("", CreatedAtRoute<MusicianDto> (
    DataContext dbContext,
    IMapper mapper,
    MusicianCreationDto creationDto
    ) =>
{
    // how to validate dto? and return BadRequest?

    var musician = mapper.Map<Musician>(creationDto);
    dbContext.Musicians.Add(musician);
    dbContext.SaveChanges();

    var musicianToReturn = mapper.Map<MusicianDto>(musician);
    return TypedResults.CreatedAtRoute(musicianToReturn, "GetMusician", new { musicianid = musicianToReturn.Id });
});

// put

musicianWithIdEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    int musicianid,
    MusicianUpdateDto updateDto
    ) =>
{
    // how to validate dto? and return BadRequest?

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

musicianOrchestrasEndpoints.MapPut("", Results<NotFound, NoContent> (
    DataContext dbContext,
    IMapper mapper,
    int musicianid,
    MusicianOrchestrasUpdateDto updateDto
    ) =>
{
    // how to validate dto? and return BadRequest?

    var musician = dbContext.Musicians
        .Include(m => m.Orchestras)
        .FirstOrDefault(m => m.Id == musicianid);
    if (musician == null)
    {
        return TypedResults.NotFound();
    }

    var newOrchestras = new List<Orchestra>();
    foreach (var orchestraid in updateDto.OrchestraIds)
    {
        var newOrchestra = dbContext.Orchestras.FirstOrDefault(o => o.Id == orchestraid);
        if (newOrchestra == null)
        {
            return TypedResults.NotFound();
        }
        newOrchestras.Add(newOrchestra);
    }

    musician.Orchestras = newOrchestras;
    dbContext.SaveChanges();

    return TypedResults.NoContent();
});

// delete

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