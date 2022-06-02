using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMailchimpCorreo
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public string MailChimpId { get; set; }
        public int? Leid { get; set; }
        public string Correo { get; set; }
        public string EstadoSuscripcion { get; set; }
        public int Rating { get; set; }
        public string Ip { get; set; }
        public DateTime LastChanged { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string ZonaHoraria { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public string ListaId { get; set; }
        public string TimestampOpt { get; set; }
        public int Prioridad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
