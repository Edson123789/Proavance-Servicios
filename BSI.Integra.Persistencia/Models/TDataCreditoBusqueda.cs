using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoBusqueda
    {
        public TDataCreditoBusqueda()
        {
            TDataCreditoConsulta = new HashSet<TDataCreditoConsulta>();
            TDataCreditoDataCuentaAhorro = new HashSet<TDataCreditoDataCuentaAhorro>();
            TDataCreditoDataCuentaCartera = new HashSet<TDataCreditoDataCuentaCartera>();
            TDataCreditoDataEndeudamientoGlobal = new HashSet<TDataCreditoDataEndeudamientoGlobal>();
            TDataCreditoDataInfAgrComposicionPortafolio = new HashSet<TDataCreditoDataInfAgrComposicionPortafolio>();
            TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio = new HashSet<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>();
            TDataCreditoDataInfAgrEvolucionDeudaTrimestre = new HashSet<TDataCreditoDataInfAgrEvolucionDeudaTrimestre>();
            TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta = new HashSet<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta>();
            TDataCreditoDataInfAgrHistoricoSaldoTotal = new HashSet<TDataCreditoDataInfAgrHistoricoSaldoTotal>();
            TDataCreditoDataInfAgrResumenComportamiento = new HashSet<TDataCreditoDataInfAgrResumenComportamiento>();
            TDataCreditoDataInfAgrResumenEndeudamiento = new HashSet<TDataCreditoDataInfAgrResumenEndeudamiento>();
            TDataCreditoDataInfAgrResumenPrincipal = new HashSet<TDataCreditoDataInfAgrResumenPrincipal>();
            TDataCreditoDataInfAgrResumenSaldo = new HashSet<TDataCreditoDataInfAgrResumenSaldo>();
            TDataCreditoDataInfAgrResumenSaldoMes = new HashSet<TDataCreditoDataInfAgrResumenSaldoMes>();
            TDataCreditoDataInfAgrResumenSaldoSector = new HashSet<TDataCreditoDataInfAgrResumenSaldoSector>();
            TDataCreditoDataInfAgrTotal = new HashSet<TDataCreditoDataInfAgrTotal>();
            TDataCreditoDataInfMicroAnalisisVector = new HashSet<TDataCreditoDataInfMicroAnalisisVector>();
            TDataCreditoDataInfMicroEndeudamientoActual = new HashSet<TDataCreditoDataInfMicroEndeudamientoActual>();
            TDataCreditoDataInfMicroEvolucionDeuda = new HashSet<TDataCreditoDataInfMicroEvolucionDeuda>();
            TDataCreditoDataInfMicroImagenTendenciaEndeudamiento = new HashSet<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento>();
            TDataCreditoDataInfMicroPerfilGeneral = new HashSet<TDataCreditoDataInfMicroPerfilGeneral>();
            TDataCreditoDataInfMicroVectorSaldoMora = new HashSet<TDataCreditoDataInfMicroVectorSaldoMora>();
            TDataCreditoDataNaturalNacional = new HashSet<TDataCreditoDataNaturalNacional>();
            TDataCreditoDataProductoValor = new HashSet<TDataCreditoDataProductoValor>();
            TDataCreditoDataScore = new HashSet<TDataCreditoDataScore>();
            TDataCreditoDataTarjetaCredito = new HashSet<TDataCreditoDataTarjetaCredito>();
        }

        public int Id { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string CodigoSeguridad { get; set; }
        public int TipoIdentificacion { get; set; }
        public string NroDocumento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TDataCreditoConsulta> TDataCreditoConsulta { get; set; }
        public virtual ICollection<TDataCreditoDataCuentaAhorro> TDataCreditoDataCuentaAhorro { get; set; }
        public virtual ICollection<TDataCreditoDataCuentaCartera> TDataCreditoDataCuentaCartera { get; set; }
        public virtual ICollection<TDataCreditoDataEndeudamientoGlobal> TDataCreditoDataEndeudamientoGlobal { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrComposicionPortafolio> TDataCreditoDataInfAgrComposicionPortafolio { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrEvolucionDeudaTrimestre> TDataCreditoDataInfAgrEvolucionDeudaTrimestre { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta> TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrHistoricoSaldoTotal> TDataCreditoDataInfAgrHistoricoSaldoTotal { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenComportamiento> TDataCreditoDataInfAgrResumenComportamiento { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenEndeudamiento> TDataCreditoDataInfAgrResumenEndeudamiento { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenPrincipal> TDataCreditoDataInfAgrResumenPrincipal { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenSaldo> TDataCreditoDataInfAgrResumenSaldo { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenSaldoMes> TDataCreditoDataInfAgrResumenSaldoMes { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrResumenSaldoSector> TDataCreditoDataInfAgrResumenSaldoSector { get; set; }
        public virtual ICollection<TDataCreditoDataInfAgrTotal> TDataCreditoDataInfAgrTotal { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroAnalisisVector> TDataCreditoDataInfMicroAnalisisVector { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroEndeudamientoActual> TDataCreditoDataInfMicroEndeudamientoActual { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroEvolucionDeuda> TDataCreditoDataInfMicroEvolucionDeuda { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento> TDataCreditoDataInfMicroImagenTendenciaEndeudamiento { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroPerfilGeneral> TDataCreditoDataInfMicroPerfilGeneral { get; set; }
        public virtual ICollection<TDataCreditoDataInfMicroVectorSaldoMora> TDataCreditoDataInfMicroVectorSaldoMora { get; set; }
        public virtual ICollection<TDataCreditoDataNaturalNacional> TDataCreditoDataNaturalNacional { get; set; }
        public virtual ICollection<TDataCreditoDataProductoValor> TDataCreditoDataProductoValor { get; set; }
        public virtual ICollection<TDataCreditoDataScore> TDataCreditoDataScore { get; set; }
        public virtual ICollection<TDataCreditoDataTarjetaCredito> TDataCreditoDataTarjetaCredito { get; set; }
    }
}
