namespace SkillSwap.API.DTOs
{
    public class RegisterDto
    {
        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Carrera { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}