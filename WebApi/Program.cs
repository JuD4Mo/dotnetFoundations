using System.Text.Json.Serialization;
using Application.UseCases.Persons;
using Application.UseCases.Visits;
using Data;
using Domain;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("db connection string not found");

// Extend methods
builder.Services.AddRepositories(connectionString);

builder.Services.AddScoped<CreatePersonUseCase>();
builder.Services.AddScoped<GetPersonByIdUseCase>();
builder.Services.AddScoped<GetPersonByCodeUseCase>();
builder.Services.AddScoped<GetAllPersonsUseCase>();
builder.Services.AddScoped<UpdatePersonUseCase>();
builder.Services.AddScoped<DeletePersonUseCase>();

builder.Services.AddScoped<GetActiveVisitsUseCase>();
builder.Services.AddScoped<GetAllVisitsUseCase>();
builder.Services.AddScoped<GetVisitsByPersonUseCase>();
builder.Services.AddScoped<RegisterEntryUseCase>();
builder.Services.AddScoped<RegisterExitUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPersonsEndpoints();
app.MapVisitsEndpoints();

app.Run();
