using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetalleOportunidadCompetidorDTO
    {
        public int Id { get; set; }
        public int IdOportunidadCompetidor { get; set; }
        public int IdCompetidor { get; set; }
        //public string RowVersion { get; set; }
    }
}
