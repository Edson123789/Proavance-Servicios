using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class PersonalExperienciaRequeridaBO : BaseBO
    {
        public int IdPuestoTrabajo { get; set; }
        public int IdIntervaloTiempo { get; set; }
        public int NumeroTiempoMinimo { get; set; }
        public decimal? IdMigracion { get; set; }
    }
}
