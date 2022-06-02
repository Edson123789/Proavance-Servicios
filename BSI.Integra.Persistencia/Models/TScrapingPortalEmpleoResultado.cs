using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TScrapingPortalEmpleoResultado
    {
        public TScrapingPortalEmpleoResultado()
        {
            TScrapingEmpleoResultadoClasificacion = new HashSet<TScrapingEmpleoResultadoClasificacion>();
        }

        public int Id { get; set; }
        public int IdScrapingPagina { get; set; }
        public string TituloAnuncio { get; set; }
        public string Url { get; set; }
        public string PortalId { get; set; }
        public DateTime FechaAnuncio { get; set; }
        public string Puesto { get; set; }
        public string Empresa { get; set; }
        public string Ubicacion { get; set; }
        public string Jornada { get; set; }
        public string TipoContrato { get; set; }
        public string Salario { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionHtml { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string Error { get; set; }
        public string Modalidad { get; set; }
        public bool? EsClasificado { get; set; }

        public virtual ICollection<TScrapingEmpleoResultadoClasificacion> TScrapingEmpleoResultadoClasificacion { get; set; }
    }
}
