using System.Reflection;
using Domain.Entities;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Request> Requests => Set<Request>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<ChatUser> ChatUsers => Set<ChatUser>();
    public DbSet<RequestAssignment> RequestAssignments => Set<RequestAssignment>();
    public DbSet<OrganizationApplication> OrganizationApplication => Set<OrganizationApplication>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}