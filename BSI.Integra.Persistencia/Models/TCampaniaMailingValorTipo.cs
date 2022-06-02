using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaMailingValorTipo
    {
        public int Id { get; set; }
        public int IdCampaniaMailing { get; set; }
        public int Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TCampaniaMailing IdCampaniaMailingNavigation { get; set; }
    }
}
