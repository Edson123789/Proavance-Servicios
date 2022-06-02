using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloPredictivoIndustriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdIndustria { get; set; }
        public double Valor { get; set; }
        public bool Validar { get; set; }
    }
}
