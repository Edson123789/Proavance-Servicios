using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BloqueHorarioProcesaOportunidadDTO
    {
        public int Id { get; set; }
        public int? IdDiaSemana { get; set; }
        public string NombreDiaSemana { get; set; }
        public string ProbabilidadOportunidad { get; set; }
        public bool TurnoM  { get; set; }
        public string NombreTurnoM { get; set; }
        public bool TurnoT  { get; set; }
        public string NombreTurnoT { get; set; }
        public TimeSpan HoraInicioM { get; set; }
        public TimeSpan HoraFinM  { get; set; }
        public TimeSpan HoraInicioT { get; set; }
        public TimeSpan HoraFinT { get; set; }
        public bool Prelanzamiento { get; set; }
        public string NombreUsuario { get; set; }
    }
}
