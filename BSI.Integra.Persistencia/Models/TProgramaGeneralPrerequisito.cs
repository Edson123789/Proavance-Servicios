using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralPrerequisito
    {
        public TProgramaGeneralPrerequisito()
        {
            TProgramaGeneralPrerequisitoModalidad = new HashSet<TProgramaGeneralPrerequisitoModalidad>();
        }

        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int? Orden { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TProgramaGeneralPrerequisitoModalidad> TProgramaGeneralPrerequisitoModalidad { get; set; }
    }
}
