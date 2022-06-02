using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProbabilidadByContactoPrograma
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public int IdPrograma { get; set; }
        public double ProbabilidadActual { get; set; }
        public string ProbabilidadActualDesc { get; set; }
        public string NombrePrograma { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
