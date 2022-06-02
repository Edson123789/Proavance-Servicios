using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class GrupoComponenteEvaluacionBO :BaseBO
    {
        public string Nombre { get; set; }
        public string NombreAbreviado { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public bool RequiereCentil { get; set; }
        public int? IdMigracion { get; set; }
        public decimal? Factor { get; set; }
    }
}
