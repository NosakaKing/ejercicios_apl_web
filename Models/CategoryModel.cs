using System.ComponentModel.DataAnnotations;

namespace SistemaInventario.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Opcional
        public string? Description { get; set; }
    }
}
