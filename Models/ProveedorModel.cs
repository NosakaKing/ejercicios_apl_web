using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SistemaInventario.Models
{
    public class ProveedorModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre del Proveedor")]
        [Required(ErrorMessage = "El nombre del proveedor es requerido")]
        [MinLength(3, ErrorMessage = "El nombre del proveedor debe tener al menos 3 caracteres")]
        public string NombreProveedor { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La dirección es requerida")]
        [MinLength(3, ErrorMessage = "La dirección debe tener al menos 3 caracteres")]
        public string Direccion { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El teléfono es requerido")]
        [MinLength(8, ErrorMessage = "El teléfono debe tener al menos 8 caracteres")]
        public string Telefono { get; set; }
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string CorreoElectronico { get; set; }
    }
}
