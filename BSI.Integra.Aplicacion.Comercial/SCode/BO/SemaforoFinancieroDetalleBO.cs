using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SemaforoFinancieroDetalleBO: BaseBO
    {
        public int IdSemaforoFinanciero { get; set; }
        public string Nombre { get; set; }
        public string Mensaje { get; set; }
        public string Color { get; set; }
        public int? IdMigracion { get; set; }
    }
}
