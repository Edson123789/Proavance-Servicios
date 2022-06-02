using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMandrilEnvioCorreo
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int IdMandrilTipoAsignacion { get; set; }
        public int? EstadoEnvio { get; set; }
        public int IdMandrilTipoEnvio { get; set; }
        public string Asunto { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string FkMandril { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool EsEnvioMasivo { get; set; }
    }
}
