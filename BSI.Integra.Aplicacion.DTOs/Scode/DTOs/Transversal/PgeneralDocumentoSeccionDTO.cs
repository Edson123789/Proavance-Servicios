using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralDocumentoSeccionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string pw_duracion { get; set; }
        public List<SeccionDocumentoDTO> ListaSeccion{ get; set; }
		public List<ProgramaGeneralSeccionAnexosHTMLDTO> ListaSeccionV2 { get; set; }
	}

}
