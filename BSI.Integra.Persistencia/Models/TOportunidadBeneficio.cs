using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOportunidadBeneficio
    {
        public int Id { get; set; }
        public int? IdOportunidadCompetidor { get; set; }
        public int? IdBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TOportunidadCompetidor IdOportunidadCompetidorNavigation { get; set; }
    }
}
