using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloGeneralBO : BaseBO
    {
        public string Nombre { get; set; }
        public decimal PeIntercepto { get; set; }
        public int PeEstado { get; set; }
        public int PeVersion { get; set; }
        public int? IdPadre { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<ModeloGeneralEscalaBO> ModeloGeneralEscala { get; set; }
        public List<ModeloGeneralCargoBO> ModeloGeneralCargo { get; set; }
        public List<ModeloGeneralIndustriaBO> ModeloGeneralIndustria { get; set; }
        public List<ModeloGeneralAFormacionBO> ModeloGeneralAFormacion { get; set; }
        public List<ModeloGeneralATrabajoBO> ModeloGeneralATrabajo { get; set; }
        public List<ModeloGeneralCategoriaDatoBO> ModeloGeneralCategoriaDato { get; set; }
        public List<ModeloGeneralTipoDatoBO> ModeloGeneralTipoDato { get; set; }

    }
}
