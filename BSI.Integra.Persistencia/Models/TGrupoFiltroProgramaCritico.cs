using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TGrupoFiltroProgramaCritico
    {
        public TGrupoFiltroProgramaCritico()
        {
            TAsesorGrupoFiltroProgramaCriticoDetalle = new HashSet<TAsesorGrupoFiltroProgramaCriticoDetalle>();
            TGrupoFiltroProgramaCriticoCentroCosto = new HashSet<TGrupoFiltroProgramaCriticoCentroCosto>();
            TGrupoFiltroProgramaCriticoPorAsesor = new HashSet<TGrupoFiltroProgramaCriticoPorAsesor>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TAsesorGrupoFiltroProgramaCriticoDetalle> TAsesorGrupoFiltroProgramaCriticoDetalle { get; set; }
        public virtual ICollection<TGrupoFiltroProgramaCriticoCentroCosto> TGrupoFiltroProgramaCriticoCentroCosto { get; set; }
        public virtual ICollection<TGrupoFiltroProgramaCriticoPorAsesor> TGrupoFiltroProgramaCriticoPorAsesor { get; set; }
    }
}
