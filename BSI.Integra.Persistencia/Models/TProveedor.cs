using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProveedor
    {
        public TProveedor()
        {
            TConvocatoriaPersonal = new HashSet<TConvocatoriaPersonal>();
            TEsquemaEvaluacionPgeneralDetalle = new HashSet<TEsquemaEvaluacionPgeneralDetalle>();
            TEsquemaEvaluacionPgeneralProveedor = new HashSet<TEsquemaEvaluacionPgeneralProveedor>();
            TProveedorTipoServicio = new HashSet<TProveedorTipoServicio>();
            TSolicitudCertificadoFisico = new HashSet<TSolicitudCertificadoFisico>();
        }

        public int Id { get; set; }
        public int IdTipoContribuyente { get; set; }
        public int IdDocumentoIdentidad { get; set; }
        public string NroDocIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public int? IdCiudad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Contacto1 { get; set; }
        public string Contacto2 { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdPrestacionRegistro { get; set; }
        public bool? EsPersonaValida { get; set; }
        public int? IdTipoImpuestoPreferido { get; set; }
        public int? IdRetencionPreferido { get; set; }
        public int? IdDetraccionPreferido { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string Alias { get; set; }

        public virtual ICollection<TConvocatoriaPersonal> TConvocatoriaPersonal { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralDetalle> TEsquemaEvaluacionPgeneralDetalle { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralProveedor> TEsquemaEvaluacionPgeneralProveedor { get; set; }
        public virtual ICollection<TProveedorTipoServicio> TProveedorTipoServicio { get; set; }
        public virtual ICollection<TSolicitudCertificadoFisico> TSolicitudCertificadoFisico { get; set; }
    }
}
