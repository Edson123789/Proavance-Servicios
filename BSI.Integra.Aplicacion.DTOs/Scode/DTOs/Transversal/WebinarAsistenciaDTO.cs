using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WebinarAsistenciaDTO
    {
        public int Id { get; set; }
        public int IdWebinarDetalle { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public bool? ConfirmoAsistencia { get; set; }
        public bool Asistio { get; set; }
        public string EstadoConfirmacion { get; set; }
    }
}
