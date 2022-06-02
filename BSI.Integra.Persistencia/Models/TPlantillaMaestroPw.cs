using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPlantillaMaestroPw
    {
        public TPlantillaMaestroPw()
        {
            TPlantillaPw = new HashSet<TPlantillaPw>();
            TSeccionMaestraPw = new HashSet<TSeccionMaestraPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Repeticion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TPlantillaPw> TPlantillaPw { get; set; }
        public virtual ICollection<TSeccionMaestraPw> TSeccionMaestraPw { get; set; }
    }
}
