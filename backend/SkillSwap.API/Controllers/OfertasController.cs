using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwap.API.Data;
using SkillSwap.API.Models;

namespace SkillSwap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfertasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OfertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oferta>>> GetOfertas()
        {
            return await _context.Ofertas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> GetOferta(int id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null)
            {
                return NotFound();
            }

            return oferta;
        }

        [HttpPost]
        public async Task<ActionResult<Oferta>> CreateOferta(Oferta oferta)
        {
            _context.Ofertas.Add(oferta);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
            nameof(GetOferta),
            new { id = oferta.Id },
            oferta
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOferta(
        int id,
        Oferta oferta
        )
        {
            if (id != oferta.Id)
            {
                return BadRequest();
            }

            _context.Entry(oferta).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOferta(int id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null)
            {
                return NotFound();
            }

            _context.Ofertas.Remove(oferta);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}