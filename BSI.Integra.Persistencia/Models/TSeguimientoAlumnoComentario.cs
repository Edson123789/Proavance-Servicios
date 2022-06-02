using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSeguimientoAlumnoComentario
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int IdSeguimientoAlumnoCategoria { get; set; }
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
