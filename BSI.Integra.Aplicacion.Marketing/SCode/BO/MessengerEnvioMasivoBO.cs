using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class MessengerEnvioMasivoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int PresupuestoDiario { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int IdActividadCabecera { get; set; }
        public int IdPlantilla { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdFacebookPagina { get; set; }
        public int IdFacebookCuentaPublicitaria { get; set; }
        public int? IdFacebookAudiencia { get; set; }
        public int? IdConjuntoAnuncioFacebook { get; set; }
        public int? IdFacebookAnuncioCreativo { get; set; }
        public int? IdFacebookAnuncio { get; set; }
        public int? IdMigracion { get; set; }
    }
}
