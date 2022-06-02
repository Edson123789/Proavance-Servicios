using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRaConstanciaAlumno
    {
        public int Id { get; set; }
        public string CodigoAlumno { get; set; }
        public int? IdRaTipoConstanciaAlumno { get; set; }
        public int IdRaEstadoConstanciaAlumno { get; set; }
        public int Correlativo { get; set; }
        public int Anho { get; set; }
        public string CentroCosto { get; set; }
        public string Alumno { get; set; }
        public string Coordinador { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaAutorizacion { get; set; }
        public string UsuarioAutorizacion { get; set; }
        public string RutaArchivoImpresion { get; set; }
        public string RutaArchivoDigital { get; set; }
        public int? TamanhoFuente { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
