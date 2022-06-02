using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SentinelSueldoIndividualListasFiltrosDTO
    {
        public List<int> Industrias { get; set; }
        public List<int> Cargos { get; set; }
        public List<int> AreaFormaciones { get; set; }
        public List<int> AreaTrabajos { get; set; }
        public List<int> Paises { get; set; }
        public List<int> Empresas { get; set; }
        public List<int> Categorias { get; set; }

    }
}
