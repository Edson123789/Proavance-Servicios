using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoPremio
    {
        public int Id { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string Denominacion { get; set; }
        public int IdFrecuencia { get; set; }
        public string Premio { get; set; }
        public string Reconocimiento { get; set; }
        public decimal MontoMeta { get; set; }
        public int IdMoneda { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
