using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAsesorCentroCosto
    {
        public TAsesorCentroCosto()
        {
            TAsesorAreaCapacitacionDetalle = new HashSet<TAsesorAreaCapacitacionDetalle>();
            TAsesorGrupoFiltroProgramaCriticoDetalle = new HashSet<TAsesorGrupoFiltroProgramaCriticoDetalle>();
            TAsesorProgramaGeneralDetalle = new HashSet<TAsesorProgramaGeneralDetalle>();
            TAsesorSubAreaCapacitacionDetalle = new HashSet<TAsesorSubAreaCapacitacionDetalle>();
            TAsesorTipoCategoriaOrigenDetalle = new HashSet<TAsesorTipoCategoriaOrigenDetalle>();
        }

        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int AsignacionMax { get; set; }
        public bool Habilitado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? AsignacionMaxBnc { get; set; }
        public int? AsignacionMin { get; set; }
        public string AsignacionPais { get; set; }

        public virtual ICollection<TAsesorAreaCapacitacionDetalle> TAsesorAreaCapacitacionDetalle { get; set; }
        public virtual ICollection<TAsesorGrupoFiltroProgramaCriticoDetalle> TAsesorGrupoFiltroProgramaCriticoDetalle { get; set; }
        public virtual ICollection<TAsesorProgramaGeneralDetalle> TAsesorProgramaGeneralDetalle { get; set; }
        public virtual ICollection<TAsesorSubAreaCapacitacionDetalle> TAsesorSubAreaCapacitacionDetalle { get; set; }
        public virtual ICollection<TAsesorTipoCategoriaOrigenDetalle> TAsesorTipoCategoriaOrigenDetalle { get; set; }
    }
}
