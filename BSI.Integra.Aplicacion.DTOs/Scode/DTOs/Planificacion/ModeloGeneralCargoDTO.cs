using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloGeneralCargoDTO
    {
        public int Id { get; set; }
        public int IdModeloGeneral { get; set; }
        public int IdCargo { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public string Usuario { get; set; }
    }
}
