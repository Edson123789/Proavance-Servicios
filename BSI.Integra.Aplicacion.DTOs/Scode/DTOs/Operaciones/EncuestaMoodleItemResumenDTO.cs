using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EncuestaMoodleItemResumenDTO
    {
        public int Id { get; set; }
        public int IdRespuesta { get; set; }
        public string CodigoAlumno { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }
        public string UsuarioCoordinador { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? ServicioCliente { get; set; }
        public decimal? CursoDocente { get; set; }
        public decimal? AulaVirtual { get; set; }
        public decimal? SesionesTutoria { get; set; }
        public string TipoEncuesta { get; set; }

    }
}
