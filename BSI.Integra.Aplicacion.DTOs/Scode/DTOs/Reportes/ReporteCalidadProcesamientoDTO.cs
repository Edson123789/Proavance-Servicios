using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCalidadProcesamientoDTO
    {
        public string DatosAsesor { get; set; }
        public string NombreFase { get; set; }
        public int? Registros { get; set; }
        public DateTime Fecha { get; set; }
        public double PromedioPerfil { get; set; }
        public double PromedioHistorialFinanciero { get; set; }
        public double PromedioPGeneral { get; set; }
        public double PromedioPEspecifico { get; set; }
        public double PromedioBeneficios { get; set; }
        public double PromedioCompetidores { get; set; }
        public double PromedioProblemaSeleccionados { get; set; }
        public double PromedioProblemaSolucionados { get; set; }
    }
}
