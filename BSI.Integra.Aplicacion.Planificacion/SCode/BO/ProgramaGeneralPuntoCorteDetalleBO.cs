using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ProgramaGeneralPuntoCorteDetalleBO : BaseBO
    {
        public int IdProgramaGeneralPuntoCorte { get; set; }
        public int IdPuntoCorte { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
        public int? IdMigracion { get; set; }

    }
}
