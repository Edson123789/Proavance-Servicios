using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonalExperiencia
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaRetiro { get; set; }
        public string MotivoRetiro { get; set; }
        public string NombreJefeInmediato { get; set; }
        public string TelefonoJefeInmediato { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
