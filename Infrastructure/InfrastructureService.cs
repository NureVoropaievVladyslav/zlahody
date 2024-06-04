using System.Reflection;
using Application.Abstractions.Interfaces;
using Domain.Common;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Hubs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddRepositoriesFromAssemblies();
        services.AddServicesFromAssemblies();
        services.InitialiseFirebaseApp(configuration);
        services.AddSignalR();
        services.AddHttpContextAccessor();
        
        return services;
    }

    public static void MigrateContext(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        using var context = scope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context?.Database.Migrate();
    }
    
    private static void AddRepositoriesFromAssemblies(this IServiceCollection services)
    {
        var entityAssembly = Assembly.GetAssembly(typeof(BaseEntity));
        var repositoryType = typeof(IRepository<>);

        foreach (var entityType in entityAssembly!.GetTypes().Where(t => t.IsClass && t.IsSubclassOf(typeof(BaseEntity))))
        {
            var repositoryInterface = repositoryType.MakeGenericType(entityType);
            var repositoryImplementation = typeof(Repository<>).MakeGenericType(entityType);

            services.AddScoped(repositoryInterface, repositoryImplementation);
        }
    }

    private static void AddServicesFromAssemblies(this IServiceCollection services)
    {
        var interfaceAssembly = Assembly.GetAssembly(typeof(IUserService));
        var implementationAssembly = Assembly.GetAssembly(typeof(UserService));

        var interfaces = interfaceAssembly!.GetTypes()
            .Where(t => t.IsInterface && t.Namespace == typeof(IUserService).Namespace && t.Name.EndsWith("Service"));

        foreach (var @interface in interfaces)
        {
            var implementationName = @interface.Name.Substring(1);
            var implementation = implementationAssembly!.GetTypes()
                .FirstOrDefault(t => t.IsClass && t.Namespace == typeof(UserService).Namespace && t.Name == implementationName);

            if (implementation != null && @interface.IsAssignableFrom(implementation))
            {
                services.AddScoped(@interface, implementation);
            }
        }
    }
    
    private static void InitialiseFirebaseApp(this IServiceCollection services, IConfiguration configuration)
    {
        var credentials = configuration.GetSection("GoogleApplicationCredentials")
            .GetChildren()
            .ToDictionary(section => section.Key, section => section.Value);
        
        var credentialsJson = JsonConvert.SerializeObject(credentials);
        
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromJson(credentialsJson),
            ProjectId = credentials.GetValueOrDefault("project_id")
        });
    }
}