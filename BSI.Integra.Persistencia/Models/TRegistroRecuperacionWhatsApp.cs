using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRegistroRecuperacionWhatsApp
    {
        public int Id { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdCampaniaGeneralDetalleResponsable { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int Dia { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public DateTime FechaInicioEnvioWhatsapp { get; set; }
        public DateTime FechaFinEnvioWhatsapp { get; set; }
        public TimeSpan HoraEnvio { get; set; }
        public bool Completado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? Fallido { get; set; }
        public bool? RecuperacionEnProceso { get; set; }
    }
}
