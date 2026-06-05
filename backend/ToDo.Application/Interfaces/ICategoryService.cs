public interface ICategoryService
{
    Task CreateAsync(CreateCategoryDto dto, int userId);
    Task<CategoryDto> GetByIdAsync(int id);
    Task<IEnumerable<CategoryDto>> GetByUserIdAsync(int userId);
    Task UpdateAsync(int id, UpdateCategoryDto updateDto);
    Task DeleteAsync(int id);
}