using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class CentroCostoDTO
    {
        public int Id { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string IdAreaCc { get; set; }
        public int? Ismtotales { get; set; }
        public int? Icpftotales { get; set; }
        public string Usuario { get; set; }
    }
}
