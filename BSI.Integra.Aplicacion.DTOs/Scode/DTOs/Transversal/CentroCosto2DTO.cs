using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentroCosto2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string IdPgeneral { get; set; }
        public string IdAreaCc { get; set; }
        public string Codigo { get; set; }
    }
}
