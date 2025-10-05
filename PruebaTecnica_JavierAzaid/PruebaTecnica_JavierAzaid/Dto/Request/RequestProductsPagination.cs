namespace PruebaTecnica_JavierAzaid.Dto.Request
{
    /// <summary>
    /// DTO para la paginación de productos.
    /// </summary>
    public class RequestProductsPagination
    {
        /// <summary>
        /// Número de página.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Tamaño de página (número de productos por página).
        /// </summary>
        public int PageSize { get; set; }
    }
}
