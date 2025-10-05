namespace PruebaTecnica_JavierAzaid.Dto.Request
{
    /// <summary>
    /// DTO para la búsqueda de productos.
    /// </summary>
    public class RequestSearchProductDto
    {
        /// <summary>
        /// Término de búsqueda (nombre o código SKU).
        /// </summary>
        public string SearchTerm { get; set; }
    }
}
