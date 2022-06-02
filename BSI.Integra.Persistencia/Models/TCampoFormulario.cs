using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampoFormulario
    {
        public int Id { get; set; }
        public int? IdFormularioSolicitud { get; set; }
        public int IdCampoContacto { get; set; }
        public int? NroVisitas { get; set; }
        public string Codigo { get; set; }
        public string Campo { get; set; }
        public bool? Siempre { get; set; }
        public bool? Inteligente { get; set; }
        public bool? Probabilidad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
