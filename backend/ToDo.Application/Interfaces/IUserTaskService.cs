public interface IUserTaskService
{
    Task CreateAsync(CreateUserTaskDto dto);
    Task<UserTaskDto?> GetByIdAsync(int id);
    Task<IEnumerable<UserTaskDto>> GetByUserIdAsync(int userId);
    Task UpdateAsync(UpdateUserTaskDto updateDto);
    Task DeleteAsync(int id);
}