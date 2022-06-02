using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoInsertarAsesorChatDTO
    {
        public AsesorChatDTO AsesorChat { get; set; }
        public List<int> ListaAreasCapacitacion { get; set; }
        public List<int> ListaSubAreasCapacitacion { get; set; }
        public List<int> ListaPais { get; set; }
        public List<int> ListaProgramaGeneral { get; set; }
    }
}
