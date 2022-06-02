using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TContenidoCertificadoIrca
    {
        public int Id { get; set; }
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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
