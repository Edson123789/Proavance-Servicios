using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFormularioLandingAb
    {
        public TFormularioLandingAb()
        {
            TSeccionFormularioAb = new HashSet<TSeccionFormularioAb>();
        }

        public int Id { get; set; }
        public int IdTesteoAb { get; set; }
        public string TextoFormulario { get; set; }
        public string NombrePrograma { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TTesteoAb IdTesteoAbNavigation { get; set; }
        public virtual ICollection<TSeccionFormularioAb> TSeccionFormularioAb { get; set; }
    }
}
