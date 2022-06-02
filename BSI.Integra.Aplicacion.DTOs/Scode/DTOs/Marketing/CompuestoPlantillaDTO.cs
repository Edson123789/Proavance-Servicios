using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion;
using BSI.Integra.Aplicacion.DTO;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoPlantillaDTO
    {
        public DatosPlantillaDTO DatosPlantilla { get; set; }
        public List<PlantillaClavesValoresDTO> PlantillaClaveValor { get; set; }
        public List<int> FasesPlantilla { get; set; }
        public string Usuario { get; set; }
        public int IdPlantilla { get; set; }
        public List<PlantillaAsociacionModuloSistemaDTO> ListaPlantillaAsociacionModuloSistema { get; set; }
        public bool Estado { get; set; }

    }
}
