using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class RaCursoMaterialBO : BaseBO
    { 
        public int Id { get; set; }
        public int IdRaCurso { get; set; }
        public int IdRaTipoCursoMaterial { get; set; }
        public int? Cantidad { get; set; }
        public string Ruta { get; set; }
        public string NombreArchivo { get; set; }
        public string ContentTypeArchivoOriginal { get; set; }
        public string NombreArchivoEditado { get; set; }
        public string ContentTypeArchivoEditado { get; set; }
        public string NombreArchivoAlumno { get; set; }
        public string ContentTypeArchivoAlumno { get; set; }
        public int? IdRaSede { get; set; }
        public int? IdRaProveedorMaterial { get; set; }
        public string ObservacionProveedor { get; set; }
        public DateTime? FechaSubida { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public DateTime? FechaEnvioImpresion { get; set; }
        public DateTime? FechaRecepcionEstimadaImpresion { get; set; }
        public DateTime? FechaRecepcionImpresion { get; set; }
        public DateTime? FechaRecepcionCoordinador { get; set; }
        public bool? SubidoAmazon { get; set; }
        public bool? PublicoAmazon { get; set; }
        public DateTime? FechaSubidaAmazon { get; set; }
        public string CarpetaAmazon { get; set; }
        public int IdRaCursoMaterialEstado { get; set; }
        public int? Grupo { get; set; }
        public string NombreArchivoEnviadoProveedor { get; set; }
        public string ContentTypeArchivoEditadoProveedor { get; set; }
        public string NombreArchivoEditadoProveedor { get; set; }
        public string ContentTypeArchivoEnviadoProveedor { get; set; }
        public string ComentarioSubidaArchivo { get; set; }
        public DateTime? FechaEnvioAprobacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string ComentariosAprobacion { get; set; }
        public bool? EnviarCorreoProveedor { get; set; }
        public bool? SubidoAmazonAlumno { get; set; }
        public DateTime? FechaSubidaAmazonAlumno { get; set; }
        public bool? EnviarCorreoAlumno { get; set; }
        public string UsuarioSubidaArchivoEditado { get; set; }
        public DateTime? FechaSubidaArchivoEditado { get; set; }
        public string UsuarioSubidaArchivoAlumno { get; set; }
        public DateTime? FechaSubidaArchivoAlumno { get; set; }
        public string UsuarioSubidaArchivoEditadoFinal { get; set; }
        public DateTime? FechaSubidaArchivoEditadoFinal { get; set; }
        public string UsuarioSubidaArchivoProveedor { get; set; }
        public DateTime? FechaSubidaArchivoProveedor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
