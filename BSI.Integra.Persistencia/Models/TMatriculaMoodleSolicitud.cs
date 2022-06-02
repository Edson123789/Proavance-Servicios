using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaMoodleSolicitud
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdCursoMoodle { get; set; }
        public int IdUsuarioMoodle { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime FechaInicioMatricula { get; set; }
        public DateTime FechaFinMatricula { get; set; }
        public int IdMatriculaMoodleSolicitudEstado { get; set; }
        public string UsuarioSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string UsuarioAprobacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
