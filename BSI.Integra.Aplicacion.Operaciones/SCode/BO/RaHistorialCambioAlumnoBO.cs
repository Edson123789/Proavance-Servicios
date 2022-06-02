using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class RaHistorialCambioAlumnoBO : BaseBO
    {
        public int Id { get; set; }
        public string CodigoAlumno { get; set; }
        public int IdRaHistorialCambioAlumnoTipo { get; set; }
        public string CentroCostoOrigen { get; set; }
        public string CentroCostoDestino { get; set; }
        public bool Cancelado { get; set; }
        public bool? Aprobado { get; set; }
        public string UsuarioSolicitud { get; set; }
        public string ComentarioSolicitud { get; set; }
        public string UsuarioAprobacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
