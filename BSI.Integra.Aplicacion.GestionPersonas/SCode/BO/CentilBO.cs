using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class CentilBO : BaseBO
    {    
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdExamen { get; set; }
        public int? IdSexo { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal? Centil { get; set; }
        public string CentilLetra { get; set; }        
        public int? IdMigracion { get; set; }
    }
}
