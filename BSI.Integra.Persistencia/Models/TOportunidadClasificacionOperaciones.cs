using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOportunidadClasificacionOperaciones
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int DiasAtrasoCuotaPago { get; set; }
        public int CuotasAtrasoCuotaPago { get; set; }
        public decimal MontoAtrasoCuotaPago { get; set; }
        public string MonedaCuotaPago { get; set; }
        public int IdAgendaTab { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? DiasSeguimiento { get; set; }
        public int? DiasActividadesEjecutadas { get; set; }
        public string UltimoContacto { get; set; }
        public DateTime? FechaUltimaActividadEjecutada { get; set; }
        public int? DiasSeguimiento7dias { get; set; }
        public int? DiasActividadesEjecutadas7dias { get; set; }
        public int? DiasSeguimiento14dias { get; set; }
        public int? DiasActividadesEjecutadas14dias { get; set; }
        public int? DiasSeguimiento21dias { get; set; }
        public int? DiasActividadesEjecutadas21dias { get; set; }
        public int? DiasSeguimiento3014dias { get; set; }
        public int? DiasActividadesEjecutadas3014dias { get; set; }
        public int? DiasSeguimiento3021dias { get; set; }
        public int? DiasActividadesEjecutadas3021dias { get; set; }
        public int? DiasSeguimiento60dias { get; set; }
        public int? DiasActividadesEjecutadas60dias { get; set; }
        public int? DiasSeguimiento90dias { get; set; }
        public int? DiasActividadesEjecutadas90dias { get; set; }
        public int? DiasAtrasoAvanceAcademico { get; set; }
        public string EstadoAutoevaluaciones { get; set; }
        public DateTime? FechaProximaCuota { get; set; }
        public int? DiasAtrasoAvanceAcademicoSinProyecto { get; set; }
        public int? IdTarifario { get; set; }
        public int? PorcentajeAvanceAcademicosinProyecto { get; set; }
        public decimal? NotaPromedio { get; set; }
        public bool? TieneProyectoFinal { get; set; }
        public int? PorcentajeAvanceAcademico { get; set; }
        public int? DiasDesdeUltimoAvance { get; set; }
        public string AvanceReal { get; set; }
        public string AvanceRealSesion { get; set; }
        public string AvanceRealAutoevaluacion { get; set; }
        public int? ValorAvanceReal { get; set; }
        public int? ReproduccionVideoReal { get; set; }
        public string AvanceProgramado { get; set; }
        public string AvanceProgramadoSesion { get; set; }
        public string AvanceProgramadoAutoevaluacion { get; set; }
        public int? ValorAvanceProgramado { get; set; }
        public int? ReproduccionVideoProgramado { get; set; }
        public int? IdEstadoCompromiso { get; set; }

        public virtual TAgendaTab IdAgendaTabNavigation { get; set; }
        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; }
        public virtual TOportunidad IdOportunidadNavigation { get; set; }
    }
}
