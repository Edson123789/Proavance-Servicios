using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEvaluacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public int Porcentaje { get; set; }
        public bool Aprobado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string UsuarioAprobacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
    }
}
