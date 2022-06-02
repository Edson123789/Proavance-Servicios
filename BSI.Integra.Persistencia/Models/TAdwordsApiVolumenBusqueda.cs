using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAdwordsApiVolumenBusqueda
    {
        public int Id { get; set; }
        public int IdAdwordsApiPalabraClave { get; set; }
        public int PromedioBusqueda { get; set; }
        public int Mes { get; set; }
        public int Anho { get; set; }
        public int IdPais { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
