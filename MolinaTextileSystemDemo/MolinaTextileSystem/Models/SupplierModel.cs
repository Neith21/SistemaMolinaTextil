using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MolinaTextileSystem.Models
{
    public class SupplierModel
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del proveedor no puede tener más de 50 caracteres.")]
        [DisplayName("Nombre del proveedor")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "El nombre del manager es obligatorio.")]
        [StringLength(50, ErrorMessage = "El manager no puede tener más de 50 caracteres.")]
        [DisplayName("Manager")]
        public string Manager { get; set; }

        [Required(ErrorMessage = "El mumero de teléfono es obligatorio.")]
        [StringLength(30, ErrorMessage = "El mumero de teléfono no puede tener más de 30 caracteres.")]
        [DisplayName("Teléfono del proveedor")]
        public string SupplierPhone { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
        [DisplayName("Correo electónico del proveedor")]
        public string SupplierEmail { get; set; }
    }
}
