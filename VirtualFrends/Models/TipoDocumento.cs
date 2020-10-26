using System.ComponentModel.DataAnnotations;

namespace VirtualFrends.Models
{
    public class TipoDocumento
    {
        [Key]
        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; }
    }
}