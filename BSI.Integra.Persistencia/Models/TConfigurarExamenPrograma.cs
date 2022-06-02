using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfigurarExamenPrograma
    {
        public TConfigurarExamenPrograma()
        {
            TConfigurarExamenProgramaPregunta = new HashSet<TConfigurarExamenProgramaPregunta>();
        }

        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdSeccionPw { get; set; }
        public int NumeroFila { get; set; }
        public int Tipo { get; set; }
        public string Nombre { get; set; }
        public int? IdPreguntaCategoria { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual TPreguntaCategoria IdPreguntaCategoriaNavigation { get; set; }
        public virtual TSeccionPw IdSeccionPwNavigation { get; set; }
        public virtual ICollection<TConfigurarExamenProgramaPregunta> TConfigurarExamenProgramaPregunta { get; set; }
    }
}
