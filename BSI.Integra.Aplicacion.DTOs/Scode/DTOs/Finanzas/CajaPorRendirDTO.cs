using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CajaPorRendirDTO
    {
        public int Id { get; set; }
        public int? IdCaja { get; set; }
        public string CodigoCaja { get; set; }
        public int? IdFur { get; set; }
        public string CodigoFur { get; set; }
        public int IdPersonalSolicitante{ get; set; }
        public string NombrePersonalSolicitante { get; set; }
        public int IdPersonalResponsable { get; set; }
        public string NombrePersonalResponsable { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public DateTime FechaEntregaEfectivo { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
