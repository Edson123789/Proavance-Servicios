using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ObtenerSeguimientoAlunoComentarioDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int IdSeguimientoAlumnoCategoria { get; set; }
        public string SeguimientoAlumnoCategoria { get; set; }
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
