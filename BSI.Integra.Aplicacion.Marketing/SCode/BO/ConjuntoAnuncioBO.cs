using System;

using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Marketing.Repositorio;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ConjuntoAnuncioBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string Origen { get; set; }
        public string IdConjuntoAnuncioFacebook { get; set; }
        public DateTime? FechaCreacionCampania { get; set; }


        public int? IdConjuntoAnuncioFuente { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdConjuntoAnuncioTipoObjetivo { get; set; }
        public int? IdFormularioPlantilla { get; set; }
        public string Adicional { get; set; }
        public string NumeroAnuncio { get; set; }
        public string NumeroSemana { get; set; }
        public string Propietario { get; set; }
        public string DiaEnvio { get; set; }
        public string EnlaceFormulario { get; set; }
        public bool? EsGrupal { get; set; }
        public bool? EsPaginaWeb { get; set; }
        public bool? EsPrelanzamiento { get; set; }
        public int? IdConjuntoAnuncioSegmento { get; set; }
        public int? IdConjuntoAnuncioTipoGenero { get; set; }
        public int? IdPais { get; set; }
        public Guid? IdMigracion { get; set; }



        private ConjuntoAnuncioRepositorio _repConjuntoAnuncio;
		public ConjuntoAnuncioBO()
		{

		}
	}

}
