using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoSilabo
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public string ObjetivoAprendizaje { get; set; }
        public string PautaComplementaria { get; set; }
        public string PublicoObjetivo { get; set; }
        public string Material { get; set; }
        public string Bibliografia { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? Aprobado { get; set; }
    }
}
