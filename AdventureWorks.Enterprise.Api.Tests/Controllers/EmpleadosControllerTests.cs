using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdventureWorks.Enterprise.Api.Controllers;
using AdventureWorks.Enterprise.Api.Data;
using AdventureWorks.Enterprise.Api.Tests.Infrastructure;
using AdventureWorks.Enterprise.Api.Models.HumanResources;

namespace AdventureWorks.Enterprise.Api.Tests.Controllers
{
    /// <summary>
    /// Tests para el controlador de Empleados
    /// </summary>
    public class EmpleadosControllerTests : BaseControllerTest
    {
        private readonly EmpleadosController _controller;
        private readonly Mock<ILogger<EmpleadosController>> _mockLogger;

        public EmpleadosControllerTests()
        {
            _mockLogger = CreateMockLogger<EmpleadosController>();
            _controller = new EmpleadosController(Context, _mockLogger.Object);
            SeedTestData();
        }

        #region ObtenerEmpleados Tests

        [Fact]
        public async Task ObtenerEmpleados_DeberiaRetornarListaDeEmpleados_CuandoExistenEmpleados()
        {
            // Act
            var result = await _controller.ObtenerEmpleados();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var empleados = okResult?.Value as List<Employee>;
            empleados.Should().NotBeNull();
            empleados.Should().HaveCountGreaterThan(0);
            empleados.All(e => e.CurrentFlag).Should().BeTrue();
        }

        [Fact]
        public async Task ObtenerEmpleados_DeberiaRetornarError500_CuandoOcurreExcepcion()
        {
            // Arrange
            var mockLogger = CreateMockLogger<EmpleadosController>();
            var controller = new EmpleadosController(null!, mockLogger.Object); // Forzar excepción

            // Act
            var result = await controller.ObtenerEmpleados();

            // Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        #endregion

        #region ObtenerEmpleado Tests

        [Fact]
        public async Task ObtenerEmpleado_DeberiaRetornarEmpleado_CuandoExisteEmpleado()
        {
            // Arrange
            var empleadoId = 1;

            // Act
            var result = await _controller.ObtenerEmpleado(empleadoId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var empleado = okResult?.Value as Employee;
            empleado.Should().NotBeNull();
            empleado?.BusinessEntityID.Should().Be(empleadoId);
        }

        [Fact]
        public async Task ObtenerEmpleado_DeberiaRetornarNotFound_CuandoNoExisteEmpleado()
        {
            // Arrange
            var empleadoId = 999;

            // Act
            var result = await _controller.ObtenerEmpleado(empleadoId);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region CrearEmpleado Tests

        [Fact]
        public async Task CrearEmpleado_DeberiaCrearEmpleado_CuandoDatosValidos()
        {
            // Arrange
            var nuevoEmpleado = new Employee
            {
                NationalIDNumber = "555666777",
                LoginID = "adventure-works\\test3",
                JobTitle = "Test Developer",
                BirthDate = new DateTime(1990, 3, 15),
                MaritalStatus = "S",
                Gender = "M",
                HireDate = DateTime.UtcNow,
                SalariedFlag = true,
                VacationHours = 0,
                SickLeaveHours = 0,
                CurrentFlag = true
            };

            // Act
            var result = await _controller.CrearEmpleado(nuevoEmpleado);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result.Result as CreatedAtActionResult;
            var empleadoCreado = createdResult?.Value as Employee;
            empleadoCreado.Should().NotBeNull();
            empleadoCreado?.LoginID.Should().Be(nuevoEmpleado.LoginID);
            empleadoCreado?.RowGuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task CrearEmpleado_DeberiaRetornarBadRequest_CuandoModeloInvalido()
        {
            // Arrange
            var empleadoInvalido = new Employee(); // Modelo inválido sin campos requeridos
            _controller.ModelState.AddModelError("NationalIDNumber", "Campo requerido");

            // Act
            var result = await _controller.CrearEmpleado(empleadoInvalido);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        #endregion

        #region ActualizarEmpleado Tests

        [Fact]
        public async Task ActualizarEmpleado_DeberiaActualizarEmpleado_CuandoDatosValidos()
        {
            // Arrange
            var empleadoId = 1;
            var empleadoActualizado = new Employee
            {
                BusinessEntityID = empleadoId,
                NationalIDNumber = "123456789",
                LoginID = "adventure-works\\test1-updated",
                JobTitle = "Senior Test Manager",
                BirthDate = new DateTime(1980, 1, 1),
                MaritalStatus = "M",
                Gender = "M",
                HireDate = new DateTime(2020, 1, 1),
                SalariedFlag = true,
                VacationHours = 45,
                SickLeaveHours = 20,
                CurrentFlag = true
            };

            // Act
            var result = await _controller.ActualizarEmpleado(empleadoId, empleadoActualizado);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            
            // Verificar que se actualizó en la base de datos
            var empleadoEnDb = await Context.Employees.FindAsync(empleadoId);
            empleadoEnDb?.JobTitle.Should().Be("Senior Test Manager");
            empleadoEnDb?.VacationHours.Should().Be(45);
        }

        [Fact]
        public async Task ActualizarEmpleado_DeberiaRetornarBadRequest_CuandoIdNoCoincide()
        {
            // Arrange
            var empleadoId = 1;
            var empleadoActualizado = new Employee { BusinessEntityID = 2 };

            // Act
            var result = await _controller.ActualizarEmpleado(empleadoId, empleadoActualizado);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ActualizarEmpleado_DeberiaRetornarNotFound_CuandoEmpleadoNoExiste()
        {
            // Arrange
            var empleadoId = 999;
            var empleadoActualizado = new Employee { BusinessEntityID = empleadoId };

            // Act
            var result = await _controller.ActualizarEmpleado(empleadoId, empleadoActualizado);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region EliminarEmpleado Tests

        [Fact]
        public async Task EliminarEmpleado_DeberiaMarcarComoInactivo_CuandoEmpleadoExiste()
        {
            // Arrange
            var empleadoId = 1;

            // Act
            var result = await _controller.EliminarEmpleado(empleadoId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            
            // Verificar que se marcó como inactivo
            var empleadoEnDb = await Context.Employees.FindAsync(empleadoId);
            empleadoEnDb?.CurrentFlag.Should().BeFalse();
        }

        [Fact]
        public async Task EliminarEmpleado_DeberiaRetornarNotFound_CuandoEmpleadoNoExiste()
        {
            // Arrange
            var empleadoId = 999;

            // Act
            var result = await _controller.EliminarEmpleado(empleadoId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region ObtenerReporteEmpleadosTiempoDepartamento Tests

        [Fact]
        public async Task ObtenerReporteEmpleadosTiempoDepartamento_DeberiaRetornarReporte_CuandoExistenDatos()
        {
            // Act
            var result = await _controller.ObtenerReporteEmpleadosTiempoDepartamento();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            var reporte = okResult?.Value as List<ReporteEmpleadosDepartamentoDto>;
            reporte.Should().NotBeNull();
        }

        #endregion
    }
}