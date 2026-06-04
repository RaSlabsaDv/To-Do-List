public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category(dto.Name, dto.UserId);
        await repository.CreateAsync(category);
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);
        
        if (category == null)
            throw new NotFoundException($"Category {id} not found");
        
        return new CategoryDto(category.Id, category.Name);
    }

    public async Task<IEnumerable<CategoryDto>> GetByUserIdAsync(int userId)
    {
        var categories = await repository.GetByUserIdAsync(userId);
        var response = categories.Select(c => new CategoryDto(c.Id, c.Name));
        
        return response;
    }

    public async Task UpdateAsync(UpdateCategoryDto updateDto)
    {
        var category = await repository.GetByIdAsync(updateDto.Id);

        if(category == null)
            throw new NotFoundException($"Category {updateDto.Id} not found");

        category.SetName(updateDto.Name);

        await repository.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);

        if (category == null)
           throw new NotFoundException($"Category {id} not found"); 

        await repository.DeleteAsync(id);
    }
}