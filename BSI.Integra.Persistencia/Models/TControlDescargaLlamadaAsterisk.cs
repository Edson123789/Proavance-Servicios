using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TControlDescargaLlamadaAsterisk
    {
        public int Id { get; set; }
        public int IdLlamadaInicial { get; set; }
        public int IdLlamadaFinal { get; set; }
        public bool DescargaCorrecta { get; set; }
        public bool DescargaEnProceso { get; set; }
        public bool TieneError { get; set; }
        public string MensajeError { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
