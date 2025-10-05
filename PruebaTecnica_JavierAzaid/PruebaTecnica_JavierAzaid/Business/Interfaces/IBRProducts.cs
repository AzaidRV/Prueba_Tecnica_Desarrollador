using PruebaTecnica_JavierAzaid.Dto.Request;
using PruebaTecnica_JavierAzaid.Dto;
using PruebaTecnica_JavierAzaid.Models;

namespace PruebaTecnica_JavierAzaid.Business.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones relacionadas con la gestión de productos.
    /// </summary>
    public interface IBRProducts
    {
        /// <summary>
        /// Obtiene una lista paginada de productos.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GenericResponse<IEnumerable<ProductsModel>>> GetProductsPagination(RequestProductsPagination request);
        /// <summary>
        /// Obtiene productos por nombre o código (SKU).
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GenericResponse<IEnumerable<ProductsModel>>> GetProductsByNameOrCode(RequestSearchProductDto request);
        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GenericResponse<bool>> DeleteProductAsync(RequestDeleteProductDto request);
        /// <summary>
        /// Crea un nuevo producto en el sistema.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GenericResponse<ProductsModel>> CreateProductAsync(RequestCreateProductDto request);
        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GenericResponse<ProductsModel>> UpdateProductAsync(RequestUpdateProductDto request);
    }
}
