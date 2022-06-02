using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProbabilidadAlumnoPrograma
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdProgramaGeneral { get; set; }
        public decimal Probabilidad { get; set; }
        public int IdProbabilidadRegistroPw { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? NroSolicitudesInformacionPg { get; set; }
        public int? NroSolicitudesInformacionArea { get; set; }
        public int? NroSolicitudesInformacionSubArea { get; set; }
    }
}
