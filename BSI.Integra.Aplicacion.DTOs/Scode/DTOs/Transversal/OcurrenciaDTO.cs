using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OcurrenciaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreM { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantilla_Speech { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public bool? Oportunidad { get; set; }
        public string RequiereLlamada { get; set; }
        public string Roles { get; set; }
        public string Color { get; set; }
        public int? NombreCs { get; set; }
        public string Usuario { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
		public string PersonalAreaTrabajo { get; set; }
        public int? IdTipoOcurrencia { get; set; }

    }
}
