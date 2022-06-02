using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteProgramasCriticosAsignacionDiariaDTO
    {
        public string Area { get; set; }
        public int IdAsesor { get; set; }
        public string Asesor { get; set; }
        public string Programa { get; set; }
        public int IdCentroCosto { get; set; }
        public int BNC_MuyAlta { get; set; }
        public int BNC1_MuyAlta { get; set; }
        public int BNC_VentaCruzada { get; set; }
        public int Total_BNC_BNC1_VentaCruzada { get; set; }
        public int BNC_SinProbabilidad { get; set; }
        public int BNC_Historico { get; set; }
        public int RN { get; set; }
        public int IT { get; set; }
        public int IP { get; set; }
        public int PF { get; set; }
        public int IC { get; set; }
        public int Seguimiento { get; set; }
        public int TotalDatos_SinMeta { get; set; }
        public int TotalDatos_ConMeta { get; set; }
        public int TotalDatos { get; set; }
        public int IS_M { get; set; }
        public int IS_M_Acumulado { get; set; }
        public int MetaCentroCosto { get; set; }
        public int BNC1_Historico { get; set; }
    }

    public class ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO
    {
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string NombreGrupoFiltroProgramaCritico { get; set; }
        public int IdGrupoFiltroProgramaCriticoExterno { get; set; }
        public int OrdenAsesorGrupo { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string NombrePaisPersonal { get; set; }
        public string AsignacionPais { get; set; }
        public int IdCodigoPais { get; set; }
        public int BNC_MuyAlta { get; set; }
        public int BNC_Historico { get; set; }
        public int BNC_AltaMediaRemarketing { get; set; }
        public int BNC_TotalDatos { get; set; }
        public int RN { get; set; }
        public int IT { get; set; }
        public int IP { get; set; }
        public int PF { get; set; }
        public int IC { get; set; }
        public int Seguimiento { get; set; }
        public int TotalDatos { get; set; }
        public int IS_M { get; set; }
        public int IS_M_Acumulado { get; set; }
    }

    public class ReporteEstructuradoAsignacionProgramasCriticosDTO
    {
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string NombreGrupoFiltroProgramaCritico { get; set; }
        public int OrdenAsesorGrupo { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string NombrePaisPersonal { get; set; }
        public string AsignacionPais { get; set; }
        public int CantidadGrupoActual { get; set; }
        public int CantidadOtrosGrupos { get; set; }
        public int BNC_MuyAlta { get; set; }
        public int BNC_Historico { get; set; }
        public int BNC_AltaMediaRemarketing { get; set; }
        public int BNC_TotalDatos { get; set; }
        public int RN { get; set; }
        public int IT { get; set; }
        public int IP { get; set; }
        public int PF { get; set; }
        public int IC { get; set; }
        public int Seguimiento { get; set; }
        public int TotalDatos { get; set; }
        public int IS_M { get; set; }
        public int IS_M_Acumulado { get; set; }
        public PaisesReporteProgramasCriticosDTO Paises { get; set; }
    }

    public class PaisesReporteProgramasCriticosDTO
    {
        public int CantidadPeru { get; set; }
        public int CantidadColombia { get; set; }
        public int CantidadBolivia { get; set; }
        public int CantidadMexico { get; set; }
        public int CantidadOtros { get; set; }
    }
}
