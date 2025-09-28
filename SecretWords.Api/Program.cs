using Microsoft.OpenApi.Models;
using SensitiveWords.Api.Data;
using SensitiveWords.Api.Interfaces;
using SensitiveWords.Api.Repositories;
using SensitiveWords.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Dependency injection
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>(); // Only need to add as singleton to not spawn multiple instances for db connections
builder.Services.AddScoped<ISensitiveWordRepository, SensitiveWordRepository>();
builder.Services.AddScoped<ISensitiveWordService, SensitiveWordService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SensitiveWords API",
        Version = "v1",
        Description = "API to manage and bloop sensitive words"
    });
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
