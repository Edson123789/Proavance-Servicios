using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class InteraccionBO : BaseBO
    {
        public int? IdActividadDetalle { get; set; }
        public int? IdTipoInteraccionGeneral { get; set; }
        public DateTime Fecha { get; set; }

        public InteraccionBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}