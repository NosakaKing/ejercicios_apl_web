using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaInventario.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre del Producto")]
        [MinLength(3)]
        public string NombreProducto { get; set; }
        [Display(Name = "Presentación del Producto")]
        [MinLength(3)]
        public string Presentacion { get; set; }
        [Display(Name = "Codigo de Barras")]
        [MinLength(5)]
        public string CodigoBarras { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public CategoryModel? Categoria { get; set; }
    }
}
