using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AscensoExperienciaCargoIndustriaDTO
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
