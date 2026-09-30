using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwap.API.Data;
using SkillSwap.API.Models;

namespace SkillSwap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntercambiosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IntercambiosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intercambio>>> GetIntercambios()
        {
            return await _context.Intercambios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Intercambio>> GetIntercambio(int id)
        {
            var intercambio = await _context.Intercambios.FindAsync(id);

            if (intercambio == null)
            {
                return NotFound();
            }

            return intercambio;
        }

        [HttpPost]
        public async Task<ActionResult<Intercambio>> CreateIntercambio(
        Intercambio intercambio
        )
        {
            _context.Intercambios.Add(intercambio);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
            nameof(GetIntercambio),
            new { id = intercambio.Id },
            intercambio
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIntercambio(
        int id,
        Intercambio intercambio
        )
        {
            if (id != intercambio.Id)
            {
                return BadRequest();
            }

            _context.Entry(intercambio).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntercambio(int id)
        {
            var intercambio = await _context.Intercambios.FindAsync(id);

            if (intercambio == null)
            {
                return NotFound();
            }

            _context.Intercambios.Remove(intercambio);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}