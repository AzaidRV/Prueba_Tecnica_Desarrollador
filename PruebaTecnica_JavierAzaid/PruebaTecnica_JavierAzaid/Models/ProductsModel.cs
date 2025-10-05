namespace PruebaTecnica_JavierAzaid.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Modelo que representa un producto en el sistema.
    /// </summary>
    public class ProductsModel
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        [Required, StringLength(100)]
        public required string NameProduct { get; set; }

        /// <summary>
        /// Código SKU del producto.
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-z0-9_-]{4,20}$",ErrorMessage = "El SKU debe tener entre 4 y 20 caracteres alfanuméricos, sin espacios ni símbolos especiales.")]
        public required string SKU { get; set; }

        /// <summary>
        /// Cantidad disponible del producto.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Precio de venta del producto.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor a 0.")]
        public decimal PriceSale { get; set; }

        /// <summary>
        /// Precio de costo del producto.
        /// </summary>
        [Range(0.0, double.MaxValue, ErrorMessage = "El precio de costo no puede ser negativo.")]
        public decimal? PriceCost { get; set; }

        public DateTime DateModify { get; set; } = DateTime.Now;
    }
}
