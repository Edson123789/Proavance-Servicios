using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCentroEstudio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPais { get; set; }
        public int IdCiudad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdTipoCentroEstudio { get; set; }
    }
}
