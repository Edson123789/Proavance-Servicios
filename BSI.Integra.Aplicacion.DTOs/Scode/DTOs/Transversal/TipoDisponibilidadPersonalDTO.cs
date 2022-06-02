using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoDisponibilidadPersonalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool GeneraCosto { get; set; }
        public string Usuario { get; set; }
    }
}
