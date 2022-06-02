using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSuscripcionProgramaGeneral
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int? OrdenBeneficio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; }
    }
}
