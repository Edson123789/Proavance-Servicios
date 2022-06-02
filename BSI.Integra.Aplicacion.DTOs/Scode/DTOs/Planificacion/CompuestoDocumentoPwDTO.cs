using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoDocumentoPwDTO
    {
        public DocumentoPwDTO ObjetoDocumento { get; set; }
        public List<DocumentoSeccionPwFiltroDTO> Lista { get; set; }
        public List<RevisionNivelPwFiltroIdPlantillaDTO> ListaRevision { get; set; }
        public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionPresencial { get; set; }
        public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionOnline { get; set; }
        public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionAOnline { get; set; }
        public string Usuario { get; set; }
    }
}
