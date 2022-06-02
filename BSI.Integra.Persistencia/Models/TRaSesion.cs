using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRaSesion
    {
        public int Id { get; set; }
        public int? IdRaCurso { get; set; }
        public int? IdRaSesionTipo { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public string Horario { get; set; }
        public int? IdRaSede { get; set; }
        public int? IdRaPresencialAula { get; set; }
        public int? IdExpositor { get; set; }
        public DateTime? HoraIngresoProfesor { get; set; }
        public DateTime? HoraInicioClase { get; set; }
        public int? NumeroAlumnoInicio { get; set; }
        public DateTime? HoraTerminoClase { get; set; }
        public string ConformidadSesion { get; set; }
        public string Observacion { get; set; }
        public decimal? ResultadoParticipacionDocente { get; set; }
        public int? CumplimientoParticipacionDocente { get; set; }
        public DateTime? HoraInicioBreak { get; set; }
        public DateTime? HoraTerminoBreak { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
