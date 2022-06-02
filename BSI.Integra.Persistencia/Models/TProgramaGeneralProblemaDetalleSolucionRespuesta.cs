using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralProblemaDetalleSolucionRespuesta
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralProblemaDetalleSolucion { get; set; }
        public bool EsSeleccionado { get; set; }
        public bool EsSolucionado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
