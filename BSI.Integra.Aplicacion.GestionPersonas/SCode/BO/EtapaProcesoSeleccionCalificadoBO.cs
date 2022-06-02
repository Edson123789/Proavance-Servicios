using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class EtapaProcesoSeleccionCalificadoBO : BaseBO
    {
        public int IdProcesoSeleccionEtapa { get; set; }
        public int IdPostulante { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public decimal? NotaCalculada { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool? EsEtapaActual { get; set; }
        public bool? EsContactado { get; set; }
    }
}
