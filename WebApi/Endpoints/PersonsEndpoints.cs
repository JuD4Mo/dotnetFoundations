using Application.DTOs.Persons;
using Application.UseCases.Persons;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Endpoints
{
  public static class PersonsEndpoints
  {
    public static void MapPersonsEndpoints(this IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("/api/persons").WithTags("Persons");

      group.MapGet("/{id:guid}", async (Guid id, GetPersonByIdUseCase useCase) =>
      {
        try
        {
          var person = await useCase.ExecuteAsync(id);
          return Results.Ok(person);
        }
        catch (InvalidOperationException ex)
        {
          return Results.NotFound(new {error = ex.Message});
        }
      }).WithName("GetPersonById")
      .WithSummary("Get person by a id")
      .Produces(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status404NotFound);
    
      group.MapPost("/", async (CreatePersonDto dto, CreatePersonUseCase useCase) =>
      {
        try
        {
          var person = await useCase.ExecuteAsync(dto);
          return Results.Created($"/api/persons/{person.Id}", person);
        }
        catch (InvalidOperationException ex)
        {
          return Results.BadRequest(new {error = ex.Message});
        }
        catch (ArgumentException ex)
        {
          return Results.BadRequest(new {error = ex.Message});
        }
        catch (Exception ex)
        {
          return Results.InternalServerError(ex.Message);
        }
      }).WithName("CreatePerson")
      .WithSummary("Creates a new person")
      .Produces(StatusCodes.Status201Created)
      .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status500InternalServerError);

      group.MapGet("/", async (GetAllPersonsUseCase useCase) =>
      {
        try
        {
          var persons = await useCase.ExecuteAsync();
          return Results.Ok(persons);
        }
        catch (Exception ex)
        {
          return Results.InternalServerError(ex.Message);
        }
      }).WithName("GetAllPersons")
      .WithSummary("Get all persons")
      .Produces(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status500InternalServerError);


      group.MapPut("/{id:guid}", async (Guid id, UpdatePersonDto dto, UpdatePersonUseCase useCase) =>
      {
        if (id != dto.Id)
        {
          return Results.BadRequest("Los ids no corresponden");
        }
        try
        {
          var person = await useCase.ExecuteAsync(dto);
          return Results.Ok(person);
        }
        catch (InvalidOperationException ex)
        {
          return Results.NotFound(new {error = ex.Message});
        }
        catch (ArgumentException ex)
        {
          return Results.BadRequest(new {error = ex.Message});
        }
        catch (Exception ex)
        {
          return Results.InternalServerError(ex.Message);
        }
      }).WithName("UpdatePerson")
      .WithSummary("update a person")
      .Produces(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status400BadRequest);

      group.MapDelete("/{id:guid}", async (Guid id, DeletePersonUseCase useCase) =>
      {
        try
        {
          await useCase.ExecuteAsync(id);
          return Results.NoContent();
        }
        catch (InvalidOperationException ex)
        {
          return Results.NotFound(new {error = ex.Message});
        }
        catch (Exception ex)
        {
          return Results.InternalServerError(new {error = ex.Message});
        }
      }).WithName("DeletePerson")
      .WithSummary("deletes a person")
      .Produces(StatusCodes.Status204NoContent)
      .Produces(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status500InternalServerError);

      group.MapGet("/code/{code}", async (string code, GetPersonByCodeUseCase useCase) =>
      {
        try
        {
          var person = await useCase.ExecuteAsync(code);
          return Results.Ok(person);
        }
        catch (InvalidOperationException ex)
        {
          return Results.NotFound(new {error = ex.Message});
        }
        catch (Exception ex)
        {
          return Results.InternalServerError(new {error = ex.Message});
        }
      }).WithName("GetPersonByCode")
      .WithSummary("gets a person by their code")
      .Produces(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status500InternalServerError);
    }
  }
}