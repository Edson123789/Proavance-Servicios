using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosProgramaespecificoHijosDTO
    {
        public int PEspecificoHijoId { get; set; }
        public int PEspecificoPadreId { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public int? IdCiudad { get; set; }
        public string TipoAmbiente { get; set; }
    }
}
