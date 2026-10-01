namespace SkillSwap.API.DTOs
{
    public class CreateUsuarioDto
    {
        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Carrera { get; set; } = string.Empty;
    }
}