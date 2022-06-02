using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralCertificacion
    {
        public TProgramaGeneralCertificacion()
        {
            TProgramaGeneralCertificacionArgumento = new HashSet<TProgramaGeneralCertificacionArgumento>();
            TProgramaGeneralCertificacionModalidad = new HashSet<TProgramaGeneralCertificacionModalidad>();
        }

        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TProgramaGeneralCertificacionArgumento> TProgramaGeneralCertificacionArgumento { get; set; }
        public virtual ICollection<TProgramaGeneralCertificacionModalidad> TProgramaGeneralCertificacionModalidad { get; set; }
    }
}
