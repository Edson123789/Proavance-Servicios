using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SeguimientoAsesorDTO
    {
        public int OportunidadesMaximas { get; set; }
        public string Grupo { get; set; }
        public int TasaConversion { get; set; }
        public int OportunidadesCerradas { get; set; }
        public int OportunidadesRestantes { get; set; }
    }
}
