public interface IUserTaskService
{
    Task CreateAsync(CreateUserTaskDto dto, int userId);
    Task<UserTaskDto> GetByIdAsync(int id);
    Task<IEnumerable<UserTaskDto>> GetByUserIdAsync(int userId);
    Task UpdateAsync(int id, UpdateUserTaskDto updateDto);
    Task DeleteAsync(int id);
    Task CompleteAsync(int id);
    Task UncompleteAsync(int id);
}