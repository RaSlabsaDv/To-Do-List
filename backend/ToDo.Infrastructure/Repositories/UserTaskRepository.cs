using Microsoft.EntityFrameworkCore;

public class UserTaskRepository(ToDoContext context) : IUserTaskRepository
{
    public async Task CreateAsync(UserTask userTask)
    {
        await context.UserTasks.AddAsync(userTask);
        await context.SaveChangesAsync();
    }

    public async Task<UserTask?> GetByIdAsync(int id)
    {
        return await context.UserTasks.FindAsync(id);
    }

    public async Task<IEnumerable<UserTask>> GetByUserIdAsync(int userId)
    {
        return await context.UserTasks
            .Include(ut => ut.Category)
            .Where(ut => ut.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateAsync(UserTask userTask)
    {
        context.UserTasks.Update(userTask);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var userTask = await context.UserTasks.FindAsync(id);

        if (userTask == null) return;

        context.UserTasks.Remove(userTask);
        await context.SaveChangesAsync();
    }

}