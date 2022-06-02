using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCaja
    {
        public int Id { get; set; }
        public string CodigoCaja { get; set; }
        public int IdMoneda { get; set; }
        public int IdEmpresaAutorizada { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int IdCiudad { get; set; }
        public int IdPersonalResponsable { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
