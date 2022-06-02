using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametrosFurDTO
    {
        public int IdArea { get; set; }
        public string Codigo { get; set; }
        public int IdRol { get; set; }
        public int IdEstadoFaseAprobacion1 { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
        public string Usuario { get; set; }
		public int? IdCiudad { get; set; }
		public int? Anio { get; set; }
		public int? Semana { get; set; }
    }
}
