using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppPlantillaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string EspacioNombre { get; set; }
        public string CodigoIdioma { get; set; }
        public string Contenido { get; set; }
        public int? IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
