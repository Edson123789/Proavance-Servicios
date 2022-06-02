using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConjuntoAnuncio
    {
        public TConjuntoAnuncio()
        {
            InverseIdConjuntoAnuncioFuenteNavigation = new HashSet<TConjuntoAnuncio>();
            TAnuncio = new HashSet<TAnuncio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string Origen { get; set; }
        public string IdConjuntoAnuncioFacebook { get; set; }
        public DateTime? FechaCreacionCampania { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdConjuntoAnuncioFuente { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdConjuntoAnuncioTipoObjetivo { get; set; }
        public int? IdFormularioPlantilla { get; set; }
        public string Adicional { get; set; }
        public string EnlaceFormulario { get; set; }
        public bool? EsGrupal { get; set; }
        public bool? EsPaginaWeb { get; set; }
        public bool? EsPreLanzamiento { get; set; }
        public int? IdConjuntoAnuncioSegmento { get; set; }
        public int? IdConjuntoAnuncioTipoGenero { get; set; }
        public int? IdPais { get; set; }
        public string Propietario { get; set; }
        public string NumeroAnuncio { get; set; }
        public string NumeroSemana { get; set; }
        public string DiaEnvio { get; set; }
        public string NombreInicial { get; set; }

        public virtual TConjuntoAnuncio IdConjuntoAnuncioFuenteNavigation { get; set; }
        public virtual TConjuntoAnuncioTipoObjetivo IdConjuntoAnuncioTipoObjetivoNavigation { get; set; }
        public virtual TFormularioPlantilla IdFormularioPlantillaNavigation { get; set; }
        public virtual ICollection<TConjuntoAnuncio> InverseIdConjuntoAnuncioFuenteNavigation { get; set; }
        public virtual ICollection<TAnuncio> TAnuncio { get; set; }
    }
}
