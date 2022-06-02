using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class EvaluacionPersonaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public decimal? TiempoLimite { get; set; }
        public bool? CronometrarExamen { get; set; }
        public int IdEvaluacionConfiguracionFormato { get; set; }
        public int IdEvaluacionComportamiento { get; set; }
        public int IdEvaluacionResultado { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPespecificoSesion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
