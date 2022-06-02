using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloGeneralCategoriaDatoDTO
    {
        public int Id { get; set; }
        public int IdAsociado { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public int IdModeloGeneral { get; set; }
        public int IdCategoriaDato { get; set; }
        public string Usuario { get; set; }
    }
}
