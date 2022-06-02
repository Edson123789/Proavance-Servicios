using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MonedaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string NombrePlural { get; set; }
        public string Simbolo { get; set; }
        public string Codigo { get; set; }
        public int IdPais { get; set; }
		public int DigitoFinanzas { get; set; }
	}
}
