using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComboDocumentoConfiguracionRecepcionDTO
    {
        public List<TipoPersonaFiltroDTO> ListaTipoPersona { get; set; }
        public List<FiltroDTO> ListaPais { get; set; }
        public List<DocumentoFiltroDTO> ListaDocumento { get; set; }
        public List<ModalidadCursoFiltroDTO> ListaModalidadCurso { get; set; }
    }
}
