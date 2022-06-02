using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DetalleOportunidadCompetidorBO : BaseBO
    {
        public int IdOportunidadCompetidor { get; set; }
        public int IdCompetidor { get; set; }
        public Guid? IdMigracion { get; set; }


        public DetalleOportunidadCompetidorBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
