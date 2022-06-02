using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampoContacto
    {
        public TCampoContacto()
        {
            TAccionFormularioPorCampoContacto = new HashSet<TAccionFormularioPorCampoContacto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoControl { get; set; }
        public int ValoresPreEstablecidos { get; set; }
        public string Procedimiento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TAccionFormularioPorCampoContacto> TAccionFormularioPorCampoContacto { get; set; }
    }
}
