using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAsignacionPreguntaExamen
    {
        public int Id { get; set; }
        public int IdExamen { get; set; }
        public int IdPregunta { get; set; }
        public int? NroOrden { get; set; }
        public int? Puntaje { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
