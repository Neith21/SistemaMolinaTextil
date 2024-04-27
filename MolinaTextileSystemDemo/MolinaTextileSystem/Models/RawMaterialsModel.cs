using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MolinaTextileSystem.Models
{
	public class RawMaterialsModel
	{
		[Key]
		public int RawMaterialId { get; set; }

		[Required(ErrorMessage = "El nombre de la materia prima es obligatorio.")]
		[StringLength(50, ErrorMessage = "El nombre de la materia prima no puede tener más de 50 caracteres.")]
		[DisplayName("Nombre de la materia prima")]
		public string RawMaterialName { get; set; }

		[DisplayName("Descripción de la materia prima")]
		[StringLength(255, ErrorMessage = "La descripción de la materia prima no puede tener más de 255 caracteres.")]
		public string RawMaterialDescription { get; set; }

		[DisplayName("Precio de compra")]
		[DataType(DataType.Currency)]
		[Range(0, double.MaxValue, ErrorMessage = "El precio de compra debe ser mayor o igual a cero.")]
		public decimal? RawMaterialPurchasePrice { get; set; }

		[Required(ErrorMessage = "La cantidad de materia prima es obligatoria.")]
		[DisplayName("Cantidad de materia prima")]
		[Range(0.01, double.MaxValue, ErrorMessage = "La cantidad de materia prima debe ser mayor que cero.")]
		public decimal RawMaterialQuantity { get; set; }

		[Required(ErrorMessage = "Se requiere seleccionar una categoría.")]
		[DisplayName("ID de la categoría")]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Se requiere seleccionar un proveedor.")]
		[DisplayName("ID del proveedor")]
		public int SupplierId { get; set; }

		public CategoryModel Category { get; set; }
		public SupplierModel Supplier { get; set; }
	}
}
