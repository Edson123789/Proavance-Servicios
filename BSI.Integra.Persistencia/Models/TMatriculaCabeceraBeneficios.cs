using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaCabeceraBeneficios
    {
        public TMatriculaCabeceraBeneficios()
        {
            TContenidoDatoAdicional = new HashSet<TContenidoDatoAdicional>();
        }

        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Nombre { get; set; }
        public int IdSuscripcionProgramaGeneral { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdConfiguracionBeneficioProgramaGeneral { get; set; }
        public int? IdEstadoMatriculaCabeceraBeneficio { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public int? IdEstadoSolicitudBeneficio { get; set; }
        public int? Duracion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioEntregoBeneficio { get; set; }

        public virtual ICollection<TContenidoDatoAdicional> TContenidoDatoAdicional { get; set; }
    }
}
