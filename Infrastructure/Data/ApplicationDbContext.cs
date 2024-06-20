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
    public DbSet<Resource> Resources => Set<Resource>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    
    public void MarkMessagesAsRead(Guid messageId, Guid chatId)
    {
        Database.ExecuteSqlRaw("CALL mark_messages_as_read({0}, {1})", messageId, chatId);
    }


}