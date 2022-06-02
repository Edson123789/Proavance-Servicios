using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfAgrResumenPrincipal
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string CreditosVigentes { get; set; }
        public string CreditosCerrados { get; set; }
        public string CreditosActualesNegativos { get; set; }
        public string HistNegUlt12Meses { get; set; }
        public string CuentasAbiertasAhoccb { get; set; }
        public string CuentasCerradasAhoccb { get; set; }
        public string ConsultadasUlt6meses { get; set; }
        public string DesacuerdosAlaFecha { get; set; }
        public string ReclamosVigentes { get; set; }
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
