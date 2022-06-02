using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneralProyectoAplicacionEnvio
    {
        public int Id { get; set; }
        public int IdPgeneralProyectoAplicacionEstado { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime? FechaCalificacion { get; set; }
        public decimal? Nota { get; set; }
        public string NombreArchivoRetroalimentacion { get; set; }
        public string RutaArchivoRetroalimentacion { get; set; }
        public string Comentarios { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
        public bool EsEntregable { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
