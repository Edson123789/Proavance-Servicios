using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProcesarPrioridadesDTO
    {
        public List<PrioridadesDTO> ListaPrioridades { get; set; }
        public string Usuario { get; set; }
    }
    public class ProcesarPrioridadesCampaniaGeneralDetalleDTO
    {
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Usuario { get; set; }
    }
    public class ValoresCalculadosEliminarTemporalDTO
    {
        public int CantidadMailing { get; set; }
        public int CantidadWhatsapp { get; set; }
    }
}
