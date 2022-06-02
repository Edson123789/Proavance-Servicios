using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TListaPlantilla
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Disenho { get; set; }
        public int? TipoDispositivo { get; set; }
        public int? TipoPlantilla { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
