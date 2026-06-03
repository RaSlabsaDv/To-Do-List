public interface ICategoryRepository
{
    Task CreateAsync(Category category);
    Task<Category?> GetByIdAsync(int id);
    Task<IEnumerable<Category>> GetByUserIdAsync(int userId);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
}