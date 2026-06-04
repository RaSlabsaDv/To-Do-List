using Microsoft.EntityFrameworkCore;

public class CategoryRepository(ToDoContext context) : ICategoryRepository
{
    public async Task CreateAsync(Category category)
    {
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetByUserIdAsync(int userId)
    {
        return await context.Categories
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category == null) return;

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }
}