using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompetidorVentajaDesventajaDTO
    {
        public int Id { get; set; }
        public int IdCompetidor { get; set; }
        public int Tipo { get; set; }
        public string Contenido { get; set; }

    }
}
