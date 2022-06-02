using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteControlDocumentosDTO
    {
        public string ProgramaEspecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdMatriculaObservacion { get; set; }
        public string EstadoMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string PeriodoMatricula { get; set; }
        public decimal PagoAcumulado { get; set; }
        public string CriterioCalificacion { get; set; }
        public string QuienEntregoDoc { get; set; }
        public DateTime? FechaEntregaDocumento { get; set; }
        public string Observaciones { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public DateTime? FechaPrimerPago { get; set; }
        public string Documentos { get; set; }
        public Nullable<int> Cronograma { get; set; }
        public Nullable<int> Convenio { get; set; }
        public Nullable<int> Pagare { get; set; }
        public Nullable<int> Carta_Autorizacion { get; set; }
        public Nullable<int> Hoja_Requisitos { get; set; }
        public Nullable<int> Orden_compra { get; set; }
        public Nullable<int> Carta_compromiso { get; set; }
        public Nullable<int> DNI { get; set; }

    }

    public class ReporteDocumentosDTO
    {
        public DateTime FechaCierre { get; set; }
        public string NombrePersonalAsesor { get; set; }
        public string NombrePersonalCoordinador { get; set; }
        public int NumeroIS { get; set; }
        public int ContratoVoz { get; set; }
        public int ContratoFirmado { get; set; }
        public int Empresa { get; set; }
        public int SinDocumentacion { get; set; }
        public decimal Convenio { get; set; }
        public decimal SinDocumentacionP { get; set; }

    }

    public class ReporteDocuemntosAgrupadoDTO
    {
        public string Fecha { get; set; }
        public List<ReporteDocumentosVistaDTO> DetalleFecha { get; set; }
    }

    public class ReporteDocumentosVistaDTO
    {
        public DateTime FechaCierre { get; set; }
        public string NombrePersonalAsesor { get; set; }
        public string Coordinador { get; set; }
        public int NumeroIS { get; set; }
        public int ContratoVoz { get; set; }
        public int ContratoFirmado { get; set; }
        public int Empresa { get; set; }
        public int SinDocumentacion { get; set; }
        public decimal Convenio { get; set; }
        public decimal SinDocumentacionP { get; set; }

    }

    public class ReporteDocumentosCompuestoDTO
    {
        public List<ReporteDocuemntosAgrupadoDTO> ReporteDocumentosAsesor { get; set; }
        public List<ReporteDocuemntosAgrupadoDTO> ReporteDocumentosEquipo { get; set; }
        public List<ReporteDocuemntosAgrupadoDTO> ReporteDocumentosCoordinador { get; set; }

    }
}