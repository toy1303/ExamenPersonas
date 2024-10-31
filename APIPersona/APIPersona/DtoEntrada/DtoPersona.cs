using System.ComponentModel.DataAnnotations;

namespace APIPersona.DtoEntrada
{
    public class DtoPersona
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
