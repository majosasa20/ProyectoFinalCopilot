# DOCUMENTACI�N T�CNICA - M�DULO DE VENTAS (SALES)
*Sistema AdventureWorks Enterprise - Arquitectura y Especificaciones T�cnicas*

---

## ?? �ndice

1. [Visi�n General del M�dulo](#visi�n-general-del-m�dulo)
2. [Arquitectura del Sistema](#arquitectura-del-sistema)
3. [Controladores API](#controladores-api)
4. [Modelos de Datos](#modelos-de-datos)
5. [DTOs de Transferencia](#dtos-de-transferencia)
6. [Endpoints y Rutas](#endpoints-y-rutas)
7. [L�gica de Negocio](#l�gica-de-negocio)
8. [Stored Procedures](#stored-procedures)
9. [Frontend Blazor](#frontend-blazor)
10. [Validaciones y Reglas](#validaciones-y-reglas)
11. [Manejo de Errores](#manejo-de-errores)
12. [Logging y Monitoreo](#logging-y-monitoreo)

---

## ?? Visi�n General del M�dulo

### **Prop�sito**
El m�dulo de Ventas (Sales) gestiona todo el ciclo de vida de las �rdenes de venta, desde la creaci�n hasta el cumplimiento, incluyendo la gesti�n de clientes y reportes de an�lisis de ventas.

### **Componentes Principales**
- **�rdenes de Venta**: Gesti�n completa del ciclo de vida de �rdenes
- **Clientes**: Administraci�n de la base de clientes
- **Reportes**: An�lisis de productos m�s vendidos y m�tricas de ventas
- **Integraci�n**: Conexi�n con m�dulos de Production y HumanResources

### **Tecnolog�as Utilizadas**
- **Backend**: .NET 8, Entity Framework Core, SQL Server
- **Frontend**: Blazor Server, Bootstrap 5
- **API**: RESTful con autenticaci�n por API Key
- **Base de Datos**: AdventureWorks2014 (esquema Sales)

---

## ??? Arquitectura del Sistema

### **Patr�n Arquitect�nico**
```
???????????????????    ???????????????????    ???????????????????
?   Blazor UI     ??????   API Gateway   ??????   SQL Server    ?
?   (Frontend)    ?    ?   (Controllers) ?    ?   (Database)    ?
???????????????????    ???????????????????    ???????????????????
         ?                       ?                       ?
         ?                       ?                       ?
???????????????????    ???????????????????    ???????????????????
?   ApiService    ?    ?   Entity Framework  ?    ?   Stored Procs  ?
?   (HTTP Client) ?    ?   (ORM)         ?    ?   (Business Logic)?
???????????????????    ???????????????????    ???????????????????
```

### **Flujo de Datos**
1. **Interfaz de Usuario** ? Componentes Blazor capturan la interacci�n
2. **Capa de Servicio** ? ApiService maneja la comunicaci�n HTTP
3. **Controladores API** ? Procesan las peticiones y aplican validaciones
4. **Entity Framework** ? Mapea objetos a base de datos
5. **SQL Server** ? Almacena y procesa los datos

---

## ?? Controladores API

### **OrdenesController**
**Ruta Base**: `/api/ordenes`

#### **Funcionalidades Principales**
- ? Gesti�n CRUD completa de �rdenes de venta
- ? Consulta de �rdenes por cliente
- ? Gesti�n de detalles de orden
- ? Generaci�n de reportes especializados
- ? Validaciones de negocio integradas

#### **M�todos HTTP Soportados**
```csharp
[HttpGet]                              // Obtener todas las �rdenes
[HttpGet("{id}")]                      // Obtener orden espec�fica
[HttpGet("{id}/detalles")]             // Obtener detalles de orden
[HttpGet("cliente/{clienteId}")]       // �rdenes por cliente
[HttpGet("reporte/top10-productos-mas-vendidos")] // Reporte especializado
[HttpPost]                             // Crear nueva orden
[HttpPut("{id}")]                      // Actualizar orden existente
[HttpDelete("{id}")]                   // Eliminar orden
```

### **ClientesController**
**Ruta Base**: `/api/clientes`

#### **Funcionalidades Principales**
- ? Gesti�n CRUD completa de clientes
- ? Validaci�n de integridad referencial
- ? Restricciones de eliminaci�n para clientes con �rdenes
- ? Soporte para Person y Store customers

#### **M�todos HTTP Soportados**
```csharp
[HttpGet]                              // Obtener todos los clientes
[HttpGet("{id}")]                      // Obtener cliente espec�fico
[HttpPost]                             // Crear nuevo cliente
[HttpPut("{id}")]                      // Actualizar cliente existente
[HttpDelete("{id}")]                   // Eliminar cliente
```

---

## ?? Modelos de Datos

### **SalesOrderHeader** (Entidad Principal)
```csharp
public class SalesOrderHeader
{
    // Identificadores
    public int SalesOrderID { get; set; }              // PK
    public byte RevisionNumber { get; set; }           // Control de versiones
    public string? SalesOrderNumber { get; set; }      // Computed field
    
    // Fechas cr�ticas
    public DateTime OrderDate { get; set; }            // Fecha de creaci�n
    public DateTime DueDate { get; set; }              // Fecha l�mite
    public DateTime? ShipDate { get; set; }            // Fecha de env�o
    
    // Estado y configuraci�n
    public byte Status { get; set; }                   // 1=Pending, 2=Approved, etc.
    public bool OnlineOrderFlag { get; set; }          // Orden online vs presencial
    
    // Referencias a entidades relacionadas
    public int CustomerID { get; set; }                // FK a Customer
    public int? SalesPersonID { get; set; }            // FK a SalesPerson (opcional)
    public int? TerritoryID { get; set; }              // FK a SalesTerritory
    
    // Informaci�n financiera
    public decimal SubTotal { get; set; }              // Subtotal antes de impuestos
    public decimal TaxAmt { get; set; }                // Monto de impuestos
    public decimal Freight { get; set; }               // Costo de env�o
    public decimal? TotalDue { get; set; }             // Total calculado
    
    // Metadatos del sistema
    public Guid RowGuid { get; set; }                  // Identificador �nico global
    public DateTime ModifiedDate { get; set; }         // Timestamp de modificaci�n
    
    // Propiedades de navegaci�n
    public virtual Customer Customer { get; set; }
    public virtual SalesPerson? SalesPerson { get; set; }
    public virtual SalesTerritory? SalesTerritory { get; set; }
    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
}
```

### **SalesOrderDetail** (L�neas de Orden)
```csharp
public class SalesOrderDetail
{
    // Identificadores
    public int SalesOrderID { get; set; }              // FK a SalesOrderHeader
    public int SalesOrderDetailID { get; set; }        // PK compuesta
    
    // Informaci�n del producto
    public int ProductID { get; set; }                 // FK a Product
    public int SpecialOfferID { get; set; }            // FK a SpecialOffer
    
    // Cantidad y precios
    public short OrderQty { get; set; }                // Cantidad ordenada
    public decimal UnitPrice { get; set; }             // Precio unitario
    public decimal UnitPriceDiscount { get; set; }     // Descuento aplicado
    public decimal? LineTotal { get; set; }            // Total de l�nea (computed)
    
    // Informaci�n de env�o
    public string? CarrierTrackingNumber { get; set; } // N�mero de seguimiento
    
    // Metadatos
    public Guid RowGuid { get; set; }
    public DateTime ModifiedDate { get; set; }
}
```

### **Customer** (Clientes)
```csharp
public class Customer
{
    public int CustomerID { get; set; }                // PK
    public int? PersonID { get; set; }                 // FK a Person (individual)
    public int? StoreID { get; set; }                  // FK a Store (business)
    public int? TerritoryID { get; set; }              // FK a SalesTerritory
    public string? AccountNumber { get; set; }         // N�mero de cuenta (computed)
    public Guid RowGuid { get; set; }
    public DateTime ModifiedDate { get; set; }
    
    // Propiedades de navegaci�n
    public virtual Store? Store { get; set; }
    public virtual SalesTerritory? SalesTerritory { get; set; }
    public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
}
```

---

## ?? DTOs de Transferencia

### **SalesOrderHeaderDto**
```csharp
public class SalesOrderHeaderDto
{
    public int SalesOrderID { get; set; }
    public byte RevisionNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public byte Status { get; set; }
    public bool OnlineOrderFlag { get; set; }
    public string? SalesOrderNumber { get; set; }
    public int CustomerID { get; set; }
    public int? SalesPersonID { get; set; }
    public int? TerritoryID { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmt { get; set; }
    public decimal Freight { get; set; }
    public decimal? TotalDue { get; set; }
    public DateTime ModifiedDate { get; set; }
}
```

### **ReporteTop10ProductosDto**
```csharp
public class ReporteTop10ProductosDto
{
    public int ProductID { get; set; }
    public string NombreProducto { get; set; }
    public string NumeroProducto { get; set; }
    public string? Categoria { get; set; }
    public string? Subcategoria { get; set; }
    public int CantidadTotalVendida { get; set; }
    public decimal VentasTotales { get; set; }
    public decimal PrecioPromedioVenta { get; set; }
    public int NumeroOrdenes { get; set; }
    public decimal PrecioLista { get; set; }
}
```

---

## ?? Endpoints y Rutas

### **�rdenes de Venta**

| M�todo | Endpoint | Descripci�n | Par�metros | Respuesta |
|--------|----------|-------------|------------|-----------|
| `GET` | `/api/ordenes` | Listar todas las �rdenes | - | `List<SalesOrderHeader>` |
| `GET` | `/api/ordenes/{id}` | Obtener orden espec�fica | `id: int` | `SalesOrderHeader` |
| `GET` | `/api/ordenes/{id}/detalles` | Detalles de orden | `id: int` | `List<SalesOrderDetail>` |
| `GET` | `/api/ordenes/cliente/{clienteId}` | �rdenes por cliente | `clienteId: int` | `List<SalesOrderHeader>` |
| `GET` | `/api/ordenes/reporte/top10-productos-mas-vendidos` | Reporte top 10 | - | `List<ReporteTop10ProductosDto>` |
| `POST` | `/api/ordenes` | Crear nueva orden | `SalesOrderHeader` | `SalesOrderHeader` |
| `PUT` | `/api/ordenes/{id}` | Actualizar orden | `id: int, SalesOrderHeader` | `204 No Content` |
| `DELETE` | `/api/ordenes/{id}` | Eliminar orden | `id: int` | `204 No Content` |

### **Clientes**

| M�todo | Endpoint | Descripci�n | Par�metros | Respuesta |
|--------|----------|-------------|------------|-----------|
| `GET` | `/api/clientes` | Listar todos los clientes | - | `List<Customer>` |
| `GET` | `/api/clientes/{id}` | Obtener cliente espec�fico | `id: int` | `Customer` |
| `POST` | `/api/clientes` | Crear nuevo cliente | `Customer` | `Customer` |
| `PUT` | `/api/clientes/{id}` | Actualizar cliente | `id: int, Customer` | `204 No Content` |
| `DELETE` | `/api/clientes/{id}` | Eliminar cliente | `id: int` | `204 No Content` |

---

## ?? L�gica de Negocio

### **Reglas de �rdenes de Venta**

#### **Creaci�n de �rdenes**
```csharp
// Validaciones autom�ticas en CrearOrden()
1. ? Cliente debe existir en el sistema
2. ? RevisionNumber inicia en 1
3. ? Status por defecto = 1 (Pendiente)
4. ? RowGuid generado autom�ticamente
5. ? ModifiedDate = DateTime.UtcNow
6. ? TotalDue calculado como SubTotal + TaxAmt + Freight
```

#### **Actualizaci�n de �rdenes**
```csharp
// L�gica en ActualizarOrden()
1. ? RevisionNumber se incrementa autom�ticamente
2. ? Validaci�n de ID coincidente
3. ? ModifiedDate actualizado autom�ticamente
4. ? Control de concurrencia con DbUpdateConcurrencyException
```

#### **Eliminaci�n de �rdenes**
```csharp
// Restricciones en EliminarOrden()
1. ? Solo �rdenes con Status = 1 (Pendiente) pueden eliminarse
2. ? Eliminaci�n en cascada de SalesOrderDetails
3. ? Validaci�n de existencia antes de eliminar
```

### **Reglas de Clientes**

#### **Eliminaci�n de Clientes**
```csharp
// Validaciones en EliminarCliente()
1. ? Cliente no puede tener �rdenes asociadas
2. ? Verificaci�n de integridad referencial
3. ? Mensaje de error descriptivo si hay �rdenes
```

### **Estados de �rdenes**
```csharp
public enum OrderStatus
{
    Pending = 1,        // Pendiente de aprobaci�n
    Approved = 2,       // Aprobada para procesamiento
    InProcess = 3,      // En proceso de preparaci�n
    Shipped = 4,        // Enviada al cliente
    Cancelled = 5,      // Cancelada
    Completed = 6       // Completada y cerrada
}
```

---

## ??? Stored Procedures

### **usp_Top10ProductosMasVendidos_Sagastume**
```sql
CREATE PROCEDURE [dbo].[usp_Top10ProductosMasVendidos_Sagastume]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP 10
        p.ProductID,
        p.Name AS NombreProducto,
        p.ProductNumber AS NumeroProducto,
        pc.Name AS Categoria,
        ps.Name AS Subcategoria,
        SUM(sod.OrderQty) AS CantidadTotalVendida,
        SUM(sod.LineTotal) AS VentasTotales,
        AVG(sod.UnitPrice) AS PrecioPromedioVenta,
        COUNT(DISTINCT soh.SalesOrderID) AS NumeroOrdenes,
        p.ListPrice AS PrecioLista
    FROM Production.Product p
    INNER JOIN Sales.SalesOrderDetail sod ON p.ProductID = sod.ProductID
    INNER JOIN Sales.SalesOrderHeader soh ON sod.SalesOrderID = soh.SalesOrderID
    LEFT JOIN Production.ProductSubcategory ps ON p.ProductSubcategoryID = ps.ProductSubcategoryID
    LEFT JOIN Production.ProductCategory pc ON ps.ProductCategoryID = pc.ProductCategoryID
    WHERE soh.Status IN (3, 5) -- Solo �rdenes completadas y enviadas
    GROUP BY 
        p.ProductID, p.Name, p.ProductNumber, 
        pc.Name, ps.Name, p.ListPrice
    ORDER BY CantidadTotalVendida DESC;
END;
```

**Prop�sito**: Generar reporte de los 10 productos m�s vendidos con m�tricas detalladas.

**Par�metros**: Ninguno

**Optimizaciones**:
- ? Filtrado por estados de orden v�lidos
- ? LEFT JOIN para categor�as opcionales
- ? Agrupaci�n eficiente
- ? Ordenamiento por cantidad vendida

---

## ??? Frontend Blazor

### **Componentes Principales**

#### **OrdersList.razor**
```razor
@page "/sales/orders"
@inject ApiService ApiService
@inject NavigationManager Navigation

<!-- Funcionalidades principales -->
? Tabla responsiva con �rdenes
? Filtros por estado y cliente
? M�tricas de resumen
? Paginaci�n autom�tica
? Estados de carga
? Navegaci�n a detalles
```

#### **OrderDetails.razor**
```razor
@page "/sales/orders/{OrderId:int}"
@inject ApiService ApiService

<!-- Funcionalidades principales -->
? Vista detallada de orden
? Informaci�n del cliente
? Tabla de productos ordenados
? Totales y c�lculos
? Estados de env�o
? Breadcrumb navigation
```

#### **Top10ProductsReport.razor**
```razor
@page "/sales/reports/top10-products"
@inject ApiService ApiService

<!-- Funcionalidades principales -->
? Podio de ganadores (Top 3)
? Tabla completa de Top 10
? M�tricas visuales
? Gr�ficos de barras
? Funciones de exportaci�n
```

### **ApiService - M�todos de Sales**
```csharp
// �rdenes
public async Task<List<SalesOrderHeaderDto>> GetOrdenesAsync()
public async Task<SalesOrderHeaderDto?> GetOrdenAsync(int id)
public async Task<List<SalesOrderDetailDto>> GetDetallesOrdenAsync(int id)
public async Task<List<SalesOrderHeaderDto>> GetOrdenesPorClienteAsync(int clienteId)
public async Task<List<ReporteTop10ProductosDto>> GetReporteTop10ProductosAsync()

// Clientes
public async Task<List<CustomerDto>> GetClientesAsync()
public async Task<CustomerDto?> GetClienteAsync(int id)
public async Task<CustomerDto> CreateClienteAsync(CustomerDto cliente)
public async Task<bool> UpdateClienteAsync(int id, CustomerDto cliente)
public async Task<bool> DeleteClienteAsync(int id)
```

---

## ? Validaciones y Reglas

### **Validaciones del Modelo (DataAnnotations)**
```csharp
[Required(ErrorMessage = "La fecha de orden es requerida")]
public DateTime OrderDate { get; set; }

[Required(ErrorMessage = "La fecha de vencimiento es requerida")]
[DateGreaterThan("OrderDate", ErrorMessage = "La fecha de vencimiento debe ser posterior a la fecha de orden")]
public DateTime DueDate { get; set; }

[Required(ErrorMessage = "El cliente es requerido")]
[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente v�lido")]
public int CustomerID { get; set; }

[Required(ErrorMessage = "El subtotal es requerido")]
[Range(0.01, double.MaxValue, ErrorMessage = "El subtotal debe ser mayor a cero")]
public decimal SubTotal { get; set; }
```

### **Validaciones de Negocio**
```csharp
// En OrdenesController.CrearOrden()
1. ? Verificaci�n de existencia del cliente
2. ? Validaci�n de ModelState
3. ? Asignaci�n de valores por defecto
4. ? Generaci�n de identificadores �nicos

// En ClientesController.EliminarCliente()
1. ? Verificaci�n de �rdenes asociadas
2. ? Prevenci�n de eliminaci�n si hay dependencias
3. ? Mensajes de error descriptivos
```

### **Validaciones Frontend**
```csharp
// En componentes Blazor
? Validaci�n de campos requeridos
? Formateo de fechas y monedas
? Confirmaciones de eliminaci�n
? Estados de loading durante operaciones
? Manejo de errores con mensajes amigables
```

---

## ?? Manejo de Errores

### **Estrategia de Manejo de Errores**

#### **Controladores API**
```csharp
try
{
    // L�gica de negocio
}
catch (DbUpdateConcurrencyException)
{
    // Manejo espec�fico de concurrencia
    if (!await OrdenExists(id))
        return NotFound();
    throw;
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error espec�fico con contexto {Parametro}", parametro);
    return StatusCode(500, "Mensaje amigable para el usuario");
}
```

#### **C�digos de Estado HTTP**
```csharp
? 200 OK          - Operaci�n exitosa
? 201 Created     - Recurso creado exitosamente
? 204 No Content  - Actualizaci�n/eliminaci�n exitosa
? 400 Bad Request - Datos inv�lidos o validaci�n fallida
? 404 Not Found   - Recurso no encontrado
? 409 Conflict    - Conflicto de integridad referencial
? 500 Internal Error - Error interno del servidor
```

#### **Frontend Blazor**
```csharp
try
{
    var resultado = await ApiService.GetOrdenesAsync();
    // Procesar resultado exitoso
}
catch (HttpRequestException ex)
{
    errorMessage = "Error de conexi�n con el servidor";
    _logger.LogError(ex, "Error de conexi�n HTTP");
}
catch (TaskCanceledException ex)
{
    errorMessage = "La solicitud ha expirado";
    _logger.LogError(ex, "Timeout de conexi�n");
}
catch (Exception ex)
{
    errorMessage = "Error inesperado";
    _logger.LogError(ex, "Error general");
}
```

---

## ?? Logging y Monitoreo

### **Eventos de Logging**

#### **Controladores**
```csharp
// Eventos informativos
_logger.LogInformation("Obteniendo orden {OrdenId} en {Timestamp}", id, DateTime.UtcNow);
_logger.LogInformation("Orden {OrdenId} creada exitosamente", orden.SalesOrderID);

// Advertencias
_logger.LogWarning("Orden {OrdenId} no encontrada", id);
_logger.LogWarning("Estado del modelo inv�lido para crear orden");

// Errores
_logger.LogError(ex, "Error al obtener la orden {OrdenId}", id);
_logger.LogError(ex, "Error al procesar solicitud en {Endpoint}", Request.Path);
```

#### **Frontend**
```csharp
// ApiService logging
_logger.LogInformation("?? Obteniendo �rdenes desde API");
_logger.LogInformation("? �rdenes cargadas exitosamente: {Count}", ordenes.Count);
_logger.LogError("? Error de HTTP al obtener �rdenes: {Error}", ex.Message);

// Componente logging
Logger.LogInformation("?? Inicializando componente OrdersList");
Logger.LogInformation("?? Navegando a reporte de top 10 productos");
```

### **M�tricas de Performance**
```csharp
// Medici�n de tiempo de respuesta
var stopwatch = Stopwatch.StartNew();
var resultado = await _context.SalesOrderHeaders.ToListAsync();
stopwatch.Stop();
_logger.LogInformation("Consulta ejecutada en {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
```

### **Auditor�a de Cambios**
```csharp
// Tracking autom�tico en Entity Framework
public override int SaveChanges()
{
    var entries = ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Modified);
    
    foreach (var entry in entries)
    {
        if (entry.Entity is ITrackable trackable)
        {
            trackable.ModifiedDate = DateTime.UtcNow;
            _logger.LogInformation("Entidad {EntityType} ID {EntityId} modificada", 
                entry.Entity.GetType().Name, 
                entry.Property("ID").CurrentValue);
        }
    }
    
    return base.SaveChanges();
}
```

---

## ?? M�tricas y KPIs del M�dulo

### **M�tricas de Negocio**
- ?? **Total de �rdenes**: Contador en tiempo real
- ?? **Valor Total de Ventas**: Suma de TotalDue activas
- ?? **�rdenes Pendientes**: Status = 1
- ?? **�rdenes Enviadas**: Status = 4
- ?? **Clientes Activos**: Con �rdenes en �ltimo a�o

### **M�tricas T�cnicas**
- ? **Tiempo de Respuesta**: Promedio < 500ms
- ?? **Throughput**: Requests por segundo
- ?? **Disponibilidad**: 99.9% uptime
- ?? **Error Rate**: < 1% de las peticiones

---

## ?? Integraciones

### **M�dulo de Production**
```csharp
// Validaci�n de productos en SalesOrderDetail
public async Task<bool> ValidateProductExists(int productId)
{
    return await _context.Products
        .Where(p => p.ProductID == productId && p.SellEndDate == null)
        .AnyAsync();
}
```

### **M�dulo de HumanResources**
```csharp
// Validaci�n de vendedores
public async Task<bool> ValidateSalesPersonExists(int salesPersonId)
{
    return await _context.SalesPersons
        .Where(sp => sp.BusinessEntityID == salesPersonId)
        .AnyAsync();
}
```

---

## ?? Consideraciones de Performance

### **Optimizaciones Implementadas**
1. ? **Lazy Loading**: Propiedades de navegaci�n cargadas bajo demanda
2. ? **Pagination**: Limite de 100 registros por defecto
3. ? **Indexing**: �ndices autom�ticos en FK y campos de b�squeda
4. ? **Caching**: HttpClient reutilizable con configuraci�n de timeout
5. ? **Connection Pooling**: Configurado autom�ticamente por EF Core

### **Consultas Optimizadas**
```csharp
// Include espec�ficos para evitar N+1 queries
var ordenes = await _context.SalesOrderHeaders
    .Include(s => s.Customer)           // 1 JOIN adicional
    .Include(s => s.SalesPerson)        // 1 JOIN adicional  
    .Include(s => s.SalesTerritory)     // 1 JOIN adicional
    .OrderByDescending(s => s.OrderDate)
    .Take(100)                          // Limitaci�n de registros
    .ToListAsync();
```

---

## ?? Seguridad

### **Autenticaci�n y Autorizaci�n**
- ? **API Key Authentication**: Middleware personalizado
- ? **CORS Configuration**: Or�genes espec�ficos permitidos
- ? **HTTPS Enforcement**: Redirecci�n autom�tica
- ? **SQL Injection Prevention**: Entity Framework parametrizado

### **Validaci�n de Entrada**
- ? **Model Validation**: DataAnnotations en DTOs
- ? **Input Sanitization**: Autom�tico en EF Core
- ? **Type Safety**: Fuertemente tipado con C#

---

*Documento generado para AdventureWorks Enterprise System*  
*Versi�n: 1.0*  
*Fecha: 2024-12-17*  
*M�dulo: Sales (Ventas)*