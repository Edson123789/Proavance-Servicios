using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TBeneficiosAlumnoPespecifico
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPespecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Beneficios { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
