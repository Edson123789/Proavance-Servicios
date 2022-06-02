using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialEnvioDetalle
    {
        public int Id { get; set; }
        public int IdMaterialEnvio { get; set; }
        public int IdMaterialVersion { get; set; }
        public int IdMaterialEstadoRecepcion { get; set; }
        public int IdPersonalReceptor { get; set; }
        public int CantidadEnvio { get; set; }
        public int CantidadRecepcion { get; set; }
        public string ComentarioEnvio { get; set; }
        public string ComentarioRecepcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMaterialEnvio IdMaterialEnvioNavigation { get; set; }
    }
}
