using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ConfiguracionWebinarBO : BaseBO
    {
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
        public int? IdMigracion { get; set; }
    }
}
