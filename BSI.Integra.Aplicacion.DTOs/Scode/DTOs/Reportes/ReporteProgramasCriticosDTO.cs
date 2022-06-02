using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteProgramasCriticosDTO
    {
        public string Grupo { get; set; }
        public int IdPadre { get; set; }
        public string Padre { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string ProgramaGeneral{ get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string Modalidad { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public int IdCentroCosto { get; set; }
        public string Programa { get; set; }
        public string FechaInicio { get; set; }
        public DateTime? FechaInicioAuxiliar { get; set; }
        public int BNC { get; set; }
        public int PF { get; set; }
        public int IC { get; set; }
        public int IT { get; set; }
        public int IP { get; set; }
        public int Seguimiento { get; set; }
        public int TotalDatos { get; set; }
        public int IS_M_Acumulado { get; set; }
        public double PrecioPromedio { get; set; }
        public double PrecioPromedio10Descuento { get; set; }
        public double IngresoPromedioIS { get; set; }
        public double CostoPrograma { get; set; }
        public int? PuntoEquilibrio { get; set; }
    }
}
