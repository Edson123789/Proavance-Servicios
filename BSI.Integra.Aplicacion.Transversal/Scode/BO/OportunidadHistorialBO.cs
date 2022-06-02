using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OportunidadHistorialBO
    {
        public int IdOportunidad { get; set; }
        public string Programa { get; set; }
        public string Area { get; set; }
        public string Grupo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Probabilidad { get; set; }
        public string FaseMaxima { get; set; }
        public string FaseOportunidad { get; set; }
        public string Precio { get; set; }
        public string Comision { get; set; }
        public string MontoTotal { get; set; }
        public string IdBusqueda { get; set; }
        public string Asesor { get; set; }
    }
}
