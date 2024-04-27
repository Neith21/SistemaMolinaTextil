using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MolinaTextileSystem.Models
{
	public class PatternDetailModel
	{
		[Key]
		public int PatternDetailId { get; set; }

		[Required(ErrorMessage = "La cantidad de materia prima es requerida.")]
		[DisplayName("Cantidad de materia prima")]
		[Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
		public decimal RawMaterialQuantity { get; set; }

		[Required(ErrorMessage = "Se requiere seleccionar un material.")]
		[DisplayName("ID del material")]
		public int RawMaterialId { get; set; }

		[DisplayName("ID del patrón")]
		public int? PatternId { get; set; }

		public RawMaterialsModel RawMaterial { get; set; }

		public PatternModel Pattern { get; set; }
	}
}
