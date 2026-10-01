using Microsoft.AspNetCore.Mvc;
using SkillSwap.API.Data;
using SkillSwap.API.DTOs;
using SkillSwap.API.Models;
using System.Linq;

namespace SkillSwap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Carrera = dto.Carrera,
                Password = dto.Password,
                Rol = "Estudiante"
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario registrado correctamente"
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(
            u => u.Correo == dto.Correo
            && u.Password == dto.Password
            );

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos"
                });
            }

            return Ok(new
            {
                mensaje = "Login exitoso",
                usuario = new
                {
                    usuario.Id,
                    usuario.Nombre,
                    usuario.Correo,
                    usuario.Rol
                }
            });
        }
    }
}