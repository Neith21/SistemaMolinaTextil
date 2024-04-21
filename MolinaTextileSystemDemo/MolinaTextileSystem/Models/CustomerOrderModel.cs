using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MolinaTextileSystem.Models
{
    public class CustomerOrderModel
    {
        public int CustomerOrderId { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DisplayName("Fecha de Creación")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Fecha de Entrega")]
        public DateTime? DeliveryDate { get; set; }

        [Required(ErrorMessage = "El monto total es obligatorio.")]
        [DisplayName("Monto Total")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "El Id del cliente es obligatorio.")]
        [DisplayName("ID del Cliente")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "El Id del empleado es obligatorio.")]
        [DisplayName("ID del Empleado")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "El Id del estado es obligatorio.")]
        [DisplayName("ID del Estado")]
        public int StateId { get; set; }

        public CustomerModel Customer { get; set; }

        public EmployeeModel Employee { get; set; }

        public StateModel State { get; set; }
    }
}
