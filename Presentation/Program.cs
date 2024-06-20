using Application;
using Infrastructure;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation.Middleware;
using Presentation.Options;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
else
{
    builder.Configuration.AddJsonFile("/etc/secrets/secrets.json");
}

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("localhostPolicy", builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:4200")
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("localhostPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.MigrateContext();

app.Run();