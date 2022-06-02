using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEnvioCorreoBicOperaciones
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string Remitente { get; set; }
        public string Destiantario { get; set; }
        public bool EnviadoCorrectamente { get; set; }
        public int IdMandrilEnvioCorreo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
