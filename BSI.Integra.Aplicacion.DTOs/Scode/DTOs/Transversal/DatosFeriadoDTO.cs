using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosFeriadoDTO
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public DateTime Dia { get; set; }
        public string Motivo { get; set; }
        public int Frecuencia { get; set; }
        public int IdCiudad { get; set; }
        public bool? EstadoCiudad { get; set; }
        public string Usuario { get; set; }
    }
}
