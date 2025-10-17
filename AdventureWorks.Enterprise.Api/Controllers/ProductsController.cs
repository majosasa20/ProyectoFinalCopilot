using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Enterprise.Api.Data;
using AdventureWorks.Enterprise.Api.Models.Production;

namespace AdventureWorks.Enterprise.Api.Controllers
{
    /// <summary>
    /// Controlador para la gestión de productos e inventario
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AdventureWorksDbContext _context;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(AdventureWorksDbContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtener todos los productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> ObtenerProductos()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los productos en {Timestamp}", DateTime.UtcNow);
                
                var productos = await _context.Products
                    .Include(p => p.ProductSubcategory)
                        .ThenInclude(ps => ps.ProductCategory)
                    .Include(p => p.ProductModel)
                    .Where(p => p.SellEndDate == null) // Solo productos activos
                    .OrderBy(p => p.Name)
                    .Take(100) // Limitar a 100 productos
                    .ToListAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener producto por ID
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Detalles del producto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> ObtenerProducto(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo producto {ProductoId} en {Timestamp}", id, DateTime.UtcNow);
                
                var producto = await _context.Products
                    .Include(p => p.ProductSubcategory)
                        .ThenInclude(ps => ps.ProductCategory)
                    .Include(p => p.ProductModel)
                    .Include(p => p.ProductInventories)
                    .FirstOrDefaultAsync(p => p.ProductID == id);

                if (producto == null)
                {
                    _logger.LogWarning("Producto {ProductoId} no encontrado", id);
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                return Ok(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el producto {ProductoId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener inventario del producto
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Detalles del inventario del producto</returns>
        [HttpGet("{id}/inventario")]
        public async Task<ActionResult<IEnumerable<ProductInventory>>> ObtenerInventarioProducto(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo inventario para producto {ProductoId} en {Timestamp}", id, DateTime.UtcNow);
                
                var inventario = await _context.ProductInventories
                    .Include(pi => pi.Product)
                    .Where(pi => pi.ProductID == id)
                    .ToListAsync();

                return Ok(inventario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener inventario para producto {ProductoId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener reporte de productos con bajo inventario
        /// </summary>
        /// <param name="umbralInventario">Umbral de inventario (por defecto 10)</param>
        /// <returns>Lista de productos con inventario bajo</returns>
        [HttpGet("reporte/bajo-inventario")]
        public async Task<ActionResult<IEnumerable<ReporteBajoInventarioDto>>> ObtenerReporteProductosBajoInventario([FromQuery] int umbralInventario = 10)
        {
            try
            {
                _logger.LogInformation("Generando reporte de productos con bajo inventario (umbral: {Umbral}) en {Timestamp}", 
                    umbralInventario, DateTime.UtcNow);
                
                var reporte = await _context.Database
                    .SqlQueryRaw<ReporteBajoInventarioDto>("EXEC usp_ProductosConBajoInventario_Sagastume @UmbralInventario = {0}", umbralInventario)
                    .ToListAsync();

                _logger.LogInformation("Reporte de bajo inventario generado exitosamente con {Count} registros", reporte.Count);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar el reporte de productos con bajo inventario");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Crear un nuevo producto
        /// </summary>
        /// <param name="producto">Datos del producto</param>
        /// <returns>Producto creado</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> CrearProducto([FromBody] Product producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Estado del modelo inválido para crear producto");
                    return BadRequest(ModelState);
                }

                // Verificar que el número de producto no existe
                var productoExiste = await _context.Products.AnyAsync(p => p.ProductNumber == producto.ProductNumber);
                if (productoExiste)
                {
                    return BadRequest("Ya existe un producto con este número");
                }

                producto.RowGuid = Guid.NewGuid();
                producto.ModifiedDate = DateTime.UtcNow;
                producto.SellStartDate = DateTime.UtcNow;

                _context.Products.Add(producto);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Producto {ProductoId} creado en {Timestamp}", producto.ProductID, DateTime.UtcNow);
                
                return CreatedAtAction(nameof(ObtenerProducto), new { id = producto.ProductID }, producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el producto");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Actualizar un producto existente
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <param name="producto">Datos actualizados del producto</param>
        /// <returns>Sin contenido si es exitoso</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Product producto)
        {
            try
            {
                if (id != producto.ProductID)
                {
                    return BadRequest("El ID del producto no coincide");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Estado del modelo inválido para actualizar producto {ProductoId}", id);
                    return BadRequest(ModelState);
                }

                var productoExistente = await _context.Products.FindAsync(id);
                if (productoExistente == null)
                {
                    _logger.LogWarning("Producto {ProductoId} no encontrado para actualizar", id);
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                // Actualizar propiedades
                productoExistente.Name = producto.Name;
                productoExistente.ProductNumber = producto.ProductNumber;
                productoExistente.MakeFlag = producto.MakeFlag;
                productoExistente.FinishedGoodsFlag = producto.FinishedGoodsFlag;
                productoExistente.Color = producto.Color;
                productoExistente.SafetyStockLevel = producto.SafetyStockLevel;
                productoExistente.ReorderPoint = producto.ReorderPoint;
                productoExistente.StandardCost = producto.StandardCost;
                productoExistente.ListPrice = producto.ListPrice;
                productoExistente.Size = producto.Size;
                productoExistente.Weight = producto.Weight;
                productoExistente.DaysToManufacture = producto.DaysToManufacture;
                productoExistente.ProductLine = producto.ProductLine;
                productoExistente.Class = producto.Class;
                productoExistente.Style = producto.Style;
                productoExistente.SellStartDate = producto.SellStartDate;
                productoExistente.SellEndDate = producto.SellEndDate;
                productoExistente.DiscontinuedDate = producto.DiscontinuedDate;
                productoExistente.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Producto {ProductoId} actualizado en {Timestamp}", id, DateTime.UtcNow);
                
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductoExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el producto {ProductoId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Eliminar un producto
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Sin contenido si es exitoso</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                var producto = await _context.Products.FindAsync(id);
                if (producto == null)
                {
                    _logger.LogWarning("Producto {ProductoId} no encontrado para eliminar", id);
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                // Verificar si el producto tiene órdenes o inventario
                var tieneOrdenes = await _context.SalesOrderDetails.AnyAsync(sod => sod.ProductID == id);
                var tieneInventario = await _context.ProductInventories.AnyAsync(pi => pi.ProductID == id);

                if (tieneOrdenes || tieneInventario)
                {
                    // En lugar de eliminar, marcar como descontinuado
                    producto.SellEndDate = DateTime.UtcNow;
                    producto.DiscontinuedDate = DateTime.UtcNow;
                    producto.ModifiedDate = DateTime.UtcNow;
                    
                    await _context.SaveChangesAsync();
                    
                    _logger.LogInformation("Producto {ProductoId} marcado como descontinuado en {Timestamp}", id, DateTime.UtcNow);
                }
                else
                {
                    _context.Products.Remove(producto);
                    await _context.SaveChangesAsync();
                    
                    _logger.LogInformation("Producto {ProductoId} eliminado en {Timestamp}", id, DateTime.UtcNow);
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el producto {ProductoId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Actualizar inventario del producto
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <param name="inventario">Datos del inventario</param>
        /// <returns>Sin contenido si es exitoso</returns>
        [HttpPut("{id}/inventario")]
        public async Task<IActionResult> ActualizarInventario(int id, [FromBody] ProductInventory inventario)
        {
            try
            {
                if (id != inventario.ProductID)
                {
                    return BadRequest("El ID del producto no coincide");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Estado del modelo inválido para actualizar inventario del producto {ProductoId}", id);
                    return BadRequest(ModelState);
                }

                var inventarioExistente = await _context.ProductInventories
                    .FirstOrDefaultAsync(i => i.ProductID == id && i.LocationID == inventario.LocationID);

                if (inventarioExistente == null)
                {
                    // Crear nuevo registro de inventario
                    inventario.RowGuid = Guid.NewGuid();
                    inventario.ModifiedDate = DateTime.UtcNow;
                    _context.ProductInventories.Add(inventario);
                    
                    _logger.LogInformation("Inventario creado para producto {ProductoId} en ubicación {UbicacionId}", id, inventario.LocationID);
                }
                else
                {
                    // Actualizar inventario existente
                    inventarioExistente.Shelf = inventario.Shelf;
                    inventarioExistente.Bin = inventario.Bin;
                    inventarioExistente.Quantity = inventario.Quantity;
                    inventarioExistente.ModifiedDate = DateTime.UtcNow;
                    
                    _logger.LogInformation("Inventario actualizado para producto {ProductoId} en ubicación {UbicacionId}", id, inventario.LocationID);
                }

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar inventario para producto {ProductoId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener productos por categoría
        /// </summary>
        /// <param name="categoriaId">ID de la categoría de producto</param>
        /// <returns>Lista de productos en la categoría</returns>
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Product>>> ObtenerProductosPorCategoria(int categoriaId)
        {
            try
            {
                _logger.LogInformation("Obteniendo productos para categoría {CategoriaId} en {Timestamp}", categoriaId, DateTime.UtcNow);
                
                var productosCategoria = await _context.Products
                    .Include(p => p.ProductSubcategory)
                        .ThenInclude(ps => ps.ProductCategory)
                    .Where(p => p.ProductSubcategory.ProductCategoryID == categoriaId)
                    .Where(p => p.SellEndDate == null)
                    .OrderBy(p => p.Name)
                    .ToListAsync();

                return Ok(productosCategoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos para categoría {CategoriaId}", categoriaId);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        private async Task<bool> ProductoExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.ProductID == id);
        }
    }
}