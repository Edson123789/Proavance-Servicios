using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionWebinar
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdTiempoFrecuenciaCorreoConfirmacion { get; set; }
        public int? ValorFrecuenciaCorreoConfirmacion { get; set; }
        public int? IdTiempoFrecuenciaCreacion { get; set; }
        public int? ValorCreacion { get; set; }
        public int? IdTiempoFrecuenciaCorreo { get; set; }
        public int? ValorFrecuenciaCorreo { get; set; }
        public int? IdTiempoFrecuenciaWhatsApp { get; set; }
        public int? ValorFrecuenciaWhasApp { get; set; }
        public int? IdPlantillaCorreoConfirmacion { get; set; }
        public int? IdPlantillaCorreo { get; set; }
        public int? IdPlantillaWhasApp { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
