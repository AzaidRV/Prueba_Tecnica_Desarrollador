using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica_JavierAzaid.Dto.Request
{
    /// <summary>
    /// DTO para la creación de un nuevo producto.
    /// </summary>
    public class RequestCreateProductDto
    {
        /// <summary>
        /// Nombre del producto.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string NameProduct { get; set; }

        /// <summary>
        /// Código SKU del producto.
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-z0-9_-]{4,20}$",ErrorMessage = "El SKU debe tener entre 4 y 20 caracteres alfanuméricos, sin espacios ni símbolos especiales.")]

        public string SKU { get; set; }

        /// <summary>
        /// Cantidad disponible del producto.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        /// <summary>
        /// Precio de venta del producto.
        /// </summary>
        [Range(0.01, double.MaxValue)]
        public decimal PriceSale { get; set; }

        /// <summary>
        /// Precio de costo del producto.
        /// </summary>
        [Range(0.0, double.MaxValue)]
        public decimal? PriceCost { get; set; }
    }
}
