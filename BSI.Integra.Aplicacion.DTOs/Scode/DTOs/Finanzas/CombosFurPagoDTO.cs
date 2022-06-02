using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosFurPagoDTO
    {
        public List <FiltroDTO> ListaAreaTrabajo { get; set; }
        public List<FiltroDTO> ListaCiudades { get; set; }
        public List<FiltroGenericoDTO> ListaMoneda { get; set; }
        public List<EstadoFurPagoDTO> ListaEstado { get; set; }
        public List<SunatDocumentoDTO> ListaSunatDocumento { get; set; }
        public List<FiltroDTO> ListaIgv { get; set; }
        public List<FiltroDTO> ListaRetencion { get; set; }
        public List<FiltroDTO> ListaDetraccion { get; set; }
        public List<FiltroDTO> ListaPais { get; set; }
        public List<ComprobantePagoDatosComboDTO> ListaComprobantePago { get; set; }
        public List<FiltroDTO> ListaCuentaCorriente { get; set; }
        public List<FormaPagoDTO> ListaFormaPago { get; set; }
        

    }
}
