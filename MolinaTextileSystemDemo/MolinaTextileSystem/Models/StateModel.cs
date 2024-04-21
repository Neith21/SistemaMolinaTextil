using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MolinaTextileSystem.Models
{
    public class StateModel
    {
        public int StateId { get; set; }

        [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre del estado no puede tener más de 30 caracteres.")]
        [DisplayName("Nombre del Estado")]
        public string StateName { get; set; }

        [StringLength(255, ErrorMessage = "La descripción del estado no puede tener más de 255 caracteres.")]
        [DisplayName("Descripción del Estado")]
        public string StateDescription { get; set; }
    }
}
