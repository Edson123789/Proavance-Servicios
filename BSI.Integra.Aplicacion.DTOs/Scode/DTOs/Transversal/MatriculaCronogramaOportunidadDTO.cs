using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaCronogramaOportunidadDTO
    {
        public int IdMontoPagoCronogramas { get; set; }
        public int IdMontoPago { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdAlumno { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public string CodigoBanco { get; set; }
        public string UsuarioAprobacion { get; set; }
    }
}
