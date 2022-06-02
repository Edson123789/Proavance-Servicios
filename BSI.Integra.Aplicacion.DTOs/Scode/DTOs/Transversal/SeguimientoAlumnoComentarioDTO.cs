using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SeguimientoAlumnoComentarioDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        //public int IdSeguimientoAlumnoCategoria { get; set; }
        public int? IdSeguimientoAlumnoCategoriaPago { get; set; }
        public int? IdSeguimientoAlumnoCategoriaAcademico { get; set; }
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        //public string Comentario { get; set; }
        public string ComentarioPago { get; set; }
        public string ComentarioAcademico { get; set; }
        //public DateTime FechaCompromiso { get; set; }
        public string Usuario { get; set; }
    }
}
