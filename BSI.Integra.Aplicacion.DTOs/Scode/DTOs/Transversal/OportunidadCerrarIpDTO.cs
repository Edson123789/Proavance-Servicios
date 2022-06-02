using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadCerrarIpDTO
    {
        public int Reasignarme { get; set; }
        public int Cerrarme { get; set; }
        public int IdAsignadoA { get; set; }
        public int IdOportunidad { get; set; }
    }
}
