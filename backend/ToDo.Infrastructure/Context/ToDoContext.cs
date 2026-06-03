using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class ToDoContext(DbContextOptions<ToDoContext> options) : DbContext(options)
{
    public DbSet<User> Users {get; set;}
    public DbSet<UserTask> UserTasks {get; set;}
    public DbSet<Category> Categories {get; set;}  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}