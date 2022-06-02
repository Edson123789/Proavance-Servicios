using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentroCostoProbableDTO
    {
        public int IdPEspecifico { get; set; }
        public string Tipo { get; set; }
        public int IdPersonal { get; set; }
        public decimal Precio { get; set; }
    }   
}
