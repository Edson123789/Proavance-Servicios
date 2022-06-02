using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteAnalyticsFiltroDetalleDTO
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public bool Excluir { get; set; }
        public int IdOperadorComparacion { get; set; }
    }
}
