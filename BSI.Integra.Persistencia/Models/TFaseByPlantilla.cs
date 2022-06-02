using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFaseByPlantilla
    {
        public int Id { get; set; }
        public int IdPlantilla { get; set; }
        public int IdFaseOrigen { get; set; }
        public string NombreFase { get; set; }
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
