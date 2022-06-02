using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MatriculaCabeceraNoRecordatorioBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public bool EnviarRecordatorioPago { get; set; }
        public bool EnviarRankingAcademico { get; set; }
        public DateTime FechaReincorporacion { get; set; }
        public bool EnviarSorteosPromociones { get; set; }
        public bool EnviarRecordatorioCumpleanos { get; set; }
        public bool EnviarRecordatorioSesionPresencial { get; set; }
        public bool EnviarCartaCobranza { get; set; }
        public int? IdMigracion { get; set; }
    }
}
