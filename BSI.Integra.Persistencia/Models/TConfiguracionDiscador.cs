using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionDiscador
    {
        public int Id { get; set; }
        public int IdEstadoOcurrencia { get; set; }
        public bool ContestaLlamada { get; set; }
        public int IdOperadorComparacionTimbradoSegundos { get; set; }
        public int TiempoTimbrado { get; set; }
        public int IdOperadorComparacionEfectivoSegundos { get; set; }
        public int TiempoEfectivo { get; set; }
        public int CantidadIntentosContacto { get; set; }
        public int TiempoEsperaLlamadaSegundos { get; set; }
        public bool DesvioLlamada { get; set; }
        public bool BuzonVoz { get; set; }
        public bool NoConectaLlamada { get; set; }
        public bool TelefonoApagado { get; set; }
        public bool NumeroNoExiste { get; set; }
        public bool NumeroSuspendido { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
