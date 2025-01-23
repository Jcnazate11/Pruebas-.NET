using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationLab.Models
{
    public class Cliente
    {
        public int Codigo { get; set; }

        [Required]
        [StringLength(10)]
        [CedulaEcuatoriana]
        public string Cedula { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string Mail { get; set; }

        public string Direccion { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El saldo no puede ser negativo")]
        public decimal SaldoCuenta { get; set; } = 0;

        [Required]
        [StringLength(2, ErrorMessage = "La provincia debe tener un máximo de 2 caracteres.")]
        public string Provincia { get; set; } // Nueva propiedad Provincia

        public bool Estado { get; set; }

        // Propiedad para mostrar la cédula generada (puede ser opcional si no se asigna directamente)
        [StringLength(10)]
        public string CedulaGenerada { get; set; }
    }


    public class CedulaEcuatorianaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cliente = (Cliente)validationContext.ObjectInstance;
            var cedula = value as string;

            if (string.IsNullOrEmpty(cedula) || cedula.Length != 10)
            {
                return new ValidationResult("La cédula debe tener 10 dígitos.");
            }

            // Verificar los dos primeros dígitos (provincia)
            int provincia;
            if (!int.TryParse(cedula.Substring(0, 2), out provincia) || provincia < 1 || provincia > 24)
            {
                return new ValidationResult("Los dos primeros dígitos de la cédula son inválidos.");
            }

            // Verificar que el tercer dígito sea menor a 6
            int tercerDigito = int.Parse(cedula.Substring(2, 1));
            if (tercerDigito >= 6)
            {
                return new ValidationResult("El tercer dígito de la cédula es inválido.");
            }

            // Validar el dígito verificador usando el Módulo 10
            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;

            for (int i = 0; i < 9; i++)
            {
                int valor = int.Parse(cedula[i].ToString()) * coeficientes[i];
                if (valor >= 10)
                    valor -= 9;
                suma += valor;
            }

            int digitoVerificador = (10 - (suma % 10)) % 10;
            if (digitoVerificador != int.Parse(cedula.Substring(9, 1)))
            {
                return new ValidationResult("La cédula es inválida. El dígito verificador no coincide.");
            }

            return ValidationResult.Success;
        }
    }
}
