using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionEnvioAutomatico
    {
        public int Id { get; set; }
        public int? IdEstadoInicial { get; set; }
        public int? IdSubEstadoInicial { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdSubEstadoDestino { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
