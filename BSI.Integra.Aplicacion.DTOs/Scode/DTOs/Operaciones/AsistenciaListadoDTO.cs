using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class AsistenciaListadoDTO
    {
        public int IdMatriculaIntegra { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public List<AsistenciaDetalleDTO> DetalleAsistencia { get; set; }
    }
}
