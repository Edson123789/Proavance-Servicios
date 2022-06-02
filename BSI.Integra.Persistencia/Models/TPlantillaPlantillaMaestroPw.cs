using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPlantillaPlantillaMaestroPw
    {
        public int Id { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdSeccionMaestraPw { get; set; }
        public string Contenido { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPlantillaPw IdPlantillaPwNavigation { get; set; }
        public virtual TSeccionMaestraPw IdSeccionMaestraPwNavigation { get; set; }
    }
}
