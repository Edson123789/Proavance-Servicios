using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetalleOportunidadOperacionesDTO
    {
        public int IdOportunidad { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public string NombreCiudad { get; set; }
        public int EscalaCalificacion { get; set; }
    }
}
