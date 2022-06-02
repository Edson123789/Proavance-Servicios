using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloPredictivoCargoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCargo { get; set; }
        public decimal Valor { get; set; }
        public bool Validar { get; set; }
    }
}
