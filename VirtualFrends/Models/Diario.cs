using DevExtreme.AspNet.Mvc.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace VirtualFrends.Models
{
    public class ViewDiario
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public int IdTransaccion { get; set; }
        public string Operacion { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 4)]
        [Display(Name = "Concepto de Operacion")]
        public string Concepto { get; set; }
        public string Codigo { get; set; }
        public decimal Valor { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }
        [EmailAddress]
        public string EmailOperacion { get; set; }
    }

    public class Diario
    {
        [Key]
        public int Id { get; set; }
        public int IdTransaccion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public int IdOperacion { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Debito { get; set; }
        public decimal Credito { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 4)]
        public string Concepto { get; set; }
        public int Estado { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Codigo { get; set; }
        [EmailAddress]
        public string EmailOperacion { get; set; }
    }

    public class Transferencia
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email de Destino")]
        public string EmailBeneficario { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Cantidad de Transferencia")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Cantidad { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 4)]
        [Display(Name = "Concepto de la Operacion")]
        public string Concepto { get; set; }

        public decimal Saldo { get; set; }


    }

    public class Compras
    {
        [Key]
       public int Id { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 4)]
        [Display(Name = "Concepto de Operacion")]
        public string Concepto { get; set; }
       
        public string Entidad { get; set; }
       
        public Guid Soporte { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public string  Email { get; set; }
        public int Estado { get; set; }
       //public Image MiImagen { get; set; }
    }

    public class Ventas
    {
       
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 4)]
        [Display(Name = "Concepto de Operacion")]
        public string Concepto { get; set; }
        [Required]
        public string Entidad { get; set; }
        [Required]
        public string TipoCuenta { get; set; }
        [Required]
        public string NumeroCuenta { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public string Email { get; set; }
        public int Estado { get; set; }
        
    }
    public class Bancos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    
    
    public class TipoCuenta
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
    }
}
