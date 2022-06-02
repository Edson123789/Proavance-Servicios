using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModeloGeneralCategoriaDato
    {
        public int Id { get; set; }
        public int IdModeloGeneral { get; set; }
        public int IdAsociado { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public int IdCategoriaDato { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TCategoriaOrigen IdCategoriaDatoNavigation { get; set; }
        public virtual TModeloGeneral IdModeloGeneralNavigation { get; set; }
    }
}
