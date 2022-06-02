using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRegistroCertificadoFisicoGenerado
    {
        public int Id { get; set; }
        public int IdSolicitudCertificadoFisico { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string FormatoArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime UltimaFechaGeneracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TSolicitudCertificadoFisico IdSolicitudCertificadoFisicoNavigation { get; set; }
        public virtual TUrlBlockStorage IdUrlBlockStorageNavigation { get; set; }
    }
}
