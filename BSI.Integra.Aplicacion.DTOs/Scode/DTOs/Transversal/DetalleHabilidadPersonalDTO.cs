using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetalleHabilidadPersonalDTO
    {
        public int Id { get; set; }
        public string NombrePersonal { get; set; }
        public string NombreHabilidad { get; set; }
        public int IdHabilidad { get; set; }
        public int Puntaje { get; set; }
    }
}
