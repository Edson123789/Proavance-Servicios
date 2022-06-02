using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CiudadBO:BaseBO
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public int LongCelular { get; set; }
        public int LongTelefono { get; set; }
    }
}
