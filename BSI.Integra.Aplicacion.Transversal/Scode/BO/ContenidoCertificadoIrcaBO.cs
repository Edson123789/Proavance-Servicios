using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ContenidoCertificadoIrcaBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int CursoIrcaId { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DuracionCurso { get; set; }
        public string ResultadoCurso { get; set; }
        public int IdCentroCostoIrca { get; set; }
        public bool Procesado { get; set; }
        public int? IdMigracion { get; set; }
    }
}
