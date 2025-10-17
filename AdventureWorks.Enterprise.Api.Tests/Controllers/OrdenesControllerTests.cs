using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdventureWorks.Enterprise.Api.Controllers;
using AdventureWorks.Enterprise.Api.Data;
using AdventureWorks.Enterprise.Api.Tests.Infrastructure;
using AdventureWorks.Enterprise.Api.Models.Sales;

namespace AdventureWorks.Enterprise.Api.Tests.Controllers
{
    /// <summary>
    /// Tests para el controlador de Órdenes
    /// </summary>
    public class OrdenesControllerTests : BaseControllerTest
    {
        private readonly OrdenesController _controller;
        private readonly Mock<ILogger<OrdenesController>> _mockLogger;

        public OrdenesControllerTests()
        {
            _mockLogger = CreateMockLogger<OrdenesController>();
            _controller = new OrdenesController(Context, _mockLogger.Object);
            SeedTestData();
        }

        #region ObtenerOrdenes Tests

        [Fact]
        public async Task ObtenerOrdenes_DeberiaRetornarListaDeOrdenes_CuandoExistenOrdenes()
        {
            // Act
            var result = await _controller.ObtenerOrdenes();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var ordenes = okResult?.Value as List<SalesOrderHeader>;
            ordenes.Should().NotBeNull();
            ordenes.Should().HaveCount(2);
        }

        [Fact]
        public async Task ObtenerOrdenes_DeberiaRetornarError500_CuandoOcurreExcepcion()
        {
            // Arrange
            var mockLogger = CreateMockLogger<OrdenesController>();
            var controller = new OrdenesController(null!, mockLogger.Object); // Forzar excepción

            // Act
            var result = await controller.ObtenerOrdenes();

            // Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        #endregion

        #region ObtenerOrden Tests

        [Fact]
        public async Task ObtenerOrden_DeberiaRetornarOrden_CuandoExisteOrden()
        {
            // Arrange
            var ordenId = 1;

            // Act
            var result = await _controller.ObtenerOrden(ordenId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var orden = okResult?.Value as SalesOrderHeader;
            orden.Should().NotBeNull();
            orden?.SalesOrderID.Should().Be(ordenId);
        }

        [Fact]
        public async Task ObtenerOrden_DeberiaRetornarNotFound_CuandoNoExisteOrden()
        {
            // Arrange
            var ordenId = 999;

            // Act
            var result = await _controller.ObtenerOrden(ordenId);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region ObtenerDetallesOrden Tests

        [Fact]
        public async Task ObtenerDetallesOrden_DeberiaRetornarDetalles_CuandoExisteOrden()
        {
            // Arrange
            var ordenId = 1;
            
            // Agregar detalles de prueba
            Context.SalesOrderDetails.Add(new SalesOrderDetail
            {
                SalesOrderID = ordenId,
                SalesOrderDetailID = 1,
                OrderQty = 2,
                ProductID = 1,
                SpecialOfferID = 1,
                UnitPrice = 150.00m,
                UnitPriceDiscount = 0.00m,
                LineTotal = 300.00m,
                RowGuid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow
            });
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.ObtenerDetallesOrden(ordenId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var detalles = okResult?.Value as List<SalesOrderDetail>;
            detalles.Should().NotBeNull();
            detalles.Should().HaveCount(1);
            detalles?.First().SalesOrderID.Should().Be(ordenId);
        }

        [Fact]
        public async Task ObtenerDetallesOrden_DeberiaRetornarListaVacia_CuandoOrdenSinDetalles()
        {
            // Arrange
            var ordenId = 2; // Orden sin detalles

            // Act
            var result = await _controller.ObtenerDetallesOrden(ordenId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var detalles = okResult?.Value as List<SalesOrderDetail>;
            detalles.Should().NotBeNull();
            detalles.Should().BeEmpty();
        }

        #endregion

        #region CrearOrden Tests

        [Fact]
        public async Task CrearOrden_DeberiaCrearOrden_CuandoDatosValidos()
        {
            // Arrange
            var nuevaOrden = new SalesOrderHeader
            {
                OrderDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(10),
                Status = 1,
                OnlineOrderFlag = true,
                CustomerID = 1,
                BillToAddressID = 1,
                ShipToAddressID = 1,
                ShipMethodID = 1,
                SubTotal = 500.00m,
                TaxAmt = 40.00m,
                Freight = 10.00m
            };

            // Act
            var result = await _controller.CrearOrden(nuevaOrden);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result.Result as CreatedAtActionResult;
            var ordenCreada = createdResult?.Value as SalesOrderHeader;
            ordenCreada.Should().NotBeNull();
            ordenCreada?.CustomerID.Should().Be(nuevaOrden.CustomerID);
            ordenCreada?.RowGuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task CrearOrden_DeberiaRetornarBadRequest_CuandoModeloInvalido()
        {
            // Arrange
            var ordenInvalida = new SalesOrderHeader(); // Modelo inválido
            _controller.ModelState.AddModelError("CustomerID", "Campo requerido");

            // Act
            var result = await _controller.CrearOrden(ordenInvalida);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CrearOrden_DeberiaRetornarBadRequest_CuandoClienteNoExiste()
        {
            // Arrange
            var nuevaOrden = new SalesOrderHeader
            {
                CustomerID = 999, // Cliente inexistente
                OrderDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(10),
                BillToAddressID = 1,
                ShipToAddressID = 1,
                ShipMethodID = 1,
                SubTotal = 100.00m,
                TaxAmt = 8.00m,
                Freight = 2.00m
            };

            // Act
            var result = await _controller.CrearOrden(nuevaOrden);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        #endregion

        #region ActualizarOrden Tests

        [Fact]
        public async Task ActualizarOrden_DeberiaActualizarOrden_CuandoDatosValidos()
        {
            // Arrange
            var ordenId = 1;
            var ordenActualizada = new SalesOrderHeader
            {
                SalesOrderID = ordenId,
                RevisionNumber = 1,
                OrderDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(15),
                ShipDate = DateTime.UtcNow.AddDays(-3),
                Status = 5,
                OnlineOrderFlag = false,
                CustomerID = 1,
                BillToAddressID = 1,
                ShipToAddressID = 1,
                ShipMethodID = 1,
                SubTotal = 1200.00m,
                TaxAmt = 96.00m,
                Freight = 24.00m
            };

            // Act
            var result = await _controller.ActualizarOrden(ordenId, ordenActualizada);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            
            // Verificar que se actualizó en la base de datos
            var ordenEnDb = await Context.SalesOrderHeaders.FindAsync(ordenId);
            ordenEnDb?.SubTotal.Should().Be(1200.00m);
            ordenEnDb?.RevisionNumber.Should().Be(2); // Se incrementa
        }

        [Fact]
        public async Task ActualizarOrden_DeberiaRetornarBadRequest_CuandoIdNoCoincide()
        {
            // Arrange
            var ordenId = 1;
            var ordenActualizada = new SalesOrderHeader { SalesOrderID = 2 };

            // Act
            var result = await _controller.ActualizarOrden(ordenId, ordenActualizada);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ActualizarOrden_DeberiaRetornarNotFound_CuandoOrdenNoExiste()
        {
            // Arrange
            var ordenId = 999;
            var ordenActualizada = new SalesOrderHeader { SalesOrderID = ordenId };

            // Act
            var result = await _controller.ActualizarOrden(ordenId, ordenActualizada);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region EliminarOrden Tests

        [Fact]
        public async Task EliminarOrden_DeberiaEliminarOrden_CuandoOrdenEsPendiente()
        {
            // Arrange
            var ordenId = 2; // Orden con status 1 (pendiente)

            // Act
            var result = await _controller.EliminarOrden(ordenId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            
            // Verificar que se eliminó de la base de datos
            var ordenEnDb = await Context.SalesOrderHeaders.FindAsync(ordenId);
            ordenEnDb.Should().BeNull();
        }

        [Fact]
        public async Task EliminarOrden_DeberiaRetornarBadRequest_CuandoOrdenYaProcesada()
        {
            // Arrange
            var ordenId = 1; // Orden con status 5 (enviado)

            // Act
            var result = await _controller.EliminarOrden(ordenId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task EliminarOrden_DeberiaRetornarNotFound_CuandoOrdenNoExiste()
        {
            // Arrange
            var ordenId = 999;

            // Act
            var result = await _controller.EliminarOrden(ordenId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region ObtenerOrdenesPorCliente Tests

        [Fact]       
        public async Task ObtenerOrdenesPorCliente_DeberiaRetornarOrdenes_CuandoClienteTieneOrdenes()
        {
            // Arrange
            var clienteId = 1;

            // Act
            var result = await _controller.ObtenerOrdenesPorCliente(clienteId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var ordenes = okResult?.Value as List<SalesOrderHeader>;
            ordenes.Should().NotBeNull();
            ordenes.Should().HaveCount(1);
            ordenes?.All(o => o.CustomerID == clienteId).Should().BeTrue();
        }

        [Fact]
        public async Task ObtenerOrdenesPorCliente_DeberiaRetornarListaVacia_CuandoClienteNoTieneOrdenes()
        {
            // Arrange
            var clienteId = 999;

            // Act
            var result = await _controller.ObtenerOrdenesPorCliente(clienteId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var ordenes = okResult?.Value as List<SalesOrderHeader>;
            ordenes.Should().NotBeNull();
            ordenes.Should().BeEmpty();
        }

        #endregion

        #region ObtenerReporteTop10ProductosMasVendidos Tests

        [Fact]
        public async Task ObtenerReporteTop10ProductosMasVendidos_DeberiaRetornarReporte_CuandoExistenDatos()
        {
            // Act
            var result = await _controller.ObtenerReporteTop10ProductosMasVendidos();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var reporte = okResult?.Value as List<ReporteTop10ProductosDto>;
            reporte.Should().NotBeNull();
        }

        #endregion
    }
}