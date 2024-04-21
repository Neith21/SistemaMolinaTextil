using Humanizer;
using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MolinaTextileSystem.Models
{
    public class CustomerOrderDetailModel
    {
        [Required(ErrorMessage = "La Detalle de Orden es obligatoria.")]
        [DisplayName("Id Detalle de Orden")]
        public int CustomerOrderDetailId { get; set; }

        [Required(ErrorMessage = "El Precio Unitario es obligatoria.")]
        [DisplayName("Precio Unitario")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "La Cantidad de Orden es obligatoria.")]
        [DisplayName("Cantidad de Orden")]
        public int CustomerOrderDetailQuantity { get; set; }

        [Required(ErrorMessage = "El Id de la Orden es obligatoria.")]
        [DisplayName("Id de la Orden")]
        public int CustomerOrderId { get; set; }

        [Required(ErrorMessage = "El Id del Producto es obligatoria.")]
        [DisplayName("Id del Producto")]
        public int ProductId { get; set; }

        public ProductModel? Producto { get; set; }
    }
}
