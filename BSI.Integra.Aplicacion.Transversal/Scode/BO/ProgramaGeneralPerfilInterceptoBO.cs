using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralPerfilInterceptoBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public double PerfilIntercepto { get; set; }
        public string PerfilEstado { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
