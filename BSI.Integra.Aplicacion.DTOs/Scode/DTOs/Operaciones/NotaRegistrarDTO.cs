using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class NotaRegistrarDTO
    {
        public int Id { get; set; }
        [Required]
        public int IdEvaluacion { get; set; }
        [Required]
        public int IdMatriculaCabecera { get; set; }

        [Range(0, 100, ErrorMessage = "El rango permitido para la nota es desde 0 a 100")]
        public decimal? Nota { get; set; }

        //public string Usuario { get; set; }
    }
}
