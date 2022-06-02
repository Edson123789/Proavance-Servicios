using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCentil
    {
        public int Id { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdExamen { get; set; }
        public int? IdSexo { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal? Centil { get; set; }
        public string CentilLetra { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
