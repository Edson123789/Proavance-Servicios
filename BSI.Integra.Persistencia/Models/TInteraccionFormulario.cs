using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionFormulario
    {
        public int Id { get; set; }
        public int? IdTipoFormulario { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdAlumno { get; set; }
        public Guid? IdContactoPortalSegmentoPw { get; set; }
        public DateTime? Fecha { get; set; }
        public string Ip { get; set; }
        public string Url { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int? IdAreaCapcitacion { get; set; }
        public string Correo { get; set; }
        public string IpV4 { get; set; }
        public Guid? IdFormulario { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string UrlOrigen { get; set; }
        public bool? CerrarPopUp { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
