using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosProgramaEspecificoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public int? IdCiudad { get; set; }
        public string Tipo { get; set; }
        public string TipoAmbiente { get; set; }
        public int? IdSesion_Inicio { get; set; }
    }
}
