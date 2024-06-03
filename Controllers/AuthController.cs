using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService db = new AuthService();

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            bool success = await db.Registrar(usuario);
            if (!success)
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            return Ok(new{
                Id = usuario.Id,
                Nombre = usuario.nombre
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            bool success = await db.Login(usuario);
            if (!success)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            return Ok(new
            {
                Id = usuario.Id,
                Nombre = usuario.nombre
            });
        }
    }
}