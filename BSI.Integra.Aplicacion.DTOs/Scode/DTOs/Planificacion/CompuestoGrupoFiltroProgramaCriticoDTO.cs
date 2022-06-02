using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoGrupoFiltroProgramaCriticoDTO
    {
        public GrupoFiltroProgramaCriticoDTO GrupoFiltroProgramaCritico { get; set; }
        public List<int> GrupoFiltroProgramaCriticoPorAsesor { get; set; }
        public string Usuario { get; set; }
        public int IdGrupo { get; set; }
    }
}
