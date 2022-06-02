using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerEnvioMasivoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double? PresupuestoDiario { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public int IdFacebookPagina { get; set; }
        public string NombreFacebookPagina { get; set; }
        public string FacebookPaginaId { get; set; }
    }
}
