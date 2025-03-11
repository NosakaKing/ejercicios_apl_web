using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.Models;  

namespace SistemaInventario.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductModel> Productos { get; set; }
        public DbSet<CategoryModel> Categoria { get; set; }
        public DbSet<StockModel> Stock { get; set; }
        public DbSet<ProveedorModel> Proveedor { get; set; }
    }
}
