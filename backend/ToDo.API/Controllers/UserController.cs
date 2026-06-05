using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> GetById()
    {   
        var id = int.Parse(User.FindFirst("userId")!.Value);
        var user = await service.GetByIdAsync(id);
        return Ok(user);
    }

    [HttpPut()]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
    {
        var id = int.Parse(User.FindFirst("userId")!.Value);
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete()
    {
        var id = int.Parse(User.FindFirst("userId")!.Value);
        await service.DeleteAsync(id);
        return NoContent();
    }
}