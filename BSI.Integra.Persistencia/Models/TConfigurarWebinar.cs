using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfigurarWebinar
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public string Modalidad { get; set; }
        public string Codigo { get; set; }
        public int IdOperadorComparacionAvance { get; set; }
        public int ValorAvance { get; set; }
        public int? ValorAvanceOpc { get; set; }
        public int IdOperadorComparacionPromedio { get; set; }
        public int ValorPromedio { get; set; }
        public int? ValorPromedioOpc { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdPespecificoPadre { get; set; }

        public virtual TPespecifico IdPespecificoNavigation { get; set; }
    }
}
