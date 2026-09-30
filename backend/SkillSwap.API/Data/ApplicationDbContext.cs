using Microsoft.EntityFrameworkCore;
using SkillSwap.API.Models;
namespace SkillSwap.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Habilidad> Habilidades { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Intercambio> Intercambios { get; set; }
    }
}