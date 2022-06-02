using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloGeneralCompuestoDTO
    {
        public ModeloGeneralDTO Modelo { get; set; }
        public List<ModeloGeneralEscalaDTO> ListaEscalaProbabilidad { get; set; }
        public List<ModeloGeneralCargoDTO> ListaCargo { get; set; }
        public List<ModeloGeneralIndustriaDTO> ListaIndustria { get; set; }
        public List<ModeloGeneralAFormacionDTO> ListaAFormacion { get; set; }
        public List<ModeloGeneralATrabajoDTO> ListaATrabajo { get; set; }
        public List<ModeloGeneralCategoriaDatoDTO> ListaCategoriaDato { get; set; }
        public List<int> ListaTipoDato { get; set; }
        public string Usuario { get; set; }
    }
}
