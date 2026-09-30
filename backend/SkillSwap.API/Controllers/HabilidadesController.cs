using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwap.API.Data;
using SkillSwap.API.Models;

namespace SkillSwap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabilidadesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HabilidadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/habilidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habilidad>>> GetHabilidades()
        {
            return await _context.Habilidades.ToListAsync();
        }

        // GET: api/habilidades/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Habilidad>> GetHabilidad(int id)
        {
            var habilidad = await _context.Habilidades.FindAsync(id);

            if (habilidad == null)
            {
                return NotFound();
            }

            return habilidad;
        }

        // POST: api/habilidades
        [HttpPost]
        public async Task<ActionResult<Habilidad>> CreateHabilidad(Habilidad habilidad)
        {
            _context.Habilidades.Add(habilidad);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
            nameof(GetHabilidad),
            new { id = habilidad.Id },
            habilidad
            );
        }

        // PUT: api/habilidades/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabilidad(
        int id,
        Habilidad habilidad
        )
        {
            if (id != habilidad.Id)
            {
                return BadRequest();
            }

            _context.Entry(habilidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/habilidades/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidad(int id)
        {
            var habilidad = await _context.Habilidades.FindAsync(id);

            if (habilidad == null)
            {
                return NotFound();
            }

            _context.Habilidades.Remove(habilidad);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}