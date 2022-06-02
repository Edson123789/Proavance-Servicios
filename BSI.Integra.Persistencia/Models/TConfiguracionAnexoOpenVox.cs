using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionAnexoOpenVox
    {
        public int Id { get; set; }
        public string Prefijo { get; set; }
        public int IdPais { get; set; }
        public string Anexo { get; set; }
        public string Servidor { get; set; }
        public string NumeroSim { get; set; }
        public string Tipo { get; set; }
        public string Puerto { get; set; }
        public bool? EnUso { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
