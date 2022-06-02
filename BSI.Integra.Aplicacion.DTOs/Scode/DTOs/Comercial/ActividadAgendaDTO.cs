using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadAgendaDTO
    {
        public int Id { get; set; }
        //public string TipoActividad { get; set; }
        public string EstadoHoja { get; set; }
        public string CentroCosto { get; set; }
        public string PEspecifico { get; set; }
        public string Modalidad { get; set; }
        public DateTime? FechaPrimeraSesion { get; set; }
        public int? ValidoAccesoTemporal { get; set; }
        public string Contacto { get; set; }
        //public int? IdCargo { get; set; }
        //public int? IdAFormacion { get; set; }
        //public int? IdATrabajo { get; set; }
        //public int? IdIndustria { get; set; }
        //public int IdCodigoFase { get; set; }
        public string CodigoFase { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string UltimoComentario { get; set; }
        public int IdActividadCabecera { get; set; }
        //public int ActividadesVencidas { get; set; }
        public bool ReprogramacionManual { get; set; }
        public bool ReprogramacionAutomatica { get; set; }
        public string ActividadCabecera { get; set; }
        public string Asesor { get; set; }
        //public int TipoDatoPreoridad { get; set; }
        //public int OrigenPrioridad { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdFaseOportunidad { get; set; }
        //public string NombreFaseOportunidad { get; set; }
        public int IdTipoDato { get; set; }
        public string ProbabilidadActualDesc { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaDescripcion { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public bool ValidaLlamada { get; set; }
        //public string EstadoOportunidad { get; set; }
        //public int IdTipoCategoriaOrigen { get; set; }
        public int? DiasSinContactoManhana { get; set; }
        public int? DiasSinContactoTarde { get; set; }
        public int? ActividadesManhana { get; set; }
        public int? ActividadesTarde { get; set; }
        public int? IdPadre { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string DNI { get; set; }
        public int? DiasAtrasoCuotaPago { get; set; }
        public string EstadoMatricula { get; set; }
        public int? GrupoCurso { get; set; }
        public string SubEstadoMatricula { get; set; }
        public int? DiasSeguimiento { get; set; }
        public int? DiasActividadesEjecutadas { get; set; }
        public string Tarifario { get; set; }
        public DateTime? FechaGrabacion { get; set; }
        public DateTime? FechaVerificacion { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public int? ActividadTotalUltimos7Dias { get; set; }
        public int? ActividadEjecutadaUltimos7Dias { get; set; }
        public int? NumeroDiasActividadesReprogramadas { get; set; }
        public int? TotalDiaActual { get; set; }
        public int? EjecutadasDiaActual { get; set; }
        public string TipoSolicitudOperaciones { get; set; }
    }
}
