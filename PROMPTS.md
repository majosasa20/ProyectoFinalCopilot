# PROMPTS - AdventureWorks Enterprise System
*Historial de prompts utilizados para el desarrollo del sistema AdventureWorks Enterprise*

---

## Fase 1: Configuración Inicial y Estructura del Proyecto

### **Prompt inicial para configuración del workspace:**
> "@workspace tengo la base de datos AdventureWorks2014, necesito crear un sistema completo con todo, con API y WebApp, necesito que analices la estructura de la base y me ayudes a organizarme para comenzar."

---

## Fase 2: Construcción del Backend

### **Prompt para generar los módulos de HR, Sales, Production:**
> "te compartiré varios scripts de las tablas que pertenecen al área de Human Resources. Necesito que por cada tabla crees un Controller los cuales irán dentro de una carpeta Controllers y dentro de ella otra carpeta con el nombre del área, en este caso Human Resources.
También necesito que crees un archivo para Data, el mismo proceso con las carpetas, la carpeta Data y dentro de ella otra carpeta con el nombre del área, y en esa última vas a agregar todos los archivos data en base a los scripts.
Y por último que crees un modelo por cada uno, creas la carpeta Models, dentro otra carpeta con el nombre del área y dentro de esta última los archivos modelos que se van a crear por cada tabla, para distinguir estos archivos de los Data, estos al final del nombre del archivo escribirás Dto y luego la extensión, por ejemplo si la tabla se llama HumanResources entonces el archivo model irá como HumanResources.Dto.
Estos son los scripts: CREATE TABLE [HumanResources].[Shift]( [ShiftID] [tinyint] IDENTITY(1,1) NOT NULL, [Name] [dbo].[Name] NOT NULL, [StartTime] 7 NOT NULL, [EndTime] 7 NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_Shift_ShiftID] PRIMARY KEY CLUSTERED ( [ShiftID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [HumanResources].[JobCandidate]( [JobCandidateID] [int] IDENTITY(1,1) NOT NULL, [BusinessEntityID] [int] NULL, [Resume] [xml](CONTENT [HumanResources].[HRResumeSchemaCollection]) NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_JobCandidate_JobCandidateID] PRIMARY KEY CLUSTERED ( [JobCandidateID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] GO
CREATE TABLE [HumanResources].[EmployeePayHistory]( [BusinessEntityID] [int] NOT NULL, [RateChangeDate] [datetime] NOT NULL, [Rate] [money] NOT NULL, [PayFrequency] [tinyint] NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_EmployeePayHistory_BusinessEntityID_RateChangeDate] PRIMARY KEY CLUSTERED ( [BusinessEntityID] ASC, [RateChangeDate] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [HumanResources].[EmployeeDepartmentHistory]( [BusinessEntityID] [int] NOT NULL, [DepartmentID] [smallint] NOT NULL, [ShiftID] [tinyint] NOT NULL, [StartDate] [date] NOT NULL, [EndDate] [date] NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_EmployeeDepartmentHistory_BusinessEntityID_StartDate_DepartmentID] PRIMARY KEY CLUSTERED ( [BusinessEntityID] ASC, [StartDate] ASC, [DepartmentID] ASC, [ShiftID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [HumanResources].[Employee]( [BusinessEntityID] [int] NOT NULL, [NationalIDNumber] 15 NOT NULL, [LoginID] 256 NOT NULL, [OrganizationNode] [hierarchyid] NULL, [OrganizationLevel]  AS ([OrganizationNode].), [JobTitle] 50 NOT NULL, [BirthDate] [date] NOT NULL, [MaritalStatus] 1 NOT NULL, [Gender] 1 NOT NULL, [HireDate] [date] NOT NULL, [SalariedFlag] [dbo].[Flag] NOT NULL, [VacationHours] [smallint] NOT NULL, [SickLeaveHours] [smallint] NOT NULL, [CurrentFlag] [dbo].[Flag] NOT NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_Employee_BusinessEntityID] PRIMARY KEY CLUSTERED ( [BusinessEntityID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [HumanResources].[Department]( [DepartmentID] [smallint] IDENTITY(1,1) NOT NULL, [Name] [dbo].[Name] NOT NULL, [GroupName] [dbo].[Name] NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_Department_DepartmentID] PRIMARY KEY CLUSTERED ( [DepartmentID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO""

### **Prompt para generar módulos de Sales:**
> "@workspace siguiendo los contextos anteriores, te comparto los scripts de las tablas pertecenientes al área Sales:
CREATE TABLE [Sales].[Store]( [BusinessEntityID] [int] NOT NULL, [Name] [dbo].[Name] NOT NULL, [SalesPersonID] [int] NULL, [Demographics] [xml](CONTENT [Sales].[StoreSurveySchemaCollection]) NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_Store_BusinessEntityID] PRIMARY KEY CLUSTERED ( [BusinessEntityID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] GO
CREATE TABLE [Sales].[SpecialOffer]( [SpecialOfferID] [int] IDENTITY(1,1) NOT NULL, [Description] 255 NOT NULL, [DiscountPct] [smallmoney] NOT NULL, [Type] 50 NOT NULL, [Category] 50 NOT NULL, [StartDate] [datetime] NOT NULL, [EndDate] [datetime] NOT NULL, [MinQty] [int] NOT NULL, [MaxQty] [int] NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_SpecialOffer_SpecialOfferID] PRIMARY KEY CLUSTERED ( [SpecialOfferID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Sales].[SalesTerritory]( [TerritoryID] [int] IDENTITY(1,1) NOT NULL, [Name] [dbo].[Name] NOT NULL, [CountryRegionCode] 3 NOT NULL, [Group] 50 NOT NULL, [SalesYTD] [money] NOT NULL, [SalesLastYear] [money] NOT NULL, [CostYTD] [money] NOT NULL, [CostLastYear] [money] NOT NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_SalesTerritory_TerritoryID] PRIMARY KEY CLUSTERED ( [TerritoryID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Sales].[SalesPerson]( [BusinessEntityID] [int] NOT NULL, [TerritoryID] [int] NULL, [SalesQuota] [money] NULL, [Bonus] [money] NOT NULL, [CommissionPct] [smallmoney] NOT NULL, [SalesYTD] [money] NOT NULL, [SalesLastYear] [money] NOT NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_SalesPerson_BusinessEntityID] PRIMARY KEY CLUSTERED ( [BusinessEntityID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Sales].[SalesOrderHeader]( [SalesOrderID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL, [RevisionNumber] [tinyint] NOT NULL, [OrderDate] [datetime] NOT NULL, [DueDate] [datetime] NOT NULL, [ShipDate] [datetime] NULL, [Status] [tinyint] NOT NULL, [OnlineOrderFlag] [dbo].[Flag] NOT NULL, [SalesOrderNumber]  AS (isnull(N'SO'+CONVERT(23,[SalesOrderID]),N'*** ERROR ***')), [PurchaseOrderNumber] [dbo].[OrderNumber] NULL, [AccountNumber] [dbo].[AccountNumber] NULL, [CustomerID] [int] NOT NULL, [SalesPersonID] [int] NULL, [TerritoryID] [int] NULL, [BillToAddressID] [int] NOT NULL, [ShipToAddressID] [int] NOT NULL, [ShipMethodID] [int] NOT NULL, [CreditCardID] [int] NULL, [CreditCardApprovalCode] 15 NULL, [CurrencyRateID] [int] NULL, [SubTotal] [money] NOT NULL, [TaxAmt] [money] NOT NULL, [Freight] [money] NOT NULL, [TotalDue]  AS (isnull(([SubTotal]+[TaxAmt])+[Freight],(0))), [Comment] 128 NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_SalesOrderHeader_SalesOrderID] PRIMARY KEY CLUSTERED ( [SalesOrderID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Sales].[SalesOrderDetail]( [SalesOrderID] [int] NOT NULL, [SalesOrderDetailID] [int] IDENTITY(1,1) NOT NULL, [CarrierTrackingNumber] 25 NULL, [OrderQty] [smallint] NOT NULL, [ProductID] [int] NOT NULL, [SpecialOfferID] [int] NOT NULL, [UnitPrice] [money] NOT NULL, [UnitPriceDiscount] [money] NOT NULL, [LineTotal]  AS (isnull(([UnitPrice]((1.0)-[UnitPriceDiscount]))[OrderQty],(0.0))), [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID] PRIMARY KEY CLUSTERED ( [SalesOrderID] ASC, [SalesOrderDetailID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Sales].[Customer]( [CustomerID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL, [PersonID] [int] NULL, [StoreID] [int] NULL, [TerritoryID] [int] NULL, [AccountNumber]  AS (isnull('AW'+[dbo].[CustomerID],'')), [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_Customer_CustomerID] PRIMARY KEY CLUSTERED ( [CustomerID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Sales].[CountryRegionCurrency]( [CountryRegionCode] 3 NOT NULL, [CurrencyCode] 3 NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode] PRIMARY KEY CLUSTERED ( [CountryRegionCode] ASC, [CurrencyCode] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Sales].[Currency]( [CurrencyCode] 3 NOT NULL, [Name] [dbo].[Name] NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_Currency_CurrencyCode] PRIMARY KEY CLUSTERED ( [CurrencyCode] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO


### **Prompt para generar módulos de Production:**
> "@workspace ahora necesito que generes el modelo de estos otros archivos pero del área Production, recuerda que dentro del mismo proyecto AdventureWorks.Enterprise.Api vas a usar la carpeta Models y dentro vas a crear una nueva carpeta con el nombre de esta área.
Generas un model por el script de cada tabla: CREATE TABLE [Production].[WorkOrder]( [WorkOrderID] [int] IDENTITY(1,1) NOT NULL, [ProductID] [int] NOT NULL, [OrderQty] [int] NOT NULL, [StockedQty]  AS (isnull([OrderQty]-[ScrappedQty],(0))), [ScrappedQty] [smallint] NOT NULL, [StartDate] [datetime] NOT NULL, [EndDate] [datetime] NULL, [DueDate] [datetime] NOT NULL, [ScrapReasonID] [smallint] NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_WorkOrder_WorkOrderID] PRIMARY KEY CLUSTERED ( [WorkOrderID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Production].[ProductSubcategory]( [ProductSubcategoryID] [int] IDENTITY(1,1) NOT NULL, [ProductCategoryID] [int] NOT NULL, [Name] [dbo].[Name] NOT NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_ProductSubcategory_ProductSubcategoryID] PRIMARY KEY CLUSTERED ( [ProductSubcategoryID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Production].[ProductModel]( [ProductModelID] [int] IDENTITY(1,1) NOT NULL, [Name] [dbo].[Name] NOT NULL, [CatalogDescription] [xml](CONTENT [Production].[ProductDescriptionSchemaCollection]) NULL, [Instructions] [xml](CONTENT [Production].[ManuInstructionsSchemaCollection]) NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_ProductModel_ProductModelID] PRIMARY KEY CLUSTERED ( [ProductModelID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] GO
CREATE TABLE [Production].[ProductInventory]( [ProductID] [int] NOT NULL, [LocationID] [smallint] NOT NULL, [Shelf] 10 NOT NULL, [Bin] [tinyint] NOT NULL, [Quantity] [smallint] NOT NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_ProductInventory_ProductID_LocationID] PRIMARY KEY CLUSTERED ( [ProductID] ASC, [LocationID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Production].[ProductCategory]( [ProductCategoryID] [int] IDENTITY(1,1) NOT NULL, [Name] [dbo].[Name] NOT NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_ProductCategory_ProductCategoryID] PRIMARY KEY CLUSTERED ( [ProductCategoryID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO
CREATE TABLE [Production].[Product]( [ProductID] [int] IDENTITY(1,1) NOT NULL, [Name] [dbo].[Name] NOT NULL, [ProductNumber] 25 NOT NULL, [MakeFlag] [dbo].[Flag] NOT NULL, [FinishedGoodsFlag] [dbo].[Flag] NOT NULL, [Color] 15 NULL, [SafetyStockLevel] [smallint] NOT NULL, [ReorderPoint] [smallint] NOT NULL, [StandardCost] [money] NOT NULL, [ListPrice] [money] NOT NULL, [Size] 5 NULL, [SizeUnitMeasureCode] 3 NULL, [WeightUnitMeasureCode] 3 NULL, [Weight] [decimal](8, 2) NULL, [DaysToManufacture] [int] NOT NULL, [ProductLine] 2 NULL, [Class] 2 NULL, [Style] 2 NULL, [ProductSubcategoryID] [int] NULL, [ProductModelID] [int] NULL, [SellStartDate] [datetime] NOT NULL, [SellEndDate] [datetime] NULL, [DiscontinuedDate] [datetime] NULL, [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, [ModifiedDate] [datetime] NOT NULL, CONSTRAINT [PK_Product_ProductID] PRIMARY KEY CLUSTERED ( [ProductID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] GO


### **Prompt para generar los controladores: **
ahora que tienes el contexto de todas las tablas a utilizar y con sus respectivos modelos, necesito que me generes los controladores que corresponden a cada área, para ello necesito que en el proyecto actual crees la carpeta Controllers.
Vas a crear el archivo EmployeesController el cual será un CRUD para empleados, es decir, utilizar los métodos GET, PUT, POST, DELETE
Otro archivo OrdersController que será una gestión de órdenes de venta.
y el archivo ProductsController que será una gestión de productos e inventario.
si consideras que de todo el esquema de los modelos, me pudo hacer falta algún controlador que tenga relación con el proyecto y el modelo del negocio el cual se trata de un Sistema de Gestión Empresarial, entonces tienes toda la libertad de crearlo.

---

## Fase 3: Backend - Middleware y Configuración

### ** Prompt para configuración inicial del API:*
utiliza la base de datos de AdventureWorks2014, la conexión ya está activa, necesito que generes y optimices stores procedures para los siguientes reportes, al crear cada uno de ellos, al final del nombre de los SPs agrega _Sagastume, esta es la idea que necesito para cada reporte, realiza la combinación de las tablas de los scripts compartidos según sea necesario:
reporte para Human Resources: generar un reporte de empleados con más tiempo en su departamento actual.
-Sales: que muestre el top 10 de productos más vendidos
-Production: un listado de productos con bajo inventario (ej. menor a 10)
al estar creados es necesario que los integres a los respectivos controladores

necesito que generes y optimices stores procedures para los siguientes reportes, al crear cada uno de ellos, al final del nombre de los SPs agrega _Sagastume, esta es la idea que necesito para cada reporte, realiza la combinación de las tablas de los scripts compartidos según sea necesario:
reporte para Human Resources: generar un reporte de empleados con más tiempo en su departamento actual.
-Sales: que muestre el top 10 de productos más vendidos
-Production: un listado de productos con bajo inventario (ej. menor a 10)
al estar creados es necesario que los integres a los respectivos controladores

### ** Prompt para configuración de EntityFramework:**
prepara el proyecto de AdventureWorks.Enterprise.Api para usar Entity Framework Core con SQL Server. Para ello, modifica el archivo .csproj para agregar las últimas versiones estables de los paquetes NuGet 'Microsoft.EntityFrameworkCore.SqlServer' y 'Microsoft.EntityFrameworkCore.Tools'.
Usando los esquemas que te di para crear los models, realiza las siguientes acciones en el proyecto:
1.	Crea una nueva carpeta llamada Data.
2.	Dentro de Data, crea una clase C# para cada uno de los archivos models creados. Mapea las columnas de la tabla a propiedades, usando nombres en PascalCase y los tipos de datos de .NET correctos. Las claves primarias deben ser como aparecían en los scripts de CREATE TABLE compartidos anteriormente para la creación de los models.
3.	En la misma carpeta, crea la clase AdventureWorksDbContext.cs que herede de DbContext. Debe incluir un constructor que acepte DbContextOptions y un DbSet para cada uno de los archivos creados como models con sus nombres correspodientes.`

### **Prompt para configuración de Base de Datos: **
Modifica mi archivo 'appsettings.json' del mismo proyecto mencionado para agregar una cadena de conexión llamada  "SDESICASQL". Debe apuntar a la base de datos 'AdventureWorksDW2014' en el servidor SDESICASQL (usando Integrated Security).
luego, configura la inyección de dependencias en Program.cs. Registra el AdventureWorksDbContext para que use SQL Server con la cadena de conexión 'SDESICASQL'.
modifica los archivos de controladores  para usar 'AdventureWorksDbContext' a través de inyección de dependencias los métodos CRUD completos y asíncronos para cada una de las entidades, siguiendo las convenciones RESTful y manejando casos de error como NotFound. adicional, modifica los controladores para que manden a llamar correctamente a los SPs que tienen creados algunos


### **Prompt para API Key Middleware:**
> "Quiero proteger mi API para que solo las aplicaciones con una clave secreta puedan acceder. ¿Cuál es una forma estándar de implementar la seguridad con una "API Key" en ASP.NET Core 8 usando middleware? Descríbeme los pasos conceptuales. y agrega esa configuración al proyecto
Agrega una sección "ApiSettings" en appsettings.json con una propiedad "ApiKey" y un valor secreto. 2. Crea un nuevo middleware llamado ApiKeyMiddleware.cs. Debe leer la clave del encabezado X-API-Key de la solicitud y compararla con la del appsettings.json. Si no coincide o no está presente, debe devolver un error 401 Unauthorized. 3. Registra este middleware en Program.cs para que se ejecute en cada solicitud a la API.

---

## Fase 4: Frontend - Configuración Blazor

### **Prompt para configuración inicial del WebApp:**
> "Expande el ApiService en el proyecto Blazor 'AdventureWorks.Enterprise.WebApp para que contenga métodos que se comuniquen con todos los endpoints de los controladores del backend.
Genera los componentes necesarios para cada módulo, te doy algunos ejemplos de los componentes necesarios: ○ Portal de RRHH: EmployeesList.razor y EmployeeDetails.razor. ○ Portal de Ventas: OrdersList.razor y OrderDetails.razor (vista maestro-detalle). ○ Portal de Producción: ProductList.razor (mostrando el nivel de inventario) y LowInventoryReport.razor para visualizar el reporte de productos con bajo stock. Para esta última parte es necesario crear una carpeta Page en el proyecto mencionado 'AdventureWorks.Enterprise.WebApp' y tal como se hizo en el backend, adentro de esa carpeta crear otra para cada una de las áreas y los archivos .blazor incluirlos en las carpetas que correspondan

---

### **Prompt para Testing del API:**
en el proyecto AdventureWorks.Enterprise.Api.Test necesito que se genere tests xUnit para los controladores para cada uno de los controladores cubriendo casos de éxito y de fallo.

## Fase 5: Frontend - Componentes Blazor

### **Prompt para NavMenu y Layout:**
> "en el archivo NavMenu.razor agrega un menú y que haga referencia a cada una de las páginas .razor creadas"

### **Prompt para EmployeesList.razor:**
> "crear una página Blazor para mostrar la lista de empleados. La página debe tener una tabla responsiva con los datos de los empleados, botones para ver detalles, editar y eliminar, un botón para agregar nuevo empleado, y un botón para navegar al reporte de tiempo en departamento. Usar Bootstrap para el diseño y mostrar estados de carga mientras se obtienen los datos de la API."

### **Prompt para OrdersList.razor:**
> "crear una página Blazor para mostrar órdenes de venta. Debe mostrar las órdenes en una tabla con información resumida, filtros por estado y cliente, métricas de resumen (total órdenes, valor total, etc.), botones para ver detalles de cada orden, y navegación al reporte de top 10 productos. Implementar paginación y estados de carga."

### **Prompt para ProductList.razor:**
> "crear una página Blazor para gestión de productos. Debe incluir cards con métricas principales (total productos, productos activos, valor total inventario), tabla con lista de productos que incluya información de inventario, filtros por categoría y estado, indicadores visuales para productos con bajo inventario, y navegación al reporte de bajo inventario."

### **Prompt para componentes de reportes:**
> "crear componentes especializados para los reportes: EmployeeTimeReport.razor que muestre empleados ordenados por tiempo en departamento actual con gráficos y métricas, Top10ProductsReport.razor que muestre los productos más vendidos con podio de ganadores y estadísticas, y LowInventoryReport.razor que muestre productos con inventario crítico con indicadores de prioridad."

---

## Fase 6: Resolución de Problemas

### **Prompt para corrección de errores Razor:**
> "Corregir #errors en #file:'ProductList.razor' , #file:'NavMenu.razor' , #file:'Top10ProductsReport.razor'"

### **Prompt para corrección de errores CSS:**
> "Corregir #errors en #file:'LowInventoryReport.razor' , #file:'EmployeeTimeReport.razor' , #file:'ProductosControllerTests.cs' , #file:'OrderDetails.razor' y #file:'ClientesControllerTests.cs'"

### **Prompt para resolución de problemas de configuración:**
> "Corregir el código en #file:'ApiService.cs' , #file:'AdventureWorks.Enterprise.WebApp\Program.cs' , #file:'EmployeesList.razor' y #file:'EmployeesController.cs' para que valides todas las configuraciones porque estoy teniendo este error al dar clic a la página de empleados: Error al cargar empleados: An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set."

### **Prompt para compilación final:**
> "sí corrige todos los erroes, por favor"

---

## Prompts de Documentación

### **Prompt para documentación de este archivo:**
> "tengo una petición algo extensa, puedes revisar todo el historial del chat abierto, cada consulta que te hice, las más extensas y que consideres más importantes (como el módulo de ventas, para generar los controladores, los SPs, las consultas inciales cuando creaste las carpetas según las áreas, etc.) todo esto necesito que lo guardes en un archivo llamado PROMPTS.md que se encuentre en la raíz del repositorio, cada uno identificado de la siguiente manera, por ejemplo: ## Fase 2: Backend - Módulo de Ventas **Prompt para generar el OrdersController:** > '@workspace ... (acá iría el prompt o la consultar que hice en ese momento)"

### **Prompt para corrección de prompts:**
> "creo que no me entendiste o no me expliqué bien, necesito que copies las consultas tal cual como yo las escribí en el chat, por ejemplo tengo este prompt: te compartiré varios scripts de las tablas que pertenecen al área de Human Resources. Necesito que por cada tabla crees un Controller los cuales irán dentro de una carpeta Controllers y dentro de ella otra carpeta con el nombre del área, en este caso Human Resources... este debe ir como: ## **Fase 2: Construcción del Backend ### **Prompt para generar los módulos de HR, Sales, Production"

---

## Notas Técnicas del Proyecto

### **Tecnologías Utilizadas:**
- .NET 8
- Blazor Server con componentes interactivos
- Entity Framework Core
- SQL Server (AdventureWorks2014)
- Swagger/OpenAPI
- API Key Authentication

### **Características del Sistema:**
- API RESTful completa con operaciones CRUD
- Frontend Blazor responsivo y moderno
- Sistema de reportes especializados
- Autenticación por API Key
- Logging detallado para debugging
- Manejo robusto de errores
- Validaciones de negocio
- Diseño responsive con Bootstrap

### **Áreas Funcionales Implementadas:**
1. **Human Resources**: Gestión de empleados y departamentos
2. **Sales**: Gestión de órdenes, clientes y reportes de ventas
3. **Production**: Gestión de productos, inventario y órdenes de trabajo
4. **Reporting**: Reportes especializados con visualizaciones

---

*Documento actualizado con los prompts exactos utilizados durante el desarrollo*
*Fecha: 2024-12-17*
*Desarrollador: Sistema con asistencia de GitHub Copilot*