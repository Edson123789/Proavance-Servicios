using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OcurrenciaActividadDTO
    {
        public int Id { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdActividadCabecera { get; set; }
        public bool? PreProgramada { get; set; }
        public int? IdOcurrenciaActividadPadre { get; set; }
        public bool? NodoPadre { get; set; }
        public string Usuario { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabeceraProgramada { get; set; }
        public string Roles { get; set; }
    }
}
