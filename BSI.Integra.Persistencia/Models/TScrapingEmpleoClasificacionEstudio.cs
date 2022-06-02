using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TScrapingEmpleoClasificacionEstudio
    {
        public int Id { get; set; }
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdTipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
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
