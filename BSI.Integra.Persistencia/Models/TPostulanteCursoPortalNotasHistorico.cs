using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulanteCursoPortalNotasHistorico
    {
        public int Id { get; set; }
        public int IdPostulanteProcesoSeleccion { get; set; }
        public int IdPgeneral { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public string GrupoPregunta { get; set; }
        public decimal? Calificacion { get; set; }
        public string IdUsuario { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPespecifico { get; set; }
        public bool? AccesoPrueba { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
