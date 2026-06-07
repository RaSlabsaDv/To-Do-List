using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService, IJwtService jwtService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
    {
        await userService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await userService.LoginAsync(dto);
        if(user == null) return Unauthorized();

        var token = jwtService.GenerateToken(user);

        Response.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true
        });

        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token"); 
        return Ok();
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var email = User.FindFirst(ClaimTypes.Email)!.Value;
        return Ok(new { userId, email});
    }
}