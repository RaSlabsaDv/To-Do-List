public interface IUserService
{
    Task CreateAsync(CreateUserDto dto);
    Task<UserDto> GetByIdAsync(int id);
    Task<UserDto> GetByEmailAsync(string email);
    Task UpdateAsync(int id, UpdateUserDto updateDto);
    Task DeleteAsync(int id);
}