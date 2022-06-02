using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRaCentroCosto
    {
        public int Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int? IdPespecifico { get; set; }
        public string NombrePespecifico { get; set; }
        public bool? VisibleCoordinador { get; set; }
        public string Coordinador { get; set; }
        public string ResponsableCoordinacion { get; set; }
        public int? IdRaCentroCostoEstado { get; set; }
        public int NroCursosCertificado { get; set; }
        public int? IdRaCertificadoBrochure { get; set; }
        public int? IdRaCertificadoPartnerComplemento { get; set; }
        public DateTime? FechaConfirmacionApertura { get; set; }
        public string UsuarioConfirmacionApertura { get; set; }
        public bool? AplicaAulaVirtual { get; set; }
        public DateTime? FechaCreacionAulaVirtual { get; set; }
        public bool? AplicaSilaboAulaVirtual { get; set; }
        public DateTime? FechaSubidaSilaboAulaVirtual { get; set; }
        public bool? AplicaMaterialAulaVirtual { get; set; }
        public DateTime? FechaSubidaMaterialAulaVirtual { get; set; }
        public bool? AplicaAutoevaluacionesAulaVirtual { get; set; }
        public DateTime? FechaSubidaAutoevaluacionesAulaVirtual { get; set; }
        public int? IdRaFrecuencia { get; set; }
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
