using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionLlamadaOcurrenciaAlterno
    {
        public int Id { get; set; }
        public int IdOcurrencia { get; set; }
        public int? IdConectorOcurrenciaLlamada { get; set; }
        public int NumeroLlamada { get; set; }
        public int IdCondicionOcurrenciaLlamada { get; set; }
        public int? IdFaseTiempoLlamada { get; set; }
        public int Duracion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
