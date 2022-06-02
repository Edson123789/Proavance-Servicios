using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TScrapingEmpleoClasificacionExperiencia
    {
        public int Id { get; set; }
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? NroAnhos { get; set; }
        public bool? Obligatorio { get; set; }
        public bool EsAutomatico { get; set; }
        public bool EsValidado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
