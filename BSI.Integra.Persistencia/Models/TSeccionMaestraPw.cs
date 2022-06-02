using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSeccionMaestraPw
    {
        public TSeccionMaestraPw()
        {
            TPlantillaPlantillaMaestroPw = new HashSet<TPlantillaPlantillaMaestroPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPlantillaMaestroPw IdPlantillaMaestroPwNavigation { get; set; }
        public virtual ICollection<TPlantillaPlantillaMaestroPw> TPlantillaPlantillaMaestroPw { get; set; }
    }
}
