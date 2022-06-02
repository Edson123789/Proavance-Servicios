using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class BloqueHorarioProcesaOportunidadBO : BaseEntity
    {
        public bool Activo { get; set; }
        public bool Prelanzamiento { get; set; }
        public int? IdDiaSemana { get; set; }
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
        //public List<string> ProbabilidadOportunidad { get; set; }
        public byte[] RowVersion { get; set; }

        public BloqueHorarioProcesaOportunidadBO()
        {
        }

    }
}
