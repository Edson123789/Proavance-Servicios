using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEvaluacionPersona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public decimal? TiempoLimite { get; set; }
        public bool? CronometrarExamen { get; set; }
        public int IdEvaluacionConfiguracionFormato { get; set; }
        public int IdEvaluacionComportamiento { get; set; }
        public int IdEvaluacionResultado { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPespecificoSesion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
