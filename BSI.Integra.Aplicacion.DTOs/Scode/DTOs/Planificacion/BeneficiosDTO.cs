using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BeneficioDTO
    {
        public int Paquete { get; set; }
        public string Titulo { get; set; }
        public int OrdenBeneficio { get; set; }

        public BeneficioDTO() {
            Paquete = 0;
            OrdenBeneficio = 0;
            Titulo = "";
        }
    }
}
