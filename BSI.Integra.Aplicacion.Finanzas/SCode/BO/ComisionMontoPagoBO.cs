using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class ComisionMontoPagoBO : BaseBO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonal { get; set; }
        public decimal MontoComision { get; set; }
        public int IdMoneda { get; set; }
        public int IdComercialTipoPersonal { get; set; }
        public int IdComisionEstadoPago { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

    }

}
