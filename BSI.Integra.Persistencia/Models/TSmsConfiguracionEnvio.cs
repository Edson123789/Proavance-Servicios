using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSmsConfiguracionEnvio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPgeneral { get; set; }

        public virtual TConjuntoListaDetalle IdConjuntoListaDetalleNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TPlantilla IdPlantillaNavigation { get; set; }
    }
}
