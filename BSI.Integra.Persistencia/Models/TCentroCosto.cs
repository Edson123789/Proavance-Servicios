using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCentroCosto
    {
        public TCentroCosto()
        {
            TCampaniaGeneralDetalle = new HashSet<TCampaniaGeneralDetalle>();
            TGrupoFiltroProgramaCriticoCentroCosto = new HashSet<TGrupoFiltroProgramaCriticoCentroCosto>();
            TPespecifico = new HashSet<TPespecifico>();
            TWebinarCentroCosto = new HashSet<TWebinarCentroCosto>();
        }

        public int Id { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string IdAreaCc { get; set; }
        public int? Ismtotales { get; set; }
        public int? Icpftotales { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TCampaniaGeneralDetalle> TCampaniaGeneralDetalle { get; set; }
        public virtual ICollection<TGrupoFiltroProgramaCriticoCentroCosto> TGrupoFiltroProgramaCriticoCentroCosto { get; set; }
        public virtual ICollection<TPespecifico> TPespecifico { get; set; }
        public virtual ICollection<TWebinarCentroCosto> TWebinarCentroCosto { get; set; }
    }
}
