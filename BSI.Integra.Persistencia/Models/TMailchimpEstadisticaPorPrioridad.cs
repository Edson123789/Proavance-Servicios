using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMailchimpEstadisticaPorPrioridad
    {
        public int Id { get; set; }
        public int? IdPrioridadMailChimpLista { get; set; }
        public int CantidadCorreoAbierto { get; set; }
        public DateTime FechaConsulta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
