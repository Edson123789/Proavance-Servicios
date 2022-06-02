using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaCabeceraNoRecordatorio
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool EnviarRecordatorioPago { get; set; }
        public bool EnviarRankingAcademico { get; set; }
        public DateTime FechaReincorporacion { get; set; }
        public bool EnviarSorteosPromociones { get; set; }
        public bool EnviarRecordatorioCumpleanos { get; set; }
        public bool EnviarRecordatorioSesionPresencial { get; set; }
        public bool EnviarCartaCobranza { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
