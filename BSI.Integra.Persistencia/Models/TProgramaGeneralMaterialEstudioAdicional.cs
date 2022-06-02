using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralMaterialEstudioAdicional
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string NombreArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public string EnlaceArchivo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string NombreConfiguracion { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; }
    }
}
