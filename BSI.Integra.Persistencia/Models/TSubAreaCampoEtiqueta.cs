using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSubAreaCampoEtiqueta
    {
        public TSubAreaCampoEtiqueta()
        {
            TCampoEtiqueta = new HashSet<TCampoEtiqueta>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdAreaCampoEtiqueta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TAreaCampoEtiqueta IdAreaCampoEtiquetaNavigation { get; set; }
        public virtual ICollection<TCampoEtiqueta> TCampoEtiqueta { get; set; }
    }
}
