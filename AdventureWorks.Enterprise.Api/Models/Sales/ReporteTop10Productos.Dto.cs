using System;

namespace AdventureWorks.Enterprise.Api.Models.Sales
{
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