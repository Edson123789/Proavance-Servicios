using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
     public class OportunidadBeneficioBO : BaseBO
    {
        public int? IdOportunidadCompetidor { get; set; }
        public int? IdBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public Guid? IdMigracion { get; set; }
        public OportunidadBeneficioBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
