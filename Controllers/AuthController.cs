using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService) => _tokenService = tokenService;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        // Replace with real user validation
        if (req.Username == "admin" && req.Password == "password")
        {
            var token = _tokenService.CreateToken(1, req.Username);
            return Ok(new { token });
        }

        return Unauthorized(new { error = "Invalid credentials" });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
