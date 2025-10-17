using System;

namespace AdventureWorks.Enterprise.WebApp.Models.Sales
{
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
        public string? PurchaseOrderNumber { get; set; }
        public string? AccountNumber { get; set; }
        public int CustomerID { get; set; }
        public int? SalesPersonID { get; set; }
        public int? TerritoryID { get; set; }
        public int BillToAddressID { get; set; }
        public int ShipToAddressID { get; set; }
        public int ShipMethodID { get; set; }
        public int? CreditCardID { get; set; }
        public string? CreditCardApprovalCode { get; set; }
        public int? CurrencyRateID { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal? TotalDue { get; set; }
        public string? Comment { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class SalesOrderDetailDto
    {
        public int SalesOrderID { get; set; }
        public int SalesOrderDetailID { get; set; }
        public string? CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public int ProductID { get; set; }
        public int SpecialOfferID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal? LineTotal { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class ReporteTop10ProductosDto
    {
        public int ProductID { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string NumeroProducto { get; set; } = string.Empty;
        public string? Categoria { get; set; }
        public string? Subcategoria { get; set; }
        public int CantidadTotalVendida { get; set; }
        public decimal VentasTotales { get; set; }
        public decimal PrecioPromedioVenta { get; set; }
        public int NumeroOrdenes { get; set; }
        public decimal PrecioLista { get; set; }
    }
}