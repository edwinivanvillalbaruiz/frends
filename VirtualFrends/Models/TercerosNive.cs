using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualFrends.Models
{
    public class TercerosNive
    {
        [Key]
        public int IdTerceros { get; set; }
        public int Cont { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public int IdTipoDocumento { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Celular { get; set; }
        public int IdTransaccion { get; set; }
        public string Nivel { get; set; }
        public string NombreC { get; set; }
    }
}