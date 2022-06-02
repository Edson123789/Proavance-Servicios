using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionCorreo
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public string MailchimpIdCampana { get; set; }
        public string MailchimpIdMember { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSubAreacapacitacion { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? EsLeido { get; set; }
        public int? ValorMedible { get; set; }
        public DateTime? Fecha { get; set; }
        public string EstadoSuscripcion { get; set; }
        public string RazonUnSuscripcion { get; set; }
        public string UrlClick { get; set; }
        public string Tipo { get; set; }
        public string Accion { get; set; }
        public string Url { get; set; }
        public string TitutloUrl { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
