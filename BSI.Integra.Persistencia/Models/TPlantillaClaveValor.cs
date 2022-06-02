using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPlantillaClaveValor
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public string Etiquetas { get; set; }
        public int IdPlantilla { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPlantilla IdPlantillaNavigation { get; set; }
    }
}
