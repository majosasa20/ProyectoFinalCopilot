-- =============================================
-- Stored Procedure para Human Resources: Empleados con más tiempo en su departamento actual
-- =============================================
CREATE PROCEDURE usp_EmpleadosConMasTiempoEnDepartamento_Sagastume
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        e.BusinessEntityID,
        p.FirstName + ' ' + ISNULL(p.LastName, '') AS NombreCompleto,
        e.JobTitle AS Cargo,
        d.Name AS Departamento,
        d.GroupName AS GrupoDepartamento,
        edh.StartDate AS FechaInicioDepartamento,
        DATEDIFF(DAY, edh.StartDate, GETDATE()) AS DiasEnDepartamento,
        DATEDIFF(YEAR, edh.StartDate, GETDATE()) AS AniosEnDepartamento,
        e.HireDate AS FechaContratacion
    FROM HumanResources.Employee e
    INNER JOIN Person.Person p ON e.BusinessEntityID = p.BusinessEntityID
    INNER JOIN HumanResources.EmployeeDepartmentHistory edh ON e.BusinessEntityID = edh.BusinessEntityID
    INNER JOIN HumanResources.Department d ON edh.DepartmentID = d.DepartmentID
    WHERE edh.EndDate IS NULL -- Solo departamentos actuales
      AND e.CurrentFlag = 1 -- Solo empleados activos
    ORDER BY DiasEnDepartamento DESC;
END;
GO

-- =============================================
-- Stored Procedure para Sales: Top 10 productos más vendidos
-- =============================================
CREATE PROCEDURE usp_Top10ProductosMasVendidos_Sagastume
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
        p.ProductID, 
        p.Name, 
        p.ProductNumber, 
        pc.Name, 
        ps.Name, 
        p.ListPrice
    ORDER BY CantidadTotalVendida DESC;
END;
GO

-- =============================================
-- Stored Procedure para Production: Productos con bajo inventario
-- =============================================
CREATE PROCEDURE usp_ProductosConBajoInventario_Sagastume
    @UmbralInventario INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.ProductID,
        p.Name AS NombreProducto,
        p.ProductNumber AS NumeroProducto,
        pc.Name AS Categoria,
        ps.Name AS Subcategoria,
        ISNULL(SUM(pi.Quantity), 0) AS CantidadEnInventario,
        p.SafetyStockLevel AS NivelStockSeguridad,
        p.ReorderPoint AS PuntoReorden,
        p.ListPrice AS PrecioLista,
        p.StandardCost AS CostoEstandar,
        CASE 
            WHEN ISNULL(SUM(pi.Quantity), 0) = 0 THEN 'SIN STOCK'
            WHEN ISNULL(SUM(pi.Quantity), 0) <= @UmbralInventario THEN 'STOCK BAJO'
            ELSE 'STOCK NORMAL'
        END AS EstadoInventario,
        l.Name AS Ubicacion
    FROM Production.Product p
    LEFT JOIN Production.ProductInventory pi ON p.ProductID = pi.ProductID
    LEFT JOIN Production.Location l ON pi.LocationID = l.LocationID
    LEFT JOIN Production.ProductSubcategory ps ON p.ProductSubcategoryID = ps.ProductSubcategoryID
    LEFT JOIN Production.ProductCategory pc ON ps.ProductCategoryID = pc.ProductCategoryID
    WHERE p.FinishedGoodsFlag = 1 -- Solo productos terminados
      AND p.SellEndDate IS NULL -- Solo productos activos
    GROUP BY 
        p.ProductID, 
        p.Name, 
        p.ProductNumber, 
        pc.Name, 
        ps.Name, 
        p.SafetyStockLevel, 
        p.ReorderPoint, 
        p.ListPrice, 
        p.StandardCost,
        l.Name
    HAVING ISNULL(SUM(pi.Quantity), 0) <= @UmbralInventario
    ORDER BY CantidadEnInventario ASC, p.Name;
END;
GO