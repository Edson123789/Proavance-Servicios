using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ProgramaGeneralPuntoCorteConfiguracionBO: BaseBO
    {
        public int IdTipoCorte { get; set; }
        public string Tipo { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdPgeneral { get; set; }
        public string Color { get; set; }
        public string Texto { get; set; }
        public int? IdMigracion { get; set; }
    }
}
