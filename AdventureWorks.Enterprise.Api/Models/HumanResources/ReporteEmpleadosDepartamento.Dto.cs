using System;

namespace AdventureWorks.Enterprise.Api.Models.HumanResources
{
    public class ReporteEmpleadosDepartamentoDto
    {
        public int BusinessEntityID { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string GrupoDepartamento { get; set; } = string.Empty;
        public DateTime FechaInicioDepartamento { get; set; }
        public int DiasEnDepartamento { get; set; }
        public int AniosEnDepartamento { get; set; }
        public DateTime FechaContratacion { get; set; }
    }
}