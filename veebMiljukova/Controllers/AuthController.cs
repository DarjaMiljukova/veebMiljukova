using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebMiljukova.Models;

namespace veebMiljukova.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Kasutaja newUser)
        {
            _context.Kasutajad.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok("Пользователь зарегистрирован");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.Authenticate(model.Kasutajanimi, model.Parool);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user); // Генерация токена
            return Ok(new { token });
        }

    }
}
