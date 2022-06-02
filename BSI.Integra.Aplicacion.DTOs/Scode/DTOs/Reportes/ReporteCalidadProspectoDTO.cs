using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCalidadProspectoDTO
    {
        public int Contador { get; set; }
        public int IdAsesor { get; set; }
        public string NombreAsesor { get; set; }
        public int Orden { get; set; }
        public string NombreFase { get; set; }
        public decimal PlazoInicio { get; set; }
        public decimal ValidacionPerfil { get; set; }
        public decimal HistorialFinanciero { get; set; }
        public decimal PGeneral { get; set; }
        public decimal Competidores { get; set; }
        public decimal PEspecifico { get; set; }
        public decimal Beneficios { get; set; }
        public decimal ProblemaSeleccionado { get; set; }
        public decimal ProblemaSolucionado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal CalidadPromedio { get; set; }

    }
}
