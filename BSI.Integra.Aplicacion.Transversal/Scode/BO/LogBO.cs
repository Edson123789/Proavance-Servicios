using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class LogBO : BaseBO
    {
        public string Ip { get; set; }
        public string Usuario { get; set; }
        public string Maquina { get; set; }
        public string Ruta { get; set; }
        public string Parametros { get; set; }
        public string Mensaje { get; set; }
        public string Excepcion { get; set; }
        public string Tipo { get; set; }
        public int? IdPadre { get; set; }
    }
}
