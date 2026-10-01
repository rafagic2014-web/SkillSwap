namespace SkillSwap.API.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Carrera { get; set; } = string.Empty;
    }
}