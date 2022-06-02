using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistrarOportunidadFitroGrillaDTO
    {
        public Paginador paginador { get; set; }
        public FiltrosRegistrarOportunidadDTO filtro { get; set; }
    }
}
