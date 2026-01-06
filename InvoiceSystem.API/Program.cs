using AutoMapper;
using FluentValidation;
using InvoiceSystem.API.Middlewares;
using InvoiceSystem.Application.Mapping;
using InvoiceSystem.Application;
using InvoiceSystem.Core;
using InvoiceSystem.Core.Repositories.Contract;
using InvoiceSystem.Infrastructure;
using InvoiceSystem.Infrastructure._Data;
using InvoiceSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using InvoiceSystem.API.ErrorsResponse;
using MediatR;
using InvoiceSystem.Application.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));
builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfile)));
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>)
);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = (actionResult) =>
    {
        var errors = actionResult.ModelState.Where(parameter => parameter.Value.Errors.Count() > 0)
                                            .SelectMany(parameter => parameter.Value.Errors)
                                            .Select(error => error.ErrorMessage)
                                            .ToList();
        var response = new ApiValidationErrorResponse()
        {
            Errors = errors
        };
        return new BadRequestObjectResult(response);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(new ApiResponse(response.StatusCode));

});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "v1"); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
