using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosPresupuestoDTO
    {
        public List<FiltroDTO> ListaCiudades { get; set; }
        public List<FiltroDTO> ListaTipoPedido { get; set; }
        public List<FurTipoSolicitudDTO> ListaRubro { get; set; }
        public List<FiltroDTO> ListaEstadoFaseFur { get; set; }
        public List<FiltroDTO> ListaEstadoSubFaseFur { get; set; }
        public List<FiltroDTO> ListaEstadoComprobante { get; set; }


    }
}
