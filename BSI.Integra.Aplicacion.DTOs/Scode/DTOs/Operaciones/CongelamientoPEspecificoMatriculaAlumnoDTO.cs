using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CongelamientoPEspecificoMatriculaAlumnoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera{ get; set; }
        public int IdPEspecifico { get; set; }
        public bool EstadoPadre{ get; set; }
        public string Nombre { get; set; }
        public int IdProgramaGeneral { get; set; }
        public List<EsquemaEvaluacionCongelado_ListadoDTO> EsquemasEvaluacion { get; set; }

    }
}