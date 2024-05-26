using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MolinaTextileSystem.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [DisplayName("Nombre de producto")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Tamaño de producto")]
        public string ProductSize { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Patrón")]
        public int PatternId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Estado")]
        public int StateId { get; set; }

        public PatternModel Pattern { get; set; }

        public StateModel States { get; set; }
    }
}
