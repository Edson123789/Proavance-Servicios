using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOcurrenciaActividad
    {
        public TOcurrenciaActividad()
        {
            TWhatsAppPlantillaPorOcurrenciaActividad = new HashSet<TWhatsAppPlantillaPorOcurrenciaActividad>();
        }

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

        public virtual ICollection<TWhatsAppPlantillaPorOcurrenciaActividad> TWhatsAppPlantillaPorOcurrenciaActividad { get; set; }
    }
}
