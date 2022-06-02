using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampaniaDetalleAutomaticoServiceDTO
    {
        public int IdCampaniaMailingDetalle { get; set; }
        public int IdCampaniaMailing { get; set; }
        public int IdConjuntoLista { get; set; }
        public bool? ActivoEjecutarFiltro { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
    }

    public class MailingMasivoAutomaticoServiceDTO
    {
        public int IdConjuntoLista { get; set; }
        public bool? ActivoEjecutarFiltro { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
    }
}
