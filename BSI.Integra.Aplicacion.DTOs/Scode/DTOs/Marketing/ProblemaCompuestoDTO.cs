using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProblemaCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
   
        public List<IndicadorProblemaCompuestoDTO> Indicadores { get; set; }
        public List<ProblemaHorarioDTO> ProblemaHorarios {get; set;}


        public string Usuario { get; set; }
    }
}
