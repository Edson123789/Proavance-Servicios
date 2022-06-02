using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfigurarExamenesEncuestasEstructura
    {
        public int Id { get; set; }
        public int IdExamen { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSeccionPw { get; set; }
        public int? Fila { get; set; }
        public int? OrdenCapitulo { get; set; }
        public int? OrdenExamen { get; set; }
        public int? PosicionExamen { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TExamen IdExamenNavigation { get; set; }
        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual TSeccionPw IdSeccionPwNavigation { get; set; }
    }
}
