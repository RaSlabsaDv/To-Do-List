using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserTaskController(IUserTaskService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserTaskDto dto)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        await service.CreateAsync(dto, userId);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await service.GetByIdAsync(id);
        return Ok(task);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetByUserId()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var tasks = await service.GetByUserIdAsync(userId);
        return Ok(tasks);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserTaskDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        await service.CompleteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}/uncomplete")]
    public async Task<IActionResult> Uncomplete(int id)
    {
        await service.UncompleteAsync(id);
        return NoContent();
    }
}