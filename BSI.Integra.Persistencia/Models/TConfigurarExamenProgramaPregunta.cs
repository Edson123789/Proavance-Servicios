using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfigurarExamenProgramaPregunta
    {
        public int Id { get; set; }
        public int IdConfigurarExamenPrograma { get; set; }
        public int IdPregunta { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TConfigurarExamenPrograma IdConfigurarExamenProgramaNavigation { get; set; }
        public virtual TPregunta IdPreguntaNavigation { get; set; }
    }
}
