public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(string email);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}