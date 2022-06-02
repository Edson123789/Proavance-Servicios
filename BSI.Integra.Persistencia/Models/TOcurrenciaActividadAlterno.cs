using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOcurrenciaActividadAlterno
    {
        public int Id { get; set; }
        public int IdOcurrencia { get; set; }
        public int IdActividadCabecera { get; set; }
        public bool? PreProgramada { get; set; }
        public int? IdOcurrenciaActividadPadre { get; set; }
        public bool NodoPadre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabeceraProgramada { get; set; }
        public string Roles { get; set; }
    }
}
