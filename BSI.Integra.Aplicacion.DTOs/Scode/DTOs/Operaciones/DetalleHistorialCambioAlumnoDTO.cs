
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetalleHistorialCambioAlumnoDTO
    {
        public int Id { get; set; }
        public string CodigoAlumno { get; set; }
        public string NombreHistorialCambioAlumnoTipo { get; set; }
        public string CentroCostoOrigen { get; set; }
        public string CentroCostoDestino { get; set; }
        public bool Cancelado { get; set; }
        public bool? Aprobado { get; set; }
        public string UsuarioSolicitud { get; set; }
        public string ComentarioSolicitud { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
