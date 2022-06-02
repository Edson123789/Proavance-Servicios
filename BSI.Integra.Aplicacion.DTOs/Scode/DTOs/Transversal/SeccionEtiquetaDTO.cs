using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SeccionEtiquetaDTO
    {
        public string Valor { get; set; }
        public Guid? IdPlantillaPW { get; set; }
        public Guid? IdSeccionPW { get; set; }
        public int? IdCentroCosto { get; set; }
    }
}
