using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class AscensoExperienciaCargoIndustriaBO : BaseBO
    {
        public int Id { get; set; }
        public int IdAscenso { get; set; }
        public int AniosExperiencia { get; set; }
        public int IdCargo { get; set; }
        public int IdIndustria { get; set; }
        public int IdAreaTrabajo { get; set; }
        public string DescripcionPuestoAnterior { get; set; }
       

    }
}
