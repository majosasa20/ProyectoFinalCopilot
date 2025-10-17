using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AdventureWorks.Enterprise.Api.Data;

namespace AdventureWorks.Enterprise.Api.Tests.Infrastructure
{
    /// <summary>
    /// Clase base para tests de controladores con configuración común
    /// </summary>
    public abstract class BaseControllerTest : IDisposable
    {
        protected readonly AdventureWorksDbContext Context;
        protected readonly Mock<ILogger> MockLogger;

        protected BaseControllerTest()
        {
            // Configurar DbContext en memoria
            var options = new DbContextOptionsBuilder<AdventureWorksDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new AdventureWorksDbContext(options);
            MockLogger = new Mock<ILogger>();
        }

        /// <summary>
        /// Crear un mock genérico de ILogger para el tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo para el logger</typeparam>
        /// <returns>Mock del logger</returns>
        protected Mock<ILogger<T>> CreateMockLogger<T>()
        {
            return new Mock<ILogger<T>>();
        }

        /// <summary>
        /// Sembrar datos de prueba en la base de datos
        /// </summary>
        protected virtual void SeedTestData()
        {
            // Employees
            Context.Employees.AddRange(
                new Employee
                {
                    BusinessEntityID = 1,
                    NationalIDNumber = "123456789",
                    LoginID = "adventure-works\\test1",
                    JobTitle = "Test Manager",
                    BirthDate = new DateTime(1980, 1, 1),
                    MaritalStatus = "M",
                    Gender = "M",
                    HireDate = new DateTime(2020, 1, 1),
                    SalariedFlag = true,
                    VacationHours = 40,
                    SickLeaveHours = 20,
                    CurrentFlag = true,
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                },
                new Employee
                {
                    BusinessEntityID = 2,
                    NationalIDNumber = "987654321",
                    LoginID = "adventure-works\\test2",
                    JobTitle = "Test Analyst",
                    BirthDate = new DateTime(1985, 5, 15),
                    MaritalStatus = "S",
                    Gender = "F",
                    HireDate = new DateTime(2021, 6, 1),
                    SalariedFlag = false,
                    VacationHours = 35,
                    SickLeaveHours = 15,
                    CurrentFlag = true,
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                }
            );

            // Departments
            Context.Departments.AddRange(
                new Department
                {
                    DepartmentID = 1,
                    Name = "IT",
                    GroupName = "Technology",
                    ModifiedDate = DateTime.UtcNow
                },
                new Department
                {
                    DepartmentID = 2,
                    Name = "HR",
                    GroupName = "Administration",
                    ModifiedDate = DateTime.UtcNow
                }
            );

            // Products
            Context.Products.AddRange(
                new Product
                {
                    ProductID = 1,
                    Name = "Test Product 1",
                    ProductNumber = "TEST-001",
                    MakeFlag = true,
                    FinishedGoodsFlag = true,
                    Color = "Red",
                    SafetyStockLevel = 50,
                    ReorderPoint = 25,
                    StandardCost = 100.00m,
                    ListPrice = 150.00m,
                    DaysToManufacture = 5,
                    SellStartDate = DateTime.UtcNow.AddYears(-1),
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                },
                new Product
                {
                    ProductID = 2,
                    Name = "Test Product 2",
                    ProductNumber = "TEST-002",
                    MakeFlag = false,
                    FinishedGoodsFlag = true,
                    Color = "Blue",
                    SafetyStockLevel = 30,
                    ReorderPoint = 15,
                    StandardCost = 75.00m,
                    ListPrice = 120.00m,
                    DaysToManufacture = 3,
                    SellStartDate = DateTime.UtcNow.AddYears(-2),
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                }
            );

            // Customers
            Context.Customers.AddRange(
                new Customer
                {
                    CustomerID = 1,
                    PersonID = null,
                    StoreID = null,
                    TerritoryID = 1,
                    AccountNumber = "AW00000001",
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                },
                new Customer
                {
                    CustomerID = 2,
                    PersonID = null,
                    StoreID = null,
                    TerritoryID = 2,
                    AccountNumber = "AW00000002",
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                }
            );

            // Sales Order Headers
            Context.SalesOrderHeaders.AddRange(
                new SalesOrderHeader
                {
                    SalesOrderID = 1,
                    RevisionNumber = 1,
                    OrderDate = DateTime.UtcNow.AddDays(-10),
                    DueDate = DateTime.UtcNow.AddDays(5),
                    ShipDate = DateTime.UtcNow.AddDays(-5),
                    Status = 5,
                    OnlineOrderFlag = true,
                    SalesOrderNumber = "SO000001",
                    CustomerID = 1,
                    BillToAddressID = 1,
                    ShipToAddressID = 1,
                    ShipMethodID = 1,
                    SubTotal = 1000.00m,
                    TaxAmt = 80.00m,
                    Freight = 20.00m,
                    TotalDue = 1100.00m,
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                },
                new SalesOrderHeader
                {
                    SalesOrderID = 2,
                    RevisionNumber = 1,
                    OrderDate = DateTime.UtcNow.AddDays(-5),
                    DueDate = DateTime.UtcNow.AddDays(10),
                    Status = 1,
                    OnlineOrderFlag = false,
                    SalesOrderNumber = "SO000002",
                    CustomerID = 2,
                    BillToAddressID = 2,
                    ShipToAddressID = 2,
                    ShipMethodID = 1,
                    SubTotal = 750.00m,
                    TaxAmt = 60.00m,
                    Freight = 15.00m,
                    TotalDue = 825.00m,
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                }
            );

            Context.SaveChanges();
        }

        public virtual void Dispose()
        {
            Context?.Dispose();
        }
    }
}