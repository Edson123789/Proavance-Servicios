using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFormularioSolicitud
    {
        public TFormularioSolicitud()
        {
            TFormularioPlantilla = new HashSet<TFormularioPlantilla>();
        }

        public int Id { get; set; }
        public int? IdFormularioRespuesta { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Campanha { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string Proveedor { get; set; }
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public int TipoSegmento { get; set; }
        public string CodigoSegmento { get; set; }
        public int TipoEvento { get; set; }
        public string UrlbotonInvitacionPagina { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TFormularioPlantilla> TFormularioPlantilla { get; set; }
    }
}
