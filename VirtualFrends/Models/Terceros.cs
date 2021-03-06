﻿using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualFrends.Models
{
    public class Terceros
    {
        [Key]
        public int IdTerceros { get; set; }
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
    }
}