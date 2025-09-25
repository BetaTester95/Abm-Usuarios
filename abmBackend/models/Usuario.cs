using System.ComponentModel.DataAnnotations;

namespace abm.models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? email { get; set; }
        public int dni { get; set; }
    }
}
