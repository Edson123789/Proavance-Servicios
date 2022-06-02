using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using BSI.Integra.Aplicacion.DTOs;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: PgeneralBO
    /// Autor: _ _ _ _ _ _ .
    /// Fecha: 30/04/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_Pgeneral
    /// </summary>
    public class PgeneralBO : BaseBO
    {
        /// Propiedades		                Significado
        /// -------------	                -----------------------
        /// IdPgeneral                      Id de Programa General                                
        /// Nombre                          Nombre de programa general
        /// PwImgPortada                    Imagen de portada de portal web
        /// PwImgPortadaAlf                 Imagen de portada de portal web 
        /// PwImgSecundaria                 Imagen secundaria de portal web
        /// PwImgSecundariaAlf              Imagen secundaria de portal web
        /// IdPartner                       Id de partner
        /// IdArea                          Id de área
        /// IdSubArea                       Id de subarea
        /// IdCategoria                     Id de categoría
        /// PwEstado                        Estado Portal Web
        /// PwMostrarBsplay                 Portal Web mostrar
        /// PwDuracion                      Duración Portal Web
        /// IdBusqueda                      Id de búsqueda
        /// IdChatZopim                     Id chat
        /// PgTitulo                        Título
        /// Codigo                          Código
        /// UrlImagenPortadaFr              Url de imagen de portada
        /// UrlBrochurePrograma             Url de brochure de portada
        /// UrlPartner                      Url de partner
        /// UrlVersion                      Url de versión
        /// PwTituloHtml                    Título html de portal web
        /// EsModulo                        Es módulo
        /// NombreCorto                     Nombre Corto
        /// IdPagina                        Id de página
        /// ChatActivo                      Chat activo
        /// IdMigracion                     Id de migración
        /// PwDescripcionGeneral            Descripción general de Portal Web
        /// TieneProyectoDeAplicacion       Validación de proyecto de aplicación
        /// IdTipoPrograma                  Id de tipo de programa
        /// CodigoPartner                   Código de partner
        /// LogoPrograma                    Logo de programa
        /// UrlLogoPrograma                 Url de logo de programa
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string PwImgPortada { get; set; }
        public string PwImgPortadaAlf { get; set; }
        public string PwImgSecundaria { get; set; }
        public string PwImgSecundariaAlf { get; set; }
        public int IdPartner { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdCategoria { get; set; }
        public string PwEstado { get; set; }
        public string PwMostrarBsplay { get; set; }
        public string PwDuracion { get; set; }
        public int IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string PgTitulo { get; set; }
        public string Codigo { get; set; }
        public string UrlImagenPortadaFr { get; set; }
        public string UrlBrochurePrograma { get; set; }
        public string UrlPartner { get; set; }
        public string UrlVersion { get; set; }
        public string PwTituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        public string NombreCorto { get; set; }
        public int IdPagina { get; set; }
        public int ChatActivo { get; set; }
        public Guid? IdMigracion { get; set; }
        public string PwDescripcionGeneral { get; set; }
        public bool? TieneProyectoDeAplicacion { get; set; }
        public int? IdTipoPrograma { get; set; }
        public string CodigoPartner { get; set; }

        public string LogoPrograma { get; set; }
        public string UrlLogoPrograma { get; set; }


        //Hijos BO
        public List<PgeneralParametroSeoPwBO> PGeneralParametroSeoPw;
        public List<PgeneralDescripcionBO> PgeneralDescripcion;
        public List<AdicionalProgramaGeneralBO> AdicionalProgramaGeneral;
        public List<ProgramaAreaRelacionadaBO> ProgramaAreaRelacionada;
        public List<PgeneralExpositorBO> PgeneralExpositor;
        public List<SuscripcionProgramaGeneralBO> SuscripcionProgramaGeneral;

        public List<ProgramaGeneralPerfilScoringCiudadBO> ProgramaGeneralPerfilScoringCiudad;
        public List<ProgramaGeneralPerfilScoringModalidadBO> ProgramaGeneralPerfilScoringModalidad;
        public List<ProgramaGeneralPerfilScoringAformacionBO> ProgramaGeneralPerfilScoringAformacion;
        public List<ProgramaGeneralPerfilScoringIndustriaBO> ProgramaGeneralPerfilScoringIndustria;
        public List<ProgramaGeneralPerfilScoringCargoBO> ProgramaGeneralPerfilScoringCargo;
        public List<ProgramaGeneralPerfilScoringAtrabajoBO> ProgramaGeneralPerfilScoringAtrabajo;
        public List<ProgramaGeneralPerfilScoringCategoriaBO> ProgramaGeneralPerfilScoringCategoria;

        public List<ProgramaGeneralPerfilCiudadCoeficienteBO> ProgramaGeneralPerfilCiudadCoeficiente;
        public List<ProgramaGeneralPerfilModalidadCoeficienteBO> ProgramaGeneralPerfilModalidadCoeficiente;
        public List<ProgramaGeneralPerfilCategoriaCoeficienteBO> ProgramaGeneralPerfilCategoriaCoeficiente;
        public List<ProgramaGeneralPerfilCargoCoeficienteBO> ProgramaGeneralPerfilCargoCoeficiente;
        public List<ProgramaGeneralPerfilIndustriaCoeficienteBO> ProgramaGeneralPerfilIndustriaCoeficiente;
        public List<ProgramaGeneralPerfilAformacionCoeficienteBO> ProgramaGeneralPerfilAformacionCoeficiente;
        public List<ProgramaGeneralPerfilAtrabajoCoeficienteBO> ProgramaGeneralPerfilAtrabajoCoeficiente;

        public List<ProgramaGeneralPerfilTipoDatoBO> ProgramaGeneralPerfilTipoDato;
        public List<ProgramaGeneralPerfilEscalaProbabilidadBO> ProgramaGeneralPerfilEscalaProbabilidad;
        public ProgramaGeneralPerfilInterceptoBO ProgramaGeneralPerfilIntercepto;

        public List<ModeloPredictivoIndustriaBO> ModeloPredictivoIndustria;
        public List<ModeloPredictivoCargoBO> ModeloPredictivoCargo;
        public List<ModeloPredictivoFormacionBO> ModeloPredictivoFormacion;
        public List<ModeloPredictivoTrabajoBO> ModeloPredictivoTrabajo;
        public List<ModeloPredictivoCategoriaDatoBO> ModeloPredictivoCategoriaDato;
        public List<ModeloPredictivoTipoDatoBO> ModeloPredictivoTipoDato;
        public List<ModeloPredictivoEscalaProbabilidadBO> ModeloPredictivoEscalaProbabilidad;
        public List<PgeneralModalidadBO> PgeneralModalidad;
        public List<PgeneralVersionProgramaBO> PgeneralVersionPrograma;
        public ModeloPredictivoBO ModeloPredictivo;
        
        private PgeneralRepositorio _repPgeneral;
        public PgeneralBO()
        {
            _repPgeneral = new PgeneralRepositorio();
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
        public PgeneralBO(int id, integraDBContext contexto)
        {
            _repPgeneral = new PgeneralRepositorio(contexto);
            var ProgramaGeneral = _repPgeneral.FirstById(id);
            this.Id = ProgramaGeneral.Id;
            this.IdPgeneral = ProgramaGeneral.Id;
            this.Nombre = ProgramaGeneral.Nombre;
            this.PwImgPortada = ProgramaGeneral.PwImgPortada;
            this.PwImgPortadaAlf = ProgramaGeneral.PwImgPortadaAlf;
            this.PwImgSecundaria = ProgramaGeneral.PwImgSecundaria;
            this.PwImgSecundariaAlf = ProgramaGeneral.PwImgSecundariaAlf;
            this.IdPartner = ProgramaGeneral.IdPartner;
            this.IdArea = ProgramaGeneral.IdArea;
            this.IdSubArea = ProgramaGeneral.IdSubArea;
            this.IdCategoria = ProgramaGeneral.IdCategoria;
            this.PwEstado = ProgramaGeneral.PwEstado;
            this.PwMostrarBsplay = ProgramaGeneral.PwMostrarBsplay;
            this.PwDuracion = ProgramaGeneral.PwDuracion;
            this.IdBusqueda = ProgramaGeneral.IdBusqueda;
            this.PgTitulo = ProgramaGeneral.PgTitulo;
            this.Codigo = ProgramaGeneral.Codigo;
            this.UrlImagenPortadaFr = ProgramaGeneral.UrlImagenPortadaFr;
            this.UrlBrochurePrograma = ProgramaGeneral.UrlBrochurePrograma;
            this.UrlPartner = ProgramaGeneral.UrlPartner;
            this.UrlVersion = ProgramaGeneral.UrlVersion;
            this.PwTituloHtml = ProgramaGeneral.PwTituloHtml;
            this.EsModulo = ProgramaGeneral.EsModulo;
            this.NombreCorto = ProgramaGeneral.NombreCorto;
            this.IdPagina = ProgramaGeneral.IdPagina;
            this.ChatActivo = ProgramaGeneral.ChatActivo;
            this.UsuarioCreacion = ProgramaGeneral.UsuarioCreacion;
            this.UsuarioModificacion = ProgramaGeneral.UsuarioModificacion;
            this.Estado = ProgramaGeneral.Estado;
            this.RowVersion = ProgramaGeneral.RowVersion;
            this.FechaCreacion = ProgramaGeneral.FechaCreacion;
            this.FechaModificacion = ProgramaGeneral.FechaModificacion;
            this.IdMigracion = ProgramaGeneral.IdMigracion;

            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
        public List<PGeneralFiltroConUrlDTO> ObtenerProgramaConUrlPortal()
        {
            HelperSlug helper = new HelperSlug();
            var programasGenerales = _repPgeneral.ObtenerProgramasConUrlFiltro();
            programasGenerales.Select(c =>
            {
                c.Url = "/" + c.Descripcion + "/" + helper.GenerateSlug(c.Titulo, c.IdBusqueda);
                return c;
            }).ToList();
            return programasGenerales;
        }

    }

    public partial class RegistroProgramaGeneral
    {
        public virtual List<PgeneralBO> listaProgramaGeneral { get; set; }
        public virtual List<AreaCapacitacionFiltroDTO> listaAreasFiltro { get; set; }
        public virtual List<SubAreaCapacitacionAutoselectDTO> listaSubAreasFiltro { get; set; }

    }
}
