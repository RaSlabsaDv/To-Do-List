public interface IUserTaskRepository
{
    Task CreateAsync(UserTask userTask);
    Task<UserTask> GetByIdAsync(int id);
    Task<IEnumerable<UserTask>> GetByUserIdAsync(int userId);
    Task UpdateAsync(UserTask userTask);
    Task DeleteAsync(int id); 
}