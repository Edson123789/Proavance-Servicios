using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    class OcurrenciaReporteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public bool? Oportunidad { get; set; }
        public string RequiereLlamada { get; set; }
        public string Roles { get; set; }
        public string Color { get; set; }
        public string Usuario { get; set; }
    }
}
