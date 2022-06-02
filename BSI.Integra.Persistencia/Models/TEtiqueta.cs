using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEtiqueta
    {
        public TEtiqueta()
        {
            TEtiquetaBotonReemplazo = new HashSet<TEtiquetaBotonReemplazo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CampoDb { get; set; }
        public bool NodoPadre { get; set; }
        public int? IdNodoPadre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdTipoEtiqueta { get; set; }

        public virtual ICollection<TEtiquetaBotonReemplazo> TEtiquetaBotonReemplazo { get; set; }
    }
}
