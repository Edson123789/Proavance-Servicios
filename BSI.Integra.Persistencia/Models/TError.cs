using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TError
    {
        public int Id { get; set; }
        public int IdErrorTipo { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionPersonalizada { get; set; }
        public string NombreObjeto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual TErrorTipo IdErrorTipoNavigation { get; set; }
    }
}
