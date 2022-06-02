using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteOperacionesEstructuraDetalleDTO
    {
        public int Nivel {get; set; }
        public string NombreNivel {get; set; }
        public int CantidadSemanaMenos1 {get; set; }
        public int CantidadSemanaMenos2 {get; set; }
        public int CantidadSemanaMenos3 {get; set; }
        public int CantidadSemanaMenos4 {get; set; }
        public int CantidadSemanaMenos5 {get; set; }
    }


    public class BaseReporteOperacionesEstructuraDetalleDTO
    {
        public int IdPersonal { get; set; }
        public int NivelSumaSemana1_1 { get; set; }
        public int NivelSumaSemana1_2 { get; set; }
        public int NivelSumaSemana1_3 { get; set; }
        public int NivelSumaSemana2_1 { get; set; }
        public int NivelSumaSemana2_2 { get; set; }
        public int NivelSumaSemana2_3 { get; set; }
        public int NivelSumaSemana3_1 { get; set; }
        public int NivelSumaSemana3_2 { get; set; }
        public int NivelSumaSemana3_3 { get; set; }
        public int NivelSumaSemana4_1 { get; set; }
        public int NivelSumaSemana4_2 { get; set; }
        public int NivelSumaSemana4_3 { get; set; }
        public int NivelSumaSemana5_1 { get; set; }
        public int NivelSumaSemana5_2 { get; set; }
        public int NivelSumaSemana5_3 { get; set; }
    }

    public class BaseReporteOperacionesEstructuraDetalleV2DTO
    {
        public int IdPersonal { get; set; }
        public int NivelSumaSemana1_1 { get; set; }
        public int NivelSumaSemana1_2 { get; set; }
        public int NivelSumaSemana1_3 { get; set; }
        public int NivelSumaSemana1_4 { get; set; }
        public int NivelSumaSemana1_5 { get; set; }
        public int NivelSumaSemana1_6 { get; set; }
        public int NivelSumaSemana1_7 { get; set; }
        public int NivelSumaSemana1_8 { get; set; }
        public int NivelSumaSemana1_9 { get; set; }
        public int NivelSumaSemana1_10 { get; set; }


        public int NivelSumaSemana2_1 { get; set; }
        public int NivelSumaSemana2_2 { get; set; }
        public int NivelSumaSemana2_3 { get; set; }
        public int NivelSumaSemana2_4 { get; set; }
        public int NivelSumaSemana2_5 { get; set; }
        public int NivelSumaSemana2_6 { get; set; }
        public int NivelSumaSemana2_7 { get; set; }
        public int NivelSumaSemana2_8 { get; set; }
        public int NivelSumaSemana2_9 { get; set; }
        public int NivelSumaSemana2_10 { get; set; }


        public int NivelSumaSemana3_1 { get; set; }
        public int NivelSumaSemana3_2 { get; set; }
        public int NivelSumaSemana3_3 { get; set; }
        public int NivelSumaSemana3_4 { get; set; }
        public int NivelSumaSemana3_5 { get; set; }
        public int NivelSumaSemana3_6 { get; set; }
        public int NivelSumaSemana3_7 { get; set; }
        public int NivelSumaSemana3_8 { get; set; }
        public int NivelSumaSemana3_9 { get; set; }
        public int NivelSumaSemana3_10 { get; set; }

        public int NivelSumaSemana4_1 { get; set; }
        public int NivelSumaSemana4_2 { get; set; }
        public int NivelSumaSemana4_3 { get; set; }
        public int NivelSumaSemana4_4 { get; set; }
        public int NivelSumaSemana4_5 { get; set; }
        public int NivelSumaSemana4_6 { get; set; }
        public int NivelSumaSemana4_7 { get; set; }
        public int NivelSumaSemana4_8 { get; set; }
        public int NivelSumaSemana4_9 { get; set; }
        public int NivelSumaSemana4_10 { get; set; }

        public int NivelSumaSemana5_1 { get; set; }
        public int NivelSumaSemana5_2 { get; set; }
        public int NivelSumaSemana5_3 { get; set; }
        public int NivelSumaSemana5_4 { get; set; }
        public int NivelSumaSemana5_5 { get; set; }
        public int NivelSumaSemana5_6 { get; set; }
        public int NivelSumaSemana5_7 { get; set; }
        public int NivelSumaSemana5_8 { get; set; }
        public int NivelSumaSemana5_9 { get; set; }
        public int NivelSumaSemana5_10 { get; set; }

    }

    public class ReporteOperacionesEstructuraDTO
    {
        public List<ReporteOperacionesEstructuraDetalleDTO> Cuadro1 { get; set; }
        public List<ReporteOperacionesEstructuraDetalleDTO> Cuadro2 { get; set; }
        public List<ReporteOperacionesEstructuraDetalleDTO> Cuadro3 { get; set; }
    }

    public class ReporteOperacionesDetallesAsignacionCoordinadoraDTO
    {
        public string Coordinador { get; set; }
        public int BoliviaAOnline { get; set; }
        public int BoliviaOnline { get; set; }
        public int BoliviaPresencial { get; set; }
        public int MexicoAOnline { get; set; }
        public int MexicoOnline { get; set; }
        public int MexicoPresencial { get; set; }
        public int ColombiaAOnline { get; set; }
        public int ColombiaOnline { get; set; }
        public int ColombiaPresencial { get; set; }
        public int PeruAOnline { get; set; }
        public int PeruOnline { get; set; }
        public int PeruPresencial { get; set; }
        public int InternacionalAOnline { get; set; }
        public int InternacionalOnline { get; set; }
        public int InternacionalPresencial { get; set; }
        public int Total { get; set; }
    }
    public class ReporteControlContactoTelefonicoDTO
    {
        public string Estado { get; set; }
        public int Orden { get; set; }
        public string DiasSeguimientoActividadesEjecutadas7dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas14dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas21dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas3021dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas60dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas90dias { get; set; }
    }
    public class ReporteAvanceAcademicoPresencialOnlineDTO
    {
        public string Coordinadora { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int Total { get; set; }
    }

    public class ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO
    {
        public string Coordinadora { get; set; }
        public string Tipo { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public int Total { get; set; }
    }
    public class ReporteIndicadoresOperativosDTO
    {
        public string Coordinadora { get; set; }
        public string Estado { get; set; }
        public string Total { get; set; }
    }
    public class ReporteIndicadoresOperativosPorDiaCoorinadorDTO
    {
        public string Dia { get; set; }
        public string Coordinadora { get; set; }
        public string Estado { get; set; }
        public string Total { get; set; }
    }
    public class ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO
    {
        public string Coordinadora { get; set; }
        public string Tipo { get; set; }
        public string EstadoAcademico { get; set; }
        public string EstadoPagos { get; set; }
        public int Total { get; set; }
    }
    public class ReporteAvanceAcademicoAlumnosPagosAtrasados
    {
        public string Coordinadora { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int NumeroAlumnos { get; set; }
        public int NumeroCuotasAtrasadas { get; set; }
        public decimal MontoTotalCuotasAtrasadas { get; set; }
    }
    public class ReporteOperacionesDetallesAsignacionCoordinadoraEstadosDTO
    {
        public string Coordinador { get; set; }
        public int REGULAR { get; set; }
        public int RESERVADO { get; set; }
        public int RETIROAPROBADO { get; set; }
        public int BECA { get; set; }
        public int CULMINADO { get; set; }
        public int REINCORPORADO { get; set; }
        public int CULMINADODEUDOR { get; set; }
        public int ABANDONO { get; set; }
        public int ABANDONOREINCORPORADO { get; set; }
        public int CERTIFICADO { get; set; }
        public int TOTAL { get; set; }
    }
}
