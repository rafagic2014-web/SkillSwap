using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwap.API.Data;
using SkillSwap.API.Models;
using SkillSwap.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace SkillSwap.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
            .Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Correo = u.Correo,
                Carrera = u.Carrera
            })
            .ToListAsync();

            return usuarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Carrera = usuario.Carrera
            };
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario(
        CreateUsuarioDto dto
            )
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Carrera = dto.Carrera
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            var usuarioDto = new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Carrera = usuario.Carrera
            };

            return CreatedAtAction(
            nameof(GetUsuario),
            new { id = usuario.Id },
            usuarioDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(
            int id,
            UpdateUsuarioDto dto
            )
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nombre = dto.Nombre;
            usuario.Correo = dto.Correo;
            usuario.Carrera = dto.Carrera;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}