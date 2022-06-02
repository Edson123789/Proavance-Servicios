using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDatoContratoPersonal
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdTipoContrato { get; set; }
        public bool EstadoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal RemuneracionFija { get; set; }
        public int? IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinancieraPago { get; set; }
        public string NumeroCuentaPago { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdCargo { get; set; }
        public int? IdTipoPerfil { get; set; }
        public int? IdPersonalJefe { get; set; }
        public int? IdEntidadFinancieraCts { get; set; }
        public string NumeroCuentaCts { get; set; }
        public bool? EsPeridoPrueba { get; set; }
        public DateTime? FechaFinPeriodoPrueba { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdContratoEstado { get; set; }
        public string UrlDocumentoContrato { get; set; }

        public virtual TCargo IdCargoNavigation { get; set; }
        public virtual TContratoEstado IdContratoEstadoNavigation { get; set; }
        public virtual TPuestoTrabajo IdPuestoTrabajoNavigation { get; set; }
        public virtual TSedeTrabajo IdSedeTrabajoNavigation { get; set; }
        public virtual TTipoContrato IdTipoContratoNavigation { get; set; }
        public virtual TTipoPagoRemuneracion IdTipoPagoRemuneracionNavigation { get; set; }
        public virtual TTipoPerfil IdTipoPerfilNavigation { get; set; }
    }
}
