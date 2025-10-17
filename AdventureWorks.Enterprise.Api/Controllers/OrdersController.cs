using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Enterprise.Api.Data;
using AdventureWorks.Enterprise.Api.Models.Sales;

namespace AdventureWorks.Enterprise.Api.Controllers
{
    /// <summary>
    /// Controlador para la gestión de órdenes de venta
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesController : ControllerBase
    {
        private readonly AdventureWorksDbContext _context;
        private readonly ILogger<OrdenesController> _logger;

        public OrdenesController(AdventureWorksDbContext context, ILogger<OrdenesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtener todas las órdenes de venta
        /// </summary>
        /// <returns>Lista de órdenes de venta</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrderHeader>>> ObtenerOrdenes()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las órdenes en {Timestamp}", DateTime.UtcNow);
                
                var ordenes = await _context.SalesOrderHeaders
                    .Include(s => s.Customer)
                    .Include(s => s.SalesPerson)
                    .Include(s => s.SalesTerritory)
                    .OrderByDescending(s => s.OrderDate)
                    .Take(100) // Limitar a las últimas 100 órdenes
                    .ToListAsync();

                return Ok(ordenes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las órdenes");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener orden de venta por ID
        /// </summary>
        /// <param name="id">ID de la orden de venta</param>
        /// <returns>Detalles de la orden de venta</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrderHeader>> ObtenerOrden(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo orden {OrdenId} en {Timestamp}", id, DateTime.UtcNow);
                
                var orden = await _context.SalesOrderHeaders
                    .Include(s => s.Customer)
                    .Include(s => s.SalesPerson)
                    .Include(s => s.SalesTerritory)
                    .Include(s => s.SalesOrderDetails)
                        .ThenInclude(d => d.Product)
                    .FirstOrDefaultAsync(s => s.SalesOrderID == id);

                if (orden == null)
                {
                    _logger.LogWarning("Orden {OrdenId} no encontrada", id);
                    return NotFound($"Orden con ID {id} no encontrada");
                }

                return Ok(orden);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la orden {OrdenId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener detalles de orden para una orden específica
        /// </summary>
        /// <param name="id">ID de la orden de venta</param>
        /// <returns>Lista de detalles de la orden</returns>
        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<IEnumerable<SalesOrderDetail>>> ObtenerDetallesOrden(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo detalles de orden para orden {OrdenId} en {Timestamp}", id, DateTime.UtcNow);
                
                var detalles = await _context.SalesOrderDetails
                    .Include(d => d.Product)
                    .Include(d => d.SpecialOffer)
                    .Where(d => d.SalesOrderID == id)
                    .ToListAsync();

                return Ok(detalles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener detalles de orden para orden {OrdenId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener reporte del top 10 de productos más vendidos
        /// </summary>
        /// <returns>Lista de los 10 productos más vendidos</returns>
        [HttpGet("reporte/top10-productos-mas-vendidos")]
        public async Task<ActionResult<IEnumerable<ReporteTop10ProductosDto>>> ObtenerReporteTop10ProductosMasVendidos()
        {
            try
            {
                _logger.LogInformation("Generando reporte de top 10 productos más vendidos en {Timestamp}", DateTime.UtcNow);
                
                var reporte = await _context.Database
                    .SqlQueryRaw<ReporteTop10ProductosDto>("EXEC usp_Top10ProductosMasVendidos_Sagastume")
                    .ToListAsync();

                _logger.LogInformation("Reporte top 10 productos generado exitosamente con {Count} registros", reporte.Count);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar el reporte de top 10 productos más vendidos");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Crear una nueva orden de venta
        /// </summary>
        /// <param name="orden">Datos de la orden de venta</param>
        /// <returns>Orden de venta creada</returns>
        [HttpPost]
        public async Task<ActionResult<SalesOrderHeader>> CrearOrden([FromBody] SalesOrderHeader orden)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Estado del modelo inválido para crear orden");
                    return BadRequest(ModelState);
                }

                // Verificar que el cliente existe
                var clienteExists = await _context.Customers.AnyAsync(c => c.CustomerID == orden.CustomerID);
                if (!clienteExists)
                {
                    return BadRequest("El cliente especificado no existe");
                }

                orden.RowGuid = Guid.NewGuid();
                orden.ModifiedDate = DateTime.UtcNow;
                orden.RevisionNumber = 1;
                orden.Status = 1; // Pendiente

                _context.SalesOrderHeaders.Add(orden);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Orden {OrdenId} creada en {Timestamp}", orden.SalesOrderID, DateTime.UtcNow);
                
                return CreatedAtAction(nameof(ObtenerOrden), new { id = orden.SalesOrderID }, orden);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la orden");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Actualizar una orden de venta existente
        /// </summary>
        /// <param name="id">ID de la orden de venta</param>
        /// <param name="orden">Datos actualizados de la orden de venta</param>
        /// <returns>Sin contenido si es exitoso</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarOrden(int id, [FromBody] SalesOrderHeader orden)
        {
            try
            {
                if (id != orden.SalesOrderID)
                {
                    return BadRequest("El ID de la orden no coincide");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Estado del modelo inválido para actualizar orden {OrdenId}", id);
                    return BadRequest(ModelState);
                }

                var ordenExistente = await _context.SalesOrderHeaders.FindAsync(id);
                if (ordenExistente == null)
                {
                    _logger.LogWarning("Orden {OrdenId} no encontrada para actualizar", id);
                    return NotFound($"Orden con ID {id} no encontrada");
                }

                // Actualizar propiedades
                ordenExistente.RevisionNumber++;
                ordenExistente.OrderDate = orden.OrderDate;
                ordenExistente.DueDate = orden.DueDate;
                ordenExistente.ShipDate = orden.ShipDate;
                ordenExistente.Status = orden.Status;
                ordenExistente.OnlineOrderFlag = orden.OnlineOrderFlag;
                ordenExistente.CustomerID = orden.CustomerID;
                ordenExistente.SalesPersonID = orden.SalesPersonID;
                ordenExistente.TerritoryID = orden.TerritoryID;
                ordenExistente.SubTotal = orden.SubTotal;
                ordenExistente.TaxAmt = orden.TaxAmt;
                ordenExistente.Freight = orden.Freight;
                ordenExistente.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Orden {OrdenId} actualizada en {Timestamp}", id, DateTime.UtcNow);
                
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrdenExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la orden {OrdenId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Eliminar una orden de venta
        /// </summary>
        /// <param name="id">ID de la orden de venta</param>
        /// <returns>Sin contenido si es exitoso</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarOrden(int id)
        {
            try
            {
                var orden = await _context.SalesOrderHeaders
                    .Include(s => s.SalesOrderDetails)
                    .FirstOrDefaultAsync(s => s.SalesOrderID == id);

                if (orden == null)
                {
                    _logger.LogWarning("Orden {OrdenId} no encontrada para eliminar", id);
                    return NotFound($"Orden con ID {id} no encontrada");
                }

                // Verificar si la orden puede ser eliminada (ejemplo: solo órdenes pendientes)
                if (orden.Status > 1)
                {
                    return BadRequest("No se puede eliminar una orden que ya ha sido procesada");
                }

                _context.SalesOrderHeaders.Remove(orden);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Orden {OrdenId} eliminada en {Timestamp}", id, DateTime.UtcNow);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la orden {OrdenId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener órdenes por ID de cliente
        /// </summary>
        /// <param name="clienteId">ID del cliente</param>
        /// <returns>Lista de órdenes para el cliente</returns>
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<SalesOrderHeader>>> ObtenerOrdenesPorCliente(int clienteId)
        {
            try
            {
                _logger.LogInformation("Obteniendo órdenes para cliente {ClienteId} en {Timestamp}", clienteId, DateTime.UtcNow);
                
                var ordenesCliente = await _context.SalesOrderHeaders
                    .Include(s => s.Customer)
                    .Include(s => s.SalesPerson)
                    .Where(s => s.CustomerID == clienteId)
                    .OrderByDescending(s => s.OrderDate)
                    .ToListAsync();

                return Ok(ordenesCliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener órdenes para cliente {ClienteId}", clienteId);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        private async Task<bool> OrdenExists(int id)
        {
            return await _context.SalesOrderHeaders.AnyAsync(e => e.SalesOrderID == id);
        }
    }
}