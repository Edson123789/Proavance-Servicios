using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAccesosIntegraDetalleLog
    {
        public int Id { get; set; }
        public int IdAccesosIntegraLog { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual TAccesosIntegraLog IdAccesosIntegraLogNavigation { get; set; }
    }
}
