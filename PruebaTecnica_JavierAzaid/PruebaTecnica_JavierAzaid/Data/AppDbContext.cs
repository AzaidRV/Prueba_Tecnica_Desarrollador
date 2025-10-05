using Microsoft.EntityFrameworkCore;
using PruebaTecnica_JavierAzaid.Models;

namespace PruebaTecnica_JavierAzaid.Data
{
    /// <summary>
    /// Contexto de base de datos para la aplicación.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración del contexto.
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Conjunto de entidades que representan los productos en la base de datos.
        /// </summary>
        public DbSet<ProductsModel> Products { get; set; }
    }
}
