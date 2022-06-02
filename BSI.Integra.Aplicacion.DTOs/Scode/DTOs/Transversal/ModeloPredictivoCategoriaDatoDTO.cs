using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloPredictivoCategoriaDatoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public double Valor { get; set; }
        public bool Validar { get; set; }
        public int IdSubCategoriaDato { get; set; }
    }
}
