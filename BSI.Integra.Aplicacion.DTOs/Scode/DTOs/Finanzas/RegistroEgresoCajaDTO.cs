using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistroEgresoCajaDTO
    {
        public int Id { get; set; }
        public int? IdCajaPorRendirCabecera { get; set; }
        public int IdCaja { get; set; }
        public int? IdComprobantePago { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string RucProveedor { get; set; }
        public int? IdSunatDocumento { get; set; }
        public string NombreSunatDocumento { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public int? IdFur { get; set; }
        public int? IdFurAnterior { get; set; }
        public string CodigoFur { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int? IdCajaEgresoAprobado { get; set; }
        public bool EsEnviado { get; set; }
        public int? IdPersonalResponsable { get; set; }
        public int? IdPersonalSolicitante { get; set; }
        public string PersonalSolicitante { get; set; }
        public decimal MontoFur { get; set; }
        public decimal MontoPendiente { get; set; }
        public bool EsCancelado { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
