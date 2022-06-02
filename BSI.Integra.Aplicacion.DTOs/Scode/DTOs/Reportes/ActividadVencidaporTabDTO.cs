using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadVencidaporTabDTO
    {
        public string Dia { get; set; }
        public string Estado { get; set; }
        public string Total { get; set; }

    }
    public class ActividadVencidaporTabPorDiaAgrupadoDTO
    {
        public string Dia { get; set; }
        public List<ActividadVencidaporTabDTO> Detalle { get; set; }
    }
}
