using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaEvaluacion_RegistrarDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int IdFormaCalculoEvaluacion { get; set; }

        public List<EsquemaEvaluacionDetalle_RegistrarDTO> ListadoDetalle { get; set; }

        public string NombreUsuario { get; set; }
    }
}
