public class UserTaskService
(
    IUserTaskRepository repository, 
    ICategoryRepository categoryRepository
) : IUserTaskService
{
    public async Task CreateAsync(CreateUserTaskDto dto, int userId)
    {
        Category? category = null;

        if(dto.CategoryId != null){
             category = await categoryRepository.GetByIdAsync(dto.CategoryId.Value)
                ?? throw new NotFoundException($"Category {dto.CategoryId} not found");
        }

        var task = new UserTask
        (
            userId,
            dto.Label,
            dto.Deadline,
            dto.Reminder,
            dto.RepeatState,
            category,
            dto.Description
        );

        await repository.CreateAsync(task);
    }

    public async Task<UserTaskDto> GetByIdAsync(int id)
    {
        var task = await repository.GetByIdAsync(id);

        if(task == null)
            throw new NotFoundException($"Task {id} not found");

        return new UserTaskDto
        (
            task.Id,
            task.Label,
            task.Description,
            task.Deadline,
            task.Reminder,
            task.RepeatState,
            task.IsCompleted,
            task.Category != null ? new CategoryDto(task.Category.Id, task.Category.Name) : null
        );
    }

    public async Task<IEnumerable<UserTaskDto>> GetByUserIdAsync(int userId)
    {
        var tasks = await repository.GetByUserIdAsync(userId);
        var response = tasks.Select(t => new UserTaskDto(
            t.Id,
            t.Label,
            t.Description,
            t.Deadline,
            t.Reminder,
            t.RepeatState,
            t.IsCompleted,
            t.Category != null ? new CategoryDto(t.Category.Id, t.Category.Name) : null
        ));

        return response;
    }

    public async Task UpdateAsync(int id, UpdateUserTaskDto updateDto)
    {
        var task = await repository.GetByIdAsync(id);

        if(task == null)
            throw new NotFoundException($"Task {id} not found");
        
        if(updateDto.Label != null)
            task.SetLabel(updateDto.Label);
        
        if(updateDto.Description != null)
            task.SetDescription(updateDto.Description);

        if(updateDto.Deadline != null)
            task.SetDeadline(updateDto.Deadline);

        if(updateDto.Reminder != null)
            task.SetReminder(updateDto.Reminder);

        if(updateDto.RepeatState != null)
            task.SetRepeatState(updateDto.RepeatState.Value);

        if(updateDto.CategoryId != null)
        {
            var category = await categoryRepository.GetByIdAsync(updateDto.CategoryId.Value)
                ?? throw new NotFoundException($"Category {updateDto.CategoryId} not found");

            task.SetCategory(category);
        }

        await repository.UpdateAsync(task);
            
    }

    public async Task DeleteAsync(int id)
    {
        var task = await repository.GetByIdAsync(id);

        if(task == null)
            throw new NotFoundException($"Task {id} not found");

        await repository.DeleteAsync(id);
    }

    public async Task CompleteAsync(int id)
    {
        var task = await repository.GetByIdAsync(id);

        if(task == null)
            throw new NotFoundException($"Task {id} not found");

        task.Complete();    

        await repository.UpdateAsync(task);
    }

    public async Task UncompleteAsync(int id)
    {
        var task = await repository.GetByIdAsync(id);

        if(task == null)
            throw new NotFoundException($"Task {id} not found");

        task.Uncomplete();    

        await repository.UpdateAsync(task);
    }
}