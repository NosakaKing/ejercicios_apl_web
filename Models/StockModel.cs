using System.ComponentModel.DataAnnotations;

namespace SistemaInventario.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "La fecha de fabricación es requerida")]
        [DataType(DataType.Date)]
        public DateOnly FechaFabricacion { get; set; }
        [Required(ErrorMessage = "La fecha de vencimiento es requerida")]
        [DataType(DataType.Date)]
        public DateOnly FechaVencimiento { get; set; }
        [Required(ErrorMessage = "La fecha de ingreso es requerida")]
        [DataType(DataType.Date)]
        public DateOnly FechaIngreso { get; set; }

        public int ProductId { get; set; }
        public ProductModel? Product { get; set; }
        public int ProveedorId { get; set; }
        public ProveedorModel? Proveedor { get; set; }
    }
}
