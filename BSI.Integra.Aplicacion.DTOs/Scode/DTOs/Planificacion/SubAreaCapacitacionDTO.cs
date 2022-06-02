using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SubAreaCapacitacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdSubArea { get; set; }
        public string DescripcionHtml { get; set; }
    }
}
