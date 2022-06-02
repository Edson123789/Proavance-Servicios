using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReporteDevolucionCongelado
    {
        public int Id { get; set; }
        public string PeriodoFechaVencimiento { get; set; }
        public string TipoRetiro { get; set; }
        public int IdPespecifico { get; set; }
        public string NombrePrograma { get; set; }
        public int IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public DateTime FechaRetiro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoDevolucion { get; set; }
        public string CodigoMatricula { get; set; }
        public string Observaciones { get; set; }
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int? IdPeriodo { get; set; }
        public DateTime FechaCongelamiento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
