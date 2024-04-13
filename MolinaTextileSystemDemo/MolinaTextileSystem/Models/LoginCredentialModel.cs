using System.ComponentModel.DataAnnotations;

namespace MolinaTextileSystem.Models
{
    public class LoginCredentialModel
    {
        public int LoginCredentialId { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre de usuario no puede tener más de 20 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(255, ErrorMessage = "La contraseña no puede tener más de 255 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Se requiere el ID del empleado.")]
        public int EmployeeId { get; set; }

        public EmployeeModel Employee { get; set; }
    }
}
