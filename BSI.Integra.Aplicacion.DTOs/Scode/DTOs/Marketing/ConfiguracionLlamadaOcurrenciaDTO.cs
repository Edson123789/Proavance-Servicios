using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfiguracionLlamadaOcurrenciaDTO
    {
        public int Id { get; set; }
        public int IdOcurrencia { get; set; }
        public int? IdConectorOcurrenciaLlamada { get; set; }
        public int NumeroLlamada { get; set; }
        public int IdCondicionOcurrenciaLlamada { get; set; }
        public int? IdFaseTiempoLlamada { get; set; }
        public int Duracion { get; set; }

       public string Usuario { get; set; }
    }
}
