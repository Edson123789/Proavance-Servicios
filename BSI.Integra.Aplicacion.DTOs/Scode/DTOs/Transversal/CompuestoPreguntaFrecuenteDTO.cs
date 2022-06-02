using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CompuestoPreguntaFrecuenteDTO
	{
		public PreguntaFrecuenteDTO PreguntaFrecuente { get; set; }
        public List<int> ListaAreas { get; set; }
        public List<int> ListaSubAreas { get; set; }
        public List<int> ListaPGenerales { get; set; }
        public List<int> ListaTipos { get; set; }
        public string Usuario { get; set; }
    }
}
