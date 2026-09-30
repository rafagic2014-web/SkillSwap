namespace SkillSwap.API.Models
{
    public class Intercambio
    {
        public int Id { get; set; }

        public int UsuarioOfertaId { get; set; }

        public int UsuarioSolicitudId { get; set; }
    }
}