using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
     public class ModeloGeneralIndustriaDTO
    {
        public int Id { get; set; }
        public int IdModeloGeneral { get; set; }
        public int IdIndustria { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public string Usuario { get; set; }
    }
}
