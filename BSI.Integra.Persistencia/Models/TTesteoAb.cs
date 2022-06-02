using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTesteoAb
    {
        public TTesteoAb()
        {
            TFormularioLandingAb = new HashSet<TFormularioLandingAb>();
        }

        public int Id { get; set; }
        public int IdFormularioLandingPage { get; set; }
        public int IdPlantillaLandingPage { get; set; }
        public string NombrePlantilla { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TFormularioLandingAb> TFormularioLandingAb { get; set; }
    }
}
