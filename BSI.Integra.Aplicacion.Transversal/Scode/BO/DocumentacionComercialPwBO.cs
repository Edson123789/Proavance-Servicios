using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DocumentacionComercialPwBO:BaseBO
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Tipo { get; set; }
        public string Modalidad { get; set; }
        public int? IdPais { get; set; }
    }
}
