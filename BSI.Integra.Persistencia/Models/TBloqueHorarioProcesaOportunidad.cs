using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TBloqueHorarioProcesaOportunidad
    {
        public int Id { get; set; }
        public bool Activo { get; set; }
        public string Descripcion { get; set; }
        public string Sede { get; set; }
        public string Dia { get; set; }
        public bool TurnoM { get; set; }
        public TimeSpan HoraInicioM { get; set; }
        public TimeSpan HoraFinM { get; set; }
        public bool TurnoT { get; set; }
        public TimeSpan HoraInicioT { get; set; }
        public TimeSpan HoraFinT { get; set; }
        public string ProbabilidadOportunidad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool Prelanzamiento { get; set; }
        public int? IdDiaSemana { get; set; }
    }
}
