using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FacebookAudienciaActividadDTO
    {
        public int IdFiltroSegmento { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public List<FiltroDTO> Cuenta { get; set; }
        public string Usuario { get; set; }
    }
}
