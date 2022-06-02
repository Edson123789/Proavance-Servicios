using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsociacionGrupoFiltroCentroCostoDTO
    {
        public List<CentroCostoSubAreaDTO> ListaCentroCosto { get; set; }
        public string Usuario { get; set; }
        public int IdGrupo { get; set; }
    }

    public class AsociacionGrupoFiltroPGeneralDTO
    {
        public List<PGeneralSubAreaDTO> ListaPGeneral { get; set; }
        public string Usuario { get; set; }
        public int IdGrupo { get; set; }
    }
}
