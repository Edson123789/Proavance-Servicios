using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoAgendaFiltroDTO
    {
        public string idOportunidad { get; set; }
        public string IdEstado { get; set; }//estado
        public string IdAlumno{ get; set; }//contacto 
        public string IdsAsesores { get; set; }
        public string IdFaseOportunidad { get; set; }//fase
        public string IdTipoDato { get; set; }//tipoDato 
        public string IdOrigen { get; set; }//origen 
        public string Fecha { get; set; }
        public string IdCentroCosto { get; set; }//centroCosto

    public int pageSize { get; set; }
        public int skip { get; set; }
        public GridFilters filter { get; set; }

        public int IdPersonal { get; set; }//idPersonal
        public string categoria { get; set; }
        public string IdProbabilidadActual { get; set; }//probActual
}
}
