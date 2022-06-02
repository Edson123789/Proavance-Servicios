using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneralConfiguracionPlantilla
    {
        public TPgeneralConfiguracionPlantilla()
        {
            TPgeneralConfiguracionPlantillaDetalle = new HashSet<TPgeneralConfiguracionPlantillaDetalle>();
        }

        public int Id { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int? IdPlantillaPosterior { get; set; }
        public int IdPgeneral { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public DateTime? UltimaFechaRemplazarCertificado { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual ICollection<TPgeneralConfiguracionPlantillaDetalle> TPgeneralConfiguracionPlantillaDetalle { get; set; }
    }
}
