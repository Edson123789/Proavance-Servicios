using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCongelamientoPespecificoEsquemaEvaluacionDetalleMatriculaAlumno
    {
        public int Id { get; set; }
        public int IdCongelamientoPespecificoEsquemaEvaluacionMatriculaAlumno { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public string NombreEsquemaDetalle { get; set; }
        public int Ponderacion { get; set; }
        public int? IdProveedor { get; set; }
        public string UrlArchivoInstrucciones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
