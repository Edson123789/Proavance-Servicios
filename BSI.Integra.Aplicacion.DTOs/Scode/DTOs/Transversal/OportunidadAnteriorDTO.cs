using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadAnteriorDTO
    {
        public int IdOportunidad { get; set; }
        public string FaseOportunidad { get; set; }
        public string FaseMaxima { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public string Probabilidad { get; set; }
        public string Grupo { get; set; }
        public Nullable<DateTime> FechaSolicitud { get; set; }
        public string Asesor { get; set; }
        public string TipoDato { get; set; }
        public string CategoriaOrigen { get; set; }
    }
}
