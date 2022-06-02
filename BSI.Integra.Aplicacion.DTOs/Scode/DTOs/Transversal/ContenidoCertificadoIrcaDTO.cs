using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ContenidoCertificadoIrcaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int CursoIrcaId { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DuracionCurso { get; set; }
        public string ResultadoCurso { get; set; }
        public int IdCentroCostoIrca { get; set; }
        public bool Procesado { get; set; }
        public string CentroCostoIrca { get; set; }
        public string Usuario { get; set; }
    }
}
