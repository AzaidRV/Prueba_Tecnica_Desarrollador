using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnica_JavierAzaid.Business.Interfaces;
using PruebaTecnica_JavierAzaid.Data;
using PruebaTecnica_JavierAzaid.Dto;
using PruebaTecnica_JavierAzaid.Dto.Request;
using PruebaTecnica_JavierAzaid.Extensions;
using PruebaTecnica_JavierAzaid.Models;
using System.Text.RegularExpressions;

namespace PruebaTecnica_JavierAzaid.Business
{
    public class BRProducts : IBRProducts
    {
        private readonly AppDbContext _context;

        public BRProducts(AppDbContext context) => (_context) = (context);

        /// <summary>
        /// Obtiene una lista paginada de productos.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GenericResponse<IEnumerable<ProductsModel>>> GetProductsPagination(RequestProductsPagination request)
        {
            var response = new GenericResponse<IEnumerable<ProductsModel>>();

            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            if (products == null || !products.Any()) return response.ToError("No se encontraron productos", 404);
            return response.ToSuccess("Consulta exitosa", 0, products);
        }

        /// <summary>
        /// Obtiene productos por nombre o código (SKU).
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public async Task<GenericResponse<IEnumerable<ProductsModel>>> GetProductsByNameOrCode(RequestSearchProductDto request)
        {
            var response = new GenericResponse<IEnumerable<ProductsModel>>();

            if (string.IsNullOrWhiteSpace(request.SearchTerm)) return response.ToError("El parámetro de búsqueda no puede estar vacío.", 400);

            var searchTerm = request.SearchTerm.Trim().ToLower();

            var products = await _context.Products
                .Where(p =>
                    p.NameProduct.ToLower().Contains(searchTerm) ||
                    p.SKU.ToLower().Contains(searchTerm))
                .ToListAsync();

            if (!products.Any()) return response.ToError("No se encontraron productos con el criterio de búsqueda.", 404);

            return response.ToSuccess("Consulta exitosa", 0, products);
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GenericResponse<bool>> DeleteProductAsync(RequestDeleteProductDto request)
        {
            var response = new GenericResponse<bool>();

            if (request.IdProduct <= 0) return response.ToError("El ID del producto no es válido.", 400);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.IdProduct);
            if (product == null) return response.ToError("No se encontró el producto especificado.", 404);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return response.ToSuccess("Producto eliminado correctamente.", 0, true);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GenericResponse<ProductsModel>> CreateProductAsync(RequestCreateProductDto request)
        {
            var response = new GenericResponse<ProductsModel>();

            var existingSku = await _context.Products.FirstOrDefaultAsync(p => p.SKU == request.SKU);
            if (existingSku != null) return response.ToError("Ya existe un producto con el mismo SKU.", 409);
            if (request.PriceSale <= 0) return response.ToError("El precio de venta debe ser mayor a 0.", 400);
            if (request.PriceCost <= 0) return response.ToError("El precio de compra debe ser mayor a 0.", 400);
            if (string.IsNullOrWhiteSpace(request.SKU)) return response.ToError("El SKU no puede estar vacío.", 400);
            if (string.IsNullOrWhiteSpace(request.NameProduct)) return response.ToError("El nombre del producto no puede estar vacío.", 400);
            if (request.Quantity <= 0) return response.ToError("La cantidad debe ser mayor a 0.", 400);

            var newProduct = new ProductsModel
            {
                NameProduct = request.NameProduct.Trim(),
                SKU = request.SKU.Trim(),
                Quantity = request.Quantity,
                PriceSale = request.PriceSale,
                PriceCost = request.PriceCost,
                DateModify = DateTime.Now
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return response.ToSuccess("Producto creado correctamente.", 0, newProduct);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GenericResponse<ProductsModel>> UpdateProductAsync(RequestUpdateProductDto request)
        {
            var response = new GenericResponse<ProductsModel>();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (product == null) return response.ToError("No se encontró el producto especificado.", 404);
            if (!Regex.IsMatch(request.Sku, @"^[A-Za-z0-9_-]{4,20}$")) return response.ToError("El SKU debe tener entre 4 y 20 caracteres alfanuméricos, sin espacios ni símbolos especiales.", 400);
            if (request.Quantity <= 0) return response.ToError("La cantidad debe ser mayor a 0.", 400);
            if (string.IsNullOrWhiteSpace(request.NameProduct)) return response.ToError("El nombre del producto no puede estar vacío.", 400);
            if (string.IsNullOrWhiteSpace(request.Sku)) return response.ToError("El SKU no puede estar vacío.", 400);
            if (request.PriceSale <= 0) response.ToError("El precio de venta debe ser mayor a 0.", 400);
            if (request.PriceCost <= 0) return response.ToError("El precio de costo debe ser mayor a 0.", 400);

            if (!product.SKU.Equals(request.Sku, StringComparison.OrdinalIgnoreCase))
            {
                var existingSku = await _context.Products.FirstOrDefaultAsync(p => p.SKU == request.Sku);
                if (existingSku != null)
                    return response.ToError("Ya existe otro producto con el mismo SKU.", 409);
            }

            product.NameProduct = request.NameProduct.Trim();
            product.SKU = request.Sku.Trim();
            product.Quantity = request.Quantity;
            product.PriceSale = request.PriceSale;
            product.PriceCost = request.PriceCost ?? product.PriceCost;
            product.DateModify = DateTime.Now;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return response.ToSuccess("Producto actualizado correctamente.", 0, product);
        }



    }
}
