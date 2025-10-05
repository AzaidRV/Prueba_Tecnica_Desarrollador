using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica_JavierAzaid.Business.Interfaces;
using PruebaTecnica_JavierAzaid.Data;
using PruebaTecnica_JavierAzaid.Dto.Request;
using PruebaTecnica_JavierAzaid.Models;

namespace PruebaTecnica_JavierAzaid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IBRProducts _productsBusiness;

        public ProductsController(IBRProducts productsBusiness) => (_productsBusiness) = (productsBusiness);

        /// <summary>
        /// Obtiene una lista paginada de productos.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("GetProductsPagination")]
        public async Task<IActionResult> GetAllProducts([FromQuery] RequestProductsPagination request)
        {
            var result = await _productsBusiness.GetProductsPagination(request);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene productos por nombre o código (SKU).
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("GetProductsByNameOrCode")]
        public async Task<IActionResult> GetProductsByNameOrCode([FromQuery] RequestSearchProductDto request)
        {
            var result = await _productsBusiness.GetProductsByNameOrCode(request);
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo producto en el sistema.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] RequestCreateProductDto request)
        {
            var result = await _productsBusiness.CreateProductAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] RequestUpdateProductDto request)
        {
            var result = await _productsBusiness.UpdateProductAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] RequestDeleteProductDto request)
        {
            var result = await _productsBusiness.DeleteProductAsync(request);
            return Ok(result);
        }

    }
}
