using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoFrecuenciaDetalle
    {
        public int Id { get; set; }
        public int? IdPespecificoFrecuencia { get; set; }
        public byte DiaSemana { get; set; }
        public TimeSpan HoraDia { get; set; }
        public decimal Duracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
