using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetalleAccesoAulaVirtualDTO
    {
        public string UsuarioMoodle { get; set; }
        public string ClaveMoodle { get; set; }
    }

    public class DetalleCursoActualAulaVirtualDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string NombreCurso { get; set; }
    }

    public class DetalleAccesoPortalWebDTO
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }
}
