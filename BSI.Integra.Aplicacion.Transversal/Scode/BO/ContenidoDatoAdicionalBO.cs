using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ContenidoDatoAdicionalBO : BaseBO
    {
        public int IdMatriculaCabeceraBeneficios { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdBeneficioDatoAdicional { get; set; }
        public string Contenido { get; set; }
    }
}
