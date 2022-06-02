using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ConjuntoAnuncioDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int? IdCategoriaOrigen { get; set; }
		public string Origen { get; set; }
		public string IdConjuntoAnuncioFacebook { get; set; }
		public DateTime? FechaCreacionCampania { get; set; }
		public DateTime FechaCreacion { get; set; }

        public int? IdConjuntoAnuncioFuente { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int? IdConjuntoAnuncioTipoObjetivo { get; set; }
        public string NombreFormularioPlantilla { get; set; }
        public int? IdFormularioPlantilla { get; set; }
        public string Adicional { get; set; }
        public string EnlaceFormulario { get; set; }
        public bool? EsGrupal { get; set; }
        public bool? EsPaginaWeb { get; set; }
        public bool? EsPrelanzamiento { get; set; }
        public int? IdConjuntoAnuncioSegmento { get; set; }
        public int? IdConjuntoAnuncioTipoGenero { get; set; }
        public int? IdPais { get; set; }
        public string Propietario { get; set; }
        public string NroAnuncio { get; set; }
        public string NroSemana { get; set; }
        public string DiaEnvio { get; set; }
        public string NombreUsuario { get; set; }

        public List<AnuncioDTO> Anuncios { get; set; }
    }

    public class DatoLandingPageDTO
    {
        public int Id { get; set; }
        public int IdFormularioLandingPage { get; set; }
        public string PeCentroCosto { get; set; }
        public int IdConjuntoAnuncio { get; set; }        
    }

    public class ConjuntoAnuncioAsociacionDTO
    {
        public int Id { get; set; }
        public string NombreConjuntoAnuncio { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string IdConjuntoAnuncioFacebook { get; set; }
        public string NombreConjuntoAnuncioFB { get; set; }
        public string FechaCreacionCampania { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class ConjuntoAnuncioAsociacionGrillaDTO
    {
        public Paginador paginador { get; set; }

    }

}
