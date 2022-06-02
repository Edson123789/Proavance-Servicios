using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TLlamadaWebphoneAsterisk
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Anexo { get; set; }
        public string TelefonoDestino { get; set; }
        public int IdActividadDetalle { get; set; }
        public int IdLlamadaWebphoneTipo { get; set; }
        public int CdrId { get; set; }
        public int DuracionTimbrado { get; set; }
        public int DuracionContesto { get; set; }
        public string NombreGrabacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdProveedorNube { get; set; }
        public string Url { get; set; }
        public bool? EsEliminado { get; set; }
        public int? NroBytes { get; set; }
        public DateTime? FechaSubida { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
