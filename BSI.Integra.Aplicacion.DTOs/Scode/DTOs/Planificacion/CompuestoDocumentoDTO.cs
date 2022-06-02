using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoDocumentoDTO
    {
        public DocumentoPwDTO ObjetoDocumento { get; set; }
        //public List<CompuestoPlantillaSeccionMaestraFiltroDTO> Lista { get; set; }
        public List<SeccionPwFiltroPlantillaPwDTO> Lista { get; set; }
        public List<RevisionNivelPwFiltroIdPlantillaDTO> ListaRevision { get; set; }
        public string Usuario { get; set; }
    }
}
