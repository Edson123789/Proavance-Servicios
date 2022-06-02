using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GrupoFiltroProgramaCriticoCombosDTO
    {
        public List<FiltroDTO> ListaAreaCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> ListaSubAreaCapacitacion { get; set; }
    }
}
