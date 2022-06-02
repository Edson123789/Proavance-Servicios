using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFacebookConjuntoAnuncioEstadisticaDiaria
    {
        public int Id { get; set; }
        public int IdConjuntoAnuncioFacebook { get; set; }
        public DateTime Fecha { get; set; }
        public int Alcance { get; set; }
        public int Impresiones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
