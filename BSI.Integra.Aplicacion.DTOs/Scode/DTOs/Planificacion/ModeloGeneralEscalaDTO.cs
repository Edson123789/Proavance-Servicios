using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloGeneralEscalaDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal ValorMinimo { get; set; }
        public int IdModeloGeneral { get; set; }
        public string Usuario { get; set; }
    }
}
