using System;

namespace AdventureWorks.Enterprise.Api.Models.Production
{
    public class ReporteBajoInventarioDto
    {
        public int ProductID { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string NumeroProducto { get; set; } = string.Empty;
        public string? Categoria { get; set; }
        public string? Subcategoria { get; set; }
        public int CantidadEnInventario { get; set; }
        public short NivelStockSeguridad { get; set; }
        public short PuntoReorden { get; set; }
        public decimal PrecioLista { get; set; }
        public decimal CostoEstandar { get; set; }
        public string EstadoInventario { get; set; } = string.Empty;
        public string? Ubicacion { get; set; }
    }
}