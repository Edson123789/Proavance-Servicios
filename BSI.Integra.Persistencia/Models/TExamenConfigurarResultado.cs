using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TExamenConfigurarResultado
    {
        public int Id { get; set; }
        public bool ExamenCalificado { get; set; }
        public decimal? PuntajeExamen { get; set; }
        public decimal? PuntajeAprobacion { get; set; }
        public bool MostrarResultado { get; set; }
        public bool MostrarPuntajeTotal { get; set; }
        public bool MostrarPuntajePregunta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
