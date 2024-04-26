using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MolinaTextileSystem.Models
{
	public class CategoryModel
	{
		[Key]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
		[StringLength(50, ErrorMessage = "El nombre de la categoría no puede tener más de 50 caracteres.")]
		[DisplayName("Nombre de la categoría")]
		public string CategoryName { get; set; }

		[DisplayName("Descripción de la categoría")]
		[StringLength(255, ErrorMessage = "La descripción de la categoría no puede tener más de 255 caracteres.")]
		public string CategoryDescription { get; set; }
	}
}
