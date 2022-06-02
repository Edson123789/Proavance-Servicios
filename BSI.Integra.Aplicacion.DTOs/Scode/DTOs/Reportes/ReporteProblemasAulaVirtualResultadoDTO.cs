using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteProblemasAulaVirtualResultadoDTO
    {
        public int Id { get; set; }
        public string NombreAlumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreCentroCosto { get; set; }
        public string Coordinador { get; set; }
        public string Capitulo { get; set; }
        public string Sesion { get; set; }
        public int IdTipoCategoriaError { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public string FechaCreacion { get; set; }

    }
}
