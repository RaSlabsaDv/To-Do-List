public class UserService(IUserRepository repository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task CreateAsync(CreateUserDto dto)
    {
        var existing = await repository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new ConflictException("User with this email already exists");

        var hash = passwordHasher.Hash(dto.Password);
        var user = new User(dto.Name, hash, dto.Email);

        await repository.CreateAsync(user);
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var user = await repository.GetByEmailAsync(email);

        if(user == null)
            throw new NotFoundException($"User {email} not found");

        return new UserDto(user.Id, user.Name, user.Email);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await repository.GetByIdAsync(id);

        if(user == null)
            throw new NotFoundException($"User {id} not found");

        return new UserDto(user.Id, user.Name, user.Email);
    }

    public async Task UpdateAsync(int id, UpdateUserDto updateDto)
    {
        var user = await repository.GetByIdAsync(id);

        if(user == null)
            throw new NotFoundException($"User {id} not found");

        if(updateDto.Name != null)
            user.SetName(updateDto.Name);

        if(updateDto.Password != null){
            var hash = passwordHasher.Hash(updateDto.Password);
            user.SetPasswordHash(hash);
        }

        if(updateDto.Email != null)
            user.SetEmail(updateDto.Email);

        await repository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await repository.GetByIdAsync(id);

        if(user == null)
            throw new NotFoundException($"User {id} not found");
        
        await repository.DeleteAsync(id);
    }

    public async Task<User?> LoginAsync(LoginDto dto)
    {
        var user = await repository.GetByEmailAsync(dto.Email);

        if(user == null) return null;

        var isValid = passwordHasher.Verify(dto.Password, user.PasswordHash);

        return isValid ? user : null;
    }
}