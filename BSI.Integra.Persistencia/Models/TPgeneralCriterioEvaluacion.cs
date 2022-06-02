using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneralCriterioEvaluacion
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
        public int? IdTipoPromedio { get; set; }
    }
}
