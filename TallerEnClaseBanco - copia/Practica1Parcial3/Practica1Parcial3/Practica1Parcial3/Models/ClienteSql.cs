using System.ComponentModel.DataAnnotations;
namespace Practica1Parcial3.Models

{
    public class ClienteSql
    {
        public int Codigo { get; set; }

        [Required]
        [StringLength(10)]
        public string Cedula { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public DateTime? fechaNacimiento { get; set; }  // Cambio a DateTime? para manejar valores nulos

        public string Mail { get; set; }

        [MaxLength(10, ErrorMessage = "El numero del telefono, debe ser de 10 digitos")]
        public string Telefono { get; set; }

        public string Direccion { get; set; }  // Corrección del error tipográfico

        public bool Estado { get; set; }

    }
}
