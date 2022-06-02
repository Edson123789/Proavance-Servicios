using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaCabeceraDatosCertificadoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Duracion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string NombreCurso { get; set; }
        public bool EstadoCambioDatos { get; set; }
        public string Usuario { get; set; }
        public string Mensaje { get; set; }
    }
}
