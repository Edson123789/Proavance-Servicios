using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralCertificacionArgumentoBO: BaseBO
    {
        public int IdProgramaGeneralCertificacion { get; set; }
        public string Nombre { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
}
