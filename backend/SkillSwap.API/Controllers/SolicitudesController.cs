using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwap.API.Data;
using SkillSwap.API.Models;

namespace SkillSwap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SolicitudesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitudes()
        {
            return await _context.Solicitudes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud>> GetSolicitud(int id)
        {
            var solicitud = await _context.Solicitudes.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            return solicitud;
        }

        [HttpPost]
        public async Task<ActionResult<Solicitud>> CreateSolicitud(Solicitud solicitud)
        {
            _context.Solicitudes.Add(solicitud);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
            nameof(GetSolicitud),
            new { id = solicitud.Id },
            solicitud
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSolicitud(
        int id,
        Solicitud solicitud
        )
        {
            if (id != solicitud.Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitud).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud(int id)
        {
            var solicitud = await _context.Solicitudes.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            _context.Solicitudes.Remove(solicitud);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}