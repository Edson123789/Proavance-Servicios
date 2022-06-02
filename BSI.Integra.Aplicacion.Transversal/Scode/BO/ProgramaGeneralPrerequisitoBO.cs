using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralPrerequisitoBO: BaseBO
    {
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int? Orden { get; set; }
		public Guid? IdMigracion { get; set; }
		public List<ProgramaGeneralPrerequisitoModalidadBO> ProgramaGeneralPrerequisitoModalidad { get; set; }
    }
}
