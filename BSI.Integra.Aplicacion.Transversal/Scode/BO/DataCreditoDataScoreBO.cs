using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataScoreBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string Tipo { get; set; }
        public string Puntaje { get; set; }
        public DateTime? Fecha { get; set; }
        public string Poblacion { get; set; }
    }
}
