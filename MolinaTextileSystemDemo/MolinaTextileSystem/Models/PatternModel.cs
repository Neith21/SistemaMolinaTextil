using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MolinaTextileSystem.Models
{
    public class PatternModel
    {
        public int PatternId { get; set; }

        [Required(ErrorMessage = "El nombre del patrón es obligatorio")]
        [DisplayName("Nombre de patron")]
        public string PatternName { get; set; }

        [Required(ErrorMessage = "La descripción del patron es obligatoria")]
        [DisplayName("Descripción")]
        public string PatternDescription { get; set; }
    }
}
