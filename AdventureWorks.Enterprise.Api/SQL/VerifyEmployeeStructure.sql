-- Consulta para verificar los tipos de datos de la tabla Employee
USE [AdventureWorks2014]
GO

-- Verificar estructura de la tabla Employee
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    CHARACTER_MAXIMUM_LENGTH,
    NUMERIC_PRECISION,
    NUMERIC_SCALE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'HumanResources' 
  AND TABLE_NAME = 'Employee'
ORDER BY ORDINAL_POSITION;

-- También verificar un registro de ejemplo
SELECT TOP 1 
    BusinessEntityID,
    NationalIDNumber,
    LoginID,
    OrganizationLevel,
    JobTitle,
    BirthDate,
    MaritalStatus,
    Gender,
    HireDate,
    SalariedFlag,
    VacationHours,
    SickLeaveHours,
    CurrentFlag
FROM HumanResources.Employee 
WHERE CurrentFlag = 1;