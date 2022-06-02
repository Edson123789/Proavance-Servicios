using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaSesionesDTO
    {
        public ListainformacionProgramaEspecificoHijoDTO Curso { get; set; }
        public decimal? Duracion { get; set; }
        public byte? Dia { get; set; }
        public DateTime? FechaAsignar { get; set; }
        public int? SesionId { get; set; }
    }
}
