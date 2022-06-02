using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SuscripcionProgramaGeneralBO: BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int? OrdenBeneficio { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
