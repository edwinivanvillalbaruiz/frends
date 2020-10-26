﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace VirtualFrends
{
    public class Archivo
    {
        // CONSTRUCTOR
        public Archivo()
        {
            this.Id = Guid.NewGuid();
            this.Creado = DateTime.Now;
        }

        // PROPIEDADES PÚBLICAS
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime Creado { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(4)]
        [Required]
        public string Extension { get; set; }

        [Required]
        public int Descargas { get; set; }

        // PROPIEDADES PRIVADAS
        public string PathRelativo
        {
            get
            {
                return ConfigurationManager.AppSettings["PathArchivos"] +
                                            this.Id.ToString() + "." +
                                            this.Extension;
            }
        }

        public string PathCompleto
        {
            get
            {
                var _PathAplicacion = HttpContext.Current.Request.PhysicalApplicationPath;
                return Path.Combine(_PathAplicacion, this.PathRelativo);
            }
        }

        // MÉTODOS PÚBLICOS
        public void SubirArchivo(byte[] archivo)
        {
            File.WriteAllBytes(this.PathCompleto, archivo);
        }

        public byte[] DescargarArchivo()
        {
            return File.ReadAllBytes(this.PathCompleto);
        }

        public void EliminarArchivo()
        {
            File.Delete(this.PathCompleto);
        }
    }
}