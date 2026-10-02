using Microsoft.AspNetCore.Mvc;
using SkillSwap.API.Data;
using SkillSwap.API.DTOs;
using SkillSwap.API.Models;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SkillSwap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(
            ApplicationDbContext context,
            IConfiguration configuration
            )
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Carrera = dto.Carrera,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
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
            );

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos"
                });
            }

            bool passwordValida =
                BCrypt.Net.BCrypt.Verify(
                dto.Password,
                usuario.Password
                );

            if (!passwordValida)
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos"
                });
            }

            var token = GenerateToken(usuario);

            return Ok(new
            {
                mensaje = "Login exitoso",
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.Nombre,
                    usuario.Correo,
                    usuario.Rol
                }
            });
        }
    
    private string GenerateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
            new Claim(
            ClaimTypes.NameIdentifier,
            usuario.Id.ToString()
            ),

            new Claim(
            ClaimTypes.Name,
            usuario.Nombre
            ),

            new Claim(
            ClaimTypes.Email,
            usuario.Correo
            ),

            new Claim(
            ClaimTypes.Role,
            usuario.Rol
            )
            };

            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"]!
            )
            );

            var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
            .WriteToken(token);
        }
    }
}