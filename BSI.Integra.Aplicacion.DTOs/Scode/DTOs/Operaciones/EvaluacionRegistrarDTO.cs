using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EvaluacionRegistrarDTO
    {
        public int? Id { get; set; }
        [Required]
        public int IdPEspecifico { get; set; }
        [Required]
        public int Grupo { get; set; }
        public string Nombre { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
        [Required]
        public int Porcentaje { get; set; }
        public string Usuario { get; set; }
    }
}
