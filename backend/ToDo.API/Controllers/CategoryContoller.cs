using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        await service.CreateAsync(dto, userId);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await service.GetByIdAsync(id);
        return Ok(category);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetByUserId()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var categories = await service.GetByUserIdAsync(userId);
        return Ok(categories);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
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
}