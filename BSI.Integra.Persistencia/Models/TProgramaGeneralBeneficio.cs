using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralBeneficio
    {
        public TProgramaGeneralBeneficio()
        {
            TProgramaGeneralBeneficioArgumento = new HashSet<TProgramaGeneralBeneficioArgumento>();
            TProgramaGeneralBeneficioModalidad = new HashSet<TProgramaGeneralBeneficioModalidad>();
        }

        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TProgramaGeneralBeneficioArgumento> TProgramaGeneralBeneficioArgumento { get; set; }
        public virtual ICollection<TProgramaGeneralBeneficioModalidad> TProgramaGeneralBeneficioModalidad { get; set; }
    }
}
