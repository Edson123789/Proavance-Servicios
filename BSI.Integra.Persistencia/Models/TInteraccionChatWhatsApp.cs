using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionChatWhatsApp
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int TotalMensajesChat { get; set; }
        public int TotalMensajesVisitante { get; set; }
        public decimal PromedioRespuestaUsuario { get; set; }
        public int? IdAlumno { get; set; }
        public int IdPersonal { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool TieneOportunidadSeguimiento { get; set; }
        public bool TieneOportunidadNueva { get; set; }
        public bool EsAtendido { get; set; }
        public int? IdPais { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
