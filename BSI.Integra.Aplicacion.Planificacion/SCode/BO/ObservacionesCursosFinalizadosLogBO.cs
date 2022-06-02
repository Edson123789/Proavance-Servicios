using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ObservacionesCursosFinalizadosLogBO : BaseBO
    {        
        public int? IdPespecifico { get; set; }
        public string ObservacionCursoFinalizado { get; set; }
        public int? IdMigracion { get; set; }
    }
}
