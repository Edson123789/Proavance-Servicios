using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TGrupoFiltroProgramaCriticoCentroCosto
    {
        public int Id { get; set; }
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public int IdCentroCosto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TCentroCosto IdCentroCostoNavigation { get; set; }
        public virtual TGrupoFiltroProgramaCritico IdGrupoFiltroProgramaCriticoNavigation { get; set; }
    }
}
