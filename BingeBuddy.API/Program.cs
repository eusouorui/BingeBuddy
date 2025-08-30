using BingeBuddy.API.Utility;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add controllers with global authorization policy
builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter(policy));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// Middleware to validate API key
app.Use(async (context, next) =>
{
    // Skip Swagger
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
        await next();
        return;
    }

    // Check for header "X-API-KEY"
    if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("API Key is missing");
        return;
    }

    if (extractedApiKey != Auth.ApiKey)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Invalid API Key");
        return;
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();