using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionInvitacion
    {
        public int Id { get; set; }
        public int IdConfiguracionCreacion { get; set; }
        public string Nombre { get; set; }
        public int IdPlantilla { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public int Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TConfiguracionCreacion IdConfiguracionCreacionNavigation { get; set; }
        public virtual TPlantillaPw IdPlantillaNavigation { get; set; }
    }
}
