using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAccionFormularioPorCampoContacto
    {
        public int Id { get; set; }
        public int? IdAccionFormulario { get; set; }
        public int IdCampoContacto { get; set; }
        public int Orden { get; set; }
        public string Campo { get; set; }
        public bool EsSiempreVisible { get; set; }
        public bool EsInteligente { get; set; }
        public bool TieneProbabilidad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid IdMigracion { get; set; }

        public virtual TAccionFormulario IdAccionFormularioNavigation { get; set; }
        public virtual TCampoContacto IdCampoContactoNavigation { get; set; }
    }
}
