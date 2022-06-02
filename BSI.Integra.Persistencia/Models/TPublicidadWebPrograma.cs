using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPublicidadWebPrograma
    {
        public int Id { get; set; }
        public int? IdPublicidadWeb { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int OrdenPrograma { get; set; }
        public bool ModificarInformacion { get; set; }
        public bool Duracion { get; set; }
        public bool Inicios { get; set; }
        public bool Precios { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPublicidadWeb IdPublicidadWebNavigation { get; set; }
    }
}
