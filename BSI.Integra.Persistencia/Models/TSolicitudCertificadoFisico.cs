using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSolicitudCertificadoFisico
    {
        public TSolicitudCertificadoFisico()
        {
            TRegistroCertificadoFisicoGenerado = new HashSet<TRegistroCertificadoFisicoGenerado>();
        }

        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonal { get; set; }
        public int? IdFur { get; set; }
        public int? IdProveedor { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaEntregaEstimada { get; set; }
        public DateTime? FechaEntregaReal { get; set; }
        public string CodigoSeguimientoEnvio { get; set; }
        public int IdEstadoCertificadoFisico { get; set; }
        public int IdCertificadoGeneradoAutomatico { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCourier { get; set; }
        public DateTime? FechaEntregaCourier { get; set; }
        public string EstadoCourier { get; set; }
        public int? IdPaisConsignado { get; set; }
        public int? IdCiudadConsignada { get; set; }
        public string CodigoSeguimiento { get; set; }

        public virtual TEstadoCertificadoFisico IdEstadoCertificadoFisicoNavigation { get; set; }
        public virtual TFur IdFurNavigation { get; set; }
        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TProveedor IdProveedorNavigation { get; set; }
        public virtual ICollection<TRegistroCertificadoFisicoGenerado> TRegistroCertificadoFisicoGenerado { get; set; }
    }
}
