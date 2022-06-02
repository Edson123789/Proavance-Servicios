using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoPwDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaPw { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
        public string Usuario { get; set; }
    }
}
