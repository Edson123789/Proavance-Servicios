using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class ProgramaGeneralPerfilCoeficienteDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public double Coeficiente { get; set; }
        public int IdSelect { get; set; }
        public int IdColumna { get; set; }
    }
}
