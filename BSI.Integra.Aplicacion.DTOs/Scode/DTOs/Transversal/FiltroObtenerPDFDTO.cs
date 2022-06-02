using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroObtenerPDFDTO
    {
        public int IdPespecifico { get; set; }
        public bool CursoIndividual { get; set; }
        public string CursoNombre { get; set; }
		public int Grupo { get; set; }
		public string Usuario { get; set; }
		public List<PespecificoSesionCompuestoDTO> Sesion { get; set; }
    }
}
