using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFlujoPorPespecifico
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int IdFlujoActividad { get; set; }
        public int? IdFlujoOcurrencia { get; set; }
        public int IdClasificacionPersona { get; set; }
        public DateTime? FechaEjecucion { get; set; }
        public DateTime? FechaSeguimiento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
