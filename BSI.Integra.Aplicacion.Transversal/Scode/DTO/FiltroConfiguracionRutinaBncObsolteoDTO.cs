using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroConfiguracionRutinaBncObsolteoDTO
    {
        public List<FaseOportunidadFiltroDTO> FaseOportunidad { get; set;}
        public List<CategoriaOrigenFiltroDTO> CategoriaOrigen { get; set;}
        public List<TipoDatoFiltroDTO> TipoDato { get; set;}
        public List<PlantillaDTO> Plantilla { get; set;}
        public List<AsesorFiltroDTO> Personal { get; set;}
        public List<FiltroOcurrenciaDTO> Ocurrencia { get; set;}
    }
}
