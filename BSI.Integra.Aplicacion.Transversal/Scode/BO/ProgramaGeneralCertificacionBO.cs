using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralCertificacionBO: BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
        public List<ProgramaGeneralCertificacionModalidadBO> ProgramaGeneralCertificacionModalidad { get; set; }
        public List<ProgramaGeneralCertificacionArgumentoBO> programaGeneralCertificacionArgumento { get; set; }
    }
}
