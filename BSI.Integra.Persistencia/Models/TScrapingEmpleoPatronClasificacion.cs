using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TScrapingEmpleoPatronClasificacion
    {
        public TScrapingEmpleoPatronClasificacion()
        {
            TScrapingEmpleoResultadoClasificacion = new HashSet<TScrapingEmpleoResultadoClasificacion>();
        }

        public int Id { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Patron { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TScrapingEmpleoResultadoClasificacion> TScrapingEmpleoResultadoClasificacion { get; set; }
    }
}
