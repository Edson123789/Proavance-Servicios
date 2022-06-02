using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OcurrenciaReporteAlternoBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public bool? Oportunidad { get; set; }
        public string RequiereLlamada { get; set; }
        public string Roles { get; set; }
        public string Color { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
