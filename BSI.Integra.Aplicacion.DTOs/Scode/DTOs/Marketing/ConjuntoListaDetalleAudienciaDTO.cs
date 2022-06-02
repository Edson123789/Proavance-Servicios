using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConjuntoListaDetalleAudienciaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public List<FacebookCuentaPublicitariaDTO> Cuenta { get; set; }
    }
}
