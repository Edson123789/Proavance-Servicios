using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModuloSistema
    {
        public TModuloSistema()
        {
            TModuloSistemaPuestoTrabajo = new HashSet<TModuloSistemaPuestoTrabajo>();
            TPlantillaAsociacionModuloSistema = new HashSet<TPlantillaAsociacionModuloSistema>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IdModuloSistemaGrupo { get; set; }
        public string Url { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdModuloSistemaSubGrupo { get; set; }
        public int? OrdenMenuPrincipal { get; set; }

        public virtual ICollection<TModuloSistemaPuestoTrabajo> TModuloSistemaPuestoTrabajo { get; set; }
        public virtual ICollection<TPlantillaAsociacionModuloSistema> TPlantillaAsociacionModuloSistema { get; set; }
    }
}
