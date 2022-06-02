using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroMensajesWhatsAppRespondidosDTO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int BandejaRespondidos { get; set; }
        public List<ValorIntDTO> ListaEstadoCreacionOportunidad { get; set; }
        public GridFiltersDTO FiltroKendo { get; set; }
    }
}
