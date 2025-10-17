using Microsoft.AspNetCore.Mvc;
using AdventureWorks.Enterprise.Api.Models.Production;

namespace AdventureWorks.Enterprise.Api.Controllers
{
    /// <summary>
    /// Controlador para la gestión de órdenes de trabajo y producción
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesTrabajosController : ControllerBase
    {
        private readonly ILogger<OrdenesTrabajosController> _logger;
        private static List<WorkOrderDto> _ordenesTrabajos = new();

        public OrdenesTrabajosController(ILogger<OrdenesTrabajosController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Obtener todas las órdenes de trabajo
        /// </summary>
        /// <returns>Lista de órdenes de trabajo</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderDto>>> ObtenerOrdenesTrabajos()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las órdenes de trabajo en {Timestamp}", DateTime.UtcNow);
                return Ok(_ordenesTrabajos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las órdenes de trabajo");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener orden de trabajo por ID
        /// </summary>
        /// <param name="id">ID de la orden de trabajo</param>
        /// <returns>Detalles de la orden de trabajo</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkOrderDto>> ObtenerOrdenTrabajo(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo orden de trabajo {OrdenTrabajoId} en {Timestamp}", id, DateTime.UtcNow);
                
                var ordenTrabajo = _ordenesTrabajos.FirstOrDefault(w => w.WorkOrderID == id);
                if (ordenTrabajo == null)
                {
                    _logger.LogWarning("Orden de trabajo {OrdenTrabajoId} no encontrada", id);
                    return NotFound($"Orden de trabajo con ID {id} no encontrada");
                }

                return Ok(ordenTrabajo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la orden de trabajo {OrdenTrabajoId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Crear una nueva orden de trabajo
        /// </summary>
        /// <param name="ordenTrabajo">Datos de la orden de trabajo</param>
        /// <returns>Orden de trabajo creada</returns>
        [HttpPost]
        public async Task<ActionResult<WorkOrderDto>> CrearOrdenTrabajo([FromBody] WorkOrderDto ordenTrabajo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Estado del modelo inválido para crear orden de trabajo");
                    return BadRequest(ModelState);
                }

                ordenTrabajo.WorkOrderID = _ordenesTrabajos.Count > 0 ? _ordenesTrabajos.Max(w => w.WorkOrderID) + 1 : 1;
                ordenTrabajo.StockedQty = ordenTrabajo.OrderQty - ordenTrabajo.ScrappedQty;
                ordenTrabajo.ModifiedDate = DateTime.UtcNow;

                _ordenesTrabajos.Add(ordenTrabajo);
                
                _logger.LogInformation("Orden de trabajo {OrdenTrabajoId} creada en {Timestamp}", ordenTrabajo.WorkOrderID, DateTime.UtcNow);
                
                return CreatedAtAction(nameof(ObtenerOrdenTrabajo), new { id = ordenTrabajo.WorkOrderID }, ordenTrabajo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la orden de trabajo");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Actualizar una orden de trabajo existente
        /// </summary>
        /// <param name="id">ID de la orden de trabajo</param>
        /// <param name="ordenTrabajo">Datos actualizados de la orden de trabajo</param>
        /// <returns>Sin contenido si es exitoso</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarOrdenTrabajo(int id, [FromBody] WorkOrderDto ordenTrabajo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Estado del modelo inválido para actualizar orden de trabajo {OrdenTrabajoId}", id);
                    return BadRequest(ModelState);
                }

                var ordenTrabajoExistente = _ordenesTrabajos.FirstOrDefault(w => w.WorkOrderID == id);
                if (ordenTrabajoExistente == null)
                {
                    _logger.LogWarning("Orden de trabajo {OrdenTrabajoId} no encontrada para actualizar", id);
                    return NotFound($"Orden de trabajo con ID {id} no encontrada");
                }

                ordenTrabajoExistente.ProductID = ordenTrabajo.ProductID;
                ordenTrabajoExistente.OrderQty = ordenTrabajo.OrderQty;
                ordenTrabajoExistente.ScrappedQty = ordenTrabajo.ScrappedQty;
                ordenTrabajoExistente.StockedQty = ordenTrabajo.OrderQty - ordenTrabajo.ScrappedQty;
                ordenTrabajoExistente.StartDate = ordenTrabajo.StartDate;
                ordenTrabajoExistente.EndDate = ordenTrabajo.EndDate;
                ordenTrabajoExistente.DueDate = ordenTrabajo.DueDate;
                ordenTrabajoExistente.ScrapReasonID = ordenTrabajo.ScrapReasonID;
                ordenTrabajoExistente.ModifiedDate = DateTime.UtcNow;

                _logger.LogInformation("Orden de trabajo {OrdenTrabajoId} actualizada en {Timestamp}", id, DateTime.UtcNow);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la orden de trabajo {OrdenTrabajoId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Eliminar una orden de trabajo
        /// </summary>
        /// <param name="id">ID de la orden de trabajo</param>
        /// <returns>Sin contenido si es exitoso</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarOrdenTrabajo(int id)
        {
            try
            {
                var ordenTrabajo = _ordenesTrabajos.FirstOrDefault(w => w.WorkOrderID == id);
                if (ordenTrabajo == null)
                {
                    _logger.LogWarning("Orden de trabajo {OrdenTrabajoId} no encontrada para eliminar", id);
                    return NotFound($"Orden de trabajo con ID {id} no encontrada");
                }

                _ordenesTrabajos.Remove(ordenTrabajo);
                
                _logger.LogInformation("Orden de trabajo {OrdenTrabajoId} eliminada en {Timestamp}", id, DateTime.UtcNow);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la orden de trabajo {OrdenTrabajoId}", id);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }

        /// <summary>
        /// Obtener órdenes de trabajo por ID de producto
        /// </summary>
        /// <param name="productoId">ID del producto</param>
        /// <returns>Lista de órdenes de trabajo para el producto</returns>
        [HttpGet("producto/{productoId}")]
        public async Task<ActionResult<IEnumerable<WorkOrderDto>>> ObtenerOrdenesTrabajoPorProducto(int productoId)
        {
            try
            {
                _logger.LogInformation("Obteniendo órdenes de trabajo para producto {ProductoId} en {Timestamp}", productoId, DateTime.UtcNow);
                
                var ordenesTrabajoProducto = _ordenesTrabajos.Where(w => w.ProductID == productoId).ToList();
                return Ok(ordenesTrabajoProducto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener órdenes de trabajo para producto {ProductoId}", productoId);
                return StatusCode(500, "Ocurrió un error al procesar su solicitud");
            }
        }
    }
}