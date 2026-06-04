public interface ICategoryService
{
    Task CreateAsync(CreateCategoryDto dto);
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<IEnumerable<CategoryDto>> GetByUserIdAsync(int userId);
    Task UpdateAsync(UpdateCategoryDto updateDto);
    Task DeleteAsync(int id);
}