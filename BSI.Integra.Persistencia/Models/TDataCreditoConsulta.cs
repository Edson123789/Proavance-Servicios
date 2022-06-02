using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoConsulta
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public string TipoCuenta { get; set; }
        public string Entidad { get; set; }
        public string Oficina { get; set; }
        public string Ciudad { get; set; }
        public string Razon { get; set; }
        public string Cantidad { get; set; }
        public string NitSuscriptor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TDataCreditoBusqueda IdDataCreditoBusquedaNavigation { get; set; }
    }
}
