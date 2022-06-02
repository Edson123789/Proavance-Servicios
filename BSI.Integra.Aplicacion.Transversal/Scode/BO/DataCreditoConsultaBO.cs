using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoConsultaBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public string TipoCuenta { get; set; }
        public string Entidad { get; set; }
        public string Oficina { get; set; }
        public string Ciudad { get; set; }
        public string Razon { get; set; }
        public string Cantidad { get; set; }
        public string NitSuscriptor { get; set; }
    }
}
