# DOCUMENTACIÓN TÉCNICA - MÓDULO DE VENTAS (SALES)
*Sistema AdventureWorks Enterprise - Arquitectura y Especificaciones Técnicas*

---

## ?? Índice

1. [Visión General del Módulo](#visión-general-del-módulo)
2. [Arquitectura del Sistema](#arquitectura-del-sistema)
3. [Controladores API](#controladores-api)
4. [Modelos de Datos](#modelos-de-datos)
5. [DTOs de Transferencia](#dtos-de-transferencia)
6. [Endpoints y Rutas](#endpoints-y-rutas)
7. [Lógica de Negocio](#lógica-de-negocio)
8. [Stored Procedures](#stored-procedures)
9. [Frontend Blazor](#frontend-blazor)
10. [Validaciones y Reglas](#validaciones-y-reglas)
11. [Manejo de Errores](#manejo-de-errores)
12. [Logging y Monitoreo](#logging-y-monitoreo)

---

## ?? Visión General del Módulo

### **Propósito**
El módulo de Ventas (Sales) gestiona todo el ciclo de vida de las órdenes de venta, desde la creación hasta el cumplimiento, incluyendo la gestión de clientes y reportes de análisis de ventas.

### **Componentes Principales**
- **Órdenes de Venta**: Gestión completa del ciclo de vida de órdenes
- **Clientes**: Administración de la base de clientes
- **Reportes**: Análisis de productos más vendidos y métricas de ventas
- **Integración**: Conexión con módulos de Production y HumanResources

### **Tecnologías Utilizadas**
- **Backend**: .NET 8, Entity Framework Core, SQL Server
- **Frontend**: Blazor Server, Bootstrap 5
- **API**: RESTful con autenticación por API Key
- **Base de Datos**: AdventureWorks2014 (esquema Sales)

---

## ??? Arquitectura del Sistema

### **Patrón Arquitectónico**
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
1. **Interfaz de Usuario** ? Componentes Blazor capturan la interacción
2. **Capa de Servicio** ? ApiService maneja la comunicación HTTP
3. **Controladores API** ? Procesan las peticiones y aplican validaciones
4. **Entity Framework** ? Mapea objetos a base de datos
5. **SQL Server** ? Almacena y procesa los datos

---

## ?? Controladores API

### **OrdenesController**
**Ruta Base**: `/api/ordenes`

#### **Funcionalidades Principales**
- ? Gestión CRUD completa de órdenes de venta
- ? Consulta de órdenes por cliente
- ? Gestión de detalles de orden
- ? Generación de reportes especializados
- ? Validaciones de negocio integradas

#### **Métodos HTTP Soportados**
```csharp
[HttpGet]                              // Obtener todas las órdenes
[HttpGet("{id}")]                      // Obtener orden específica
[HttpGet("{id}/detalles")]             // Obtener detalles de orden
[HttpGet("cliente/{clienteId}")]       // Órdenes por cliente
[HttpGet("reporte/top10-productos-mas-vendidos")] // Reporte especializado
[HttpPost]                             // Crear nueva orden
[HttpPut("{id}")]                      // Actualizar orden existente
[HttpDelete("{id}")]                   // Eliminar orden
```

### **ClientesController**
**Ruta Base**: `/api/clientes`

#### **Funcionalidades Principales**
- ? Gestión CRUD completa de clientes
- ? Validación de integridad referencial
- ? Restricciones de eliminación para clientes con órdenes
- ? Soporte para Person y Store customers

#### **Métodos HTTP Soportados**
```csharp
[HttpGet]                              // Obtener todos los clientes
[HttpGet("{id}")]                      // Obtener cliente específico
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
    
    // Fechas críticas
    public DateTime OrderDate { get; set; }            // Fecha de creación
    public DateTime DueDate { get; set; }              // Fecha límite
    public DateTime? ShipDate { get; set; }            // Fecha de envío
    
    // Estado y configuración
    public byte Status { get; set; }                   // 1=Pending, 2=Approved, etc.
    public bool OnlineOrderFlag { get; set; }          // Orden online vs presencial
    
    // Referencias a entidades relacionadas
    public int CustomerID { get; set; }                // FK a Customer
    public int? SalesPersonID { get; set; }            // FK a SalesPerson (opcional)
    public int? TerritoryID { get; set; }              // FK a SalesTerritory
    
    // Información financiera
    public decimal SubTotal { get; set; }              // Subtotal antes de impuestos
    public decimal TaxAmt { get; set; }                // Monto de impuestos
    public decimal Freight { get; set; }               // Costo de envío
    public decimal? TotalDue { get; set; }             // Total calculado
    
    // Metadatos del sistema
    public Guid RowGuid { get; set; }                  // Identificador único global
    public DateTime ModifiedDate { get; set; }         // Timestamp de modificación
    
    // Propiedades de navegación
    public virtual Customer Customer { get; set; }
    public virtual SalesPerson? SalesPerson { get; set; }
    public virtual SalesTerritory? SalesTerritory { get; set; }
    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
}
```

### **SalesOrderDetail** (Líneas de Orden)
```csharp
public class SalesOrderDetail
{
    // Identificadores
    public int SalesOrderID { get; set; }              // FK a SalesOrderHeader
    public int SalesOrderDetailID { get; set; }        // PK compuesta
    
    // Información del producto
    public int ProductID { get; set; }                 // FK a Product
    public int SpecialOfferID { get; set; }            // FK a SpecialOffer
    
    // Cantidad y precios
    public short OrderQty { get; set; }                // Cantidad ordenada
    public decimal UnitPrice { get; set; }             // Precio unitario
    public decimal UnitPriceDiscount { get; set; }     // Descuento aplicado
    public decimal? LineTotal { get; set; }            // Total de línea (computed)
    
    // Información de envío
    public string? CarrierTrackingNumber { get; set; } // Número de seguimiento
    
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
    public string? AccountNumber { get; set; }         // Número de cuenta (computed)
    public Guid RowGuid { get; set; }
    public DateTime ModifiedDate { get; set; }
    
    // Propiedades de navegación
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

### **Órdenes de Venta**

| Método | Endpoint | Descripción | Parámetros | Respuesta |
|--------|----------|-------------|------------|-----------|
| `GET` | `/api/ordenes` | Listar todas las órdenes | - | `List<SalesOrderHeader>` |
| `GET` | `/api/ordenes/{id}` | Obtener orden específica | `id: int` | `SalesOrderHeader` |
| `GET` | `/api/ordenes/{id}/detalles` | Detalles de orden | `id: int` | `List<SalesOrderDetail>` |
| `GET` | `/api/ordenes/cliente/{clienteId}` | Órdenes por cliente | `clienteId: int` | `List<SalesOrderHeader>` |
| `GET` | `/api/ordenes/reporte/top10-productos-mas-vendidos` | Reporte top 10 | - | `List<ReporteTop10ProductosDto>` |
| `POST` | `/api/ordenes` | Crear nueva orden | `SalesOrderHeader` | `SalesOrderHeader` |
| `PUT` | `/api/ordenes/{id}` | Actualizar orden | `id: int, SalesOrderHeader` | `204 No Content` |
| `DELETE` | `/api/ordenes/{id}` | Eliminar orden | `id: int` | `204 No Content` |

### **Clientes**

| Método | Endpoint | Descripción | Parámetros | Respuesta |
|--------|----------|-------------|------------|-----------|
| `GET` | `/api/clientes` | Listar todos los clientes | - | `List<Customer>` |
| `GET` | `/api/clientes/{id}` | Obtener cliente específico | `id: int` | `Customer` |
| `POST` | `/api/clientes` | Crear nuevo cliente | `Customer` | `Customer` |
| `PUT` | `/api/clientes/{id}` | Actualizar cliente | `id: int, Customer` | `204 No Content` |
| `DELETE` | `/api/clientes/{id}` | Eliminar cliente | `id: int` | `204 No Content` |

---

## ?? Lógica de Negocio

### **Reglas de Órdenes de Venta**

#### **Creación de Órdenes**
```csharp
// Validaciones automáticas en CrearOrden()
1. ? Cliente debe existir en el sistema
2. ? RevisionNumber inicia en 1
3. ? Status por defecto = 1 (Pendiente)
4. ? RowGuid generado automáticamente
5. ? ModifiedDate = DateTime.UtcNow
6. ? TotalDue calculado como SubTotal + TaxAmt + Freight
```

#### **Actualización de Órdenes**
```csharp
// Lógica en ActualizarOrden()
1. ? RevisionNumber se incrementa automáticamente
2. ? Validación de ID coincidente
3. ? ModifiedDate actualizado automáticamente
4. ? Control de concurrencia con DbUpdateConcurrencyException
```

#### **Eliminación de Órdenes**
```csharp
// Restricciones en EliminarOrden()
1. ? Solo órdenes con Status = 1 (Pendiente) pueden eliminarse
2. ? Eliminación en cascada de SalesOrderDetails
3. ? Validación de existencia antes de eliminar
```

### **Reglas de Clientes**

#### **Eliminación de Clientes**
```csharp
// Validaciones en EliminarCliente()
1. ? Cliente no puede tener órdenes asociadas
2. ? Verificación de integridad referencial
3. ? Mensaje de error descriptivo si hay órdenes
```

### **Estados de Órdenes**
```csharp
public enum OrderStatus
{
    Pending = 1,        // Pendiente de aprobación
    Approved = 2,       // Aprobada para procesamiento
    InProcess = 3,      // En proceso de preparación
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
    WHERE soh.Status IN (3, 5) -- Solo órdenes completadas y enviadas
    GROUP BY 
        p.ProductID, p.Name, p.ProductNumber, 
        pc.Name, ps.Name, p.ListPrice
    ORDER BY CantidadTotalVendida DESC;
END;
```

**Propósito**: Generar reporte de los 10 productos más vendidos con métricas detalladas.

**Parámetros**: Ninguno

**Optimizaciones**:
- ? Filtrado por estados de orden válidos
- ? LEFT JOIN para categorías opcionales
- ? Agrupación eficiente
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
? Tabla responsiva con órdenes
? Filtros por estado y cliente
? Métricas de resumen
? Paginación automática
? Estados de carga
? Navegación a detalles
```

#### **OrderDetails.razor**
```razor
@page "/sales/orders/{OrderId:int}"
@inject ApiService ApiService

<!-- Funcionalidades principales -->
? Vista detallada de orden
? Información del cliente
? Tabla de productos ordenados
? Totales y cálculos
? Estados de envío
? Breadcrumb navigation
```

#### **Top10ProductsReport.razor**
```razor
@page "/sales/reports/top10-products"
@inject ApiService ApiService

<!-- Funcionalidades principales -->
? Podio de ganadores (Top 3)
? Tabla completa de Top 10
? Métricas visuales
? Gráficos de barras
? Funciones de exportación
```

### **ApiService - Métodos de Sales**
```csharp
// Órdenes
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
[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente válido")]
public int CustomerID { get; set; }

[Required(ErrorMessage = "El subtotal es requerido")]
[Range(0.01, double.MaxValue, ErrorMessage = "El subtotal debe ser mayor a cero")]
public decimal SubTotal { get; set; }
```

### **Validaciones de Negocio**
```csharp
// En OrdenesController.CrearOrden()
1. ? Verificación de existencia del cliente
2. ? Validación de ModelState
3. ? Asignación de valores por defecto
4. ? Generación de identificadores únicos

// En ClientesController.EliminarCliente()
1. ? Verificación de órdenes asociadas
2. ? Prevención de eliminación si hay dependencias
3. ? Mensajes de error descriptivos
```

### **Validaciones Frontend**
```csharp
// En componentes Blazor
? Validación de campos requeridos
? Formateo de fechas y monedas
? Confirmaciones de eliminación
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
    // Lógica de negocio
}
catch (DbUpdateConcurrencyException)
{
    // Manejo específico de concurrencia
    if (!await OrdenExists(id))
        return NotFound();
    throw;
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error específico con contexto {Parametro}", parametro);
    return StatusCode(500, "Mensaje amigable para el usuario");
}
```

#### **Códigos de Estado HTTP**
```csharp
? 200 OK          - Operación exitosa
? 201 Created     - Recurso creado exitosamente
? 204 No Content  - Actualización/eliminación exitosa
? 400 Bad Request - Datos inválidos o validación fallida
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
    errorMessage = "Error de conexión con el servidor";
    _logger.LogError(ex, "Error de conexión HTTP");
}
catch (TaskCanceledException ex)
{
    errorMessage = "La solicitud ha expirado";
    _logger.LogError(ex, "Timeout de conexión");
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
_logger.LogWarning("Estado del modelo inválido para crear orden");

// Errores
_logger.LogError(ex, "Error al obtener la orden {OrdenId}", id);
_logger.LogError(ex, "Error al procesar solicitud en {Endpoint}", Request.Path);
```

#### **Frontend**
```csharp
// ApiService logging
_logger.LogInformation("?? Obteniendo órdenes desde API");
_logger.LogInformation("? Órdenes cargadas exitosamente: {Count}", ordenes.Count);
_logger.LogError("? Error de HTTP al obtener órdenes: {Error}", ex.Message);

// Componente logging
Logger.LogInformation("?? Inicializando componente OrdersList");
Logger.LogInformation("?? Navegando a reporte de top 10 productos");
```

### **Métricas de Performance**
```csharp
// Medición de tiempo de respuesta
var stopwatch = Stopwatch.StartNew();
var resultado = await _context.SalesOrderHeaders.ToListAsync();
stopwatch.Stop();
_logger.LogInformation("Consulta ejecutada en {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
```

### **Auditoría de Cambios**
```csharp
// Tracking automático en Entity Framework
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

## ?? Métricas y KPIs del Módulo

### **Métricas de Negocio**
- ?? **Total de Órdenes**: Contador en tiempo real
- ?? **Valor Total de Ventas**: Suma de TotalDue activas
- ?? **Órdenes Pendientes**: Status = 1
- ?? **Órdenes Enviadas**: Status = 4
- ?? **Clientes Activos**: Con órdenes en último año

### **Métricas Técnicas**
- ? **Tiempo de Respuesta**: Promedio < 500ms
- ?? **Throughput**: Requests por segundo
- ?? **Disponibilidad**: 99.9% uptime
- ?? **Error Rate**: < 1% de las peticiones

---

## ?? Integraciones

### **Módulo de Production**
```csharp
// Validación de productos en SalesOrderDetail
public async Task<bool> ValidateProductExists(int productId)
{
    return await _context.Products
        .Where(p => p.ProductID == productId && p.SellEndDate == null)
        .AnyAsync();
}
```

### **Módulo de HumanResources**
```csharp
// Validación de vendedores
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
1. ? **Lazy Loading**: Propiedades de navegación cargadas bajo demanda
2. ? **Pagination**: Limite de 100 registros por defecto
3. ? **Indexing**: Índices automáticos en FK y campos de búsqueda
4. ? **Caching**: HttpClient reutilizable con configuración de timeout
5. ? **Connection Pooling**: Configurado automáticamente por EF Core

### **Consultas Optimizadas**
```csharp
// Include específicos para evitar N+1 queries
var ordenes = await _context.SalesOrderHeaders
    .Include(s => s.Customer)           // 1 JOIN adicional
    .Include(s => s.SalesPerson)        // 1 JOIN adicional  
    .Include(s => s.SalesTerritory)     // 1 JOIN adicional
    .OrderByDescending(s => s.OrderDate)
    .Take(100)                          // Limitación de registros
    .ToListAsync();
```

---

## ?? Seguridad

### **Autenticación y Autorización**
- ? **API Key Authentication**: Middleware personalizado
- ? **CORS Configuration**: Orígenes específicos permitidos
- ? **HTTPS Enforcement**: Redirección automática
- ? **SQL Injection Prevention**: Entity Framework parametrizado

### **Validación de Entrada**
- ? **Model Validation**: DataAnnotations en DTOs
- ? **Input Sanitization**: Automático en EF Core
- ? **Type Safety**: Fuertemente tipado con C#

---

*Documento generado para AdventureWorks Enterprise System*  
*Versión: 1.0*  
*Fecha: 2024-12-17*  
*Módulo: Sales (Ventas)*