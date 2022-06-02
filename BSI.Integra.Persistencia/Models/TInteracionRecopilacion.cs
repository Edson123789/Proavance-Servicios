using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteracionRecopilacion
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public Guid? IdCookie { get; set; }
        public string Fecha { get; set; }
        public string FormaInteracion { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public string TipoContacto { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public string Contacto { get; set; }
        public string Fechacontacto { get; set; }
        public int? IdTipoFormulario { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
