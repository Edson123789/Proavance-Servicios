using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaSesionDTO
    {
        public TPespecifico Curso { get; set; }
        public decimal? Duracion { get; set; }
        public byte? Dia { get; set; }
        public DateTime? FechaAsignar { get; set; }
        public int? SesionId { get; set; }
    }
}
