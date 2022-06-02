using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Maestros.BO
{
    public class BloqueHorarioBO : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int? IdConfiguracionBic { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
