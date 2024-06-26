using AutoMapper;
using FluentValidation;
using Library.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;
using Project.Entities;
using Project.Services;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);


// ADDING SERVICES TO CONTAINER
// ----------------------------

// Data
//builder.Services.AddScoped<IData, InMemoryData>();
builder.Services.AddScoped<IData, EfData>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Getting connection string & registering DbContext to container
var connection = builder.Configuration["ConnectionStrings:DbConnectionString"];
builder.Services.AddDbContext<DataContext>(o => o.UseMySql(connection, ServerVersion.AutoDetect(connection), b => b.MigrationsAssembly("MinimalAPI")));

// Fluent Validators
// Fluent validation implementation based on docs https://docs.fluentvalidation.net/en/latest/aspnet.html
builder.Services.AddScoped<IValidator<CountryCreationDto>, CountryCreatonDtoValidator>();
builder.Services.AddScoped<IValidator<CountryUpdateDto>, CountryUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<OrchestraCreationDto>, OrchestraCreationDtoValidator>();
builder.Services.AddScoped<IValidator<OrchestraUpdateDto>, OrchestraUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<MusicianCreationDto>, MusicianCreationDtoValidator>();
builder.Services.AddScoped<IValidator<MusicianUpdateDto>, MusicianUpdateDtoValidator>();

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
var countryWithCodeEndpoints = countryEndpoints.MapGroup("/{countrycode}");
var countryOrchestraEndpoints = countryWithCodeEndpoints.MapGroup("/orchestras");

var orchestraEndpoints = app.MapGroup("/orchestras");
var orchestraWithIdEndpoints = orchestraEndpoints.MapGroup("/{orchestraid}");
var orchestraMusiciansEndpoints = orchestraWithIdEndpoints.MapGroup("/musicians");

var musicianEndpoints = app.MapGroup("/musicians");
var musicianWithIdEndpoints = musicianEndpoints.MapGroup("/{musicianid}");
var musicianOrchestrasEndpoints = musicianWithIdEndpoints.MapGroup("/orchestras");


// Country --------------------------------------------------------------------------------

// get

countryEndpoints.MapGet("", Ok<IEnumerable<CountryDto>> (
    [FromServices] IData data,
    [FromServices] IMapper mapper
    ) =>
{
    var countries = mapper.Map<IEnumerable<CountryDto>>(data.GetCountries());
    return TypedResults.Ok(countries);
});

countryWithCodeEndpoints.MapGet("", Results<Ok<CountryDto>, NotFound> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute][RegularExpression("^[A-Z]{2}$")] string countrycode
    ) =>
{
    var country = data.GetCountry(countrycode);
    if (country == null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(mapper.Map<CountryDto>(country));

}).WithName("GetCountry");

countryOrchestraEndpoints.MapGet("", Results<Ok<IEnumerable<OrchestraDto>>, NotFound> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute][RegularExpression("^[A-Z]{2}$")] string countrycode
    ) =>
{
    var country = data.GetCountry(countrycode);
    if (country == null)
    {
        return TypedResults.NotFound();
    }

    var orchestras = mapper.Map<IEnumerable<OrchestraDto>>(country.Orchestras);

    return TypedResults.Ok(orchestras);
});

// post

countryEndpoints.MapPost("", Results<CreatedAtRoute<CountryDto>, ValidationProblem> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromBody] CountryCreationDto creationDto,
    [FromServices] IValidator<CountryCreationDto> validator
    ) =>
{
    FluentValidation.Results.ValidationResult validation = validator.Validate(creationDto);
    if (!validation.IsValid)
    {
        return TypedResults.ValidationProblem(validation.ToDictionary()); // Bad Request 400
    }

    var country = mapper.Map<Country>(creationDto);
    data.AddCountry(country);

    var countryToReturn = mapper.Map<CountryDto>(country);
    return TypedResults.CreatedAtRoute(countryToReturn, "GetCountry", new { countrycode = countryToReturn.Code });
}) ;

// put

countryWithCodeEndpoints.MapPut("", Results<NotFound, NoContent, ValidationProblem> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute][RegularExpression("^[A-Z]{2}$")] string countrycode,
    [FromBody] CountryUpdateDto updateDto,
    [FromServices] IValidator<CountryUpdateDto> validator

    ) =>
{
    FluentValidation.Results.ValidationResult validation = validator.Validate(updateDto);
    if (!validation.IsValid)
    {
        return TypedResults.ValidationProblem(validation.ToDictionary()); // Bad Request 400
    }

    var country = data.GetCountry(countrycode);
    if (country == null)
    {
        return TypedResults.NotFound(); // 404
    }

    country.Name = updateDto.Name;
    data.UpdateCountry(country);

    return TypedResults.NoContent();
});

countryOrchestraEndpoints.MapPut("", Results<NotFound, NoContent> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute][RegularExpression("^[A-Z]{2}$")] string countrycode,
    [FromBody] CountryOrchestrasUpdateDto updateDto
    ) =>
{
    var country = data.GetCountry(countrycode);
    if (country == null)
    {
        return TypedResults.NotFound();
    }

    // the reason that this happens in the endpoint instead of the data service,
    // is so we can send a 404 response if an orchestra is not found
    var newOrchestras = new List<Orchestra>();
    foreach (var orchestraId in updateDto.OrchestraIds)
    {
        // check if orchestra exists
        var newOrchestra = data.GetOrchestra(orchestraId);
        if (newOrchestra == null)
        {
            return TypedResults.NotFound();
        }
        newOrchestras.Add(newOrchestra);
    }

    country.Orchestras = newOrchestras;
    data.UpdateCountryOrchestras(country);

    return TypedResults.NoContent();
});

// delete

countryWithCodeEndpoints.MapDelete("", Results<NotFound, NoContent> (
    [FromServices] IData data,
    [FromRoute][RegularExpression("^[A-Z]{2}$")] string countrycode
    ) =>
{
    var country = data.GetCountry(countrycode);
    if (country == null)
    {
        return TypedResults.NotFound();
    }

    data.DeleteCountry(country);

    return TypedResults.NoContent();
});


// Orchestra --------------------------------------------------------------------------------

// get

orchestraEndpoints.MapGet("", Ok<IEnumerable<OrchestraDto>> (
    [FromServices] IData data,
    [FromServices] IMapper mapper
    ) =>
{
    var orchestras = mapper.Map<IEnumerable<OrchestraDto>>(data.GetOrchestras());
    return TypedResults.Ok(orchestras);
});

orchestraWithIdEndpoints.MapGet("", Results<Ok<OrchestraDto>, NotFound> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute] int orchestraid
    ) =>
{
    var orchestra = data.GetOrchestra(orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(mapper.Map<OrchestraDto>(orchestra));

}).WithName("GetOrchestra");

orchestraMusiciansEndpoints.MapGet("", Results<Ok<IEnumerable<MusicianDto>>, NotFound> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute] int orchestraid
    ) =>
{
    var orchestra = data.GetOrchestra(orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    var musicians = mapper.Map<IEnumerable<MusicianDto>>(orchestra.Musicians);

    return TypedResults.Ok(musicians);
});

// post

orchestraEndpoints.MapPost("", Results<CreatedAtRoute<OrchestraDto>, ValidationProblem, NotFound> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromBody] OrchestraCreationDto creationDto,
    [FromServices] IValidator<OrchestraCreationDto> validator
    ) =>
{
    FluentValidation.Results.ValidationResult validation = validator.Validate(creationDto);
    if (!validation.IsValid)
    {
        return TypedResults.ValidationProblem(validation.ToDictionary()); // Bad Request 400
    }

    // check if country exists
    var country = data.GetCountry(creationDto.CountryCode);
    if (country == null)
    {
        return TypedResults.NotFound();
    }

    var orchestra = mapper.Map<Orchestra>(creationDto);
    orchestra.Country = country;

    data.AddOrchestra(orchestra);

    var orchestraToReturn = mapper.Map<OrchestraDto>(orchestra);
    return TypedResults.CreatedAtRoute(orchestraToReturn, "GetOrchestra", new { orchestraid = orchestraToReturn.Id });
});

// put

orchestraWithIdEndpoints.MapPut("", Results<NotFound, NoContent, ValidationProblem> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute] int orchestraid,
    [FromBody] OrchestraUpdateDto updateDto,
    [FromServices] IValidator<OrchestraUpdateDto> validator
    ) =>
{
    FluentValidation.Results.ValidationResult validation = validator.Validate(updateDto);
    if (!validation.IsValid)
    {
        return TypedResults.ValidationProblem(validation.ToDictionary()); // Bad Request 400
    }

    var orchestra = data.GetOrchestra(orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    var newCountry = data.GetCountry(updateDto.CountryCode);
    if (newCountry == null)
    {
        return TypedResults.NotFound();
    }

    orchestra.Name = updateDto.Name;
    orchestra.Conductor = updateDto.Conductor;
    orchestra.Country = newCountry;

    data.UpdateOrchestraMusicians(orchestra);

    return TypedResults.NoContent();
});

orchestraMusiciansEndpoints.MapPut("", Results<NotFound, NoContent> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute] int orchestraid,
    [FromBody] OrchestraMusiciansUpdateDto updateDto
    ) =>
{
    var orchestra = data.GetOrchestra(orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    // the reason that this happens in the endpoint instead of the data service,
    // is so we can send a 404 response if a musician is not found
    var newMusicians = new List<Musician>();
    foreach (var musicianId in updateDto.MusicianIds)
    {
        // check if orchestra exists
        var newMusician = data.GetMusician(musicianId);
        if (newMusician == null)
        {
            return TypedResults.NotFound();
        }
        newMusicians.Add(newMusician);
    }

    orchestra.Musicians = newMusicians;
    data.UpdateOrchestraMusicians(orchestra);

    return TypedResults.NoContent();
});

// delete

orchestraWithIdEndpoints.MapDelete("", Results<NotFound, NoContent> (
    [FromServices] IData data,
    [FromRoute] int orchestraid
    ) =>
{
    var orchestra = data.GetOrchestra(orchestraid);
    if (orchestra == null)
    {
        return TypedResults.NotFound();
    }

    data.DeleteOrchestra(orchestra);

    return TypedResults.NoContent();
});


// Musician --------------------------------------------------------------------------------

// get

musicianEndpoints.MapGet("", Ok<IEnumerable<MusicianDto>> (
    [FromServices] IData data,
    [FromServices] IMapper mapper
    ) =>
{
    var musicians = mapper.Map<IEnumerable<MusicianDto>>(data.GetMusicians());
    return TypedResults.Ok(musicians);
});

musicianWithIdEndpoints.MapGet("", Results<Ok<MusicianDto>, NotFound> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute] int musicianid
    ) =>
{
    var musician = data.GetMusician(musicianid);
    if (musician == null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(mapper.Map<MusicianDto>(musician));

}).WithName("GetMusician");

musicianOrchestrasEndpoints.MapGet("", Results<Ok<IEnumerable<OrchestraDto>>, NotFound> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute] int musicianid
    ) =>
{
    var musician = data.GetMusician(musicianid);
    if (musician == null)
    {
        return TypedResults.NotFound();
    }

    var orchestras = mapper.Map<IEnumerable<OrchestraDto>>(musician.Orchestras);

    return TypedResults.Ok(mapper.Map<IEnumerable<OrchestraDto>>(orchestras));
});

// post

musicianEndpoints.MapPost("", Results<CreatedAtRoute<MusicianDto>, ValidationProblem> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromBody] MusicianCreationDto creationDto,
    [FromServices] IValidator<MusicianCreationDto> validator
    ) =>
{
    FluentValidation.Results.ValidationResult validation = validator.Validate(creationDto);
    if (!validation.IsValid)
    {
        return TypedResults.ValidationProblem(validation.ToDictionary()); // Bad Request 400
    }

    var musician = mapper.Map<Musician>(creationDto);
    data.AddMusician(musician);

    var musicianToReturn = mapper.Map<MusicianDto>(musician);
    return TypedResults.CreatedAtRoute(musicianToReturn, "GetMusician", new { musicianid = musicianToReturn.Id });
});

// put

musicianWithIdEndpoints.MapPut("", Results<NotFound, NoContent, ValidationProblem> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute] int musicianid,
    [FromBody] MusicianUpdateDto updateDto,
    [FromServices] IValidator<MusicianUpdateDto> validator
    ) =>
{
    FluentValidation.Results.ValidationResult validation = validator.Validate(updateDto);
    if (!validation.IsValid)
    {
        return TypedResults.ValidationProblem(validation.ToDictionary()); // Bad Request 400
    }

    var musician = data.GetMusician(musicianid);
    if (musician == null)
    {
        return TypedResults.NotFound();
    }

    musician.Name = updateDto.Name;
    musician.Instrument = updateDto.Instrument;
    data.UpdateMusician(musician);

    return TypedResults.NoContent();
});

musicianOrchestrasEndpoints.MapPut("", Results<NotFound, NoContent> (
    [FromServices] IData data,
    [FromServices] IMapper mapper,
    [FromRoute] int musicianid,
    [FromBody] MusicianOrchestrasUpdateDto updateDto
    ) =>
{
    var musician = data.GetMusician(musicianid);
    if (musician == null)
    {
        return TypedResults.NotFound();
    }

    var newOrchestras = new List<Orchestra>();
    foreach (var orchestraId in updateDto.OrchestraIds)
    {
        var newOrchestra = data.GetOrchestra(orchestraId);
        if (newOrchestra == null)
        {
            return TypedResults.NotFound();
        }
        newOrchestras.Add(newOrchestra);
    }

    musician.Orchestras = newOrchestras;
    data.UpdateMusicianOrchestras(musician);

    return TypedResults.NoContent();
});

// delete

musicianWithIdEndpoints.MapDelete("", Results<NotFound, NoContent> (
    [FromServices] IData data,
    [FromRoute] int musicianid
    ) =>
{
    var musician = data.GetMusician(musicianid);
    if (musician == null)
    {
        return TypedResults.NotFound();
    }

    data.DeleteMusician(musician);

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