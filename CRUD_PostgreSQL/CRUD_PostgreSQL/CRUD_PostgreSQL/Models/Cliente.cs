using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD_PostgreSQL.Models
{
    public class Cliente
    {
        public int Codigo { get; set; }

        [Required]
        [StringLength(10)]
        public string Cedula { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? FechaNacimiento { get; set; }

        public string Mail { get; set; }

        [MaxLength(10, ErrorMessage = "El numero del telefono, debe ser de 10 digitos")]
        public string Telefono { get; set; }

        public string Direccion { get; set; }  // Corrección del error tipográfico

        public bool Estado { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El saldo no puede ser negativo.")]
        public decimal Saldo { get; set; }


        [StringLength(100, ErrorMessage = "La provincia debe tener un máximo de 100 caracteres.")]
        [Required(ErrorMessage = "La provincia es obligatoria")]
        public string Provincia { get; set; }  // Nuevo campo


    }
}
