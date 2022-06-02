using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using System.Transactions;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using System.Net;
using System.IO;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Http;
using BSI.Integra.Aplicacion.Planificacion.SCode.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.SCode.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Servicios;
using Newtonsoft.Json;
using System.Text;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/ProgramaGeneral
    /// Autor: Carlos Crispin R.
    /// Fecha: 04/03/2021
    /// <summary>
    /// Gestiona todo el modulo de programa general y sus asociados
    /// </summary>

    [Route("api/ProgramaGeneral")]
    public class ProgramaGeneralController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PGeneralForoAsignacionProveedorRepositorio _repPGeneralForoAsignacionProveedor;
        private readonly ProveedorRepositorio _repProveedor;
        private readonly ModalidadCursoRepositorio _repModalidadCurso;
        private readonly IntegraAspNetUsersRepositorio _repIntegraAspNetUsers;

        public ProgramaGeneralController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPGeneralForoAsignacionProveedor = new PGeneralForoAsignacionProveedorRepositorio(integraDBContext);
            _repProveedor = new ProveedorRepositorio(integraDBContext);
            _repModalidadCurso = new ModalidadCursoRepositorio(integraDBContext);
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(integraDBContext);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerProgramaGeneralParaChatPorId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                var ProgramaChat = _repPGeneral.ObtenerPgeneralporId(id);
                return Ok(ProgramaChat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los elementos para el filtro
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase PGeneralSubAreaFiltroDTO, caso contrario response 400</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                return Ok(_repPGeneral.ObtenerProgramaSubAreaFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPgeneralFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PgeneralRepositorio _repProgramaGeneral = new PgeneralRepositorio(contexto);
                var repositorio = _repProgramaGeneral.ObtenerProgramasFiltro();

                return Ok(new { data = repositorio });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult GetProgramaGeneralIdNombre()
        {
            try
            {
                PgeneralRepositorio _pgeneralRepositorio = new PgeneralRepositorio();
                return Ok(_pgeneralRepositorio.ObtenerProgramasFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerProgramasGeneralesPanel()
        {
            try
            {
                PgeneralRepositorio _repProgramaGeneral = new PgeneralRepositorio();
                var listaProgramas = _repProgramaGeneral.ListarProgramasPanel();

                return Ok(listaProgramas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerPGCriterioEvaluacionPresencial([FromBody] int objeto)
        {
            try
            {
                PGeneralCriterioEvaluacionRepositorio _repProgramaGeneral = new PGeneralCriterioEvaluacionRepositorio();
                var listaProgramas = _repProgramaGeneral.ListarPGcriteriosEvaluacionPresencial(objeto);

                return Ok(listaProgramas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerPGCriterioEvaluacionAonline([FromBody] int objeto)
        {
            try
            {
                PGeneralCriterioEvaluacionRepositorio _repProgramaGeneral = new PGeneralCriterioEvaluacionRepositorio();
                var listaProgramas = _repProgramaGeneral.ListarPGcriteriosEvaluacionAonline(objeto);

                return Ok(listaProgramas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerPGCriterioEvaluacionOnline([FromBody] int objeto)
        {
            try
            {
                PGeneralCriterioEvaluacionRepositorio _repProgramaGeneral = new PGeneralCriterioEvaluacionRepositorio();
                var listaProgramas = _repProgramaGeneral.ListarPGcriteriosEvaluacionOnline(objeto);

                return Ok(listaProgramas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerProgramasGeneral([FromBody] FiltroGridProgramaGeneralDTO objeto)
        {
            try
            {
                PgeneralRepositorio _repProgramaGeneral = new PgeneralRepositorio();
                var listaProgramas = _repProgramaGeneral.ListarProgramaGeneral(objeto);

                return Ok(listaProgramas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosProgramaGeneral()
        {
            try
            {
                AreaCapacitacionRepositorio _repSubArea = new AreaCapacitacionRepositorio();
                SubAreaCapacitacionRepositorio _repAreaCapacitacion = new SubAreaCapacitacionRepositorio();
                PartnerPwRepositorio _repPartner = new PartnerPwRepositorio();
                ParametroSeoPwRepositorio _repParametroSeo = new ParametroSeoPwRepositorio();
                ExpositorRepositorio _repExpositores = new ExpositorRepositorio();
                CategoriaProgramaRepositorio _repCategoria = new CategoriaProgramaRepositorio();
                VisualizacionBsPlayRepositorio _repBsPlay = new VisualizacionBsPlayRepositorio();
                TituloRepositorio _repTitulo = new TituloRepositorio();

                CargoRepositorio _repCargo = new CargoRepositorio();
                AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio();
                AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio();
                IndustriaRepositorio _repIndustria = new IndustriaRepositorio();
                CiudadRepositorio _repCiudad = new CiudadRepositorio();
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                PgeneralRepositorio _repPrograma = new PgeneralRepositorio();
                PerfilContactoProgramaColumnaRepositorio _repColumnas = new PerfilContactoProgramaColumnaRepositorio();
                ModalidadCursoRepositorio repModalidad = new ModalidadCursoRepositorio();
                PaginaWebPwRepositorio repPagina = new PaginaWebPwRepositorio();
                VersionProgramaRepositorio _repVersionPrograma = new VersionProgramaRepositorio();
                TipoProgramaRepositorio _repTipoPrograma = new TipoProgramaRepositorio();
                ProveedorRepositorio _repProveedor = new ProveedorRepositorio();
                var combosProgramaGeneral = new
                {
                    AreasCapacitacion = _repSubArea.ObtenerAreaCapacitacionFiltro(),
                    SubAreasCapacitacion = _repAreaCapacitacion.ObtenerSubAreasParaFiltro(),
                    Partners = _repPartner.ObtenerPartnerFiltro(),
                    ParametrosSeo = _repParametroSeo.ObtnerParametroSeoFiltro(),
                    Expositores = _repExpositores.ObtenerExpositoresFiltro(),
                    CategoriasPrograma = _repCategoria.ObtenerCategoriasPrograma(),
                    VisualizacionBsPlays = _repBsPlay.ObtenerBsPlayFiltro(),
                    Titulos = _repTitulo.ObtenerTitulosFiltro(),
                    Cargos = _repCargo.ObtenerCargoFiltro(),
                    AreasFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro(),
                    AreasTrabajo = _repAreaTrabajo.ObtenerAreasTrabajo(),
                    Industrias = _repIndustria.ObtenerIndustriaFiltro(),
                    Ciudades = _repCiudad.ObtenerCiudadFiltro(),
                    CategoriasOrigen = _repCategoriaOrigen.ObtenerCategoriaConHijos(),
                    TiposDatos = _repTipoDato.ObtenerFiltro(),
                    ProgramasGenerales = _repPrograma.ObtenerProgramasFiltro(),
                    ColumnasPerfilContacto = _repColumnas.ObtenerTodoFiltro(),
                    Modalidades = repModalidad.ObtenerModalidadCursoFiltro(),
                    Pagina = repPagina.ObtenerPaginasWeb(),
                    VersionPrograma = _repVersionPrograma.ObtenerFiltro(),
                    TipoPrograma = _repTipoPrograma.ObtenerFiltro(),
                    proveedorCurso = _repProveedor.ObtenerProveedorParaFiltro()
                };

                return Ok(combosProgramaGeneral);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosProgramaGeneralVideo()
        {
            try
            {
                AreaCapacitacionRepositorio _repSubArea = new AreaCapacitacionRepositorio();
                SubAreaCapacitacionRepositorio _repAreaCapacitacion = new SubAreaCapacitacionRepositorio();
                PartnerPwRepositorio _repPartner = new PartnerPwRepositorio();
                PgeneralRepositorio _repPrograma = new PgeneralRepositorio();
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                TipoEvaluacionTrabajoRepositorio _tipoEvaluacionTrabajo = new TipoEvaluacionTrabajoRepositorio();
                TipoMarcadorRepositorio _repTipoMarcador = new TipoMarcadorRepositorio();

                var combosProgramaGeneral = new
                {
                    AreasCapacitacion = _repSubArea.ObtenerAreaCapacitacionFiltro(),
                    SubAreasCapacitacion = _repAreaCapacitacion.ObtenerSubAreasParaFiltro(),
                    Partners = _repPartner.ObtenerPartnerFiltro(),
                    ProgramasGenerales = _repPrograma.ObtenerProgramasFiltro(),
                    TipoVistaVideo = _configurarVideoProgramaRepositorio.ListaTipoVistaVideo(),
                    TipoEvaluacionTrabajo = _tipoEvaluacionTrabajo.listaTipoEvaluacionTrabajo(),
                    tipoMarcador = _repTipoMarcador.ObtenerTipoMarcador()
                };

                return Ok(combosProgramaGeneral);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Edgar Serruto.
        /// Fecha: 26/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de programa por Id
        /// </summary>
        /// <param name="IdProgramaGeneral">Id de Programa General</param>
        /// <returns>DetallesProgramasDTO</returns>
        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerDetalleProgramas(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralRepositorio _repProgramaGeneral = new PgeneralRepositorio();
                PgeneralDescripcionRepositorio _repDescripcion = new PgeneralDescripcionRepositorio();
                AdicionalProgramaGeneralRepositorio _repDescripcionAdicional = new AdicionalProgramaGeneralRepositorio();
                PgeneralExpositorRepositorio _repPGeneralExpositores = new PgeneralExpositorRepositorio();
                ProgramaAreaRelacionadaRepositorio _repAreasRelacionadas = new ProgramaAreaRelacionadaRepositorio();
                SuscripcionProgramaGeneralRepositorio _repSuscripciones = new SuscripcionProgramaGeneralRepositorio();
                PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio();
                PgeneralConfiguracionBeneficioRepositorio _repPgeneralConfiguracionBeneficios = new PgeneralConfiguracionBeneficioRepositorio();
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio();
                PgeneralModalidadRepositorio _repPgeneralModalidad = new PgeneralModalidadRepositorio();
                PgeneralCodigoPartnerRepositorio _repPgeneralCodigoPartner = new PgeneralCodigoPartnerRepositorio();
                PgeneralProyectoAplicacionRepositorio _repPgeneralProyectoAplicacion = new PgeneralProyectoAplicacionRepositorio();

                DetallesProgramasDTO detalles = new DetallesProgramasDTO();

                detalles.ParametrosSeo = _repProgramaGeneral.ListarParametrosSeoPorPrograma(IdProgramaGeneral);
                detalles.DescripcionesGenerales = _repDescripcion.ObtnerPgeneralDescripcionPorPrograma(IdProgramaGeneral);
                detalles.DescripcionesAdicionales = _repDescripcionAdicional.ObtenerDescripcionesAdicionales(IdProgramaGeneral);
                detalles.Expositores = _repPGeneralExpositores.ObtenerExpositoresPorPrograma(IdProgramaGeneral);
                detalles.Modalidad = _repPgeneralModalidad.ObtenerModalidaCurso(IdProgramaGeneral);
                detalles.AreasRelacionadas = _repAreasRelacionadas.ObtenerAreasRelacionadasPorPrograma(IdProgramaGeneral);
                detalles.Suscripciones = _repSuscripciones.ObtenerSuscripcionesPorPrograma(IdProgramaGeneral);
                detalles.ConfiguracionPlantilla = _repPgeneralConfiguracionPlantilla.ObtenerPgeneralConfiuracionPlantilla(IdProgramaGeneral);
                detalles.ConfiguracionBeneficio = _repPgeneralConfiguracionBeneficios.ObtenerPgeneralConfiuracionBeneficios(IdProgramaGeneral);
                detalles.PgeneralVersionPrograma = _repProgramaGeneral.ListaVersionPrograma(IdProgramaGeneral);
                detalles.PgeneralCodigoPartner = _repPgeneralCodigoPartner.ObtenerListaCodigoPartner(IdProgramaGeneral);
                detalles.pgeneralProyectoAplicacion = _repPgeneralProyectoAplicacion.ObtenerPgeneralProyectoAplicacion(IdProgramaGeneral);

                var sinAgrupacionPGeneralForoAsignacionProveedor = _repPGeneralForoAsignacionProveedor.GetBy(x => x.IdPgeneral == IdProgramaGeneral).ToList();
                var agrupacionPGeneralForoAsignacionProveedor = sinAgrupacionPGeneralForoAsignacionProveedor.GroupBy(x => new { x.IdPgeneral, x.IdModalidadCurso }).Select(x => new PGeneralForoAsignacionProveedorDTO
                {
                    IdModalidadCurso = x.Key.IdModalidadCurso,
                    IdProveedor = x.GroupBy(y => y.IdProveedor).Select(y => y.Key).ToList(),
                }).ToList();

                detalles.PGeneralForoAsignacionProveedor = agrupacionPGeneralForoAsignacionProveedor;
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosConfiguracionPlantilla(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralRepositorio _rePgeneral = new PgeneralRepositorio();
                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio();
                EstadoMatriculaRepositorio _repEstadoMatricula = new EstadoMatriculaRepositorio();
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                OperadorComparacionRepositorio _repOperadorComparacion = new OperadorComparacionRepositorio();
                PaisRepositorio _repPais = new PaisRepositorio();
                BeneficioDatoAdicionalRepositorio _repBeneficioDatoAdicional = new BeneficioDatoAdicionalRepositorio();
                PgeneralVersionProgramaRepositorio _repVersionesPrograma = new PgeneralVersionProgramaRepositorio();

                var detalles = new
                {
                    filtroPlantilla = _repPlantilla.ObtenerListaPlantillaCertificado(),
                    filtroModalidadCurso = _repModalidadCurso.ObtenerTodoFiltro(),
                    filtroEstadoMatricula = _repEstadoMatricula.ObtenerTodoFiltro(),
                    filtroOperadorComparacion = _repOperadorComparacion.ObtenerListaOperadorComparacion(),
                    filtroSubEstadoMatricula = _repMatriculaCabecera.ObtenerSubEstadoMatricula(),
                    filtroPaises = _repPais.ObtenerListaPais(),
                    filtroBeneficioDatoAdicional = _repBeneficioDatoAdicional.ObtenerTodoFiltro(),
                    filtroVersiones = _repVersionesPrograma.ObtenerListaVersionesPrograma(IdProgramaGeneral)
                };

                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosEstadosSubEstados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                EstadoMatriculaRepositorio _repEstadoMatricula = new EstadoMatriculaRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                MonedaRepositorio _repMoneda = new MonedaRepositorio(_integraDBContext);
                var detalles = new
                {

                    filtroEstadoMatricula = _repEstadoMatricula.ObtenerTodoFiltro(),
                    filtroSubEstadoMatricula = _repMatriculaCabecera.ObtenerSubEstadoMatricula(),
                    filtroPaises = _repPais.ObtenerListaPaisTarifarios(),
                    filtroMoneda = _repMoneda.ObtenerMonedaTodo()
                };

                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerCursos(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralAsubPgeneralRepositorio cursos = new PgeneralAsubPgeneralRepositorio();

                var cursosHijos = cursos.ObtenerCursosPorPrograma(IdProgramaGeneral);
                return Ok(cursosHijos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerDocumentosNoAsociadosYAsociados(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaPaisRepositorio repDocumentos = new PlantillaPaisRepositorio();
                DocumentoProgramaDTO documentos = new DocumentoProgramaDTO();

                documentos.DocumentosNoAsociados = repDocumentos.ObtenerDocumentosNoAsociados(IdProgramaGeneral);
                documentos.DocumentosAsociados = repDocumentos.ObtenerDocumentosAsociados(IdProgramaGeneral);
                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerCursosRelacionados(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RelacionProgramasDTO relacionCursos = new RelacionProgramasDTO();
                PgeneralRelacionadoRepositorio repCursos = new PgeneralRelacionadoRepositorio();
                relacionCursos.CursosRelacionados = repCursos.ObtenerCursosRelacionadosPorPrograma(IdProgramaGeneral);
                relacionCursos.CursosNoRelacionados = repCursos.ObtenerCursosNoRelacionadosPorPrograma(IdProgramaGeneral);
                return Ok(relacionCursos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerBeneficiosYPreRequisitos(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BeneficioPreRequisitoDTO beneficioPreRequisito = new BeneficioPreRequisitoDTO();
                ProgramaGeneralPrerequisitoRepositorio repPreRequisitos = new ProgramaGeneralPrerequisitoRepositorio();
                ProgramaGeneralBeneficioRepositorio repBeneficios = new ProgramaGeneralBeneficioRepositorio();
                ProgramaGeneralMotivacionRepositorio _repMotivacion = new ProgramaGeneralMotivacionRepositorio();
                ProgramaGeneralCertificacionRepositorio _repCertificacion = new ProgramaGeneralCertificacionRepositorio();
                ProgramaGeneralProblemaRepositorio _repProblema = new ProgramaGeneralProblemaRepositorio();

                beneficioPreRequisito.PreRequisitos = repPreRequisitos.ObtenerPreRequisitosPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Beneficios = repBeneficios.ObteneBeneficiosPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Motivaciones = _repMotivacion.ObteneMotivacionesPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Certificaciones = _repCertificacion.ObteneCertificacionesPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Problemas = _repProblema.ObteneProblemasPorModalidades(IdProgramaGeneral);

                return Ok(beneficioPreRequisito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerPerfilContacto(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPerfilScoringCiudadRepositorio repCiudadScoring = new ProgramaGeneralPerfilScoringCiudadRepositorio();
                ProgramaGeneralPerfilScoringModalidadRepositorio repModalidadScoring = new ProgramaGeneralPerfilScoringModalidadRepositorio();
                ProgramaGeneralPerfilScoringAformacionRepositorio repFormacionScoring = new ProgramaGeneralPerfilScoringAformacionRepositorio();
                ProgramaGeneralPerfilScoringIndustriaRepositorio repIndustriaScoring = new ProgramaGeneralPerfilScoringIndustriaRepositorio();
                ProgramaGeneralPerfilScoringCargoRepositorio repCargoScoring = new ProgramaGeneralPerfilScoringCargoRepositorio();
                ProgramaGeneralPerfilScoringAtrabajoRepositorio repTrabajoScoring = new ProgramaGeneralPerfilScoringAtrabajoRepositorio();
                ProgramaGeneralPerfilScoringCategoriaRepositorio repCategoraiScoring = new ProgramaGeneralPerfilScoringCategoriaRepositorio();

                ProgramaGeneralPerfilCiudadCoeficienteRepositorio repCiudadCoeficiente = new ProgramaGeneralPerfilCiudadCoeficienteRepositorio();
                ProgramaGeneralPerfilModalidadCoeficienteRepositorio repModalidadCoeficiente = new ProgramaGeneralPerfilModalidadCoeficienteRepositorio();
                ProgramaGeneralPerfilCategoriaCoeficienteRepositorio repCategoriaCoeficiente = new ProgramaGeneralPerfilCategoriaCoeficienteRepositorio();
                ProgramaGeneralPerfilCargoCoeficienteRepositorio repCargoCoeficiente = new ProgramaGeneralPerfilCargoCoeficienteRepositorio();
                ProgramaGeneralPerfilIndustriaCoeficienteRepositorio repIndustriaCoeficiente = new ProgramaGeneralPerfilIndustriaCoeficienteRepositorio();
                ProgramaGeneralPerfilAformacionCoeficienteRepositorio repAFormacionCoeficiente = new ProgramaGeneralPerfilAformacionCoeficienteRepositorio();
                ProgramaGeneralPerfilAtrabajoCoeficienteRepositorio repAreaTrabajoCoeficiente = new ProgramaGeneralPerfilAtrabajoCoeficienteRepositorio();

                ProgramaGeneralPerfilTipoDatoRepositorio repTipoDato = new ProgramaGeneralPerfilTipoDatoRepositorio();
                ProgramaGeneralPerfilEscalaProbabilidadRepositorio repEscala = new ProgramaGeneralPerfilEscalaProbabilidadRepositorio();
                ProgramaGeneralPerfilInterceptoRepositorio repIntercepto = new ProgramaGeneralPerfilInterceptoRepositorio();

                PerfilContactoProgramaDTO perfilPrograma = new PerfilContactoProgramaDTO();
                CoeficienteScoringCiudadDTO ciudad = new CoeficienteScoringCiudadDTO();
                ciudad.CiudadEscoring = repCiudadScoring.ObtenerScoringCiudadPorPrograma(IdProgramaGeneral);
                ciudad.CiudadCoefiente = repCiudadCoeficiente.ObtenerCoeficienteCiudadPorPrograma(IdProgramaGeneral);
                perfilPrograma.Ciudad = ciudad;

                CoeficienteScoringModalidadDTO modalidad = new CoeficienteScoringModalidadDTO();
                modalidad.ModalidadEscoring = repModalidadScoring.ObtenerScoringModalidadPorPrograma(IdProgramaGeneral);
                modalidad.ModalidadCoefiente = repModalidadCoeficiente.ObtenerCoeficienteModalidadPorPrograma(IdProgramaGeneral);
                perfilPrograma.Modalidad = modalidad;

                CoeficienteScoringAFormacionDTO formacion = new CoeficienteScoringAFormacionDTO();
                formacion.FormacionEscoring = repFormacionScoring.ObtenerScoringAFormacionPorPrograma(IdProgramaGeneral);
                formacion.FormacionCoefiente = repAFormacionCoeficiente.ObtenerCoeficienteAformacionPorPrograma(IdProgramaGeneral);
                perfilPrograma.Formacion = formacion;

                CoeficienteScoringIndustriaDTO industria = new CoeficienteScoringIndustriaDTO();
                industria.IndustriaEscoring = repIndustriaScoring.ObtenerScoringIndustriaPorPrograma(IdProgramaGeneral);
                industria.IndustriaCoefiente = repIndustriaCoeficiente.ObtenerCoeficienteIndustriaPorPrograma(IdProgramaGeneral);
                perfilPrograma.Industria = industria;

                CoeficienteScoringCargoDTO cargo = new CoeficienteScoringCargoDTO();
                cargo.CargoEscoring = repCargoScoring.ObtenerScoringCargoPorPrograma(IdProgramaGeneral);
                cargo.CargoCoefiente = repCargoCoeficiente.ObtenerCoeficienteCargoPorPrograma(IdProgramaGeneral);
                perfilPrograma.Cargo = cargo;

                CoeficienteScoringATrabajoDTO trabajo = new CoeficienteScoringATrabajoDTO();
                trabajo.TrabajoEscoring = repTrabajoScoring.ObtenerScoringTrabajoPorPrograma(IdProgramaGeneral);
                trabajo.TrabajoCoefiente = repAreaTrabajoCoeficiente.ObtenerCoeficienteATrabajoPorPrograma(IdProgramaGeneral);
                perfilPrograma.Trabajo = trabajo;

                CoeficienteScoringCategoriaDTO categoria = new CoeficienteScoringCategoriaDTO();
                categoria.CategoriaCoefiente = repCategoriaCoeficiente.ObtenerCoeficienteCategoriaPorPrograma(IdProgramaGeneral);
                categoria.CategoriaEscoring = repCategoraiScoring.ObtenerScoringCategoriaPorPrograma(IdProgramaGeneral);
                perfilPrograma.Categoria = categoria;

                perfilPrograma.Escala = repEscala.ObtenerEscalaPorPrograma(IdProgramaGeneral);
                perfilPrograma.Intercepto = repIntercepto.ObtenerInterceptoPorPrograma(IdProgramaGeneral);
                perfilPrograma.TipoDato = repTipoDato.ObtenerTipoDatoPorPrograma(IdProgramaGeneral);

                return Ok(perfilPrograma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerModeloPredictivo(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModeloPredictivoRepositorio repPredictivo = new ModeloPredictivoRepositorio();
                ModeloPredictivoEscalaProbabilidadRepositorio repEscala = new ModeloPredictivoEscalaProbabilidadRepositorio();
                ModeloPredictivoTipoDatoRepositorio repTipoDato = new ModeloPredictivoTipoDatoRepositorio();

                ModeloPredictivoIndustriaRepositorio repIndustria = new ModeloPredictivoIndustriaRepositorio();
                ModeloPredictivoCargoRepositorio repCargo = new ModeloPredictivoCargoRepositorio();
                ModeloPredictivoFormacionRepositorio repFormacion = new ModeloPredictivoFormacionRepositorio();
                ModeloPredictivoTrabajoRepositorio repTrabajo = new ModeloPredictivoTrabajoRepositorio();
                ModeloPredictivoCategoriaDatoRepositorio repCategoria = new ModeloPredictivoCategoriaDatoRepositorio();

                ModeloPredictivoProgramaDTO modeloPredicitivo = new ModeloPredictivoProgramaDTO
                {
                    Cargo = repCargo.ObtenerCargoPorPrograma(IdProgramaGeneral),
                    Industria = repIndustria.ObtenerIndustriaPorPrograma(IdProgramaGeneral),
                    Formacion = repFormacion.ObtenerAreaFormacionPorPrograma(IdProgramaGeneral),
                    CategoriaOrigen = repCategoria.ObtenerCategoriaDatoPorPrograma(IdProgramaGeneral),
                    Trabajo = repTrabajo.ObtenerTrabajoPorPrograma(IdProgramaGeneral),
                    Escala = repEscala.ObtenerEscalaPorPrograma(IdProgramaGeneral),
                    TipoDato = repTipoDato.ObtenerTipoDatoPorPrograma(IdProgramaGeneral),
                    Intercepto = repPredictivo.ObtenerInterceptoPorPrograma(IdProgramaGeneral)
                };

                return Ok(modeloPredicitivo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 26/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Agrega registro nuevo de programa General
        /// </summary>
        /// <param name="Json">Información Compuesta de Programa General</param>
        /// <returns>PgeneralBO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProgramaGeneral([FromBody] DatosProgramaGeneralDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PartnerPwRepositorio repPartner = new PartnerPwRepositorio(contexto);
                AreaCapacitacionRepositorio repArea = new AreaCapacitacionRepositorio(contexto);
                SubAreaCapacitacionRepositorio repSubArea = new SubAreaCapacitacionRepositorio(contexto);
                TroncalPgeneralRepositorio repTroncalPgeneral = new TroncalPgeneralRepositorio(contexto);
                PgeneralRepositorio repPrograma = new PgeneralRepositorio();
                MontoPagoRepositorio repMontoPago = new MontoPagoRepositorio(contexto);
                PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(contexto);
                PgeneralConfiguracionPlantillaDetalleRepositorio _repPgeneralConfiguracionPlantillaDetalle = new PgeneralConfiguracionPlantillaDetalleRepositorio(contexto);

                PgeneralCodigoPartnerRepositorio _repPgeneralCodigoPartner = new PgeneralCodigoPartnerRepositorio(contexto);
                PgeneralCodigoPartnerVersionProgramaRepositorio _repPgeneralCodigoPartnerVersionPrograma = new PgeneralCodigoPartnerVersionProgramaRepositorio(contexto);
                PgeneralCodigoPartnerModalidadCursoRepositorio _repPgeneralCodigoPartnerModalidadCurso = new PgeneralCodigoPartnerModalidadCursoRepositorio(contexto);

                PgeneralProyectoAplicacionRepositorio _repPgeneralProyectoAplicacion = new PgeneralProyectoAplicacionRepositorio(contexto);
                PgeneralProyectoAplicacionModalidadRepositorio _repPgeneralProyectoAplicacionModalidad = new PgeneralProyectoAplicacionModalidadRepositorio(contexto);
                PgeneralProyectoAplicacionProveedorRepositorio _repPgeneralProyectoAplicacionProveedor = new PgeneralProyectoAplicacionProveedorRepositorio(contexto);

                PGeneralCriterioEvaluacionHijoRepositorio _repPgeneralCEHijo = new PGeneralCriterioEvaluacionHijoRepositorio(contexto);//LPPG


                TroncalPgeneralBO troncal = new TroncalPgeneralBO();
                troncal.IdTroncalPartner = repPartner.ObtenerPartnerAnterior(Json.ProgramaGeneral.IdPartner);
                troncal.IdArea = repArea.ObtenerAreaCapacitacionAnterior(Json.ProgramaGeneral.IdArea);
                troncal.IdSubArea = repSubArea.ObtenerSubAreaCapacitacionAnterior(Json.ProgramaGeneral.IdSubArea);
                troncal.Codigo = Json.ProgramaGeneral.Codigo;
                troncal.IdBusqueda = 0;
                troncal.Nombre = Json.ProgramaGeneral.Nombre;
                troncal.FechaCreacion = DateTime.Now;
                troncal.FechaModificacion = DateTime.Now;
                troncal.UsuarioCreacion = Json.Usuario;
                troncal.UsuarioModificacion = Json.Usuario;
                troncal.Estado = true;
                repTroncalPgeneral.Insert(troncal);

                troncal = repTroncalPgeneral.FirstById(troncal.Id);

                troncal.IdBusqueda = troncal.Id;
                repTroncalPgeneral.Update(troncal);

                PgeneralBO pgeneral = new PgeneralBO();
                pgeneral.IdPgeneral = troncal.Id;
                pgeneral.Nombre = Json.ProgramaGeneral.Nombre;
                pgeneral.PwImgPortada = Json.ProgramaGeneral.PwImgPortada;
                pgeneral.PwImgPortadaAlf = Json.ProgramaGeneral.PwImgPortadaAlf;
                pgeneral.PwImgSecundaria = Json.ProgramaGeneral.PwImgSecundaria;
                pgeneral.PwImgSecundariaAlf = Json.ProgramaGeneral.PwImgSecundariaAlf;
                pgeneral.IdPartner = Json.ProgramaGeneral.IdPartner;
                pgeneral.IdArea = Json.ProgramaGeneral.IdArea;
                pgeneral.IdSubArea = Json.ProgramaGeneral.IdSubArea;
                pgeneral.IdCategoria = Json.ProgramaGeneral.IdCategoria;
                pgeneral.PwEstado = Json.ProgramaGeneral.PwEstado;
                pgeneral.PwMostrarBsplay = Json.ProgramaGeneral.PwMostrarBsplay;
                pgeneral.PwDuracion = Json.ProgramaGeneral.PwDuracion;
                pgeneral.IdBusqueda = troncal.Id;
                pgeneral.PgTitulo = Json.ProgramaGeneral.PgTitulo;
                pgeneral.Codigo = Json.ProgramaGeneral.Codigo;
                pgeneral.UrlImagenPortadaFr = Json.ProgramaGeneral.UrlImagenPortadaFr;
                pgeneral.UrlBrochurePrograma = Json.ProgramaGeneral.UrlBrochurePrograma;
                pgeneral.UrlPartner = Json.ProgramaGeneral.UrlPartner;
                pgeneral.UrlVersion = Json.ProgramaGeneral.UrlVersion;
                pgeneral.PwTituloHtml = Json.ProgramaGeneral.PwTituloHtml;
                pgeneral.EsModulo = Json.ProgramaGeneral.EsModulo;
                pgeneral.NombreCorto = Json.ProgramaGeneral.NombreCorto;
                pgeneral.IdPagina = Json.ProgramaGeneral.IdPagina;
                pgeneral.ChatActivo = Json.ProgramaGeneral.ChatActivo;
                pgeneral.PwDescripcionGeneral = Json.ProgramaGeneral.PwDescripcionGeneral;
                pgeneral.TieneProyectoDeAplicacion = Json.ProgramaGeneral.TieneProyectoDeAplicacion;
                pgeneral.IdTipoPrograma = Json.ProgramaGeneral.IdTipoPrograma;
                pgeneral.IdTipoPrograma = Json.ProgramaGeneral.IdTipoPrograma;
                pgeneral.CodigoPartner = Json.ProgramaGeneral.CodigoPartner;
                pgeneral.UsuarioCreacion = Json.Usuario;
                pgeneral.UsuarioModificacion = Json.Usuario;

                if (!string.IsNullOrEmpty(Json.ProgramaGeneral.LogoPrograma))
                {
                    pgeneral.LogoPrograma = Json.ProgramaGeneral.LogoPrograma;
                    pgeneral.UrlLogoPrograma = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/programas/logo/" + Json.ProgramaGeneral.LogoPrograma.Replace(" ", "%20");
                }

                var idPgeneral = repPrograma.InsertaPGeneralSinIdentity(pgeneral);
                PgeneralBO pgeneralInsertado = new PgeneralBO(idPgeneral, contexto);

                List<PgeneralParametroSeoPwBO> listaParametros = new List<PgeneralParametroSeoPwBO>();
                foreach (var item in Json.DetallesProgramaGeneral.ParametrosSeo)
                {
                    PgeneralParametroSeoPwBO parametro = new PgeneralParametroSeoPwBO();
                    parametro.Descripcion = item.Descripcion;
                    parametro.IdParametroSeo = item.IdParametroSEO;
                    parametro.UsuarioCreacion = Json.Usuario;
                    parametro.UsuarioModificacion = Json.Usuario;
                    parametro.FechaCreacion = DateTime.Now;
                    parametro.FechaModificacion = DateTime.Now;
                    parametro.Estado = true;
                    listaParametros.Add(parametro);
                }
                List<PgeneralDescripcionBO> listaDescripcion = new List<PgeneralDescripcionBO>();
                //foreach (var item in Json.DetallesProgramaGeneral.DescripcionesGenerales)
                //{
                //    PgeneralDescripcionBO descripcion = new PgeneralDescripcionBO();
                //    descripcion.Texto = item.Texto;
                //    descripcion.UsuarioCreacion = Json.Usuario;
                //    descripcion.UsuarioModificacion = Json.Usuario;
                //    descripcion.FechaCreacion = DateTime.Now;
                //    descripcion.FechaModificacion = DateTime.Now;
                //    descripcion.Estado = true;
                //    listaDescripcion.Add(descripcion);
                //}
                List<AdicionalProgramaGeneralBO> listaAdicionales = new List<AdicionalProgramaGeneralBO>();
                //foreach (var item in Json.DetallesProgramaGeneral.DescripcionesAdicionales)
                //{
                //    AdicionalProgramaGeneralBO adicional = new AdicionalProgramaGeneralBO();
                //    adicional.Descripcion = item.Descripcion;
                //    adicional.NombreImagen = item.NombreImagen;
                //    adicional.IdTitulo = item.IdTitulo;
                //    adicional.NombreTitulo = item.NombreTitulo;
                //    adicional.UsuarioCreacion = Json.Usuario;
                //    adicional.UsuarioModificacion = Json.Usuario;
                //    adicional.FechaCreacion = DateTime.Now;
                //    adicional.FechaModificacion = DateTime.Now;
                //    adicional.Estado = true;
                //    listaAdicionales.Add(adicional);
                //}
                List<PgeneralExpositorBO> listaExpositores = new List<PgeneralExpositorBO>();

                int posicion = 0;
                foreach (var item in Json.DetallesProgramaGeneral.Expositores)
                {
                    PgeneralExpositorBO expositor = new PgeneralExpositorBO();
                    expositor.IdExpositor = item;
                    expositor.Posicion = posicion++;
                    expositor.UsuarioCreacion = Json.Usuario;
                    expositor.UsuarioModificacion = Json.Usuario;
                    expositor.FechaCreacion = DateTime.Now;
                    expositor.FechaModificacion = DateTime.Now;
                    expositor.Estado = true;
                    listaExpositores.Add(expositor);
                }
                List<ModalidadCursoBO> listamodalidad = new List<ModalidadCursoBO>();
                foreach (var item in Json.DetallesProgramaGeneral.Modalidad)
                {
                    ModalidadCursoBO modalidadd = new ModalidadCursoBO();
                    modalidadd.Id = item;
                    modalidadd.UsuarioCreacion = Json.Usuario;
                    modalidadd.UsuarioModificacion = Json.Usuario;
                    modalidadd.FechaCreacion = DateTime.Now;
                    modalidadd.FechaModificacion = DateTime.Now;
                    modalidadd.Estado = true;
                    listamodalidad.Add(modalidadd);
                }

                List<ProgramaAreaRelacionadaBO> listaAreas = new List<ProgramaAreaRelacionadaBO>();
                foreach (var item in Json.DetallesProgramaGeneral.AreasRelacionadas)
                {
                    ProgramaAreaRelacionadaBO area = new ProgramaAreaRelacionadaBO();
                    area.IdAreaCapacitacion = item;
                    area.UsuarioCreacion = Json.Usuario;
                    area.UsuarioModificacion = Json.Usuario;
                    area.FechaCreacion = DateTime.Now;
                    area.FechaModificacion = DateTime.Now;
                    area.Estado = true;
                    listaAreas.Add(area);
                }
                List<SuscripcionProgramaGeneralBO> listaSuscripciones = new List<SuscripcionProgramaGeneralBO>();
                foreach (var item in Json.DetallesProgramaGeneral.Suscripciones)
                {
                    SuscripcionProgramaGeneralBO suscripcion = new SuscripcionProgramaGeneralBO();
                    suscripcion.Titulo = item.Titulo;
                    suscripcion.Descripcion = item.Descripcion;
                    suscripcion.OrdenBeneficio = item.OrdenBeneficio;
                    suscripcion.UsuarioCreacion = Json.Usuario;
                    suscripcion.UsuarioModificacion = Json.Usuario;
                    suscripcion.FechaCreacion = DateTime.Now;
                    suscripcion.FechaModificacion = DateTime.Now;
                    suscripcion.Estado = true;
                    listaSuscripciones.Add(suscripcion);
                }
                List<PgeneralModalidadBO> listaModalidades = new List<PgeneralModalidadBO>();//LPPG
                foreach (var item in Json.DetallesProgramaGeneral.Modalidad)
                {
                    PgeneralModalidadBO modalidad = new PgeneralModalidadBO();
                    //modalidad.IdPgeneral = idPgeneral;
                    modalidad.IdModalidadCurso = item;
                    modalidad.UsuarioCreacion = Json.Usuario;
                    modalidad.UsuarioModificacion = Json.Usuario;
                    modalidad.FechaCreacion = DateTime.Now;
                    modalidad.FechaModificacion = DateTime.Now;
                    modalidad.Estado = true;
                    listaModalidades.Add(modalidad);

                }
                List<PgeneralVersionProgramaBO> listaVersionPrograma = new List<PgeneralVersionProgramaBO>();
                foreach (var item in Json.DetallesProgramaGeneral.PgeneralVersionPrograma)
                {
                    PgeneralVersionProgramaBO versionPrograma = new PgeneralVersionProgramaBO();
                    versionPrograma.Duracion = item.Duracion;
                    versionPrograma.IdVersionPrograma = item.IdVersionPrograma;
                    versionPrograma.UsuarioCreacion = Json.Usuario;
                    versionPrograma.UsuarioModificacion = Json.Usuario;
                    versionPrograma.FechaCreacion = DateTime.Now;
                    versionPrograma.FechaModificacion = DateTime.Now;
                    versionPrograma.Estado = true;
                    listaVersionPrograma.Add(versionPrograma);
                }
                pgeneralInsertado.PGeneralParametroSeoPw = listaParametros;
                pgeneralInsertado.PgeneralDescripcion = listaDescripcion;
                pgeneralInsertado.AdicionalProgramaGeneral = listaAdicionales;
                pgeneralInsertado.ProgramaAreaRelacionada = listaAreas;
                pgeneralInsertado.PgeneralExpositor = listaExpositores;
                pgeneralInsertado.SuscripcionProgramaGeneral = listaSuscripciones;
                pgeneralInsertado.PgeneralModalidad = listaModalidades; //LPPG
                repPrograma.Update(pgeneralInsertado);

                //LPPG
                foreach (var item in Json.DetallesProgramaGeneral.Modalidad)
                {
                    PgeneralModalidadBO modalidad = new PgeneralModalidadBO();
                    //modalidad.IdPgeneral = idPgeneral;
                    modalidad.IdModalidadCurso = item;
                    modalidad.UsuarioCreacion = Json.Usuario;
                    modalidad.UsuarioModificacion = Json.Usuario;
                    modalidad.FechaCreacion = DateTime.Now;
                    modalidad.FechaModificacion = DateTime.Now;
                    modalidad.Estado = true;
                    _repPgeneralCEHijo.InsertarModalidadPGHIjo(modalidad);
                }



                foreach (var item in Json.DetallesProgramaGeneral.MontoPago)
                {
                    MontoPagoBO montoPago = new MontoPagoBO();
                    montoPago.Precio = item.Precio;
                    montoPago.PrecioLetras = item.PrecioLetras;
                    montoPago.IdMoneda = item.IdMoneda;
                    montoPago.Matricula = item.Matricula;
                    montoPago.Cuotas = item.Cuotas;
                    montoPago.NroCuotas = item.NroCuotas;
                    montoPago.IdTipoDescuento = item.IdTipoDescuento;
                    montoPago.IdPrograma = pgeneralInsertado.Id;
                    montoPago.IdTipoPago = item.IdTipoPago;
                    montoPago.IdPais = item.IdPais;
                    montoPago.Vencimiento = item.Vencimiento;
                    montoPago.PrimeraCuota = item.PrimeraCuota;
                    montoPago.CuotaDoble = item.CuotaDoble;
                    montoPago.Descripcion = item.Descripcion;
                    montoPago.VisibleWeb = item.VisibleWeb;
                    montoPago.Paquete = item.Paquete;
                    montoPago.PorDefecto = item.PorDefecto;
                    montoPago.MontoDescontado = item.MontoDescontado;
                    montoPago.UsuarioCreacion = Json.Usuario;
                    montoPago.UsuarioModificacion = Json.Usuario;
                    montoPago.FechaCreacion = DateTime.Now;
                    montoPago.FechaModificacion = DateTime.Now;
                    montoPago.Estado = true;

                    montoPago.MontoPagoPlataforma = new List<MontoPagoPlataformaBO>();
                    montoPago.MontoPagoSuscripcion = new List<MontoPagoSuscripcionBO>();

                    foreach (var item2 in item.PlataformasPagos)
                    {
                        MontoPagoPlataformaBO plataforma = new MontoPagoPlataformaBO();
                        plataforma.IdPlataformaPago = item2;
                        plataforma.UsuarioCreacion = Json.Usuario;
                        plataforma.UsuarioModificacion = Json.Usuario;
                        plataforma.FechaCreacion = DateTime.Now;
                        plataforma.FechaModificacion = DateTime.Now;
                        plataforma.Estado = true;

                        montoPago.MontoPagoPlataforma.Add(plataforma);
                    }
                    foreach (var item3 in item.SuscripcionesPagos)
                    {
                        MontoPagoSuscripcionBO suscripcion = new MontoPagoSuscripcionBO();
                        suscripcion.IdSuscripcionProgramaGeneral = item3;
                        suscripcion.UsuarioCreacion = Json.Usuario;
                        suscripcion.UsuarioModificacion = Json.Usuario;
                        suscripcion.FechaCreacion = DateTime.Now;
                        suscripcion.FechaModificacion = DateTime.Now;
                        suscripcion.Estado = true;

                        montoPago.MontoPagoSuscripcion.Add(suscripcion);
                    }
                    repMontoPago.Insert(montoPago);
                }
                foreach (var item in Json.DetallesProgramaGeneral.ConfiguracionPlantilla)
                {
                    PgeneralConfiguracionPlantillaBO plantillaCertificado = new PgeneralConfiguracionPlantillaBO();
                    plantillaCertificado.IdPgeneral = pgeneralInsertado.Id;
                    plantillaCertificado.IdPlantillaFrontal = item.IdPlantillaFrontal;
                    plantillaCertificado.IdPlantillaPosterior = item.IdPlantillaPosterior;
                    plantillaCertificado.UsuarioCreacion = Json.Usuario;
                    plantillaCertificado.UsuarioModificacion = Json.Usuario;
                    plantillaCertificado.FechaCreacion = DateTime.Now;
                    plantillaCertificado.FechaModificacion = DateTime.Now;
                    plantillaCertificado.Estado = true;
                    _repPgeneralConfiguracionPlantilla.Insert(plantillaCertificado);

                    foreach (var item2 in item.detalle)
                    {
                        PgeneralConfiguracionPlantillaDetalleBO plantillaCertificadoDetalle = new PgeneralConfiguracionPlantillaDetalleBO();
                        plantillaCertificadoDetalle.IdPgeneralConfiguracionPlantilla = plantillaCertificado.Id;
                        //plantillaCertificadoDetalle.IdEstadoMatricula = item2.IdEstadoMatricula;
                        plantillaCertificadoDetalle.IdOperadorComparacion = item2.IdOperadorComparacion;
                        plantillaCertificadoDetalle.IdModalidadCurso = item2.IdModalidadCurso;
                        plantillaCertificadoDetalle.NotaAprobatoria = item2.NotaAprobatoria;
                        plantillaCertificadoDetalle.DeudaPendiente = item2.DeudaPendiente;
                        plantillaCertificadoDetalle.UsuarioCreacion = Json.Usuario;
                        plantillaCertificadoDetalle.UsuarioModificacion = Json.Usuario;
                        plantillaCertificadoDetalle.FechaCreacion = DateTime.Now;
                        plantillaCertificadoDetalle.FechaModificacion = DateTime.Now;
                        plantillaCertificadoDetalle.Estado = true;
                        _repPgeneralConfiguracionPlantillaDetalle.Insert(plantillaCertificadoDetalle);
                    }
                }
                if (Json.DetallesProgramaGeneral.ConfiguracionPlantillaConstancia != null)
                {
                    foreach (var item in Json.DetallesProgramaGeneral.ConfiguracionPlantillaConstancia)
                    {
                        PgeneralConfiguracionPlantillaBO plantillaCertificadoConstancia = new PgeneralConfiguracionPlantillaBO();
                        plantillaCertificadoConstancia.IdPgeneral = pgeneralInsertado.Id;
                        plantillaCertificadoConstancia.IdPlantillaFrontal = item.IdPlantillaFrontal;
                        plantillaCertificadoConstancia.IdPlantillaPosterior = item.IdPlantillaPosterior;
                        plantillaCertificadoConstancia.UsuarioCreacion = Json.Usuario;
                        plantillaCertificadoConstancia.UsuarioModificacion = Json.Usuario;
                        plantillaCertificadoConstancia.FechaCreacion = DateTime.Now;
                        plantillaCertificadoConstancia.FechaModificacion = DateTime.Now;
                        plantillaCertificadoConstancia.Estado = true;
                        _repPgeneralConfiguracionPlantilla.Insert(plantillaCertificadoConstancia);

                        foreach (var item2 in item.detalle)
                        {
                            PgeneralConfiguracionPlantillaDetalleBO plantillaConstanciaDetalle = new PgeneralConfiguracionPlantillaDetalleBO();
                            plantillaConstanciaDetalle.IdPgeneralConfiguracionPlantilla = plantillaCertificadoConstancia.Id;
                            //plantillaConstanciaDetalle.IdEstadoMatricula = item2.IdEstadoMatricula;
                            plantillaConstanciaDetalle.IdOperadorComparacion = item2.IdOperadorComparacion;
                            plantillaConstanciaDetalle.IdModalidadCurso = item2.IdModalidadCurso;
                            plantillaConstanciaDetalle.NotaAprobatoria = item2.NotaAprobatoria;
                            plantillaConstanciaDetalle.UsuarioCreacion = Json.Usuario;
                            plantillaConstanciaDetalle.UsuarioModificacion = Json.Usuario;
                            plantillaConstanciaDetalle.FechaCreacion = DateTime.Now;
                            plantillaConstanciaDetalle.FechaModificacion = DateTime.Now;
                            plantillaConstanciaDetalle.Estado = true;
                            _repPgeneralConfiguracionPlantillaDetalle.Insert(plantillaConstanciaDetalle);
                        }
                    }
                }
                if (Json.DetallesProgramaGeneral.PgeneralCodigoPartner != null)
                {
                    foreach (var item in Json.DetallesProgramaGeneral.PgeneralCodigoPartner)
                    {
                        PgeneralCodigoPartnerBO pgeneralCodigoPartnerBO = new PgeneralCodigoPartnerBO();
                        pgeneralCodigoPartnerBO.IdPgeneral = pgeneralInsertado.Id;
                        pgeneralCodigoPartnerBO.Codigo = item.Codigo;
                        pgeneralCodigoPartnerBO.UsuarioCreacion = Json.Usuario;
                        pgeneralCodigoPartnerBO.UsuarioModificacion = Json.Usuario;
                        pgeneralCodigoPartnerBO.FechaCreacion = DateTime.Now;
                        pgeneralCodigoPartnerBO.FechaModificacion = DateTime.Now;
                        pgeneralCodigoPartnerBO.Estado = true;
                        _repPgeneralCodigoPartner.Insert(pgeneralCodigoPartnerBO);

                        foreach (var item2 in item.IdModalidadCurso)
                        {
                            PgeneralCodigoPartnerModalidadCursoBO pgeneralCodigoPartnerModalidadCursoBO = new PgeneralCodigoPartnerModalidadCursoBO();
                            pgeneralCodigoPartnerModalidadCursoBO.IdPgeneralCodigoPartner = pgeneralCodigoPartnerBO.Id;
                            pgeneralCodigoPartnerModalidadCursoBO.IdModalidadCurso = item2.Id.Value;
                            pgeneralCodigoPartnerModalidadCursoBO.UsuarioCreacion = Json.Usuario;
                            pgeneralCodigoPartnerModalidadCursoBO.UsuarioModificacion = Json.Usuario;
                            pgeneralCodigoPartnerModalidadCursoBO.FechaCreacion = DateTime.Now;
                            pgeneralCodigoPartnerModalidadCursoBO.FechaModificacion = DateTime.Now;
                            pgeneralCodigoPartnerModalidadCursoBO.Estado = true;
                            _repPgeneralCodigoPartnerModalidadCurso.Insert(pgeneralCodigoPartnerModalidadCursoBO);
                        }
                        foreach (var item2 in item.IdVersionPrograma)
                        {
                            PgeneralCodigoPartnerVersionProgramaBO pgeneralCodigoPartnerVersionProgramaBO = new PgeneralCodigoPartnerVersionProgramaBO();
                            pgeneralCodigoPartnerVersionProgramaBO.IdPgeneralCodigoPartner = pgeneralCodigoPartnerBO.Id;
                            pgeneralCodigoPartnerVersionProgramaBO.IdVersionPrograma = item2.Id;
                            pgeneralCodigoPartnerVersionProgramaBO.UsuarioCreacion = Json.Usuario;
                            pgeneralCodigoPartnerVersionProgramaBO.UsuarioModificacion = Json.Usuario;
                            pgeneralCodigoPartnerVersionProgramaBO.FechaCreacion = DateTime.Now;
                            pgeneralCodigoPartnerVersionProgramaBO.FechaModificacion = DateTime.Now;
                            pgeneralCodigoPartnerVersionProgramaBO.Estado = true;
                            _repPgeneralCodigoPartnerVersionPrograma.Insert(pgeneralCodigoPartnerVersionProgramaBO);
                        }
                    }
                }

                if (Json.DetallesProgramaGeneral.pgeneralProyectoAplicacion != null)
                {
                    foreach (var item in Json.DetallesProgramaGeneral.pgeneralProyectoAplicacion)
                    {
                        PgeneralProyectoAplicacionBO proyectoAplicacionBO = new PgeneralProyectoAplicacionBO();
                        proyectoAplicacionBO.IdPgeneral = pgeneralInsertado.Id;
                        proyectoAplicacionBO.UsuarioCreacion = Json.Usuario;
                        proyectoAplicacionBO.UsuarioModificacion = Json.Usuario;
                        proyectoAplicacionBO.FechaCreacion = DateTime.Now;
                        proyectoAplicacionBO.FechaModificacion = DateTime.Now;
                        proyectoAplicacionBO.Estado = true;
                        _repPgeneralProyectoAplicacion.Insert(proyectoAplicacionBO);

                        foreach (var item2 in item.IdProveedor)
                        {
                            PgeneralProyectoAplicacionProveedorBO pgeneralProyectoAplicacionProveedorBO = new PgeneralProyectoAplicacionProveedorBO();
                            pgeneralProyectoAplicacionProveedorBO.IdPgeneralProyectoAplicacion = proyectoAplicacionBO.Id;
                            pgeneralProyectoAplicacionProveedorBO.IdProveedor = item2.Id;
                            pgeneralProyectoAplicacionProveedorBO.UsuarioCreacion = Json.Usuario;
                            pgeneralProyectoAplicacionProveedorBO.UsuarioModificacion = Json.Usuario;
                            pgeneralProyectoAplicacionProveedorBO.FechaCreacion = DateTime.Now;
                            pgeneralProyectoAplicacionProveedorBO.FechaModificacion = DateTime.Now;
                            pgeneralProyectoAplicacionProveedorBO.Estado = true;
                            _repPgeneralProyectoAplicacionProveedor.Insert(pgeneralProyectoAplicacionProveedorBO);
                        }
                        foreach (var item2 in item.IdModalidadCurso)
                        {
                            PgeneralProyectoAplicacionModalidadBO pgeneralProyectoAplicacionModalidadBO = new PgeneralProyectoAplicacionModalidadBO();
                            pgeneralProyectoAplicacionModalidadBO.IdPgeneralProyectoAplicacion = proyectoAplicacionBO.Id;
                            pgeneralProyectoAplicacionModalidadBO.IdModalidadCurso = item2.Id;
                            pgeneralProyectoAplicacionModalidadBO.UsuarioCreacion = Json.Usuario;
                            pgeneralProyectoAplicacionModalidadBO.UsuarioModificacion = Json.Usuario;
                            pgeneralProyectoAplicacionModalidadBO.FechaCreacion = DateTime.Now;
                            pgeneralProyectoAplicacionModalidadBO.FechaModificacion = DateTime.Now;
                            pgeneralProyectoAplicacionModalidadBO.Estado = true;
                            _repPgeneralProyectoAplicacionModalidad.Insert(pgeneralProyectoAplicacionModalidadBO);
                        }
                    }
                }
                if (Json.DetallesProgramaGeneral.PGeneralForoAsignacionProveedor != null && Json.DetallesProgramaGeneral.PGeneralForoAsignacionProveedor.Count > 0)
                {
                    //Validación para evitar la duplicidad de datos
                    List<PGeneralForoAsignacionProveedorAuxiliarDTO> listaAuxiliarForoAsignacionProveedor = new List<PGeneralForoAsignacionProveedorAuxiliarDTO>();
                    PGeneralForoAsignacionProveedorAuxiliarDTO agregar;
                    PGeneralForoAsignacionProveedorBO nuevoRegistro;
                    //Armamos una lista de datos lineal para distinción de proveedores y modalidades
                    foreach (var configuracionForoAsignacionProveedor in Json.DetallesProgramaGeneral.PGeneralForoAsignacionProveedor)
                    {
                        foreach (var proveedor in configuracionForoAsignacionProveedor.IdProveedor)
                        {
                            agregar = new PGeneralForoAsignacionProveedorAuxiliarDTO()
                            {
                                IdPgeneral = idPgeneral,
                                IdModalidadCurso = configuracionForoAsignacionProveedor.IdModalidadCurso,
                                IdProveedor = proveedor
                            };
                            listaAuxiliarForoAsignacionProveedor.Add(agregar);
                        }
                    }
                    if (listaAuxiliarForoAsignacionProveedor.Count > 0)
                    {
                        //Se distingue lista para evitar repetición de registros
                        List<int> listaIdProveedor = new List<int>();
                        var listaAuxiliarForoAsignacionProveedorSinRepeticiones = listaAuxiliarForoAsignacionProveedor.Select(x => new { x.IdModalidadCurso, x.IdPgeneral, x.IdProveedor }).Distinct().ToList();
                        foreach (var registro in listaAuxiliarForoAsignacionProveedorSinRepeticiones)
                        {
                            listaIdProveedor.Add(registro.IdProveedor);
                            nuevoRegistro = new PGeneralForoAsignacionProveedorBO()
                            {
                                IdPgeneral = registro.IdPgeneral,
                                IdModalidadCurso = registro.IdModalidadCurso,
                                IdProveedor = registro.IdProveedor,
                                Estado = true,
                                UsuarioCreacion = Json.Usuario,
                                UsuarioModificacion = Json.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPGeneralForoAsignacionProveedor.Insert(nuevoRegistro);
                        }
                    }
                }
                return Ok(pgeneralInsertado);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 26/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Información de programa General
        /// </summary>
        /// <param name="Json">Información Compuesta de Programa General</param>
        /// <returns>PgeneralBO</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarProgramaGeneral([FromBody] DatosProgramaGeneralDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PartnerPwRepositorio repPartner = new PartnerPwRepositorio();
                AreaCapacitacionRepositorio repArea = new AreaCapacitacionRepositorio();
                SubAreaCapacitacionRepositorio repSubArea = new SubAreaCapacitacionRepositorio();
                TroncalPgeneralRepositorio repTroncalPgeneral = new TroncalPgeneralRepositorio();

                PGeneralParametroSeoPwRepositorio repParametro = new PGeneralParametroSeoPwRepositorio();
                PgeneralDescripcionRepositorio repDescripcion = new PgeneralDescripcionRepositorio();
                AdicionalProgramaGeneralRepositorio repAdicional = new AdicionalProgramaGeneralRepositorio();
                PgeneralExpositorRepositorio repExpositor = new PgeneralExpositorRepositorio();
                ProgramaAreaRelacionadaRepositorio repAreaRelacionada = new ProgramaAreaRelacionadaRepositorio();
                SuscripcionProgramaGeneralRepositorio repSuscripciones = new SuscripcionProgramaGeneralRepositorio();
                CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio();

                MontoPagoRepositorio repMontoPago = new MontoPagoRepositorio(contexto);
                MontoPagoPlataformaRepositorio repPlataforma = new MontoPagoPlataformaRepositorio(contexto);
                MontoPagoSuscripcionRepositorio repSuscripcion = new MontoPagoSuscripcionRepositorio(contexto);

                PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(contexto);
                PgeneralConfiguracionPlantillaDetalleRepositorio _repPgeneralConfiguracionPlantillaDetalle = new PgeneralConfiguracionPlantillaDetalleRepositorio(contexto);

                PgeneralConfiguracionPlantillaEstadoMatriculaRepositorio _repPgeneralconfiguracionPlantillaEstadoMatricula = new PgeneralConfiguracionPlantillaEstadoMatriculaRepositorio(contexto);
                PgeneralConfiguracionPlantillaSubEstadoMatriculaRepositorio _repPgeneralConfiguracionPlantillaSubEstadoMatricula = new PgeneralConfiguracionPlantillaSubEstadoMatriculaRepositorio(contexto);

                PgeneralConfiguracionBeneficioRepositorio _repPgeneralConfiguracionBeneficio = new PgeneralConfiguracionBeneficioRepositorio(contexto);
                ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepositorio _repConfiguracionBeneficioEstado = new ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepositorio();
                ConfiguracionBeneficioProgramaGeneralSubEstadoRepositorio _repConfiguracionBeneficioSubEstado = new ConfiguracionBeneficioProgramaGeneralSubEstadoRepositorio();
                ConfiguracionBeneficioProgramaGeneralPaisRepositorio _repConfiguracionBeneficioPais = new ConfiguracionBeneficioProgramaGeneralPaisRepositorio();
                ConfiguracionBeneficioProgramaGeneralVersionRepositorio _repConfiguracionBeneficioVersion = new ConfiguracionBeneficioProgramaGeneralVersionRepositorio();
                ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepositorio _repConfiguracionBeneficioDatoAdicional = new ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepositorio();
                PgeneralModalidadRepositorio _repPgeneralModalidad = new PgeneralModalidadRepositorio(contexto);

                PGeneralCriterioEvaluacionHijoRepositorio _repPgeneralCEHijo = new PGeneralCriterioEvaluacionHijoRepositorio(contexto);//LPPG
                PGeneralCriterioEvaluacionRepositorio _repPgeneralCriterioEvaluacion = new PGeneralCriterioEvaluacionRepositorio(contexto);//LPPG
                PgeneralVersionProgramaRepositorio _repPgeneralVersionPrograma = new PgeneralVersionProgramaRepositorio(contexto);

                PgeneralCodigoPartnerRepositorio _repPgeneralCodigoPartner = new PgeneralCodigoPartnerRepositorio(contexto);
                PgeneralCodigoPartnerVersionProgramaRepositorio _repPgeneralCodigoPartnerVersionPrograma = new PgeneralCodigoPartnerVersionProgramaRepositorio(contexto);
                PgeneralCodigoPartnerModalidadCursoRepositorio _repPgeneralCodigoPartnerModalidadCurso = new PgeneralCodigoPartnerModalidadCursoRepositorio(contexto);

                PgeneralProyectoAplicacionRepositorio _repPgeneralProyectoAplicacion = new PgeneralProyectoAplicacionRepositorio(contexto);
                PgeneralProyectoAplicacionModalidadRepositorio _repPgeneralProyectoAplicacionModalidad = new PgeneralProyectoAplicacionModalidadRepositorio(contexto);
                PgeneralProyectoAplicacionProveedorRepositorio _repPgeneralProyectoAplicacionProveedor = new PgeneralProyectoAplicacionProveedorRepositorio(contexto);

                PgeneralRepositorio repPrograma = new PgeneralRepositorio();

                TroncalPgeneralBO troncal = new TroncalPgeneralBO(Json.ProgramaGeneral.IdPgeneral);
                troncal.IdTroncalPartner = repPartner.ObtenerPartnerAnterior(Json.ProgramaGeneral.IdPartner);
                troncal.IdArea = repArea.ObtenerAreaCapacitacionAnterior(Json.ProgramaGeneral.IdArea);
                troncal.IdSubArea = repSubArea.ObtenerSubAreaCapacitacionAnterior(Json.ProgramaGeneral.IdSubArea);
                troncal.Codigo = Json.ProgramaGeneral.Codigo;
                troncal.Nombre = Json.ProgramaGeneral.Nombre;
                troncal.FechaModificacion = DateTime.Now;
                troncal.UsuarioModificacion = Json.Usuario;

                repTroncalPgeneral.Update(troncal);

                PgeneralBO pgeneral = new PgeneralBO(Json.ProgramaGeneral.IdPgeneral, contexto);
                pgeneral.IdPgeneral = Json.ProgramaGeneral.IdPgeneral;
                pgeneral.Nombre = Json.ProgramaGeneral.Nombre;
                pgeneral.PwImgPortada = Json.ProgramaGeneral.PwImgPortada;
                pgeneral.PwImgPortadaAlf = Json.ProgramaGeneral.PwImgPortadaAlf;
                //pgeneral.PwImgSecundaria = Json.ProgramaGeneral.PwImgSecundaria;
                //pgeneral.PwImgSecundariaAlf = Json.ProgramaGeneral.PwImgSecundariaAlf;
                pgeneral.IdPartner = Json.ProgramaGeneral.IdPartner;
                pgeneral.IdArea = Json.ProgramaGeneral.IdArea;
                pgeneral.IdSubArea = Json.ProgramaGeneral.IdSubArea;
                pgeneral.IdCategoria = Json.ProgramaGeneral.IdCategoria;
                pgeneral.PwEstado = Json.ProgramaGeneral.PwEstado;
                pgeneral.PwMostrarBsplay = Json.ProgramaGeneral.PwMostrarBsplay;
                pgeneral.PwDuracion = Json.ProgramaGeneral.PwDuracion;
                pgeneral.IdBusqueda = Json.ProgramaGeneral.IdBusqueda;
                pgeneral.PgTitulo = Json.ProgramaGeneral.PgTitulo;
                pgeneral.Codigo = Json.ProgramaGeneral.Codigo;
                //pgeneral.UrlImagenPortadaFr = Json.ProgramaGeneral.UrlImagenPortadaFr;
                pgeneral.UrlBrochurePrograma = Json.ProgramaGeneral.UrlBrochurePrograma;
                //pgeneral.UrlPartner = Json.ProgramaGeneral.UrlPartner;
                //pgeneral.UrlVersion = Json.ProgramaGeneral.UrlVersion;
                pgeneral.PwTituloHtml = Json.ProgramaGeneral.PwTituloHtml;
                pgeneral.EsModulo = Json.ProgramaGeneral.EsModulo;
                pgeneral.NombreCorto = Json.ProgramaGeneral.NombreCorto;
                pgeneral.IdPagina = Json.ProgramaGeneral.IdPagina;
                pgeneral.ChatActivo = Json.ProgramaGeneral.ChatActivo;
                pgeneral.PwDescripcionGeneral = Json.ProgramaGeneral.PwDescripcionGeneral;
                pgeneral.TieneProyectoDeAplicacion = Json.ProgramaGeneral.TieneProyectoDeAplicacion;
                pgeneral.IdTipoPrograma = Json.ProgramaGeneral.IdTipoPrograma;
                pgeneral.CodigoPartner = Json.ProgramaGeneral.CodigoPartner == "" ? null : Json.ProgramaGeneral.CodigoPartner;
                pgeneral.UsuarioModificacion = Json.Usuario;
                pgeneral.FechaModificacion = DateTime.Now;

                if (!string.IsNullOrEmpty(Json.ProgramaGeneral.LogoPrograma))
                {
                    pgeneral.LogoPrograma = Json.ProgramaGeneral.LogoPrograma;
                    pgeneral.UrlLogoPrograma = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/programas/logo/" + Json.ProgramaGeneral.LogoPrograma.Replace(" ", "%20");
                }

                repParametro.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.ParametrosSeo);
                _repPgeneralVersionPrograma.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.PgeneralVersionPrograma);
                //repDescripcion.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.DescripcionesGenerales);
                //repAdicional.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.DescripcionesAdicionales);
                repExpositor.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.Expositores);
                repAreaRelacionada.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.AreasRelacionadas);
                repSuscripciones.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.Suscripciones);
                _repPgeneralConfiguracionPlantilla.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.ConfiguracionPlantilla, 12);
                _repPgeneralConfiguracionPlantilla.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.ConfiguracionPlantillaConstancia, 13);

                _repPgeneralCodigoPartner.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.PgeneralCodigoPartner);
                _repPgeneralProyectoAplicacion.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.pgeneralProyectoAplicacion);

                List<PgeneralParametroSeoPwBO> listaParametros = new List<PgeneralParametroSeoPwBO>();
                foreach (var item in Json.DetallesProgramaGeneral.ParametrosSeo)
                {
                    PgeneralParametroSeoPwBO parametro;
                    if (repParametro.Exist(item.IdPGeneralParametroSEO))
                    {
                        parametro = repParametro.FirstById(item.IdPGeneralParametroSEO);
                        parametro.Descripcion = item.Descripcion;
                        parametro.IdParametroSeo = parametro.IdParametroSeo;
                        parametro.UsuarioModificacion = Json.Usuario;
                        parametro.FechaModificacion = DateTime.Now;
                        parametro.Estado = true;
                    }
                    else
                    {
                        parametro = new PgeneralParametroSeoPwBO();
                        parametro.Descripcion = item.Descripcion;
                        parametro.IdParametroSeo = item.IdParametroSEO;
                        parametro.UsuarioCreacion = Json.Usuario;
                        parametro.UsuarioModificacion = Json.Usuario;
                        parametro.FechaCreacion = DateTime.Now;
                        parametro.FechaModificacion = DateTime.Now;
                        parametro.Estado = true;
                    }
                    listaParametros.Add(parametro);
                }
                List<PgeneralDescripcionBO> listaDescripcion = new List<PgeneralDescripcionBO>();
                //foreach (var item in Json.DetallesProgramaGeneral.DescripcionesGenerales)
                //{
                //    PgeneralDescripcionBO descripcion;
                //    if (repDescripcion.Exist(item.Id))
                //    {
                //        descripcion = repDescripcion.FirstById(item.Id);
                //        descripcion.Texto = item.Texto;
                //        descripcion.UsuarioModificacion = Json.Usuario;
                //        descripcion.FechaModificacion = DateTime.Now;
                //        descripcion.Estado = true;
                //    }
                //    else {
                //        descripcion = new PgeneralDescripcionBO();
                //        descripcion.Texto = item.Texto;
                //        descripcion.UsuarioCreacion = Json.Usuario;
                //        descripcion.UsuarioModificacion = Json.Usuario;
                //        descripcion.FechaCreacion = DateTime.Now;
                //        descripcion.FechaModificacion = DateTime.Now;
                //        descripcion.Estado = true;
                //    }
                //    listaDescripcion.Add(descripcion);
                //}
                List<AdicionalProgramaGeneralBO> listaAdicionales = new List<AdicionalProgramaGeneralBO>();
                //foreach (var item in Json.DetallesProgramaGeneral.DescripcionesAdicionales)
                //{
                //    AdicionalProgramaGeneralBO adicional;
                //    if (repAdicional.Exist(item.Id))
                //    {
                //        adicional = repAdicional.FirstById(item.Id);
                //        adicional.Descripcion = item.Descripcion;
                //        adicional.NombreImagen = item.NombreImagen;
                //        adicional.IdTitulo = item.IdTitulo;
                //        adicional.NombreTitulo = item.NombreTitulo;
                //        adicional.UsuarioModificacion = Json.Usuario;
                //        adicional.FechaModificacion = DateTime.Now;
                //        adicional.Estado = true;
                //    }
                //    else
                //    {
                //        adicional = new AdicionalProgramaGeneralBO();
                //        adicional.Descripcion = item.Descripcion;
                //        adicional.NombreImagen = item.NombreImagen;
                //        adicional.IdTitulo = item.IdTitulo;
                //        adicional.NombreTitulo = item.NombreTitulo;
                //        adicional.UsuarioCreacion = Json.Usuario;
                //        adicional.UsuarioModificacion = Json.Usuario;
                //        adicional.FechaCreacion = DateTime.Now;
                //        adicional.FechaModificacion = DateTime.Now;
                //        adicional.Estado = true;
                //    }
                //    listaAdicionales.Add(adicional);
                //}



                List<PgeneralExpositorBO> listaExpositores = new List<PgeneralExpositorBO>();
                int posicion = 0;
                foreach (var item in Json.DetallesProgramaGeneral.Expositores)
                {
                    PgeneralExpositorBO expositor;
                    expositor = repExpositor.FirstBy(x => x.IdExpositor == item && x.IdPgeneral == Json.ProgramaGeneral.IdPgeneral);
                    if (expositor != null)
                    {
                        expositor.IdExpositor = item;
                        expositor.Posicion = posicion++;
                        expositor.UsuarioModificacion = Json.Usuario;
                        expositor.FechaModificacion = DateTime.Now;
                        expositor.Estado = true;
                    }
                    else
                    {
                        expositor = new PgeneralExpositorBO();
                        expositor.IdExpositor = item;
                        expositor.Posicion = posicion++;
                        expositor.UsuarioCreacion = Json.Usuario;
                        expositor.UsuarioModificacion = Json.Usuario;
                        expositor.FechaCreacion = DateTime.Now;
                        expositor.FechaModificacion = DateTime.Now;
                        expositor.Estado = true;
                    }
                    listaExpositores.Add(expositor);
                }



                List<ProgramaAreaRelacionadaBO> listaAreas = new List<ProgramaAreaRelacionadaBO>();
                foreach (var item in Json.DetallesProgramaGeneral.AreasRelacionadas)
                {
                    ProgramaAreaRelacionadaBO area;
                    area = repAreaRelacionada.FirstBy(x => x.IdAreaCapacitacion == item && x.IdPgeneral == Json.ProgramaGeneral.IdPgeneral);
                    if (area != null)
                    {
                        area.IdAreaCapacitacion = item;
                        area.UsuarioModificacion = Json.Usuario;
                        area.FechaModificacion = DateTime.Now;
                        area.Estado = true;
                    }
                    else
                    {
                        area = new ProgramaAreaRelacionadaBO();
                        area.IdAreaCapacitacion = item;
                        area.UsuarioCreacion = Json.Usuario;
                        area.UsuarioModificacion = Json.Usuario;
                        area.FechaCreacion = DateTime.Now;
                        area.FechaModificacion = DateTime.Now;
                        area.Estado = true;
                    }


                    listaAreas.Add(area);
                }

                List<SuscripcionProgramaGeneralBO> listaSuscripciones = new List<SuscripcionProgramaGeneralBO>();
                foreach (var item in Json.DetallesProgramaGeneral.Suscripciones)
                {
                    SuscripcionProgramaGeneralBO suscripcion;
                    if (repSuscripciones.Exist(item.Id))
                    {
                        suscripcion = repSuscripciones.FirstById(item.Id);
                        suscripcion.Titulo = item.Titulo;
                        suscripcion.Descripcion = item.Descripcion;
                        suscripcion.OrdenBeneficio = item.OrdenBeneficio;
                        suscripcion.UsuarioModificacion = Json.Usuario;
                        suscripcion.FechaModificacion = DateTime.Now;
                        suscripcion.Estado = true;
                    }
                    else
                    {
                        suscripcion = new SuscripcionProgramaGeneralBO();
                        suscripcion.Titulo = item.Titulo;
                        suscripcion.Descripcion = item.Descripcion;
                        suscripcion.OrdenBeneficio = item.OrdenBeneficio;
                        suscripcion.UsuarioCreacion = Json.Usuario;
                        suscripcion.UsuarioModificacion = Json.Usuario;
                        suscripcion.FechaCreacion = DateTime.Now;
                        suscripcion.FechaModificacion = DateTime.Now;
                        suscripcion.Estado = true;
                    }
                    listaSuscripciones.Add(suscripcion);
                }

                _repPgeneralModalidad.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.Modalidad);
                List<PgeneralModalidadBO> listamodalidades = new List<PgeneralModalidadBO>();

                foreach (var item in Json.DetallesProgramaGeneral.Modalidad)
                {

                    PgeneralModalidadBO modalidad;
                    modalidad = _repPgeneralModalidad.FirstBy(x => x.IdModalidadCurso == item && x.IdPgeneral == Json.ProgramaGeneral.IdPgeneral);
                    if (modalidad != null)
                    {
                        modalidad = new PgeneralModalidadBO();
                        modalidad.IdModalidadCurso = item;
                        modalidad.UsuarioModificacion = Json.Usuario;
                        modalidad.FechaModificacion = DateTime.Now;
                        modalidad.Estado = true;
                    }
                    else
                    {
                        modalidad = new PgeneralModalidadBO();
                        modalidad.IdModalidadCurso = item;
                        modalidad.UsuarioCreacion = Json.Usuario;
                        modalidad.UsuarioModificacion = Json.Usuario;
                        modalidad.FechaCreacion = DateTime.Now;
                        modalidad.FechaModificacion = DateTime.Now;
                        modalidad.Estado = true;
                    }
                    listamodalidades.Add(modalidad);
                }

                List<PgeneralVersionProgramaBO> listaVersiones = new List<PgeneralVersionProgramaBO>();
                foreach (var item in Json.DetallesProgramaGeneral.PgeneralVersionPrograma)
                {
                    PgeneralVersionProgramaBO pgeneralVersionProgramaBO;
                    if (_repPgeneralVersionPrograma.Exist(item.IdPgeneralVersionPrograma ?? default(int)))
                    {
                        pgeneralVersionProgramaBO = _repPgeneralVersionPrograma.FirstById(item.IdPgeneralVersionPrograma ?? default(int));
                        pgeneralVersionProgramaBO.Duracion = item.Duracion;
                        pgeneralVersionProgramaBO.IdVersionPrograma = pgeneralVersionProgramaBO.IdVersionPrograma;
                        pgeneralVersionProgramaBO.UsuarioModificacion = Json.Usuario;
                        pgeneralVersionProgramaBO.FechaModificacion = DateTime.Now;
                        pgeneralVersionProgramaBO.Estado = true;
                    }
                    else
                    {
                        pgeneralVersionProgramaBO = new PgeneralVersionProgramaBO();
                        pgeneralVersionProgramaBO.Duracion = item.Duracion;
                        pgeneralVersionProgramaBO.IdVersionPrograma = item.IdVersionPrograma;
                        pgeneralVersionProgramaBO.UsuarioCreacion = Json.Usuario;
                        pgeneralVersionProgramaBO.UsuarioModificacion = Json.Usuario;
                        pgeneralVersionProgramaBO.FechaCreacion = DateTime.Now;
                        pgeneralVersionProgramaBO.FechaModificacion = DateTime.Now;
                        pgeneralVersionProgramaBO.Estado = true;
                    }
                    listaVersiones.Add(pgeneralVersionProgramaBO);
                }


                pgeneral.PGeneralParametroSeoPw = listaParametros;
                pgeneral.PgeneralDescripcion = listaDescripcion;
                pgeneral.AdicionalProgramaGeneral = listaAdicionales;
                pgeneral.ProgramaAreaRelacionada = listaAreas;
                pgeneral.PgeneralExpositor = listaExpositores;
                pgeneral.SuscripcionProgramaGeneral = listaSuscripciones;
                pgeneral.PgeneralModalidad = listamodalidades;
                pgeneral.PgeneralVersionPrograma = listaVersiones;

                repPrograma.Update(pgeneral);

                //LPPG
                //_repPgeneralCEHijo.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, Json.DetallesProgramaGeneral.Modalidad);

                PGeneralCriterioEvaluacionHijoBO pgeneralcriterioevaluacion = new PGeneralCriterioEvaluacionHijoBO();
                List<PCriterioGeneralHijoModalidadDTO> listamodalidadhijo;
                List<PgeneralcriterioEvaluacionModalidadDTO> listModalidadCriterio;
                listamodalidadhijo = _repPgeneralCEHijo.ListarCriteriosEvaluacionHijo(Json.ProgramaGeneral.IdPgeneral);//Aqui estamos recuperando que modalidades tiene el curso en la tabla de hijos
                listModalidadCriterio = _repPgeneralCriterioEvaluacion.ListarCriteriosEvaluacion(Json.ProgramaGeneral.IdPgeneral);

                foreach (var item in Json.DetallesProgramaGeneral.Modalidad)
                {
                    if (!listamodalidadhijo.Exists(w => w.IdModalidadCurso == item && w.IdPgeneral == Json.ProgramaGeneral.IdPgeneral))//&& listamodalidadhijo.Count > 0
                    {
                        PgeneralModalidadBO modalidad;
                        modalidad = new PgeneralModalidadBO();
                        modalidad.IdPgeneral = Json.ProgramaGeneral.IdPgeneral;
                        modalidad.IdModalidadCurso = item;
                        modalidad.UsuarioCreacion = Json.Usuario;
                        modalidad.UsuarioModificacion = Json.Usuario;
                        modalidad.FechaCreacion = DateTime.Now;
                        modalidad.FechaModificacion = DateTime.Now;
                        modalidad.Estado = true;
                        _repPgeneralCEHijo.InsertarModalidadPGHIjo(modalidad);
                    }
                    else
                    {
                        listamodalidadhijo.RemoveAll(w => w.IdModalidadCurso == item);

                    }
                }

                foreach (var item in listamodalidadhijo)
                {
                    _repPgeneralCEHijo.EliminarLogico(Json.ProgramaGeneral.IdPgeneral, item.IdModalidadCurso);
                    //_repPgeneralCriterioEvaluacion.EliminarLogico(Json.ProgramaGeneral.IdPgeneral, item.IdModalidadCurso);

                }

                foreach (var item in Json.DetallesProgramaGeneral.Modalidad)
                {
                    if (listModalidadCriterio.Exists(w => w.IdModalidadCurso == item && w.IdPgeneral == Json.ProgramaGeneral.IdPgeneral))
                    {
                        listModalidadCriterio.RemoveAll(w => w.IdModalidadCurso == item);
                    }
                }
                foreach (var item in listModalidadCriterio)
                {
                    _repPgeneralCriterioEvaluacion.EliminarLogico(Json.ProgramaGeneral.IdPgeneral, item.IdModalidadCurso);
                }

                ///

                foreach (var item in Json.DetallesProgramaGeneral.MontoPago)
                {
                    MontoPagoBO montoPago = new MontoPagoBO();
                    repPlataforma.EliminacionLogicoPorMontoPago(pgeneral.Id, Json.Usuario, item.PlataformasPagos);
                    repSuscripcion.EliminacionLogicoPorMontoPago(pgeneral.Id, Json.Usuario, item.SuscripcionesPagos);
                    if (repMontoPago.Exist(x => x.Id == item.Id))
                    {
                        montoPago = repMontoPago.FirstById(item.Id);

                        montoPago.Precio = item.Precio;
                        montoPago.PrecioLetras = item.PrecioLetras;
                        montoPago.IdMoneda = item.IdMoneda;
                        montoPago.Matricula = item.Matricula;
                        montoPago.Cuotas = item.Cuotas;
                        montoPago.NroCuotas = item.NroCuotas;
                        montoPago.IdTipoDescuento = item.IdTipoDescuento;
                        montoPago.IdPrograma = item.IdPrograma;
                        montoPago.IdTipoPago = item.IdTipoPago;
                        montoPago.IdPais = item.IdPais;
                        montoPago.Vencimiento = item.Vencimiento;
                        montoPago.PrimeraCuota = item.PrimeraCuota;
                        montoPago.CuotaDoble = item.CuotaDoble;
                        montoPago.Descripcion = item.Descripcion;
                        montoPago.VisibleWeb = item.VisibleWeb;
                        montoPago.Paquete = item.Paquete;
                        montoPago.PorDefecto = item.PorDefecto;
                        montoPago.MontoDescontado = item.MontoDescontado;
                        montoPago.UsuarioModificacion = Json.Usuario;
                        montoPago.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        montoPago.Precio = item.Precio;
                        montoPago.PrecioLetras = item.PrecioLetras;
                        montoPago.IdMoneda = item.IdMoneda;
                        montoPago.Matricula = item.Matricula;
                        montoPago.Cuotas = item.Cuotas;
                        montoPago.NroCuotas = item.NroCuotas;
                        montoPago.IdTipoDescuento = item.IdTipoDescuento;
                        montoPago.IdPrograma = item.IdPrograma;
                        montoPago.IdTipoPago = item.IdTipoPago;
                        montoPago.IdPais = item.IdPais;
                        montoPago.Vencimiento = item.Vencimiento;
                        montoPago.PrimeraCuota = item.PrimeraCuota;
                        montoPago.CuotaDoble = item.CuotaDoble;
                        montoPago.Descripcion = item.Descripcion;
                        montoPago.VisibleWeb = item.VisibleWeb;
                        montoPago.Paquete = item.Paquete;
                        montoPago.PorDefecto = item.PorDefecto;
                        montoPago.MontoDescontado = item.MontoDescontado;
                        montoPago.Estado = true;
                        montoPago.UsuarioModificacion = Json.Usuario;
                        montoPago.UsuarioCreacion = Json.Usuario;
                        montoPago.FechaModificacion = DateTime.Now;
                        montoPago.FechaCreacion = DateTime.Now;
                    }


                    montoPago.MontoPagoPlataforma = new List<MontoPagoPlataformaBO>();
                    //montoPago.MontoPagoSuscripcion = new List<MontoPagoSuscripcionBO>();
                    foreach (var item2 in item.PlataformasPagos)
                    {
                        MontoPagoPlataformaBO plataforma;
                        if (repPlataforma.Exist(x => x.IdPlataformaPago == item2 && x.IdMontoPago == item.Id))
                        {
                            plataforma = repPlataforma.FirstBy(x => x.IdPlataformaPago == item2 && x.IdMontoPago == item.Id);
                            plataforma.IdPlataformaPago = item2;
                            plataforma.UsuarioModificacion = Json.Usuario;
                            plataforma.FechaModificacion = DateTime.Now;

                        }
                        else
                        {
                            plataforma = new MontoPagoPlataformaBO();
                            plataforma.IdPlataformaPago = item2;
                            plataforma.UsuarioCreacion = Json.Usuario;
                            plataforma.UsuarioModificacion = Json.Usuario;
                            plataforma.FechaCreacion = DateTime.Now;
                            plataforma.FechaModificacion = DateTime.Now;
                            plataforma.Estado = true;
                        }
                        montoPago.MontoPagoPlataforma.Add(plataforma);
                    }
                    //foreach (var item3 in item.SuscripcionesPagos)
                    //{
                    //    MontoPagoSuscripcionBO suscripcion;
                    //    if (repSuscripcion.Exist(x => x.IdSuscripcionProgramaGeneral == item3 && x.IdMontoPago == item.Id))
                    //    {
                    //        suscripcion = repSuscripcion.FirstBy(x => x.IdSuscripcionProgramaGeneral == item3 && x.IdMontoPago == item.Id);
                    //        suscripcion.IdSuscripcionProgramaGeneral = item3;
                    //        suscripcion.UsuarioModificacion = Json.Usuario;
                    //        suscripcion.FechaModificacion = DateTime.Now;
                    //    }
                    //    else
                    //    {
                    //        suscripcion = new MontoPagoSuscripcionBO();
                    //        suscripcion.IdSuscripcionProgramaGeneral = item3;
                    //        suscripcion.UsuarioCreacion = Json.Usuario;
                    //        suscripcion.UsuarioModificacion = Json.Usuario;
                    //        suscripcion.FechaCreacion = DateTime.Now;
                    //        suscripcion.FechaModificacion = DateTime.Now;
                    //        suscripcion.Estado = true;
                    //    }
                    //    montoPago.MontoPagoSuscripcion.Add(suscripcion);
                    //}
                    repMontoPago.Update(montoPago);
                }
                foreach (var item in Json.DetallesProgramaGeneral.ConfiguracionPlantilla)
                {
                    if (item.RemplazarCertificados == true)
                    {
                        _repCertificadoGeneradoAutomatico.ActualizarCertificadosGenerados(item.Id, pgeneral.Id);
                    }
                    PgeneralConfiguracionPlantillaBO certificadoPlantilla = new PgeneralConfiguracionPlantillaBO();

                    if (_repPgeneralConfiguracionPlantilla.Exist(x => x.Id == item.Id))
                    {
                        certificadoPlantilla = _repPgeneralConfiguracionPlantilla.FirstById(item.Id);
                        certificadoPlantilla.IdPgeneral = pgeneral.Id;
                        certificadoPlantilla.IdPlantillaFrontal = item.IdPlantillaFrontal;
                        certificadoPlantilla.IdPlantillaPosterior = item.IdPlantillaPosterior;
                        if (item.RemplazarCertificados == true)
                        {
                            certificadoPlantilla.UltimaFechaRemplazarCertificado = DateTime.Now;
                        }
                        certificadoPlantilla.UltimaFechaRemplazarCertificado = DateTime.Now;
                        certificadoPlantilla.UsuarioModificacion = Json.Usuario;
                        certificadoPlantilla.FechaModificacion = DateTime.Now;

                    }
                    else
                    {
                        certificadoPlantilla.IdPgeneral = pgeneral.Id;
                        certificadoPlantilla.IdPlantillaFrontal = item.IdPlantillaFrontal;
                        certificadoPlantilla.IdPlantillaPosterior = item.IdPlantillaPosterior;
                        if (item.RemplazarCertificados == true)
                        {
                            certificadoPlantilla.UltimaFechaRemplazarCertificado = DateTime.Now;
                        }
                        certificadoPlantilla.UsuarioCreacion = Json.Usuario;
                        certificadoPlantilla.UsuarioModificacion = Json.Usuario;
                        certificadoPlantilla.FechaCreacion = DateTime.Now;
                        certificadoPlantilla.FechaModificacion = DateTime.Now;
                        certificadoPlantilla.Estado = true;
                    }
                    _repPgeneralConfiguracionPlantilla.Update(certificadoPlantilla);

                    var lista = _repPgeneralConfiguracionPlantillaDetalle.GetBy(w => w.IdPgeneralConfiguracionPlantilla == certificadoPlantilla.Id);
                    if (lista.Count() > 0)
                    {
                        var listaId = lista.Select(w => w.Id);

                        _repPgeneralConfiguracionPlantillaDetalle.Delete(listaId, Json.Usuario);
                        foreach (var id in listaId)
                        {
                            if (_repPgeneralconfiguracionPlantillaEstadoMatricula.Exist(w => w.IdPgeneralConfiguracionPlantillaDetalle == id))
                            {
                                var listaIds = _repPgeneralconfiguracionPlantillaEstadoMatricula.GetBy(w => w.IdPgeneralConfiguracionPlantillaDetalle == id).Select(w => w.Id);
                                _repPgeneralconfiguracionPlantillaEstadoMatricula.Delete(listaIds, Json.Usuario);
                            }
                            if (_repPgeneralConfiguracionPlantillaSubEstadoMatricula.Exist(w => w.IdPgeneralConfiguracionPlantillaDetalle == id))
                            {
                                var listaIds = _repPgeneralConfiguracionPlantillaSubEstadoMatricula.GetBy(w => w.IdPgeneralConfiguracionPlantillaDetalle == id).Select(w => w.Id);
                                _repPgeneralConfiguracionPlantillaSubEstadoMatricula.Delete(listaIds, Json.Usuario);
                            }
                        }

                    }

                    foreach (var item2 in item.detalle)
                    {
                        PgeneralConfiguracionPlantillaDetalleBO plantillaCertificadoDetalle = new PgeneralConfiguracionPlantillaDetalleBO();
                        plantillaCertificadoDetalle.IdPgeneralConfiguracionPlantilla = certificadoPlantilla.Id;
                        //plantillaCertificadoDetalle.IdEstadoMatricula = item2.IdEstadoMatricula;
                        plantillaCertificadoDetalle.IdOperadorComparacion = item2.IdOperadorComparacion;
                        plantillaCertificadoDetalle.IdModalidadCurso = item2.IdModalidadCurso;
                        plantillaCertificadoDetalle.NotaAprobatoria = item2.NotaAprobatoria;
                        plantillaCertificadoDetalle.DeudaPendiente = item2.DeudaPendiente;
                        plantillaCertificadoDetalle.UsuarioCreacion = Json.Usuario;
                        plantillaCertificadoDetalle.UsuarioModificacion = Json.Usuario;
                        plantillaCertificadoDetalle.FechaCreacion = DateTime.Now;
                        plantillaCertificadoDetalle.FechaModificacion = DateTime.Now;
                        plantillaCertificadoDetalle.Estado = true;
                        _repPgeneralConfiguracionPlantillaDetalle.Insert(plantillaCertificadoDetalle);

                        foreach (var estados in item2.IdEstadoMatricula)
                        {
                            PgeneralConfiguracionPlantillaEstadoMatriculaBO pgeneralConfiguracionPlantillaEstado = new PgeneralConfiguracionPlantillaEstadoMatriculaBO();
                            pgeneralConfiguracionPlantillaEstado.IdEstadoMatricula = estados.Id;
                            pgeneralConfiguracionPlantillaEstado.IdPgeneralConfiguracionPlantillaDetalle = plantillaCertificadoDetalle.Id;
                            pgeneralConfiguracionPlantillaEstado.UsuarioCreacion = Json.Usuario;
                            pgeneralConfiguracionPlantillaEstado.UsuarioModificacion = Json.Usuario;
                            pgeneralConfiguracionPlantillaEstado.FechaCreacion = DateTime.Now;
                            pgeneralConfiguracionPlantillaEstado.FechaModificacion = DateTime.Now;
                            pgeneralConfiguracionPlantillaEstado.Estado = true;

                            _repPgeneralconfiguracionPlantillaEstadoMatricula.Insert(pgeneralConfiguracionPlantillaEstado);
                        }
                        foreach (var estados in item2.IdSubEstadoMatricula)
                        {
                            PgeneralConfiguracionPlantillaSubEstadoMatriculaBO pgeneralConfiguracionPlantillaSubEstado = new PgeneralConfiguracionPlantillaSubEstadoMatriculaBO();
                            pgeneralConfiguracionPlantillaSubEstado.IdSubEstadoMatricula = estados.Id;
                            pgeneralConfiguracionPlantillaSubEstado.IdPgeneralConfiguracionPlantillaDetalle = plantillaCertificadoDetalle.Id;
                            pgeneralConfiguracionPlantillaSubEstado.UsuarioCreacion = Json.Usuario;
                            pgeneralConfiguracionPlantillaSubEstado.UsuarioModificacion = Json.Usuario;
                            pgeneralConfiguracionPlantillaSubEstado.FechaCreacion = DateTime.Now;
                            pgeneralConfiguracionPlantillaSubEstado.FechaModificacion = DateTime.Now;
                            pgeneralConfiguracionPlantillaSubEstado.Estado = true;

                            _repPgeneralConfiguracionPlantillaSubEstadoMatricula.Insert(pgeneralConfiguracionPlantillaSubEstado);
                        }
                    }
                    //var ListaIds =  _repPgeneralConfiguracionPlantillaDetalle
                }
                if (Json.DetallesProgramaGeneral.ConfiguracionPlantillaConstancia != null)
                {
                    foreach (var item in Json.DetallesProgramaGeneral.ConfiguracionPlantillaConstancia)
                    {
                        if (item.RemplazarCertificados == true)
                        {
                            _repCertificadoGeneradoAutomatico.ActualizarCertificadosGenerados(item.Id, pgeneral.Id);
                        }
                        PgeneralConfiguracionPlantillaBO certificadoPlantilla = new PgeneralConfiguracionPlantillaBO();

                        if (_repPgeneralConfiguracionPlantilla.Exist(x => x.Id == item.Id))
                        {
                            certificadoPlantilla = _repPgeneralConfiguracionPlantilla.FirstById(item.Id);
                            certificadoPlantilla.IdPgeneral = pgeneral.Id;
                            certificadoPlantilla.IdPlantillaFrontal = item.IdPlantillaFrontal;
                            certificadoPlantilla.IdPlantillaPosterior = item.IdPlantillaPosterior;
                            if (item.RemplazarCertificados == true)
                            {
                                certificadoPlantilla.UltimaFechaRemplazarCertificado = DateTime.Now;
                            }
                            certificadoPlantilla.UltimaFechaRemplazarCertificado = DateTime.Now;
                            certificadoPlantilla.UsuarioModificacion = Json.Usuario;
                            certificadoPlantilla.FechaModificacion = DateTime.Now;

                        }
                        else
                        {
                            certificadoPlantilla.IdPgeneral = pgeneral.Id;
                            certificadoPlantilla.IdPlantillaFrontal = item.IdPlantillaFrontal;
                            certificadoPlantilla.IdPlantillaPosterior = item.IdPlantillaPosterior;
                            if (item.RemplazarCertificados == true)
                            {
                                certificadoPlantilla.UltimaFechaRemplazarCertificado = DateTime.Now;
                            }
                            certificadoPlantilla.UsuarioCreacion = Json.Usuario;
                            certificadoPlantilla.UsuarioModificacion = Json.Usuario;
                            certificadoPlantilla.FechaCreacion = DateTime.Now;
                            certificadoPlantilla.FechaModificacion = DateTime.Now;
                            certificadoPlantilla.Estado = true;
                        }
                        _repPgeneralConfiguracionPlantilla.Update(certificadoPlantilla);

                        var lista = _repPgeneralConfiguracionPlantillaDetalle.GetBy(w => w.IdPgeneralConfiguracionPlantilla == certificadoPlantilla.Id);
                        if (lista.Count() > 0)
                        {
                            var listaId = lista.Select(w => w.Id);

                            _repPgeneralConfiguracionPlantillaDetalle.Delete(listaId, Json.Usuario);
                            foreach (var id in listaId)
                            {
                                if (_repPgeneralconfiguracionPlantillaEstadoMatricula.Exist(w => w.IdPgeneralConfiguracionPlantillaDetalle == id))
                                {
                                    var listaIds = _repPgeneralconfiguracionPlantillaEstadoMatricula.GetBy(w => w.IdPgeneralConfiguracionPlantillaDetalle == id).Select(w => w.Id);
                                    _repPgeneralconfiguracionPlantillaEstadoMatricula.Delete(listaIds, Json.Usuario);
                                }
                                if (_repPgeneralConfiguracionPlantillaSubEstadoMatricula.Exist(w => w.IdPgeneralConfiguracionPlantillaDetalle == id))
                                {
                                    var listaIds = _repPgeneralConfiguracionPlantillaSubEstadoMatricula.GetBy(w => w.IdPgeneralConfiguracionPlantillaDetalle == id).Select(w => w.Id);
                                    _repPgeneralConfiguracionPlantillaSubEstadoMatricula.Delete(listaIds, Json.Usuario);
                                }
                            }
                        }

                        foreach (var item2 in item.detalle)
                        {

                            PgeneralConfiguracionPlantillaDetalleBO plantillaCertificadoDetalle = new PgeneralConfiguracionPlantillaDetalleBO();
                            plantillaCertificadoDetalle.IdPgeneralConfiguracionPlantilla = certificadoPlantilla.Id;
                            //plantillaCertificadoDetalle.IdEstadoMatricula = item2.IdEstadoMatricula;
                            plantillaCertificadoDetalle.IdOperadorComparacion = item2.IdOperadorComparacion == 0 ? null : item2.IdOperadorComparacion;
                            plantillaCertificadoDetalle.IdModalidadCurso = item2.IdModalidadCurso;
                            plantillaCertificadoDetalle.NotaAprobatoria = null;
                            plantillaCertificadoDetalle.DeudaPendiente = false;
                            plantillaCertificadoDetalle.UsuarioCreacion = Json.Usuario;
                            plantillaCertificadoDetalle.UsuarioModificacion = Json.Usuario;
                            plantillaCertificadoDetalle.FechaCreacion = DateTime.Now;
                            plantillaCertificadoDetalle.FechaModificacion = DateTime.Now;
                            plantillaCertificadoDetalle.Estado = true;
                            _repPgeneralConfiguracionPlantillaDetalle.Insert(plantillaCertificadoDetalle);

                            foreach (var estados in item2.IdEstadoMatricula)
                            {
                                PgeneralConfiguracionPlantillaEstadoMatriculaBO pgeneralConfiguracionPlantillaEstado = new PgeneralConfiguracionPlantillaEstadoMatriculaBO();
                                pgeneralConfiguracionPlantillaEstado.IdEstadoMatricula = estados.Id;
                                pgeneralConfiguracionPlantillaEstado.IdPgeneralConfiguracionPlantillaDetalle = plantillaCertificadoDetalle.Id;
                                pgeneralConfiguracionPlantillaEstado.UsuarioCreacion = Json.Usuario;
                                pgeneralConfiguracionPlantillaEstado.UsuarioModificacion = Json.Usuario;
                                pgeneralConfiguracionPlantillaEstado.FechaCreacion = DateTime.Now;
                                pgeneralConfiguracionPlantillaEstado.FechaModificacion = DateTime.Now;
                                pgeneralConfiguracionPlantillaEstado.Estado = true;

                                _repPgeneralconfiguracionPlantillaEstadoMatricula.Insert(pgeneralConfiguracionPlantillaEstado);
                            }
                            foreach (var estados in item2.IdSubEstadoMatricula)
                            {
                                PgeneralConfiguracionPlantillaSubEstadoMatriculaBO pgeneralConfiguracionPlantillaSubEstado = new PgeneralConfiguracionPlantillaSubEstadoMatriculaBO();
                                pgeneralConfiguracionPlantillaSubEstado.IdSubEstadoMatricula = estados.Id;
                                pgeneralConfiguracionPlantillaSubEstado.IdPgeneralConfiguracionPlantillaDetalle = plantillaCertificadoDetalle.Id;
                                pgeneralConfiguracionPlantillaSubEstado.UsuarioCreacion = Json.Usuario;
                                pgeneralConfiguracionPlantillaSubEstado.UsuarioModificacion = Json.Usuario;
                                pgeneralConfiguracionPlantillaSubEstado.FechaCreacion = DateTime.Now;
                                pgeneralConfiguracionPlantillaSubEstado.FechaModificacion = DateTime.Now;
                                pgeneralConfiguracionPlantillaSubEstado.Estado = true;

                                _repPgeneralConfiguracionPlantillaSubEstadoMatricula.Insert(pgeneralConfiguracionPlantillaSubEstado);
                            }
                        }
                        //var ListaIds =  _repPgeneralConfiguracionPlantillaDetalle
                    }
                }

                foreach (var item in Json.DetallesProgramaGeneral.ConfiguracionBeneficio)
                {
                    TConfiguracionBeneficioProgramaGeneralBO objeto;

                    if (item.Asosiar)
                    {
                        if (_repPgeneralConfiguracionBeneficio.Exist(x => x.Id == item.Id)) // Editar
                        {
                            objeto = _repPgeneralConfiguracionBeneficio.FirstBy(x => x.Id == item.Id);
                            objeto.IdPGeneral = item.IdPGeneral;
                            objeto.IdBeneficio = item.IdBeneficio;
                            objeto.OrdenBeneficio = item.OrdenBeneficio;
                            objeto.DatosAdicionales = item.DatosAdicionales;
                            objeto.Tipo = item.TipoBeneficio.Value;
                            objeto.Asosiar = item.Asosiar;
                            objeto.DeudaPendiente = item.DeudaPendiente;
                            objeto.AvanceAcademico = item.AvanceAcademico;
                            objeto.Entrega = item.Entrega;
                            objeto.UsuarioModificacion = Json.Usuario;
                            objeto.FechaModificacion = DateTime.Now;
                        }
                        else // Id = 0 es Nuevo
                        {
                            objeto = new TConfiguracionBeneficioProgramaGeneralBO();
                            objeto.IdPGeneral = item.IdPGeneral;
                            objeto.IdBeneficio = item.IdBeneficio;
                            objeto.OrdenBeneficio = item.OrdenBeneficio;
                            objeto.DatosAdicionales = item.DatosAdicionales;
                            objeto.Tipo = item.TipoBeneficio.Value;
                            objeto.Asosiar = item.Asosiar;
                            objeto.Entrega = item.Entrega;
                            objeto.DeudaPendiente = item.DeudaPendiente;
                            objeto.AvanceAcademico = item.AvanceAcademico;
                            objeto.UsuarioCreacion = Json.Usuario;
                            objeto.UsuarioModificacion = Json.Usuario;
                            objeto.FechaCreacion = DateTime.Now;
                            objeto.FechaModificacion = DateTime.Now;
                            objeto.Estado = true;
                        }
                        _repPgeneralConfiguracionBeneficio.Update(objeto);

                        var _listaEstado = _repConfiguracionBeneficioEstado.GetBy(x => x.IdConfiguracionBeneficioPgneral == objeto.Id);
                        if (_listaEstado.Count() > 0)
                        {
                            var listaId = _listaEstado.Select(w => w.Id);
                            _repConfiguracionBeneficioEstado.Delete(listaId, Json.Usuario);
                        }
                        foreach (var itemLista in item.IdEstadoMatricula)
                        {
                            TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO _objeto = new TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO();
                            _objeto.IdConfiguracionBeneficioPGneral = objeto.Id;
                            _objeto.IdEstadoMatricula = itemLista.Id;
                            _objeto.UsuarioCreacion = Json.Usuario;
                            _objeto.UsuarioModificacion = Json.Usuario;
                            _objeto.FechaCreacion = DateTime.Now;
                            _objeto.FechaModificacion = DateTime.Now;
                            _objeto.Estado = true;
                            _repConfiguracionBeneficioEstado.Insert(_objeto);
                        }

                        var _listaSubEstado = _repConfiguracionBeneficioSubEstado.GetBy(x => x.IdConfiguracionBeneficioPgneral == objeto.Id);
                        if (_listaSubEstado.Count() > 0)
                        {
                            var listaId = _listaSubEstado.Select(w => w.Id);
                            _repConfiguracionBeneficioSubEstado.Delete(listaId, Json.Usuario);
                        }
                        foreach (var itemLista in item.IdSubEstadoMatricula)
                        {
                            TConfiguracionBeneficioProgramaGeneralSubEstadoBO _objeto = new TConfiguracionBeneficioProgramaGeneralSubEstadoBO();
                            _objeto.IdConfiguracionBeneficioPGneral = objeto.Id;
                            _objeto.IdSubEstadoMatricula = itemLista.Id;
                            _objeto.UsuarioCreacion = Json.Usuario;
                            _objeto.UsuarioModificacion = Json.Usuario;
                            _objeto.FechaCreacion = DateTime.Now;
                            _objeto.FechaModificacion = DateTime.Now;
                            _objeto.Estado = true;
                            _repConfiguracionBeneficioSubEstado.Insert(_objeto);
                        }

                        var _listaPaises = _repConfiguracionBeneficioPais.GetBy(x => x.IdConfiguracionBeneficioPgneral == objeto.Id);
                        if (_listaPaises.Count() > 0)
                        {
                            var listaId = _listaPaises.Select(w => w.Id);
                            _repConfiguracionBeneficioPais.Delete(listaId, Json.Usuario);
                        }
                        foreach (var itemLista in item.IdPais)
                        {
                            TConfiguracionBeneficioProgramaGeneralPaisBO _objeto = new TConfiguracionBeneficioProgramaGeneralPaisBO();
                            _objeto.IdConfiguracionBeneficioPGneral = objeto.Id;
                            _objeto.IdPais = itemLista.Id;
                            _objeto.UsuarioCreacion = Json.Usuario;
                            _objeto.UsuarioModificacion = Json.Usuario;
                            _objeto.FechaCreacion = DateTime.Now;
                            _objeto.FechaModificacion = DateTime.Now;
                            _objeto.Estado = true;
                            _repConfiguracionBeneficioPais.Insert(_objeto);
                        }

                        var _listaDatoAdicional = _repConfiguracionBeneficioDatoAdicional.GetBy(x => x.IdConfiguracionBeneficioPgeneral == objeto.Id);
                        if (_listaDatoAdicional.Count() > 0)
                        {
                            var listaId = _listaDatoAdicional.Select(w => w.Id);
                            _repConfiguracionBeneficioDatoAdicional.Delete(listaId, Json.Usuario);
                        }
                        if (item.DatosAdicionales == true)
                        {
                            if (item.IdDatoAdicional != null)
                            {
                                foreach (var itemLista in item.IdDatoAdicional)
                                {
                                    ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO _objeto = new ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO();
                                    _objeto.IdConfiguracionBeneficioPgeneral = objeto.Id;
                                    _objeto.IdBeneficioDatoAdicional = itemLista.Id;
                                    _objeto.UsuarioCreacion = Json.Usuario;
                                    _objeto.UsuarioModificacion = Json.Usuario;
                                    _objeto.FechaCreacion = DateTime.Now;
                                    _objeto.FechaModificacion = DateTime.Now;
                                    _objeto.Estado = true;
                                    _repConfiguracionBeneficioDatoAdicional.Insert(_objeto);
                                }
                            }
                        }

                        var _listaVersiones = _repConfiguracionBeneficioVersion.GetBy(x => x.IdConfiguracionBeneficioPgneral == objeto.Id);
                        if (_listaVersiones.Count() > 0)
                        {
                            var listaId = _listaVersiones.Select(w => w.Id);
                            _repConfiguracionBeneficioVersion.Delete(listaId, Json.Usuario);
                        }
                        foreach (var itemLista in item.IdVersion)
                        {
                            TConfiguracionBeneficioProgramaGeneralVersionBO _objeto = new TConfiguracionBeneficioProgramaGeneralVersionBO();
                            _objeto.IdConfiguracionBeneficioPGneral = objeto.Id;
                            if (itemLista.Id == 0)
                            {
                                _objeto.IdVersionPrograma = null;
                            }
                            else
                            {
                                if (itemLista.Id == 1 || itemLista.Id == 2 || itemLista.Id == 3 || itemLista.Id == 4) _objeto.IdVersionPrograma = itemLista.Id;
                                else
                                {
                                    if (itemLista.IdVersion == 1 || itemLista.IdVersion == 2 || itemLista.IdVersion == 3 || itemLista.IdVersion == 4 || itemLista.IdVersion == null) _objeto.IdVersionPrograma = itemLista.IdVersion;
                                    //_objeto.IdVersionPrograma = itemLista.IdVersion;
                                }
                            }



                            _objeto.UsuarioCreacion = Json.Usuario;
                            _objeto.UsuarioModificacion = Json.Usuario;
                            _objeto.FechaCreacion = DateTime.Now;
                            _objeto.FechaModificacion = DateTime.Now;
                            _objeto.Estado = true;
                            _repConfiguracionBeneficioVersion.Insert(_objeto);
                        }
                    }
                    else
                    {
                        if (item.Id != 0)
                        {
                            _repPgeneralConfiguracionBeneficio.Delete(item.Id, Json.Usuario);

                            var _listaEstado = _repConfiguracionBeneficioEstado.GetBy(x => x.IdConfiguracionBeneficioPgneral == item.Id);
                            if (_listaEstado.Count() > 0)
                            {
                                var listaId = _listaEstado.Select(w => w.Id);
                                _repConfiguracionBeneficioEstado.Delete(listaId, Json.Usuario);
                            }

                            var _listaSubEstado = _repConfiguracionBeneficioSubEstado.GetBy(x => x.IdConfiguracionBeneficioPgneral == item.Id);
                            if (_listaSubEstado.Count() > 0)
                            {
                                var listaId = _listaSubEstado.Select(w => w.Id);
                                _repConfiguracionBeneficioSubEstado.Delete(listaId, Json.Usuario);
                            }

                            var _listaPaises = _repConfiguracionBeneficioPais.GetBy(x => x.IdConfiguracionBeneficioPgneral == item.Id);
                            if (_listaPaises.Count() > 0)
                            {
                                var listaId = _listaPaises.Select(w => w.Id);
                                _repConfiguracionBeneficioPais.Delete(listaId, Json.Usuario);
                            }
                            var _listaVersiones = _repConfiguracionBeneficioVersion.GetBy(x => x.IdConfiguracionBeneficioPgneral == item.Id);
                            if (_listaVersiones.Count() > 0)
                            {
                                var listaId = _listaVersiones.Select(w => w.Id);
                                _repConfiguracionBeneficioVersion.Delete(listaId, Json.Usuario);
                            }
                        }
                    }
                }
                foreach (var item in Json.DetallesProgramaGeneral.PgeneralCodigoPartner)
                {
                    PgeneralCodigoPartnerBO pgeneralCodigoPartnerBO = new PgeneralCodigoPartnerBO();

                    if (_repPgeneralCodigoPartner.Exist(item.Id))
                    {
                        pgeneralCodigoPartnerBO = _repPgeneralCodigoPartner.FirstById(item.Id);
                        pgeneralCodigoPartnerBO.Codigo = item.Codigo;
                        pgeneralCodigoPartnerBO.IdPgeneral = pgeneral.Id;
                        pgeneralCodigoPartnerBO.UsuarioModificacion = Json.Usuario;
                        pgeneralCodigoPartnerBO.FechaModificacion = DateTime.Now;
                        pgeneralCodigoPartnerBO.Estado = true;
                    }
                    else
                    {

                        pgeneralCodigoPartnerBO.Codigo = item.Codigo;
                        pgeneralCodigoPartnerBO.IdPgeneral = pgeneral.Id;
                        pgeneralCodigoPartnerBO.UsuarioCreacion = Json.Usuario;
                        pgeneralCodigoPartnerBO.UsuarioModificacion = Json.Usuario;
                        pgeneralCodigoPartnerBO.FechaCreacion = DateTime.Now;
                        pgeneralCodigoPartnerBO.FechaModificacion = DateTime.Now;
                        pgeneralCodigoPartnerBO.Estado = true;
                    }
                    _repPgeneralCodigoPartner.Update(pgeneralCodigoPartnerBO);
                    foreach (var item2 in item.IdModalidadCurso)
                    {
                        PgeneralCodigoPartnerModalidadCursoBO pgeneralCodigoPartnerModalidadCursoBO = new PgeneralCodigoPartnerModalidadCursoBO();

                        if (_repPgeneralCodigoPartnerModalidadCurso.Exist(w => w.IdModalidadCurso == item2.Id && w.IdPgeneralCodigoPartner == pgeneralCodigoPartnerBO.Id))
                        {
                            pgeneralCodigoPartnerModalidadCursoBO = _repPgeneralCodigoPartnerModalidadCurso.FirstBy(w => w.IdModalidadCurso == item2.Id && w.IdPgeneralCodigoPartner == pgeneralCodigoPartnerBO.Id);
                            pgeneralCodigoPartnerModalidadCursoBO.IdPgeneralCodigoPartner = pgeneralCodigoPartnerBO.Id;
                            pgeneralCodigoPartnerModalidadCursoBO.IdModalidadCurso = item2.Id.Value;
                            pgeneralCodigoPartnerModalidadCursoBO.UsuarioModificacion = Json.Usuario;
                            pgeneralCodigoPartnerModalidadCursoBO.FechaModificacion = DateTime.Now;
                            pgeneralCodigoPartnerModalidadCursoBO.Estado = true;
                        }
                        else
                        {

                            pgeneralCodigoPartnerModalidadCursoBO.IdPgeneralCodigoPartner = pgeneralCodigoPartnerBO.Id;
                            pgeneralCodigoPartnerModalidadCursoBO.IdModalidadCurso = item2.Id.Value;
                            pgeneralCodigoPartnerModalidadCursoBO.UsuarioCreacion = Json.Usuario;
                            pgeneralCodigoPartnerModalidadCursoBO.UsuarioModificacion = Json.Usuario;
                            pgeneralCodigoPartnerModalidadCursoBO.FechaCreacion = DateTime.Now;
                            pgeneralCodigoPartnerModalidadCursoBO.FechaModificacion = DateTime.Now;
                            pgeneralCodigoPartnerModalidadCursoBO.Estado = true;
                        }
                        _repPgeneralCodigoPartnerModalidadCurso.Update(pgeneralCodigoPartnerModalidadCursoBO);
                    }
                    foreach (var item2 in item.IdVersionPrograma)
                    {
                        PgeneralCodigoPartnerVersionProgramaBO pgeneralCodigoPartnerVersionProgramaBO = new PgeneralCodigoPartnerVersionProgramaBO();

                        if (_repPgeneralCodigoPartnerVersionPrograma.Exist(w => w.IdVersionPrograma == item2.Id && w.IdPgeneralCodigoPartner == pgeneralCodigoPartnerBO.Id))
                        {
                            pgeneralCodigoPartnerVersionProgramaBO = _repPgeneralCodigoPartnerVersionPrograma.FirstBy(w => w.IdVersionPrograma == item2.Id && w.IdPgeneralCodigoPartner == pgeneralCodigoPartnerBO.Id);
                            pgeneralCodigoPartnerVersionProgramaBO.IdPgeneralCodigoPartner = pgeneralCodigoPartnerBO.Id;
                            pgeneralCodigoPartnerVersionProgramaBO.IdVersionPrograma = item2.Id;
                            pgeneralCodigoPartnerVersionProgramaBO.UsuarioModificacion = Json.Usuario;
                            pgeneralCodigoPartnerVersionProgramaBO.FechaModificacion = DateTime.Now;
                            pgeneralCodigoPartnerVersionProgramaBO.Estado = true;
                        }
                        else
                        {

                            pgeneralCodigoPartnerVersionProgramaBO.IdPgeneralCodigoPartner = pgeneralCodigoPartnerBO.Id;
                            pgeneralCodigoPartnerVersionProgramaBO.IdVersionPrograma = item2.Id;
                            pgeneralCodigoPartnerVersionProgramaBO.UsuarioCreacion = Json.Usuario;
                            pgeneralCodigoPartnerVersionProgramaBO.UsuarioModificacion = Json.Usuario;
                            pgeneralCodigoPartnerVersionProgramaBO.FechaCreacion = DateTime.Now;
                            pgeneralCodigoPartnerVersionProgramaBO.FechaModificacion = DateTime.Now;
                            pgeneralCodigoPartnerVersionProgramaBO.Estado = true;
                        }
                        _repPgeneralCodigoPartnerVersionPrograma.Update(pgeneralCodigoPartnerVersionProgramaBO);
                    }
                }
                foreach (var item in Json.DetallesProgramaGeneral.pgeneralProyectoAplicacion)
                {
                    PgeneralProyectoAplicacionBO pgeneralProyectoAplicacionBO = new PgeneralProyectoAplicacionBO();

                    if (_repPgeneralProyectoAplicacion.Exist(item.Id))
                    {
                        pgeneralProyectoAplicacionBO = _repPgeneralProyectoAplicacion.FirstById(item.Id);
                        pgeneralProyectoAplicacionBO.IdPgeneral = pgeneral.Id;
                        pgeneralProyectoAplicacionBO.UsuarioModificacion = Json.Usuario;
                        pgeneralProyectoAplicacionBO.FechaModificacion = DateTime.Now;
                        pgeneralProyectoAplicacionBO.Estado = true;
                    }
                    else
                    {
                        pgeneralProyectoAplicacionBO.IdPgeneral = pgeneral.Id;
                        pgeneralProyectoAplicacionBO.UsuarioCreacion = Json.Usuario;
                        pgeneralProyectoAplicacionBO.UsuarioModificacion = Json.Usuario;
                        pgeneralProyectoAplicacionBO.FechaCreacion = DateTime.Now;
                        pgeneralProyectoAplicacionBO.FechaModificacion = DateTime.Now;
                        pgeneralProyectoAplicacionBO.Estado = true;
                    }
                    _repPgeneralProyectoAplicacion.Update(pgeneralProyectoAplicacionBO);
                    foreach (var item2 in item.IdModalidadCurso)
                    {
                        //_repPgeneralProyectoAplicacionModalidad.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, item.IdModalidadCurso);

                        PgeneralProyectoAplicacionModalidadBO pgeneralProyectoAplicacionModalidadBO = new PgeneralProyectoAplicacionModalidadBO();

                        if (_repPgeneralProyectoAplicacionModalidad.Exist(w => w.IdModalidadCurso == item2.Id && w.IdPgeneralProyectoAplicacion == item.Id))
                        {
                            pgeneralProyectoAplicacionModalidadBO = _repPgeneralProyectoAplicacionModalidad.FirstBy(w => w.IdModalidadCurso == item2.Id && w.IdPgeneralProyectoAplicacion == item.Id);
                            pgeneralProyectoAplicacionModalidadBO.IdPgeneralProyectoAplicacion = pgeneralProyectoAplicacionBO.Id;
                            pgeneralProyectoAplicacionModalidadBO.IdModalidadCurso = item2.Id;
                            pgeneralProyectoAplicacionModalidadBO.UsuarioModificacion = Json.Usuario;
                            pgeneralProyectoAplicacionModalidadBO.FechaModificacion = DateTime.Now;
                            pgeneralProyectoAplicacionModalidadBO.Estado = true;
                        }
                        else
                        {

                            pgeneralProyectoAplicacionModalidadBO.IdPgeneralProyectoAplicacion = pgeneralProyectoAplicacionBO.Id;
                            pgeneralProyectoAplicacionModalidadBO.IdModalidadCurso = item2.Id;
                            pgeneralProyectoAplicacionModalidadBO.UsuarioCreacion = Json.Usuario;
                            pgeneralProyectoAplicacionModalidadBO.UsuarioModificacion = Json.Usuario;
                            pgeneralProyectoAplicacionModalidadBO.FechaCreacion = DateTime.Now;
                            pgeneralProyectoAplicacionModalidadBO.FechaModificacion = DateTime.Now;
                            pgeneralProyectoAplicacionModalidadBO.Estado = true;
                        }
                        _repPgeneralProyectoAplicacionModalidad.Update(pgeneralProyectoAplicacionModalidadBO);
                    }
                    foreach (var item2 in item.IdProveedor)
                    {
                        //_repPgeneralProyectoAplicacionProveedor.DeleteLogicoPorPrograma(Json.ProgramaGeneral.IdPgeneral, Json.Usuario, item.IdProveedor);

                        PgeneralProyectoAplicacionProveedorBO pgeneralProyectoAplicacionProveedorBO = new PgeneralProyectoAplicacionProveedorBO();

                        if (_repPgeneralProyectoAplicacionProveedor.Exist(w => w.IdProveedor == item2.Id && w.IdPgeneralProyectoAplicacion == item.Id))
                        {
                            pgeneralProyectoAplicacionProveedorBO = _repPgeneralProyectoAplicacionProveedor.FirstBy(w => w.IdProveedor == item2.Id && w.IdPgeneralProyectoAplicacion == item.Id);
                            pgeneralProyectoAplicacionProveedorBO.IdPgeneralProyectoAplicacion = pgeneralProyectoAplicacionBO.Id;
                            pgeneralProyectoAplicacionProveedorBO.IdProveedor = item2.Id;
                            pgeneralProyectoAplicacionProveedorBO.UsuarioModificacion = Json.Usuario;
                            pgeneralProyectoAplicacionProveedorBO.FechaModificacion = DateTime.Now;
                            pgeneralProyectoAplicacionProveedorBO.Estado = true;
                        }
                        else
                        {

                            pgeneralProyectoAplicacionProveedorBO.IdPgeneralProyectoAplicacion = pgeneralProyectoAplicacionBO.Id;
                            pgeneralProyectoAplicacionProveedorBO.IdProveedor = item2.Id;
                            pgeneralProyectoAplicacionProveedorBO.UsuarioCreacion = Json.Usuario;
                            pgeneralProyectoAplicacionProveedorBO.UsuarioModificacion = Json.Usuario;
                            pgeneralProyectoAplicacionProveedorBO.FechaCreacion = DateTime.Now;
                            pgeneralProyectoAplicacionProveedorBO.FechaModificacion = DateTime.Now;
                            pgeneralProyectoAplicacionProveedorBO.Estado = true;
                        }
                        _repPgeneralProyectoAplicacionProveedor.Update(pgeneralProyectoAplicacionProveedorBO);
                    }
                }
                if (Json.DetallesProgramaGeneral.PGeneralForoAsignacionProveedor.Count > 0 && Json.DetallesProgramaGeneral.CambioPGeneralForoAsignacion == true)
                {
                    List<int> proveedoresAnteriores = new List<int>();
                    List<int> proveedoresActualizados = new List<int>();
                    //Se elimina registros anteriores
                    var registrosAnteriores = _repPGeneralForoAsignacionProveedor.GetBy(x => x.IdPgeneral == Json.ProgramaGeneral.Id).ToList();
                    foreach (var eliminar in registrosAnteriores)
                    {
                        proveedoresAnteriores.Add(eliminar.Id);
                        _repPGeneralForoAsignacionProveedor.Delete(eliminar.Id, Json.Usuario);
                    }
                    //Validación para evitar la duplicidad de datos
                    List<PGeneralForoAsignacionProveedorAuxiliarDTO> listaAuxiliarForoAsignacionProveedor = new List<PGeneralForoAsignacionProveedorAuxiliarDTO>();
                    PGeneralForoAsignacionProveedorAuxiliarDTO agregar;
                    PGeneralForoAsignacionProveedorBO nuevoRegistro;
                    //Armamos una lista de datos lineal para distinción de proveedores y modalidades
                    foreach (var configuracionForoAsignacionProveedor in Json.DetallesProgramaGeneral.PGeneralForoAsignacionProveedor)
                    {
                        foreach (var proveedor in configuracionForoAsignacionProveedor.IdProveedor)
                        {
                            agregar = new PGeneralForoAsignacionProveedorAuxiliarDTO()
                            {
                                IdPgeneral = Json.ProgramaGeneral.Id,
                                IdModalidadCurso = configuracionForoAsignacionProveedor.IdModalidadCurso,
                                IdProveedor = proveedor
                            };
                            listaAuxiliarForoAsignacionProveedor.Add(agregar);
                        }
                    }
                    if (listaAuxiliarForoAsignacionProveedor.Count > 0)
                    {
                        List<int> listaIdProveedor = new List<int>();
                        //Se distingue lista para evitar repetición de registros
                        var listaAuxiliarForoAsignacionProveedorSinRepeticiones = listaAuxiliarForoAsignacionProveedor.Select(x => new { x.IdModalidadCurso, x.IdPgeneral, x.IdProveedor }).Distinct().ToList();

                        foreach (var registro in listaAuxiliarForoAsignacionProveedorSinRepeticiones)
                        {
                            nuevoRegistro = new PGeneralForoAsignacionProveedorBO()
                            {
                                IdPgeneral = registro.IdPgeneral,
                                IdModalidadCurso = registro.IdModalidadCurso,
                                IdProveedor = registro.IdProveedor,
                                Estado = true,
                                UsuarioCreacion = Json.Usuario,
                                UsuarioModificacion = Json.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repPGeneralForoAsignacionProveedor.Insert(nuevoRegistro);
                        }
                    }
                }
                return Ok(pgeneral);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult registrarArchivoLogoPrograma([FromForm] IFormFile Files)
        {
            try
            {
                string respuesta = string.Empty;

                PgeneralRepositorio _repPrograma = new PgeneralRepositorio();

                using (var ms = new MemoryStream())
                {
                    Files.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    respuesta = _repPrograma.guardarArchivos(fileBytes, Files.ContentType, Files.FileName);
                }

                if (string.IsNullOrEmpty(respuesta))
                {
                    return Ok(new { Resultado = "Error" });
                }
                else
                {
                    return Ok(new { Resultado = "Ok", UrlArchivo = respuesta, NombreArchivo = Files.FileName });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProgramasAsociados([FromBody] ProgramaRelacionadoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PgeneralRelacionadoRepositorio repRelacionados = new PgeneralRelacionadoRepositorio();
                    foreach (var item in Json.Cursos)
                    {
                        PgeneralRelacionadoBO relacionados; relacionados = new PgeneralRelacionadoBO();
                        relacionados.IdPgeneral = Json.IdPGeneral;
                        relacionados.IdPgeneralRelacionado = item.IdRelacionado;
                        relacionados.UsuarioCreacion = Json.Usuario;
                        relacionados.UsuarioModificacion = Json.Usuario;
                        relacionados.FechaCreacion = DateTime.Now;
                        relacionados.FechaModificacion = DateTime.Now;
                        relacionados.Estado = true;
                        repRelacionados.Insert(relacionados);
                    }
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult AsociarProgramasAsociados([FromBody] ProgramaRelacionadoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PgeneralRelacionadoRepositorio repRelacionados = new PgeneralRelacionadoRepositorio();
                    repRelacionados.DeleteLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Cursos);

                    foreach (var item in Json.Cursos)
                    {
                        if (repRelacionados.Exist(item.Id))
                        {
                            PgeneralRelacionadoBO relacionados; relacionados = repRelacionados.FirstById(item.Id);
                            relacionados.IdPgeneralRelacionado = item.IdRelacionado;
                            relacionados.UsuarioModificacion = Json.Usuario;
                            relacionados.FechaModificacion = DateTime.Now;
                            relacionados.Estado = true;
                            repRelacionados.Update(relacionados);
                        }
                        else
                        {
                            PgeneralRelacionadoBO relacionados; relacionados = new PgeneralRelacionadoBO();
                            relacionados.IdPgeneral = Json.IdPGeneral;
                            relacionados.IdPgeneralRelacionado = item.IdRelacionado;
                            relacionados.UsuarioCreacion = Json.Usuario;
                            relacionados.UsuarioModificacion = Json.Usuario;
                            relacionados.FechaCreacion = DateTime.Now;
                            relacionados.FechaModificacion = DateTime.Now;
                            relacionados.Estado = true;
                            repRelacionados.Insert(relacionados);
                        }
                    }
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarDocuementosAsociados([FromBody] DocumentoAsociadoProgramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                DocumentoPwRepositorio repDocumento = new DocumentoPwRepositorio(contexto);
                PgeneralDocumentoPwRepositorio repPgeneralDocumento = new PgeneralDocumentoPwRepositorio(contexto);

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var item in Json.Documentos)
                    {
                        PgeneralDocumentoPwBO pgeneralDocumento = new PgeneralDocumentoPwBO();
                        pgeneralDocumento.IdDocumento = item.IdDocumentos;
                        pgeneralDocumento.IdPgeneral = Json.IdPGeneral;
                        pgeneralDocumento.UsuarioCreacion = Json.Usuario;
                        pgeneralDocumento.UsuarioModificacion = Json.Usuario;
                        pgeneralDocumento.FechaCreacion = DateTime.Now;
                        pgeneralDocumento.FechaModificacion = DateTime.Now;
                        pgeneralDocumento.Estado = true;
                        repPgeneralDocumento.Update(pgeneralDocumento);

                        DocumentoPwBO documento = repDocumento.FirstById(item.IdDocumentos);
                        documento.Asignado = true;
                        documento.UsuarioModificacion = Json.Usuario;
                        documento.FechaModificacion = DateTime.Now;

                        repPgeneralDocumento.Update(pgeneralDocumento);
                        repDocumento.Update(documento);
                    }
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPut]
        public ActionResult AsociarDocumentosAsociados([FromBody] DocumentoAsociadoProgramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                DocumentoPwRepositorio repDocumento = new DocumentoPwRepositorio(contexto);
                PgeneralDocumentoPwRepositorio repPgeneralDocumento = new PgeneralDocumentoPwRepositorio(contexto);

                using (TransactionScope scope = new TransactionScope())
                {
                    var listaBorrada = repPgeneralDocumento.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Documentos);
                    foreach (var item in Json.Documentos)
                    {
                        if (repPgeneralDocumento.Exist(item.IdPGeneralDocumento))
                        {
                            PgeneralDocumentoPwBO pgeneralDocumento = repPgeneralDocumento.FirstById(item.IdPGeneralDocumento);
                            pgeneralDocumento.IdDocumento = item.IdDocumentos;
                            pgeneralDocumento.IdPgeneral = Json.IdPGeneral;
                            pgeneralDocumento.UsuarioModificacion = Json.Usuario;
                            pgeneralDocumento.FechaModificacion = DateTime.Now;
                            repPgeneralDocumento.Update(pgeneralDocumento);
                        }
                        else
                        {
                            PgeneralDocumentoPwBO pgeneralDocumento = new PgeneralDocumentoPwBO();
                            pgeneralDocumento.IdDocumento = item.IdDocumentos;
                            pgeneralDocumento.IdPgeneral = Json.IdPGeneral;
                            pgeneralDocumento.UsuarioCreacion = Json.Usuario;
                            pgeneralDocumento.UsuarioModificacion = Json.Usuario;
                            pgeneralDocumento.FechaCreacion = DateTime.Now;
                            pgeneralDocumento.FechaModificacion = DateTime.Now;
                            pgeneralDocumento.Estado = true;
                            repPgeneralDocumento.Insert(pgeneralDocumento);
                        }
                        DocumentoPwBO documento = repDocumento.FirstById(item.IdDocumentos);
                        documento.Asignado = true;
                        documento.UsuarioModificacion = Json.Usuario;
                        documento.FechaModificacion = DateTime.Now;
                        repDocumento.Update(documento);
                    }
                    foreach (var item in listaBorrada)
                    {
                        DocumentoPwBO documento = repDocumento.FirstById(item);
                        documento.Asignado = false;
                        documento.UsuarioModificacion = Json.Usuario;
                        documento.FechaModificacion = DateTime.Now;
                        repDocumento.Update(documento);
                    }

                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarCursoAPGeneral([FromBody] PGeneralASubPGeneralInsertDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralAsubPgeneralRepositorio repProgramaPadre = new PgeneralAsubPgeneralRepositorio();
                PGeneralASubPGeneralBO programaPadre = new PGeneralASubPGeneralBO();
                programaPadre.IdPgeneralPadre = Json.PGeneralPadre;
                programaPadre.IdPgeneralHijo = Json.PGeneralHijo;
                programaPadre.Orden = Json.Orden;
                programaPadre.UsuarioCreacion = Json.Usuario;
                programaPadre.UsuarioModificacion = Json.Usuario;
                programaPadre.FechaCreacion = DateTime.Now;
                programaPadre.FechaModificacion = DateTime.Now;
                programaPadre.Estado = true;
                repProgramaPadre.Insert(programaPadre);
                return Ok(programaPadre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarCursoAPGeneral([FromBody] PGeneralASubPGeneralInsertDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralAsubPgeneralRepositorio repProgramaPadre = new PgeneralAsubPgeneralRepositorio(_integraDBContext);
                PgeneralAsubPgeneralVersionProgramaRepositorio _repPgeneralAsubPgeneralVersionPrograma = new PgeneralAsubPgeneralVersionProgramaRepositorio(_integraDBContext);
                PGeneralASubPGeneralBO programaPadre = repProgramaPadre.FirstById(Json.Id);
                if (repProgramaPadre.Exist(w => w.IdPgeneralPadre == programaPadre.IdPgeneralPadre && w.Orden == Json.Orden && w.Id != Json.Id))
                {
                    return BadRequest("Ya Existe una registro asociado con el mismo orden");
                }

                programaPadre.Orden = Json.Orden;
                programaPadre.UsuarioModificacion = Json.Usuario;
                programaPadre.FechaModificacion = DateTime.Now;
                repProgramaPadre.Update(programaPadre);
                if (_repPgeneralAsubPgeneralVersionPrograma.Exist(w => w.IdPgeneralAsubPgeneral == programaPadre.Id))
                {
                    _repPgeneralAsubPgeneralVersionPrograma.Delete(_repPgeneralAsubPgeneralVersionPrograma.GetBy(w => w.IdPgeneralAsubPgeneral == programaPadre.Id).Select(w => w.Id), Json.Usuario);
                }
                foreach (var item in Json.listaVersion)
                {
                    PgeneralAsubPgeneralVersionProgramaBO pgeneralAsubPgeneralVersion = new PgeneralAsubPgeneralVersionProgramaBO();
                    pgeneralAsubPgeneralVersion.IdPgeneralAsubPgeneral = programaPadre.Id;
                    pgeneralAsubPgeneralVersion.IdVersionPrograma = item.Id;
                    pgeneralAsubPgeneralVersion.Estado = true;
                    pgeneralAsubPgeneralVersion.FechaCreacion = DateTime.Now;
                    pgeneralAsubPgeneralVersion.FechaModificacion = DateTime.Now;
                    pgeneralAsubPgeneralVersion.UsuarioCreacion = Json.Usuario;
                    pgeneralAsubPgeneralVersion.UsuarioModificacion = Json.Usuario;

                    _repPgeneralAsubPgeneralVersionPrograma.Insert(pgeneralAsubPgeneralVersion);
                }
                return Ok(programaPadre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpDelete]
        public ActionResult EliminarCursoAPGeneral([FromBody] PGeneralASubPGeneralDeleteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralAsubPgeneralRepositorio repProgramaPadre = new PgeneralAsubPgeneralRepositorio(_integraDBContext);
                PgeneralAsubPgeneralVersionProgramaRepositorio _repPgeneralAsubPgeneralVersionPrograma = new PgeneralAsubPgeneralVersionProgramaRepositorio(_integraDBContext);
                bool result = false;
                if (repProgramaPadre.Exist(Json.IdCursoPGeneral))
                {
                    result = repProgramaPadre.Delete(Json.IdCursoPGeneral, Json.Usuario);

                    if (_repPgeneralAsubPgeneralVersionPrograma.Exist(w => w.IdPgeneralAsubPgeneral == Json.IdCursoPGeneral))
                    {
                        _repPgeneralAsubPgeneralVersionPrograma.Delete(_repPgeneralAsubPgeneralVersionPrograma.GetBy(w => w.IdPgeneralAsubPgeneral == Json.IdCursoPGeneral).Select(w => w.Id), Json.Usuario);
                    }
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarBeneficiosPreRequisitos([FromBody] BeneficioPreRequisitoProgramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ProgramaGeneralPrerequisitoRepositorio repPreRequisito = new ProgramaGeneralPrerequisitoRepositorio(contexto);
                ProgramaGeneralBeneficioRepositorio repBeneficio = new ProgramaGeneralBeneficioRepositorio(contexto);
                ProgramaGeneralBeneficioModalidadRepositorio repBeneficioModalidad = new ProgramaGeneralBeneficioModalidadRepositorio(contexto);
                ProgramaGeneralBeneficioArgumentoRepositorio repBeneficioArgumento = new ProgramaGeneralBeneficioArgumentoRepositorio(contexto);
                ProgramaGeneralPrerequisitoModalidadRepositorio repPreRequisitoModalidad = new ProgramaGeneralPrerequisitoModalidadRepositorio(contexto);


                List<ProgramaGeneralBeneficioArgumentoBO> argumentos;
                List<ProgramaGeneralBeneficioModalidadBO> modalidadBeneficios;
                List<ProgramaGeneralPrerequisitoModalidadBO> modalidadPreRequisito;
                bool flagBeficios = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    var eliminadosPreRequisitos = repPreRequisito.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.PreRequisitos);
                    var eliminadosBeneficios = repBeneficio.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Beneficios);
                    repBeneficioModalidad.EliminacionLogicoPorListaBeneficio(Json.Usuario, eliminadosBeneficios);
                    repBeneficioArgumento.EliminacionLogicoPorListaBeneficios(Json.Usuario, eliminadosBeneficios);
                    repPreRequisitoModalidad.EliminacionLogicoPorListaPrerequisito(Json.Usuario, eliminadosPreRequisitos);

                    foreach (var item in Json.Beneficios)
                    {

                        ProgramaGeneralBeneficioBO beneficio;
                        if (repBeneficio.Exist(item.IdBeneficio))
                        {

                            beneficio = repBeneficio.FirstById(item.IdBeneficio);
                            beneficio.IdPgeneral = item.IdPGeneral;
                            beneficio.Nombre = item.NombreBeneficio;
                            beneficio.UsuarioModificacion = Json.Usuario;
                            beneficio.FechaModificacion = DateTime.Now;

                            repBeneficioArgumento.EliminacionLogicoPorBeneficio(item.IdBeneficio, Json.Usuario, item.BeneficiosArgumentos);
                            repBeneficioModalidad.EliminacionLogicoPorBeneficio(item.IdBeneficio, Json.Usuario, item.Modalidades);
                        }
                        else
                        {
                            beneficio = new ProgramaGeneralBeneficioBO();
                            beneficio.IdPgeneral = item.IdPGeneral;
                            beneficio.Nombre = item.NombreBeneficio;
                            beneficio.UsuarioCreacion = Json.Usuario;
                            beneficio.UsuarioModificacion = Json.Usuario;
                            beneficio.FechaCreacion = DateTime.Now;
                            beneficio.FechaModificacion = DateTime.Now;
                            beneficio.Estado = true;
                            flagBeficios = true;
                        }
                        argumentos = new List<ProgramaGeneralBeneficioArgumentoBO>();
                        foreach (var subItem in item.BeneficiosArgumentos)
                        {
                            ProgramaGeneralBeneficioArgumentoBO argumento;
                            if (repBeneficioArgumento.Exist(subItem.Id ?? 0))
                            {
                                argumento = repBeneficioArgumento.FirstById(subItem.Id ?? 0);
                                argumento.Nombre = subItem.Nombre;
                                argumento.IdPgeneral = item.IdPGeneral;
                                beneficio.UsuarioModificacion = Json.Usuario;
                                beneficio.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                argumento = new ProgramaGeneralBeneficioArgumentoBO();
                                argumento.Nombre = subItem.Nombre;
                                argumento.IdPgeneral = item.IdPGeneral;
                                argumento.UsuarioCreacion = Json.Usuario;
                                argumento.UsuarioModificacion = Json.Usuario;
                                argumento.FechaCreacion = DateTime.Now;
                                argumento.FechaModificacion = DateTime.Now;
                                argumento.Estado = true;
                            }
                            argumentos.Add(argumento);
                        }
                        modalidadBeneficios = new List<ProgramaGeneralBeneficioModalidadBO>();
                        foreach (var subItem in item.Modalidades)
                        {
                            ProgramaGeneralBeneficioModalidadBO modalidad;
                            if (repBeneficioArgumento.Exist(subItem.Id))
                            {
                                modalidad = repBeneficioModalidad.FirstById(subItem.Id);
                                modalidad.Nombre = subItem.Nombre;
                                modalidad.IdPgeneral = item.IdPGeneral;
                                modalidad.IdModalidadCurso = subItem.IdModalidad;
                                modalidad.UsuarioModificacion = Json.Usuario;
                                modalidad.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                modalidad = new ProgramaGeneralBeneficioModalidadBO();
                                modalidad.Nombre = subItem.Nombre;
                                modalidad.IdPgeneral = item.IdPGeneral;
                                modalidad.IdModalidadCurso = subItem.IdModalidad;
                                modalidad.UsuarioCreacion = Json.Usuario;
                                modalidad.UsuarioModificacion = Json.Usuario;
                                modalidad.FechaCreacion = DateTime.Now;
                                modalidad.FechaModificacion = DateTime.Now;
                                modalidad.Estado = true;
                            }
                            modalidadBeneficios.Add(modalidad);
                        }
                        beneficio.programaGeneralBeneficioArgumento = argumentos;
                        beneficio.ProgramaGeneralBeneficioModalidad = modalidadBeneficios;
                        if (flagBeficios)
                        {
                            repBeneficio.Insert(beneficio);
                        }
                        else
                        {
                            repBeneficio.Update(beneficio);
                        }
                        flagBeficios = false;

                    }
                    bool flagRequisitos = false;
                    foreach (var item in Json.PreRequisitos)
                    {

                        ProgramaGeneralPrerequisitoBO preRequisito;
                        if (repPreRequisito.Exist(item.IdPreRequisito))
                        {
                            preRequisito = repPreRequisito.FirstById(item.IdPreRequisito);
                            preRequisito.Nombre = item.NombrePreRequisito;
                            preRequisito.Tipo = item.Tipo;
                            preRequisito.Orden = item.Orden;
                            preRequisito.UsuarioModificacion = Json.Usuario;
                            preRequisito.FechaModificacion = DateTime.Now;

                            repPreRequisitoModalidad.EliminacionLogicoPorBeneficio(item.IdPreRequisito, Json.Usuario, item.Modalidades);
                        }
                        else
                        {
                            preRequisito = new ProgramaGeneralPrerequisitoBO();
                            preRequisito.Nombre = item.NombrePreRequisito;
                            preRequisito.IdPgeneral = item.IdPGeneral;
                            preRequisito.Tipo = item.Tipo;
                            preRequisito.Orden = item.Orden;
                            preRequisito.UsuarioCreacion = Json.Usuario;
                            preRequisito.UsuarioModificacion = Json.Usuario;
                            preRequisito.FechaCreacion = DateTime.Now;
                            preRequisito.FechaModificacion = DateTime.Now;
                            preRequisito.Estado = true;
                            flagRequisitos = true;
                        }
                        modalidadPreRequisito = new List<ProgramaGeneralPrerequisitoModalidadBO>();
                        foreach (var subItem in item.Modalidades)
                        {
                            ProgramaGeneralPrerequisitoModalidadBO preRequisitoModalidad;
                            if (repPreRequisitoModalidad.Exist(subItem.Id))
                            {
                                preRequisitoModalidad = repPreRequisitoModalidad.FirstById(subItem.Id);
                                preRequisitoModalidad.Nombre = subItem.Nombre;
                                preRequisitoModalidad.IdModalidadCurso = subItem.IdModalidad;
                                preRequisitoModalidad.IdPgeneral = item.IdPGeneral;
                                preRequisito.UsuarioModificacion = Json.Usuario;
                                preRequisito.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                preRequisitoModalidad = new ProgramaGeneralPrerequisitoModalidadBO();
                                preRequisitoModalidad.Nombre = subItem.Nombre;
                                preRequisitoModalidad.IdModalidadCurso = subItem.IdModalidad;
                                preRequisitoModalidad.IdPgeneral = item.IdPGeneral;
                                preRequisitoModalidad.UsuarioCreacion = Json.Usuario;
                                preRequisitoModalidad.UsuarioModificacion = Json.Usuario;
                                preRequisitoModalidad.FechaCreacion = DateTime.Now;
                                preRequisitoModalidad.FechaModificacion = DateTime.Now;
                                preRequisitoModalidad.Estado = true;
                            }
                            modalidadPreRequisito.Add(preRequisitoModalidad);
                        }
                        preRequisito.ProgramaGeneralPrerequisitoModalidad = modalidadPreRequisito;
                        if (flagRequisitos)
                        {
                            repPreRequisito.Insert(preRequisito);
                        }
                        else
                        {
                            repPreRequisito.Update(preRequisito);
                        }
                        flagRequisitos = false;
                    }
                    scope.Complete();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Guarda un PreRequisito en Especifico // LPPG
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarPreRequisitos([FromBody] CompuestoPreRequisitoModalidadAlternaDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ProgramaGeneralPrerequisitoRepositorio repPreRequisito = new ProgramaGeneralPrerequisitoRepositorio(contexto);
                ProgramaGeneralPrerequisitoModalidadRepositorio repPreRequisitoModalidad = new ProgramaGeneralPrerequisitoModalidadRepositorio(contexto);
                bool flagRequisitos = false;
                List<ProgramaGeneralPrerequisitoModalidadBO> modalidadPreRequisito;

                ProgramaGeneralPrerequisitoBO preRequisito;
                if (repPreRequisito.Exist(Json.IdPreRequisito))
                {
                    preRequisito = repPreRequisito.FirstById(Json.IdPreRequisito);
                    preRequisito.Nombre = Json.NombrePreRequisito;
                    preRequisito.Tipo = Json.Tipo;
                    //preRequisito.Orden = Json.Orden;
                    preRequisito.UsuarioModificacion = Json.Usuario;
                    preRequisito.FechaModificacion = DateTime.Now;

                    repPreRequisitoModalidad.EliminacionLogicoPorBeneficio(Json.IdPreRequisito, Json.Usuario, Json.Modalidades);
                }
                else
                {
                    preRequisito = new ProgramaGeneralPrerequisitoBO();
                    preRequisito.Nombre = Json.NombrePreRequisito;
                    preRequisito.IdPgeneral = Json.IdPGeneral;
                    preRequisito.Tipo = Json.Tipo;
                    preRequisito.Orden = Json.Orden;
                    preRequisito.UsuarioCreacion = Json.Usuario;
                    preRequisito.UsuarioModificacion = Json.Usuario;
                    preRequisito.FechaCreacion = DateTime.Now;
                    preRequisito.FechaModificacion = DateTime.Now;
                    preRequisito.Estado = true;
                    flagRequisitos = true;
                }
                modalidadPreRequisito = new List<ProgramaGeneralPrerequisitoModalidadBO>();
                foreach (var subItem in Json.Modalidades)
                {
                    ProgramaGeneralPrerequisitoModalidadBO preRequisitoModalidad;
                    if (repPreRequisitoModalidad.Exist(subItem.Id))
                    {
                        preRequisitoModalidad = repPreRequisitoModalidad.FirstById(subItem.Id);
                        preRequisitoModalidad.Nombre = subItem.Nombre;
                        preRequisitoModalidad.IdModalidadCurso = subItem.IdModalidad;
                        preRequisitoModalidad.IdPgeneral = Json.IdPGeneral;
                        preRequisito.UsuarioModificacion = Json.Usuario;
                        preRequisito.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        preRequisitoModalidad = new ProgramaGeneralPrerequisitoModalidadBO();
                        preRequisitoModalidad.Nombre = subItem.Nombre;
                        preRequisitoModalidad.IdModalidadCurso = subItem.IdModalidad;
                        preRequisitoModalidad.IdPgeneral = Json.IdPGeneral;
                        preRequisitoModalidad.UsuarioCreacion = Json.Usuario;
                        preRequisitoModalidad.UsuarioModificacion = Json.Usuario;
                        preRequisitoModalidad.FechaCreacion = DateTime.Now;
                        preRequisitoModalidad.FechaModificacion = DateTime.Now;
                        preRequisitoModalidad.Estado = true;
                    }
                    modalidadPreRequisito.Add(preRequisitoModalidad);
                }
                preRequisito.ProgramaGeneralPrerequisitoModalidad = modalidadPreRequisito;
                if (flagRequisitos)
                {
                    repPreRequisito.Insert(preRequisito);
                }
                else
                {
                    repPreRequisito.Update(preRequisito);
                }
                flagRequisitos = false;

                return Ok(preRequisito);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Eliminar BeneficioVenta
        [Route("[action]/{IdPrerequisito}/{Usuario}/{ListaModalidades}")]
        [HttpGet]
        public ActionResult EliminarPreRequisitos(int IdPrerequisito, string Usuario, List<ModalidadCursoDTO> ListaModalidades)
        {

            try
            {
                integraDBContext contexto = new integraDBContext();
                ProgramaGeneralPrerequisitoRepositorio repPreRequisito = new ProgramaGeneralPrerequisitoRepositorio(contexto);
                ProgramaGeneralPrerequisitoModalidadRepositorio repPreRequisitoModalidad = new ProgramaGeneralPrerequisitoModalidadRepositorio(contexto);

                repPreRequisito.Delete(IdPrerequisito, Usuario);
                repPreRequisitoModalidad.EliminacionLogicoPorBeneficio(IdPrerequisito, Usuario, ListaModalidades);
                bool flagRequisitos = false;
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarBeneficiosVentas([FromBody] BeneficioVentasDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralBeneficioRepositorio repBeneficio = new ProgramaGeneralBeneficioRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioModalidadRepositorio repBeneficioModalidad = new ProgramaGeneralBeneficioModalidadRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioArgumentoRepositorio repBeneficioArgumento = new ProgramaGeneralBeneficioArgumentoRepositorio(_integraDBContext);


                List<ProgramaGeneralBeneficioArgumentoBO> argumentos;
                List<ProgramaGeneralBeneficioModalidadBO> modalidadBeneficios;
                bool flagBeficios = false;
                ProgramaGeneralBeneficioBO beneficio;
                using (TransactionScope scope = new TransactionScope())
                {

                    //var eliminadosBeneficios = repBeneficio.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Beneficios);
                    //repBeneficioModalidad.EliminacionLogicoPorListaBeneficio(Json.Usuario, eliminadosBeneficios);
                    //repBeneficioArgumento.EliminacionLogicoPorListaBeneficios(Json.Usuario, eliminadosBeneficios);                    


                    if (repBeneficio.Exist(Json.Beneficios.IdBeneficio))
                    {

                        beneficio = repBeneficio.FirstById(Json.Beneficios.IdBeneficio);
                        beneficio.IdPgeneral = Json.Beneficios.IdPGeneral;
                        beneficio.Nombre = Json.Beneficios.NombreBeneficio;
                        beneficio.UsuarioModificacion = Json.Usuario;
                        beneficio.FechaModificacion = DateTime.Now;

                        repBeneficioArgumento.EliminacionLogicoPorBeneficio(Json.Beneficios.IdBeneficio, Json.Usuario, Json.Beneficios.BeneficiosArgumentos);
                        repBeneficioModalidad.EliminacionLogicoPorBeneficio(Json.Beneficios.IdBeneficio, Json.Usuario, Json.Beneficios.Modalidades);
                    }
                    else
                    {
                        beneficio = new ProgramaGeneralBeneficioBO();
                        beneficio.IdPgeneral = Json.Beneficios.IdPGeneral;
                        beneficio.Nombre = Json.Beneficios.NombreBeneficio;
                        beneficio.UsuarioCreacion = Json.Usuario;
                        beneficio.UsuarioModificacion = Json.Usuario;
                        beneficio.FechaCreacion = DateTime.Now;
                        beneficio.FechaModificacion = DateTime.Now;
                        beneficio.Estado = true;
                        flagBeficios = true;
                    }
                    argumentos = new List<ProgramaGeneralBeneficioArgumentoBO>();
                    foreach (var subItem in Json.Beneficios.BeneficiosArgumentos)
                    {
                        ProgramaGeneralBeneficioArgumentoBO argumento;
                        if (repBeneficioArgumento.Exist(subItem.Id ?? 0))
                        {
                            argumento = repBeneficioArgumento.FirstById(subItem.Id ?? 0);
                            argumento.Nombre = subItem.Nombre;
                            argumento.IdPgeneral = Json.Beneficios.IdPGeneral;
                            beneficio.UsuarioModificacion = Json.Usuario;
                            beneficio.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            argumento = new ProgramaGeneralBeneficioArgumentoBO();
                            argumento.Nombre = subItem.Nombre;
                            argumento.IdPgeneral = Json.Beneficios.IdPGeneral;
                            argumento.UsuarioCreacion = Json.Usuario;
                            argumento.UsuarioModificacion = Json.Usuario;
                            argumento.FechaCreacion = DateTime.Now;
                            argumento.FechaModificacion = DateTime.Now;
                            argumento.Estado = true;
                        }
                        argumentos.Add(argumento);
                    }
                    modalidadBeneficios = new List<ProgramaGeneralBeneficioModalidadBO>();
                    foreach (var subItem in Json.Beneficios.Modalidades)
                    {
                        ProgramaGeneralBeneficioModalidadBO modalidad;
                        if (repBeneficioArgumento.Exist(subItem.Id))
                        {
                            modalidad = repBeneficioModalidad.FirstById(subItem.Id);
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Beneficios.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralBeneficioModalidadBO();
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Beneficios.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        modalidadBeneficios.Add(modalidad);
                    }
                    beneficio.programaGeneralBeneficioArgumento = argumentos;
                    beneficio.ProgramaGeneralBeneficioModalidad = modalidadBeneficios;
                    if (flagBeficios)
                    {
                        repBeneficio.Insert(beneficio);
                    }
                    else
                    {
                        repBeneficio.Update(beneficio);
                    }

                    scope.Complete();
                }

                return Ok(beneficio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Eliminar BeneficioVenta
        [Route("[action]/{IdProgramaGeneralBeneficio}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarBeneficioVenta(int IdProgramaGeneralBeneficio, string Usuario)
        {

            try
            {
                ProgramaGeneralBeneficioRepositorio repBeneficio = new ProgramaGeneralBeneficioRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioModalidadRepositorio repBeneficioModalidad = new ProgramaGeneralBeneficioModalidadRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioArgumentoRepositorio repBeneficioArgumento = new ProgramaGeneralBeneficioArgumentoRepositorio(_integraDBContext);

                repBeneficio.Delete(IdProgramaGeneralBeneficio, Usuario);

                repBeneficioModalidad.EliminacionLogicoPorIdBeneficio(IdProgramaGeneralBeneficio, Usuario);
                repBeneficioArgumento.EliminacionLogicoPorIdBeneficio(IdProgramaGeneralBeneficio, Usuario);

                bool flagBeficios = false;


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarPerfilContactoPrograma([FromBody] CompuestoPerfilContactoProgramaDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ProgramaGeneralPerfilScoringCiudadRepositorio repCiudadScoring = new ProgramaGeneralPerfilScoringCiudadRepositorio(contexto);
                ProgramaGeneralPerfilScoringModalidadRepositorio repModalidadScoring = new ProgramaGeneralPerfilScoringModalidadRepositorio(contexto);
                ProgramaGeneralPerfilScoringAformacionRepositorio repFormacionScoring = new ProgramaGeneralPerfilScoringAformacionRepositorio(contexto);
                ProgramaGeneralPerfilScoringIndustriaRepositorio repIndustriaScoring = new ProgramaGeneralPerfilScoringIndustriaRepositorio(contexto);
                ProgramaGeneralPerfilScoringCargoRepositorio repCargoScoring = new ProgramaGeneralPerfilScoringCargoRepositorio(contexto);
                ProgramaGeneralPerfilScoringAtrabajoRepositorio repTrabajoScoring = new ProgramaGeneralPerfilScoringAtrabajoRepositorio(contexto);
                ProgramaGeneralPerfilScoringCategoriaRepositorio repCategoraiScoring = new ProgramaGeneralPerfilScoringCategoriaRepositorio(contexto);

                ProgramaGeneralPerfilCiudadCoeficienteRepositorio repCiudadCoeficiente = new ProgramaGeneralPerfilCiudadCoeficienteRepositorio(contexto);
                ProgramaGeneralPerfilModalidadCoeficienteRepositorio repModalidadCoeficiente = new ProgramaGeneralPerfilModalidadCoeficienteRepositorio(contexto);
                ProgramaGeneralPerfilCategoriaCoeficienteRepositorio repCategoriaCoeficiente = new ProgramaGeneralPerfilCategoriaCoeficienteRepositorio(contexto);
                ProgramaGeneralPerfilCargoCoeficienteRepositorio repCargoCoeficiente = new ProgramaGeneralPerfilCargoCoeficienteRepositorio(contexto);
                ProgramaGeneralPerfilIndustriaCoeficienteRepositorio repIndustriaCoeficiente = new ProgramaGeneralPerfilIndustriaCoeficienteRepositorio(contexto);
                ProgramaGeneralPerfilAformacionCoeficienteRepositorio repAFormacionCoeficiente = new ProgramaGeneralPerfilAformacionCoeficienteRepositorio(contexto);
                ProgramaGeneralPerfilAtrabajoCoeficienteRepositorio repAreaTrabajoCoeficiente = new ProgramaGeneralPerfilAtrabajoCoeficienteRepositorio(contexto);

                ProgramaGeneralPerfilTipoDatoRepositorio repTipoDato = new ProgramaGeneralPerfilTipoDatoRepositorio(contexto);
                ProgramaGeneralPerfilEscalaProbabilidadRepositorio repEscala = new ProgramaGeneralPerfilEscalaProbabilidadRepositorio(contexto);
                ProgramaGeneralPerfilInterceptoRepositorio repIntercepto = new ProgramaGeneralPerfilInterceptoRepositorio(contexto);

                PgeneralRepositorio repPgeneral = new PgeneralRepositorio(contexto);
                using (TransactionScope scope = new TransactionScope())
                {
                    repCiudadScoring.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Ciudad.CiudadEscoring);
                    repModalidadScoring.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Modalidad.ModalidadEscoring);
                    repFormacionScoring.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Formacion.FormacionEscoring);
                    repIndustriaScoring.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Industria.IndustriaEscoring);
                    repCargoScoring.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Cargo.CargoEscoring);
                    repTrabajoScoring.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Trabajo.TrabajoEscoring);
                    repCategoraiScoring.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Categoria.CategoriaEscoring);

                    repCiudadCoeficiente.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Ciudad.CiudadCoefiente);
                    repModalidadCoeficiente.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Modalidad.ModalidadCoefiente);
                    repCategoriaCoeficiente.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Categoria.CategoriaCoefiente);
                    repCargoCoeficiente.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Cargo.CargoCoefiente);
                    repIndustriaCoeficiente.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Industria.IndustriaCoefiente);
                    repAFormacionCoeficiente.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Formacion.FormacionCoefiente);
                    repAreaTrabajoCoeficiente.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Trabajo.TrabajoCoefiente);

                    repTipoDato.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.TipoDato);
                    repEscala.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Escala);

                    PgeneralBO pgeneral = new PgeneralBO(Json.IdPGeneral, contexto);

                    pgeneral.ProgramaGeneralPerfilScoringCiudad = new List<ProgramaGeneralPerfilScoringCiudadBO>();
                    pgeneral.ProgramaGeneralPerfilCiudadCoeficiente = new List<ProgramaGeneralPerfilCiudadCoeficienteBO>();
                    foreach (var item in Json.Ciudad.CiudadEscoring)
                    {
                        ProgramaGeneralPerfilScoringCiudadBO ciudad;
                        if (repCiudadScoring.Exist(item.Id))
                        {
                            ciudad = repCiudadScoring.FirstById(item.Id);
                            ciudad.Nombre = item.Nombre;
                            ciudad.IdCiudad = item.IdCiudad;
                            ciudad.IdSelect = item.IdSelect;
                            ciudad.Valor = item.Valor;
                            ciudad.Fila = item.Fila;
                            ciudad.Columna = item.Columna;
                            ciudad.Validar = item.Validar;
                            ciudad.FechaModificacion = DateTime.Now;
                            ciudad.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            ciudad = new ProgramaGeneralPerfilScoringCiudadBO();
                            ciudad.Nombre = item.Nombre;
                            ciudad.IdPgeneral = item.IdPGeneral;
                            ciudad.IdCiudad = item.IdCiudad;
                            ciudad.IdSelect = item.IdSelect;
                            ciudad.Valor = item.Valor;
                            ciudad.Fila = item.Fila;
                            ciudad.Columna = item.Columna;
                            ciudad.Validar = item.Validar;
                            ciudad.UsuarioCreacion = Json.Usuario;
                            ciudad.UsuarioModificacion = Json.Usuario;
                            ciudad.FechaCreacion = DateTime.Now;
                            ciudad.FechaModificacion = DateTime.Now;
                            ciudad.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilScoringCiudad.Add(ciudad);
                    }
                    foreach (var item in Json.Ciudad.CiudadCoefiente)
                    {
                        ProgramaGeneralPerfilCiudadCoeficienteBO ciudad;
                        if (repCiudadCoeficiente.Exist(item.Id))
                        {
                            ciudad = repCiudadCoeficiente.FirstById(item.Id);
                            ciudad.Nombre = item.Nombre;
                            ciudad.Coeficiente = item.Coeficiente;
                            ciudad.IdSelect = item.IdSelect;
                            ciudad.Columna = item.IdColumna;
                            ciudad.FechaModificacion = DateTime.Now;
                            ciudad.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            ciudad = new ProgramaGeneralPerfilCiudadCoeficienteBO();
                            ciudad.Nombre = item.Nombre;
                            ciudad.IdPgeneral = item.IdPGeneral;
                            ciudad.Coeficiente = item.Coeficiente;
                            ciudad.IdSelect = item.IdSelect;
                            ciudad.Columna = item.IdColumna;
                            ciudad.UsuarioCreacion = Json.Usuario;
                            ciudad.UsuarioModificacion = Json.Usuario;
                            ciudad.FechaCreacion = DateTime.Now;
                            ciudad.FechaModificacion = DateTime.Now;
                            ciudad.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilCiudadCoeficiente.Add(ciudad);
                    }
                    pgeneral.ProgramaGeneralPerfilScoringModalidad = new List<ProgramaGeneralPerfilScoringModalidadBO>();
                    pgeneral.ProgramaGeneralPerfilModalidadCoeficiente = new List<ProgramaGeneralPerfilModalidadCoeficienteBO>();
                    foreach (var item in Json.Modalidad.ModalidadEscoring)
                    {
                        ProgramaGeneralPerfilScoringModalidadBO modalidad;
                        if (repModalidadScoring.Exist(item.Id))
                        {
                            modalidad = repModalidadScoring.FirstById(item.Id);
                            modalidad.Nombre = item.Nombre;
                            modalidad.IdModalidadCurso = item.IdModalidad;
                            modalidad.IdSelect = item.IdSelect;
                            modalidad.Valor = item.Valor;
                            modalidad.Fila = item.Fila;
                            modalidad.Columna = item.Columna;
                            modalidad.Validar = item.Validar;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralPerfilScoringModalidadBO();
                            modalidad.Nombre = item.Nombre;
                            modalidad.IdPgeneral = item.IdPGeneral;
                            modalidad.IdModalidadCurso = item.IdModalidad;
                            modalidad.IdSelect = item.IdSelect;
                            modalidad.Valor = item.Valor;
                            modalidad.Fila = item.Fila;
                            modalidad.Columna = item.Columna;
                            modalidad.Validar = item.Validar;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilScoringModalidad.Add(modalidad);
                    }
                    foreach (var item in Json.Modalidad.ModalidadCoefiente)
                    {
                        ProgramaGeneralPerfilModalidadCoeficienteBO modalidad;
                        if (repModalidadCoeficiente.Exist(item.Id))
                        {
                            modalidad = repModalidadCoeficiente.FirstById(item.Id);
                            modalidad.Nombre = item.Nombre;
                            modalidad.Coeficiente = item.Coeficiente;
                            modalidad.IdSelect = item.IdSelect;
                            modalidad.Columna = item.IdColumna;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralPerfilModalidadCoeficienteBO();
                            modalidad.Nombre = item.Nombre;
                            modalidad.IdPgeneral = item.IdPGeneral;
                            modalidad.Coeficiente = item.Coeficiente;
                            modalidad.IdSelect = item.IdSelect;
                            modalidad.Columna = item.IdColumna;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilModalidadCoeficiente.Add(modalidad);
                    }
                    pgeneral.ProgramaGeneralPerfilScoringAformacion = new List<ProgramaGeneralPerfilScoringAformacionBO>();
                    pgeneral.ProgramaGeneralPerfilAformacionCoeficiente = new List<ProgramaGeneralPerfilAformacionCoeficienteBO>();
                    foreach (var item in Json.Formacion.FormacionEscoring)
                    {
                        ProgramaGeneralPerfilScoringAformacionBO formacion;
                        if (repFormacionScoring.Exist(item.Id))
                        {
                            formacion = repFormacionScoring.FirstById(item.Id);
                            formacion.Nombre = item.Nombre;
                            formacion.IdAreaFormacion = item.IdAreaFormacion;
                            formacion.IdSelect = item.IdSelect;
                            formacion.Valor = item.Valor;
                            formacion.Fila = item.Fila;
                            formacion.Columna = item.Columna;
                            formacion.Validar = item.Validar;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            formacion = new ProgramaGeneralPerfilScoringAformacionBO();
                            formacion.Nombre = item.Nombre;
                            formacion.IdPgeneral = item.IdPGeneral;
                            formacion.IdAreaFormacion = item.IdAreaFormacion;
                            formacion.IdSelect = item.IdSelect;
                            formacion.Valor = item.Valor;
                            formacion.Fila = item.Fila;
                            formacion.Columna = item.Columna;
                            formacion.Validar = item.Validar;
                            formacion.UsuarioCreacion = Json.Usuario;
                            formacion.UsuarioModificacion = Json.Usuario;
                            formacion.FechaCreacion = DateTime.Now;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilScoringAformacion.Add(formacion);
                    }
                    foreach (var item in Json.Formacion.FormacionCoefiente)
                    {
                        ProgramaGeneralPerfilAformacionCoeficienteBO formacion;
                        if (repAFormacionCoeficiente.Exist(item.Id))
                        {
                            formacion = repAFormacionCoeficiente.FirstById(item.Id);
                            formacion.Nombre = item.Nombre;
                            formacion.Coeficiente = item.Coeficiente;
                            formacion.IdSelect = item.IdSelect;
                            formacion.Columna = item.IdColumna;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            formacion = new ProgramaGeneralPerfilAformacionCoeficienteBO();
                            formacion.Nombre = item.Nombre;
                            formacion.IdPgeneral = item.IdPGeneral;
                            formacion.Coeficiente = item.Coeficiente;
                            formacion.IdSelect = item.IdSelect;
                            formacion.Columna = item.IdColumna;
                            formacion.UsuarioCreacion = Json.Usuario;
                            formacion.UsuarioModificacion = Json.Usuario;
                            formacion.FechaCreacion = DateTime.Now;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilAformacionCoeficiente.Add(formacion);
                    }
                    pgeneral.ProgramaGeneralPerfilScoringIndustria = new List<ProgramaGeneralPerfilScoringIndustriaBO>();
                    pgeneral.ProgramaGeneralPerfilIndustriaCoeficiente = new List<ProgramaGeneralPerfilIndustriaCoeficienteBO>();
                    foreach (var item in Json.Industria.IndustriaEscoring)
                    {
                        ProgramaGeneralPerfilScoringIndustriaBO industria;
                        if (repIndustriaScoring.Exist(item.Id))
                        {
                            industria = repIndustriaScoring.FirstById(item.Id);
                            industria.Nombre = item.Nombre;
                            industria.IdIndustria = item.IdIndustria;
                            industria.IdSelect = item.IdSelect;
                            industria.Valor = item.Valor;
                            industria.Fila = item.Fila;
                            industria.Columna = item.Columna;
                            industria.Validar = item.Validar;
                            industria.FechaModificacion = DateTime.Now;
                            industria.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            industria = new ProgramaGeneralPerfilScoringIndustriaBO();
                            industria.Nombre = item.Nombre;
                            industria.IdPgeneral = item.IdPGeneral;
                            industria.IdIndustria = item.IdIndustria;
                            industria.IdSelect = item.IdSelect;
                            industria.Valor = item.Valor;
                            industria.Fila = item.Fila;
                            industria.Columna = item.Columna;
                            industria.Validar = item.Validar;
                            industria.UsuarioCreacion = Json.Usuario;
                            industria.UsuarioModificacion = Json.Usuario;
                            industria.FechaCreacion = DateTime.Now;
                            industria.FechaModificacion = DateTime.Now;
                            industria.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilScoringIndustria.Add(industria);
                    }
                    foreach (var item in Json.Industria.IndustriaCoefiente)
                    {
                        ProgramaGeneralPerfilIndustriaCoeficienteBO industria;
                        if (repIndustriaCoeficiente.Exist(item.Id))
                        {
                            industria = repIndustriaCoeficiente.FirstById(item.Id);
                            industria.Nombre = item.Nombre;
                            industria.Coeficiente = item.Coeficiente;
                            industria.IdSelect = item.IdSelect;
                            industria.Columna = item.IdColumna;
                            industria.FechaModificacion = DateTime.Now;
                            industria.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            industria = new ProgramaGeneralPerfilIndustriaCoeficienteBO();
                            industria.Nombre = item.Nombre;
                            industria.IdPgeneral = item.IdPGeneral;
                            industria.Coeficiente = item.Coeficiente;
                            industria.IdSelect = item.IdSelect;
                            industria.Columna = item.IdColumna;
                            industria.UsuarioCreacion = Json.Usuario;
                            industria.UsuarioModificacion = Json.Usuario;
                            industria.FechaCreacion = DateTime.Now;
                            industria.FechaModificacion = DateTime.Now;
                            industria.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilIndustriaCoeficiente.Add(industria);
                    }
                    pgeneral.ProgramaGeneralPerfilScoringCargo = new List<ProgramaGeneralPerfilScoringCargoBO>();
                    pgeneral.ProgramaGeneralPerfilCargoCoeficiente = new List<ProgramaGeneralPerfilCargoCoeficienteBO>();
                    foreach (var item in Json.Cargo.CargoEscoring)
                    {
                        ProgramaGeneralPerfilScoringCargoBO cargo;
                        if (repCargoScoring.Exist(item.Id))
                        {
                            cargo = repCargoScoring.FirstById(item.Id);
                            cargo.Nombre = item.Nombre;
                            cargo.IdCargo = item.IdCargo;
                            cargo.IdSelect = item.IdSelect;
                            cargo.Valor = item.Valor;
                            cargo.Fila = item.Fila;
                            cargo.Columna = item.Columna;
                            cargo.Validar = item.Validar;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            cargo = new ProgramaGeneralPerfilScoringCargoBO();
                            cargo.Nombre = item.Nombre;
                            cargo.IdPgeneral = item.IdPGeneral;
                            cargo.IdCargo = item.IdCargo;
                            cargo.IdSelect = item.IdSelect;
                            cargo.Valor = item.Valor;
                            cargo.Fila = item.Fila;
                            cargo.Columna = item.Columna;
                            cargo.Validar = item.Validar;
                            cargo.UsuarioCreacion = Json.Usuario;
                            cargo.UsuarioModificacion = Json.Usuario;
                            cargo.FechaCreacion = DateTime.Now;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilScoringCargo.Add(cargo);
                    }
                    foreach (var item in Json.Cargo.CargoCoefiente)
                    {
                        ProgramaGeneralPerfilCargoCoeficienteBO cargo;
                        if (repCargoCoeficiente.Exist(item.Id))
                        {
                            cargo = repCargoCoeficiente.FirstById(item.Id);
                            cargo.Nombre = item.Nombre;
                            cargo.Coeficiente = item.Coeficiente;
                            cargo.IdSelect = item.IdSelect;
                            cargo.Columna = item.IdColumna;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            cargo = new ProgramaGeneralPerfilCargoCoeficienteBO();
                            cargo.Nombre = item.Nombre;
                            cargo.IdPgeneral = item.IdPGeneral;
                            cargo.Coeficiente = item.Coeficiente;
                            cargo.IdSelect = item.IdSelect;
                            cargo.Columna = item.IdColumna;
                            cargo.UsuarioCreacion = Json.Usuario;
                            cargo.UsuarioModificacion = Json.Usuario;
                            cargo.FechaCreacion = DateTime.Now;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilCargoCoeficiente.Add(cargo);
                    }

                    pgeneral.ProgramaGeneralPerfilScoringAtrabajo = new List<ProgramaGeneralPerfilScoringAtrabajoBO>();
                    pgeneral.ProgramaGeneralPerfilAtrabajoCoeficiente = new List<ProgramaGeneralPerfilAtrabajoCoeficienteBO>();
                    foreach (var item in Json.Trabajo.TrabajoEscoring)
                    {
                        ProgramaGeneralPerfilScoringAtrabajoBO trabajo;
                        if (repTrabajoScoring.Exist(item.Id))
                        {
                            trabajo = repTrabajoScoring.FirstById(item.Id);
                            trabajo.Nombre = item.Nombre;
                            trabajo.IdAreaTrabajo = item.IdAreaTrabajo;
                            trabajo.IdSelect = item.IdSelect;
                            trabajo.Valor = item.Valor;
                            trabajo.Fila = item.Fila;
                            trabajo.Columna = item.Columna;
                            trabajo.Validar = item.Validar;
                            trabajo.FechaModificacion = DateTime.Now;
                            trabajo.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            trabajo = new ProgramaGeneralPerfilScoringAtrabajoBO();
                            trabajo.Nombre = item.Nombre;
                            trabajo.IdPgeneral = item.IdPGeneral;
                            trabajo.IdAreaTrabajo = item.IdAreaTrabajo;
                            trabajo.IdSelect = item.IdSelect;
                            trabajo.Valor = item.Valor;
                            trabajo.Fila = item.Fila;
                            trabajo.Columna = item.Columna;
                            trabajo.Validar = item.Validar;
                            trabajo.UsuarioCreacion = Json.Usuario;
                            trabajo.UsuarioModificacion = Json.Usuario;
                            trabajo.FechaCreacion = DateTime.Now;
                            trabajo.FechaModificacion = DateTime.Now;
                            trabajo.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilScoringAtrabajo.Add(trabajo);
                    }
                    foreach (var item in Json.Trabajo.TrabajoCoefiente)
                    {
                        ProgramaGeneralPerfilAtrabajoCoeficienteBO trabajo;
                        if (repAreaTrabajoCoeficiente.Exist(item.Id))
                        {
                            trabajo = repAreaTrabajoCoeficiente.FirstById(item.Id);
                            trabajo.Nombre = item.Nombre;
                            trabajo.Coeficiente = item.Coeficiente;
                            trabajo.IdSelect = item.IdSelect;
                            trabajo.Columna = item.IdColumna;
                            trabajo.FechaModificacion = DateTime.Now;
                            trabajo.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            trabajo = new ProgramaGeneralPerfilAtrabajoCoeficienteBO();
                            trabajo.Nombre = item.Nombre;
                            trabajo.IdPgeneral = item.IdPGeneral;
                            trabajo.Coeficiente = item.Coeficiente;
                            trabajo.IdSelect = item.IdSelect;
                            trabajo.Columna = item.IdColumna;
                            trabajo.UsuarioCreacion = Json.Usuario;
                            trabajo.UsuarioModificacion = Json.Usuario;
                            trabajo.FechaCreacion = DateTime.Now;
                            trabajo.FechaModificacion = DateTime.Now;
                            trabajo.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilAtrabajoCoeficiente.Add(trabajo);
                    }
                    pgeneral.ProgramaGeneralPerfilScoringCategoria = new List<ProgramaGeneralPerfilScoringCategoriaBO>();
                    pgeneral.ProgramaGeneralPerfilCategoriaCoeficiente = new List<ProgramaGeneralPerfilCategoriaCoeficienteBO>();
                    foreach (var item in Json.Categoria.CategoriaEscoring)
                    {
                        ProgramaGeneralPerfilScoringCategoriaBO categoria;
                        if (repCategoraiScoring.Exist(item.Id))
                        {
                            categoria = repCategoraiScoring.FirstById(item.Id);
                            categoria.Nombre = item.Nombre;
                            categoria.IdCategoriaOrigen = item.IdCategoriaOrigen;
                            categoria.IdSelect = item.IdSelect;
                            categoria.Valor = item.Valor;
                            categoria.Fila = item.Fila;
                            categoria.Columna = item.Columna;
                            categoria.Validar = item.Validar;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            categoria = new ProgramaGeneralPerfilScoringCategoriaBO();
                            categoria.Nombre = item.Nombre;
                            categoria.IdPgeneral = item.IdPGeneral;
                            categoria.IdCategoriaOrigen = item.IdCategoriaOrigen;
                            categoria.IdSelect = item.IdSelect;
                            categoria.Valor = item.Valor;
                            categoria.Fila = item.Fila;
                            categoria.Columna = item.Columna;
                            categoria.Validar = item.Validar;
                            categoria.UsuarioCreacion = Json.Usuario;
                            categoria.UsuarioModificacion = Json.Usuario;
                            categoria.FechaCreacion = DateTime.Now;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilScoringCategoria.Add(categoria);
                    }
                    foreach (var item in Json.Categoria.CategoriaCoefiente)
                    {
                        ProgramaGeneralPerfilCategoriaCoeficienteBO categoria;
                        if (repCategoriaCoeficiente.Exist(item.Id))
                        {
                            categoria = repCategoriaCoeficiente.FirstById(item.Id);
                            categoria.Nombre = item.Nombre;
                            categoria.Coeficiente = item.Coeficiente;
                            categoria.IdSelect = item.IdSelect;
                            categoria.Columna = item.IdColumna;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            categoria = new ProgramaGeneralPerfilCategoriaCoeficienteBO();
                            categoria.Nombre = item.Nombre;
                            categoria.IdPgeneral = item.IdPGeneral;
                            categoria.Coeficiente = item.Coeficiente;
                            categoria.IdSelect = item.IdSelect;
                            categoria.Columna = item.IdColumna;
                            categoria.UsuarioCreacion = Json.Usuario;
                            categoria.UsuarioModificacion = Json.Usuario;
                            categoria.FechaCreacion = DateTime.Now;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilCategoriaCoeficiente.Add(categoria);
                    }
                    pgeneral.ProgramaGeneralPerfilTipoDato = new List<ProgramaGeneralPerfilTipoDatoBO>();
                    foreach (var item in Json.TipoDato)
                    {
                        ProgramaGeneralPerfilTipoDatoBO tipoDato;
                        if (repTipoDato.Exist(item.Id))
                        {
                            tipoDato = repTipoDato.FirstById(item.Id);
                            tipoDato.IdPgeneral = Json.IdPGeneral;
                            tipoDato.IdTipoDato = item.IdTipoDato;
                            tipoDato.FechaModificacion = DateTime.Now;
                            tipoDato.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            tipoDato = new ProgramaGeneralPerfilTipoDatoBO();
                            tipoDato.IdPgeneral = Json.IdPGeneral;
                            tipoDato.IdTipoDato = item.IdTipoDato;
                            tipoDato.UsuarioCreacion = Json.Usuario;
                            tipoDato.UsuarioModificacion = Json.Usuario;
                            tipoDato.FechaCreacion = DateTime.Now;
                            tipoDato.FechaModificacion = DateTime.Now;
                            tipoDato.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilTipoDato.Add(tipoDato);
                    }
                    pgeneral.ProgramaGeneralPerfilEscalaProbabilidad = new List<ProgramaGeneralPerfilEscalaProbabilidadBO>();
                    foreach (var item in Json.Escala)
                    {
                        ProgramaGeneralPerfilEscalaProbabilidadBO escala;
                        if (repEscala.Exist(item.Id))
                        {
                            escala = repEscala.FirstById(item.Id);
                            escala.IdPgeneral = Json.IdPGeneral;
                            escala.ProbabilidadActual = item.ProbabilidadActual;
                            escala.ProbabilidadInicial = item.ProbabilidadInicial;
                            escala.Orden = item.Orden;
                            escala.Nombre = item.Nombre;
                            escala.FechaModificacion = DateTime.Now;
                            escala.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            escala = new ProgramaGeneralPerfilEscalaProbabilidadBO();
                            escala.IdPgeneral = Json.IdPGeneral;
                            escala.ProbabilidadActual = item.ProbabilidadActual;
                            escala.ProbabilidadInicial = item.ProbabilidadInicial;
                            escala.Orden = item.Orden;
                            escala.Nombre = item.Nombre;
                            escala.UsuarioCreacion = Json.Usuario;
                            escala.UsuarioModificacion = Json.Usuario;
                            escala.FechaCreacion = DateTime.Now;
                            escala.FechaModificacion = DateTime.Now;
                            escala.Estado = true;
                        }
                        pgeneral.ProgramaGeneralPerfilEscalaProbabilidad.Add(escala);
                    }

                    ProgramaGeneralPerfilInterceptoBO intercepto;
                    if (repIntercepto.Exist(Json.Intercepto.Id))
                    {
                        intercepto = repIntercepto.FirstById(Json.Intercepto.Id);
                        intercepto.IdPgeneral = Json.IdPGeneral;
                        intercepto.PerfilIntercepto = Json.Intercepto.PerfilIntercepto;
                        intercepto.PerfilEstado = Json.Intercepto.PerfilEstado;
                        intercepto.FechaModificacion = DateTime.Now;
                        intercepto.UsuarioModificacion = Json.Usuario;
                    }
                    else
                    {
                        intercepto = new ProgramaGeneralPerfilInterceptoBO();
                        intercepto.IdPgeneral = Json.IdPGeneral;
                        intercepto.PerfilIntercepto = Json.Intercepto.PerfilIntercepto;
                        intercepto.PerfilEstado = Json.Intercepto.PerfilEstado;
                        intercepto.UsuarioCreacion = Json.Usuario;
                        intercepto.UsuarioModificacion = Json.Usuario;
                        intercepto.FechaCreacion = DateTime.Now;
                        intercepto.FechaModificacion = DateTime.Now;
                        intercepto.Estado = true;
                    }
                    pgeneral.ProgramaGeneralPerfilIntercepto = intercepto;
                    repPgeneral.Update(pgeneral);
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult GuardarModeloPredictivo([FromBody] CompuestoModeloPredictivoProgramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ModeloPredictivoRepositorio repPredictivo = new ModeloPredictivoRepositorio(contexto);
                ModeloPredictivoEscalaProbabilidadRepositorio repEscala = new ModeloPredictivoEscalaProbabilidadRepositorio(contexto);
                ModeloPredictivoTipoDatoRepositorio repTipoDato = new ModeloPredictivoTipoDatoRepositorio(contexto);

                ModeloPredictivoIndustriaRepositorio repIndustria = new ModeloPredictivoIndustriaRepositorio(contexto);
                ModeloPredictivoCargoRepositorio repCargo = new ModeloPredictivoCargoRepositorio(contexto);
                ModeloPredictivoFormacionRepositorio repFormacion = new ModeloPredictivoFormacionRepositorio(contexto);
                ModeloPredictivoTrabajoRepositorio repTrabajo = new ModeloPredictivoTrabajoRepositorio(contexto);
                ModeloPredictivoCategoriaDatoRepositorio repCategoria = new ModeloPredictivoCategoriaDatoRepositorio(contexto);

                ModeloGeneralPgeneralRepositorio repModeloPgeneralPgeneral = new ModeloGeneralPgeneralRepositorio(contexto);
                ModeloGeneralRepositorio repModeloPgeneral = new ModeloGeneralRepositorio(contexto);

                PgeneralRepositorio repPgeneral = new PgeneralRepositorio(contexto);

                ModeloPredictivoProgramaDTO modeloPredicitivo = new ModeloPredictivoProgramaDTO();
                using (
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                                   new System.TimeSpan(0, 15, 0)))
                {
                    PgeneralBO pgeneral = new PgeneralBO(Json.IdPGeneral, contexto);

                    repIndustria.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Industria);
                    repCargo.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Cargo);
                    repFormacion.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Formacion);
                    repTrabajo.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Trabajo);
                    repCategoria.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.CategoriaOrigen);
                    repTipoDato.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.TipoDato);
                    repEscala.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Escala);

                    pgeneral.ModeloPredictivoIndustria = new List<ModeloPredictivoIndustriaBO>();
                    foreach (var item in Json.Industria)
                    {
                        ModeloPredictivoIndustriaBO industria;
                        if (repIndustria.Exist(item.Id))
                        {
                            industria = repIndustria.FirstById(item.Id);
                            industria.Nombre = item.Nombre;
                            industria.Valor = item.Valor;
                            industria.Validar = item.Validar;
                            industria.IdIndustria = item.IdIndustria;
                            industria.FechaModificacion = DateTime.Now;
                            industria.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            industria = new ModeloPredictivoIndustriaBO();
                            industria.IdPgeneral = Json.IdPGeneral;
                            industria.Nombre = item.Nombre;
                            industria.Valor = item.Valor;
                            industria.Validar = item.Validar;
                            industria.IdIndustria = item.IdIndustria;
                            industria.UsuarioCreacion = Json.Usuario;
                            industria.UsuarioModificacion = Json.Usuario;
                            industria.FechaCreacion = DateTime.Now;
                            industria.FechaModificacion = DateTime.Now;
                            industria.Estado = true;
                        }
                        pgeneral.ModeloPredictivoIndustria.Add(industria);
                    }
                    pgeneral.ModeloPredictivoCargo = new List<ModeloPredictivoCargoBO>();
                    foreach (var item in Json.Cargo)
                    {
                        ModeloPredictivoCargoBO cargo;
                        if (repCargo.Exist(item.Id))
                        {
                            cargo = repCargo.FirstById(item.Id);
                            cargo.Nombre = item.Nombre;
                            cargo.Valor = item.Valor;
                            cargo.Validar = item.Validar;
                            cargo.IdCargo = item.IdCargo;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            cargo = new ModeloPredictivoCargoBO();
                            cargo.IdPgeneral = Json.IdPGeneral;
                            cargo.Nombre = item.Nombre;
                            cargo.Valor = item.Valor;
                            cargo.Validar = item.Validar;
                            cargo.IdCargo = item.IdCargo;
                            cargo.UsuarioCreacion = Json.Usuario;
                            cargo.UsuarioModificacion = Json.Usuario;
                            cargo.FechaCreacion = DateTime.Now;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.Estado = true;
                        }
                        pgeneral.ModeloPredictivoCargo.Add(cargo);
                    }
                    pgeneral.ModeloPredictivoFormacion = new List<ModeloPredictivoFormacionBO>();
                    foreach (var item in Json.Formacion)
                    {
                        ModeloPredictivoFormacionBO formacion;
                        if (repFormacion.Exist(item.Id))
                        {
                            formacion = repFormacion.FirstById(item.Id);
                            formacion.Nombre = item.Nombre;
                            formacion.Valor = item.Valor;
                            formacion.Validar = item.Validar;
                            formacion.IdAreaFormacion = item.IdAreaFormacion;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            formacion = new ModeloPredictivoFormacionBO();
                            formacion.IdPgeneral = Json.IdPGeneral;
                            formacion.Nombre = item.Nombre;
                            formacion.Valor = item.Valor;
                            formacion.Validar = item.Validar;
                            formacion.IdAreaFormacion = item.IdAreaFormacion;
                            formacion.UsuarioCreacion = Json.Usuario;
                            formacion.UsuarioModificacion = Json.Usuario;
                            formacion.FechaCreacion = DateTime.Now;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.Estado = true;
                        }
                        pgeneral.ModeloPredictivoFormacion.Add(formacion);
                    }
                    pgeneral.ModeloPredictivoTrabajo = new List<ModeloPredictivoTrabajoBO>();
                    foreach (var item in Json.Trabajo)
                    {
                        ModeloPredictivoTrabajoBO formacion;
                        if (repTrabajo.Exist(item.Id))
                        {
                            formacion = repTrabajo.FirstById(item.Id);
                            formacion.Nombre = item.Nombre;
                            formacion.Valor = item.Valor;
                            formacion.Validar = item.Validar;
                            formacion.IdAreaTrabajo = item.IdAreaTrabajo;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            formacion = new ModeloPredictivoTrabajoBO();
                            formacion.IdPgeneral = Json.IdPGeneral;
                            formacion.Nombre = item.Nombre;
                            formacion.Valor = item.Valor;
                            formacion.Validar = item.Validar;
                            formacion.IdAreaTrabajo = item.IdAreaTrabajo;
                            formacion.UsuarioCreacion = Json.Usuario;
                            formacion.UsuarioModificacion = Json.Usuario;
                            formacion.FechaCreacion = DateTime.Now;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.Estado = true;
                        }
                        pgeneral.ModeloPredictivoTrabajo.Add(formacion);
                    }
                    pgeneral.ModeloPredictivoCategoriaDato = new List<ModeloPredictivoCategoriaDatoBO>();
                    foreach (var item in Json.CategoriaOrigen)
                    {
                        ModeloPredictivoCategoriaDatoBO categoria;
                        if (repCategoria.Exist(item.Id))
                        {
                            categoria = repCategoria.FirstById(item.Id);
                            categoria.Nombre = item.Nombre;
                            categoria.Valor = item.Valor;
                            categoria.Validar = item.Validar;
                            categoria.IdCategoriaOrigen = item.IdCategoriaOrigen;
                            categoria.IdSubCategoriaDato = item.IdSubCategoriaDato;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            categoria = new ModeloPredictivoCategoriaDatoBO();
                            categoria.IdPgeneral = Json.IdPGeneral;
                            categoria.Nombre = item.Nombre;
                            categoria.Valor = item.Valor;
                            categoria.Validar = item.Validar;
                            categoria.IdCategoriaOrigen = item.IdCategoriaOrigen;
                            categoria.IdSubCategoriaDato = item.IdSubCategoriaDato;
                            categoria.UsuarioCreacion = Json.Usuario;
                            categoria.UsuarioModificacion = Json.Usuario;
                            categoria.FechaCreacion = DateTime.Now;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.Estado = true;
                        }
                        pgeneral.ModeloPredictivoCategoriaDato.Add(categoria);
                    }
                    pgeneral.ModeloPredictivoTipoDato = new List<ModeloPredictivoTipoDatoBO>();
                    foreach (var item in Json.TipoDato)
                    {
                        ModeloPredictivoTipoDatoBO tipoDato;
                        if (repTipoDato.Exist(item.Id))
                        {
                            tipoDato = repTipoDato.FirstById(item.Id);
                            tipoDato.IdPgeneral = Json.IdPGeneral;
                            tipoDato.IdTipoDato = item.IdTipoDato;
                            tipoDato.FechaModificacion = DateTime.Now;
                            tipoDato.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            tipoDato = new ModeloPredictivoTipoDatoBO();
                            tipoDato.IdPgeneral = Json.IdPGeneral;
                            tipoDato.IdTipoDato = item.IdTipoDato;
                            tipoDato.UsuarioCreacion = Json.Usuario;
                            tipoDato.UsuarioModificacion = Json.Usuario;
                            tipoDato.FechaCreacion = DateTime.Now;
                            tipoDato.FechaModificacion = DateTime.Now;
                            tipoDato.Estado = true;
                        }
                        pgeneral.ModeloPredictivoTipoDato.Add(tipoDato);
                    }
                    pgeneral.ModeloPredictivoEscalaProbabilidad = new List<ModeloPredictivoEscalaProbabilidadBO>();
                    foreach (var item in Json.Escala)
                    {
                        ModeloPredictivoEscalaProbabilidadBO escala;
                        if (repEscala.Exist(item.Id))
                        {
                            escala = repEscala.FirstById(item.Id);
                            escala.IdPgeneral = Json.IdPGeneral;
                            escala.Orden = item.Orden;
                            escala.Nombre = item.Nombre;
                            escala.ProbabilidadActual = item.ProbabilidadActual;
                            escala.ProbabilidaIinicial = item.ProbabilidaIInicial;
                            escala.FechaModificacion = DateTime.Now;
                            escala.UsuarioModificacion = Json.Usuario;
                        }
                        else
                        {
                            escala = new ModeloPredictivoEscalaProbabilidadBO();
                            escala.IdPgeneral = Json.IdPGeneral;
                            escala.Orden = item.Orden;
                            escala.Nombre = item.Nombre;
                            escala.ProbabilidadActual = item.ProbabilidadActual;
                            escala.ProbabilidaIinicial = item.ProbabilidaIInicial;
                            escala.UsuarioCreacion = Json.Usuario;
                            escala.UsuarioModificacion = Json.Usuario;
                            escala.FechaCreacion = DateTime.Now;
                            escala.FechaModificacion = DateTime.Now;
                            escala.Estado = true;
                        }
                        pgeneral.ModeloPredictivoEscalaProbabilidad.Add(escala);
                    }
                    ModeloPredictivoBO modelo;
                    if (repPredictivo.Exist(Json.Intercepto.Id))
                    {
                        modelo = repPredictivo.FirstById(Json.Intercepto.Id);
                        modelo.IdPgeneral = Json.IdPGeneral;
                        modelo.PeIntercepto = Json.Intercepto.PeIntercepto;
                        modelo.PeEstado = Json.Intercepto.PeEstado;
                        modelo.FechaModificacion = DateTime.Now;
                        modelo.UsuarioModificacion = Json.Usuario;
                    }
                    else
                    {
                        modelo = new ModeloPredictivoBO();
                        modelo.IdPgeneral = Json.IdPGeneral;
                        modelo.PeIntercepto = Json.Intercepto.PeIntercepto;
                        modelo.PeEstado = Json.Intercepto.PeEstado;
                        modelo.UsuarioCreacion = Json.Usuario;
                        modelo.UsuarioModificacion = Json.Usuario;
                        modelo.FechaCreacion = DateTime.Now;
                        modelo.FechaModificacion = DateTime.Now;
                        modelo.Estado = true;
                    }
                    pgeneral.ModeloPredictivo = modelo;

                    ModeloGeneralPgeneralBO asociado = repModeloPgeneralPgeneral.FirstBy(x => x.IdProgramaGeneral == Json.IdPGeneral);
                    if (asociado != null && asociado.IdModeloGeneral != null)
                    {
                        ModeloGeneralBO modeloPrograma = repModeloPgeneral.FirstBy(x => x.Id == asociado.IdModeloGeneral);
                        ModeloGeneralBO nuevo = new ModeloGeneralBO();
                        nuevo.PeIntercepto = modeloPrograma.PeIntercepto;
                        nuevo.Nombre = modeloPrograma.Nombre;
                        nuevo.PeEstado = modeloPrograma.PeEstado;
                        nuevo.PeVersion = 0;
                        nuevo.IdPadre = modeloPrograma.Id;
                        nuevo.UsuarioCreacion = Json.Usuario;
                        nuevo.UsuarioModificacion = Json.Usuario;
                        nuevo.FechaCreacion = DateTime.Now;
                        nuevo.FechaModificacion = DateTime.Now;
                        nuevo.Estado = true;

                        modeloPrograma.PeVersion = modeloPrograma.PeVersion + 1;
                        modeloPrograma.FechaModificacion = DateTime.Now;
                        modeloPrograma.UsuarioModificacion = Json.Usuario;
                        repModeloPgeneral.Insert(nuevo);
                        repModeloPgeneral.Update(modeloPrograma);

                        asociado.IdModeloGeneral = nuevo.Id;
                        asociado.FechaModificacion = DateTime.Now;
                        asociado.UsuarioModificacion = Json.Usuario;
                        repModeloPgeneralPgeneral.Update(asociado);
                    }
                    repPgeneral.Update(pgeneral);
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///
        [Route("[action]")]
        [HttpGet]
        public ActionResult RegularizarAsignacionAutomatica()
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                var listado = _repPGeneral.Regularizar_EliminarMetodo();


                //                var listado = new List<int>()
                //                {
                //                    1087567        ,
                //1087766
                //                };
                foreach (var item in listado)
                {
                    try
                    {
                        string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + item.Id;
                        //string URI = "http://localhost:4348/marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + item;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception e)
                    {
                        //throw;
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult RegularizarAsignacionAutomaticaError()
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                var listado = _repPGeneral.RegularizarAAError_EliminarMetodo();

                foreach (var item in listado)
                {
                    try
                    {
                        string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomaticaError?IdAsignacionAutomatica=" + item.Id;
                        //string URI = "http://localhost:4348/marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + item;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult RegularizarAsignacionAutomaticaTemp()
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                var listado = _repPGeneral.RegularizarAATemp_EliminarMetodo();
                foreach (var item in listado)
                {
                    try
                    {
                        string URI = "http://localhost:4348/marketing/InsertarActualizarAsignacionAutomaticaTemp?IdAsignacionAutomaticaTemp=" + item.Id;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult RegularizarAlumnos()
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                var listado = _repPGeneral.RegularizarAlumno_EliminarMetodo();
                foreach (var item in listado)
                {
                    try
                    {
                        string URI = "http://localhost:4348/marketing/InsertarActualizarAlumno_a_v3?IdAlumno=" + item.Id;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult RegularizarOportunidad()
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                var listado = _repPGeneral.RegularizarOportunidad_EliminarMetodo();
                foreach (var item in listado)
                {
                    try
                    {
                        string URI = "http://localhost:4348/marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + item.Id;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult RegularizarOportunidad_SinModeloDataMining()
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                var listado = _repPGeneral.RegularizarOportunidad_SinModeloDataMining();
                foreach (var item in listado)
                {
                    try
                    {
                        string URI = "http://integrav4-servicios.bsginstitute.com/api/Oportunidad/RegularizarModeloDataMining/" + item.Id;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ProgramaGeneralAutocomplete([FromBody] ValorFiltroDTO Filtro)
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                return Ok(_repPGeneral.ObtenerProgramaGeneralAutocomplete(Filtro.Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdDocumentoPw}")]
        [HttpGet]
        public IActionResult ObtenerCriterioEvaluacionPorDocumento(int IdDocumentoPw)
        {
            try
            {
                DocumentoPwRepositorio _repoDocumentoPw = new DocumentoPwRepositorio();
                var documentoPwBo = _repoDocumentoPw.FirstById(IdDocumentoPw);
                if (documentoPwBo.IdPlantillaPw != 1)
                    return Ok();

                PgeneralDocumentoPwRepositorio _repoPgeneralDocumento = new PgeneralDocumentoPwRepositorio();
                var documentoBo = _repoPgeneralDocumento.FirstBy(w => w.IdDocumento == IdDocumentoPw);

                PGeneralCriterioEvaluacionRepositorio _repocriterios = new PGeneralCriterioEvaluacionRepositorio();
                var listado = _repocriterios.GetBy(w => w.IdPgeneral == documentoBo.IdPgeneral);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]/{IdDocumentoPw}")]
        [HttpGet]
        public IActionResult ObtenerPorDocumentoPW(int IdDocumentoPw)
        {
            try
            {
                PgeneralDocumentoPwRepositorio _repoPgeneralDocumento = new PgeneralDocumentoPwRepositorio();
                var documentoBo = _repoPgeneralDocumento.FirstBy(w => w.IdDocumento == IdDocumentoPw);

                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();

                return Ok(_repPGeneral.FirstById(documentoBo.IdPgeneral));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{IdPlantillaF}/{IdPlantillaP}/{IdPrograma}")]
        [HttpGet]
        public IActionResult GenerarVistaPreviaCertificado(int IdPlantillaF, int IdPlantillaP, int IdPrograma)
        {
            try
            {
                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio(_integraDBContext);
                DocumentosBO documentosBO = new DocumentosBO();
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                ContenidoCertificadoIrcaRepositorio _repContenidoCertificadoIrca = new ContenidoCertificadoIrcaRepositorio(_integraDBContext);
                byte[] documentoByte = null;


                var TipoPlantilla = _repPlantilla.FirstById(IdPlantillaF);

                //var _reemplazoEtiquetaPlantillaPosterior = new ReemplazoEtiquetaPlantillaBO(_integraDBContext);
                //var _reemplazoEtiquetaPlantillaFrontal = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                //{
                //    IdOportunidad = 1222003,
                //    IdPlantilla = IdPlantillaF
                //};
                //_reemplazoEtiquetaPlantillaFrontal.ReemplazarEtiquetas();

                //if (IdPlantillaP != 0 )
                //{
                //    _reemplazoEtiquetaPlantillaPosterior.IdOportunidad = 1222003;
                //    _reemplazoEtiquetaPlantillaPosterior.IdPlantilla = IdPlantillaP;

                //    _reemplazoEtiquetaPlantillaPosterior.ReemplazarEtiquetas();
                //}

                int Idplantillabase = 0;
                int IdPgeneral = 0;
                string codigoCertificado = "";
                if (TipoPlantilla.Nombre.ToUpper().Contains("IRCA"))
                {
                    var datosVistaPrevia = _repContenidoCertificadoIrca.ObtenerValoresVistaPreviaIrca(IdPrograma);
                    if (datosVistaPrevia.Id == 0)
                    {
                        datosVistaPrevia.Id = 2;
                        datosVistaPrevia.IdPespecifico = 8627;
                    }
                    documentoByte = documentosBO.GenerarCertificadoIrca(IdPlantillaF, datosVistaPrevia.Id, datosVistaPrevia.IdPespecifico, ref codigoCertificado, ref IdPgeneral);
                }
                else
                {
                    //var IdOportunidad = _repOportunidad.ObtenerOportunidadPrograma(IdPrograma);
                    //if (IdOportunidad == 0)
                    //{
                    //    IdOportunidad = 1222003;
                    //}
                    documentoByte = documentosBO.GenerarVistaModeloCertificado(IdPlantillaF, IdPlantillaP, IdPrograma);
                }



                return Ok(documentoByte);
                //var ms = new MemoryStream();
                //ms.Write(documentoByte, 0, documentoByte.Length);
                //ms.Position = 0;
                //return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{IdPrograma}/{Estado}/{Usuario}")]
        [HttpGet]
        public IActionResult ActualizarEstadoPrograma(int IdPrograma, bool Estado, string Usuario)
        {
            try
            {
                PgeneralRepositorio _repPgeneralRepositorio = new PgeneralRepositorio(_integraDBContext);

                var ProgramaGeneral = _repPgeneralRepositorio.ObtenerProgramaPorId(IdPrograma);

                ProgramaGeneral.Estado = Estado;
                ProgramaGeneral.FechaModificacion = DateTime.Now;
                ProgramaGeneral.UsuarioModificacion = Usuario;
                _repPgeneralRepositorio.Update(ProgramaGeneral);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosCriterios()
        {

            try
            {
                CriterioEvaluacionRepositorio combosCriterioEvaluacion = new CriterioEvaluacionRepositorio();
                var cmbCE = new { CriteriosEvaluacion = combosCriterioEvaluacion.ObtenerFiltro() };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Regresa valores de Criterios Evaluacion Online
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPGCriteriosEvaluacionOnline(int idPgeneral)
        {

            try
            {
                PGeneralCriterioEvaluacionRepositorio combosCriterioEvaluacion = new PGeneralCriterioEvaluacionRepositorio();
                var cmbCE = new { CriteriosEvaluacion = combosCriterioEvaluacion.ListarPGcriteriosEvaluacionOnline(idPgeneral) };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Regresa valores de Criterios Evaluacion Presencial
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPGCriteriosEvaluacionPresencial(int idPgeneral)
        {

            try
            {
                PGeneralCriterioEvaluacionRepositorio combosCriterioEvaluacion = new PGeneralCriterioEvaluacionRepositorio();
                var cmbCE = new { CriteriosEvaluacion = combosCriterioEvaluacion.ListarPGcriteriosEvaluacionPresencial(idPgeneral) };
                //return Ok(combosCriterioEvaluacion.ListarPGcriteriosEvaluacionPresencial(idPgeneral));
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Regresa valores de Criterios Evaluacion Aonline
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPGCriteriosEvaluacionAonline(int idPgeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralCriterioEvaluacionRepositorio combosCriterioEvaluacion = new PGeneralCriterioEvaluacionRepositorio();
                var cmbCE = new { CriteriosEvaluacion = combosCriterioEvaluacion.ListarPGcriteriosEvaluacionAonline(idPgeneral) };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Nos retorna los cursos hijo del IdGeneral de la modalidad presencial
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPPadreCEvaluacionPresencial(int idPgeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralCriterioEvaluacionHijoRepositorio combosCriterioEvaluacion = new PGeneralCriterioEvaluacionHijoRepositorio();
                var cmbCE = new { ComboPadreCursoCE = combosCriterioEvaluacion.ListarPadreCursosCriteriosPresencial(idPgeneral) };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Nos retorna los cursos hijo del IdGeneral de la modalidad Aonline
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPPadreCEvaluacionAonline(int idPgeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralCriterioEvaluacionHijoRepositorio combosCriterioEvaluacion = new PGeneralCriterioEvaluacionHijoRepositorio();
                var cmbCE = new { ComboPadreCursoCE = combosCriterioEvaluacion.ListarPadreCursosCriteriosAonline(idPgeneral) };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Nos retorna los cursos hijo del IdGeneral de la modalidad Online
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPPadreCEvaluacionOnline(int idPgeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PGeneralCriterioEvaluacionHijoRepositorio combosCriterioEvaluacion = new PGeneralCriterioEvaluacionHijoRepositorio();
                var cmbCE = new { ComboPadreCursoCE = combosCriterioEvaluacion.ListarPadreCursosCriteriosOnline(idPgeneral) };
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //IdTipoPrograma segun IdPgeneral
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult obtenerIdTipoPrograma(int idPgeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoProgramaRepositorio idTipoPrograma = new TipoProgramaRepositorio();
                var lista = idTipoPrograma.getIdNombreTipoPrograma(idPgeneral);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Obtener IdModalidad segun el tipo de programa
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public IActionResult ObtenerIdmodalidad(int idPgeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralModalidadRepositorio repCriterioE = new PgeneralModalidadRepositorio();
                var lista = repCriterioE.ListarModalidadesCurso(idPgeneral);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PGeneralCriteriosEvaluacion segun modalidad
        [Route("[action]/{tipoprograma}/{modalidadprograma}")]
        [HttpGet]
        public IActionResult ObtenerPGCriteriosEvaluacion(int tipoprograma, int modalidadprograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionRepositorio repCriterioE = new CriterioEvaluacionRepositorio();
                var lista = repCriterioE.ObtenerCriterio(tipoprograma, modalidadprograma);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PGeneralCriteriosEvaluacion trae todos los criterios
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerALLPGCriteriosEvaluacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionRepositorio repCriterioE = new CriterioEvaluacionRepositorio();
                var lista = repCriterioE.ObtenerFiltro();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //inserta-actualiza en la tabla P_GeneralCriterioEvaluacion
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarInsertarPGCEvaluacion([FromBody] InsertarActualizarPGCriteriosEvaluacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralCriterioEvaluacionRepositorio _repPGcritterioevaluacionHijo = new PGeneralCriterioEvaluacionRepositorio();
                PGeneralCriterioEvaluacionBO pgCriterioEvaluacionBO = new PGeneralCriterioEvaluacionBO();
                if (dto.Id == 0)
                {
                    pgCriterioEvaluacionBO.IdPgeneral = dto.IdPGeneral;
                    pgCriterioEvaluacionBO.IdModalidadCurso = dto.IdModalidadCurso;
                    pgCriterioEvaluacionBO.IdCriterioEvaluacion = dto.IdCriterioEvaluacion;
                    pgCriterioEvaluacionBO.Nombre = "";
                    pgCriterioEvaluacionBO.Porcentaje = dto.Porcentaje;
                    pgCriterioEvaluacionBO.IdTipoPromedio = dto.IdTipoPromedio;
                    pgCriterioEvaluacionBO.IdCriterioEvaluacion = dto.IdCriterioEvaluacion;
                    pgCriterioEvaluacionBO.Estado = true;
                    pgCriterioEvaluacionBO.FechaCreacion = DateTime.Now;
                    pgCriterioEvaluacionBO.FechaModificacion = DateTime.Now;
                    pgCriterioEvaluacionBO.UsuarioCreacion = dto.usuario;
                    pgCriterioEvaluacionBO.UsuarioModificacion = dto.usuario;

                    _repPGcritterioevaluacionHijo.Insert(pgCriterioEvaluacionBO);

                    return Ok(pgCriterioEvaluacionBO);
                }
                else
                {
                    pgCriterioEvaluacionBO = _repPGcritterioevaluacionHijo.FirstBy(w => w.Id == dto.Id);
                    if (pgCriterioEvaluacionBO != null)
                    {
                        pgCriterioEvaluacionBO.IdModalidadCurso = dto.IdModalidadCurso;
                        //pgCriterioEvaluacionBO.Nombre = dto.Nombre;
                        pgCriterioEvaluacionBO.Porcentaje = dto.Porcentaje;
                        pgCriterioEvaluacionBO.IdTipoPromedio = dto.IdTipoPromedio;
                        pgCriterioEvaluacionBO.FechaModificacion = DateTime.Now;
                        pgCriterioEvaluacionBO.UsuarioModificacion = dto.usuario;

                        _repPGcritterioevaluacionHijo.Update(pgCriterioEvaluacionBO);

                        return Ok(pgCriterioEvaluacionBO);
                    }

                }
                return Ok(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //inserta-actualiza en la tabla P_GeneralCriterioEvaluacionHijo
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarInsertarPGCEvaluacionHijo([FromBody] InsertarActualizarPGCriteriosEvaluacionHijoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralCriterioEvaluacionHijoRepositorio _repPGcritterioevaluacionHijo = new PGeneralCriterioEvaluacionHijoRepositorio();
                PGeneralCriterioEvaluacionHijoBO pgCriterioEvaluacionBO = new PGeneralCriterioEvaluacionHijoBO();
                if (dto.Id == 0)
                {
                    pgCriterioEvaluacionBO.IdPgeneral = dto.IdPgeneral;
                    pgCriterioEvaluacionBO.IdModalidadCurso = dto.IdModalidadCurso;
                    pgCriterioEvaluacionBO.ConsiderarNota = dto.ConsiderarNota;
                    pgCriterioEvaluacionBO.Porcentaje = dto.Porcentaje;
                    pgCriterioEvaluacionBO.IdTipoPromedio = dto.IdTipoPromedio;
                    pgCriterioEvaluacionBO.Estado = true;
                    pgCriterioEvaluacionBO.FechaCreacion = DateTime.Now;
                    pgCriterioEvaluacionBO.FechaModificacion = DateTime.Now;
                    pgCriterioEvaluacionBO.UsuarioCreacion = dto.Usuario;
                    pgCriterioEvaluacionBO.UsuarioModificacion = dto.Usuario;

                    _repPGcritterioevaluacionHijo.Insert(pgCriterioEvaluacionBO);

                    return Ok(true);
                }
                else
                {
                    pgCriterioEvaluacionBO = _repPGcritterioevaluacionHijo.FirstBy(w => w.Id == dto.Id);
                    if (pgCriterioEvaluacionBO != null)
                    {
                        //pgCriterioEvaluacionBO.IdModalidadCurso = dto.IdPGeneralHijo;
                        pgCriterioEvaluacionBO.ConsiderarNota = dto.ConsiderarNota;
                        pgCriterioEvaluacionBO.Porcentaje = dto.Porcentaje;
                        pgCriterioEvaluacionBO.IdTipoPromedio = dto.IdTipoPromedio;
                        pgCriterioEvaluacionBO.FechaModificacion = DateTime.Now;
                        pgCriterioEvaluacionBO.UsuarioModificacion = dto.Usuario;

                        _repPGcritterioevaluacionHijo.Update(pgCriterioEvaluacionBO);

                        return Ok(true);
                    }

                }
                return Ok(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            //traer los datos de la columna de porcentaje

        }

        //Eliminar CriteriosEvaluacion 
        [Route("[action]")]
        [HttpDelete]
        public ActionResult EliminarCriterioEvaluacion([FromBody] EliminarCriterioEvaluacionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralCriterioEvaluacionRepositorio _repcriterioevaluacion = new PGeneralCriterioEvaluacionRepositorio();

                bool result = false;
                if (_repcriterioevaluacion.Exist(Json.Id))
                {
                    result = _repcriterioevaluacion.Delete(Json.Id, Json.Usuario);
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Devuelve  todos los puntos de corte asociados a programas generales
        /// </summary>
        /// <returns>List<ProgramaGeneralPuntoCorteDTO></returns>

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoProgramaGeneralPuntoCorte()
        {
            try
            {
                ProgramaGeneralPuntoCorteRepositorio programaGeneralPuntoCorteRepositorio = new ProgramaGeneralPuntoCorteRepositorio(_integraDBContext);
                return Ok(programaGeneralPuntoCorteRepositorio.ObtenerPuntoCorteGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Todos los filtros del modulo
        /// </summary>
        /// <returns>Objeto de clase PGeneralPuntoCorteDTO para rellenar los Multiselect en formato JSON</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerComboProgramaGeneralPuntoCorte()
        {
            try
            {
                AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
                SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                PuntoCorteRepositorio _repPuntoCorte = new PuntoCorteRepositorio(_integraDBContext);

                PGeneralPuntoCorteDTO puntoCorteFiltro = new PGeneralPuntoCorteDTO
                {
                    //ListaAreaCapacitacion = _repAreaCapacitacion.ObtenerTodoFiltroWeb(),
                    ListaAreaCapacitacion = _repAreaCapacitacion.GetAll().Select(s => new FiltroDTO { Id = s.Id, Nombre = s.Nombre }).ToList(),
                    //ListaSubAreaCapacitacion = _repSubAreaCapacitacion.ObtenerSubAreasParaFiltroWeb(),
                    ListaSubAreaCapacitacion = _repSubAreaCapacitacion.GetAll().Select(s => new SubAreaCapacitacionFiltroDTO { Id = s.Id, Nombre = s.Nombre, IdAreaCapacitacion = s.IdAreaCapacitacion }).ToList(),
                    ListaProgramaGeneral = _repPGeneral.ObtenerProgramaSubAreaFiltroTodo(),
                    ListaPuntoCorte = _repPuntoCorte.GetAll().Select(s => new FiltroDTO { Id = s.Id, Nombre = s.Nombre }).ToList()
                };

                return Ok(puntoCorteFiltro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo punto de corte
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult InsertarProgramaGeneralPuntoCorte([FromBody] ProgramaGeneralPuntoCorteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPuntoCorteRepositorio _repProgramaGeneralPuntoCorte = new ProgramaGeneralPuntoCorteRepositorio(_integraDBContext);

                if (Json.IdProgramaGeneral == null)
                {
                    return BadRequest("Ingrese un programa general");
                }

                if (_repProgramaGeneralPuntoCorte.Exist(x => x.IdProgramaGeneral == Json.IdProgramaGeneral))
                {
                    return BadRequest("El programa seleccionado ya tiene puntos de corte");
                }

                ProgramaGeneralPuntoCorteDetalleBO programaGeneralPuntoCorteDetalleNuevo = new ProgramaGeneralPuntoCorteDetalleBO();
                ProgramaGeneralPuntoCorteDetalleRepositorio _repProgramaGeneralPuntoCorteDetalle = new ProgramaGeneralPuntoCorteDetalleRepositorio(_integraDBContext);

                ProgramaGeneralPuntoCorteBO programaGeneralPuntoCorteBO = new ProgramaGeneralPuntoCorteBO();
                programaGeneralPuntoCorteBO.IdProgramaGeneral = Json.IdProgramaGeneral;
                programaGeneralPuntoCorteBO.PuntoCorteMedia = Json.PuntoCorteMedia;
                programaGeneralPuntoCorteBO.PuntoCorteAlta = Json.PuntoCorteAlta;
                programaGeneralPuntoCorteBO.PuntoCorteMuyAlta = Json.PuntoCorteMuyAlta;
                programaGeneralPuntoCorteBO.Estado = true;
                programaGeneralPuntoCorteBO.UsuarioCreacion = Json.Usuario;
                programaGeneralPuntoCorteBO.UsuarioModificacion = Json.Usuario;
                programaGeneralPuntoCorteBO.FechaCreacion = DateTime.Now;
                programaGeneralPuntoCorteBO.FechaModificacion = DateTime.Now;

                _repProgramaGeneralPuntoCorte.Insert(programaGeneralPuntoCorteBO);

                foreach (var item in Json.ListaPuntoCorteMedia)
                {
                    programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteBO.Id;
                    programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 1;
                    programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                    programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                    programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                    programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                    programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                    programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = Json.Usuario;
                    programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = Json.Usuario;
                    programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                    programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                    _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                    programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                }
                foreach (var item in Json.ListaPuntoCorteAlta)
                {
                    programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteBO.Id;
                    programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 2;
                    programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                    programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                    programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                    programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                    programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                    programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = Json.Usuario;
                    programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = Json.Usuario;
                    programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                    programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                    _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                    programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                }
                foreach (var item in Json.ListaPuntoCorteMuyAlta)
                {
                    programaGeneralPuntoCorteDetalleNuevo.IdProgramaGeneralPuntoCorte = programaGeneralPuntoCorteBO.Id;
                    programaGeneralPuntoCorteDetalleNuevo.IdPuntoCorte = 3;
                    programaGeneralPuntoCorteDetalleNuevo.Tipo = item.Tipo;
                    programaGeneralPuntoCorteDetalleNuevo.Descripcion = item.Descripcion;
                    programaGeneralPuntoCorteDetalleNuevo.ValorMinimo = item.ValorMinimo;
                    programaGeneralPuntoCorteDetalleNuevo.ValorMaximo = item.ValorMaximo;
                    programaGeneralPuntoCorteDetalleNuevo.Estado = true;
                    programaGeneralPuntoCorteDetalleNuevo.UsuarioCreacion = Json.Usuario;
                    programaGeneralPuntoCorteDetalleNuevo.UsuarioModificacion = Json.Usuario;
                    programaGeneralPuntoCorteDetalleNuevo.FechaCreacion = DateTime.Now;
                    programaGeneralPuntoCorteDetalleNuevo.FechaModificacion = DateTime.Now;
                    _repProgramaGeneralPuntoCorteDetalle.Insert(programaGeneralPuntoCorteDetalleNuevo);
                    programaGeneralPuntoCorteDetalleNuevo.Id = 0;
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 07/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista para el modulo de programa general punto corte
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase ProgramaGeneralPuntoCorteAreaSubAreaDTO, caso contrario response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerListaProgramaGeneralPuntoCorte([FromBody] ProgramaGeneralPuntoCorteFiltroDTO FiltroProgramaGeneralPuntoCorte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPuntoCorteRepositorio _repProgramaGeneralPuntoCorte = new ProgramaGeneralPuntoCorteRepositorio(_integraDBContext);

                return Ok(_repProgramaGeneralPuntoCorte.ObtenerListaProgramaGeneralPuntoCorte(FiltroProgramaGeneralPuntoCorte).OrderBy(x => x.IdProgramaGeneralPuntoCorte).ThenBy(y => y.PuntoCorteMedia).ThenBy(z => z.PuntoCorteAlta).ThenBy(zz => zz.PuntoCorteMuyAlta).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Carlos Crispin - Gian Miranda
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un punto de corte existente
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarProgramaGeneralPuntoCorte([FromBody] ProgramaGeneralPuntoCorteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPuntoCorteRepositorio _repProgramaGeneralPuntoCorte = new ProgramaGeneralPuntoCorteRepositorio(_integraDBContext);
                ProgramaGeneralPuntoCorteBO programaGeneralPuntoCorteBO = new ProgramaGeneralPuntoCorteBO(_integraDBContext);

                bool resultadoActualizacion = programaGeneralPuntoCorteBO.ActualizarProgramaGeneralPuntoCorte(Json);

                return Ok(resultadoActualizacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Gian Miranda
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza multiples puntos de corte
        /// </summary>
        /// <returns>Response 200 con booleano, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarProgramaGeneralPuntoCorteMasivo([FromBody] ProgramaGeneralPuntoCorteMasivoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPuntoCorteBO programaGeneralPuntoCorteBO = new ProgramaGeneralPuntoCorteBO(_integraDBContext);

                programaGeneralPuntoCorteBO.ActualizarProgramaGeneralPuntoCorteMasivo(Json);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina un punto de corte existente
        /// </summary>
        /// <returns>bool</returns>
        [Route("[Action]")]
        [HttpDelete]
        public ActionResult EliminarProgramaGeneralPuntoCorte([FromBody] ProgramaGeneralPuntoCorteAEliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPuntoCorteRepositorio programaGeneralPuntoCorteRepositorio = new ProgramaGeneralPuntoCorteRepositorio(_integraDBContext);
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (programaGeneralPuntoCorteRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = programaGeneralPuntoCorteRepositorio.Delete(Json.Id, Json.Usuario);
                    }

                    scope.Complete();
                }
                return Ok(estadoEliminacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: POST
        /// Autor: Edgar Serruto
        /// Fecha: 20/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Beneficios Requisito y Detalle para Interfaz
        /// </summary>
        /// <param name="Filtro">Información de programa y beneficio</param>
        /// <returns>Response 200 con la lista de objetos de clase List<BeneficioDetalleRequisitoDTO>, caso contrario response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerInformacionBeneficioRequisitpDetalle([FromBody] BeneficioDetalleRequisitoFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralConfiguracionBeneficioRepositorio _repPgeneralConfiguracionBeneficioRepositorio = new PgeneralConfiguracionBeneficioRepositorio(_integraDBContext);
                var respuesta = _repPgeneralConfiguracionBeneficioRepositorio.GetBy(x => x.IdPgeneral == Filtro.IdProgramaGeneral && x.IdBeneficio == Filtro.IdBeneficio).FirstOrDefault();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: POST
        /// Autor: Edgar Serruto
        /// Fecha: 20/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza información de detalle beneficio 
        /// </summary>
        /// <param name="InformacionBeneficioDetalle">Información codificada de Detalle de Requisitos</param>
        /// <returns>Objeto Agrupado, Bool de Estado y mensaje para interfaz</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarInformacionBeneficioDetalleRequisito([FromBody] BeneficioDetalleRequisitoCodificadoDTO InformacionBeneficioDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralConfiguracionBeneficioRepositorio _repConfiguracionBeneficioProgramaGeneral = new PgeneralConfiguracionBeneficioRepositorio(_integraDBContext);
                ConfiguracionBeneficioDetalleRequisitoDTO detalleBeneficioRequisito = JsonConvert.DeserializeObject<ConfiguracionBeneficioDetalleRequisitoDTO>(Encoding.UTF8.GetString(Convert.FromBase64String(InformacionBeneficioDetalle.DatosCodificados)));
                //Validación de campos obligatorios
                if (detalleBeneficioRequisito.IdPGeneral > 0 && detalleBeneficioRequisito.IdBeneficio > 0)
                {
                    var registro = _repConfiguracionBeneficioProgramaGeneral.GetBy(x => x.IdPgeneral == detalleBeneficioRequisito.IdPGeneral && x.IdBeneficio == detalleBeneficioRequisito.IdBeneficio).FirstOrDefault();
                    if (registro != null)
                    {
                        registro.Requisitos = detalleBeneficioRequisito.Requisito;
                        registro.ProcesoSolicitud = detalleBeneficioRequisito.ProcesoSolicitud;
                        registro.DetallesAdicionales = detalleBeneficioRequisito.DetallesAdicionales;
                        registro.UsuarioModificacion = detalleBeneficioRequisito.Usuario;
                        registro.FechaModificacion = DateTime.Now;
                        _repConfiguracionBeneficioProgramaGeneral.Update(registro);
                        string respuesta = "Los datos se Actualizaron Correctamente";
                        return Ok(new { Respuesta = true, Mensaje = respuesta });
                    }
                    else
                    {
                        string respuesta = "El programa no tiene asociado este beneficio";
                        return Ok(new { Respuesta = false, Mensaje = respuesta });
                    }
                }
                else
                {
                    string respuesta = "Falta Información Obligatoria";
                    return Ok(new { Respuesta = false, Mensaje = respuesta });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edgar Serruto
        /// Fecha: 21/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de beneficios por programa general asociado
        /// </summary>
        /// <param name="IdBeneficio">Id de Beneficio</param>
        /// <param name="IdPGeneral">Id de Programa General</param>
        /// <returns>Objeto Agrupado, Bool de Estado y mensaje para interfaz</returns>
        [Route("[Action]/{IdBeneficio}/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerBeneficioDetalleRequisito(int IdBeneficio, int IdPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralConfiguracionBeneficioRepositorio _repConfiguracionBeneficioProgramaGeneral = new PgeneralConfiguracionBeneficioRepositorio(_integraDBContext);
                var registro = _repConfiguracionBeneficioProgramaGeneral.GetBy(x => x.IdPgeneral == IdPGeneral && x.IdBeneficio == IdBeneficio).Select(x => new { x.Requisitos, x.ProcesoSolicitud, x.DetallesAdicionales }).FirstOrDefault();
                if (registro != null)
                {
                    return Ok(new { Respuesta = true, Datos = registro });
                }
                else
                {
                    return Ok(new { Respuesta = false });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 06/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las grillas de los punto de corte
        /// </summary>
        /// <param name="IdProgramaGeneralPuntoCorte">Id de programa general de punto de corte</param>
        /// <returns>Objeto Agrupado, Bool de Estado y mensaje para interfaz</returns>
        [Route("[Action]/{IdProgramaGeneralPuntoCorte}/{IdPuntoCorte}")]
        [HttpGet]
        public ActionResult ObtenerGrillaPuntoCorte(int IdProgramaGeneralPuntoCorte, int IdPuntoCorte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPuntoCorteDetalleRepositorio _repProgramaGeneralPuntoCorteDetalle = new ProgramaGeneralPuntoCorteDetalleRepositorio(_integraDBContext);
                var grilla = _repProgramaGeneralPuntoCorteDetalle.GetBy(x => x.IdProgramaGeneralPuntoCorte == IdProgramaGeneralPuntoCorte && x.IdPuntoCorte == IdPuntoCorte).ToList();
                return Ok(grilla);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 06/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las grillas de los tipos de probabilidad
        /// </summary>
        /// <param name="IdProgramaGeneralPuntoCorte">Id de programa general de punto de corte</param>
        /// <returns>Objeto Agrupado, Bool de Estado y mensaje para interfaz</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerGrillaTipoProbabilidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPuntoCorteConfiguracionRepositorio _repProgramaGeneralPuntoCorteConfiguracion = new ProgramaGeneralPuntoCorteConfiguracionRepositorio(_integraDBContext);
                var grilla = _repProgramaGeneralPuntoCorteConfiguracion.GetAll().ToList();
                return Ok(grilla);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza configuracion punto de corte existente
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarProgramaGeneralPuntoCorteConfiguracion([FromBody] ListaProgramaGeneralPuntoCorteConfiguracionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralPuntoCorteConfiguracionRepositorio _repProgramaGeneralPuntoCorteConfiguracion = new ProgramaGeneralPuntoCorteConfiguracionRepositorio(_integraDBContext);
                ProgramaGeneralPuntoCorteConfiguracionBO programaGeneralPuntoCorteConfiguracionBO = new ProgramaGeneralPuntoCorteConfiguracionBO();

                List<int> listaIds = _repProgramaGeneralPuntoCorteConfiguracion.GetAll().Select(s => s.Id).ToList();
                if (listaIds.Any())
                    _repProgramaGeneralPuntoCorteConfiguracion.Delete(listaIds, Json.Usuario);
                foreach (var item in Json.Datos)
                {
                    programaGeneralPuntoCorteConfiguracionBO.IdTipoCorte = item.IdTipoCorte;
                    programaGeneralPuntoCorteConfiguracionBO.Tipo = item.Tipo;
                    programaGeneralPuntoCorteConfiguracionBO.IdAreaCapacitacion = item.IdAreaCapacitacion;
                    programaGeneralPuntoCorteConfiguracionBO.IdSubAreaCapacitacion = item.IdSubAreaCapacitacion;
                    programaGeneralPuntoCorteConfiguracionBO.IdPgeneral = item.IdPgeneral;
                    programaGeneralPuntoCorteConfiguracionBO.Color = item.Color;
                    programaGeneralPuntoCorteConfiguracionBO.Texto = item.Texto;

                    programaGeneralPuntoCorteConfiguracionBO.Estado = true;
                    programaGeneralPuntoCorteConfiguracionBO.UsuarioCreacion = Json.Usuario;
                    programaGeneralPuntoCorteConfiguracionBO.UsuarioModificacion = Json.Usuario;
                    programaGeneralPuntoCorteConfiguracionBO.FechaCreacion = DateTime.Now;
                    programaGeneralPuntoCorteConfiguracionBO.FechaModificacion = DateTime.Now;
                    _repProgramaGeneralPuntoCorteConfiguracion.Insert(programaGeneralPuntoCorteConfiguracionBO);
                    programaGeneralPuntoCorteConfiguracionBO.Id = 0;
                }

                return Ok(_repProgramaGeneralPuntoCorteConfiguracion.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Motivacion
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarMotivacionesVentas([FromBody] MotivacionVentasDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralMotivacionRepositorio repMotivacion = new ProgramaGeneralMotivacionRepositorio(_integraDBContext);
                ProgramaGeneralMotivacionModalidadRepositorio repMotivacionModalidad = new ProgramaGeneralMotivacionModalidadRepositorio(_integraDBContext);
                ProgramaGeneralMotivacionArgumentoRepositorio repMotivacionArgumento = new ProgramaGeneralMotivacionArgumentoRepositorio(_integraDBContext);


                List<ProgramaGeneralMotivacionArgumentoBO> argumentos;
                List<ProgramaGeneralMotivacionModalidadBO> modalidadMotivaciones;
                bool flagBeficios = false;
                ProgramaGeneralMotivacionBO motivacion;
                using (TransactionScope scope = new TransactionScope())
                {

                    //var eliminadosMotivaciones = repMotivacion.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Motivaciones);
                    //repMotivacionModalidad.EliminacionLogicoPorListaMotivacion(Json.Usuario, eliminadosMotivaciones);
                    //repMotivacionArgumento.EliminacionLogicoPorListaMotivaciones(Json.Usuario, eliminadosMotivaciones);                    


                    if (repMotivacion.Exist(Json.Motivaciones.IdMotivacion))
                    {

                        motivacion = repMotivacion.FirstById(Json.Motivaciones.IdMotivacion);
                        motivacion.IdPgeneral = Json.Motivaciones.IdPGeneral;
                        motivacion.Nombre = Json.Motivaciones.NombreMotivacion;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaModificacion = DateTime.Now;

                        repMotivacionArgumento.EliminacionLogicoPorMotivacion(Json.Motivaciones.IdMotivacion, Json.Usuario, Json.Motivaciones.MotivacionesArgumentos);
                        repMotivacionModalidad.EliminacionLogicoPorMotivacion(Json.Motivaciones.IdMotivacion, Json.Usuario, Json.Motivaciones.Modalidades);
                    }
                    else
                    {
                        motivacion = new ProgramaGeneralMotivacionBO();
                        motivacion.IdPgeneral = Json.Motivaciones.IdPGeneral;
                        motivacion.Nombre = Json.Motivaciones.NombreMotivacion;
                        motivacion.UsuarioCreacion = Json.Usuario;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaCreacion = DateTime.Now;
                        motivacion.FechaModificacion = DateTime.Now;
                        motivacion.Estado = true;
                        flagBeficios = true;
                    }
                    argumentos = new List<ProgramaGeneralMotivacionArgumentoBO>();
                    foreach (var subItem in Json.Motivaciones.MotivacionesArgumentos)
                    {
                        ProgramaGeneralMotivacionArgumentoBO argumento;
                        if (repMotivacionArgumento.Exist(subItem.Id ?? 0))
                        {
                            argumento = repMotivacionArgumento.FirstById(subItem.Id ?? 0);
                            argumento.Nombre = subItem.Nombre;
                            argumento.IdPgeneral = Json.Motivaciones.IdPGeneral;
                            motivacion.UsuarioModificacion = Json.Usuario;
                            motivacion.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            argumento = new ProgramaGeneralMotivacionArgumentoBO();
                            argumento.Nombre = subItem.Nombre;
                            argumento.IdPgeneral = Json.Motivaciones.IdPGeneral;
                            argumento.UsuarioCreacion = Json.Usuario;
                            argumento.UsuarioModificacion = Json.Usuario;
                            argumento.FechaCreacion = DateTime.Now;
                            argumento.FechaModificacion = DateTime.Now;
                            argumento.Estado = true;
                        }
                        argumentos.Add(argumento);
                    }
                    modalidadMotivaciones = new List<ProgramaGeneralMotivacionModalidadBO>();
                    foreach (var subItem in Json.Motivaciones.Modalidades)
                    {
                        ProgramaGeneralMotivacionModalidadBO modalidad;
                        if (repMotivacionArgumento.Exist(subItem.Id))
                        {
                            modalidad = repMotivacionModalidad.FirstById(subItem.Id);
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Motivaciones.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralMotivacionModalidadBO();
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Motivaciones.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        modalidadMotivaciones.Add(modalidad);
                    }
                    motivacion.programaGeneralMotivacionArgumento = argumentos;
                    motivacion.ProgramaGeneralMotivacionModalidad = modalidadMotivaciones;
                    if (flagBeficios)
                    {
                        repMotivacion.Insert(motivacion);
                    }
                    else
                    {
                        repMotivacion.Update(motivacion);
                    }

                    scope.Complete();
                }

                return Ok(motivacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Motivacion
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]/{IdProgramaGeneralMotivacion}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarMotivacionVenta(int IdProgramaGeneralMotivacion, string Usuario)
        {

            try
            {
                ProgramaGeneralMotivacionRepositorio repMotivacion = new ProgramaGeneralMotivacionRepositorio(_integraDBContext);
                ProgramaGeneralMotivacionModalidadRepositorio repMotivacionModalidad = new ProgramaGeneralMotivacionModalidadRepositorio(_integraDBContext);
                ProgramaGeneralMotivacionArgumentoRepositorio repMotivacionArgumento = new ProgramaGeneralMotivacionArgumentoRepositorio(_integraDBContext);

                repMotivacion.Delete(IdProgramaGeneralMotivacion, Usuario);

                repMotivacionModalidad.EliminacionLogicoPorIdMotivacion(IdProgramaGeneralMotivacion, Usuario);
                repMotivacionArgumento.EliminacionLogicoPorIdMotivacion(IdProgramaGeneralMotivacion, Usuario);

                bool flagBeficios = false;


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Certificacion
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarCertificacionesVentas([FromBody] CertificacionVentasDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralCertificacionRepositorio repCertificacion = new ProgramaGeneralCertificacionRepositorio(_integraDBContext);
                ProgramaGeneralCertificacionModalidadRepositorio repCertificacionModalidad = new ProgramaGeneralCertificacionModalidadRepositorio(_integraDBContext);
                ProgramaGeneralCertificacionArgumentoRepositorio repCertificacionArgumento = new ProgramaGeneralCertificacionArgumentoRepositorio(_integraDBContext);


                List<ProgramaGeneralCertificacionArgumentoBO> argumentos;
                List<ProgramaGeneralCertificacionModalidadBO> modalidadCertificaciones;
                bool flagBeficios = false;
                ProgramaGeneralCertificacionBO motivacion;
                using (TransactionScope scope = new TransactionScope())
                {

                    //var eliminadosCertificaciones = repCertificacion.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Certificaciones);
                    //repCertificacionModalidad.EliminacionLogicoPorListaCertificacion(Json.Usuario, eliminadosCertificaciones);
                    //repCertificacionArgumento.EliminacionLogicoPorListaCertificaciones(Json.Usuario, eliminadosCertificaciones);                    


                    if (repCertificacion.Exist(Json.Certificaciones.IdCertificacion))
                    {

                        motivacion = repCertificacion.FirstById(Json.Certificaciones.IdCertificacion);
                        motivacion.IdPgeneral = Json.Certificaciones.IdPGeneral;
                        motivacion.Nombre = Json.Certificaciones.NombreCertificacion;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaModificacion = DateTime.Now;

                        repCertificacionArgumento.EliminacionLogicoPorCertificacion(Json.Certificaciones.IdCertificacion, Json.Usuario, Json.Certificaciones.CertificacionesArgumentos);
                        repCertificacionModalidad.EliminacionLogicoPorCertificacion(Json.Certificaciones.IdCertificacion, Json.Usuario, Json.Certificaciones.Modalidades);
                    }
                    else
                    {
                        motivacion = new ProgramaGeneralCertificacionBO();
                        motivacion.IdPgeneral = Json.Certificaciones.IdPGeneral;
                        motivacion.Nombre = Json.Certificaciones.NombreCertificacion;
                        motivacion.UsuarioCreacion = Json.Usuario;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaCreacion = DateTime.Now;
                        motivacion.FechaModificacion = DateTime.Now;
                        motivacion.Estado = true;
                        flagBeficios = true;
                    }
                    argumentos = new List<ProgramaGeneralCertificacionArgumentoBO>();
                    foreach (var subItem in Json.Certificaciones.CertificacionesArgumentos)
                    {
                        ProgramaGeneralCertificacionArgumentoBO argumento;
                        if (repCertificacionArgumento.Exist(subItem.Id ?? 0))
                        {
                            argumento = repCertificacionArgumento.FirstById(subItem.Id ?? 0);
                            argumento.Nombre = subItem.Nombre;
                            argumento.IdPgeneral = Json.Certificaciones.IdPGeneral;
                            motivacion.UsuarioModificacion = Json.Usuario;
                            motivacion.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            argumento = new ProgramaGeneralCertificacionArgumentoBO();
                            argumento.Nombre = subItem.Nombre;
                            argumento.IdPgeneral = Json.Certificaciones.IdPGeneral;
                            argumento.UsuarioCreacion = Json.Usuario;
                            argumento.UsuarioModificacion = Json.Usuario;
                            argumento.FechaCreacion = DateTime.Now;
                            argumento.FechaModificacion = DateTime.Now;
                            argumento.Estado = true;
                        }
                        argumentos.Add(argumento);
                    }
                    modalidadCertificaciones = new List<ProgramaGeneralCertificacionModalidadBO>();
                    foreach (var subItem in Json.Certificaciones.Modalidades)
                    {
                        ProgramaGeneralCertificacionModalidadBO modalidad;
                        if (repCertificacionArgumento.Exist(subItem.Id))
                        {
                            modalidad = repCertificacionModalidad.FirstById(subItem.Id);
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Certificaciones.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralCertificacionModalidadBO();
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Certificaciones.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        modalidadCertificaciones.Add(modalidad);
                    }
                    motivacion.programaGeneralCertificacionArgumento = argumentos;
                    motivacion.ProgramaGeneralCertificacionModalidad = modalidadCertificaciones;
                    if (flagBeficios)
                    {
                        repCertificacion.Insert(motivacion);
                    }
                    else
                    {
                        repCertificacion.Update(motivacion);
                    }

                    scope.Complete();
                }

                return Ok(motivacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Certificacion
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]/{IdProgramaGeneralCertificacion}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarCertificacionVenta(int IdProgramaGeneralCertificacion, string Usuario)
        {

            try
            {
                ProgramaGeneralCertificacionRepositorio repCertificacion = new ProgramaGeneralCertificacionRepositorio(_integraDBContext);
                ProgramaGeneralCertificacionModalidadRepositorio repCertificacionModalidad = new ProgramaGeneralCertificacionModalidadRepositorio(_integraDBContext);
                ProgramaGeneralCertificacionArgumentoRepositorio repCertificacionArgumento = new ProgramaGeneralCertificacionArgumentoRepositorio(_integraDBContext);

                repCertificacion.Delete(IdProgramaGeneralCertificacion, Usuario);

                repCertificacionModalidad.EliminacionLogicoPorIdCertificacion(IdProgramaGeneralCertificacion, Usuario);
                repCertificacionArgumento.EliminacionLogicoPorIdCertificacion(IdProgramaGeneralCertificacion, Usuario);

                bool flagBeficios = false;


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Problema
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarProblemasVentas([FromBody] ProblemaVentasDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralProblemaRepositorio repProblema = new ProgramaGeneralProblemaRepositorio(_integraDBContext);
                ProgramaGeneralProblemaModalidadRepositorio repProblemaModalidad = new ProgramaGeneralProblemaModalidadRepositorio(_integraDBContext);
                ProgramaGeneralProblemaDetalleSolucionRepositorio repProblemaArgumento = new ProgramaGeneralProblemaDetalleSolucionRepositorio(_integraDBContext);


                List<ProgramaGeneralProblemaDetalleSolucionBO> argumentos;
                List<ProgramaGeneralProblemaModalidadBO> modalidadProblemas;
                bool flagBeficios = false;
                ProgramaGeneralProblemaBO motivacion;
                using (TransactionScope scope = new TransactionScope())
                {

                    //var eliminadosProblemas = repProblema.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Problemas);
                    //repProblemaModalidad.EliminacionLogicoPorListaProblema(Json.Usuario, eliminadosProblemas);
                    //repProblemaArgumento.EliminacionLogicoPorListaProblemas(Json.Usuario, eliminadosProblemas);                    


                    if (repProblema.Exist(Json.Problemas.IdProblema))
                    {

                        motivacion = repProblema.FirstById(Json.Problemas.IdProblema);
                        motivacion.IdPgeneral = Json.Problemas.IdPGeneral;
                        motivacion.Nombre = Json.Problemas.NombreProblema;
                        motivacion.EsVisibleAgenda = Json.Problemas.EsVisibleAgenda;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaModificacion = DateTime.Now;

                        repProblemaArgumento.EliminacionLogicoPorProblema(Json.Problemas.IdProblema, Json.Usuario, Json.Problemas.ProblemasArgumentos);
                        repProblemaModalidad.EliminacionLogicoPorProblema(Json.Problemas.IdProblema, Json.Usuario, Json.Problemas.Modalidades);
                    }
                    else
                    {
                        motivacion = new ProgramaGeneralProblemaBO();
                        motivacion.IdPgeneral = Json.Problemas.IdPGeneral;
                        motivacion.Nombre = Json.Problemas.NombreProblema;
                        motivacion.EsVisibleAgenda = Json.Problemas.EsVisibleAgenda;
                        motivacion.UsuarioCreacion = Json.Usuario;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaCreacion = DateTime.Now;
                        motivacion.FechaModificacion = DateTime.Now;
                        motivacion.Estado = true;
                        flagBeficios = true;
                    }
                    argumentos = new List<ProgramaGeneralProblemaDetalleSolucionBO>();
                    foreach (var subItem in Json.Problemas.ProblemasArgumentos)
                    {
                        ProgramaGeneralProblemaDetalleSolucionBO argumento;
                        if (repProblemaArgumento.Exist(subItem.Id ?? 0))
                        {
                            argumento = repProblemaArgumento.FirstById(subItem.Id ?? 0);
                            argumento.Detalle = subItem.Detalle;
                            argumento.Solucion = subItem.Solucion;
                            argumento.IdPgeneral = Json.Problemas.IdPGeneral;
                            motivacion.UsuarioModificacion = Json.Usuario;
                            motivacion.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            argumento = new ProgramaGeneralProblemaDetalleSolucionBO();
                            argumento.Detalle = subItem.Detalle;
                            argumento.Solucion = subItem.Solucion;
                            argumento.IdPgeneral = Json.Problemas.IdPGeneral;
                            argumento.UsuarioCreacion = Json.Usuario;
                            argumento.UsuarioModificacion = Json.Usuario;
                            argumento.FechaCreacion = DateTime.Now;
                            argumento.FechaModificacion = DateTime.Now;
                            argumento.Estado = true;
                        }
                        argumentos.Add(argumento);
                    }
                    modalidadProblemas = new List<ProgramaGeneralProblemaModalidadBO>();
                    foreach (var subItem in Json.Problemas.Modalidades)
                    {
                        ProgramaGeneralProblemaModalidadBO modalidad;
                        if (repProblemaArgumento.Exist(subItem.Id))
                        {
                            modalidad = repProblemaModalidad.FirstById(subItem.Id);
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Problemas.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralProblemaModalidadBO();
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Problemas.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        modalidadProblemas.Add(modalidad);
                    }
                    motivacion.programaGeneralProblemaDetalleSolucion = argumentos;
                    motivacion.ProgramaGeneralProblemaModalidad = modalidadProblemas;
                    if (flagBeficios)
                    {
                        repProblema.Insert(motivacion);
                    }
                    else
                    {
                        repProblema.Update(motivacion);
                    }

                    scope.Complete();
                }

                return Ok(motivacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Problema
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]/{IdProgramaGeneralProblema}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarProblemaVenta(int IdProgramaGeneralProblema, string Usuario)
        {

            try
            {
                ProgramaGeneralProblemaRepositorio repProblema = new ProgramaGeneralProblemaRepositorio(_integraDBContext);
                ProgramaGeneralProblemaModalidadRepositorio repProblemaModalidad = new ProgramaGeneralProblemaModalidadRepositorio(_integraDBContext);
                ProgramaGeneralProblemaDetalleSolucionRepositorio repProblemaArgumento = new ProgramaGeneralProblemaDetalleSolucionRepositorio(_integraDBContext);

                repProblema.Delete(IdProgramaGeneralProblema, Usuario);

                repProblemaModalidad.EliminacionLogicoPorIdProblema(IdProgramaGeneralProblema, Usuario);
                repProblemaArgumento.EliminacionLogicoPorIdProblema(IdProgramaGeneralProblema, Usuario);

                bool flagBeficios = false;


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene Problema
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTodosProblemas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BeneficioPreRequisitoDTO beneficioPreRequisito = new BeneficioPreRequisitoDTO();
                ProgramaGeneralProblemaRepositorio _repProblema = new ProgramaGeneralProblemaRepositorio();
                beneficioPreRequisito.Problemas = _repProblema.ObtenerTodoProblemas();

                return Ok(beneficioPreRequisito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene combos Problema
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosProblemaCliente()
        {
            try
            {

                ModalidadCursoRepositorio repModalidad = new ModalidadCursoRepositorio();
                var combosProgramaGeneral = new
                {
                    Modalidades = repModalidad.ObtenerModalidadCursoFiltro(),
                };

                return Ok(combosProgramaGeneral);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 046/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Problema
        /// </summary>
        /// <returns>bool</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarProblemasVentasV2([FromBody] ProblemaVentasDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProgramaGeneralProblemaRepositorio repProblema = new ProgramaGeneralProblemaRepositorio(_integraDBContext);
                ProgramaGeneralProblemaModalidadRepositorio repProblemaModalidad = new ProgramaGeneralProblemaModalidadRepositorio(_integraDBContext);
                ProgramaGeneralProblemaDetalleSolucionRepositorio repProblemaArgumento = new ProgramaGeneralProblemaDetalleSolucionRepositorio(_integraDBContext);


                List<ProgramaGeneralProblemaDetalleSolucionBO> argumentos;
                List<ProgramaGeneralProblemaModalidadBO> modalidadProblemas;
                bool flagBeficios = false;
                ProgramaGeneralProblemaBO motivacion;
                using (TransactionScope scope = new TransactionScope())
                {

                    //var eliminadosProblemas = repProblema.EliminacionLogicoPorPrograma(Json.IdPGeneral, Json.Usuario, Json.Problemas);
                    //repProblemaModalidad.EliminacionLogicoPorListaProblema(Json.Usuario, eliminadosProblemas);
                    //repProblemaArgumento.EliminacionLogicoPorListaProblemas(Json.Usuario, eliminadosProblemas);                    


                    if (repProblema.Exist(Json.Problemas.IdProblema))
                    {

                        motivacion = repProblema.FirstById(Json.Problemas.IdProblema);
                        motivacion.IdPgeneral = Json.Problemas.IdPGeneral;
                        motivacion.Nombre = Json.Problemas.NombreProblema;
                        motivacion.EsVisibleAgenda = Json.Problemas.EsVisibleAgenda;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaModificacion = DateTime.Now;

                        //repProblemaArgumento.EliminacionLogicoPorProblema(Json.Problemas.IdProblema, Json.Usuario, Json.Problemas.ProblemasArgumentos);
                        repProblemaModalidad.EliminacionLogicoPorProblema(Json.Problemas.IdProblema, Json.Usuario, Json.Problemas.Modalidades);
                    }
                    else
                    {
                        motivacion = new ProgramaGeneralProblemaBO();
                        motivacion.IdPgeneral = Json.Problemas.IdPGeneral;
                        motivacion.Nombre = Json.Problemas.NombreProblema;
                        motivacion.EsVisibleAgenda = Json.Problemas.EsVisibleAgenda;
                        motivacion.UsuarioCreacion = Json.Usuario;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaCreacion = DateTime.Now;
                        motivacion.FechaModificacion = DateTime.Now;
                        motivacion.Estado = true;
                        flagBeficios = true;
                    }
                    argumentos = new List<ProgramaGeneralProblemaDetalleSolucionBO>();

                    modalidadProblemas = new List<ProgramaGeneralProblemaModalidadBO>();
                    foreach (var subItem in Json.Problemas.Modalidades)
                    {
                        ProgramaGeneralProblemaModalidadBO modalidad;
                        if (repProblemaArgumento.Exist(subItem.Id))
                        {
                            modalidad = repProblemaModalidad.FirstById(subItem.Id);
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Problemas.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralProblemaModalidadBO();
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Problemas.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        modalidadProblemas.Add(modalidad);
                    }
                    motivacion.programaGeneralProblemaDetalleSolucion = argumentos;
                    motivacion.ProgramaGeneralProblemaModalidad = modalidadProblemas;
                    if (flagBeficios)
                    {
                        repProblema.Insert(motivacion);
                    }
                    else
                    {
                        repProblema.Update(motivacion);
                    }

                    scope.Complete();
                }

                return Ok(motivacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }

    [Route("api/ProgramaGeneralRegistrosBO")]
    public class ProgramaGeneralRegistrosController : Controller
    {
        public ProgramaGeneralRegistrosController()
        {

        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult listaRegistroProgramaGeneral()
        {
            try
            {
                //PgeneralBO objProgramaGeneral = new PgeneralBO();

                //return Ok(objProgramaGeneral.RegistroProgramaGeneral());
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }


}
