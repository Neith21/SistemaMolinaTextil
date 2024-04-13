using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MolinaTextileSystem.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "El nombre del empleado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del empleado no puede tener más de 50 caracteres.")]
        [DisplayName("Nombre del empleado")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "El apellido del empleado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido del empleado no puede tener más de 50 caracteres.")]
        [DisplayName("Apellido del empleado")]
        public string EmployeeLastname { get; set; }

        [Required(ErrorMessage = "La dirección del empleado es obligatoria.")]
        [StringLength(255, ErrorMessage = "La dirección del empleado no puede tener más de 255 caracteres.")]
        [DisplayName("Dirección del empleado")]
        public string EmployeeAddress { get; set; }

        [Required(ErrorMessage = "El teléfono del empleado es obligatorio.")]
        [StringLength(30, ErrorMessage = "El teléfono del empleado no puede tener más de 30 caracteres.")]
        [DisplayName("Teléfono del empleado")]
        public string EmployeePhone { get; set; }

        [Required(ErrorMessage = "El correo electrónico del empleado es obligatorio.")]
        [StringLength(100, ErrorMessage = "El correo electrónico del empleado no puede tener más de 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida.")]
        [DisplayName("Correo electrónico del empleado")]
        public string EmployeeEmail { get; set; }
    }
}
