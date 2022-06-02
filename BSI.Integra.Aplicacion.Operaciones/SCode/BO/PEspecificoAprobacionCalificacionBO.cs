using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class PEspecificoAprobacionCalificacionBO : BaseBO
    {
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public bool? EsNotaAprobada { get; set; }
        public bool? EsAsistenciaAprobada { get; set; }
        public DateTime? FechaAprobacionNota { get; set; }
        public DateTime? FechaAprobacionAsistencia { get; set; }
    }
}
