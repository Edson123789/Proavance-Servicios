using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class AsignacionAutomaticaAsesorPosibilidadDTO
    {
        public int IdAsesor { get; set; }
        public int AsignadosTotalesBNC { get; set; }
        public int AsignadosTotales { get; set; }
        public int Minimo { get; set; }
        public int MaximoBNC { get; set; }
        public int MaximoTotal { get; set; }

    }
}
