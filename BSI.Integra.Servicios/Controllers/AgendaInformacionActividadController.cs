using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Operaciones.BO;
using System.Transactions;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using System.Net;
using BSI.Integra.Aplicacion.Operaciones.SCode.BO;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Persistencia.Models.AulaVirtual;
using BSI.Integra.Aplicacion.Servicios.SCode.DTOs;
using BSI.Integra.Aplicacion.Operaciones.SCode.Repositorio;
using Microsoft.AspNetCore.Http;
using BSI.Integra.Aplicacion.Base.BO;
using System.Text.RegularExpressions;
using MoreLinq.Extensions;
using MoreLinq;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Comercial/AgendaInformacionActividad
    /// Autor: Carlos Crispin - Fischer Valdez - Johan Cayo - Luis Huallpa - Wilber Choque
    /// Fecha: 09/03/2021
    /// <summary>
    /// Gestiona todo lo referente a la agenda de informacion actividad
    /// </summary>
    [Route("api/AgendaInformacionActividad")]
    public class AgendaInformacionActividadController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;
        private readonly PlantillaClaveValorRepositorio _repPlantillaClaveValor;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly NotaRepositorio _repoNota;
        private readonly MaterialVersionRepositorio _repMaterialVersion;
        private readonly DocumentoOportunidadRepositorio _repDocumentoOportunidad;
        private readonly ProyectoAplicacionEntregaVersionPwRepositorio _repProyectoAplicacionEntregaVersionPw;
        private readonly MatriculaCabeceraBeneficiosRepositorio _repMatriculaCabeceraBeneficios;
        private ListadoEtiquetaBO ListadoEtiqueta;

        public AgendaInformacionActividadController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(_integraDBContext);
            _repOportunidad = new OportunidadRepositorio(_integraDBContext);
            _repoNota = new NotaRepositorio(integraDBContext);
            _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);
            _repDocumentoOportunidad = new DocumentoOportunidadRepositorio(_integraDBContext);
            _repProyectoAplicacionEntregaVersionPw = new ProyectoAplicacionEntregaVersionPwRepositorio(_integraDBContext);
            _repMatriculaCabeceraBeneficios = new MatriculaCabeceraBeneficiosRepositorio(_integraDBContext);
            ListadoEtiqueta = new ListadoEtiquetaBO(_integraDBContext);
        }

        //private readonly integraDBContext _integraDBContext;
        //public AgendaInformacionActividadController()
        //{
        //    _integraDBContext = new integraDBContext();
        //}


        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conteo de Oportunidades Cerradas por el Asesor por Grupos
        /// </summary>
        /// <returns> Objeto DTO </returns>
        /// <returns> objetoDTO: SeguimientoAsesorDTO </returns>
        [Route("[Action]/{IdAsesor}/{IdCategoriaOrigen}/{EstadoPantalla2}")]
        [HttpGet]
        public ActionResult GetPantalla1(int IdAsesor, int IdCategoriaOrigen, int EstadoPantalla2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadMaximaPorCategoriaBO OportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaBO();
                OportunidadMaximaPorCategoria.IdPersonal = IdAsesor;
                OportunidadMaximaPorCategoria.IdTipoCategoriaOrigen = IdCategoriaOrigen;
                OportunidadMaximaPorCategoria.estadoPantalla2 = EstadoPantalla2;

                if (!OportunidadMaximaPorCategoria.HasErrors)
                {
                    OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio();
                    var informacionAsesor = _repOportunidadMaximaPorCategoria.CargarSeguimientoAsesor(OportunidadMaximaPorCategoria);
                    return Ok(informacionAsesor);
                }
                else
                    return BadRequest(OportunidadMaximaPorCategoria.GetErrors(null));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conteo de Oportunidades Cerradas por el Asesor por Grupos
        /// </summary>
        /// <returns> Objeto DTO </returns>
        /// <returns> objetoDTO: SeguimientoAsesorDTO </returns>
        [Route("[Action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerCuponAlumno(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                var cuponAlumno = _repAlumno.ObtenerCuponPorIdAlumno(IdAlumno);
                return Ok(cuponAlumno);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Carga una lista de oportunidades con venta cruzada por idAlumno y Carga un historial de oportunidades por idAlumno
        /// </summary>
        /// <returns> Devuelve un objeto BO de la Oportunidad </returns>
        /// <returns> Objeto BO : OportunidadInformacionBO </returns>
        [Route("[action]/{IdAlumno}/{IdClasificacionPersona}")]
        [HttpGet]
        public ActionResult GetInformacionOportunidad(int IdAlumno, int IdClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadInformacionBO oportunidadInformacion = new OportunidadInformacionBO(IdAlumno, IdClasificacionPersona);
                return Ok(oportunidadInformacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de Oportunidad Competidor por Oportunidad
        /// </summary>
        /// <returns> Lista de información de Oportunidad Competidor por Oportunidad </returns>
        /// <returns> Lista objeto DTO : List<OportunidadCompetidoresDTO> </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public IActionResult GetInformacionOportunidaCompetidores(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdOportunidad <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(202));
            }
            try
            {
                CompetidorRepositorio _competidorRepositorio = new CompetidorRepositorio();
                List<OportunidadCompetidoresDTO> listaCompetidores = _competidorRepositorio.CargarOportunidadCompetidores(IdOportunidad);

                var idAnterior = 0;
                var _DatosCompetidores = new OportunidadCompetidoresDTO();
                List<OportunidadCompetidoresDTO> listaCompetidoresAgrupado = new List<OportunidadCompetidoresDTO>();
                foreach (var item in listaCompetidores)
                {
                    if (idAnterior != 0 && idAnterior != item.Id)
                    {
                        listaCompetidoresAgrupado.Add(_DatosCompetidores);
                    }
                    if (idAnterior == item.Id)
                    {
                        _DatosCompetidores.ContenidoCompetidorVentajaDesventaja += item.ContenidoCompetidorVentajaDesventaja ?? "Sin Desventaja";
                        continue;
                    }
                    _DatosCompetidores = new OportunidadCompetidoresDTO();
                    idAnterior = item.Id;
                    _DatosCompetidores.Id = item.Id;
                    _DatosCompetidores.IdOportunidad = item.IdOportunidad;
                    _DatosCompetidores.Nombre = item.Nombre;
                    _DatosCompetidores.DuracionCronologica = item.DuracionCronologica;
                    _DatosCompetidores.CostoNeto = item.CostoNeto;
                    _DatosCompetidores.Precio = item.Precio;
                    _DatosCompetidores.Categoria = item.Categoria;
                    _DatosCompetidores.Empresa = item.Empresa;
                    _DatosCompetidores.RegionCiudad = item.RegionCiudad;
                    _DatosCompetidores.Moneda = item.Moneda;
                    _DatosCompetidores.IdCompetidorVentajaDesventaja = item.IdCompetidorVentajaDesventaja == null ? 0 : item.IdCompetidorVentajaDesventaja.Value;
                    _DatosCompetidores.ContenidoCompetidorVentajaDesventaja = item.ContenidoCompetidorVentajaDesventaja == null ? "Sin Desventaja" : item.ContenidoCompetidorVentajaDesventaja;
                    _DatosCompetidores.TipoCompetidorVentajaDesventaja = item.TipoCompetidorVentajaDesventaja == null ? 0 : item.TipoCompetidorVentajaDesventaja.Value;
                }

                return Ok(listaCompetidoresAgrupado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas Frecuentes
        /// </summary>
        /// <returns> Lista de Preguntas Frecuentes </returns>
        /// <returns> Lista objeto DTO : List<PreguntaFrecuenteSeccionesDTO> </returns>
        [Route("[Action]/{IdCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerPreguntasFrecuentes(int idCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InformacionProgramaBO objeto = new InformacionProgramaBO();
                integraDBContext contexto = new integraDBContext();
                PgeneralRepositorio _repGPreguntasFrecuentes = new PgeneralRepositorio(contexto);
                PreguntaFrecuentePgeneralRepositorio _repPreguntaFrecuentePgeneral = new PreguntaFrecuentePgeneralRepositorio(contexto);

                var repositorioGeneral = _repGPreguntasFrecuentes.ObtenerDatosPFrecuentes(idCentroCosto);
                var repositorioPreguntaFrecuente = _repPreguntaFrecuentePgeneral.ObtenerPreguntaFrecuente(repositorioGeneral);
                var data = objeto.CargarInformacionPrograma(repositorioPreguntaFrecuente);
                return Ok(new { data });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas Frecuentes de Cambio
        /// </summary>
        /// <returns> Lista de Preguntas Frecuentes de Cambio </returns>
        /// <returns> Lista objeto DTO : List<PreguntaFrecuenteSeccionesDTO> </returns>
        [Route("[Action]/{IdCentroCosto}/{IdPrograma}")]
        [HttpGet]
        public ActionResult ObtenerPreguntasFrecuentesCambio(int IdCentroCosto, int IdPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InformacionProgramaBO objeto = new InformacionProgramaBO();
                integraDBContext contexto = new integraDBContext();
                PgeneralRepositorio _repGPreguntasFrecuentes = new PgeneralRepositorio(contexto);
                PespecificoRepositorio _repEPreguntasFrecuentes = new PespecificoRepositorio(contexto);
                PreguntaFrecuentePgeneralRepositorio _repPreguntaFrecuentePgeneral = new PreguntaFrecuentePgeneralRepositorio(contexto);

                var repositorioGeneral = _repGPreguntasFrecuentes.ObtenerAreaSubArea(IdPrograma);
                var repositorioEspecifico = _repEPreguntasFrecuentes.ObtenerTipoId(IdCentroCosto);
                var repositorioPreguntaFrecuente = _repPreguntaFrecuentePgeneral.ObtenerPreguntaFrecuenteCambio(IdPrograma, repositorioGeneral.IdArea, repositorioGeneral.IdSubArea, repositorioEspecifico.TipoId);
                var data = objeto.CargarInformacionProgramaChange(repositorioPreguntaFrecuente);
                return Ok(new { data });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Resumen de Programas
        /// </summary>
        /// <returns> Obtiene Resumen de Programas </returns>
        /// <returns> Lista objeto DTO : List<ResumenProgramaDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetResumenProgramas([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InformacionProgramaBO informacionProgramaBO = new InformacionProgramaBO()
                {
                    Filtros = Filtros
                };
                informacionProgramaBO.CargarResumenProgramas();
                return Ok(informacionProgramaBO.ResumenProgramas);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas V2
        /// </summary>
        /// <returns> Lista de objeto DTO de resumen de programas V2 </returns>
        /// <returns> Lista objeto DTO : List<ResumenProgramaV2DTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetResumenProgramasV2([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InformacionProgramaBO informacionProgramaBO = new InformacionProgramaBO()
                {
                    Filtros = Filtros
                };
                informacionProgramaBO.CargarResumenProgramasV2();
                return Ok(informacionProgramaBO.ResumenProgramasV2);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas
        /// </summary>
        /// <returns> Lista de objeto DTO de resumen de programas </returns>
        /// <returns> Lista objeto DTO : List<ResumenProgramaDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetInformacionPrograma([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int idPgeneral = 0;
                int codigoPais = 0;
                bool successPgeneral = Int32.TryParse(Filtros["idPGeneral"], out idPgeneral);
                bool successCodigoPais = Int32.TryParse(Filtros["codigoPais"], out codigoPais);
                InformacionProgramaBO informacionPrograma = new InformacionProgramaBO()
                {
                    IdPGeneral = idPgeneral,
                    CodigoPais = codigoPais
                };
                informacionPrograma.CargarInformacionPrograma();
                return Ok(new { informacionPrograma.InformacionPrograma });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas Versión 1
        /// </summary>
        /// <returns> Lista de objeto DTO de resumen de programas </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetInformacionProgramav1([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InformacionProgramaBO informacionPrograma = new InformacionProgramaBO()
                {
                    IdCentroCosto = Convert.ToInt32(Filtros["idCentroCosto"]),
                    CodigoPais = Convert.ToInt32(Filtros["codigoPais"]),
                    IdMatriculaCabecera = Convert.ToInt32(Filtros["idMatriculaCabecera"]),
                    IdOportunidad = Convert.ToInt32(Filtros["IdOportunidad"])
                };
                informacionPrograma.CargarInformacionProgramaAutomatico();
                return Ok(new { informacionPrograma.IdPGeneral, informacionPrograma.InformacionPrograma, ResumenProgramas = informacionPrograma.ResumenProgramasV2, informacionPrograma.EtiquetaDuracionHorarios, informacionPrograma.EtiquetaExpositores, informacionPrograma.EtiquetaBeneficiosInversion, informacionPrograma.EtiquetaFormasPago, informacionPrograma.EtiquetaTarifarios, informacionPrograma.ListaBeneficios });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Speech para Paso 2 por Oportunidad
        /// </summary>
        /// <returns> objeto información Speech </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult GetSpeechPaso2(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TiempoCapacitacionRepositorio _repTiempoCapacitacion = new TiempoCapacitacionRepositorio();
                var tiempoCapacitacion = _repTiempoCapacitacion.ObtenerTodoFiltro();
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                var oportunidad = _repOportunidad.ObtenerOportunidadTiempoCapacitacion(IdOportunidad);
                if (oportunidad.IdTiempoCapacitacion == null)
                {
                    if (oportunidad.IdTiempoCapacitacionValidacion == null)
                    {
                        return Ok(new { Records = tiempoCapacitacion, Record = oportunidad.IdTiempoCapacitacion, Lista = false, ListaValidacion = false, RecordValidado = oportunidad.IdTiempoCapacitacionValidacion });
                    }
                    else
                    {
                        return Ok(new { Records = tiempoCapacitacion, Record = oportunidad.IdTiempoCapacitacion, Lista = false, ListaValidacion = true, RecordValidado = oportunidad.IdTiempoCapacitacionValidacion });
                    }
                }
                else
                {
                    if (oportunidad.IdTiempoCapacitacionValidacion == null)
                    {
                        return Ok(new { Records = tiempoCapacitacion, Record = oportunidad.IdTiempoCapacitacion, Lista = true, ListaValidacion = false, RecordValidado = oportunidad.IdTiempoCapacitacionValidacion });
                    }
                    else
                    {
                        return Ok(new { Records = tiempoCapacitacion, Record = oportunidad.IdTiempoCapacitacion, Lista = true, ListaValidacion = true, RecordValidado = oportunidad.IdTiempoCapacitacionValidacion });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Competidores Registrados
        /// </summary>
        /// <returns> Lista de competidores Registrados </returns>
        /// <returns> Lita de objeto DTO : List<EmpresaFiltroDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult GetCompetidores()
        {
            try
            {
                EmpresaBO empresa = new EmpresaBO();
                return Ok(empresa.ObtenerTodoCompetidores());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene los prerequisitos, beneficios y empresa competidora para un Programa General
        /// </summary>
        /// <returns> Lista de prerequisitos, beneficios y empresa competidora </returns>
        /// <returns> Objeto DTO : ProgramaGeneralPreBenCompuestoDTO </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult GetSpeechPaso56Argumentos(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadInformacionBO oportunidadInformacionBO = new OportunidadInformacionBO();
                oportunidadInformacionBO.CargarPrerequisitosBeneficios(IdOportunidad);
                return Ok(oportunidadInformacionBO.ProgramaGeneralPreBen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene Speech Bienvenida y Despedida por actividad detalle
        /// </summary>
        /// <returns> Speech Bienvenida y Despedida por actividad detalle </returns>
        /// <returns> Objeto DTO : SpeechBienvenidaDespedidaDTO </returns>
        [Route("[action]/{IdActividadDetalle}")]
        [HttpGet]
        public ActionResult GetIdSpeechBienvenidaDespedida(int IdActividadDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SpeechBienvenidaDespedidaDTO speechBienvenidaDespedida = new SpeechBienvenidaDespedidaDTO();
                PlantillaBaseRepositorio plantillaBase = new PlantillaBaseRepositorio();
                var idSpeechBienvenida = plantillaBase.ObtenerIdPorNombre("speech");
                speechBienvenidaDespedida.IdPlantillaBienvenida = plantillaBase.ObtenerIdPlantillaSpeechBienvenida(IdActividadDetalle, idSpeechBienvenida.Id).IdPlantillaBienvenida;
                var idSpeechDespedida = plantillaBase.ObtenerIdPorNombre("Speech Despedida");
                speechBienvenidaDespedida.IdPlantillaDespedida = plantillaBase.ObtenerIdPlantillaSpeechDespedida(IdActividadDetalle, idSpeechDespedida.Id).IdPlantillaDespedida;
                return Ok(new { data = speechBienvenidaDespedida });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene Historial de Interacciones de Oportunidad
        /// </summary>
        /// <returns> Información de Interacciones de Oportunidad  </returns>
        /// <returns> Lista de Objeto DTO : List<ReporteSeguimientoOportunidadLogGridDTO> </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult GetHistorialInteraccionesOportunidad(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                Reportes reporte = new Reportes();
                var resultado = reporte.ObtenerOportunidadesLog(IdOportunidad);
                var listanueva = new List<ReporteSeguimientoOportunidadLogGridDTO>
                {
                    resultado.Where(x => x.Estado == "NO EJECUTADO").FirstOrDefault()
                };
                listanueva.AddRange(resultado.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());

                return Ok(listanueva);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene Historial de Interacciones de Oportunidad
        /// </summary>
        /// <returns> Información de Interacciones de Oportunidad  </returns>
        /// <returns> Lista de Objeto DTO : List<ReporteSeguimientoOportunidadLogGridDTO> </returns>
        [Route("[action]/{IdAlumno}/{IdOportunidad}/{IdPadre}")]
        [HttpGet]
        public ActionResult ObtenerHistorialInteraccionesOportunidad(int IdAlumno, int? IdOportunidad, int? IdPadre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                Reportes reporte = new Reportes();
                int IdOportunidadtemp = IdOportunidad == null ? 0 : IdOportunidad.Value;
                int IdPadretemp = IdPadre == null ? 0 : IdPadre.Value;
                var resultado = reporte.ObtenerOportunidadesLogPorAlumno(IdAlumno, IdOportunidadtemp, IdPadretemp);
                var listanueva = new List<ReporteSeguimientoOportunidadLogGridDTO>
                {
                    resultado.Where(x => x.Estado == "NO EJECUTADO").FirstOrDefault()
                };
                listanueva.AddRange(resultado.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());

                return Ok(listanueva);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida si existe una oportunidad en seguimiento para el mismo centro costo
        /// </summary>
        /// <returns> Confirmación de validación </returns>
        /// <returns> Bool </returns>
        [Route("[action]/{IdContacto}/{IdCentroCosto}/{IdOcurrencia}")]
        [HttpGet]
        public ActionResult ValidarRN2(int IdContacto, int IdCentroCosto, int IdOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                return Ok(_repOportunidad.ValidarRN2(IdContacto, IdCentroCosto, IdOcurrencia));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Valores de Oportunidad PEspecifico, Pgeneral Por la ActividadDetalle y Programas Especificos
        /// </summary>
        /// <returns> Valores de Oportunidad PEspecifico, Pgeneral Por la ActividadDetalle y Programas Especificos </returns>
        /// <returns> Objeto BO : DocumentosBO </returns>
        [Route("[Action]/{IdActividadDetalle}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadPespecifico(int IdActividadDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentosBO Objeto = new DocumentosBO();
                Objeto.ObtenerOportunidadPespecifico(IdActividadDetalle);
                return Ok(Objeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Documentos por Actividad Detalle
        /// </summary>
        /// <returns> Documentos por Actividad Detalle </returns>
        /// <returns> Objeto BO : DocumentosBO </returns>
        [Route("[Action]/{IdActividadDetalle}")]
        [HttpGet]
        public ActionResult ObtenerDocumentos(int IdActividadDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentosBO Objeto = new DocumentosBO();
                Objeto.ObtenerDocumentos(IdActividadDetalle);
                Objeto.ListarAlertarAListadoDocumentos();
                return Ok(Objeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Documentos Whatsapp por Alumno
        /// </summary>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Objeto BO : DocumentosBO </returns>
        [Route("[Action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerDocumentosWhatsApp(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                DocumentosBO Objeto = new DocumentosBO();
                Objeto.ObtenerDocumentosIdAlumno(IdAlumno);

                return Ok(Objeto);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<CorreoInteraccionesAlumnoDTO> </returns>
        [Route("[Action]/{IdAlumno}/{IdAsesor}")]
        [HttpGet]
        public ActionResult GetInteraccionesCorreosEnviados(int IdAlumno, int IdAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdAlumno <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(205));
            }
            if (IdAsesor <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                MandrilRepositorio _mandrilRepositorio = new MandrilRepositorio(_integraDBContext);
                return Ok(_mandrilRepositorio.ListaInteraccionCorreoAlumnoPorOportunidad(IdAlumno, IdAsesor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<CorreoInteraccionesAlumnoDTO> </returns>
        [Route("[Action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult GetInteraccionesCorreosEnviados(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdAlumno <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(205));
            }
            try
            {
                MandrilRepositorio _mandrilRepositorio = new MandrilRepositorio(_integraDBContext);
                return Ok(_mandrilRepositorio.ListaInteraccionCorreoAlumnoPorOportunidad(IdAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una Lista de problemas cliente por idOportunidad
        /// </summary>
        /// <returns> Lista de problemas cliente </returns>
        /// <returns> Lista de Objeto DTO : List<OportunidadProblemaClienteDTO> </returns>
        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult GetOportunidadProblemasCliente(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                return Ok(_repOportunidad.ObtenerOportunidadProblemasCliente(IdOportunidad));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Un Compuesto de Valores de Etiqueta para Remplazar en las Plantillas
        /// </summary>
        /// <returns> Compuesto de Valores de Etiqueta </returns>
        /// <returns> objetos Agrupados </returns>
        [Route("[Action]/{IdCentroCosto}/{IdFaseOportunidad}/{IdOportunidad}")]
        [HttpGet]
        public ActionResult GetValorEtiqueta(int IdCentroCosto, int IdFaseOportunidad, int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            PgeneralTipoDescuentoRepositorio _repPgeneralTipoDescuento = new PgeneralTipoDescuentoRepositorio(_integraDBContext);
            PlantillaPwBO Objeto = new PlantillaPwBO();
            Objeto.GetValorEtiqueta(IdCentroCosto, IdFaseOportunidad, IdOportunidad);

            var DatosOportunidad = Objeto.ObtenerDatosOportunidad(IdOportunidad);
            string FechaInicioPrograma = "";

            var Promocion = _repPgeneralTipoDescuento.FirstBy(w => w.Estado == true && w.IdPgeneral == DatosOportunidad.IdPgeneral.Value && w.IdTipoDescuento == 143, y => new { y.FlagPromocion });
            if (Promocion != null)
            {
                DatosOportunidad.Promocion25 = Promocion.FlagPromocion;
            }
            //FechaInicioPrograma = Objeto.ObtenerFechaInicioPrograma(DatosOportunidad.IdPgeneral.Value, DatosOportunidad.IdCentroCosto.Value);
            FechaInicioPrograma = ListadoEtiqueta.FechaInicioProgramaV2(DatosOportunidad.IdPgeneral.Value);
            //var etiquetaDuracionYHorarios = Objeto.DuracionYHorarios(DatosOportunidad.IdCentroCosto);
            DatosOportunidad.CostoTotalConDescuento = Objeto.ObtenerCostoTotalConDescuento(IdOportunidad);
            Objeto.ObtenerDatosProgramaGeneral(DatosOportunidad.IdPgeneral.Value);

            return Ok(new { Objeto, DatosOportunidad, FechaInicioPrograma });
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Los Valores Para Etiquetas Lista de Programas
        /// </summary>
        /// <returns> Valores Para Etiquetas </returns>
        /// <returns> string </returns>
        [Route("[Action]/{IdOportunidad}/{IdAreaEtiqueta}")]
        [HttpGet]
        public ActionResult GetValorEtiquetaListas(int IdOportunidad, int IdAreaEtiqueta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaPwBO Objeto = new PlantillaPwBO();
                return Ok(Objeto.GetValorEtiquetaListas(IdOportunidad, IdAreaEtiqueta));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Información de Actividad de Oportunidad Ejecutada
        /// </summary>
        /// <returns> Información de Actividad de Oportunidad Ejecutada </returns>
        /// <returns> Vacio </returns>
        [Route("[Action]/{idActividadDetalle}")]
        [HttpGet]
        public ActionResult OportunidadActividadEjecutada(int idActividadDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Aplicacion.Transversal.BO.OportunidadBO oportunidad = new Aplicacion.Transversal.BO.OportunidadBO();
                //oportunidad.GetAgendaRealizadaRegistroRealTime(IdActividadDetalle);
                //return Ok(Objeto._actividadEjecutada);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene Información de alumno, Sueldo Promedio y valida su visualización en la agenda
        /// </summary>
        /// <returns> Información de Alumno </returns>
        /// <returns> objetos Agrupados </returns>
        [Route("[Action]/{IdClasificacionPersona}/{IdOportunidad}/{IdPersonal}")]
        [HttpGet]
        public ActionResult DatosAlumno(int IdClasificacionPersona, int IdOportunidad, int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();

                SentinelBO Sentinel = new SentinelBO();
                var alumno = _repAlumno.ObtenerDatosAlumno(IdClasificacionPersona);
                if (alumno.IdCargo == null)
                    alumno.IdCargo = 11;
                if (alumno.IdIndustria == null)
                    alumno.IdIndustria = 48;

                if (alumno.DNI == null)
                    alumno.DNI = "";

                var probabilidadsueldo = Sentinel.GetPromedioSueldo(alumno.IdEmpresa, alumno.DNI, alumno.IdCargo, alumno.IdIndustria);

                var VisualizarDatos = _repAlumno.ValidarVisualizarAgenda(IdOportunidad, IdPersonal);//0:no puede ver pero puede ingresar solicitud,1:puede verlo hasta la fecha,2:no puede verlo y no puede ingresar

                if (VisualizarDatos == null)
                {
                    VisualizarDatos = new ResultadoFinalVisualizarOportunidadDTO();
                    VisualizarDatos.Id = 0;
                    VisualizarDatos.FechaVisibleHasta = DateTime.Now;
                    VisualizarDatos.Valor = 0;
                }

                return Ok(new { alumno, probabilidadsueldo, VisualizarDatos });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida si puede mostrarse o no en la agenda
        /// </summary>
        /// <returns> Confirmación de Validación </returns>
        /// <returns> Bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarSolicitudVisualizarDatosOportunidad([FromBody] SolicitudVisualizarOportunidadDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();

                var VisualizarDatos = _repAlumno.InsertarSolicitudVisualizarDatosOportunidad(DTO.IdOportunidad, DTO.IdPersonal);//0:no puede ver pero puede ingresar solicitud,1:puede verlo hasta la fecha,2:no puede verlo y no puede ingresar

                if (VisualizarDatos.Valor == 1)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest("No se puede solicitar ya que cuenta con una solicitud pendiente");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Email Principal de Alumno
        /// </summary>
        /// <returns> Información de Alumno </returns>
        /// <returns> objetoBO : AlumnoBO </returns>
        [Route("[Action]/{usuario}/{AreaTrabajo}")]
        [HttpPost]
        public ActionResult ActualizarEmailPrincipalAlumno([FromBody] AlumnoActualizarEmailPrincipalDTO alumnocorreo, string usuario, string AreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (alumnocorreo != null)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                    ClasificacionPersonaRepositorio _repClasificacionPersona = new ClasificacionPersonaRepositorio(_integraDBContext);
                    PersonaRepositorio _repPersona = new PersonaRepositorio(_integraDBContext);
                    AlumnoLogRepositorio alumnoLogRepositorio = new AlumnoLogRepositorio(_integraDBContext);
                    List<AlumnoLogBO> alumnoLogBOs = new List<AlumnoLogBO>();

                    var existealumno = _repAlumno.FirstBy(w => w.Email1 == alumnocorreo.EmailAPrincipal);
                    if (existealumno != null)
                    {
                        return BadRequest("Ya Existe un alumno con ese correo principal");
                    }
                    else
                    {

                        ClasificacionPersonaBO ClasificacionPersona = _repClasificacionPersona.FirstBy(w => w.IdTipoPersona == 1 && w.IdTablaOriginal == alumnocorreo.IdAlumno);
                        if (ClasificacionPersona != null)
                        {
                            PersonaBO persona = _repPersona.FirstById(ClasificacionPersona.IdPersona);
                            if (persona != null)
                            {
                                //ACTUALIZO ALUMNO
                                AlumnoBO Alumno = _repAlumno.FirstById(alumnocorreo.IdAlumno);
                                string correoantiguo = Alumno.Email1;
                                Alumno.Email1 = alumnocorreo.EmailAPrincipal;
                                Alumno.ValidarEstadoContactoWhatsAppTemporal(_integraDBContext);
                                _repAlumno.Update(Alumno);
                                //ACTUALIZO PERSONA
                                persona.Email1 = alumnocorreo.EmailAPrincipal;
                                _repPersona.Update(persona);
                                //SE INSERTA LOG
                                alumnoLogBOs.Add(new AlumnoLogBO(alumnocorreo.IdAlumno, "Email 1", correoantiguo, alumnocorreo.EmailAPrincipal, usuario));
                                alumnoLogRepositorio.Insert(alumnoLogBOs);

                                return Ok(Alumno);
                            }
                            else
                            {
                                return BadRequest("No existe Persona");
                            }
                        }
                        else
                        {
                            return BadRequest("No existe Clasificacion Persona");
                        }
                    }
                }
                else
                {
                    return BadRequest("No se enviaron datos");
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Carlos Crispin-Jose Villena.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Datos Personales Alumno
        /// </summary>
        /// <returns>Objeto(Alumno)<returns>
        [Route("[Action]/{usuario}/{AreaTrabajo}")]
        [HttpPost]
        public ActionResult ActualizarAlumno([FromBody] AlumnoActualizarDTO Alumno, string usuario, string AreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlumnoRepositorio repAlumno = new AlumnoRepositorio(_integraDBContext);
                CiudadRepositorio repCiudad = new CiudadRepositorio(_integraDBContext);
                AlumnoLogRepositorio alumnoLogRepositorio = new AlumnoLogRepositorio(_integraDBContext);
                List<AlumnoLogBO> alumnoLogBOs = new List<AlumnoLogBO>();
                var validarEmail1 = repAlumno.ValidarEmail1Alumno(Alumno.Email1);
                var validarEmail2 = repAlumno.ValidarEmail2Alumno(Alumno.Email2);

                if (validarEmail1 != null)
                {
                    if (Alumno.Email1 == validarEmail1.Email1 && Alumno.Id != validarEmail1.Id) throw new ArgumentException("El Email ya existe");
                }
                if (validarEmail2 != null)
                {
                    if (Alumno.Email2 == validarEmail2.Email1 && Alumno.Id != validarEmail2.Id) throw new ArgumentException("El Email ya existe");
                }

                if (Alumno.IdEmpresa == 0) Alumno.IdEmpresa = null;

                AlumnoBO alumnoBO = repAlumno.FirstById(Alumno.Id);

                if (alumnoBO.Nombre1 != Alumno.Nombre1) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Nombre 1", alumnoBO.Nombre1 ?? "", Alumno.Nombre1 ?? "", usuario));
                if (alumnoBO.Nombre2 != Alumno.Nombre2 && (alumnoBO.Nombre2 != null || Alumno.Nombre2 != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Nombre 2", alumnoBO.Nombre2 ?? "", Alumno.Nombre2 ?? "", usuario));
                if (alumnoBO.ApellidoPaterno != Alumno.ApellidoPaterno) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Apellido Paterno", alumnoBO.ApellidoPaterno ?? "", Alumno.ApellidoPaterno ?? "", usuario));
                if (alumnoBO.ApellidoMaterno != Alumno.ApellidoMaterno && (alumnoBO.ApellidoMaterno != null || Alumno.ApellidoMaterno != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Apellido Materno", alumnoBO.ApellidoMaterno ?? "", Alumno.ApellidoMaterno ?? "", usuario));
                if (alumnoBO.Dni != Alumno.Dni && (alumnoBO.Dni != null || Alumno.Dni != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Dni", alumnoBO.Dni ?? "", Alumno.Dni ?? "", usuario));
                if (alumnoBO.Email2 != Alumno.Email2 && (alumnoBO.Email2 != null || Alumno.Email2 != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Email 2", alumnoBO.Email2 ?? "", Alumno.Email2 ?? "", usuario));
                if (alumnoBO.Celular2 != Alumno.Celular2 && (alumnoBO.Celular2 != null || Alumno.Celular2 != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Celular 2", alumnoBO.Celular2 ?? "", Alumno.Celular2 ?? "", usuario));
                if (alumnoBO.Telefono != Alumno.Telefono && (alumnoBO.Telefono != null || Alumno.Telefono != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Telefono", alumnoBO.Telefono ?? "", Alumno.Telefono ?? "", usuario));
                if (alumnoBO.Telefono2 != Alumno.Telefono2 && (alumnoBO.Telefono2 != null || Alumno.Telefono2 != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Telefono 2", alumnoBO.Telefono2 ?? "", Alumno.Telefono2 ?? "", usuario));
                if (alumnoBO.Direccion != Alumno.Direccion && (alumnoBO.Direccion != null || Alumno.Direccion != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Direccion", alumnoBO.Direccion ?? "", Alumno.Direccion ?? "", usuario));

                if (alumnoBO.IdCargo != Alumno.IdCargo && alumnoBO.Cargo != Alumno.Cargo) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Cargo", alumnoBO.Cargo ?? "", Alumno.Cargo ?? "", usuario));
                if (alumnoBO.IdAtrabajo != Alumno.IdAtrabajo && alumnoBO.Atrabajo != Alumno.Atrabajo) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Trabajo", alumnoBO.Atrabajo ?? "", Alumno.Atrabajo ?? "", usuario));
                if (alumnoBO.IdEmpresa != Alumno.IdEmpresa && alumnoBO.Empresa != Alumno.Empresa) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Empresa", alumnoBO.Empresa ?? "", Alumno.Empresa ?? "", usuario));
                if (alumnoBO.IdAformacion != Alumno.IdAformacion && alumnoBO.Aformacion != Alumno.Aformacion) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Formacion", alumnoBO.Aformacion ?? "", Alumno.Aformacion ?? "", usuario));
                if (alumnoBO.IdIndustria != Alumno.IdIndustria && alumnoBO.Industria != Alumno.Industria) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Industria", alumnoBO.Industria ?? "", Alumno.Industria ?? "", usuario));
                if (Alumno.IdCiudad != null)
                {
                    var ciudadAlumnoDestino = repCiudad.ObtenerNombreCiudadPorId(Alumno.IdCiudad.Value);
                    if (Alumno.Ciudad == null || alumnoBO.IdCiudad == 0)
                    {
                        if (alumnoBO.IdCiudad != Alumno.IdCiudad) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Ciudad", Alumno.Ciudad ?? "", ciudadAlumnoDestino.Nombre ?? "", usuario));
                    }
                    else
                    {
                        if (alumnoBO.IdCiudad != Alumno.IdCiudad) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Ciudad", Alumno.Ciudad.ToString() ?? "", ciudadAlumnoDestino.Nombre ?? "", usuario));
                    }
                }
                //alumnoBO.Id = Alumno.Id;
                alumnoBO.Nombre1 = Alumno.Nombre1;
                alumnoBO.Nombre2 = Alumno.Nombre2;
                alumnoBO.ApellidoPaterno = Alumno.ApellidoPaterno;
                alumnoBO.ApellidoMaterno = Alumno.ApellidoMaterno;
                alumnoBO.Dni = Alumno.Dni;
                //Email1 = Alumno.Email1,
                alumnoBO.Email2 = Alumno.Email2;
                alumnoBO.Celular = Alumno.Celular;
                alumnoBO.Celular2 = Alumno.Celular2;
                alumnoBO.Telefono = Alumno.Telefono;
                alumnoBO.Telefono2 = Alumno.Telefono2;
                alumnoBO.Direccion = Alumno.Direccion;
                if (alumnoBO.IdPais == null || alumnoBO.IdPais == 0)
                {
                    alumnoBO.IdPais = Alumno.IdCodigoPais;
                }
                alumnoBO.IdCargo = Alumno.IdCargo;
                alumnoBO.Cargo = Alumno.Cargo;
                alumnoBO.IdAtrabajo = Alumno.IdAtrabajo;
                alumnoBO.Atrabajo = Alumno.Atrabajo;
                if (alumnoBO.IdEmpresa != Alumno.IdEmpresa) alumnoBO.Empresa = Alumno.Empresa;
                alumnoBO.IdEmpresa = Alumno.IdEmpresa;
                alumnoBO.IdAformacion = Alumno.IdAformacion;
                alumnoBO.Aformacion = Alumno.Aformacion;
                alumnoBO.IdIndustria = Alumno.IdIndustria;
                alumnoBO.Industria = Alumno.Industria;
                alumnoBO.IdCiudad = Alumno.IdCiudad;
                alumnoBO.FechaModificacion = DateTime.Now;
                alumnoBO.UsuarioModificacion = usuario == null ? "" : usuario;

                if (AreaTrabajo == "OP")
                {
                    if (alumnoBO.Genero != Alumno.Genero && (alumnoBO.Genero != null || Alumno.Genero != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Genero", alumnoBO.Genero ?? "", Alumno.Genero ?? "", usuario));
                    if (alumnoBO.Parentesco != Alumno.Parentesco && (alumnoBO.Parentesco != null || Alumno.Parentesco != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Parentesco", alumnoBO.Parentesco ?? "", Alumno.Parentesco ?? "", usuario));
                    if (alumnoBO.NombreFamiliar != Alumno.NombreFamiliar && (alumnoBO.NombreFamiliar != null || Alumno.NombreFamiliar != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Nombre Familiar", alumnoBO.NombreFamiliar ?? "", Alumno.NombreFamiliar ?? "", usuario));
                    if (alumnoBO.TelefonoFamiliar != Alumno.TelefonoFamiliar && (alumnoBO.TelefonoFamiliar != null || Alumno.TelefonoFamiliar != "")) alumnoLogBOs.Add(new AlumnoLogBO(Alumno.Id, "Telefono Familiar", alumnoBO.TelefonoFamiliar ?? "", Alumno.TelefonoFamiliar ?? "", usuario));

                    alumnoBO.Genero = Alumno.Genero;
                    alumnoBO.Parentesco = Alumno.Parentesco;
                    alumnoBO.NombreFamiliar = Alumno.NombreFamiliar;
                    alumnoBO.TelefonoFamiliar = Alumno.TelefonoFamiliar;
                    //FechaNacimiento = alumno.FechaNacimiento,
                }

                alumnoBO.ValidarEstadoContactoWhatsAppTemporal(_integraDBContext);

                if (!alumnoBO.HasErrors)
                {
                    repAlumno.Update(alumnoBO);
                    alumnoLogRepositorio.Insert(alumnoLogBOs);
                    //ActualizarAlumno a v3
                    try
                    {
                        //string URI = "";
                        string url = "http://localhost:4348/Marketing/InsertarActualizarAlumno_a_v3?IdAlumno=" + alumnoBO.Id;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(url);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                else
                {
                    return BadRequest(alumnoBO.GetErrors(null));
                }

                return Ok(alumnoBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Historial Modificaciones Datos Personales Alumno
        /// </summary>
        /// <returns>Objeto(historial)<returns>
        [Route("[Action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerHistorialModificacionAlumno(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no Existe");
            }
            AlumnoLogRepositorio alumnoLogRepositorio = new AlumnoLogRepositorio(_integraDBContext);

            var historialAux = alumnoLogRepositorio.GetBy(x => x.IdAlumno == IdAlumno).OrderByDescending(x => x.FechaCreacion).ToList();

            var historial = historialAux.Select(y => new AlumnoLogDTO
            {
                Id = y.Id,
                CampoActualizado = y.CampoActualizado,
                ValorAnterior = y.ValorAnterior,
                ValorNuevo = y.ValorNuevo,
                FechaCreacion = y.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                UsuarioCreacion = y.UsuarioCreacion
            }).ToList();

            return Ok(historial);
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Historiales de Modificaciones de Docente
        /// </summary>
        /// <returns> Información histórica de modificaciones de docente </returns>
        /// <returns> objetoDTO : ExpositorLogDTO </returns>
        [Route("[Action]/{IdExpositor}")]
        [HttpGet]
        public ActionResult ObtenerHistorialModificacionDocente(int IdExpositor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdExpositor <= 0)
            {
                return BadRequest("El Id del expositor no Existe");
            }
            var _repExpositorLog = new ExpositorLogRepositorio(_integraDBContext);

            var listaExpositorLog = _repExpositorLog.GetBy(x => x.IdExpositor == IdExpositor).OrderByDescending(x => x.FechaCreacion)
                .Select(y => new ExpositorLogDTO
                {
                    Id = y.Id,
                    CampoActualizado = y.CampoActualizado,
                    ValorAnterior = y.ValorAnterior,
                    ValorNuevo = y.ValorNuevo,
                    FechaCreacion = y.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                    UsuarioCreacion = y.UsuarioCreacion
                }).ToList();

            return Ok(listaExpositorLog);
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Historiales de Modificaciones de Proveedor
        /// </summary>
        /// <returns> Información histórica de modificaciones de proveedor </returns>
        /// <returns> objetoDTO : ProveedorLogDTO </returns>
        [Route("[Action]/{IdProveedor}")]
        [HttpGet]
        public ActionResult ObtenerHistorialModificacionProveedor(int IdProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdProveedor <= 0)
            {
                return BadRequest("El Id del expositor no Existe");
            }
            var _repProveedorLog = new ProveedorLogRepositorio(_integraDBContext);

            var listaExpositorLog = _repProveedorLog.GetBy(x => x.IdProveedor == IdProveedor).OrderByDescending(x => x.FechaCreacion)
                .Select(y => new ProveedorLogDTO
                {
                    Id = y.Id,
                    CampoActualizado = y.CampoActualizado,
                    ValorAnterior = y.ValorAnterior,
                    ValorNuevo = y.ValorNuevo,
                    FechaCreacion = y.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                    UsuarioCreacion = y.UsuarioCreacion
                }).ToList();

            return Ok(listaExpositorLog);
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Sentinel por Alumno
        /// </summary>
        /// <returns> Información de Sentinel por Alumno </returns>
        /// <returns> objetoDTO :  SentinelDatosContactoDTO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerDatoSentinelAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no Existe");
            }
            integraDBContext contexto = new integraDBContext();
            SentinelRepositorio _repSentinel = new SentinelRepositorio(contexto);
            SentinelSdtLincreItemRepositorio _repSentinelSdtLincreItem = new SentinelSdtLincreItemRepositorio(contexto);
            SentinelSdtRepSbsitemRepositorio _repSentinelSdtRepSbsitem = new SentinelSdtRepSbsitemRepositorio(contexto);
            SentinelDatosContactoDTO datosSentinel = new SentinelDatosContactoDTO();


            datosSentinel = _repSentinel.ObtenerDastosAlumnoSentinel(idAlumno);
            if (datosSentinel != null)
            {
                datosSentinel.lineaCredito = _repSentinelSdtLincreItem.ObtenerLineaDeCredito(datosSentinel.IdSentinel.Value);
                datosSentinel.lineaDeuda = _repSentinelSdtRepSbsitem.ObtenerLineaDeuda(datosSentinel.IdSentinel.Value);
            }

            return Ok(datosSentinel);
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Información de Sentinel por Alumno
        /// </summary>
        /// <returns> objeto Agrupado, confirmación y Sentinel Actualizado </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[Action]/{dni}/{idContacto}/{usuario}")]
        [HttpGet]
        public ActionResult ActualizarSentinelAlumno(string dni, int idContacto, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                SentinelRepositorio _repSentinel = new SentinelRepositorio(contexto);
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(contexto);
                SentinelSdtEstandarItemRepositorio _repSentinelSdtEstandarItem = new SentinelSdtEstandarItemRepositorio(contexto);
                SentinelSdtInfGenRepositorio _repSentinelSdtInfGen = new SentinelSdtInfGenRepositorio(contexto);
                SentinelSdtLincreItemRepositorio _repSentinelSdtLincreItem = new SentinelSdtLincreItemRepositorio(contexto);
                SentinelSdtPoshisItemRepositorio _repSentinelSdtPoshisItem = new SentinelSdtPoshisItemRepositorio(contexto);
                SentinelRepLegItemRepositorio _repSentinelRepLegItem = new SentinelRepLegItemRepositorio(contexto);
                SentinelSdtRepSbsitemRepositorio _repSentinelSdtRepSBSItem = new SentinelSdtRepSbsitemRepositorio(contexto);
                SentinelSdtResVenItemRepositorio _repSentinelSdtResVenItem = new SentinelSdtResVenItemRepositorio();

                var idSentinel = _repSentinel.ObtenerIdSentinelPorDni(dni);

                AlumnoBO Alumno = new AlumnoBO(idContacto);

                SentinelBO Sentinel = new SentinelBO();
                bool rpta = false;
                bool estado = true;
                if (idSentinel != null)
                {
                    Sentinel = _repSentinel.FirstBy(x => x.Dni == dni);
                    //_repSentinel.Delete(idSentinel.Id.Value, usuario);
                    var SentinelSdtEstandarItem = _repSentinelSdtEstandarItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtInfGen = _repSentinelSdtInfGen.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtLincreItem = _repSentinelSdtLincreItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtPoshisItem = _repSentinelSdtPoshisItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelRepLegItem = _repSentinelRepLegItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtRepSBSItem = _repSentinelSdtRepSBSItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtResVenItem = _repSentinelSdtResVenItem.GetBy(x => x.IdSentinel == idSentinel.Id);

                    if (SentinelSdtEstandarItem != null && SentinelSdtEstandarItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtEstandarItem)
                        {
                            _repSentinelSdtEstandarItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtInfGen != null && SentinelSdtInfGen.Count() > 0)
                    {
                        foreach (var item in SentinelSdtInfGen)
                        {
                            _repSentinelSdtInfGen.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtLincreItem != null && SentinelSdtLincreItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtLincreItem)
                        {
                            _repSentinelSdtLincreItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtPoshisItem != null && SentinelSdtPoshisItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtPoshisItem)
                        {
                            _repSentinelSdtPoshisItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelRepLegItem != null && SentinelRepLegItem.Count() > 0)
                    {
                        foreach (var item in SentinelRepLegItem)
                        {
                            _repSentinelRepLegItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtRepSBSItem != null && SentinelSdtRepSBSItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtRepSBSItem)
                        {
                            _repSentinelSdtRepSBSItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtResVenItem != null && SentinelSdtResVenItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtResVenItem)
                        {
                            _repSentinelSdtResVenItem.Delete(item.Id, usuario);
                        }
                    }

                    Sentinel.Dni = dni;
                    Sentinel.UsuarioModificacion = usuario;
                    Sentinel.FechaModificacion = DateTime.Now;
                    Sentinel.ActualizarSentinelAlumno(dni, usuario);
                    if (Sentinel.DatosGenerales.Dni == "")
                    {
                        estado = false;
                        //return BadRequest("El numero de DNI a consultar es invalido o no esta registrado en sentinel");
                    }
                    if (estado)
                    {
                        Alumno.Dni = dni;
                        Alumno.ValidarEstadoContactoWhatsAppTemporal(contexto);
                        if (Sentinel.DatosGenerales != null)
                        {
                            Alumno.FechaNacimiento = Sentinel.DatosGenerales.FechaNacimiento;
                        }
                        _repAlumno.Update(Alumno);

                        rpta = _repSentinel.Update(Sentinel);
                    }

                }
                else
                {
                    Sentinel.Dni = dni;
                    Sentinel.Estado = true;
                    Sentinel.UsuarioCreacion = usuario;
                    Sentinel.UsuarioModificacion = usuario;
                    Sentinel.FechaCreacion = DateTime.Now;
                    Sentinel.FechaModificacion = DateTime.Now;
                    Sentinel.ActualizarSentinelAlumno(dni, usuario);
                    if (Sentinel.DatosGenerales.Dni == "")
                    {
                        estado = false;
                        //return BadRequest("El numero de DNI a consultar es invalido o no esta registrado en sentinel");
                    }
                    if (estado)
                    {
                        Alumno.Dni = dni;
                        Alumno.ValidarEstadoContactoWhatsAppTemporal(contexto);
                        if (Sentinel.DatosGenerales != null)
                        {
                            Alumno.FechaNacimiento = Sentinel.DatosGenerales.FechaNacimiento;
                            Alumno.UsuarioModificacion = usuario;
                            Alumno.FechaModificacion = DateTime.Now;
                        }
                        _repAlumno.Update(Alumno);
                        rpta = _repSentinel.Insert(Sentinel);
                    }
                }
                return Ok(new { rpta, idSentinel = Sentinel.Id, estado });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Información de Sentinel por Alumno
        /// </summary>
        /// <returns> objeto Agrupado, confirmación y Sentinel Actualizado </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[Action]/{dni}/{idContacto}/{usuario}")]
        [HttpGet]
        public ActionResult ActualizarSentinelAlumnoReporteCoordinador(string dni, int idContacto, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                SentinelRepositorio _repSentinel = new SentinelRepositorio(contexto);
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(contexto);
                SentinelSdtEstandarItemRepositorio _repSentinelSdtEstandarItem = new SentinelSdtEstandarItemRepositorio(contexto);
                SentinelSdtInfGenRepositorio _repSentinelSdtInfGen = new SentinelSdtInfGenRepositorio(contexto);
                SentinelSdtLincreItemRepositorio _repSentinelSdtLincreItem = new SentinelSdtLincreItemRepositorio(contexto);
                SentinelSdtPoshisItemRepositorio _repSentinelSdtPoshisItem = new SentinelSdtPoshisItemRepositorio(contexto);
                SentinelRepLegItemRepositorio _repSentinelRepLegItem = new SentinelRepLegItemRepositorio(contexto);
                SentinelSdtRepSbsitemRepositorio _repSentinelSdtRepSBSItem = new SentinelSdtRepSbsitemRepositorio(contexto);
                SentinelSdtResVenItemRepositorio _repSentinelSdtResVenItem = new SentinelSdtResVenItemRepositorio();

                var idSentinel = _repSentinel.ObtenerIdSentinelPorDni(dni);

                AlumnoBO Alumno = new AlumnoBO(idContacto);

                SentinelBO Sentinel = new SentinelBO();
                bool rpta = false;
                bool estado = true;
                if (idSentinel != null)
                {
                    Sentinel = _repSentinel.FirstBy(x => x.Dni == dni);
                    //_repSentinel.Delete(idSentinel.Id.Value, usuario);
                    var SentinelSdtEstandarItem = _repSentinelSdtEstandarItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtInfGen = _repSentinelSdtInfGen.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtLincreItem = _repSentinelSdtLincreItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtPoshisItem = _repSentinelSdtPoshisItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelRepLegItem = _repSentinelRepLegItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtRepSBSItem = _repSentinelSdtRepSBSItem.GetBy(x => x.IdSentinel == idSentinel.Id);
                    var SentinelSdtResVenItem = _repSentinelSdtResVenItem.GetBy(x => x.IdSentinel == idSentinel.Id);

                    if (SentinelSdtEstandarItem != null && SentinelSdtEstandarItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtEstandarItem)
                        {
                            _repSentinelSdtEstandarItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtInfGen != null && SentinelSdtInfGen.Count() > 0)
                    {
                        foreach (var item in SentinelSdtInfGen)
                        {
                            _repSentinelSdtInfGen.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtLincreItem != null && SentinelSdtLincreItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtLincreItem)
                        {
                            _repSentinelSdtLincreItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtPoshisItem != null && SentinelSdtPoshisItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtPoshisItem)
                        {
                            _repSentinelSdtPoshisItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelRepLegItem != null && SentinelRepLegItem.Count() > 0)
                    {
                        foreach (var item in SentinelRepLegItem)
                        {
                            _repSentinelRepLegItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtRepSBSItem != null && SentinelSdtRepSBSItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtRepSBSItem)
                        {
                            _repSentinelSdtRepSBSItem.Delete(item.Id, usuario);
                        }
                    }
                    if (SentinelSdtResVenItem != null && SentinelSdtResVenItem.Count() > 0)
                    {
                        foreach (var item in SentinelSdtResVenItem)
                        {
                            _repSentinelSdtResVenItem.Delete(item.Id, usuario);
                        }
                    }

                    Sentinel.Dni = dni;
                    Sentinel.UsuarioModificacion = usuario;
                    Sentinel.FechaModificacion = DateTime.Now;
                    Sentinel.ActualizarSentinelAlumno(dni, usuario);
                    if (Sentinel.DatosGenerales.Dni == "")
                    {
                        estado = false;
                        //return BadRequest("El numero de DNI a consultar es invalido o no esta registrado en sentinel");
                    }
                    if (estado)
                    {
                        Alumno.Dni = dni;
                        Alumno.ValidarEstadoContactoWhatsAppTemporal(contexto);
                        if (Sentinel.DatosGenerales != null)
                        {
                            Alumno.FechaNacimiento = Sentinel.DatosGenerales.FechaNacimiento;
                        }
                        _repAlumno.Update(Alumno);

                        rpta = _repSentinel.Update(Sentinel);
                    }

                }
                else
                {
                    Sentinel.Dni = dni;
                    Sentinel.Estado = true;
                    Sentinel.UsuarioCreacion = usuario;
                    Sentinel.UsuarioModificacion = usuario;
                    Sentinel.FechaCreacion = DateTime.Now;
                    Sentinel.FechaModificacion = DateTime.Now;
                    Sentinel.ActualizarSentinelAlumno(dni, usuario);
                    if (Sentinel.DatosGenerales.Dni == "")
                    {
                        estado = false;
                        //return BadRequest("El numero de DNI a consultar es invalido o no esta registrado en sentinel");
                    }
                    if (estado)
                    {
                        Alumno.Dni = dni;
                        Alumno.ValidarEstadoContactoWhatsAppTemporal(contexto);
                        if (Sentinel.DatosGenerales != null)
                        {
                            Alumno.FechaNacimiento = Sentinel.DatosGenerales.FechaNacimiento;
                            Alumno.UsuarioModificacion = usuario;
                            Alumno.FechaModificacion = DateTime.Now;
                        }
                        _repAlumno.Update(Alumno);
                        rpta = _repSentinel.Insert(Sentinel);
                    }
                }


                var resultado = _repSentinelSdtEstandarItem.GetBy(x => x.IdSentinel == Sentinel.Id && x.Estado == true).OrderByDescending(x => x.FechaCreacion).Select(x => new { SemaforoActual = x.SemanaActual }).FirstOrDefault();
                return Ok(new { rpta, Resultado = resultado, estado, IdSentinel = Sentinel.Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Arbol de Ocurrencias por Actividad Cabecera e IdOcurrencia Padre
        /// </summary>
        /// <returns> Arbol de Ocurrencias </returns>
        /// <returns> lista de objeto DTO : List<ArbolOcurenciaDTO> </returns>
        [Route("[action]/{IdActividadCabecera}/{IdOcurrenciaPadre?}")]
        [HttpGet]
        public ActionResult GetArbolOcurrencias(int IdActividadCabecera, int IdOcurrenciaPadre = 0)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OcurrenciaActividadBO ocurrenciaActividadBO = new OcurrenciaActividadBO
                {
                    IdActividadCabecera = IdActividadCabecera,
                    IdOcurrenciaActividadPadre = IdOcurrenciaPadre
                };
                
                if (!ocurrenciaActividadBO.HasErrors)
                {
                    OcurrenciaActividadRepositorio _ocurrenciaActividadRepositorio = new OcurrenciaActividadRepositorio(_integraDBContext);
                    List<ArbolOcurenciaDTO> listaArbolOcurrencia = _ocurrenciaActividadRepositorio.ObtenerArbolOcurrencia(ocurrenciaActividadBO);
                    return Ok(listaArbolOcurrencia);
                }
                else
                {
                    return BadRequest(ocurrenciaActividadBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Arbol de Ocurrencias por Actividad Cabecera e IdOcurrencia Padre
        /// </summary>
        /// <returns> Arbol de Ocurrencias </returns>
        /// <returns> lista de objeto DTO : List<ArbolOcurenciaDTO> </returns>
        [Route("[action]/{IdActividadCabecera}/{IdOcurrenciaPadre?}")]
        [HttpGet]
        public ActionResult GetArbolOcurrenciasAlterno(int IdActividadCabecera, int IdOcurrenciaPadre = 0)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OcurrenciaActividadAlternoBO ocurrenciaActividadBO = new OcurrenciaActividadAlternoBO
                {
                    IdActividadCabecera = IdActividadCabecera,
                    IdOcurrenciaActividadPadre = IdOcurrenciaPadre
                };

                if (!ocurrenciaActividadBO.HasErrors)
                {
                    OcurrenciaActividadAlternoRepositorio _ocurrenciaActividadRepositorio = new OcurrenciaActividadAlternoRepositorio(_integraDBContext);
                    List<ArbolOcurenciaDTO> listaArbolOcurrencia = _ocurrenciaActividadRepositorio.ObtenerArbolOcurrencia(ocurrenciaActividadBO);
                    return Ok(listaArbolOcurrencia);
                }
                else
                {
                    return BadRequest(ocurrenciaActividadBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Arbol de Ocurrencias por Actividad Cabecera e IdOcurrencia Padre
        /// </summary>
        /// <returns> Arbol de Ocurrencias </returns>
        /// <returns> lista de objeto DTO : List<ArbolOcurenciaDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GetArbolOcurrenciasAlterno([FromBody] ObtenerCategoriaFiltroV2DTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OcurrenciaActividadAlternoBO ocurrenciaActividadBO = new OcurrenciaActividadAlternoBO
                {
                    IdActividadCabecera = Objeto.IdActividadCabecera,
                    IdOcurrenciaActividadPadre = Objeto.IdOcurrenciaPadre
                };

                if (!ocurrenciaActividadBO.HasErrors)
                {
                    OcurrenciaActividadAlternoRepositorio _ocurrenciaActividadRepositorio = new OcurrenciaActividadAlternoRepositorio(_integraDBContext);
                    List<ArbolOcurenciaDTO> listaArbolOcurrencia = _ocurrenciaActividadRepositorio.ObtenerArbolOcurrencia(ocurrenciaActividadBO);
                    return Ok(listaArbolOcurrencia);
                }
                else
                {
                    return BadRequest(ocurrenciaActividadBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: , Carlos C.
        /// Fecha: 15/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener todas la plantillas tanto de whatsapp y correos
        /// </summary>
        /// <returns> Plantillas </returns>
        /// <returns> lista de objeto DTO : List<ArbolOcurenciaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPlantillas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();

                var plantillasWP = _repPlantillaClaveValor.ObtenerPlantillasWP();
                var plantillasCorreo = _repPlantillaClaveValor.ObtenerPlantillasCorreo();


                return Ok(new { plantillasWP, plantillasCorreo });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Hoja de Actividades por Ocurrencia
        /// </summary>
        /// <returns> Hoja de Actividades </returns>
        /// <returns> lista de objeto DTO : List<HojaActividadesDTO> </returns>
        [Route("[action]/{IdOcurrencia}")]
        [HttpGet]
        public IActionResult GetHojaActividadesByOcurrencia(int IdOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                OcurrenciaRepositorio _dapOcurrencia = new OcurrenciaRepositorio();
                var _HojaActividades = _dapOcurrencia.HojaGetActividadesByOcurrencia(IdOcurrencia);


                return Ok(_HojaActividades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// TipoFuncion: GET
        /// Autor: , Carlos C.
        /// Fecha: 03/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Hoja de Actividades por Ocurrencia Alterno
        /// </summary>
        /// <returns> Hoja de Actividades </returns>
        /// <returns> lista de objeto DTO : List<HojaActividadesDTO> </returns>
        [Route("[action]/{IdOcurrencia}")]
        [HttpGet]
        public IActionResult GetHojaActividadesByOcurrenciaAlterno(int IdOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                OcurrenciaRepositorio _dapOcurrencia = new OcurrenciaRepositorio();
                var _HojaActividades = _dapOcurrencia.HojaGetActividadesByOcurrenciaAlterno(IdOcurrencia);


                return Ok(_HojaActividades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <returns> Confirmación </returns>
        /// <returns> Bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirDocumentosProyecto(ProyectoAplicacionEntregaVersionPwDTO ProyectoAplicacion, [FromForm] IList<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string NombreArchivotemp = "";
                string ContentType = "";
                var urlArchivoRepositorio = "";

                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        ContentType = file.ContentType;
                        NombreArchivotemp = file.FileName;
                        NombreArchivotemp = string.Concat(NombreArchivotemp);
                        urlArchivoRepositorio = _repMaterialVersion.SubirDocumentosProyectoAplicacionRepositorio(file.ConvertToByte(), file.ContentType, NombreArchivotemp);
                    }
                }


                var ProyectoAplicacionEntregaVersionPw = new ProyectoAplicacionEntregaVersionPwBO
                {

                    NombreArchivo = NombreArchivotemp,
                    RutaArchivo = urlArchivoRepositorio,
                    Version = ProyectoAplicacion.Version,
                    FechaEnvio = ProyectoAplicacion.FechaEnvio,
                    IdMatriculaCabecera = ProyectoAplicacion.IdMatriculaCabecera,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = ProyectoAplicacion.Usuario,
                    UsuarioModificacion = ProyectoAplicacion.Usuario,
                    Estado = true
                };

                var result = _repProyectoAplicacionEntregaVersionPw.Insert(ProyectoAplicacionEntregaVersionPw);

                return Ok(urlArchivoRepositorio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <returns> Url direccion </returns>
        /// <returns> String </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirDocumentosOportunidad(DocumentosOportunidadDTO MaterialPEspecificoDetalle, [FromForm] IList<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string NombreArchivotemp = "";
                string ContentType = "";
                var urlArchivoRepositorio = "";

                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        ContentType = file.ContentType;
                        NombreArchivotemp = file.FileName;
                        NombreArchivotemp = string.Concat(NombreArchivotemp);
                        urlArchivoRepositorio = _repMaterialVersion.SubirDocumentosOportunidadRepositorio(file.ConvertToByte(), file.ContentType, NombreArchivotemp);
                    }
                }


                var DocumentoOportunidad = new DocumentoOportunidadBO
                {
                    IdAlumno = MaterialPEspecificoDetalle.IdAlumno,
                    IdOportunidad = MaterialPEspecificoDetalle.IdOportunidad,
                    IdClasificacionPersona = MaterialPEspecificoDetalle.IdClasificacionPersona,
                    NombreArchivo = NombreArchivotemp,
                    Ruta = urlArchivoRepositorio,
                    Comentario = MaterialPEspecificoDetalle.ComentarioSubida,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = MaterialPEspecificoDetalle.NombreUsuario,
                    UsuarioModificacion = MaterialPEspecificoDetalle.NombreUsuario,
                    Estado = true
                };

                var result = _repDocumentoOportunidad.Insert(DocumentoOportunidad);
                //if (result)
                //{
                //    List<string> correos = new List<string>();
                //    correos.Add("atrelles@bsginstitute.com");
                //    correos.Add("bamontoya@bsginstitute.com ");
                //    correos.Add("ccrispin@bsginstitute.com ");

                //    string mensaje = comprobantePago.MensajeEmailComprobantePago();


                //    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                //    TMKMailDataDTO mailData = new TMKMailDataDTO
                //    {
                //        Sender = "jcayo@bsginstitute.com",
                //        Recipient = string.Join(",", correos),
                //        Subject = "BSG INSTITUTE - Convenios Subido",
                //        Message = mensaje,
                //        Cc = "",
                //        Bcc = "",
                //        AttachedFiles = null,
                //        RemitenteC = comprobantePago.Nombres
                //    };

                //    Mailservice.SetData(mailData);

                //    List<TMKMensajeIdDTO> MensajeIdDTO = Mailservice.SendMessageTask();

                //    //Correos.envio_email("jcayo@bsginstitute.com", comprobantePago.nombresContacto + " " + comprobantePago.apellidos, "BSG INSTITUTE - Datos Alumno en IS ", mensaje, correos);
                //}

                return Ok(urlArchivoRepositorio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las etiquetas de V2 caso contrario devuelve V1 mediante el idCentroCosto
        /// </summary>
        /// <returns> Etiquetas </returns>
        /// <returns> Lista de Objeto DTO : List<TemplateV2ReemplazoEtiquetaDTO> </returns>
        [Route("[action]/{idCentroCosto}")]
        [HttpGet]
        public ActionResult ProbarTemplatesV2Agenda(int idCentroCosto)
        {
            PlantillaPwBO Objeto = new PlantillaPwBO(_integraDBContext);

            return Ok(Objeto.ObtenerTemplatesV2ReemplazoEtiqueta(idCentroCosto));
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza el envio de Correo de Condiciones y Caracteríticas por Oportunidad
        /// </summary>
        /// <returns> Vacio </returns>
        [Route("[action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult RegularizarEnvioCorreoCondicionesCarateristicas(int idOportunidad)
        {
            var _repOportunidad = new OportunidadRepositorio(_integraDBContext);
            var _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);


            var Oportunidad = _repOportunidad.FirstById(idOportunidad);
            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
            {
                //if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad) && ActividadAntigua.IdOcurrencia == ValorEstatico.IdOcurrenciaConfirmaPagoIs)
                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                {
                    try
                    {
                        //Enviar plantilla de condiciones
                        //1246    Nuevas Condiciones y Características -Perú
                        //1247    Nuevas Condiciones y Características -Colombia
                        //1401    Condiciones y Características - Mexico
                        //1402    Contrato de uso de datos biométricos por voz  - México

                        var _repAlumno = new AlumnoRepositorio(_integraDBContext);

                        var alumno = _repAlumno.FirstById(Oportunidad.IdAlumno);
                        var _idPlantilla = 0;
                        var _idPlantilla2 = 0;

                        if (alumno.IdCodigoPais == 57)//Colombia
                        {
                            _idPlantilla = 1247;
                        }
                        else if (alumno.IdCodigoPais == 51)//Peru
                        {
                            _idPlantilla = 1246;
                        }
                        else if (alumno.IdCodigoPais == 52)//Mexico
                        {
                            _idPlantilla = 1401;
                            _idPlantilla2 = 1402;
                        }
                        if (_idPlantilla != 0)
                        {
                            var Objeto = new PlantillaPwBO(_integraDBContext)
                            {
                                IdOportunidad = Oportunidad.Id,
                                IdPlantilla = _idPlantilla
                            };
                            var _repPgeneralTipoDescuento = new PgeneralTipoDescuentoRepositorio(_integraDBContext);
                            Objeto.GetValorEtiqueta(Oportunidad.IdCentroCosto.Value, Oportunidad.IdFaseOportunidad, Oportunidad.Id);

                            var DatosOportunidad = Objeto.ObtenerDatosOportunidad(Oportunidad.Id);
                            string FechaInicioPrograma = "";

                            var Promocion = _repPgeneralTipoDescuento.FirstBy(w => w.Estado == true && w.IdPgeneral == DatosOportunidad.IdPgeneral.Value && w.IdTipoDescuento == 143, y => new { y.FlagPromocion });
                            if (Promocion != null)
                            {
                                DatosOportunidad.Promocion25 = Promocion.FlagPromocion;
                            }
                            FechaInicioPrograma = Objeto.ObtenerFechaInicioPrograma(DatosOportunidad.IdPgeneral.Value, DatosOportunidad.IdCentroCosto.Value);
                            //var etiquetaDuracionYHorarios = Objeto.DuracionYHorarios(DatosOportunidad.IdCentroCosto);
                            DatosOportunidad.CostoTotalConDescuento = Objeto.ObtenerCostoTotalConDescuento(Oportunidad.Id);
                            Objeto.ObtenerDatosProgramaGeneral(DatosOportunidad.IdPgeneral.Value);
                            //reemplazo 1
                            Objeto.ReemplazarEtiquetas();

                            var emailCalculado = Objeto.EmailReemplazado;
                            List<string> correosPersonalizadosCopia = new List<string>
                            {
                                "grabaciones@bsginstitute.com",
                                "mzegarraj@bsginstitute.com"
                            };

                            List<string> correosPersonalizados = new List<string>
                            {
                                Objeto.DatosOportunidadAlumno.OportunidadAlumno.Email1
                            };

                            var mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = "matriculas@bsginstitute.com",
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = emailCalculado.Asunto,
                                Message = emailCalculado.CuerpoHTML,
                                Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                                Bcc = "ccrispin@bsginstitute.com",
                                AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                            };
                            var mailServie = new TMK_MailServiceImpl();
                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();
                            var _repDocumentoEnviadoWebPw = new DocumentoEnviadoWebPwRepositorio(_integraDBContext);
                            var documentoEnviadoWebPwBO = new DocumentoEnviadoWebPwBO()
                            {
                                IdAlumno = Oportunidad.IdAlumno,
                                Nombre = "BSG Institute - Condiciones y Características",
                                IdPespecifico = Objeto.DatosOportunidadAlumno.IdPEspecifico,
                                FechaEnvio = DateTime.Now,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "regularizacion",
                                UsuarioModificacion = "regularizacion",
                                Estado = true
                            };
                            if (!documentoEnviadoWebPwBO.HasErrors)
                            {
                                _repDocumentoEnviadoWebPw.Insert(documentoEnviadoWebPwBO);
                            }

                            if (_idPlantilla2 != 0)
                            {
                                //reemplazo 2
                                Objeto.IdPlantilla = _idPlantilla2;
                                Objeto.ReemplazarEtiquetas();
                                var emailCalculado2 = Objeto.EmailReemplazado;

                                var mailDataPersonalizado2 = new TMKMailDataDTO
                                {
                                    Sender = "matriculas@bsginstitute.com",
                                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                    Subject = emailCalculado2.Asunto,
                                    Message = emailCalculado2.CuerpoHTML,
                                    Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                                    Bcc = "ccrispin@bsginstitute.com",
                                    AttachedFiles = emailCalculado2.ListaArchivosAdjuntos
                                };
                                var mailServie2 = new TMK_MailServiceImpl();
                                mailServie.SetData(mailDataPersonalizado2);
                                mailServie.SendMessageTask();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            return Ok();
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza la Programación de Actividades
        /// </summary>
        /// <returns> Información de Programación de Actividad </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarYProgramarActividad([FromBody] ParametroFinalizarActividadDTO dto)
        {
            string parametros = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                parametros = JsonConvert.SerializeObject(dto);

                // Desactivado de redireccion
                var _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                if (dto.ActividadAntigua.IdOportunidad.HasValue)
                {
                    try
                    {
                        _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(dto.ActividadAntigua.IdOportunidad.Value);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                var _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);

                if (dto.ActividadAntigua.IdOportunidad.HasValue)
                    if (!_repOportunidad.Exist(x => x.Id == dto.ActividadAntigua.IdOportunidad && x.IdFaseOportunidad == dto.datosOportunidad.IdFaseOportunidad))
                        return BadRequest(new { Codigo = 409, Mensaje = $"La oportunidad fue trabajada por otra persona: IdOportunidad {dto.ActividadAntigua.IdOportunidad}" });

                var _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                var _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                var _repHoraBloqueada = new HoraBloqueadaRepositorio(_integraDBContext);
                var _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                var _repLlamadaActividad = new LlamadaActividadRepositorio(_integraDBContext);
                var _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                var _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
                var _repReprogramacionCabecera = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                var _repReprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalRepositorio(_integraDBContext);
                var _repComprobantePagoOportunidad = new ComprobantePagoOportunidadRepositorio(_integraDBContext);
                var _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);
                var oportunidadPrerequisitoGeneralRepositorio = new OportunidadPrerequisitoGeneralRepositorio(_integraDBContext);
                var oportunidadPrerequisitoEspecificoRepositorio = new OportunidadPrerequisitoEspecificoRepositorio(_integraDBContext);
                var oportunidadBeneficioRepositorio = new OportunidadBeneficioRepositorio(_integraDBContext);
                var detalleOportunidadCompetidorRepositorio = new DetalleOportunidadCompetidorRepositorio(_integraDBContext);

                int idReprogramacionCabecera = 0;
                HoraBloqueadaBO horaBloqueada = new HoraBloqueadaBO
                {
                    IdPersonal = dto.filtro.IdPersonal,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = dto.filtro.Usuario,
                    UsuarioModificacion = dto.filtro.Usuario,
                    Estado = true
                };

                if (dto.datosOportunidad.UltimaFechaProgramada != null)
                {
                    horaBloqueada.Fecha = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                    horaBloqueada.Hora = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                }

                if (horaBloqueada.HasErrors)
                {
                    return BadRequest(horaBloqueada.GetErrors(null));
                }

                var Oportunidad = new OportunidadBO(dto.ActividadAntigua.IdOportunidad.Value, dto.filtro.Usuario, _integraDBContext)
                {
                    IdFaseOportunidadIp = dto.datosOportunidad.IdFaseOportunidadIp,
                    IdFaseOportunidadIc = dto.datosOportunidad.IdFaseOportunidadIc,
                    FechaEnvioFaseOportunidadPf = dto.datosOportunidad.FechaEnvioFaseOportunidadPf,
                    FechaPagoFaseOportunidadPf = dto.datosOportunidad.FechaPagoFaseOportunidadPf,
                    FechaPagoFaseOportunidadIc = dto.datosOportunidad.FechaPagoFaseOportunidadIc,
                    IdFaseOportunidadPf = dto.datosOportunidad.IdFaseOportunidadPf,
                    CodigoPagoIc = dto.datosOportunidad.CodigoPagoIc,
                    ValidacionCorrecta = true
                };

                var ActividadAntigua = new ActividadDetalleBO
                {
                    Id = dto.ActividadAntigua.Id,
                    IdActividadCabecera = dto.ActividadAntigua.IdActividadCabecera,
                    FechaProgramada = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada),
                    FechaReal = dto.ActividadAntigua.FechaReal,
                    DuracionReal = dto.ActividadAntigua.DuracionReal,
                    IdOcurrencia = dto.ActividadAntigua.IdOcurrencia.Value,
                    IdEstadoActividadDetalle = dto.ActividadAntigua.IdEstadoActividadDetalle,
                    Comentario = dto.ActividadAntigua.Comentario,
                    IdAlumno = dto.ActividadAntigua.IdAlumno,
                    Actor = dto.ActividadAntigua.Actor,
                    IdOportunidad = dto.ActividadAntigua.IdOportunidad.Value,
                    IdCentralLlamada = dto.ActividadAntigua.IdCentralLlamada,
                    RefLlamada = dto.ActividadAntigua.RefLlamada,
                    IdOcurrenciaActividad = dto.ActividadAntigua.IdOcurrenciaActividad,
                    IdClasificacionPersona = Oportunidad.IdClasificacionPersona
                };

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(dto.ActividadAntigua.Id, _integraDBContext);

                OportunidadCompetidorBO OportunidadCompetidor;
                if (dto.DatosCompuesto.OportunidadCompetidor.Id == 0)
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO
                    {
                        Id = 0,
                        IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.IdOportunidad,
                        OtroBeneficio = dto.DatosCompuesto.OportunidadCompetidor.OtroBeneficio,
                        Respuesta = dto.DatosCompuesto.OportunidadCompetidor.Respuesta,
                        Completado = dto.DatosCompuesto.OportunidadCompetidor.Completado,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = dto.Usuario,
                        UsuarioModificacion = dto.Usuario,
                        Estado = true
                    };
                }
                else
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO(dto.DatosCompuesto.OportunidadCompetidor.Id, _integraDBContext)
                    {
                        IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.IdOportunidad,
                        OtroBeneficio = dto.DatosCompuesto.OportunidadCompetidor.OtroBeneficio,
                        Respuesta = dto.DatosCompuesto.OportunidadCompetidor.Respuesta,
                        Completado = dto.DatosCompuesto.OportunidadCompetidor.Completado
                    };
                }
                if (!OportunidadCompetidor.HasErrors)
                {
                    Oportunidad.OportunidadCompetidor = OportunidadCompetidor;
                }
                else
                {
                    return BadRequest(OportunidadCompetidor.GetErrors(null));
                }

                var CalidadBO = new CalidadProcesamientoBO
                {
                    IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.IdOportunidad,
                    PerfilCamposLlenos = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposLlenos,
                    PerfilCamposTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposTotal,
                    Dni = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.Dni,
                    PgeneralValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralValidados,
                    PgeneralTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralTotal,
                    PespecificoValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoValidados,
                    PespecificoTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoTotal,
                    BeneficiosValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosValidados,
                    BeneficiosTotales = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosTotales,
                    CompetidoresVerificacion = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.CompetidoresVerificacion,
                    ProblemaSeleccionados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSeleccionados,
                    ProblemaSolucionados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSolucionados,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    Estado = true
                };
                if (!CalidadBO.HasErrors)
                {
                    Oportunidad.CalidadProcesamiento = CalidadBO;
                }
                else
                {
                    return BadRequest(CalidadBO.GetErrors(null));
                }

                OportunidadCompetidor.ListaPrerequisitoGeneral = new List<OportunidadPrerequisitoGeneralBO>();
                OportunidadPrerequisitoGeneralBO ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO();
                var listaPreRequisitoGeneralAgrupado = dto.DatosCompuesto.ListaPrerequisitoGeneral.GroupBy(x => x.IdProgramaGeneralBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaPreRequisitoGeneralAgrupado)
                {
                    ListaPrerequisitoGeneral = oportunidadPrerequisitoGeneralRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio);

                    if (ListaPrerequisitoGeneral != null)
                    {
                        var listaPreRequisitoGeneralEliminar = oportunidadPrerequisitoGeneralRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio && x.Id != ListaPrerequisitoGeneral.Id).ToList();
                        foreach (var itemEliminar in listaPreRequisitoGeneralEliminar)
                        {
                            oportunidadPrerequisitoGeneralRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }


                    if (ListaPrerequisitoGeneral == null)
                    {
                        ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaPrerequisitoGeneral.Respuesta = item.Respuesta;
                    ListaPrerequisitoGeneral.Completado = item.Completado;
                    ListaPrerequisitoGeneral.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoGeneral.UsuarioModificacion = dto.Usuario;
                    if (!ListaPrerequisitoGeneral.HasErrors)
                    {
                        OportunidadCompetidor.ListaPrerequisitoGeneral.Add(ListaPrerequisitoGeneral);
                    }
                    else
                    {
                        return BadRequest(ListaPrerequisitoGeneral.GetErrors(null));
                    }
                }
                OportunidadCompetidor.ListaPrerequisitoEspecifico = new List<OportunidadPrerequisitoEspecificoBO>();
                OportunidadPrerequisitoEspecificoBO ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO();


                var listaPreRequisitoEspecificoAgrupado = dto.DatosCompuesto.ListaPrerequisitoEspecifico.GroupBy(x => x.IdProgramaGeneralBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaPreRequisitoEspecificoAgrupado)
                {
                    ListaPrerequisitoEspecifico = oportunidadPrerequisitoEspecificoRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio);
                    if (ListaPrerequisitoEspecifico != null)
                    {
                        var listaPreRequisitoEspecificoEliminar = oportunidadPrerequisitoEspecificoRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio && x.Id != ListaPrerequisitoEspecifico.Id).ToList();
                        foreach (var itemEliminar in listaPreRequisitoEspecificoEliminar)
                        {
                            oportunidadPrerequisitoEspecificoRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }

                    if (ListaPrerequisitoEspecifico == null)
                    {
                        ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaPrerequisitoEspecifico.Respuesta = item.Respuesta;
                    ListaPrerequisitoEspecifico.Completado = item.Completado;
                    ListaPrerequisitoEspecifico.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.UsuarioModificacion = dto.Usuario;
                    if (!ListaPrerequisitoEspecifico.HasErrors)
                    {
                        OportunidadCompetidor.ListaPrerequisitoEspecifico.Add(ListaPrerequisitoEspecifico);
                    }
                    else
                    {
                        return BadRequest(ListaPrerequisitoEspecifico.GetErrors(null));
                    }
                }

                OportunidadCompetidor.ListaBeneficio = new List<OportunidadBeneficioBO>();
                OportunidadBeneficioBO ListaBeneficio = new OportunidadBeneficioBO();
                var listaBeneficioAgrupado = dto.DatosCompuesto.ListaBeneficio.GroupBy(x => x.IdBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaBeneficioAgrupado)
                {
                    ListaBeneficio = oportunidadBeneficioRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdBeneficio == item.IdBeneficio);

                    if (ListaBeneficio != null)
                    {
                        var listaBeneficioEliminar = oportunidadBeneficioRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdBeneficio == item.IdBeneficio && x.Id != ListaBeneficio.Id).ToList();
                        foreach (var itemEliminar in listaBeneficioEliminar)
                        {
                            oportunidadBeneficioRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }

                    if (ListaBeneficio == null)
                    {
                        ListaBeneficio = new OportunidadBeneficioBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdBeneficio = item.IdBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaBeneficio.Respuesta = item.Respuesta;
                    ListaBeneficio.Completado = item.Completado;
                    ListaBeneficio.FechaModificacion = DateTime.Now;
                    ListaBeneficio.UsuarioModificacion = dto.Usuario;
                    if (!ListaBeneficio.HasErrors)
                    {
                        OportunidadCompetidor.ListaBeneficio.Add(ListaBeneficio);
                    }
                    else
                    {
                        return BadRequest(ListaBeneficio.GetErrors(null));
                    }
                }
                detalleOportunidadCompetidorRepositorio.DeleteLogicoPorOportunidadCompetidor(dto.DatosCompuesto.OportunidadCompetidor.Id, dto.Usuario, dto.DatosCompuesto.ListaCompetidor);
                OportunidadCompetidor.ListaCompetidor = new List<DetalleOportunidadCompetidorBO>();
                DetalleOportunidadCompetidorBO ListaCompetidor = new DetalleOportunidadCompetidorBO();
                foreach (var item in dto.DatosCompuesto.ListaCompetidor)
                {
                    ListaCompetidor = detalleOportunidadCompetidorRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdCompetidor == item);
                    if (ListaCompetidor == null)
                    {
                        ListaCompetidor = new DetalleOportunidadCompetidorBO
                        {
                            IdOportunidadCompetidor = 0,
                            IdCompetidor = item,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario
                        };
                    }
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioModificacion = dto.Usuario;
                    if (!ListaCompetidor.HasErrors)
                    {
                        OportunidadCompetidor.ListaCompetidor.Add(ListaCompetidor);
                    }
                    else
                    {
                        return BadRequest(ListaCompetidor.GetErrors(null));
                    }
                }
                Oportunidad.ListaSoluciones = new List<SolucionClienteByActividadBO>();
                foreach (var item in dto.DatosCompuesto.ListaSoluciones)
                {
                    var ListaSoluciones = new SolucionClienteByActividadBO
                    {
                        IdOportunidad = item.IdOportunidad,
                        IdActividadDetalle = item.IdActividadDetalle,
                        IdCausa = item.IdCausa,
                        IdPersonal = item.IdPersonal,
                        Solucionado = item.Solucionado,
                        IdProblemaCliente = item.IdProblemaCliente,
                        OtroProblema = item.OtroProblema,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = dto.Usuario,
                        UsuarioModificacion = dto.Usuario,
                        Estado = true
                    };

                    if (!ListaSoluciones.HasErrors)
                    {
                        Oportunidad.ListaSoluciones.Add(ListaSoluciones);
                    }
                    else
                    {
                        return BadRequest(ListaSoluciones.GetErrors(null));
                    }
                }

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    ActividadNueva.LlamadaActividad = null;
                    Oportunidad.FinalizarActividad(false, dto.datosOportunidad);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                            {
                                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 0);
                                }
                                else
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 1);

                                }
                            }

                            _repHoraBloqueada.Insert(horaBloqueada);

                            Oportunidad.ProgramaActividad();

                            _repActividadDetalle.Insert(Oportunidad.ActividadNuevaProgramarActividad);
                            Oportunidad.IdActividadDetalleUltima = Oportunidad.ActividadNuevaProgramarActividad.Id;
                            Oportunidad.ActividadNuevaProgramarActividad = null;
                            _repOportunidad.Update(Oportunidad);

                            //_repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);

                            if (dto.filtro.Tipo == "manual")
                            {
                                if (dto.datosOportunidad.IdFaseOportunidad != Oportunidad.IdFaseOportunidad)
                                {
                                    if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                                    {
                                        //if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad) && ActividadAntigua.IdOcurrencia == ValorEstatico.IdOcurrenciaConfirmaPagoIs)
                                        if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                        {
                                            try
                                            {
                                                //Enviar plantilla de condiciones
                                                //1246    Nuevas Condiciones y Características -Perú
                                                //1247    Nuevas Condiciones y Características -Colombia
                                                //1401    Condiciones y Características - Mexico
                                                //1402    Contrato de uso de datos biométricos por voz  - México

                                                var _repAlumno = new AlumnoRepositorio(_integraDBContext);

                                                var alumno = _repAlumno.FirstById(Oportunidad.IdAlumno);
                                                var _idPlantilla = 0;
                                                var _idPlantilla2 = 0;

                                                if (alumno.IdCodigoPais == 57)//Colombia
                                                {
                                                    _idPlantilla = 1247;
                                                }
                                                else if (alumno.IdCodigoPais == 51)//Peru
                                                {
                                                    _idPlantilla = 1246;
                                                }
                                                else if (alumno.IdCodigoPais == 52)//Mexico
                                                {
                                                    _idPlantilla = 1401;
                                                    _idPlantilla2 = 1402;
                                                }

                                                if (_idPlantilla != 0)
                                                {
                                                    var Objeto = new PlantillaPwBO(_integraDBContext)
                                                    {
                                                        IdOportunidad = Oportunidad.Id,
                                                        IdPlantilla = _idPlantilla
                                                    };
                                                    var _repPgeneralTipoDescuento = new PgeneralTipoDescuentoRepositorio(_integraDBContext);
                                                    Objeto.GetValorEtiqueta(Oportunidad.IdCentroCosto.Value, Oportunidad.IdFaseOportunidad, Oportunidad.Id);

                                                    var DatosOportunidad = Objeto.ObtenerDatosOportunidad(Oportunidad.Id);
                                                    string FechaInicioPrograma = "";

                                                    var Promocion = _repPgeneralTipoDescuento.FirstBy(w => w.Estado == true && w.IdPgeneral == DatosOportunidad.IdPgeneral.Value && w.IdTipoDescuento == 143, y => new { y.FlagPromocion });
                                                    if (Promocion != null)
                                                    {
                                                        DatosOportunidad.Promocion25 = Promocion.FlagPromocion;
                                                    }
                                                    FechaInicioPrograma = Objeto.ObtenerFechaInicioPrograma(DatosOportunidad.IdPgeneral.Value, DatosOportunidad.IdCentroCosto.Value);
                                                    //var etiquetaDuracionYHorarios = Objeto.DuracionYHorarios(DatosOportunidad.IdCentroCosto);
                                                    DatosOportunidad.CostoTotalConDescuento = Objeto.ObtenerCostoTotalConDescuento(Oportunidad.Id);
                                                    Objeto.ObtenerDatosProgramaGeneral(DatosOportunidad.IdPgeneral.Value);
                                                    //reemplazo
                                                    Objeto.ReemplazarEtiquetas();

                                                    var emailCalculado = Objeto.EmailReemplazado;
                                                    List<string> correosPersonalizadosCopia = new List<string>
                                                    {
                                                        "grabaciones@bsginstitute.com",
                                                         "mzegarraj@bsginstitute.com"
                                                    };

                                                    List<string> correosPersonalizados = new List<string>
                                                    {
                                                        Objeto.DatosOportunidadAlumno.OportunidadAlumno.Email1
                                                    };

                                                    var mailDataPersonalizado = new TMKMailDataDTO
                                                    {
                                                        Sender = "matriculas@bsginstitute.com",
                                                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                                        Subject = emailCalculado.Asunto,
                                                        Message = emailCalculado.CuerpoHTML,
                                                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                                                        Bcc = "",
                                                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                                                    };
                                                    var mailServie = new TMK_MailServiceImpl();
                                                    mailServie.SetData(mailDataPersonalizado);
                                                    mailServie.SendMessageTask();
                                                    var _repDocumentoEnviadoWebPw = new DocumentoEnviadoWebPwRepositorio(_integraDBContext);
                                                    var documentoEnviadoWebPwBO = new DocumentoEnviadoWebPwBO()
                                                    {
                                                        IdAlumno = Oportunidad.IdAlumno,
                                                        Nombre = "BSG Institute - Condiciones y Características",
                                                        IdPespecifico = Objeto.DatosOportunidadAlumno.IdPEspecifico,
                                                        FechaEnvio = DateTime.Now,
                                                        FechaCreacion = DateTime.Now,
                                                        FechaModificacion = DateTime.Now,
                                                        UsuarioCreacion = dto.Usuario,
                                                        UsuarioModificacion = dto.Usuario,
                                                        Estado = true
                                                    };
                                                    if (!documentoEnviadoWebPwBO.HasErrors)
                                                    {
                                                        _repDocumentoEnviadoWebPw.Insert(documentoEnviadoWebPwBO);
                                                    }


                                                    if (_idPlantilla2 != 0)
                                                    {
                                                        //reemplazo 2
                                                        Objeto.IdPlantilla = _idPlantilla2;
                                                        Objeto.ReemplazarEtiquetas();
                                                        var emailCalculado2 = Objeto.EmailReemplazado;

                                                        var mailDataPersonalizado2 = new TMKMailDataDTO
                                                        {
                                                            Sender = "matriculas@bsginstitute.com",
                                                            Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                                            Subject = emailCalculado2.Asunto,
                                                            Message = emailCalculado2.CuerpoHTML,
                                                            Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                                                            Bcc = "ccrispin@bsginstitute.com",
                                                            AttachedFiles = emailCalculado2.ListaArchivosAdjuntos
                                                        };
                                                        var mailServie2 = new TMK_MailServiceImpl();
                                                        mailServie2.SetData(mailDataPersonalizado2);
                                                        mailServie2.SendMessageTask();

                                                    }


                                                }

                                            }
                                            catch (Exception e)
                                            {
                                            }


                                            if (ActividadAntigua.IdOcurrencia == ValorEstatico.IdOcurrenciaConfirmaPagoIs || ActividadAntigua.IdOcurrencia == ValorEstatico.IdOcurrenciaIsSinLlamada)
                                            {
                                                var comprobantePago = new ComprobantePagoOportunidadBO
                                                {
                                                    IdContacto = dto.ComprobantePago.IdContacto,
                                                    Nombres = dto.ComprobantePago.Nombres,
                                                    Apellidos = dto.ComprobantePago.Apellidos,
                                                    Celular = dto.ComprobantePago.Celular,
                                                    Dni = dto.ComprobantePago.Dni == null ? "" : dto.ComprobantePago.Dni,
                                                    Correo = dto.ComprobantePago.Correo,
                                                    NombrePais = dto.ComprobantePago.NombrePais,
                                                    IdPais = dto.ComprobantePago.IdPais,
                                                    NombreCiudad = dto.ComprobantePago.NombreCiudad == null ? "" : dto.ComprobantePago.NombreCiudad,
                                                    TipoComprobante = dto.ComprobantePago.TipoComprobante,
                                                    NroDocumento = dto.ComprobantePago.NroDocumento != null ? dto.ComprobantePago.NroDocumento : "",
                                                    NombreRazonSocial = dto.ComprobantePago.NombreRazonSocial,
                                                    Direccion = dto.ComprobantePago.Direccion != null ? dto.ComprobantePago.Direccion : "",
                                                    BitComprobante = dto.ComprobantePago.BitComprobante,
                                                    IdOcurrencia = dto.ComprobantePago.IdOcurrencia,
                                                    IdAsesor = dto.ComprobantePago.IdAsesor,
                                                    IdOportunidad = dto.ComprobantePago.IdOportunidad,
                                                    Comentario = dto.ComprobantePago.Comentario,
                                                    FechaCreacion = DateTime.Now,
                                                    FechaModificacion = DateTime.Now,
                                                    UsuarioCreacion = dto.Usuario,
                                                    UsuarioModificacion = dto.Usuario,
                                                    Estado = true
                                                };

                                                var result = _repComprobantePagoOportunidad.Insert(comprobantePago);
                                                if (result)
                                                {
                                                    //Enviar Correo Finanzas  Boleta = 0 && Factura = 1
                                                    List<string> correos = new List<string>();
                                                    correos.Add("atrelles@bsginstitute.com");
                                                    //correos.Add("pteran@bsginstitute.com");
                                                    correos.Add("mlopezo@bsginstitute.com");
                                                    correos.Add("mzegarraj@bsginstitute.com");
                                                    correos.Add("ccrispin@bsginstitute.com");

                                                    string mensaje = comprobantePago.MensajeEmailComprobantePago();

                                                    //TMK_ImapServiceImpl correo = new TMK_ImapServiceImpl("jcayo@bsginstitute.com", "johancayocarrasco");

                                                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                                                    TMKMailDataDTO mailData = new TMKMailDataDTO
                                                    {
                                                        Sender = "jcayo@bsginstitute.com",
                                                        Recipient = string.Join(",", correos),
                                                        Subject = "BSG INSTITUTE - Datos Alumno en IS ",
                                                        Message = mensaje,
                                                        Cc = "",
                                                        Bcc = "",
                                                        AttachedFiles = null,
                                                        RemitenteC = comprobantePago.Nombres
                                                    };

                                                    Mailservice.SetData(mailData);

                                                    List<TMKMensajeIdDTO> MensajeIdDTO = Mailservice.SendMessageTask();

                                                    //Correos.envio_email("jcayo@bsginstitute.com", comprobantePago.nombresContacto + " " + comprobantePago.apellidos, "BSG INSTITUTE - Datos Alumno en IS ", mensaje, correos);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            else if (dto.filtro.Tipo == "automatica")
                            {
                                ReprogramacionCabeceraBO reprogramacionCabecera = _repReprogramacionCabecera.ObtenerReprogramacionCabecera(dto.filtro.IdActividadCabecera, dto.filtro.IdCategoria);
                                idReprogramacionCabecera = reprogramacionCabecera.Id;
                                ReprogramacionCabeceraPersonalBO reprogramacionCabeceraPersonal = _repReprogramacionCabeceraPersonal.FirstBy(w => w.IdActividadCabecera == dto.filtro.IdActividadCabecera && w.IdCategoriaOrigen == dto.filtro.IdCategoria && w.IdPersonal == dto.filtro.IdPersonal.Value && w.FechaReprogramacion.Date == DateTime.Now.Date);
                                //ReprogramacionCabeceraPersonalBO reprogramacionCabeceraPersonal = _repReprogramacionCabeceraPersonal.ObtenerReprogramacionCabeceraPersonal(dto.filtro.IdActividadCabecera, dto.filtro.IdCategoria, dto.filtro.IdPersonal.Value, DateTime.Now.Date);

                                if (reprogramacionCabeceraPersonal == null)
                                {
                                    reprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalBO
                                    {
                                        IdActividadCabecera = dto.filtro.IdActividadCabecera,
                                        IdCategoriaOrigen = dto.filtro.IdCategoria,
                                        IdPersonal = dto.filtro.IdPersonal.Value,
                                        ReproDia = 1,
                                        FechaReprogramacion = DateTime.Now.Date,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.filtro.Usuario,
                                        UsuarioModificacion = dto.filtro.Usuario,
                                        Estado = true
                                    };
                                    _repReprogramacionCabeceraPersonal.Insert(reprogramacionCabeceraPersonal);
                                    //_repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);
                                }
                                else
                                {
                                    if (reprogramacionCabecera == null)
                                    {
                                        return null;
                                    }
                                    else
                                    {
                                        if (reprogramacionCabecera.MaxReproPorDia > reprogramacionCabeceraPersonal.ReproDia)
                                        {
                                            reprogramacionCabeceraPersonal.ReproDia += 1;
                                            reprogramacionCabeceraPersonal.FechaReprogramacion = DateTime.Now.Date;
                                            reprogramacionCabeceraPersonal.FechaModificacion = DateTime.Now;
                                            reprogramacionCabeceraPersonal.UsuarioModificacion = dto.filtro.Usuario;
                                            reprogramacionCabeceraPersonal.Estado = true;
                                            _repReprogramacionCabeceraPersonal.Update(reprogramacionCabeceraPersonal);
                                        }
                                    }
                                }
                            }
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "jcayo@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = "Error FinalizarYProgramarActividad Transaction",
                                Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    try
                    {
                        //para poder medir la calidadde la llamada//08/08/2019
                        CalidadLlamadaLogRepositorio _repCalidadLlamadaLog = new CalidadLlamadaLogRepositorio(_integraDBContext);
                        var calidadLlamadaLog = new CalidadLlamadaLogBO
                        {
                            IdCalidadLlamada = dto.CalidadLlamada.IdCalidadLlamada,
                            IdProblemaLlamada = dto.CalidadLlamada.IdProblemaLlamada,
                            IdActividadDetalle = dto.ActividadAntigua.Id,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = dto.filtro.Usuario,
                            UsuarioModificacion = dto.filtro.Usuario,
                            Estado = true
                        };
                        _repCalidadLlamadaLog.Insert(calidadLlamadaLog);
                    }
                    catch (Exception ex)
                    {
                        List<string> correos = new List<string>();
                        correos.Add("sistemas@bsginstitute.com");

                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO
                        {
                            Sender = "jcayo@bsginstitute.com",
                            Recipient = string.Join(",", correos),
                            Subject = "Error FinalizarYProgramarActividad CalidadLlamada",
                            Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                            Cc = "",
                            Bcc = "",
                            AttachedFiles = null
                        };

                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }

                CompuestoActividadEjecutadaDTO realizada = new CompuestoActividadEjecutadaDTO();
                try
                {
                    realizada = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);
                }
                catch (Exception ex)
                {
                    realizada = new CompuestoActividadEjecutadaDTO();

                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Jojan 2",
                        Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                return Ok(new { realizada, idHoraBloqueada = horaBloqueada.Id, idOportunidad = Oportunidad.Id, IdReprogramacionCabecera = idReprogramacionCabecera });
            }
            catch (Exception ex)
            {
                try
                {
                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error FinalizarYProgramarActividad General",
                        Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                catch (Exception)
                {
                }

                return BadRequest(ex.Message);
            }
        }
        
        /// TipoFuncion: POST
        /// Autor: Jashin Salazar
        /// Fecha: 25/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza la Programación de Actividades
        /// </summary>
        /// <returns> Información de Programación de Actividad </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarYProgramarActividadAlterno([FromBody] ParametroFinalizarActividadDTO dto)
        {
            string parametros = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                parametros = JsonConvert.SerializeObject(dto);

                // Desactivado de redireccion
                var _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                if (dto.ActividadAntigua.IdOportunidad.HasValue)
                {
                    try
                    {
                        _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(dto.ActividadAntigua.IdOportunidad.Value);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                var _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);

                if (dto.ActividadAntigua.IdOportunidad.HasValue)
                    if (!_repOportunidad.Exist(x => x.Id == dto.ActividadAntigua.IdOportunidad && x.IdFaseOportunidad == dto.datosOportunidad.IdFaseOportunidad))
                        return BadRequest(new { Codigo = 409, Mensaje = $"La oportunidad fue trabajada por otra persona: IdOportunidad {dto.ActividadAntigua.IdOportunidad}" });

                var _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                var _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                var _repHoraBloqueada = new HoraBloqueadaRepositorio(_integraDBContext);
                var _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                var _repLlamadaActividad = new LlamadaActividadRepositorio(_integraDBContext);
                var _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                var _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
                var _repReprogramacionCabecera = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                var _repReprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalRepositorio(_integraDBContext);
                var _repComprobantePagoOportunidad = new ComprobantePagoOportunidadRepositorio(_integraDBContext);
                var _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);
                var oportunidadPrerequisitoGeneralRepositorio = new OportunidadPrerequisitoGeneralRepositorio(_integraDBContext);
                var oportunidadPrerequisitoEspecificoRepositorio = new OportunidadPrerequisitoEspecificoRepositorio(_integraDBContext);
                var oportunidadBeneficioRepositorio = new OportunidadBeneficioRepositorio(_integraDBContext);
                var detalleOportunidadCompetidorRepositorio = new DetalleOportunidadCompetidorRepositorio(_integraDBContext);

                int idReprogramacionCabecera = 0;
                HoraBloqueadaBO horaBloqueada = new HoraBloqueadaBO
                {
                    IdPersonal = dto.filtro.IdPersonal,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = dto.filtro.Usuario,
                    UsuarioModificacion = dto.filtro.Usuario,
                    Estado = true
                };

                if (dto.datosOportunidad.UltimaFechaProgramada != null)
                {
                    horaBloqueada.Fecha = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                    horaBloqueada.Hora = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                }

                if (horaBloqueada.HasErrors)
                {
                    return BadRequest(horaBloqueada.GetErrors(null));
                }

                var Oportunidad = new OportunidadBO(dto.ActividadAntigua.IdOportunidad.Value, dto.filtro.Usuario, _integraDBContext)
                {
                    IdFaseOportunidadIp = dto.datosOportunidad.IdFaseOportunidadIp,
                    IdFaseOportunidadIc = dto.datosOportunidad.IdFaseOportunidadIc,
                    FechaEnvioFaseOportunidadPf = dto.datosOportunidad.FechaEnvioFaseOportunidadPf,
                    FechaPagoFaseOportunidadPf = dto.datosOportunidad.FechaPagoFaseOportunidadPf,
                    FechaPagoFaseOportunidadIc = dto.datosOportunidad.FechaPagoFaseOportunidadIc,
                    IdFaseOportunidadPf = dto.datosOportunidad.IdFaseOportunidadPf,
                    CodigoPagoIc = dto.datosOportunidad.CodigoPagoIc,
                    ValidacionCorrecta = true
                };

                var ActividadAntigua = new ActividadDetalleBO
                {
                    Id = dto.ActividadAntigua.Id,
                    IdActividadCabecera = dto.ActividadAntigua.IdActividadCabecera,
                    FechaProgramada = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada),
                    FechaReal = dto.ActividadAntigua.FechaReal,
                    DuracionReal = dto.ActividadAntigua.DuracionReal,
                    //IdOcurrencia = dto.ActividadAntigua.IdOcurrencia.Value,
                    IdEstadoActividadDetalle = dto.ActividadAntigua.IdEstadoActividadDetalle,
                    Comentario = dto.ActividadAntigua.Comentario,
                    IdAlumno = dto.ActividadAntigua.IdAlumno,
                    Actor = dto.ActividadAntigua.Actor,
                    IdOportunidad = dto.ActividadAntigua.IdOportunidad.Value,
                    IdCentralLlamada = dto.ActividadAntigua.IdCentralLlamada,
                    RefLlamada = dto.ActividadAntigua.RefLlamada,
                    //IdOcurrenciaActividad = dto.ActividadAntigua.IdOcurrenciaActividad,
                    IdClasificacionPersona = Oportunidad.IdClasificacionPersona,
                    IdOcurrenciaAlterno = dto.ActividadAntigua.IdOcurrencia.Value,
                    IdOcurrenciaActividadAlterno= dto.ActividadAntigua.IdOcurrenciaActividad
                };

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(dto.ActividadAntigua.Id, _integraDBContext);

                OportunidadCompetidorBO OportunidadCompetidor;
                if (dto.DatosCompuesto.OportunidadCompetidor.Id == 0)
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO
                    {
                        Id = 0,
                        IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.IdOportunidad,
                        OtroBeneficio = dto.DatosCompuesto.OportunidadCompetidor.OtroBeneficio,
                        Respuesta = dto.DatosCompuesto.OportunidadCompetidor.Respuesta,
                        Completado = dto.DatosCompuesto.OportunidadCompetidor.Completado,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = dto.Usuario,
                        UsuarioModificacion = dto.Usuario,
                        Estado = true
                    };
                }
                else
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO(dto.DatosCompuesto.OportunidadCompetidor.Id, _integraDBContext)
                    {
                        IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.IdOportunidad,
                        OtroBeneficio = dto.DatosCompuesto.OportunidadCompetidor.OtroBeneficio,
                        Respuesta = dto.DatosCompuesto.OportunidadCompetidor.Respuesta,
                        Completado = dto.DatosCompuesto.OportunidadCompetidor.Completado
                    };
                }
                if (!OportunidadCompetidor.HasErrors)
                {
                    Oportunidad.OportunidadCompetidor = OportunidadCompetidor;
                }
                else
                {
                    return BadRequest(OportunidadCompetidor.GetErrors(null));
                }

                var CalidadBO = new CalidadProcesamientoBO
                {
                    IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.IdOportunidad,
                    PerfilCamposLlenos = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposLlenos,
                    PerfilCamposTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposTotal,
                    Dni = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.Dni,
                    PgeneralValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralValidados,
                    PgeneralTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralTotal,
                    PespecificoValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoValidados,
                    PespecificoTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoTotal,
                    BeneficiosValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosValidados,
                    BeneficiosTotales = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosTotales,
                    CompetidoresVerificacion = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.CompetidoresVerificacion,
                    ProblemaSeleccionados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSeleccionados,
                    ProblemaSolucionados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSolucionados,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    Estado = true
                };
                if (!CalidadBO.HasErrors)
                {
                    Oportunidad.CalidadProcesamiento = CalidadBO;
                }
                else
                {
                    return BadRequest(CalidadBO.GetErrors(null));
                }

                OportunidadCompetidor.ListaPrerequisitoGeneral = new List<OportunidadPrerequisitoGeneralBO>();
                OportunidadPrerequisitoGeneralBO ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO();
                var listaPreRequisitoGeneralAgrupado = dto.DatosCompuesto.ListaPrerequisitoGeneral.GroupBy(x => x.IdProgramaGeneralBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaPreRequisitoGeneralAgrupado)
                {
                    ListaPrerequisitoGeneral = oportunidadPrerequisitoGeneralRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio);

                    if (ListaPrerequisitoGeneral != null)
                    {
                        var listaPreRequisitoGeneralEliminar = oportunidadPrerequisitoGeneralRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio && x.Id != ListaPrerequisitoGeneral.Id).ToList();
                        foreach (var itemEliminar in listaPreRequisitoGeneralEliminar)
                        {
                            oportunidadPrerequisitoGeneralRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }


                    if (ListaPrerequisitoGeneral == null)
                    {
                        ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaPrerequisitoGeneral.Respuesta = item.Respuesta;
                    ListaPrerequisitoGeneral.Completado = item.Completado;
                    ListaPrerequisitoGeneral.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoGeneral.UsuarioModificacion = dto.Usuario;
                    if (!ListaPrerequisitoGeneral.HasErrors)
                    {
                        OportunidadCompetidor.ListaPrerequisitoGeneral.Add(ListaPrerequisitoGeneral);
                    }
                    else
                    {
                        return BadRequest(ListaPrerequisitoGeneral.GetErrors(null));
                    }
                }
                OportunidadCompetidor.ListaPrerequisitoEspecifico = new List<OportunidadPrerequisitoEspecificoBO>();
                OportunidadPrerequisitoEspecificoBO ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO();


                var listaPreRequisitoEspecificoAgrupado = dto.DatosCompuesto.ListaPrerequisitoEspecifico.GroupBy(x => x.IdProgramaGeneralBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaPreRequisitoEspecificoAgrupado)
                {
                    ListaPrerequisitoEspecifico = oportunidadPrerequisitoEspecificoRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio);
                    if (ListaPrerequisitoEspecifico != null)
                    {
                        var listaPreRequisitoEspecificoEliminar = oportunidadPrerequisitoEspecificoRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio && x.Id != ListaPrerequisitoEspecifico.Id).ToList();
                        foreach (var itemEliminar in listaPreRequisitoEspecificoEliminar)
                        {
                            oportunidadPrerequisitoEspecificoRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }

                    if (ListaPrerequisitoEspecifico == null)
                    {
                        ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaPrerequisitoEspecifico.Respuesta = item.Respuesta;
                    ListaPrerequisitoEspecifico.Completado = item.Completado;
                    ListaPrerequisitoEspecifico.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.UsuarioModificacion = dto.Usuario;
                    if (!ListaPrerequisitoEspecifico.HasErrors)
                    {
                        OportunidadCompetidor.ListaPrerequisitoEspecifico.Add(ListaPrerequisitoEspecifico);
                    }
                    else
                    {
                        return BadRequest(ListaPrerequisitoEspecifico.GetErrors(null));
                    }
                }

                OportunidadCompetidor.ListaBeneficio = new List<OportunidadBeneficioBO>();
                OportunidadBeneficioBO ListaBeneficio = new OportunidadBeneficioBO();
                var listaBeneficioAgrupado = dto.DatosCompuesto.ListaBeneficio.GroupBy(x => x.IdBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaBeneficioAgrupado)
                {
                    ListaBeneficio = oportunidadBeneficioRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdBeneficio == item.IdBeneficio);

                    if (ListaBeneficio != null)
                    {
                        var listaBeneficioEliminar = oportunidadBeneficioRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdBeneficio == item.IdBeneficio && x.Id != ListaBeneficio.Id).ToList();
                        foreach (var itemEliminar in listaBeneficioEliminar)
                        {
                            oportunidadBeneficioRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }

                    if (ListaBeneficio == null)
                    {
                        ListaBeneficio = new OportunidadBeneficioBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdBeneficio = item.IdBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaBeneficio.Respuesta = item.Respuesta;
                    ListaBeneficio.Completado = item.Completado;
                    ListaBeneficio.FechaModificacion = DateTime.Now;
                    ListaBeneficio.UsuarioModificacion = dto.Usuario;
                    if (!ListaBeneficio.HasErrors)
                    {
                        OportunidadCompetidor.ListaBeneficio.Add(ListaBeneficio);
                    }
                    else
                    {
                        return BadRequest(ListaBeneficio.GetErrors(null));
                    }
                }

                //======================================
                ProgramaGeneralBeneficioRespuestaRepositorio _repBeneficioAlternoRespuesta = new ProgramaGeneralBeneficioRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioRespuestaBO beneficioAlterno = new ProgramaGeneralBeneficioRespuestaBO();
                var listaBeneficioAlternoAgrupado = dto.DatosCompuesto.ListaBeneficioAlterno.GroupBy(x => x.IdBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaBeneficioAlternoAgrupado)
                {
                    beneficioAlterno = _repBeneficioAlternoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralBeneficio == item.IdBeneficio);

                    if (beneficioAlterno != null)
                    {
                        beneficioAlterno.Respuesta = item.Respuesta;
                        beneficioAlterno.UsuarioModificacion = dto.Usuario;
                        beneficioAlterno.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Update(beneficioAlterno);
                    }
                    else
                    {
                        ProgramaGeneralBeneficioRespuestaBO beneficioAlternoV2 = new ProgramaGeneralBeneficioRespuestaBO();
                        beneficioAlternoV2.IdOportunidad = item.IdOportunidad;
                        beneficioAlternoV2.IdProgramaGeneralBeneficio = item.IdBeneficio;
                        beneficioAlternoV2.Respuesta = item.Respuesta;
                        beneficioAlternoV2.Estado = true;
                        beneficioAlternoV2.UsuarioCreacion = dto.Usuario;
                        beneficioAlternoV2.UsuarioModificacion = dto.Usuario;
                        beneficioAlternoV2.FechaCreacion = DateTime.Now;
                        beneficioAlternoV2.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Insert(beneficioAlternoV2);
                    }
                }

                ProgramaGeneralPrerequisitoRespuestaRepositorio _repPrerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaBO();
                var listaPrerequisitoRespuestaAgrupado = dto.DatosCompuesto.ListaPrerequisitoGeneralAlterno.GroupBy(x => x.IdProgramaGeneralPrerequisito).Select(x => x.First()).ToList();
                foreach (var item in listaPrerequisitoRespuestaAgrupado)
                {
                    prerequisitoRespuesta = _repPrerequisitoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralPrerequisito);

                    if (prerequisitoRespuesta != null)
                    {
                        prerequisitoRespuesta.Respuesta = item.Respuesta;
                        prerequisitoRespuesta.UsuarioModificacion = dto.Usuario;
                        prerequisitoRespuesta.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Update(prerequisitoRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuestaAlterno = new ProgramaGeneralPrerequisitoRespuestaBO();
                        prerequisitoRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        prerequisitoRespuestaAlterno.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralPrerequisito;
                        prerequisitoRespuestaAlterno.Respuesta = item.Respuesta;
                        prerequisitoRespuestaAlterno.Estado = true;
                        prerequisitoRespuestaAlterno.UsuarioCreacion = dto.Usuario;
                        prerequisitoRespuestaAlterno.UsuarioModificacion = dto.Usuario;
                        prerequisitoRespuestaAlterno.FechaCreacion = DateTime.Now;
                        prerequisitoRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Insert(prerequisitoRespuestaAlterno);
                    }
                }

                //======================================

                detalleOportunidadCompetidorRepositorio.DeleteLogicoPorOportunidadCompetidor(dto.DatosCompuesto.OportunidadCompetidor.Id, dto.Usuario, dto.DatosCompuesto.ListaCompetidor);
                OportunidadCompetidor.ListaCompetidor = new List<DetalleOportunidadCompetidorBO>();
                DetalleOportunidadCompetidorBO ListaCompetidor = new DetalleOportunidadCompetidorBO();
                foreach (var item in dto.DatosCompuesto.ListaCompetidor)
                {
                    ListaCompetidor = detalleOportunidadCompetidorRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdCompetidor == item);
                    if (ListaCompetidor == null)
                    {
                        ListaCompetidor = new DetalleOportunidadCompetidorBO
                        {
                            IdOportunidadCompetidor = 0,
                            IdCompetidor = item,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario
                        };
                    }
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioModificacion = dto.Usuario;
                    if (!ListaCompetidor.HasErrors)
                    {
                        OportunidadCompetidor.ListaCompetidor.Add(ListaCompetidor);
                    }
                    else
                    {
                        return BadRequest(ListaCompetidor.GetErrors(null));
                    }
                }
                Oportunidad.ListaSoluciones = new List<SolucionClienteByActividadBO>();
                foreach (var item in dto.DatosCompuesto.ListaSoluciones)
                {
                    var ListaSoluciones = new SolucionClienteByActividadBO
                    {
                        IdOportunidad = item.IdOportunidad,
                        IdActividadDetalle = item.IdActividadDetalle,
                        IdCausa = item.IdCausa,
                        IdPersonal = item.IdPersonal,
                        Solucionado = item.Solucionado,
                        IdProblemaCliente = item.IdProblemaCliente,
                        OtroProblema = item.OtroProblema,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = dto.Usuario,
                        UsuarioModificacion = dto.Usuario,
                        Estado = true
                    };

                    if (!ListaSoluciones.HasErrors)
                    {
                        Oportunidad.ListaSoluciones.Add(ListaSoluciones);
                    }
                    else
                    {
                        return BadRequest(ListaSoluciones.GetErrors(null));
                    }
                }

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    ActividadNueva.LlamadaActividad = null;
                    Oportunidad.FinalizarActividadAlterno(false, dto.datosOportunidad, dto.ActividadAntigua.IdOcurrenciaActividad);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                            {
                                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 0);
                                }
                                else
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 1);

                                }
                            }

                            _repHoraBloqueada.Insert(horaBloqueada);

                            Oportunidad.ProgramaActividadAlterno();

                            _repActividadDetalle.Insert(Oportunidad.ActividadNuevaProgramarActividad);
                            Oportunidad.IdActividadDetalleUltima = Oportunidad.ActividadNuevaProgramarActividad.Id;
                            Oportunidad.ActividadNuevaProgramarActividad = null;
                            _repOportunidad.Update(Oportunidad);

                            //_repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);

                            if (dto.filtro.Tipo == "manual")
                            {
                                if (dto.datosOportunidad.IdFaseOportunidad != Oportunidad.IdFaseOportunidad)
                                {
                                    if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                                    {
                                        //if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad) && ActividadAntigua.IdOcurrencia == ValorEstatico.IdOcurrenciaConfirmaPagoIs)
                                        if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                        {
                                            try
                                            {
                                                //Enviar plantilla de condiciones
                                                //1246    Nuevas Condiciones y Características -Perú
                                                //1247    Nuevas Condiciones y Características -Colombia
                                                //1401    Condiciones y Características - Mexico
                                                //1402    Contrato de uso de datos biométricos por voz  - México

                                                var _repAlumno = new AlumnoRepositorio(_integraDBContext);

                                                var alumno = _repAlumno.FirstById(Oportunidad.IdAlumno);
                                                var _idPlantilla = 0;
                                                var _idPlantilla2 = 0;

                                                if (alumno.IdCodigoPais == 57)//Colombia
                                                {
                                                    _idPlantilla = 1247;
                                                }
                                                else if (alumno.IdCodigoPais == 51)//Peru
                                                {
                                                    _idPlantilla = 1246;
                                                }
                                                else if (alumno.IdCodigoPais == 52)//Mexico
                                                {
                                                    _idPlantilla = 1401;
                                                    _idPlantilla2 = 1402;
                                                }

                                                if (_idPlantilla != 0)
                                                {
                                                    var Objeto = new PlantillaPwBO(_integraDBContext)
                                                    {
                                                        IdOportunidad = Oportunidad.Id,
                                                        IdPlantilla = _idPlantilla
                                                    };
                                                    var _repPgeneralTipoDescuento = new PgeneralTipoDescuentoRepositorio(_integraDBContext);
                                                    Objeto.GetValorEtiqueta(Oportunidad.IdCentroCosto.Value, Oportunidad.IdFaseOportunidad, Oportunidad.Id);

                                                    var DatosOportunidad = Objeto.ObtenerDatosOportunidad(Oportunidad.Id);
                                                    string FechaInicioPrograma = "";

                                                    var Promocion = _repPgeneralTipoDescuento.FirstBy(w => w.Estado == true && w.IdPgeneral == DatosOportunidad.IdPgeneral.Value && w.IdTipoDescuento == 143, y => new { y.FlagPromocion });
                                                    if (Promocion != null)
                                                    {
                                                        DatosOportunidad.Promocion25 = Promocion.FlagPromocion;
                                                    }
                                                    FechaInicioPrograma = Objeto.ObtenerFechaInicioPrograma(DatosOportunidad.IdPgeneral.Value, DatosOportunidad.IdCentroCosto.Value);
                                                    //var etiquetaDuracionYHorarios = Objeto.DuracionYHorarios(DatosOportunidad.IdCentroCosto);
                                                    DatosOportunidad.CostoTotalConDescuento = Objeto.ObtenerCostoTotalConDescuento(Oportunidad.Id);
                                                    Objeto.ObtenerDatosProgramaGeneral(DatosOportunidad.IdPgeneral.Value);
                                                    //reemplazo
                                                    Objeto.ReemplazarEtiquetas();

                                                    var emailCalculado = Objeto.EmailReemplazado;
                                                    List<string> correosPersonalizadosCopia = new List<string>
                                                    {
                                                        "grabaciones@bsginstitute.com",
                                                         "mzegarraj@bsginstitute.com"
                                                    };

                                                    List<string> correosPersonalizados = new List<string>
                                                    {
                                                        Objeto.DatosOportunidadAlumno.OportunidadAlumno.Email1
                                                    };

                                                    var mailDataPersonalizado = new TMKMailDataDTO
                                                    {
                                                        Sender = "matriculas@bsginstitute.com",
                                                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                                        Subject = emailCalculado.Asunto,
                                                        Message = emailCalculado.CuerpoHTML,
                                                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                                                        Bcc = "",
                                                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                                                    };
                                                    var mailServie = new TMK_MailServiceImpl();
                                                    mailServie.SetData(mailDataPersonalizado);
                                                    mailServie.SendMessageTask();
                                                    var _repDocumentoEnviadoWebPw = new DocumentoEnviadoWebPwRepositorio(_integraDBContext);
                                                    var documentoEnviadoWebPwBO = new DocumentoEnviadoWebPwBO()
                                                    {
                                                        IdAlumno = Oportunidad.IdAlumno,
                                                        Nombre = "BSG Institute - Condiciones y Características",
                                                        IdPespecifico = Objeto.DatosOportunidadAlumno.IdPEspecifico,
                                                        FechaEnvio = DateTime.Now,
                                                        FechaCreacion = DateTime.Now,
                                                        FechaModificacion = DateTime.Now,
                                                        UsuarioCreacion = dto.Usuario,
                                                        UsuarioModificacion = dto.Usuario,
                                                        Estado = true
                                                    };
                                                    if (!documentoEnviadoWebPwBO.HasErrors)
                                                    {
                                                        _repDocumentoEnviadoWebPw.Insert(documentoEnviadoWebPwBO);
                                                    }


                                                    if (_idPlantilla2 != 0)
                                                    {
                                                        //reemplazo 2
                                                        Objeto.IdPlantilla = _idPlantilla2;
                                                        Objeto.ReemplazarEtiquetas();
                                                        var emailCalculado2 = Objeto.EmailReemplazado;

                                                        var mailDataPersonalizado2 = new TMKMailDataDTO
                                                        {
                                                            Sender = "matriculas@bsginstitute.com",
                                                            Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                                            Subject = emailCalculado2.Asunto,
                                                            Message = emailCalculado2.CuerpoHTML,
                                                            Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                                                            Bcc = "ccrispin@bsginstitute.com",
                                                            AttachedFiles = emailCalculado2.ListaArchivosAdjuntos
                                                        };
                                                        var mailServie2 = new TMK_MailServiceImpl();
                                                        mailServie2.SetData(mailDataPersonalizado2);
                                                        mailServie2.SendMessageTask();

                                                    }


                                                }

                                            }
                                            catch (Exception e)
                                            {
                                            }


                                            if (ActividadAntigua.IdOcurrenciaAlterno == ValorEstatico.IdOcurrenciaConfirmaPagoIs || ActividadAntigua.IdOcurrenciaAlterno == ValorEstatico.IdOcurrenciaIsSinLlamada)
                                            {
                                                var comprobantePago = new ComprobantePagoOportunidadBO
                                                {
                                                    IdContacto = dto.ComprobantePago.IdContacto,
                                                    Nombres = dto.ComprobantePago.Nombres,
                                                    Apellidos = dto.ComprobantePago.Apellidos,
                                                    Celular = dto.ComprobantePago.Celular,
                                                    Dni = dto.ComprobantePago.Dni == null ? "" : dto.ComprobantePago.Dni,
                                                    Correo = dto.ComprobantePago.Correo,
                                                    NombrePais = dto.ComprobantePago.NombrePais,
                                                    IdPais = dto.ComprobantePago.IdPais,
                                                    NombreCiudad = dto.ComprobantePago.NombreCiudad == null ? "" : dto.ComprobantePago.NombreCiudad,
                                                    TipoComprobante = dto.ComprobantePago.TipoComprobante,
                                                    NroDocumento = dto.ComprobantePago.NroDocumento != null ? dto.ComprobantePago.NroDocumento : "",
                                                    NombreRazonSocial = dto.ComprobantePago.NombreRazonSocial,
                                                    Direccion = dto.ComprobantePago.Direccion != null ? dto.ComprobantePago.Direccion : "",
                                                    BitComprobante = dto.ComprobantePago.BitComprobante,
                                                    IdOcurrencia = dto.ComprobantePago.IdOcurrencia,
                                                    IdAsesor = dto.ComprobantePago.IdAsesor,
                                                    IdOportunidad = dto.ComprobantePago.IdOportunidad,
                                                    Comentario = dto.ComprobantePago.Comentario,
                                                    FechaCreacion = DateTime.Now,
                                                    FechaModificacion = DateTime.Now,
                                                    UsuarioCreacion = dto.Usuario,
                                                    UsuarioModificacion = dto.Usuario,
                                                    Estado = true
                                                };

                                                var result = _repComprobantePagoOportunidad.Insert(comprobantePago);
                                                if (result)
                                                {
                                                    //Enviar Correo Finanzas  Boleta = 0 && Factura = 1
                                                    List<string> correos = new List<string>();
                                                    correos.Add("atrelles@bsginstitute.com");
                                                    //correos.Add("pteran@bsginstitute.com");
                                                    correos.Add("mlopezo@bsginstitute.com");
                                                    correos.Add("mzegarraj@bsginstitute.com");
                                                    correos.Add("ccrispin@bsginstitute.com");

                                                    string mensaje = comprobantePago.MensajeEmailComprobantePago();

                                                    //TMK_ImapServiceImpl correo = new TMK_ImapServiceImpl("jcayo@bsginstitute.com", "johancayocarrasco");

                                                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                                                    TMKMailDataDTO mailData = new TMKMailDataDTO
                                                    {
                                                        Sender = "jcayo@bsginstitute.com",
                                                        Recipient = string.Join(",", correos),
                                                        Subject = "BSG INSTITUTE - Datos Alumno en IS ",
                                                        Message = mensaje,
                                                        Cc = "",
                                                        Bcc = "",
                                                        AttachedFiles = null,
                                                        RemitenteC = comprobantePago.Nombres
                                                    };

                                                    Mailservice.SetData(mailData);

                                                    List<TMKMensajeIdDTO> MensajeIdDTO = Mailservice.SendMessageTask();

                                                    //Correos.envio_email("jcayo@bsginstitute.com", comprobantePago.nombresContacto + " " + comprobantePago.apellidos, "BSG INSTITUTE - Datos Alumno en IS ", mensaje, correos);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            else if (dto.filtro.Tipo == "automatica")
                            {
                                ReprogramacionCabeceraBO reprogramacionCabecera = _repReprogramacionCabecera.ObtenerReprogramacionCabecera(dto.filtro.IdActividadCabecera, dto.filtro.IdCategoria);
                                idReprogramacionCabecera = reprogramacionCabecera.Id;
                                ReprogramacionCabeceraPersonalBO reprogramacionCabeceraPersonal = _repReprogramacionCabeceraPersonal.FirstBy(w => w.IdActividadCabecera == dto.filtro.IdActividadCabecera && w.IdCategoriaOrigen == dto.filtro.IdCategoria && w.IdPersonal == dto.filtro.IdPersonal.Value && w.FechaReprogramacion.Date == DateTime.Now.Date);
                                //ReprogramacionCabeceraPersonalBO reprogramacionCabeceraPersonal = _repReprogramacionCabeceraPersonal.ObtenerReprogramacionCabeceraPersonal(dto.filtro.IdActividadCabecera, dto.filtro.IdCategoria, dto.filtro.IdPersonal.Value, DateTime.Now.Date);

                                if (reprogramacionCabeceraPersonal == null)
                                {
                                    reprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalBO
                                    {
                                        IdActividadCabecera = dto.filtro.IdActividadCabecera,
                                        IdCategoriaOrigen = dto.filtro.IdCategoria,
                                        IdPersonal = dto.filtro.IdPersonal.Value,
                                        ReproDia = 1,
                                        FechaReprogramacion = DateTime.Now.Date,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.filtro.Usuario,
                                        UsuarioModificacion = dto.filtro.Usuario,
                                        Estado = true
                                    };
                                    _repReprogramacionCabeceraPersonal.Insert(reprogramacionCabeceraPersonal);
                                    //_repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);
                                }
                                else
                                {
                                    if (reprogramacionCabecera == null)
                                    {
                                        return null;
                                    }
                                    else
                                    {
                                        if (reprogramacionCabecera.MaxReproPorDia > reprogramacionCabeceraPersonal.ReproDia)
                                        {
                                            reprogramacionCabeceraPersonal.ReproDia += 1;
                                            reprogramacionCabeceraPersonal.FechaReprogramacion = DateTime.Now.Date;
                                            reprogramacionCabeceraPersonal.FechaModificacion = DateTime.Now;
                                            reprogramacionCabeceraPersonal.UsuarioModificacion = dto.filtro.Usuario;
                                            reprogramacionCabeceraPersonal.Estado = true;
                                            _repReprogramacionCabeceraPersonal.Update(reprogramacionCabeceraPersonal);
                                        }
                                    }
                                }
                            }
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "jcayo@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = "Error FinalizarYProgramarActividad Transaction",
                                Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    try
                    {
                        //para poder medir la calidadde la llamada//08/08/2019
                        CalidadLlamadaLogRepositorio _repCalidadLlamadaLog = new CalidadLlamadaLogRepositorio(_integraDBContext);
                        var calidadLlamadaLog = new CalidadLlamadaLogBO
                        {
                            IdCalidadLlamada = dto.CalidadLlamada.IdCalidadLlamada,
                            IdProblemaLlamada = dto.CalidadLlamada.IdProblemaLlamada,
                            IdActividadDetalle = dto.ActividadAntigua.Id,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = dto.filtro.Usuario,
                            UsuarioModificacion = dto.filtro.Usuario,
                            Estado = true
                        };
                        _repCalidadLlamadaLog.Insert(calidadLlamadaLog);
                    }
                    catch (Exception ex)
                    {
                        List<string> correos = new List<string>();
                        correos.Add("sistemas@bsginstitute.com");

                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO
                        {
                            Sender = "jcayo@bsginstitute.com",
                            Recipient = string.Join(",", correos),
                            Subject = "Error FinalizarYProgramarActividad CalidadLlamada",
                            Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                            Cc = "",
                            Bcc = "",
                            AttachedFiles = null
                        };

                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }

                CompuestoActividadEjecutadaDTO realizada = new CompuestoActividadEjecutadaDTO();
                try
                {
                    realizada = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);
                }
                catch (Exception ex)
                {
                    realizada = new CompuestoActividadEjecutadaDTO();

                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Jojan 2",
                        Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                return Ok(new { realizada, idHoraBloqueada = horaBloqueada.Id, idOportunidad = Oportunidad.Id, IdReprogramacionCabecera = idReprogramacionCabecera });
            }
            catch (Exception ex)
            {
                try
                {
                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error FinalizarYProgramarActividad General",
                        Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                catch (Exception)
                {
                }

                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza la Programación de Actividades en Operaciones
        /// </summary>
        /// <returns> Información de Programación de Actividad </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarYProgramarActividadOperaciones([FromBody] ParametroFinalizarActividadDTO dto)
        {
            string parametros = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                parametros = JsonConvert.SerializeObject(dto);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                PreCalculadaCambioFaseRepositorio _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                HoraBloqueadaRepositorio _repHoraBloqueada = new HoraBloqueadaRepositorio(_integraDBContext);
                OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                LlamadaActividadRepositorio _repLlamadaActividad = new LlamadaActividadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
                ReprogramacionCabeceraRepositorio _repReprogramacionCabecera = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                ReprogramacionCabeceraPersonalRepositorio _repReprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalRepositorio(_integraDBContext);
                ComprobantePagoOportunidadRepositorio _repComprobantePagoOportunidad = new ComprobantePagoOportunidadRepositorio(_integraDBContext);
                LlamadaActividadRepositorio _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);
                OportunidadPrerequisitoGeneralRepositorio oportunidadPrerequisitoGeneralRepositorio = new OportunidadPrerequisitoGeneralRepositorio(_integraDBContext);
                OportunidadPrerequisitoEspecificoRepositorio oportunidadPrerequisitoEspecificoRepositorio = new OportunidadPrerequisitoEspecificoRepositorio(_integraDBContext);
                OportunidadBeneficioRepositorio oportunidadBeneficioRepositorio = new OportunidadBeneficioRepositorio(_integraDBContext);
                DetalleOportunidadCompetidorRepositorio detalleOportunidadCompetidorRepositorio = new DetalleOportunidadCompetidorRepositorio(_integraDBContext);

                int idReprogramacionCabecera = 0;
                HoraBloqueadaBO horaBloqueada = new HoraBloqueadaBO
                {
                    IdPersonal = dto.filtro.IdPersonal
                };
                if (dto.datosOportunidad.UltimaFechaProgramada != null)
                {
                    dto.datosOportunidad.UltimaFechaProgramada = dto.datosOportunidad.UltimaFechaProgramada + " " + dto.datosOportunidad.UltimaHoraProgramada;
                    if (dto.tipoProgramacion == "manual")
                    {
                        var datosOportunidad = _repOportunidad.ObtenerDatosParaReprogramcionAutomatica(dto.ActividadAntigua.IdOportunidad.Value);

                        PersonalHorarioBO PersonalHorario = new PersonalHorarioBO(_integraDBContext)
                        {
                            IdPersonal = datosOportunidad.IdPersonalAsignado
                        };

                        HoraReprogramacionAutomaticaBO HorarioReprogramacion = new HoraReprogramacionAutomaticaBO(_integraDBContext)
                        {
                            personalHorario = PersonalHorario.GetHorarioAsTable(),
                            IdPersonal = datosOportunidad.IdPersonalAsignado
                        };
                        dto.datosOportunidad.UltimaFechaProgramada = HorarioReprogramacion.ObtenerFechaHoraReprogramacionManualOperaciones(DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada));


                    }

                    horaBloqueada.Fecha = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                    horaBloqueada.Hora = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                }

                horaBloqueada.FechaCreacion = DateTime.Now;
                horaBloqueada.FechaModificacion = DateTime.Now;
                horaBloqueada.UsuarioCreacion = dto.filtro.Usuario;
                horaBloqueada.UsuarioModificacion = dto.filtro.Usuario;
                horaBloqueada.Estado = true;

                if (horaBloqueada.HasErrors)
                    return BadRequest(horaBloqueada.GetErrors(null));

                OportunidadBO Oportunidad = new OportunidadBO(dto.ActividadAntigua.IdOportunidad.Value, dto.filtro.Usuario, _integraDBContext);

                Oportunidad.IdFaseOportunidadIp = dto.datosOportunidad.IdFaseOportunidadIp;
                Oportunidad.IdFaseOportunidadIc = dto.datosOportunidad.IdFaseOportunidadIc;
                Oportunidad.FechaEnvioFaseOportunidadPf = dto.datosOportunidad.FechaEnvioFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadPf = dto.datosOportunidad.FechaPagoFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadIc = dto.datosOportunidad.FechaPagoFaseOportunidadIc;
                Oportunidad.IdFaseOportunidadPf = dto.datosOportunidad.IdFaseOportunidadPf;
                Oportunidad.CodigoPagoIc = dto.datosOportunidad.CodigoPagoIc;

                ActividadDetalleBO ActividadAntigua = new ActividadDetalleBO();
                ActividadAntigua.Id = dto.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = dto.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                ActividadAntigua.FechaReal = dto.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = dto.ActividadAntigua.DuracionReal;
                ActividadAntigua.IdOcurrencia = dto.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = dto.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = dto.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = dto.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = dto.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = dto.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = dto.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = dto.ActividadAntigua.RefLlamada;
                ActividadAntigua.IdOcurrenciaActividad = dto.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(dto.ActividadAntigua.Id, _integraDBContext);

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    ActividadNueva.LlamadaActividad = null;
                    Oportunidad.FinalizarActividad(false, dto.datosOportunidad);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }


                            _repHoraBloqueada.Insert(horaBloqueada);

                            Oportunidad.ProgramaActividad();
                            if (dto.filtro.IdActividadCabecera == 47 || dto.filtro.IdActividadCabecera == 63)
                            {
                                Oportunidad.ActividadNuevaProgramarActividad.IdActividadCabecera = 48;
                                Oportunidad.IdActividadCabeceraUltima = 48;
                            }

                            _repActividadDetalle.Insert(Oportunidad.ActividadNuevaProgramarActividad);
                            Oportunidad.IdActividadDetalleUltima = Oportunidad.ActividadNuevaProgramarActividad.Id;
                            Oportunidad.ActividadNuevaProgramarActividad = null;
                            _repOportunidad.Update(Oportunidad);


                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error FinalizarYProgramarActividad Transaction";
                            mailData.Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros;
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }

                CompuestoActividadEjecutadaDTO realizada = new CompuestoActividadEjecutadaDTO();

                return Ok(new { realizada, idOportunidad = Oportunidad.Id, IdReprogramacionCabecera = idReprogramacionCabecera });
            }
            catch (Exception ex)
            {
                try
                {
                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "jcayo@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Error FinalizarYProgramarActividad General";
                    mailData.Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros;
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                catch (Exception)
                {
                }

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza la Programación de Actividades en Operaciones Docentes
        /// </summary>
        /// <returns> Información de Programación de Actividad </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarYProgramarActividadOperacionesDocente([FromBody] ParametroFinalizarActividadDTO dto)
        {
            string parametros = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                parametros = JsonConvert.SerializeObject(dto);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                PreCalculadaCambioFaseRepositorio _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                HoraBloqueadaRepositorio _repHoraBloqueada = new HoraBloqueadaRepositorio(_integraDBContext);
                OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                LlamadaActividadRepositorio _repLlamadaActividad = new LlamadaActividadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
                ReprogramacionCabeceraRepositorio _repReprogramacionCabecera = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                ReprogramacionCabeceraPersonalRepositorio _repReprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalRepositorio(_integraDBContext);
                ComprobantePagoOportunidadRepositorio _repComprobantePagoOportunidad = new ComprobantePagoOportunidadRepositorio(_integraDBContext);
                LlamadaActividadRepositorio _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);
                OportunidadPrerequisitoGeneralRepositorio oportunidadPrerequisitoGeneralRepositorio = new OportunidadPrerequisitoGeneralRepositorio(_integraDBContext);
                OportunidadPrerequisitoEspecificoRepositorio oportunidadPrerequisitoEspecificoRepositorio = new OportunidadPrerequisitoEspecificoRepositorio(_integraDBContext);
                OportunidadBeneficioRepositorio oportunidadBeneficioRepositorio = new OportunidadBeneficioRepositorio(_integraDBContext);
                DetalleOportunidadCompetidorRepositorio detalleOportunidadCompetidorRepositorio = new DetalleOportunidadCompetidorRepositorio(_integraDBContext);

                int idReprogramacionCabecera = 0;
                HoraBloqueadaBO horaBloqueada = new HoraBloqueadaBO
                {
                    IdPersonal = dto.filtro.IdPersonal
                };
                if (dto.datosOportunidad.UltimaFechaProgramada != null)
                {
                    dto.datosOportunidad.UltimaFechaProgramada = dto.datosOportunidad.UltimaFechaProgramada + " " + dto.datosOportunidad.UltimaHoraProgramada;
                    if (dto.tipoProgramacion == "manual")
                    {
                        var datosOportunidad = _repOportunidad.ObtenerDatosParaReprogramcionAutomatica(dto.ActividadAntigua.IdOportunidad.Value);

                        PersonalHorarioBO PersonalHorario = new PersonalHorarioBO(_integraDBContext)
                        {
                            IdPersonal = datosOportunidad.IdPersonalAsignado
                        };

                        HoraReprogramacionAutomaticaBO HorarioReprogramacion = new HoraReprogramacionAutomaticaBO(_integraDBContext)
                        {
                            personalHorario = PersonalHorario.GetHorarioAsTable(),
                            IdPersonal = datosOportunidad.IdPersonalAsignado
                        };
                        dto.datosOportunidad.UltimaFechaProgramada = HorarioReprogramacion.ObtenerFechaHoraReprogramacionManualOperaciones(DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada));


                    }

                    horaBloqueada.Fecha = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                    horaBloqueada.Hora = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                }

                horaBloqueada.FechaCreacion = DateTime.Now;
                horaBloqueada.FechaModificacion = DateTime.Now;
                horaBloqueada.UsuarioCreacion = dto.filtro.Usuario;
                horaBloqueada.UsuarioModificacion = dto.filtro.Usuario;
                horaBloqueada.Estado = true;

                if (horaBloqueada.HasErrors)
                    return BadRequest(horaBloqueada.GetErrors(null));

                OportunidadBO Oportunidad = new OportunidadBO(dto.ActividadAntigua.IdOportunidad.Value, dto.filtro.Usuario, _integraDBContext);

                Oportunidad.IdFaseOportunidadIp = dto.datosOportunidad.IdFaseOportunidadIp;
                Oportunidad.IdFaseOportunidadIc = dto.datosOportunidad.IdFaseOportunidadIc;
                Oportunidad.FechaEnvioFaseOportunidadPf = dto.datosOportunidad.FechaEnvioFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadPf = dto.datosOportunidad.FechaPagoFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadIc = dto.datosOportunidad.FechaPagoFaseOportunidadIc;
                Oportunidad.IdFaseOportunidadPf = dto.datosOportunidad.IdFaseOportunidadPf;
                Oportunidad.CodigoPagoIc = dto.datosOportunidad.CodigoPagoIc;

                ActividadDetalleBO ActividadAntigua = new ActividadDetalleBO();
                ActividadAntigua.Id = dto.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = dto.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                ActividadAntigua.FechaReal = dto.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = dto.ActividadAntigua.DuracionReal;
                ActividadAntigua.IdOcurrencia = dto.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = dto.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = dto.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = dto.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = dto.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = dto.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = dto.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = dto.ActividadAntigua.RefLlamada;
                ActividadAntigua.IdOcurrenciaActividad = dto.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(dto.ActividadAntigua.Id, _integraDBContext);

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    ActividadNueva.LlamadaActividad = null;
                    Oportunidad.FinalizarActividad(false, dto.datosOportunidad);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }


                            _repHoraBloqueada.Insert(horaBloqueada);

                            Oportunidad.ProgramaActividad();
                            if (dto.filtro.IdActividadCabecera == 47 || dto.filtro.IdActividadCabecera == 63)
                            {
                                Oportunidad.ActividadNuevaProgramarActividad.IdActividadCabecera = 48;
                                Oportunidad.IdActividadCabeceraUltima = 48;
                            }

                            _repActividadDetalle.Insert(Oportunidad.ActividadNuevaProgramarActividad);
                            Oportunidad.IdActividadDetalleUltima = Oportunidad.ActividadNuevaProgramarActividad.Id;
                            Oportunidad.ActividadNuevaProgramarActividad = null;
                            _repOportunidad.Update(Oportunidad);


                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error FinalizarYProgramarActividad Transaction";
                            mailData.Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros;
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }

                CompuestoActividadEjecutadaDTO realizada = new CompuestoActividadEjecutadaDTO();

                return Ok(new { realizada, idOportunidad = Oportunidad.Id, IdReprogramacionCabecera = idReprogramacionCabecera });
            }
            catch (Exception ex)
            {
                try
                {
                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "jcayo@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Error FinalizarYProgramarActividad General";
                    mailData.Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros;
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                catch (Exception)
                {
                }

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las plantillas disponibles por fase
        /// </summary>
        /// <returns> Información de Plantillas disponibles </returns>
        /// <returns> Lista Objeto DTO : List<FiltroDTO> </returns>
        [Route("[action]/{idFaseOportunidad}")]
        [HttpGet]
        public IActionResult ObtenerPlantillaPorFase(int idFaseOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idFaseOportunidad <= 0)
            {
                return BadRequest("El Id FaseOportunidad no Existe");
            }
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
                var PlantillaFase = _repPlantillaClaveValor.ObtenerPlantillaGenerarMensaje(idFaseOportunidad).OrderBy(w => w.Nombre);
                return Ok(PlantillaFase);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las plantillas WhatsApp
        /// </summary>
        /// <returns> Información de Plantillas WhatsApp </returns>
        /// <returns> Lista Objeto DTO : List<PlantillaWhatsAppDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenterPlantillaWhatsApp()
        {
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
                var PlantillaFase = _repPlantillaClaveValor.ObtenterPlantillaWhatsApp().OrderBy(w => w.Nombre);
                return Ok(PlantillaFase);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: Gian Miranda
        /// Fecha: 10/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las plantillas Sms
        /// </summary>
        /// <returns> Lista de objetos de clase SmsPlantillaClaveValorDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerPlantillaSms()
        {
            try
            {
                var _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();

                return Ok(_repPlantillaClaveValor.ObtenerPlantillaSmsMarketing());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las plantillas WhatsApp Operaciones
        /// </summary>
        /// <returns> Información de Plantillas WhatsApp Operaciones </returns>
        /// <returns> Lista Objeto DTO : List<PlantillaWhatsAppDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenterPlantillaWhatsAppOperaciones()
        {
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
                return Ok(_repPlantillaClaveValor.ObtenterPlantillaWhatsAppOperaciones().OrderBy(w => w.Nombre));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las plantillas WhatsApp Docentes
        /// </summary>
        /// <returns> Información de Plantillas WhatsApp Docentes </returns>
        /// <returns> Lista Objeto DTO : List<PlantillaWhatsAppDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerPlantillaWhatsAppDocentes()
        {
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
                return Ok(_repPlantillaClaveValor.ObtenerPlantillaWhatsAppDocentes().OrderBy(w => w.Nombre));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la Probabilidad del Sueldo del Alumno
        /// </summary>
        /// <returns> Información de Sueldo de Alumno </returns>
        /// <returns> Objeto DTO : SueldoAlumnoDTO </returns>
        [Route("[action]/{idOportunidad}/{idPais}")]
        [HttpGet]
        public IActionResult ProbabilidadSueldo(int idOportunidad, int idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                var CuotaMensual = _repAlumno.ObtenerProbabilidadSueldo(idOportunidad, idPais);
                return Ok(new { CuotaMensual });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Fecha y Hora de Reprogramación Automática
        /// </summary>
        /// <returns> Fecha y Hora de Reprogramación Automática </returns>
        /// <returns> String </returns>
        [Route("[action]/{IdOportunidad}/{CodigoFase}/{IdOcurrencia}")]
        [HttpGet]
        public IActionResult ObtenerFechaHoraActividadReprogramacionAutomatica(int IdOportunidad, string CodigoFase, int IdOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var datosOportunidad = _repOportunidad.ObtenerDatosParaReprogramcionAutomatica(IdOportunidad);

                PersonalHorarioBO PersonalHorario = new PersonalHorarioBO(_integraDBContext)
                {
                    IdPersonal = datosOportunidad.IdPersonalAsignado
                };

                HoraReprogramacionAutomaticaBO HorarioReprogramacion = new HoraReprogramacionAutomaticaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    CodigoFase = CodigoFase,
                    IdOcurrencia = IdOcurrencia,
                    IdPersonal = datosOportunidad.IdPersonalAsignado,
                    IdActividadCabecera = datosOportunidad.IdActividadCabeceraUltima,
                    IdCategoriaOrigen = datosOportunidad.IdCategoriaOrigen,
                    IdTipoDato = datosOportunidad.IdTipoDato,
                    personalHorario = PersonalHorario.GetHorarioAsTable()
                };

                var respuesta = HorarioReprogramacion.ObtenerFechaHoraActividadReprogramacionAutomatica();
                return Ok(respuesta);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Fecha de Reprogramación Automática
        /// </summary>
        /// <returns> Fecha y de Reprogramación Automática </returns>
        /// <returns> String </returns>
        [Route("[action]/{IdOportunidad}/{taboperaciones}")]
        [HttpGet]
        public IActionResult ObtenerFechaReprogramacionAutomatica(int IdOportunidad, int taboperaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var datosOportunidad = _repOportunidad.ObtenerDatosParaReprogramcionAutomatica(IdOportunidad);
                PersonalHorarioBO PersonalHorario = new PersonalHorarioBO(_integraDBContext)
                {
                    IdPersonal = datosOportunidad.IdPersonalAsignado
                };

                HoraReprogramacionAutomaticaBO HorarioReprogramacion = new HoraReprogramacionAutomaticaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    IdPersonal = datosOportunidad.IdPersonalAsignado,
                    IdActividadCabecera = datosOportunidad.IdActividadCabeceraUltima,
                    IdCategoriaOrigen = datosOportunidad.IdCategoriaOrigen,
                    IdTipoDato = datosOportunidad.IdTipoDato,
                    personalHorario = PersonalHorario.GetHorarioAsTable()
                };

                var respuesta = HorarioReprogramacion.ObtenerFechaHoraReprogramacionAutomaticaOperaciones(taboperaciones);

                return Ok(respuesta);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Fecha de Programación Ejecutada
        /// </summary>
        /// <returns> Fecha de Programación Ejecutada </returns>
        /// <returns> Objeto BO : HoraReprogramacionAutomaticaBO </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public IActionResult ObtenerFechaReprogramacionEjecutada(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var datosOportunidad = _repOportunidad.ObtenerCasosReprogramacionManualOperaciones(IdOportunidad);
                var Oportunidad = _repOportunidad.ObtenerDatosParaReprogramcionAutomatica(IdOportunidad);

                if (datosOportunidad.FechaProximaCuota != null)
                {
                    datosOportunidad.FechaProximaCuotaTexto = datosOportunidad.FechaProximaCuota.Value.Year + "/" + datosOportunidad.FechaProximaCuota.Value.Month + "/" + datosOportunidad.FechaProximaCuota.Value.Day + " " + datosOportunidad.FechaProximaCuota.Value.Hour + ":" + datosOportunidad.FechaProximaCuota.Value.Minute + ":" + datosOportunidad.FechaProximaCuota.Value.Second;
                }
                //obtener el horario del personal
                PersonalHorarioBO PersonalHorario = new PersonalHorarioBO(_integraDBContext)
                {
                    IdPersonal = Oportunidad.IdPersonalAsignado
                };
                HoraReprogramacionAutomaticaBO HorarioReprogramacion = new HoraReprogramacionAutomaticaBO(_integraDBContext)
                {
                    personalHorario = PersonalHorario.GetHorarioAsTable(),
                    IdPersonal = Oportunidad.IdPersonalAsignado
                };
                //fin obtener el horario del personal
                datosOportunidad.personalHorario = HorarioReprogramacion.personalHorario;

                return Ok(new { records = datosOportunidad });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Información Usuario y Fecha Modificación de Alumno
        /// </summary>
        /// <returns> Vacío </returns>
        /// <returns> Vacío </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult crearOportunidadSinValidarOportunidadEnSeguimientoActualizarAlumno([FromBody] OportunidadDTO datosOportunidad, AlumnoHoraDTO alumnoHoraDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (datosOportunidad.UltimaFechaProgramada != null)
                {
                    datosOportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }
                else
                {
                    datosOportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                }

                AlumnoRepositorio _alumnoRepositorio = new AlumnoRepositorio(_integraDBContext);

                AlumnoBO alumnoBO = new AlumnoBO(datosOportunidad.IdAlumno)
                {
                    HoraContacto = alumnoHoraDTO.HoraContacto,
                    HoraPeru = alumnoHoraDTO.HoraPeru
                };

                if (alumnoBO.existeContacto())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        alumnoBO.ValidarEstadoContactoWhatsAppTemporal(_integraDBContext);
                        alumnoBO.FechaModificacion = DateTime.Now;
                        alumnoBO.UsuarioModificacion = alumnoHoraDTO.Usuario;
                        _alumnoRepositorio.Update(alumnoBO);
                        //Crear Oportunidad
                        scope.Complete();

                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Envía mensaje de texto a Alumno
        /// </summary>
        /// <returns> Confirmación de envío </returns>
        /// <returns> Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarMensajeTexto([FromBody] AgendaMensajeTextoDTO Mensaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                MensajeTextoRepositorio _repMensajeTexto = new MensajeTextoRepositorio();
                var datosAlumno = _repAlumno.ObtenerCelularCodigoPais(Mensaje.IdAlumno);
                var codigoMatricula = _repMensajeTexto.ObtenerCodigoMatricula(Mensaje.IdOportunidad);
                var Accesos = _repMensajeTexto.ObtenerAccesosPorEmail(Mensaje.IdAlumno);

                MensajeTextoBO MensajeTexto = new MensajeTextoBO();
                MensajeTexto.IdOportunidad = Mensaje.IdOportunidad;
                MensajeTexto.CodigoPais = datosAlumno.IdCodigoPais.Value;
                MensajeTexto.IdMatriculaCabecera = codigoMatricula.CodigoMatricula;
                MensajeTexto.Numero = datosAlumno.Celular;
                MensajeTexto.origenAgenda = true;
                MensajeTexto.UserName = Accesos.UserName;
                MensajeTexto.Clave = Accesos.Clave;
                if (!MensajeTexto.HasErrors)
                {

                    var idEnvio = MensajeTexto.EnviarMensaje();
                    MensajeTexto.IdSeguimientoTwilio = idEnvio;
                    MensajeTexto.FechaCreacion = DateTime.Now;
                    MensajeTexto.FechaModificacion = DateTime.Now;
                    MensajeTexto.UsuarioCreacion = Mensaje.Usuario;
                    MensajeTexto.UsuarioModificacion = Mensaje.Usuario;
                    MensajeTexto.Estado = true;
                    _repMensajeTexto.Insert(MensajeTexto);

                    return Ok(true);
                }
                else
                {
                    return BadRequest(MensajeTexto.GetErrors(null));
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el registro de configuración de contacto de acuerdo al IdTipoDato.
        /// </summary>
        /// <returns> Confirmación de envío </returns>
        /// <returns> objeto DTO : ContactoConfiguracionDTO </returns>
        [Route("[Action]/{IdTipoDato}")]
        [HttpGet]
        public ActionResult GetConfiguracionContactos(int IdTipoDato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ContactoConfiguracionRepositorio _referenciaConfiguracionRepositorio = new ContactoConfiguracionRepositorio();
                return Ok(_referenciaConfiguracionRepositorio.GetConfiguracionContactos(IdTipoDato));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el primer registro de configuración de contacto de acuerdo al IdTipoDato.
        /// </summary>
        /// <returns> Confirmación de envío </returns>
        /// <returns> objeto DTO : ReferidoConfiguracionDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult GetConfiguracionReferidos()
        {
            try
            {
                ReferidoConfiguracionRepositorio _referenciaConfiguracionRepositorio = new ReferidoConfiguracionRepositorio();
                return Ok(_referenciaConfiguracionRepositorio.GetConfiguracionReferidos());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Empresas Id, Nombre AutoComplete
        /// </summary>
        /// <returns> Información de Empresas Id, Nombre </returns>
        /// <returns> objeto DTO : List<EmpresaFiltroDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetEmpresasAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                EmpresaRepositorio _empresaRepositorio = new EmpresaRepositorio(_integraDBContext);
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(_empresaRepositorio.CargarEmpresasAutoComplete(Filtros["Valor"].ToString()));
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Personal Id, Nombre AutoComplete
        /// </summary>
        /// <returns> Información de Personal Id, Nombre </returns>
        /// <returns> objeto DTO : List<PersonalAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetPersonalAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                PersonalRepositorio _personalRepositorio = new PersonalRepositorio(_integraDBContext);
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(_personalRepositorio.CargarPersonalAutoComplete(Filtros["Valor"].ToString()));
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Información de Tiempo de Capacitación
        /// </summary>
        /// <returns> Confirmación de Actualización </returns>
        /// <returns> Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarTiempoCapacitacion([FromBody] OportunidadTiempoCapacitacionDTO oportunidadTiempoCapacitacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio(_integraDBContext);
                OportunidadBO oportunidad = oportunidadRepositorio.FirstById(oportunidadTiempoCapacitacionDTO.Id);
                oportunidad.IdTiempoCapacitacionValidacion = oportunidadTiempoCapacitacionDTO.IdTiempoCapacitacionValidacion ?? 0;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.UsuarioModificacion = oportunidadTiempoCapacitacionDTO.Usuario;
                oportunidadRepositorio.Update(oportunidad);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Campaña Mailing Etiqueta Horarios
        /// </summary>
        /// <returns> Etiqueta Horarios de Campaña Mailing </returns>
        /// <returns> Lita de Objeto DTO : List<CampaniaMailingDetalleContenidoEtiquetaDTO> </returns>
        [Route("[Action]/{IdCampaniaMailingDetalle}")]
        [HttpGet]
        public ActionResult TestCampania(int IdCampaniaMailingDetalle)
        {
            try
            {
                CampaniaMailingDetalleRepositorio _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio();
                //return Ok(_repCampaniaMailingDetalle.ObtenerExpositoresPorProgramaGeneral(IdCampaniaMailingDetalle));
                CampaniaMailingDetalleProgramaRepositorio campaniaMailingDetalleProgramaRepositorio = new CampaniaMailingDetalleProgramaRepositorio();
                var listasPGeneral = campaniaMailingDetalleProgramaRepositorio.GetBy(x => x.IdCampaniaMailingDetalle == IdCampaniaMailingDetalle && x.Tipo != "Filtro")
                    .Select(x =>
                       new { IdPGeneral = x.IdPgeneral, Etiqueta = string.Concat("*|PGDURACIONHORARIOS_PP", x.Orden, "|*"), Contenido = "" }
                    ).ToList().Distinct();
                CampaniaMailingDetalleBO _campania = new CampaniaMailingDetalleBO();

                List<CampaniaMailingDetalleContenidoEtiquetaDTO> detalle = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                foreach (var programaGeneral in listasPGeneral)
                {
                    detalle.Add(new CampaniaMailingDetalleContenidoEtiquetaDTO()
                    {
                        Etiqueta = programaGeneral.Etiqueta,
                        Contenido = _campania.GetContenidoHorarios(programaGeneral.IdPGeneral)
                    });
                }
                return Ok(detalle);

                //
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Días sin Contacto por Oportunidad
        /// </summary>
        /// <returns> Cantidad de días sin contacto </returns>
        /// <returns> int </returns>
        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerDiasSinCotacto(int IdOportunidad)
        {
            try
            {
                OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);

                var fecha = _repOportunidadLog.ObtenerFechasSinCotacto(IdOportunidad);

                if (fecha.Count() == 0)
                {
                    return Ok(0);
                }
                else
                {
                    //var fechaactual = fecha.Select(w => w.Fecha_Log == DateTime.Now).FirstOrDefault();
                    fecha.RemoveAll(w => w.Fecha_Log == DateTime.Now.Date);
                    return Ok(fecha.Count());
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Estado Matriculado de Alumno por Id
        /// </summary>
        /// <returns> Estado Matriculado de Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<EstadoMatriculadoDTO> </returns>
        [Route("[Action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerEstadoMatriculado(int IdAlumno)
        {
            try
            {
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                List<EstadoMatriculadoDTO> estados = new List<EstadoMatriculadoDTO>();
                EstadoMatriculadoDTO estadoIndividual = new EstadoMatriculadoDTO();
                MoodleCronogramaEvaluacionBO evaluacionesBO = new MoodleCronogramaEvaluacionBO();

                estados = _repMatriculaCabecera.ObtenerEstadoMatriculado(IdAlumno);
                if (estados.Count() == 0)
                {
                    var matriculas = _repMatriculaCabecera.ObtenerMatriculaPorAlumno(IdAlumno);
                    estados = new List<EstadoMatriculadoDTO>();
                    foreach (var matricula in matriculas)
                    {
                        //estadoIndividual.IdCentroCosto = matricula.Id;
                        estadoIndividual = _repMatriculaCabecera.ObtenerEstadoEvaluacion(matricula.CodigoMatricula);
                        estadoIndividual.IdMatriculaCabecera = matricula.Id;
                        estadoIndividual.CodigoMatricula = matricula.CodigoMatricula;
                        estadoIndividual.NroCuota = 0;
                        estadoIndividual.NroSubCuota = 0;
                        estadoIndividual.VersionPrograma = matricula.VersionPrograma;
                        estadoIndividual.IdCentroCosto = matricula.IdCentroCosto ?? default(int);
                        estadoIndividual.Documentos = matricula.Documentos;
                        estadoIndividual.NombreProgramaGeneral = matricula.NombreProgramaGeneral;

                        estadoIndividual.EstadoFinanciero = $@" <strong> Pago Completo </strong>";
                        var resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersion(estadoIndividual.IdMatriculaCabecera);
                        if (resultado.Count > 0)
                        {
                            string estadoEvaluacion = "";
                            var ultimaEvaluacionRealizadas = resultado.Where(w => w.Nota != null && w.FechaRendicion != null).OrderByDescending(w => w.FechaCronograma).FirstOrDefault();
                            var CantidadEvaluaciones = resultado.Where(w => w.Nota == null && w.FechaRendicion == null).OrderBy(w => w.FechaCronograma).ToList();
                            if (CantidadEvaluaciones.Count > 0)
                            {
                                if (CantidadEvaluaciones.FirstOrDefault().FechaCronograma >= DateTime.Now)
                                {
                                    if (ultimaEvaluacionRealizadas != null)
                                    {
                                        if (ultimaEvaluacionRealizadas.FechaCronograma > DateTime.Now)
                                        {
                                            estadoEvaluacion = $@" <strong>Evaluacion Adelantado </strong>
                                                <ul>                                                     
                                                ";
                                        }
                                        else
                                        {
                                            estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                        }

                                    }
                                    else
                                    {
                                        estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                    }
                                }
                                else
                                {
                                    estadoEvaluacion = $@" <strong>Evaluacion Atrasado </strong>
                                                <ul>                                                     
                                                ";
                                }

                                foreach (var Evaluacion in CantidadEvaluaciones)
                                {
                                    var fecha = Evaluacion.FechaCronograma == null ? null : Evaluacion.FechaCronograma.Value.ToString("dd/M/yyyy");
                                    estadoEvaluacion = estadoEvaluacion + $@"<li> { Evaluacion.NombreEvaluacion } : {fecha} </li>";
                                }
                                estadoEvaluacion = estadoEvaluacion + "</ul>";
                                estadoIndividual.EstadoEvaluacion = estadoEvaluacion;

                            }
                            else
                            {
                                estadoIndividual.EstadoEvaluacion = $@" <strong>Evaluacion Finalizada </strong>";
                            }
                        }
                        else
                        {
                            estadoIndividual.EstadoEvaluacion = $@" <strong>Evaluacion Sin definir </strong>";
                        }

                        estados.Add(estadoIndividual);
                    }
                }
                else
                {
                    var matriculas = _repMatriculaCabecera.ObtenerMatriculaPorAlumno(IdAlumno);
                    foreach (var matriculaAlumno in matriculas)
                    {
                        var culminado = estados.Where(w => w.CodigoMatricula == matriculaAlumno.CodigoMatricula);
                        if (culminado.Count() == 0)
                        {
                            estadoIndividual = _repMatriculaCabecera.ObtenerEstadoEvaluacion(matriculaAlumno.CodigoMatricula);
                            estadoIndividual.IdMatriculaCabecera = matriculaAlumno.Id;
                            estadoIndividual.CodigoMatricula = matriculaAlumno.CodigoMatricula;
                            estadoIndividual.NroCuota = 0;
                            estadoIndividual.NroSubCuota = 0;
                            estadoIndividual.VersionPrograma = matriculaAlumno.VersionPrograma;
                            estadoIndividual.IdCentroCosto = matriculaAlumno.IdCentroCosto ?? default(int);
                            estadoIndividual.Documentos = matriculaAlumno.Documentos;
                            estadoIndividual.NombreProgramaGeneral = matriculaAlumno.NombreProgramaGeneral;

                            estadoIndividual.EstadoFinanciero = $@" <strong> Pago Completo </strong>";
                            var resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersion(estadoIndividual.IdMatriculaCabecera);
                            if (resultado.Count > 0)
                            {
                                string estadoEvaluacion = "";
                                var ultimaEvaluacionRealizadas = resultado.Where(w => w.Nota != null && w.FechaRendicion != null).OrderByDescending(w => w.FechaCronograma).FirstOrDefault();
                                var CantidadEvaluaciones = resultado.Where(w => w.Nota == null && w.FechaRendicion == null).OrderBy(w => w.FechaCronograma).ToList();
                                if (CantidadEvaluaciones.Count > 0)
                                {
                                    if (CantidadEvaluaciones.FirstOrDefault().FechaCronograma >= DateTime.Now)
                                    {
                                        if (ultimaEvaluacionRealizadas != null)
                                        {
                                            if (ultimaEvaluacionRealizadas.FechaCronograma > DateTime.Now)
                                            {
                                                estadoEvaluacion = $@" <strong>Evaluacion Adelantado </strong>
                                                <ul>                                                     
                                                ";
                                            }
                                            else
                                            {
                                                estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                            }

                                        }
                                        else
                                        {
                                            estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                        }
                                    }
                                    else
                                    {
                                        estadoEvaluacion = $@" <strong>Evaluacion Atrasado </strong>
                                                <ul>                                                     
                                                ";
                                    }

                                    foreach (var Evaluacion in CantidadEvaluaciones)
                                    {
                                        var fecha = Evaluacion.FechaCronograma == null ? null : Evaluacion.FechaCronograma.Value.ToString("dd/M/yyyy");
                                        estadoEvaluacion = estadoEvaluacion + $@"<li> { Evaluacion.NombreEvaluacion } : {fecha} </li>";
                                    }
                                    estadoEvaluacion = estadoEvaluacion + "</ul>";
                                    estadoIndividual.EstadoEvaluacion = estadoEvaluacion;

                                }
                                else
                                {
                                    estadoIndividual.EstadoEvaluacion = $@" <strong>Evaluacion Finalizada </strong>";
                                }
                            }
                            else
                            {
                                estadoIndividual.EstadoEvaluacion = $@" <strong>Evaluacion Sin definir </strong>";
                            }

                            estados.Add(estadoIndividual);
                        }
                        else
                        {
                            foreach (var item in estados)
                            {
                                if (item.EstadoFinanciero == "Cuota Vencida")
                                {
                                    string estadoFinanciero = "";
                                    var cuotasRestantes = _repCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzas(item.Version, item.IdMatriculaCabecera);
                                    cuotasRestantes = cuotasRestantes.Where(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    //var cuotasRestantes = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Cancelado==false && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    if (cuotasRestantes.Count() == 1)
                                    {
                                        estadoFinanciero = $@" <strong>Cuota Vencida :</strong> 
                                                <ul> 
                                                    <li> { item.TipoCuota } {item.NroCuota} : {item.FechaVencimiento.ToString("dd/M/yyyy")} </li>
                                                </ul>
                                                ";
                                        item.EstadoFinanciero = estadoFinanciero;

                                    }
                                    else
                                    {
                                        estadoFinanciero = $@" <strong>Cuotas Vencida :</strong> 
                                                <ul>                                                     
                                                ";
                                        foreach (var cuota in cuotasRestantes)
                                        {
                                            estadoFinanciero = estadoFinanciero + $@"<li> { cuota.TipoCuota } {cuota.NroCuota} : {cuota.FechaVencimiento.Value.ToString("dd/M/yyyy")} </li>";
                                        }
                                        estadoFinanciero = estadoFinanciero + "</ul>";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }

                                }
                                else if (item.EstadoFinanciero == "Cuota Adelantada")
                                {
                                    string estadoFinanciero = "";
                                    var cuotasRestantes = _repCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzas(item.Version, item.IdMatriculaCabecera);
                                    cuotasRestantes = cuotasRestantes.Where(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    //var cuotasRestantes = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Cancelado == false && w.Version == item.Version).OrderBy(w => w.NroCuota);
                                    if (cuotasRestantes.Count() == 1)
                                    {
                                        estadoFinanciero = $@" <strong>Cuota Adelantada :</strong> 
                                                <ul> 
                                                    <li> { item.TipoCuota } {item.NroCuota} : {item.FechaVencimiento.ToString("dd/M/yyyy")} </li>
                                                </ul>
                                                ";
                                        item.EstadoFinanciero = estadoFinanciero;

                                    }
                                    else
                                    {
                                        estadoFinanciero = $@" <strong>Cuotas Adelantada :</strong> 
                                                <ul>                                                     
                                                ";
                                        foreach (var cuota in cuotasRestantes)
                                        {
                                            estadoFinanciero = estadoFinanciero + $@"<li> { cuota.TipoCuota } {cuota.NroCuota} : {cuota.FechaVencimiento.Value.ToString("dd/M/yyyy")} </li>";
                                        }
                                        estadoFinanciero = estadoFinanciero + "</ul>";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }
                                }
                                else if ((item.EstadoFinanciero == "Cuota Al Dia"))
                                {
                                    string estadoFinanciero = "";
                                    var cuotasRestantes = _repCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzas(item.Version, item.IdMatriculaCabecera);
                                    cuotasRestantes = cuotasRestantes.Where(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    //var cuotasRestantes = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == item.IdMatriculaCabecera && w.NroCuota >= item.NroCuota && w.Version == item.Version).OrderBy(w => w.NroCuota).ToList();
                                    if (cuotasRestantes.Count() == 1)
                                    {
                                        estadoFinanciero = $@" <strong>Cuota Al Dia :</strong> 
                                                <ul> 
                                                    <li> { item.TipoCuota } {item.NroCuota} : {item.FechaVencimiento.ToString("dd/M/yyyy")} </li>
                                                </ul>
                                                ";
                                        item.EstadoFinanciero = estadoFinanciero;

                                    }
                                    else
                                    {
                                        estadoFinanciero = $@" <strong>Cuota Al Dia :</strong> 
                                                <ul>                                                     
                                                ";
                                        foreach (var cuota in cuotasRestantes)
                                        {
                                            estadoFinanciero = estadoFinanciero + $@"<li> { cuota.TipoCuota } {cuota.NroCuota} : {cuota.FechaVencimiento.Value.ToString("dd/M/yyyy")} </li>";
                                        }
                                        estadoFinanciero = estadoFinanciero + "</ul>";
                                        item.EstadoFinanciero = estadoFinanciero;
                                    }
                                }
                                else
                                {
                                    item.EstadoFinanciero = $@" <strong>{item.EstadoFinanciero}</strong>";

                                }

                                var resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersion(item.IdMatriculaCabecera);
                                if (resultado.Count > 0)
                                {
                                    string estadoEvaluacion = "";
                                    var ultimaEvaluacionRealizadas = resultado.Where(w => w.Nota != null && w.FechaRendicion != null).OrderByDescending(w => w.FechaCronograma).FirstOrDefault();
                                    var CantidadEvaluaciones = resultado.Where(w => w.Nota == null && w.FechaRendicion == null).OrderBy(w => w.FechaCronograma).ToList();
                                    if (CantidadEvaluaciones.Count > 0)
                                    {
                                        if (CantidadEvaluaciones.FirstOrDefault().FechaCronograma >= DateTime.Now)
                                        {
                                            if (ultimaEvaluacionRealizadas != null)
                                            {
                                                if (ultimaEvaluacionRealizadas.FechaCronograma > DateTime.Now)
                                                {
                                                    estadoEvaluacion = $@" <strong>Evaluacion Adelantado </strong>
                                                <ul>                                                     
                                                ";
                                                }
                                                else
                                                {
                                                    estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                                }

                                            }
                                            else
                                            {
                                                estadoEvaluacion = $@" <strong>Evaluacion Al Dia </strong>
                                                <ul>                                                     
                                                ";
                                            }
                                        }
                                        else
                                        {
                                            estadoEvaluacion = $@" <strong>Evaluacion Atrasado </strong>
                                                <ul>                                                     
                                                ";
                                        }

                                        foreach (var Evaluacion in CantidadEvaluaciones)
                                        {
                                            var fecha = Evaluacion.FechaCronograma == null ? null : Evaluacion.FechaCronograma.Value.ToString("dd/M/yyyy");
                                            estadoEvaluacion = estadoEvaluacion + $@"<li> { Evaluacion.NombreEvaluacion } : {fecha} </li>";
                                        }
                                        estadoEvaluacion = estadoEvaluacion + "</ul>";
                                        item.EstadoEvaluacion = estadoEvaluacion;

                                    }
                                    else
                                    {
                                        item.EstadoEvaluacion = $@" <strong>Evaluacion Finalizada </strong>";
                                    }
                                }
                                else
                                {
                                    item.EstadoEvaluacion = $@" <strong>Evaluacion Sin definir </strong>";
                                }

                            }
                        }


                    }

                }

                return Ok(estados);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Costos Administrativos por IdMatriculaCabecera
        /// </summary>
        /// <returns> Lista de Costos Administrativos </returns>
        /// <returns> Lista de Objeto DTO : List<CostosAdministrativosDTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCostosAdministrativos(int IdMatriculaCabecera)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var CostosAdministrativos = _repMatriculaCabecera.ObtenerCostosAdministrativos(IdMatriculaCabecera);

                return Ok(CostosAdministrativos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Costos Administrativos por Codigo de Matricula
        /// </summary>
        /// <returns> Lista de Costos Administrativos </returns>
        /// <returns> Lista de Objeto DTO : List<CostosAdministrativosDTO> </returns>
        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCostosAdministrativosCodigoMatricula(string CodigoMatricula)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var MatriculaTemporal = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == CodigoMatricula).FirstOrDefault();

                if (MatriculaTemporal == null)
                {
                    return BadRequest("No existe Codigo de Matricula con Costos Administrativos");
                }
                else
                {
                    var CostosAdministrativos = _repMatriculaCabecera.ObtenerCostosAdministrativos(MatriculaTemporal.Id);

                    return Ok(CostosAdministrativos);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Lista de Cursos Moodle por Codigo de Matricula
        /// </summary>
        /// <returns> Lista de Cursos Moodle </returns>
        /// <returns> Lista de Objeto DTO : List<CursoMoodleDTO> </returns>
        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCrongramaMoodle(string CodigoMatricula)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var CursoMoodle = _repMatriculaCabecera.ObtenerCursoMoodle(CodigoMatricula);

                return Ok(CursoMoodle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comentarios de operaciones por tipo
        /// </summary>
        /// <returns> Lista de Comentarios </returns>
        /// <returns> Lista de Objeto DTO : List<ObtenerSeguimientoAlunoComentarioDTO>
        [Route("[Action]/{IdOportunidad}/{IdTipoSeguimientoAlumnoCategoria}")]
        [HttpGet]
        public ActionResult ObtenerComentarioOperaciones(int IdOportunidad, int IdTipoSeguimientoAlumnoCategoria)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var Comentario = _repOportunidad.ObtenerComentariosOperaciones(IdOportunidad, IdTipoSeguimientoAlumnoCategoria);
                return Ok(Comentario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cronograma de Autoevaluaciones
        /// </summary>
        /// <returns> Cronograma de Autoevaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionDTO>
        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerCrongramaEvaluaciones(int IdOportunidad)
        {
            try
            {
                MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                List<CronogramaAutoEvaluacionDTO> resultado = new List<CronogramaAutoEvaluacionDTO>();
                MatriculaCabeceraBO matricula = new MatriculaCabeceraBO();

                var ExisteCronograma = _repMontoPagoCronograma.GetBy(w => w.IdOportunidad == IdOportunidad).FirstOrDefault();
                if (ExisteCronograma == null || _repMatriculaCabecera.FirstBy(w => w.CodigoMatricula == ExisteCronograma.CodigoMatricula) == null)
                {
                    var a = _repOportunidad.ObtenerDatosOportunidad(IdOportunidad);
                    matricula = _repMatriculaCabecera.FirstBy(w => w.IdPespecifico == a.IdPespecifico && w.IdAlumno == a.IdAlumno);
                }
                else
                {
                    matricula = _repMatriculaCabecera.FirstBy(w => w.CodigoMatricula == ExisteCronograma.CodigoMatricula);
                }


                if (matricula != null)
                    resultado = _repMatriculaCabecera.ObtenerCrongramaAutoEvaluaciones(matricula.CodigoMatricula);


                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cronograma de Autoevaluaciones por Código de Matrícula
        /// </summary>
        /// <returns> Cronograma de Autoevaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionDTO>
        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionesPorMatricula(string CodigoMatricula)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                List<CronogramaAutoEvaluacionDTO> resultado = new List<CronogramaAutoEvaluacionDTO>();
                MatriculaCabeceraBO matricula = new MatriculaCabeceraBO();


                resultado = _repMatriculaCabecera.ObtenerCrongramaAutoEvaluaciones(CodigoMatricula);


                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cronograma de Autoevaluaciones por IdMatriculaCabecera
        /// </summary>
        /// <returns> Cronograma de Autoevaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionV2DTO>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionesPorMatriculaV2(int IdMatriculaCabecera)
        {
            try
            {
                List<CronogramaAutoEvaluacionV2DTO> resultado = new List<CronogramaAutoEvaluacionV2DTO>();

                MoodleCronogramaEvaluacionBO evaluacionesBO = new MoodleCronogramaEvaluacionBO();
                resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersion(IdMatriculaCabecera);

                var versiones = evaluacionesBO.ObtenerVersionesCronograma(IdMatriculaCabecera);

                //return Ok(resultado)
                return Ok(new { Versiones = versiones, CronogramaUltimaVersion = resultado });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cronograma de Evaluaciones Aonline por IdAlumno
        /// </summary>
        /// <returns> cronograma de Evaluaciones Aonline </returns>
        /// <returns> objeto Agrupado  </returns>
        [Route("[Action]/{IdAlumno}")]
        [HttpPost]
        public ActionResult ObtenerEvaluacionesAONLINEPorIdAlumno(int IdAlumno)
        {
            try
            {
                List<CronogramaAutoEvaluacionV2DTO> resultado = new List<CronogramaAutoEvaluacionV2DTO>();
                MoodleCronogramaEvaluacionRepositorio evaluacionRep = new MoodleCronogramaEvaluacionRepositorio();

                List<ConsolidadoNotasDTO> listaCursosAonline = new List<ConsolidadoNotasDTO>();
                List<ConsolidadoNotasDTO> listaCursosPresencialOnline = new List<ConsolidadoNotasDTO>();
                //MoodleCronogramaEvaluacionBO evaluacionesBO = new MoodleCronogramaEvaluacionBO();
                resultado = evaluacionRep.ObtenerCronogramaAutoEvaluacion_UltimaVersionPortal(IdAlumno);

                listaCursosAonline = resultado.GroupBy(u => (u.NombreCurso, u.IdMatriculaCabecera, u.IdCursoMoodle))
                                    .Select(group =>
                                    new ConsolidadoNotasDTO
                                    {
                                        NombreCurso = group.Key.NombreCurso
                                    ,
                                        InicioCurso = group.Select(x => x.FechaCronograma).Min()
                                    ,
                                        FinCurso = group.Select(x => x.FechaCronograma).Max()
                                    ,
                                        IdMatriculaCabecera = Int32.Parse(group.Key.IdMatriculaCabecera ?? "0")
                                    ,
                                        IdCursoMoodle = group.Key.IdCursoMoodle
                                    ,
                                        Promedio = ((group.Where(x => x.NombreEvaluacion.ToUpper().Contains("AUTOEVALUACI")).Sum(x => x.Nota) / (group.Where(x => x.NombreEvaluacion.ToUpper().Contains("AUTOEVALUACI")).Count() == 0 ? 1 : group.Where(x => x.NombreEvaluacion.ToUpper().Contains("AUTOEVALUACI")).Count())) * (group.Where(x => x.NombreEvaluacion.ToUpper().Contains("AUTOEVALUACI")).Count() == 0 ? 1 : (group.Where(x => x.NombreEvaluacion.ToUpper().Contains("AUTOEVALUACI")).Count() == 1 ? 1 : 0.4M))) +
                                                ((group.Where(x => x.NombreEvaluacion.ToUpper().Contains("PROYECTO")).Sum(x => x.Nota) / (group.Where(x => x.NombreEvaluacion.ToUpper().Contains("PROYECTO")).Count() == 0 ? 1 : group.Where(x => x.NombreEvaluacion.ToUpper().Contains("PROYECTO")).Count())) * (group.Where(x => x.NombreEvaluacion.ToUpper().Contains("PROYECTO")).Count() == 0 ? 1 : (group.Where(x => x.NombreEvaluacion.ToUpper().Contains("PROYECTO")).Count() == 1 ? 1 : 0.6M)))
                                    ,
                                        Autoevaluaciones = group.Select(x => new CronogramalistaCursosOonlineV2DTO { NombreEvaluacion = x.NombreEvaluacion, FechaCronograma = x.FechaCronograma, FechaRendicion = x.FechaRendicion, Nota = x.Nota, IdCursoMoodle = x.IdCursoMoodle, Version = x.Version, Porcentaje = 1, IdEvaluacionMoodle = x.IdEvaluacionMoodle }).ToList()
                                    }).ToList();

                NotaRepositorio _repoNota = new NotaRepositorio();
                var listadoNotasPorIdAlumno = _repoNota.ListadoNotaPorIdAlumno(IdAlumno);

                EvaluacionEscalaCalificacionRepositorio _repoEscala = new EvaluacionEscalaCalificacionRepositorio();

                foreach (var item in listadoNotasPorIdAlumno)
                {
                    item.NombreEvaluacion = item.NombreEvaluacion == null ? "" : item.NombreEvaluacion;
                    if (item.NombreEvaluacion.ToUpper().Contains("ASISTENCIA"))
                    {
                        var escalaCalificacion = _repoEscala.ObtenerEscalaPorPEspecifico_Presencial(item.IdPEspecifico);
                        item.Nota = item.Nota * Convert.ToInt32(escalaCalificacion?.EscalaCalificacion ?? 0);
                    }
                    if (item.IdEvaluacion == null)
                    {
                        item.Nota = null;
                    }
                    item.Porcentaje = (item.Porcentaje ?? 0.0M) / 100.0M;
                    item.Nota = item.Nota ?? 0;
                }

                listaCursosPresencialOnline = listadoNotasPorIdAlumno.GroupBy(u => (u.NombrePEspecifico, u.IdMatriculaCabecera, u.IdPEspecifico))
                                    .Select(group =>
                                    new ConsolidadoNotasDTO
                                    {
                                        NombreCurso = group.Key.NombrePEspecifico
                                    ,
                                        InicioCurso = group.Select(x => x.FechaInicio).FirstOrDefault()
                                    ,
                                        FinCurso = group.Select(x => x.FechaFin).FirstOrDefault()
                                    ,
                                        IdMatriculaCabecera = group.Key.IdMatriculaCabecera
                                    ,
                                        IdCursoMoodle = group.Key.IdPEspecifico
                                    ,
                                        Promedio = group.Sum(x => (x.Nota * x.Porcentaje))
                                    ,
                                        Autoevaluaciones = group.Select(x => new CronogramalistaCursosOonlineV2DTO { NombreEvaluacion = x.NombreEvaluacion, FechaCronograma = x.FechaInicio, FechaRendicion = x.FechaInicio, Nota = x.Nota, IdCursoMoodle = x.IdPEspecifico, Version = 0 }).ToList()
                                    }).ToList();

                return Ok(new { CronogramaUltimaVersion = listaCursosAonline, CronogramaPresencial = listaCursosPresencialOnline });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cronograma de Evaluaciones por Id de MatriculaCabecera y Id de Curso Moodle
        /// </summary>
        /// <returns> cronograma de Evaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionV2DTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}/{IdCursoMoodle}")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionesPorMatriculaV3(int IdMatriculaCabecera, int IdCursoMoodle)
        {
            try
            {
                List<CronogramaAutoEvaluacionV2DTO> resultado = new List<CronogramaAutoEvaluacionV2DTO>();

                MoodleCronogramaEvaluacionBO evaluacionesBO = new MoodleCronogramaEvaluacionBO();
                resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersionPorCurso(IdMatriculaCabecera, IdCursoMoodle);

                var versiones = evaluacionesBO.ObtenerVersionesCronograma(IdMatriculaCabecera);

                //return Ok(resultado);
                return Ok(new { Versiones = versiones, CronogramaUltimaVersion = resultado });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene versiones de cronograma por Id de Matrícula Cabecera
        /// </summary>
        /// <returns> versiones de cronograma </returns>
        /// <returns> Lista de Objeto DTO : List<VersionCronogramaAutoEvaluacionDTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerVersionesCronogramaPorMatricula(int IdMatriculaCabecera)
        {
            try
            {
                MoodleCronogramaEvaluacionBO evaluacionesBO = new MoodleCronogramaEvaluacionBO();
                var versiones = evaluacionesBO.ObtenerVersionesCronograma(IdMatriculaCabecera);
                return Ok(versiones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Evaluaciones por Id de Matrícula Cabecera y Versión
        /// </summary>
        /// <returns> Evaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionV2DTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}/{Version}")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionesPorVersion(int IdMatriculaCabecera, int Version)
        {
            try
            {
                List<CronogramaAutoEvaluacionV2DTO> resultado = new List<CronogramaAutoEvaluacionV2DTO>();

                MoodleCronogramaEvaluacionBO evaluacionesBO = new MoodleCronogramaEvaluacionBO();
                resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionPorVersion(IdMatriculaCabecera, Version);

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Seguimiento de Alumnos y categorías
        /// </summary>
        /// <returns> Evaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionV2DTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerSeguimientoAlumnoCategoria()
        {
            try
            {
                SeguimientoAlumnoCategoriaRepositorio _repSeguimientoAlumnoCategoria = new SeguimientoAlumnoCategoriaRepositorio(_integraDBContext);
                return Ok(_repSeguimientoAlumnoCategoria.ObtenerSeguimientoAlumnoCategoria());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Tipo de Seguimiento de Alumnos
        /// </summary>
        /// <returns> Tipo de Seguimiento de Alumnos </returns>
        /// <returns> Lista de Objeto DTO : List<FiltroDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoSeguimientoAlumnoCategoria()
        {
            try
            {
                TipoSeguimientoAlumnoCategoriaRepositorio _repSeguimientoAlumnoCategoria = new TipoSeguimientoAlumnoCategoriaRepositorio(_integraDBContext);
                return Ok(_repSeguimientoAlumnoCategoria.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Programas Especificos asociados a una matricula
        /// </summary>
        /// <returns> Programas Especificos asociados a una matricula </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoPorMatricula(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PEspecificoMatriculaAlumnoRepositorio pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
                var _listaCursosMatriculados = pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(IdMatriculaCabecera);
                var _listadoNotasPorMatricula = _repoNota.ListadoNotaPorMatriculaCabeceraPromedio(IdMatriculaCabecera);

                if (_listadoNotasPorMatricula != null && _listadoNotasPorMatricula.Count() > 0)
                {
                    foreach (var item in _listaCursosMatriculados)
                    {
                        item.TipoPrograma = 1;
                        var datoNota = _listadoNotasPorMatricula.Where(x => x.IdPEspecifico == item.IdPEspecifico).FirstOrDefault();

                        if (datoNota != null)
                        {
                            item.IdMatriculaCabecera = IdMatriculaCabecera;
                            item.FechaFin = datoNota.FechaTermino;
                            item.FechaInicio = datoNota.FechaInicio;
                            item.Promedio = datoNota.Promedio;
                        }
                        else
                        {
                            item.IdMatriculaCabecera = IdMatriculaCabecera;
                            item.FechaFin = "-";
                            item.FechaInicio = "-";
                            item.Promedio = "0";
                        }

                    }
                }
                else
                {
                    MoodleCronogramaEvaluacionBO evaluacionesBO = new MoodleCronogramaEvaluacionBO();
                    var _resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersionPromedio(IdMatriculaCabecera);
                    for (int i = 0; i < _listaCursosMatriculados.Count; i++)
                    {
                        _listaCursosMatriculados[i].IdMatriculaCabecera = IdMatriculaCabecera;
                        _listaCursosMatriculados[i].TipoPrograma = 2;
                        if (i < _resultado.Count)
                        {
                            _listaCursosMatriculados[i].IdPEspecifico = _resultado[i].IdCursoMoodle;
                            _listaCursosMatriculados[i].FechaInicio = _resultado[i].FechaCronograma.Value.ToString("dd/MM/yyyy");
                            _listaCursosMatriculados[i].FechaInicio = _resultado[i].FechaRendicion != null ? _resultado[i].FechaRendicion.Value.ToString("dd/MM/yyyy") : "-";
                            _listaCursosMatriculados[i].Promedio = _resultado[i].Promedio != null ? _resultado[i].Promedio.Value.ToString("0") : "0";
                            _listaCursosMatriculados[i].Nombre = _resultado[i].NombreCurso;
                        }
                        else
                        {
                            _listaCursosMatriculados[i].FechaInicio = "-";
                            _listaCursosMatriculados[i].FechaInicio = "-";
                            _listaCursosMatriculados[i].Promedio = "0";
                        }


                    }
                }





                return Ok(_listaCursosMatriculados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Lista de Notas Promedio por Programa Específico
        /// </summary>
        /// <returns> Lista de Notas Promedio por Programa Específico </returns>
        /// <returns> Lista de Objeto DTO : List<NotaPresencialPromedioEspecificoDTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}/{IdEspecifico}/{TipoPrograma}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoPorMatriculaPorIdEspecifico(int IdMatriculaCabecera, int IdEspecifico, int TipoPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (TipoPrograma == 1)
                {
                    var _listadoNotasPorMatricula = _repoNota.ListadoNotaPorMatriculaCabeceraPromedioIdEspecifico(IdMatriculaCabecera, IdEspecifico);

                    return Ok(_listadoNotasPorMatricula);
                }
                else
                {
                    List<NotaPresencialPromedioEspecificoDTO> _datosRegistroDetalle = new List<NotaPresencialPromedioEspecificoDTO>();

                    MoodleCronogramaEvaluacionBO evaluacionesBO = new MoodleCronogramaEvaluacionBO();
                    var _resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersionPorCurso(IdMatriculaCabecera, IdEspecifico);

                    foreach (var item in _resultado)
                    {
                        NotaPresencialPromedioEspecificoDTO _dato = new NotaPresencialPromedioEspecificoDTO();
                        _dato.IdPEspecifico = IdEspecifico;
                        _dato.NombrePEspecifico = item.NombreEvaluacion;
                        _dato.Nota = item.Nota != null ? item.Nota.Value.ToString("0") : "0";
                        _dato.Promedio = item.Nota != null ? item.Nota.Value.ToString("0") : "0";
                        _dato.Porcentaje = "100%";
                        _datosRegistroDetalle.Add(_dato);
                    }

                    return Ok(_datosRegistroDetalle);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Lista de Programas Específicos Relacionados por Programa General
        /// </summary>
        /// <returns> Lista de Programas Específicos Relacionados </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoRelacionadoPorIdPGeneralDTO> </returns>
        [Route("[Action]/{IdPEspecifico}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoRelacionadoPorIdPGeneral(int IdPEspecifico, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                return Ok(pespecificoRepositorio.ObtenerPEspecificoRelacionadoPorIdPGeneral(IdPEspecifico, IdMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta Programa Específico por Matrícula y Envia Correo de Confirmación
        /// </summary>
        /// <returns> PEspecificos asociados a una matricula </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarPEspecificoMatriculaAlumnoRepositorio([FromBody] PEspecificoMatriculaAlumnoDTO pEspecificoMatriculaAlumnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            RespuestaWebDTO cronograma = new RespuestaWebDTO();
            MoodleCronogramaEvaluacionBO objetoCongelarCronograma = new MoodleCronogramaEvaluacionBO();
            MdlUser moodleUser = new MdlUser();

            try
            {
                AulaVirtualContext moodleContext = new AulaVirtualContext();
                MoodleWebService moodleWebService = new MoodleWebService();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio matriculaCabeceraRepositorio = new MatriculaCabeceraRepositorio(_integraDBContext);
                PEspecificoMatriculaAlumnoRepositorio pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
                PEspecificoMatriculaAlumnoRepositorio repActualizacionMatricula = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                MoodleCursoRepositorio moodleCursoRepositorio = new MoodleCursoRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);

                var matricula = matriculaCabeceraRepositorio.FirstById(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);
                List<PespecificoPadrePespecificoHijoDTO> listaPEspecificoPadrePespecificoHijo = new List<PespecificoPadrePespecificoHijoDTO>();
                var pespecifico = pespecificoRepositorio.FirstById(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                var codigoMatricula = matriculaCabeceraRepositorio.getCodigoMatricula(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);
                var nombreCursoRecuperacion = pespecificoRepositorio.getNombrePEspecifico(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                var nombreCursoAnterior = pespecificoRepositorio.getNombrePEspecifico(pEspecificoMatriculaAlumnoDTO.IdPEspecificoRecuperacion);
                var IdCursoMoodle = pEspecificoMatriculaAlumnoRepositorio.IdCursoMoodle(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                //var IdUsuarioMoodle = pEspecificoMatriculaAlumnoRepositorio.IdUsuarioMoodle(pEspecificoMatriculaAlumnoDTO.IdAlumno);//Aqui se recueperaba el usuario

                var oportunidad = _repOportunidad.FirstById(pEspecificoMatriculaAlumnoDTO.IdOportunidad);
                var pEspecificoNuevaAulaVirtual = pespecificoRepositorio.ObtenerPEspecificoNuevaAulaVirtual();

                //Codigo de Luis para crear Usuario Moodle
                var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                if (pEspecificoNuevaAulaVirtual.Exists(x => x.Id == pespecifico.Id))
                {
                    var matriculaCabecera = matriculaCabeceraRepositorio.FirstById(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);

                    repActualizacionMatricula.ActualizacionTipoMatriculaPEspecifico(pEspecificoMatriculaAlumnoDTO.IdPEspecificoRecuperacion, pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);
                    PEspecificoMatriculaAlumnoBO pEspecificoMatriculaAlumnoBO = new PEspecificoMatriculaAlumnoBO();
                    pEspecificoMatriculaAlumnoBO.IdMatriculaCabecera = pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera;
                    pEspecificoMatriculaAlumnoBO.IdPespecifico = pEspecificoMatriculaAlumnoDTO.IdPespecifico;
                    pEspecificoMatriculaAlumnoBO.IdPespecificoTipoMatricula = 2;
                    pEspecificoMatriculaAlumnoBO.Estado = true;
                    pEspecificoMatriculaAlumnoBO.AplicaNuevaAulaVirtual = true;
                    pEspecificoMatriculaAlumnoBO.IdCursoMoodle = IdCursoMoodle;
                    pEspecificoMatriculaAlumnoBO.IdUsuarioMoodle = Convert.ToInt32(moodleUser.Id);

                    if (pEspecificoMatriculaAlumnoDTO.Grupo == 0) pEspecificoMatriculaAlumnoBO.Grupo = 1;
                    else pEspecificoMatriculaAlumnoBO.Grupo = pEspecificoMatriculaAlumnoDTO.Grupo;

                    pEspecificoMatriculaAlumnoBO.UsuarioCreacion = pEspecificoMatriculaAlumnoDTO.Usuario;
                    pEspecificoMatriculaAlumnoBO.UsuarioModificacion = pEspecificoMatriculaAlumnoDTO.Usuario;
                    pEspecificoMatriculaAlumnoBO.FechaCreacion = DateTime.Now;
                    pEspecificoMatriculaAlumnoBO.FechaModificacion = DateTime.Now;

                    pEspecificoMatriculaAlumnoRepositorio.Insert(pEspecificoMatriculaAlumnoBO);

                    if (pespecifico.Tipo == "Online Asincronica")
                    {
                        return Ok(pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera));
                    }
                    else
                    {
                        //ENVIO CORREO   
                        //var oportunidad = _repOportunidad.FirstById(pEspecificoMatriculaAlumnoDTO.IdOportunidad);
                        var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                        //List<string> correosPersonalizados = new List<string>
                        //{
                        //    alumno.Email1
                        //};
                        List<string> correosPersonalizadosCopiaOculta = new List<string>
                        {
                            //"fvaldez@bsginstitute.com",
                            //"lhuallpa@bsginstitute.com",
                            //"controldeaccesos@bsginstitute.com",
                            //"pbeltran@bsginstitute.com",
                            //"bamontoya@bsginstitute.com",
                            //personal.Email
                            "aarcana@bsginsitute.com"
                        };

                        string mensaje = "Saludos. <br> Se matriculo al codigo:<strong> " + codigoMatricula + " </strong>del Programa Especifico Anterior:<strong> " + nombreCursoAnterior +
                            "</strong> al nuevo Programa Especifico:<strong> " + nombreCursoRecuperacion + "</strong><br>Atentamente.";

                        TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = personal.Email,
                            //Sender = "w.choque.itusaca@isur.edu.pe",
                            //Recipient = "aarcana@bsginsitute.com",
                            Recipient = "aarcana@bsginsitute.com",
                            Subject = "Recuperacion en Otra Modalidad",
                            Message = mensaje,
                            Cc = "",
                            //Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            Bcc = "",
                            //AttachedFiles = reemplazoEtiquetaPlantilla.EmailReemplazado.ListaArchivosAdjuntos,
                            //AttachedFiles = "";
                        };
                        var mailServie = new TMK_MailServiceImpl();
                        mailServie.SetData(mailDataPersonalizado);
                        mailServie.SendMessageTask();

                        return Ok(pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera));
                    }
                }
                else
                {
                    var accesos = _repAlumno.ObtenerAccesosInicialesMoodle(alumno.Id);//Verifica si tiene Accesos en Moodle
                    var username = string.Concat(alumno.Nombre1, alumno.ApellidoPaterno).ToLower();
                    moodleUser = moodleContext.MdlUser.Where(x => x.Email.Equals(alumno.Email1)).FirstOrDefault(); //Verifica si tiene un Usuario

                    string dni = "";
                    username = username.Replace(" ", "").ToLower();
                    username = QuitarAcentos(username);
                    if (alumno.Dni == "" || alumno.Dni == null)
                    {
                        dni = matricula.CodigoMatricula;
                    }
                    else
                    {
                        dni = alumno.Dni;
                    }
                    var password = string.Concat(dni, "Bs1");
                    if (accesos == null && moodleUser == null) // no tiene accesos
                    {
                        var moodlePorUsuario = moodleContext.MdlUser.Where(x => x.Username.Equals(username)).FirstOrDefault();
                        if (moodlePorUsuario != null)
                        {
                            if (!moodlePorUsuario.Email.Equals(alumno.Email1))
                            {
                                if (!string.IsNullOrEmpty(alumno.ApellidoMaterno))
                                {
                                    username = username + alumno.ApellidoMaterno.Substring(0, 2).ToLower().Trim();
                                }
                                else
                                {
                                    username = username + alumno.Nombre1.Substring(0, 2).ToLower().Trim();
                                }
                                username = QuitarAcentos(username);
                            }
                        }
                        if (alumno.IdCiudad.HasValue)
                        {
                            var ciudad = _repCiudad.FirstById(alumno.IdCiudad.Value);
                            var pais = _repPais.FirstById(ciudad.IdPais);

                            //Crear Usuario Moodle
                            MoodleWebServiceCrearUsuarioDTO usuarioNuevoMoodle = new MoodleWebServiceCrearUsuarioDTO();
                            usuarioNuevoMoodle.firstname = alumno.Nombre1;
                            usuarioNuevoMoodle.lastname = alumno.ApellidoPaterno;
                            usuarioNuevoMoodle.email = alumno.Email1;
                            usuarioNuevoMoodle.country = pais.CodigoPaisMoodle;
                            usuarioNuevoMoodle.city = ciudad.Nombre;
                            usuarioNuevoMoodle.username = username;
                            usuarioNuevoMoodle.password = password;
                            usuarioNuevoMoodle.auth = "manual";

                            var rpta = moodleWebService.CrearUsuario(usuarioNuevoMoodle);
                            if (!rpta.Estado)
                            {
                                return BadRequest(rpta.Mensaje);
                            }
                            else
                            {
                                moodleUser = moodleContext.MdlUser.Where(x => x.Email.Equals(alumno.Email1)).FirstOrDefault(); // obtenemos el objeto mdlUser insertado
                                pEspecificoMatriculaAlumnoRepositorio.InsertarTAlumno_Moodle(alumno.Id, moodleUser.Id.ToString(), username, password);
                                accesos = _repAlumno.ObtenerAccesosInicialesMoodle(alumno.Id);
                            }
                        }
                        else
                        {
                            return BadRequest("El alumno no tiene ciudad");
                        }
                    }
                    if (accesos == null && moodleUser != null) //Portal Web
                    {
                        var userPortal = matriculaCabeceraRepositorio.ObtenerDetalleAccesoPortalWebV4(matricula.Id);
                        if (userPortal != null && userPortal.Usuario != null)
                        {
                            if (userPortal.Usuario.Equals(moodleUser.Email))
                            {
                                pEspecificoMatriculaAlumnoRepositorio.InsertarTAlumno_Moodle(alumno.Id, moodleUser.Id.ToString(), moodleUser.Username, userPortal.Clave);
                            }
                            else
                            {
                                MoodleWebServiceActualizarClaveDTO moodleWebServiceActualizarClave = new MoodleWebServiceActualizarClaveDTO();
                                moodleWebServiceActualizarClave.IdMoodle = moodleUser.Id;
                                moodleWebServiceActualizarClave.Clave = password;

                                var actualizarClave = moodleWebService.ActualizarClaveMoodle(moodleWebServiceActualizarClave);
                                if (actualizarClave.Estado)
                                {
                                    pEspecificoMatriculaAlumnoRepositorio.InsertarTAlumno_Moodle(alumno.Id, moodleUser.Id.ToString(), moodleUser.Username, password);
                                }

                            }
                        }
                        else
                        {
                            MoodleWebServiceActualizarClaveDTO moodleWebServiceActualizarClave = new MoodleWebServiceActualizarClaveDTO();
                            moodleWebServiceActualizarClave.IdMoodle = moodleUser.Id;
                            moodleWebServiceActualizarClave.Clave = password;

                            var actualizarClave = moodleWebService.ActualizarClaveMoodle(moodleWebServiceActualizarClave);
                            if (actualizarClave.Estado)
                            {
                                pEspecificoMatriculaAlumnoRepositorio.InsertarTAlumno_Moodle(alumno.Id, moodleUser.Id.ToString(), moodleUser.Username, password);
                            }
                        }
                    }
                    if (accesos != null && moodleUser == null)
                    {
                        moodleUser = moodleContext.MdlUser.Where(x => x.Id == Convert.ToInt64(accesos.IdMoodle)).FirstOrDefault();
                    }

                    if (pespecifico.Tipo == "Online Asincronica")
                    {

                        var matriculaCabecera = matriculaCabeceraRepositorio.FirstById(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);

                        //var accesos = _repAlumno.ObtenerAccesosInicialesMoodle(matriculaCabecera.IdAlumno);
                        //if (accesos == null || accesos.UsuarioMoodle == null)
                        //{
                        //    return BadRequest("El alumno aún no tiene los accesos creados del aula virtual.");
                        //}
                        //var moodleAlumno = moodleContext.MdlUser.Where(x => x.Username == accesos.UsuarioMoodle).FirstOrDefault();

                        //if (moodleAlumno == null)
                        //{
                        //    //return BadRequest("El alumno aún no tiene los accesos creados del aula virtual.");
                        //}                    

                        if (pespecifico.IdCursoMoodle == null)
                        {
                            return BadRequest("El Programa seleccionado no tiene una relación con el Aula Virtual.");
                        }

                        int idCursoMoodlePRevio = (pespecifico.IdCursoMoodle ?? 0);
                        MoodleCursoBO moodleCurso = moodleCursoRepositorio.FirstBy(x => x.IdCursoMoodle == idCursoMoodlePRevio);

                        if (moodleCurso == null || moodleCurso.Id == 0)
                        {
                            return BadRequest("El Programa seleccionado no tiene una relación con el Aula Virtual.");
                        }

                        var idUMoodle = Convert.ToInt32(moodleUser.Id);

                        //LLAMA AL METODO DE ANSOLI 
                        cronograma = objetoCongelarCronograma.CongelarCrongrogramaRecuperacionEnAonline(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera, moodleCurso.IdCursoMoodle,
                            idUMoodle, moodleUser.Username);

                        if (cronograma.Estado == true)
                        {
                            MoodleWebServiceRegistrarMatriculaDTO moodleWebServiceRegistrarMatriculaDTO = new MoodleWebServiceRegistrarMatriculaDTO();
                            DateTime dia_unix = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            moodleWebServiceRegistrarMatriculaDTO.userid = Convert.ToInt32(moodleUser.Id);
                            moodleWebServiceRegistrarMatriculaDTO.courseid = moodleCurso.IdCursoMoodle;
                            moodleWebServiceRegistrarMatriculaDTO.roleid = 5;
                            moodleWebServiceRegistrarMatriculaDTO.timestart = (long)(DateTime.Now.ToUniversalTime() - dia_unix).TotalSeconds;
                            moodleWebServiceRegistrarMatriculaDTO.timeend = (long)(DateTime.Now.AddYears(1).ToUniversalTime() - dia_unix).TotalSeconds;

                            var respuesta = moodleWebService.RegistrarMatricula(moodleWebServiceRegistrarMatriculaDTO);
                            if (respuesta.Estado == true)
                            {
                                //Actualiza el estado a "recuperacion en otra modalidad"
                                repActualizacionMatricula.ActualizacionTipoMatriculaPEspecifico(pEspecificoMatriculaAlumnoDTO.IdPEspecificoRecuperacion, pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);
                                PEspecificoMatriculaAlumnoBO pEspecificoMatriculaAlumnoBO = new PEspecificoMatriculaAlumnoBO();
                                pEspecificoMatriculaAlumnoBO.IdMatriculaCabecera = pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera;
                                pEspecificoMatriculaAlumnoBO.IdPespecifico = pEspecificoMatriculaAlumnoDTO.IdPespecifico;
                                pEspecificoMatriculaAlumnoBO.IdPespecificoTipoMatricula = 2;
                                pEspecificoMatriculaAlumnoBO.Grupo = 1;
                                pEspecificoMatriculaAlumnoBO.IdCursoMoodle = moodleCurso.IdCursoMoodle;
                                pEspecificoMatriculaAlumnoBO.IdUsuarioMoodle = Convert.ToInt32(moodleUser.Id);
                                pEspecificoMatriculaAlumnoBO.Estado = true;
                                pEspecificoMatriculaAlumnoBO.UsuarioCreacion = pEspecificoMatriculaAlumnoDTO.Usuario;
                                pEspecificoMatriculaAlumnoBO.UsuarioModificacion = pEspecificoMatriculaAlumnoDTO.Usuario;
                                pEspecificoMatriculaAlumnoBO.FechaCreacion = DateTime.Now;
                                pEspecificoMatriculaAlumnoBO.FechaModificacion = DateTime.Now;

                                pEspecificoMatriculaAlumnoRepositorio.Insert(pEspecificoMatriculaAlumnoBO);

                                //var datosMoodle = (from matriculas_usuario in moodleContext.MdlUserEnrolments
                                //                   join mat in moodleContext.MdlEnrol on matriculas_usuario.Enrolid equals mat.Id
                                //                   where matriculas_usuario.Userid == moodleUser.Id &&
                                //                         mat.Courseid == moodleCurso.IdCursoMoodle
                                //                   select matriculas_usuario).FirstOrDefault();

                                //pEspecificoMatriculaAlumnoRepositorio.InsertarTMatAlumnosMoodle(matriculaCabecera.CodigoMatricula, matriculaCabecera.IdAlumno, Convert.ToInt32(datosMoodle.Id), Convert.ToInt32(moodleUser.Id), moodleCurso.IdCursoMoodle, pEspecificoMatriculaAlumnoDTO.Usuario);
                                var datosMoodle = (from matriculas_usuario in moodleContext.MdlUserEnrolments
                                                   join mat in moodleContext.MdlEnrol on matriculas_usuario.Enrolid equals mat.Id
                                                   where matriculas_usuario.Userid == moodleUser.Id &&
                                                         mat.Courseid == moodleCurso.IdCursoMoodle
                                                   select new { matriculas_usuario, mat }).FirstOrDefault();

                                //Se inserta en V3
                                var matriculaMoodle = moodleCursoRepositorio.ObtenerCursoMoodlePorMatricula(Convert.ToInt32(datosMoodle.matriculas_usuario.Id), matricula.CodigoMatricula);
                                if (matriculaMoodle.Count == 0)
                                {
                                    pEspecificoMatriculaAlumnoRepositorio.InsertarTMatAlumnosMoodle(matricula.CodigoMatricula, matricula.IdAlumno, Convert.ToInt32(datosMoodle.matriculas_usuario.Id), Convert.ToInt32(moodleUser.Id), moodleCurso.IdCursoMoodle, pEspecificoMatriculaAlumnoDTO.Usuario);
                                }

                                var listaCursosMatriculados = moodleCursoRepositorio.ObtenerDatosMatriculaMoodlePorIdUsuarioMoodle(moodleUser.Id);
                                if (listaCursosMatriculados.Count > 0)
                                {
                                    var listaCursosMatriculadosMoodle = (from matriculas_usuario in moodleContext.MdlUserEnrolments
                                                                         join mat in moodleContext.MdlEnrol on matriculas_usuario.Enrolid equals mat.Id
                                                                         where matriculas_usuario.Userid == moodleUser.Id
                                                                         select new { matriculas_usuario, mat }).ToList();

                                    foreach (var item in listaCursosMatriculados)
                                    {
                                        var element = listaCursosMatriculadosMoodle.Where(x => x.matriculas_usuario.Id == item.IdMatriculaMoodle).FirstOrDefault();
                                        if (element != null)
                                        {
                                            DateTime timestart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                                            timestart = timestart.AddSeconds(element.matriculas_usuario.Timestart).ToLocalTime();
                                            DateTime timeend = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                                            timeend = timeend.AddSeconds(element.matriculas_usuario.Timeend).ToLocalTime();
                                            pEspecificoMatriculaAlumnoRepositorio.ActualizarTmpMatriculasMoodle(Convert.ToInt32(element.matriculas_usuario.Userid), timestart, timeend, Convert.ToBoolean(element.matriculas_usuario.Status), Convert.ToInt32(element.mat.Courseid), Convert.ToInt32(element.matriculas_usuario.Enrolid), Convert.ToInt32(element.matriculas_usuario.Id));
                                        }
                                        else
                                        {
                                            pEspecificoMatriculaAlumnoRepositorio.EliminarTmpMatriculasMoodle(Convert.ToInt32(item.IdMatriculaMoodle));
                                        }
                                    }
                                    DateTime fechaIniciof = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                                    fechaIniciof = fechaIniciof.AddSeconds(datosMoodle.matriculas_usuario.Timestart).ToLocalTime();
                                    DateTime fechaFinf = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                                    fechaFinf = fechaFinf.AddSeconds(datosMoodle.matriculas_usuario.Timeend).ToLocalTime();
                                    var matriculaTmpMoodle = moodleCursoRepositorio.ObtenerDatosMatriculaMoodlePorMatriculaMoodle(datosMoodle.matriculas_usuario.Id);
                                    if (matriculaTmpMoodle.Count == 0)
                                        pEspecificoMatriculaAlumnoRepositorio.InsertarTmpMatriculasMoodle(Convert.ToInt32(datosMoodle.matriculas_usuario.Userid), fechaIniciof, fechaFinf, Convert.ToBoolean(datosMoodle.matriculas_usuario.Status), Convert.ToInt32(datosMoodle.mat.Courseid), Convert.ToInt32(datosMoodle.matriculas_usuario.Enrolid), Convert.ToInt32(datosMoodle.matriculas_usuario.Id));
                                }
                                else
                                {
                                    DateTime fechaInicio = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                                    fechaInicio = fechaInicio.AddSeconds(datosMoodle.matriculas_usuario.Timestart).ToLocalTime();

                                    DateTime fechaFin = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                                    fechaFin = fechaFin.AddSeconds(datosMoodle.matriculas_usuario.Timeend).ToLocalTime();

                                    pEspecificoMatriculaAlumnoRepositorio.InsertarTmpMatriculasMoodle(Convert.ToInt32(datosMoodle.matriculas_usuario.Userid), fechaInicio, fechaFin, Convert.ToBoolean(datosMoodle.matriculas_usuario.Status), Convert.ToInt32(datosMoodle.mat.Courseid), Convert.ToInt32(datosMoodle.matriculas_usuario.Enrolid), Convert.ToInt32(datosMoodle.matriculas_usuario.Id));
                                }
                                return Ok(pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera));
                            }
                            else
                            {
                                objetoCongelarCronograma.EliminarUltimaVersionCongelada(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera, moodleUser.Username);
                                cronograma.Estado = false;
                                return BadRequest(respuesta.Mensaje);
                            }
                        }
                        else
                        {
                            return BadRequest(cronograma.Mensaje);
                        }
                    }

                    else //si no es Aonline
                    {
                        listaPEspecificoPadrePespecificoHijo = pEspecificoMatriculaAlumnoRepositorio.ListaPespecificoPadrePespecificoHijo(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                        if (listaPEspecificoPadrePespecificoHijo.Count > 0) //significa que es padre
                        {
                            IdCursoMoodle = pEspecificoMatriculaAlumnoRepositorio.IdCursoMoodle(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                        }
                        else //significa que es hijo
                        {
                            IdCursoMoodle = pEspecificoMatriculaAlumnoRepositorio.getIdPEspecificoPadre(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                        }

                        repActualizacionMatricula.ActualizacionTipoMatriculaPEspecifico(pEspecificoMatriculaAlumnoDTO.IdPEspecificoRecuperacion, pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);

                        PEspecificoMatriculaAlumnoBO pEspecificoMatriculaAlumnoBO = new PEspecificoMatriculaAlumnoBO();
                        pEspecificoMatriculaAlumnoBO.IdMatriculaCabecera = pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera;
                        pEspecificoMatriculaAlumnoBO.IdPespecifico = pEspecificoMatriculaAlumnoDTO.IdPespecifico;
                        pEspecificoMatriculaAlumnoBO.IdPespecificoTipoMatricula = 2;
                        pEspecificoMatriculaAlumnoBO.Estado = true;
                        pEspecificoMatriculaAlumnoBO.IdCursoMoodle = IdCursoMoodle;
                        pEspecificoMatriculaAlumnoBO.IdUsuarioMoodle = Convert.ToInt32(moodleUser.Id);
                        if (pEspecificoMatriculaAlumnoDTO.Grupo == 0) pEspecificoMatriculaAlumnoBO.Grupo = 1;
                        else pEspecificoMatriculaAlumnoBO.Grupo = pEspecificoMatriculaAlumnoDTO.Grupo;
                        pEspecificoMatriculaAlumnoBO.UsuarioCreacion = pEspecificoMatriculaAlumnoDTO.Usuario;
                        pEspecificoMatriculaAlumnoBO.UsuarioModificacion = pEspecificoMatriculaAlumnoDTO.Usuario;
                        pEspecificoMatriculaAlumnoBO.FechaCreacion = DateTime.Now;
                        pEspecificoMatriculaAlumnoBO.FechaModificacion = DateTime.Now;

                        pEspecificoMatriculaAlumnoRepositorio.Insert(pEspecificoMatriculaAlumnoBO);

                        //ENVIO CORREO   
                        //var oportunidad = _repOportunidad.FirstById(pEspecificoMatriculaAlumnoDTO.IdOportunidad);
                        var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                        //List<string> correosPersonalizados = new List<string>
                        //{
                        //    alumno.Email1
                        //};
                        List<string> correosPersonalizadosCopiaOculta = new List<string>
                        {
                            //"fvaldez@bsginstitute.com",
                            //"lhuallpa@bsginstitute.com",
                            //"controldeaccesos@bsginstitute.com",
                            //"pbeltran@bsginstitute.com",
                            //"bamontoya@bsginstitute.com",
                            //personal.Email
                            "aarcana@bsginsitute.com"
                        };

                        string mensaje = "Saludos. <br> Se matriculo al codigo:<strong> " + codigoMatricula + " </strong>del Programa Especifico Anterior:<strong> " + nombreCursoAnterior +
                            "</strong> al nuevo Programa Especifico:<strong> " + nombreCursoRecuperacion + "</strong><br>Atentamente.";

                        TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = personal.Email,
                            //Sender = "w.choque.itusaca@isur.edu.pe",
                            //Recipient = "aarcana@bsginsitute.com",
                            Recipient = "aarcana@bsginsitute.com",
                            Subject = "Recuperacion en Otra Modalidad",
                            Message = mensaje,
                            Cc = "",
                            //Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            Bcc = "",
                            //AttachedFiles = reemplazoEtiquetaPlantilla.EmailReemplazado.ListaArchivosAdjuntos,
                            //AttachedFiles = "";
                        };
                        var mailServie = new TMK_MailServiceImpl();
                        mailServie.SetData(mailDataPersonalizado);
                        mailServie.SendMessageTask();

                        return Ok(pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera));
                    }
                }
            }
            catch (Exception e)
            {
                if (cronograma.Estado == true) objetoCongelarCronograma.EliminarUltimaVersionCongelada(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera, moodleUser.Username);
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: 
        /// Fecha: 06/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene plantillas disponibles por fase
        /// </summary>
        /// <param name="IdFaseOportunidad">Id de Fase de Oportunidad</param>
        /// <param name="IdPersonalAreaTrabajo">Id de Arera de Trabajo de Personal</param>
        /// <returns>List<FiltroDTO></returns>
        [Route("[action]/{IdFaseOportunidad}/{IdPersonalAreaTrabajo}")]
        [HttpGet]
        public IActionResult ObtenerPlantillaPorFase(int IdFaseOportunidad, int IdPersonalAreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdFaseOportunidad <= 0)
            {
                return BadRequest("El Id FaseOportunidad no Existe");
            }
            try
            {
                var plantillaFase = _repPlantillaClaveValor.ObtenerPlantillaGenerarMensaje(IdFaseOportunidad);
                if (!_repPersonalAreaTrabajo.Exist(IdPersonalAreaTrabajo))
                {
                    return BadRequest("No existe el PersonalAreaTrabajo");
                }
                if (IdPersonalAreaTrabajo == ValorEstatico.IdPersonalAreaTrabajoOperaciones)
                {
                    plantillaFase.AddRange(_repPlantillaClaveValor.ObtenerPlantillaGenerarMensajeOperaciones());
                }
                return Ok(plantillaFase.OrderBy(w => w.Nombre));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la Fecha de visualización de Whatsapp
        /// </summary>
        /// <returns> Confirmación de Actualización </returns>
        /// <returns> Bool </returns>
        [Route("[action]/{IdActividadDetalle}/{Usuario}")]
        [HttpGet]
        public IActionResult ActualizarFechaOcultarWhatsapp(int IdActividadDetalle, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);

                var actividad = _repActividadDetalle.FirstById(IdActividadDetalle);
                actividad.FechaModificacion = DateTime.Now;
                actividad.FechaOcultarWhatsapp = DateTime.Now;
                actividad.UsuarioModificacion = Usuario;

                _repActividadDetalle.Update(actividad);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta Registro de Envio
        /// </summary>
        /// <returns> Confirmación de Inserción </returns>
        /// <returns> Bool </returns>
        [Route("[action]/{IdOportunidad}/{NombreUsuario}")]
        [HttpGet]
        public IActionResult InsertarEnvio(int IdOportunidad, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repDocumentoEnviadoWebPw = new DocumentoEnviadoWebPwRepositorio(_integraDBContext);
                var _repPEspecifico = new PespecificoRepositorio(_integraDBContext);

                var _oportunidad = _repOportunidad.FirstById(IdOportunidad);
                var _pEspecifico = _repPEspecifico.FirstBy(x => x.IdCentroCosto == _oportunidad.IdCentroCosto);
                var documentoEnviadoWebPwBO = new DocumentoEnviadoWebPwBO()
                {
                    IdAlumno = _oportunidad.IdAlumno,
                    Nombre = "BSG Institute - Condiciones y Características",
                    IdPespecifico = _pEspecifico.Id,
                    FechaEnvio = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = NombreUsuario,
                    UsuarioModificacion = NombreUsuario,
                    Estado = true
                };
                if (!documentoEnviadoWebPwBO.HasErrors)
                {
                    _repDocumentoEnviadoWebPw.Insert(documentoEnviadoWebPwBO);
                }
                return Ok(documentoEnviadoWebPwBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Beneficios por Código de Matrícula
        /// </summary>
        /// <returns> Beneficios por CodigoMatricula </returns>
        /// <returns> Objeto DTO : CorrespondeBeneficiosDTO </returns>
        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public IActionResult ObtenerBeneficiosPorMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var corresponde = true;
                CorrespondeBeneficiosDTO beneficios = new CorrespondeBeneficiosDTO();
                var beneficiosmatricula = _repMatriculaCabecera.ObtenerBeneficiosCongeladosPorMatricula(CodigoMatricula);

                var pespecifico = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(CodigoMatricula);
                var pgeneral = _repMatriculaCabecera.ObtenerProgramaGeneral(pespecifico);
                List<EstadosMatriculaDTO> listestado = _repMatriculaCabecera.ObtenerEstadoPgeneralBeneficio(pgeneral);
                List<EstadosMatriculaDTO> listsubestado = _repMatriculaCabecera.ObtenerSubEstadoPgeneralBeneficio(pgeneral);
                var estadoalumno = _repMatriculaCabecera.ObtenerEstadoAlumno(CodigoMatricula);
                var subestadoalumno = _repMatriculaCabecera.ObtenerSubestadoAlumno(CodigoMatricula);
                foreach (var item in listestado)
                {
                    if (item.Id == estadoalumno) { corresponde = true; break; }
                }
                if (estadoalumno == 1 && subestadoalumno == 0) corresponde = false;
                else
                {
                    foreach (var item in listsubestado)
                    {
                        if (item.Id == subestadoalumno) { corresponde = false; break; }
                    }
                }
                beneficios.beneficios = beneficiosmatricula;
                beneficios.corresponde = corresponde;

                return Ok(beneficios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Beneficios Solicitados por Código de Matrícula
        /// </summary>
        /// <returns> Beneficios por CodigoMatricula </returns>
        /// <returns> Lista de Objeto DTO : List<InformacionBeneficioSolicitadoDTO> </returns>
        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public IActionResult ObtenerInformacionBeneficioSolicitado(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var solicitados = _repMatriculaCabecera.ObtenerBeneficiosSolicitadosPorMatricula(CodigoMatricula);
                return Ok(solicitados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Beneficios Solicitados
        /// </summary>
        /// <returns> Beneficios Registrados </returns>
        /// <returns> Lista de Objeto DTO : List<BeneficioSolicitadoReporteDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTodoInformacionBeneficioSolicitado()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var solicitados = _repMatriculaCabecera.ObtenerTodoBeneficioSolicitado();
                return Ok(solicitados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Beneficios Adicionales por Código de Matrícula
        /// </summary>
        /// <returns> Beneficios Datos Adicionales </returns>
        /// <returns> Lista de Objeto DTO : List<DatoAdicionalPWDTO> </returns>
        [Route("[action]/{CodigoMatricula}/{Id}")]
        [HttpGet]
        public IActionResult ObtenerDatosAdicionalesPorCodigo(string CodigoMatricula, int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var solicitados = _repMatriculaCabecera.ObtenerDatosAdicionalesPorCodigo(CodigoMatricula, Id);
                return Ok(solicitados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Priscila Paci - Fischer Valdez - Edgar Serruto
        /// Fecha: 10/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Actualiza Estado Beneficios por Matrícula y Estado de Mactrícula Beneficio
        /// </summary>
        /// <param name="IdConfiguracionBeneficioProgramaGeneral">Id Configuración de Programa General</param>
        /// <param name="IdMatriculaCabeceraBeneficio">Id de Beneficio de Matrícula de Cabecera</param>
        /// <param name="Usuario">Usuario de Módulo</param>
        /// <returns>Objeto Agrupado</returns>        
        [Route("[action]/{IdMatriculaCabeceraBeneficio}/{IdConfiguracionBeneficioProgramaGeneral}/{Usuario}")]
        [HttpGet]
        public IActionResult ActualizarEstadoMatriculaBeneficio(int IdMatriculaCabeceraBeneficio, int IdConfiguracionBeneficioProgramaGeneral, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<BeneficioDatosAdicionalesDTO> datosAdicionales = new List<BeneficioDatosAdicionalesDTO>();
                var beneficiosmatricula = 0;
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                datosAdicionales = _repMatriculaCabecera.ObtenerDatosAdicionalesPgeneralPorIdConfiguracion(IdConfiguracionBeneficioProgramaGeneral);

                if (datosAdicionales.Count > 0)
                {
                    beneficiosmatricula = _repMatriculaCabecera.ActualizarEstadoMatriculaCabeceraBeneficio(IdMatriculaCabeceraBeneficio);
                }
                else
                {
                    _repMatriculaCabecera.ActualizarEstadoMatriculaCabeceraBeneficio(IdMatriculaCabeceraBeneficio);
                    beneficiosmatricula = _repMatriculaCabecera.PorAprobarSolicitudBeneficio(IdMatriculaCabeceraBeneficio);
                }
                var matriculaCabeceraBeneficio = _repMatriculaCabeceraBeneficios.FirstById(IdMatriculaCabeceraBeneficio);
                if (matriculaCabeceraBeneficio != null)
                {
                    matriculaCabeceraBeneficio.FechaModificacion = DateTime.Now;
                    matriculaCabeceraBeneficio.UsuarioModificacion = Usuario;
                    matriculaCabeceraBeneficio.IdEstadoMatriculaCabeceraBeneficio = 1; //1 Pendiente
                    _repMatriculaCabeceraBeneficios.Update(matriculaCabeceraBeneficio);
                }
                return Ok(new { beneficiosmatricula = beneficiosmatricula, datosadicionales = datosAdicionales });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Priscila Pacsi - Fischer Valdez - Edgar Serruto
        /// Fecha: 10/02/2021
        /// Versión: 1.2
        /// <summary>
        /// Aprobar Solicitud de Beneficio
        /// </summary>
        /// <param name="IdMatriculaCabeceraBeneficio">Id de Beneficio de matrícula</param>
        /// <param name="Usuario">Usuario de Interfaz</param>
        /// <param name="IdEstadoSolicitudAprobado">Id de Estado de Solicitud Aprobado</param>
        /// <returns>bool, string</returns>
        [Route("[action]/{IdMatriculaCabeceraBeneficio}/{Usuario}/{IdEstadoSolicitudAprobado}")]
        [HttpGet]
        public IActionResult AprobarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario, int IdEstadoSolicitudAprobado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string mensaje = string.Empty;
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var matriculaCabeceraBeneficio = _repMatriculaCabeceraBeneficios.FirstById(IdMatriculaCabeceraBeneficio);
                if (matriculaCabeceraBeneficio != null && IdEstadoSolicitudAprobado > 0)
                {
                    matriculaCabeceraBeneficio.UsuarioAprobacion = Usuario;
                    matriculaCabeceraBeneficio.FechaAprobacion = DateTime.Now;
                    matriculaCabeceraBeneficio.FechaProgramada = DateTime.Now.AddDays(30);
                    matriculaCabeceraBeneficio.FechaModificacion = DateTime.Now;
                    matriculaCabeceraBeneficio.UsuarioModificacion = Usuario;
                    matriculaCabeceraBeneficio.IdEstadoSolicitudBeneficio = IdEstadoSolicitudAprobado; //4 Aprobar Solicitud
                    _repMatriculaCabeceraBeneficios.Update(matriculaCabeceraBeneficio);


                    mensaje = "Se Aprobó la solicitud correctamente";
                    return Ok(new { Respuesta = true, Mensaje = mensaje });
                }
                else
                {
                    mensaje = "No se encontró el beneficio solicitado";
                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Priscila Pacsi - Fischer Valdez - Edgar Serruto
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Rechaza Solicitud de Beneficio
        /// </summary>
        /// <param name="IdMatriculaCabeceraBeneficio">Id de Beneficio de matrícula</param>
        /// <param name="Usuario">Usuario de Interfaz</param>
        /// <param name="IdEstadoSolicitudRechazado">Id de Estado de Solicitud Rechazado</param>
        /// <param name="IdEstadoMatriculaCabeceraBeneficio">Estado de Beneficio</param>
        /// <returns>bool, string</returns>
        [Route("[action]/{IdMatriculaCabeceraBeneficio}/{Usuario}/{IdEstadoSolicitudRechazado}/{IdEstadoMatriculaCabeceraBeneficio}")]
        [HttpGet]
        public IActionResult RechazarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario, int IdEstadoSolicitudRechazado, int IdEstadoMatriculaCabeceraBeneficio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string mensaje = string.Empty;
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var matriculaCabeceraBeneficio = _repMatriculaCabeceraBeneficios.FirstById(IdMatriculaCabeceraBeneficio);
                if (matriculaCabeceraBeneficio != null && IdEstadoSolicitudRechazado > 0)
                {
                    matriculaCabeceraBeneficio.FechaModificacion = DateTime.Now;
                    matriculaCabeceraBeneficio.UsuarioModificacion = Usuario;
                    matriculaCabeceraBeneficio.IdEstadoSolicitudBeneficio = IdEstadoSolicitudRechazado; //5 Rechazar Solicitud
                    matriculaCabeceraBeneficio.IdEstadoMatriculaCabeceraBeneficio = IdEstadoMatriculaCabeceraBeneficio; //3 Rechazado
                    _repMatriculaCabeceraBeneficios.Update(matriculaCabeceraBeneficio);

                    mensaje = "Se guardó correctamente";
                    return Ok(new { Respuesta = true, Mensaje = mensaje });
                }
                else
                {
                    mensaje = "No se encontró el beneficio solicitado";
                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Entrega Beneficio
        /// </summary>
        /// <returns> Confirmación de Entrega: Objeto </returns>
        /// <returns> int </returns>
        [Route("[action]/{idMatriculaCabeceraBeneficio}/{Usuario}")]
        [HttpGet]
        public IActionResult EntregarBeneficio(int idMatriculaCabeceraBeneficio, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);

                var beneficiosmatricula = _repMatriculaCabecera.EntregarBeneficio(idMatriculaCabeceraBeneficio, Usuario);

                return Ok(beneficiosmatricula);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el grupo segun el IdPEspecifico
        /// </summary>
        /// <returns> Lista de Grupo por PEspecifico </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoSesionGruposDTO>  </returns>
        [Route("[action]/{IdPEspecifico}")]
        [HttpGet]
        public IActionResult PEspesificoSesionGrupo(int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();

                var grupos = _repPespecificoSesion.ListarGruposPEspecifico(IdPEspecifico);

                return Ok(grupos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Sesion por Id de Programa Específico
        /// </summary>
        /// <returns> Sesion por Id de Programa Específico </returns>
        /// <returns> Objeto DTO : PEspecificoTipoDTO </returns>
        [Route("[action]/{IdPEspecifico}")]
        [HttpGet]
        public IActionResult PEspesificoSesionTipo(int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                var tipo = _repPespecifico.getTipoPEspecifico(IdPEspecifico);
                return Ok(tipo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cursos IRCA por Id de Matrícula Cabecera
        /// </summary>
        /// <returns> Lista de CUrsos IRCA </returns>
        /// <returns> Lista de Objeto DTO : List<PgeneralCursoIRCADTO> </returns>
        [Route("[action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCursoIRCA(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralRepositorio _repPgeneral = new PgeneralRepositorio();
                var irca = new { listaCursos = _repPgeneral.obtenerCursosIrcaAlumno(IdMatriculaCabecera) };
                return Ok(irca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cursos Relacionados por Programa General
        /// </summary>
        /// <returns> Lista de Cursos Relacionados por Programa General </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoRelacionadoPorIdPGeneralDTO> </returns>
        [Route("[Action]/{IdPEspecifico}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoRelacionadoPGeneral(int IdPEspecifico, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                return Ok(pespecificoRepositorio.ObtenerPEspecificoRelacionadoPGeneral(IdPEspecifico, IdMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Campo fechaFinalizacion de Matricula
        /// </summary>
        /// <returns> FechaFinalizacion de Matricula </returns>
        /// <returns> String </returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerFechaFinalizacionMatricula(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var rptas = _repMatriculaCabecera.ObtenerFechaFinalizacion(IdMatriculaCabecera);
                return Ok(new { rptas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta Detalles de Datos Adicionales 
        /// </summary>
        /// <returns> Confirmación de Inserción </returns>
        /// <returns> Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult AgregarDetalleDatosAdicionales([FromBody] DetallesDatoAdicionalDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ContenidoDatoAdicionalRepositorio _repContenidoDatoAdicional = new ContenidoDatoAdicionalRepositorio();
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                _repMatriculaCabecera.PorAprobarSolicitudBeneficio(json.IdMatriculaCabeceraBeneficios); //aqui le cambiamos el estado de la solicitud                
                ContenidoDatoAdicionalBO datoadicional;
                foreach (var item in json.DatosAdicionales)
                {
                    datoadicional = new ContenidoDatoAdicionalBO();
                    datoadicional.IdMatriculaCabecera = json.IdMatriculaCabecera;
                    datoadicional.CodigoMatricula = json.CodigoMatricula;
                    datoadicional.IdMatriculaCabeceraBeneficios = json.IdMatriculaCabeceraBeneficios;
                    datoadicional.IdBeneficioDatoAdicional = item.Id;
                    datoadicional.Contenido = item.Contenido;
                    datoadicional.UsuarioCreacion = "portalweb";
                    datoadicional.UsuarioModificacion = "portalweb";
                    datoadicional.FechaCreacion = DateTime.Now;
                    datoadicional.FechaModificacion = DateTime.Now;
                    datoadicional.Estado = true;
                    _repContenidoDatoAdicional.Insert(datoadicional);
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        private string QuitarAcentos(string inputString)
        {
            Regex a = new Regex("[á|à|ä|â|Á|À|Ä|Â]", RegexOptions.Compiled);
            Regex e = new Regex("[é|è|ë|ê|É|È|Ë|Ê]", RegexOptions.Compiled);
            Regex i = new Regex("[í|ì|ï|î|Í|Ì|Ï|Î]", RegexOptions.Compiled);
            Regex o = new Regex("[ó|ò|ö|ô|Ó|Ò|Ö|Ô]", RegexOptions.Compiled);
            Regex u = new Regex("[ú|ù|ü|û|Ú|Ù|Ü|Û]", RegexOptions.Compiled);
            Regex n = new Regex("[ñ|Ñ]", RegexOptions.Compiled);
            inputString = a.Replace(inputString, "a");
            inputString = e.Replace(inputString, "e");
            inputString = i.Replace(inputString, "i");
            inputString = o.Replace(inputString, "o");
            inputString = u.Replace(inputString, "u");
            inputString = n.Replace(inputString, "n");
            return inputString;
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cursos relacionados de irca por programa especifico
        /// </summary>
        /// <returns> Cursos relacionados de irca por programa especifico </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoRelacionadoPorIdPGeneralDTO> </returns>
        [Route("[Action]/{IdPEspecifico}/{IdMatriculaCabecera}/{EsCursoDSig}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoRelacionadoIrca(int IdPEspecifico, int IdMatriculaCabecera, bool EsCursoDSig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                return Ok(pespecificoRepositorio.ObtenerPEspecificoRelacionadoIrca(IdPEspecifico, IdMatriculaCabecera, EsCursoDSig));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Priscila Pacsi
        /// Fecha: 28/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Restablece Solicitud de Beneficio
        /// </summary>
        /// <param name="IdMatriculaCabeceraBeneficio">Id de Beneficio de matrícula</param>
        /// <param name="Usuario">Usuario de Interfaz</param>
        /// <returns>bool, string</returns>
        [Route("[action]/{IdMatriculaCabeceraBeneficio}/{Usuario}")]
        [HttpGet]
        public IActionResult RestablecerSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string mensaje = string.Empty;
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                var matriculaCabeceraBeneficio = _repMatriculaCabeceraBeneficios.FirstById(IdMatriculaCabeceraBeneficio);
                if (matriculaCabeceraBeneficio != null)
                {
                    matriculaCabeceraBeneficio.FechaModificacion = DateTime.Now;
                    matriculaCabeceraBeneficio.UsuarioModificacion = Usuario;
                    matriculaCabeceraBeneficio.IdEstadoSolicitudBeneficio = null; 
                    matriculaCabeceraBeneficio.IdEstadoMatriculaCabeceraBeneficio = 1;
                    matriculaCabeceraBeneficio.FechaAprobacion = null;
                    matriculaCabeceraBeneficio.FechaSolicitud = null;
                    matriculaCabeceraBeneficio.FechaProgramada = null;
                    _repMatriculaCabeceraBeneficios.Update(matriculaCabeceraBeneficio);

                    mensaje = "Se guardó correctamente";
                    return Ok(new { Respuesta = true, Mensaje = mensaje });
                }
                else
                {
                    mensaje = "No se encontró el beneficio solicitado";
                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 10/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la cabecera del speech
        /// </summary>
        /// <returns> true or false </returns>
        [Route("[Action]/{IdOportunidad}/{IdCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerCabeceraSpeech(int idOportunidad, int idCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(contexto);

                return Ok(_repPGeneral.ObtenerCabeceraSpeech(idOportunidad,idCentroCosto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 10/12/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene el publico general para un Programa General
        /// </summary>
        /// <returns> Lista de prerequisitos, beneficios y empresa competidora </returns>
        /// <returns> Objeto DTO : ProgramaGeneralPreBenCompuestoDTO </returns>
        [Route("[action]/{IdCentroCosto}/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerPublicoObjetivoPrograma(int IdCentroCosto, int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(contexto);

                return Ok(_repPGeneral.ObtenerPublicoObjetivoPrograma(IdCentroCosto, IdOportunidad));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 10/12/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene los requisitos de certificacion de Programa General
        /// </summary>
        /// <returns> Lista de prerequisitos, beneficios y empresa competidora </returns>
        /// <returns> Objeto DTO : ProgramaGeneralPreBenCompuestoDTO </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerRequisitosCertificacionPrograma(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(contexto);
                var resultado = _repPGeneral.ObtenerRequisitosCertificacionPrograma(IdOportunidad);
                foreach (var item in resultado)
                {
                    item.Requisitos = _repPGeneral.ObtenerRequisitosCertificacionArgPrograma(item.IdCertificacion);
                }
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 10/12/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene los factores de motivacion de Programa General
        /// </summary>
        /// <returns> Lista de prerequisitos, beneficios y empresa competidora </returns>
        /// <returns> Objeto DTO : ProgramaGeneralPreBenCompuestoDTO </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerFactorMotivacionPrograma(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(contexto);
                var resultado = _repPGeneral.ObtenerFactorMotivacionPrograma(IdOportunidad);
                foreach (var item in resultado)
                {
                    item.Argumentos = _repPGeneral.ObtenerArgumentosMotivacionPrograma(item.IdMotivacion);
                }
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 10/12/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene los problemas de Programa General
        /// </summary>
        /// <returns> Lista de prerequisitos, beneficios y empresa competidora </returns>
        /// <returns> Objeto DTO : ProgramaGeneralPreBenCompuestoDTO </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerProblemaProgramaGeneral(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(contexto);
                var resultado = _repPGeneral.ObtenerProblemaPrograma(IdOportunidad);
                foreach (var item in resultado)
                {
                    item.Argumentos = _repPGeneral.ObtenerArgumentosProblemaPrograma(item.IdProblema, IdOportunidad);
                }
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin SAlazar
        /// Fecha: 11/12/2021
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene Historial de comentarios de Oportunidad
        /// </summary>
        /// <returns> Información de Interacciones de Oportunidad  </returns>
        /// <returns> Lista de Objeto DTO : List<ReporteSeguimientoOportunidadLogGridDTO> </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult GetHistorialInteraccionesComentariosOportunidad(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                Reportes reporte = new Reportes();
                var resultado = reporte.ObtenerOportunidadesLogV2(IdOportunidad);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 14/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Sentinel para la cabecera de agenda
        /// </summary>
        /// <returns> Información de Sentinel por Alumno </returns>
        /// <returns> objetoDTO :  SentinelDatosContactoDTO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerSemaforoSentinelAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no Existe");
            }
            integraDBContext contexto = new integraDBContext();
            SentinelRepositorio _repSentinel = new SentinelRepositorio(contexto);
            SentinelDatosContactoDTO datosSentinel = new SentinelDatosContactoDTO();
            SentinelDatosCabeceraDTO cabecera = new SentinelDatosCabeceraDTO();

            datosSentinel = _repSentinel.ObtenerDastosAlumnoSentinel(idAlumno);
            if (datosSentinel != null)
            {
                cabecera = _repSentinel.ObtenerCabeceraSentinel(datosSentinel.IdSentinel.Value);
            }

            return Ok(cabecera);
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 14/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene moneda por alumno
        /// </summary>
        /// <returns> Información de Sentinel por Alumno </returns>
        /// <returns> objetoDTO :  SentinelDatosContactoDTO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerMonedaPorAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no Existe");
            }
            integraDBContext contexto = new integraDBContext();
            MonedaRepositorio _repMoneda = new MonedaRepositorio(contexto);
            return Ok(_repMoneda.ObtenerMonedaPorAlumno(idAlumno));
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 21/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico con la ultima interaccion
        /// </summary>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<CorreoInteraccionesAlumnoDTO> </returns>
        [Route("[Action]/{IdAlumno}/{IdAsesor}")]
        [HttpGet]
        public ActionResult GetInteraccionesCorreosEnviadosV2(int IdAlumno, int IdAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdAlumno <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(205));
            }
            if (IdAsesor <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                MandrilRepositorio _mandrilRepositorio = new MandrilRepositorio(_integraDBContext);
                return Ok(_mandrilRepositorio.ListaInteraccionCorreoAlumnoPorOportunidadV2(IdAlumno, IdAsesor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 21/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico con la ultima interaccion
        /// </summary>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<CorreoInteraccionesAlumnoDTO> </returns>
        [Route("[Action]/{IdAlumno}/{IdAsesor}/{MessageId}")]
        [HttpGet]
        public ActionResult ObtenerInteraccionesCorreosEnviados(int IdAlumno, int IdAsesor, string MessageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdAlumno <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(205));
            }
            if (IdAsesor <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                MandrilRepositorio _mandrilRepositorio = new MandrilRepositorio(_integraDBContext);
                return Ok(_mandrilRepositorio.ListaInteraccionCorreoAlumno(IdAlumno, IdAsesor, MessageId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico con la ultima interaccion
        /// </summary>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<CorreoInteraccionesAlumnoDTO> </returns>
        [Route("[Action]/{CorreoReceptor}/{MessageId}")]
        [HttpGet]
        public ActionResult ObtenerCorreosEnviadosSpeech(string CorreoReceptor, string MessageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MandrilRepositorio _mandrilRepositorio = new MandrilRepositorio(_integraDBContext);
                return Ok(_mandrilRepositorio.VerCorreoAlumnoSpeech(CorreoReceptor, MessageId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jashin Salazar
        /// Fecha: 25/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza la Programación de Actividades
        /// </summary>
        /// <returns> Información de Programación de Actividad </returns>
        /// <returns> objeto Agrupado </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarYProgramarActividadAlternoV2([FromBody] ParametroFinalizarActividadAlternoDTO dto)
        {
            string parametros = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                parametros = JsonConvert.SerializeObject(dto);

                // Desactivado de redireccion
                var _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                if (dto.ActividadAntigua.IdOportunidad.HasValue)
                {
                    try
                    {
                        _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(dto.ActividadAntigua.IdOportunidad.Value);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                var _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);

                if (dto.ActividadAntigua.IdOportunidad.HasValue)
                    if (!_repOportunidad.Exist(x => x.Id == dto.ActividadAntigua.IdOportunidad && x.IdFaseOportunidad == dto.datosOportunidad.IdFaseOportunidad))
                        return BadRequest(new { Codigo = 409, Mensaje = $"La oportunidad fue trabajada por otra persona: IdOportunidad {dto.ActividadAntigua.IdOportunidad}" });

                var _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                var _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                var _repHoraBloqueada = new HoraBloqueadaRepositorio(_integraDBContext);
                var _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                var _repLlamadaActividad = new LlamadaActividadRepositorio(_integraDBContext);
                var _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                var _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
                var _repReprogramacionCabecera = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                var _repReprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalRepositorio(_integraDBContext);
                var _repComprobantePagoOportunidad = new ComprobantePagoOportunidadRepositorio(_integraDBContext);
                var _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);
                var oportunidadPrerequisitoGeneralRepositorio = new OportunidadPrerequisitoGeneralRepositorio(_integraDBContext);
                var oportunidadPrerequisitoEspecificoRepositorio = new OportunidadPrerequisitoEspecificoRepositorio(_integraDBContext);
                var oportunidadBeneficioRepositorio = new OportunidadBeneficioRepositorio(_integraDBContext);
                var detalleOportunidadCompetidorRepositorio = new DetalleOportunidadCompetidorRepositorio(_integraDBContext);

                int idReprogramacionCabecera = 0;
                HoraBloqueadaBO horaBloqueada = new HoraBloqueadaBO
                {
                    IdPersonal = dto.filtro.IdPersonal,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = dto.filtro.Usuario,
                    UsuarioModificacion = dto.filtro.Usuario,
                    Estado = true
                };

                if (dto.datosOportunidad.UltimaFechaProgramada != null)
                {
                    horaBloqueada.Fecha = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                    horaBloqueada.Hora = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                }

                if (horaBloqueada.HasErrors)
                {
                    return BadRequest(horaBloqueada.GetErrors(null));
                }

                var Oportunidad = new OportunidadBO(dto.ActividadAntigua.IdOportunidad.Value, dto.filtro.Usuario, _integraDBContext)
                {
                    IdFaseOportunidadIp = dto.datosOportunidad.IdFaseOportunidadIp,
                    IdFaseOportunidadIc = dto.datosOportunidad.IdFaseOportunidadIc,
                    FechaEnvioFaseOportunidadPf = dto.datosOportunidad.FechaEnvioFaseOportunidadPf,
                    FechaPagoFaseOportunidadPf = dto.datosOportunidad.FechaPagoFaseOportunidadPf,
                    FechaPagoFaseOportunidadIc = dto.datosOportunidad.FechaPagoFaseOportunidadIc,
                    IdFaseOportunidadPf = dto.datosOportunidad.IdFaseOportunidadPf,
                    CodigoPagoIc = dto.datosOportunidad.CodigoPagoIc,
                    ValidacionCorrecta = true
                };

                var ActividadAntigua = new ActividadDetalleBO
                {
                    Id = dto.ActividadAntigua.Id,
                    IdActividadCabecera = dto.ActividadAntigua.IdActividadCabecera,
                    FechaProgramada = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada),
                    FechaReal = dto.ActividadAntigua.FechaReal,
                    DuracionReal = dto.ActividadAntigua.DuracionReal,
                    //IdOcurrencia = dto.ActividadAntigua.IdOcurrencia.Value,
                    IdEstadoActividadDetalle = dto.ActividadAntigua.IdEstadoActividadDetalle,
                    Comentario = dto.ActividadAntigua.Comentario,
                    IdAlumno = dto.ActividadAntigua.IdAlumno,
                    Actor = dto.ActividadAntigua.Actor,
                    IdOportunidad = dto.ActividadAntigua.IdOportunidad.Value,
                    IdCentralLlamada = dto.ActividadAntigua.IdCentralLlamada,
                    RefLlamada = dto.ActividadAntigua.RefLlamada,
                    //IdOcurrenciaActividad = dto.ActividadAntigua.IdOcurrenciaActividad,
                    IdClasificacionPersona = Oportunidad.IdClasificacionPersona,
                    IdOcurrenciaAlterno = dto.ActividadAntigua.IdOcurrencia.Value,
                    IdOcurrenciaActividadAlterno = dto.ActividadAntigua.IdOcurrenciaActividad
                };

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(dto.ActividadAntigua.Id, _integraDBContext);

                OportunidadCompetidorBO OportunidadCompetidor;
                if (dto.DatosCompuesto.OportunidadCompetidor.Id == 0)
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO
                    {
                        Id = 0,
                        IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.IdOportunidad,
                        OtroBeneficio = dto.DatosCompuesto.OportunidadCompetidor.OtroBeneficio,
                        Respuesta = dto.DatosCompuesto.OportunidadCompetidor.Respuesta,
                        Completado = dto.DatosCompuesto.OportunidadCompetidor.Completado,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = dto.Usuario,
                        UsuarioModificacion = dto.Usuario,
                        Estado = true
                    };
                }
                else
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO(dto.DatosCompuesto.OportunidadCompetidor.Id, _integraDBContext)
                    {
                        IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.IdOportunidad,
                        OtroBeneficio = dto.DatosCompuesto.OportunidadCompetidor.OtroBeneficio,
                        Respuesta = dto.DatosCompuesto.OportunidadCompetidor.Respuesta,
                        Completado = dto.DatosCompuesto.OportunidadCompetidor.Completado
                    };
                }
                if (!OportunidadCompetidor.HasErrors)
                {
                    Oportunidad.OportunidadCompetidor = OportunidadCompetidor;
                }
                else
                {
                    return BadRequest(OportunidadCompetidor.GetErrors(null));
                }

                var CalidadBO = new CalidadProcesamientoBO
                {
                    IdOportunidad = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.IdOportunidad,
                    PerfilCamposLlenos = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposLlenos,
                    PerfilCamposTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposTotal,
                    Dni = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.Dni,
                    PgeneralValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralValidados,
                    PgeneralTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralTotal,
                    PespecificoValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoValidados,
                    PespecificoTotal = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoTotal,
                    BeneficiosValidados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosValidados,
                    BeneficiosTotales = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosTotales,
                    CompetidoresVerificacion = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.CompetidoresVerificacion,
                    ProblemaSeleccionados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSeleccionados,
                    ProblemaSolucionados = dto.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSolucionados,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    Estado = true
                };
                if (!CalidadBO.HasErrors)
                {
                    Oportunidad.CalidadProcesamiento = CalidadBO;
                }
                else
                {
                    return BadRequest(CalidadBO.GetErrors(null));
                }
                /*
                OportunidadCompetidor.ListaPrerequisitoGeneral = new List<OportunidadPrerequisitoGeneralBO>();
                OportunidadPrerequisitoGeneralBO ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO();
                var listaPreRequisitoGeneralAgrupado = dto.DatosCompuesto.ListaPrerequisitoGeneral.GroupBy(x => x.IdProgramaGeneralBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaPreRequisitoGeneralAgrupado)
                {
                    ListaPrerequisitoGeneral = oportunidadPrerequisitoGeneralRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio);

                    if (ListaPrerequisitoGeneral != null)
                    {
                        var listaPreRequisitoGeneralEliminar = oportunidadPrerequisitoGeneralRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio && x.Id != ListaPrerequisitoGeneral.Id).ToList();
                        foreach (var itemEliminar in listaPreRequisitoGeneralEliminar)
                        {
                            oportunidadPrerequisitoGeneralRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }


                    if (ListaPrerequisitoGeneral == null)
                    {
                        ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaPrerequisitoGeneral.Respuesta = item.Respuesta;
                    ListaPrerequisitoGeneral.Completado = item.Completado;
                    ListaPrerequisitoGeneral.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoGeneral.UsuarioModificacion = dto.Usuario;
                    if (!ListaPrerequisitoGeneral.HasErrors)
                    {
                        OportunidadCompetidor.ListaPrerequisitoGeneral.Add(ListaPrerequisitoGeneral);
                    }
                    else
                    {
                        return BadRequest(ListaPrerequisitoGeneral.GetErrors(null));
                    }
                }
                OportunidadCompetidor.ListaPrerequisitoEspecifico = new List<OportunidadPrerequisitoEspecificoBO>();
                OportunidadPrerequisitoEspecificoBO ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO();


                var listaPreRequisitoEspecificoAgrupado = dto.DatosCompuesto.ListaPrerequisitoEspecifico.GroupBy(x => x.IdProgramaGeneralBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaPreRequisitoEspecificoAgrupado)
                {
                    ListaPrerequisitoEspecifico = oportunidadPrerequisitoEspecificoRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio);
                    if (ListaPrerequisitoEspecifico != null)
                    {
                        var listaPreRequisitoEspecificoEliminar = oportunidadPrerequisitoEspecificoRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralBeneficio && x.Id != ListaPrerequisitoEspecifico.Id).ToList();
                        foreach (var itemEliminar in listaPreRequisitoEspecificoEliminar)
                        {
                            oportunidadPrerequisitoEspecificoRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }

                    if (ListaPrerequisitoEspecifico == null)
                    {
                        ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaPrerequisitoEspecifico.Respuesta = item.Respuesta;
                    ListaPrerequisitoEspecifico.Completado = item.Completado;
                    ListaPrerequisitoEspecifico.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.UsuarioModificacion = dto.Usuario;
                    if (!ListaPrerequisitoEspecifico.HasErrors)
                    {
                        OportunidadCompetidor.ListaPrerequisitoEspecifico.Add(ListaPrerequisitoEspecifico);
                    }
                    else
                    {
                        return BadRequest(ListaPrerequisitoEspecifico.GetErrors(null));
                    }
                }

                OportunidadCompetidor.ListaBeneficio = new List<OportunidadBeneficioBO>();
                OportunidadBeneficioBO ListaBeneficio = new OportunidadBeneficioBO();
                var listaBeneficioAgrupado = dto.DatosCompuesto.ListaBeneficio.GroupBy(x => x.IdBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaBeneficioAgrupado)
                {
                    ListaBeneficio = oportunidadBeneficioRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdBeneficio == item.IdBeneficio);

                    if (ListaBeneficio != null)
                    {
                        var listaBeneficioEliminar = oportunidadBeneficioRepositorio.GetBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdBeneficio == item.IdBeneficio && x.Id != ListaBeneficio.Id).ToList();
                        foreach (var itemEliminar in listaBeneficioEliminar)
                        {
                            oportunidadBeneficioRepositorio.Delete(itemEliminar.Id, dto.Usuario);
                        }
                    }

                    if (ListaBeneficio == null)
                    {
                        ListaBeneficio = new OportunidadBeneficioBO
                        {
                            IdOportunidadCompetidor = item.IdOportunidadCompetidor,
                            IdBeneficio = item.IdBeneficio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario,
                            Estado = true
                        };
                    }
                    ListaBeneficio.Respuesta = item.Respuesta;
                    ListaBeneficio.Completado = item.Completado;
                    ListaBeneficio.FechaModificacion = DateTime.Now;
                    ListaBeneficio.UsuarioModificacion = dto.Usuario;
                    if (!ListaBeneficio.HasErrors)
                    {
                        OportunidadCompetidor.ListaBeneficio.Add(ListaBeneficio);
                    }
                    else
                    {
                        return BadRequest(ListaBeneficio.GetErrors(null));
                    }
                }*/
                //======================================
                ProgramaGeneralBeneficioRespuestaRepositorio _repBeneficioAlternoRespuesta = new ProgramaGeneralBeneficioRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioRespuestaBO beneficioAlterno = new ProgramaGeneralBeneficioRespuestaBO();
                var listaBeneficioAlternoAgrupado = dto.DatosCompuesto.ListaBeneficioAlterno.GroupBy(x => x.IdBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaBeneficioAlternoAgrupado)
                {
                    beneficioAlterno = _repBeneficioAlternoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralBeneficio == item.IdBeneficio);

                    if (beneficioAlterno != null)
                    {
                        beneficioAlterno.Respuesta = item.Respuesta;
                        beneficioAlterno.UsuarioModificacion = dto.Usuario;
                        beneficioAlterno.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Update(beneficioAlterno);
                    }
                    else
                    {
                        ProgramaGeneralBeneficioRespuestaBO beneficioAlternoV2 = new ProgramaGeneralBeneficioRespuestaBO();
                        beneficioAlternoV2.IdOportunidad = item.IdOportunidad;
                        beneficioAlternoV2.IdProgramaGeneralBeneficio = item.IdBeneficio;
                        beneficioAlternoV2.Respuesta = item.Respuesta;
                        beneficioAlternoV2.Estado = true;
                        beneficioAlternoV2.UsuarioCreacion = dto.Usuario;
                        beneficioAlternoV2.UsuarioModificacion = dto.Usuario;
                        beneficioAlternoV2.FechaCreacion = DateTime.Now;
                        beneficioAlternoV2.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Insert(beneficioAlternoV2);
                    }
                }

                ProgramaGeneralMotivacionRespuestaRepositorio _repMotiviacionRespuesta = new ProgramaGeneralMotivacionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralMotivacionRespuestaBO motivacionRespuesta = new ProgramaGeneralMotivacionRespuestaBO();
                var listaMotivacionRespuestaAgrupado = dto.DatosCompuesto.ListaMotivacion.GroupBy(x => x.IdMotivacion).Select(x => x.First()).ToList();
                foreach (var item in listaMotivacionRespuestaAgrupado)
                {
                    motivacionRespuesta = _repMotiviacionRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralMotivacion == item.IdMotivacion);

                    if (motivacionRespuesta != null)
                    {
                        motivacionRespuesta.Respuesta = item.Respuesta;
                        motivacionRespuesta.UsuarioModificacion = dto.Usuario;
                        motivacionRespuesta.FechaModificacion = DateTime.Now;
                        _repMotiviacionRespuesta.Update(motivacionRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralMotivacionRespuestaBO motivacionRespuestaAlterno = new ProgramaGeneralMotivacionRespuestaBO();
                        motivacionRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        motivacionRespuestaAlterno.IdProgramaGeneralMotivacion = item.IdMotivacion;
                        motivacionRespuestaAlterno.Respuesta = item.Respuesta;
                        motivacionRespuestaAlterno.Estado = true;
                        motivacionRespuestaAlterno.UsuarioCreacion = dto.Usuario;
                        motivacionRespuestaAlterno.UsuarioModificacion = dto.Usuario;
                        motivacionRespuestaAlterno.FechaCreacion = DateTime.Now;
                        motivacionRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repMotiviacionRespuesta.Insert(motivacionRespuestaAlterno);
                    }
                }

                PublicoObjetivoRespuestaRepositorio _repPublicoObjetivoRespuesta = new PublicoObjetivoRespuestaRepositorio(_integraDBContext);
                PublicoObjetivoRespuestaBO publicoObjetivoRespuesta = new PublicoObjetivoRespuestaBO();
                var listaPublicoObjetivoRespuestaAgrupado = dto.DatosCompuesto.ListaPublicoObjetivo.GroupBy(x => x.IdPublicoObjetivo).Select(x => x.First()).ToList();
                foreach (var item in listaPublicoObjetivoRespuestaAgrupado)
                {
                    publicoObjetivoRespuesta = _repPublicoObjetivoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdDocumentoSeccionPw == item.IdPublicoObjetivo);

                    if (publicoObjetivoRespuesta != null)
                    {
                        publicoObjetivoRespuesta.NivelCumplimiento = item.Respuesta;
                        publicoObjetivoRespuesta.UsuarioModificacion = dto.Usuario;
                        publicoObjetivoRespuesta.FechaModificacion = DateTime.Now;
                        _repPublicoObjetivoRespuesta.Update(publicoObjetivoRespuesta);
                    }
                    else
                    {
                        PublicoObjetivoRespuestaBO publicoObjetivoRespuestaAlterno = new PublicoObjetivoRespuestaBO();
                        publicoObjetivoRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        publicoObjetivoRespuestaAlterno.IdDocumentoSeccionPw = item.IdPublicoObjetivo;
                        publicoObjetivoRespuestaAlterno.NivelCumplimiento = item.Respuesta;
                        publicoObjetivoRespuestaAlterno.Estado = true;
                        publicoObjetivoRespuestaAlterno.UsuarioCreacion = dto.Usuario;
                        publicoObjetivoRespuestaAlterno.UsuarioModificacion = dto.Usuario;
                        publicoObjetivoRespuestaAlterno.FechaCreacion = DateTime.Now;
                        publicoObjetivoRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repPublicoObjetivoRespuesta.Insert(publicoObjetivoRespuestaAlterno);
                    }
                }

                ProgramaGeneralCertificacionRespuestaRepositorio _repCertificacionRespuesta = new ProgramaGeneralCertificacionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralCertificacionRespuestaBO certificacionRespuesta = new ProgramaGeneralCertificacionRespuestaBO();
                var listaCertificacionRespuestaAgrupado = dto.DatosCompuesto.ListaCertificacion.GroupBy(x => x.IdCertificacion).Select(x => x.First()).ToList();
                foreach (var item in listaCertificacionRespuestaAgrupado)
                {
                    certificacionRespuesta = _repCertificacionRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralCertificacion == item.IdCertificacion);

                    if (certificacionRespuesta != null)
                    {
                        certificacionRespuesta.Respuesta = item.Respuesta;
                        certificacionRespuesta.UsuarioModificacion = dto.Usuario;
                        certificacionRespuesta.FechaModificacion = DateTime.Now;
                        _repCertificacionRespuesta.Update(certificacionRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralCertificacionRespuestaBO certificacionRespuestaAlterno = new ProgramaGeneralCertificacionRespuestaBO();
                        certificacionRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        certificacionRespuestaAlterno.IdProgramaGeneralCertificacion = item.IdCertificacion;
                        certificacionRespuestaAlterno.Respuesta = item.Respuesta;
                        certificacionRespuestaAlterno.Estado = true;
                        certificacionRespuestaAlterno.UsuarioCreacion = dto.Usuario;
                        certificacionRespuestaAlterno.UsuarioModificacion = dto.Usuario;
                        certificacionRespuestaAlterno.FechaCreacion = DateTime.Now;
                        certificacionRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repCertificacionRespuesta.Insert(certificacionRespuestaAlterno);
                    }
                }

                ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio _repProblemaRespuesta = new ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralProblemaDetalleSolucionRespuestaBO problemaRespuesta = new ProgramaGeneralProblemaDetalleSolucionRespuestaBO();
                var listaProblemaRespuestaAgrupado = dto.DatosCompuesto.ListaSolucionesAlterno.GroupBy(x => x.IdProblema).Select(x => x.First()).ToList();
                foreach (var item in listaProblemaRespuestaAgrupado)
                {
                    problemaRespuesta = _repProblemaRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralProblemaDetalleSolucion == item.IdProblema);

                    if (problemaRespuesta != null)
                    {
                        problemaRespuesta.EsSeleccionado = item.Seleccionado;
                        problemaRespuesta.EsSolucionado = item.Solucionado;
                        problemaRespuesta.UsuarioModificacion = dto.Usuario;
                        problemaRespuesta.FechaModificacion = DateTime.Now;
                        _repProblemaRespuesta.Update(problemaRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralProblemaDetalleSolucionRespuestaBO problemaRespuestaAlterno = new ProgramaGeneralProblemaDetalleSolucionRespuestaBO();
                        problemaRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        problemaRespuestaAlterno.IdProgramaGeneralProblemaDetalleSolucion = item.IdProblema;
                        problemaRespuestaAlterno.EsSeleccionado = item.Seleccionado;
                        problemaRespuestaAlterno.EsSolucionado = item.Solucionado;
                        problemaRespuestaAlterno.Estado = true;
                        problemaRespuestaAlterno.UsuarioCreacion = dto.Usuario;
                        problemaRespuestaAlterno.UsuarioModificacion = dto.Usuario;
                        problemaRespuestaAlterno.FechaCreacion = DateTime.Now;
                        problemaRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repProblemaRespuesta.Insert(problemaRespuestaAlterno);
                    }
                }

                ProgramaGeneralPrerequisitoRespuestaRepositorio _repPrerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaBO();
                var listaPrerequisitoRespuestaAgrupado = dto.DatosCompuesto.ListaPrerequisitoGeneralAlterno.GroupBy(x => x.IdProgramaGeneralPrerequisito).Select(x => x.First()).ToList();
                foreach (var item in listaPrerequisitoRespuestaAgrupado)
                {
                    prerequisitoRespuesta = _repPrerequisitoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralPrerequisito);

                    if (prerequisitoRespuesta != null)
                    {
                        prerequisitoRespuesta.Respuesta = item.Respuesta;
                        prerequisitoRespuesta.UsuarioModificacion = dto.Usuario;
                        prerequisitoRespuesta.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Update(prerequisitoRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuestaAlterno = new ProgramaGeneralPrerequisitoRespuestaBO();
                        prerequisitoRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        prerequisitoRespuestaAlterno.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralPrerequisito;
                        prerequisitoRespuestaAlterno.Respuesta = item.Respuesta;
                        prerequisitoRespuestaAlterno.Estado = true;
                        prerequisitoRespuestaAlterno.UsuarioCreacion = dto.Usuario;
                        prerequisitoRespuestaAlterno.UsuarioModificacion = dto.Usuario;
                        prerequisitoRespuestaAlterno.FechaCreacion = DateTime.Now;
                        prerequisitoRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Insert(prerequisitoRespuestaAlterno);
                    }
                }

                //======================================
                detalleOportunidadCompetidorRepositorio.DeleteLogicoPorOportunidadCompetidor(dto.DatosCompuesto.OportunidadCompetidor.Id, dto.Usuario, dto.DatosCompuesto.ListaCompetidor);
                OportunidadCompetidor.ListaCompetidor = new List<DetalleOportunidadCompetidorBO>();
                DetalleOportunidadCompetidorBO ListaCompetidor = new DetalleOportunidadCompetidorBO();
                foreach (var item in dto.DatosCompuesto.ListaCompetidor)
                {
                    ListaCompetidor = detalleOportunidadCompetidorRepositorio.FirstBy(x => x.IdOportunidadCompetidor == OportunidadCompetidor.Id && x.IdCompetidor == item);
                    if (ListaCompetidor == null)
                    {
                        ListaCompetidor = new DetalleOportunidadCompetidorBO
                        {
                            IdOportunidadCompetidor = 0,
                            IdCompetidor = item,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = dto.Usuario
                        };
                    }
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioModificacion = dto.Usuario;
                    if (!ListaCompetidor.HasErrors)
                    {
                        OportunidadCompetidor.ListaCompetidor.Add(ListaCompetidor);
                    }
                    else
                    {
                        return BadRequest(ListaCompetidor.GetErrors(null));
                    }
                }
                /*Oportunidad.ListaSoluciones = new List<SolucionClienteByActividadBO>();
                foreach (var item in dto.DatosCompuesto.ListaSoluciones)
                {
                    var ListaSoluciones = new SolucionClienteByActividadBO
                    {
                        IdOportunidad = item.IdOportunidad,
                        IdActividadDetalle = item.IdActividadDetalle,
                        IdCausa = item.IdCausa,
                        IdPersonal = item.IdPersonal,
                        Solucionado = item.Solucionado,
                        IdProblemaCliente = item.IdProblemaCliente,
                        OtroProblema = item.OtroProblema,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = dto.Usuario,
                        UsuarioModificacion = dto.Usuario,
                        Estado = true
                    };

                    if (!ListaSoluciones.HasErrors)
                    {
                        Oportunidad.ListaSoluciones.Add(ListaSoluciones);
                    }
                    else
                    {
                        return BadRequest(ListaSoluciones.GetErrors(null));
                    }
                }*/

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    ActividadNueva.LlamadaActividad = null;
                    Oportunidad.FinalizarActividadAlterno(false, dto.datosOportunidad, dto.ActividadAntigua.IdOcurrenciaActividad);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                            {
                                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 0);
                                }
                                else
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 1);

                                }
                            }

                            _repHoraBloqueada.Insert(horaBloqueada);

                            Oportunidad.ProgramaActividadAlterno();

                            _repActividadDetalle.Insert(Oportunidad.ActividadNuevaProgramarActividad);
                            Oportunidad.IdActividadDetalleUltima = Oportunidad.ActividadNuevaProgramarActividad.Id;
                            Oportunidad.ActividadNuevaProgramarActividad = null;
                            _repOportunidad.Update(Oportunidad);

                            //_repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);

                            if (dto.filtro.Tipo == "manual")
                            {
                                if (dto.datosOportunidad.IdFaseOportunidad != Oportunidad.IdFaseOportunidad)
                                {
                                    if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                                    {
                                        //if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad) && ActividadAntigua.IdOcurrencia == ValorEstatico.IdOcurrenciaConfirmaPagoIs)
                                        if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                        {
                                            try
                                            {
                                                //Enviar plantilla de condiciones
                                                //1246    Nuevas Condiciones y Características -Perú
                                                //1247    Nuevas Condiciones y Características -Colombia
                                                //1401    Condiciones y Características - Mexico
                                                //1402    Contrato de uso de datos biométricos por voz  - México

                                                var _repAlumno = new AlumnoRepositorio(_integraDBContext);

                                                var alumno = _repAlumno.FirstById(Oportunidad.IdAlumno);
                                                var _idPlantilla = 0;
                                                var _idPlantilla2 = 0;

                                                if (alumno.IdCodigoPais == 57)//Colombia
                                                {
                                                    _idPlantilla = 1247;
                                                }
                                                else if (alumno.IdCodigoPais == 51)//Peru
                                                {
                                                    _idPlantilla = 1246;
                                                }
                                                else if (alumno.IdCodigoPais == 52)//Mexico
                                                {
                                                    _idPlantilla = 1401;
                                                    _idPlantilla2 = 1402;
                                                }

                                                if (_idPlantilla != 0)
                                                {
                                                    var Objeto = new PlantillaPwBO(_integraDBContext)
                                                    {
                                                        IdOportunidad = Oportunidad.Id,
                                                        IdPlantilla = _idPlantilla
                                                    };
                                                    var _repPgeneralTipoDescuento = new PgeneralTipoDescuentoRepositorio(_integraDBContext);
                                                    Objeto.GetValorEtiqueta(Oportunidad.IdCentroCosto.Value, Oportunidad.IdFaseOportunidad, Oportunidad.Id);

                                                    var DatosOportunidad = Objeto.ObtenerDatosOportunidad(Oportunidad.Id);
                                                    string FechaInicioPrograma = "";

                                                    var Promocion = _repPgeneralTipoDescuento.FirstBy(w => w.Estado == true && w.IdPgeneral == DatosOportunidad.IdPgeneral.Value && w.IdTipoDescuento == 143, y => new { y.FlagPromocion });
                                                    if (Promocion != null)
                                                    {
                                                        DatosOportunidad.Promocion25 = Promocion.FlagPromocion;
                                                    }
                                                    FechaInicioPrograma = Objeto.ObtenerFechaInicioPrograma(DatosOportunidad.IdPgeneral.Value, DatosOportunidad.IdCentroCosto.Value);
                                                    //var etiquetaDuracionYHorarios = Objeto.DuracionYHorarios(DatosOportunidad.IdCentroCosto);
                                                    DatosOportunidad.CostoTotalConDescuento = Objeto.ObtenerCostoTotalConDescuento(Oportunidad.Id);
                                                    Objeto.ObtenerDatosProgramaGeneral(DatosOportunidad.IdPgeneral.Value);
                                                    //reemplazo
                                                    Objeto.ReemplazarEtiquetas();

                                                    var emailCalculado = Objeto.EmailReemplazado;
                                                    List<string> correosPersonalizadosCopia = new List<string>
                                                    {
                                                        "grabaciones@bsginstitute.com",
                                                         "mzegarraj@bsginstitute.com"
                                                    };

                                                    List<string> correosPersonalizados = new List<string>
                                                    {
                                                        Objeto.DatosOportunidadAlumno.OportunidadAlumno.Email1
                                                    };

                                                    var mailDataPersonalizado = new TMKMailDataDTO
                                                    {
                                                        Sender = "matriculas@bsginstitute.com",
                                                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                                        Subject = emailCalculado.Asunto,
                                                        Message = emailCalculado.CuerpoHTML,
                                                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                                                        Bcc = "",
                                                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                                                    };
                                                    var mailServie = new TMK_MailServiceImpl();
                                                    mailServie.SetData(mailDataPersonalizado);
                                                    mailServie.SendMessageTask();
                                                    var _repDocumentoEnviadoWebPw = new DocumentoEnviadoWebPwRepositorio(_integraDBContext);
                                                    var documentoEnviadoWebPwBO = new DocumentoEnviadoWebPwBO()
                                                    {
                                                        IdAlumno = Oportunidad.IdAlumno,
                                                        Nombre = "BSG Institute - Condiciones y Características",
                                                        IdPespecifico = Objeto.DatosOportunidadAlumno.IdPEspecifico,
                                                        FechaEnvio = DateTime.Now,
                                                        FechaCreacion = DateTime.Now,
                                                        FechaModificacion = DateTime.Now,
                                                        UsuarioCreacion = dto.Usuario,
                                                        UsuarioModificacion = dto.Usuario,
                                                        Estado = true
                                                    };
                                                    if (!documentoEnviadoWebPwBO.HasErrors)
                                                    {
                                                        _repDocumentoEnviadoWebPw.Insert(documentoEnviadoWebPwBO);
                                                    }


                                                    if (_idPlantilla2 != 0)
                                                    {
                                                        //reemplazo 2
                                                        Objeto.IdPlantilla = _idPlantilla2;
                                                        Objeto.ReemplazarEtiquetas();
                                                        var emailCalculado2 = Objeto.EmailReemplazado;

                                                        var mailDataPersonalizado2 = new TMKMailDataDTO
                                                        {
                                                            Sender = "matriculas@bsginstitute.com",
                                                            Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                                            Subject = emailCalculado2.Asunto,
                                                            Message = emailCalculado2.CuerpoHTML,
                                                            Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                                                            Bcc = "ccrispin@bsginstitute.com",
                                                            AttachedFiles = emailCalculado2.ListaArchivosAdjuntos
                                                        };
                                                        var mailServie2 = new TMK_MailServiceImpl();
                                                        mailServie2.SetData(mailDataPersonalizado2);
                                                        mailServie2.SendMessageTask();

                                                    }


                                                }

                                            }
                                            catch (Exception e)
                                            {
                                            }


                                            if (ActividadAntigua.IdOcurrenciaAlterno == ValorEstatico.IdOcurrenciaConfirmaPagoIs || ActividadAntigua.IdOcurrenciaAlterno == ValorEstatico.IdOcurrenciaIsSinLlamada)
                                            {
                                                var comprobantePago = new ComprobantePagoOportunidadBO
                                                {
                                                    IdContacto = dto.ComprobantePago.IdContacto,
                                                    Nombres = dto.ComprobantePago.Nombres,
                                                    Apellidos = dto.ComprobantePago.Apellidos,
                                                    Celular = dto.ComprobantePago.Celular,
                                                    Dni = dto.ComprobantePago.Dni == null ? "" : dto.ComprobantePago.Dni,
                                                    Correo = dto.ComprobantePago.Correo,
                                                    NombrePais = dto.ComprobantePago.NombrePais,
                                                    IdPais = dto.ComprobantePago.IdPais,
                                                    NombreCiudad = dto.ComprobantePago.NombreCiudad == null ? "" : dto.ComprobantePago.NombreCiudad,
                                                    TipoComprobante = dto.ComprobantePago.TipoComprobante,
                                                    NroDocumento = dto.ComprobantePago.NroDocumento != null ? dto.ComprobantePago.NroDocumento : "",
                                                    NombreRazonSocial = dto.ComprobantePago.NombreRazonSocial,
                                                    Direccion = dto.ComprobantePago.Direccion != null ? dto.ComprobantePago.Direccion : "",
                                                    BitComprobante = dto.ComprobantePago.BitComprobante,
                                                    IdOcurrencia = dto.ComprobantePago.IdOcurrencia,
                                                    IdAsesor = dto.ComprobantePago.IdAsesor,
                                                    IdOportunidad = dto.ComprobantePago.IdOportunidad,
                                                    Comentario = dto.ComprobantePago.Comentario,
                                                    FechaCreacion = DateTime.Now,
                                                    FechaModificacion = DateTime.Now,
                                                    UsuarioCreacion = dto.Usuario,
                                                    UsuarioModificacion = dto.Usuario,
                                                    Estado = true
                                                };

                                                var result = _repComprobantePagoOportunidad.Insert(comprobantePago);
                                                if (result)
                                                {
                                                    //Enviar Correo Finanzas  Boleta = 0 && Factura = 1
                                                    List<string> correos = new List<string>();
                                                    correos.Add("atrelles@bsginstitute.com");
                                                    //correos.Add("pteran@bsginstitute.com");
                                                    correos.Add("mlopezo@bsginstitute.com");
                                                    correos.Add("mzegarraj@bsginstitute.com");
                                                    correos.Add("ccrispin@bsginstitute.com");

                                                    string mensaje = comprobantePago.MensajeEmailComprobantePago();

                                                    //TMK_ImapServiceImpl correo = new TMK_ImapServiceImpl("jcayo@bsginstitute.com", "johancayocarrasco");

                                                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                                                    TMKMailDataDTO mailData = new TMKMailDataDTO
                                                    {
                                                        Sender = "jcayo@bsginstitute.com",
                                                        Recipient = string.Join(",", correos),
                                                        Subject = "BSG INSTITUTE - Datos Alumno en IS ",
                                                        Message = mensaje,
                                                        Cc = "",
                                                        Bcc = "",
                                                        AttachedFiles = null,
                                                        RemitenteC = comprobantePago.Nombres
                                                    };

                                                    Mailservice.SetData(mailData);

                                                    List<TMKMensajeIdDTO> MensajeIdDTO = Mailservice.SendMessageTask();

                                                    //Correos.envio_email("jcayo@bsginstitute.com", comprobantePago.nombresContacto + " " + comprobantePago.apellidos, "BSG INSTITUTE - Datos Alumno en IS ", mensaje, correos);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            else if (dto.filtro.Tipo == "automatica")
                            {
                                ReprogramacionCabeceraBO reprogramacionCabecera = _repReprogramacionCabecera.ObtenerReprogramacionCabecera(dto.filtro.IdActividadCabecera, dto.filtro.IdCategoria);
                                idReprogramacionCabecera = reprogramacionCabecera.Id;
                                ReprogramacionCabeceraPersonalBO reprogramacionCabeceraPersonal = _repReprogramacionCabeceraPersonal.FirstBy(w => w.IdActividadCabecera == dto.filtro.IdActividadCabecera && w.IdCategoriaOrigen == dto.filtro.IdCategoria && w.IdPersonal == dto.filtro.IdPersonal.Value && w.FechaReprogramacion.Date == DateTime.Now.Date);
                                //ReprogramacionCabeceraPersonalBO reprogramacionCabeceraPersonal = _repReprogramacionCabeceraPersonal.ObtenerReprogramacionCabeceraPersonal(dto.filtro.IdActividadCabecera, dto.filtro.IdCategoria, dto.filtro.IdPersonal.Value, DateTime.Now.Date);

                                if (reprogramacionCabeceraPersonal == null)
                                {
                                    reprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalBO
                                    {
                                        IdActividadCabecera = dto.filtro.IdActividadCabecera,
                                        IdCategoriaOrigen = dto.filtro.IdCategoria,
                                        IdPersonal = dto.filtro.IdPersonal.Value,
                                        ReproDia = 1,
                                        FechaReprogramacion = DateTime.Now.Date,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.filtro.Usuario,
                                        UsuarioModificacion = dto.filtro.Usuario,
                                        Estado = true
                                    };
                                    _repReprogramacionCabeceraPersonal.Insert(reprogramacionCabeceraPersonal);
                                    //_repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);
                                }
                                else
                                {
                                    if (reprogramacionCabecera == null)
                                    {
                                        return null;
                                    }
                                    else
                                    {
                                        if (reprogramacionCabecera.MaxReproPorDia > reprogramacionCabeceraPersonal.ReproDia)
                                        {
                                            reprogramacionCabeceraPersonal.ReproDia += 1;
                                            reprogramacionCabeceraPersonal.FechaReprogramacion = DateTime.Now.Date;
                                            reprogramacionCabeceraPersonal.FechaModificacion = DateTime.Now;
                                            reprogramacionCabeceraPersonal.UsuarioModificacion = dto.filtro.Usuario;
                                            reprogramacionCabeceraPersonal.Estado = true;
                                            _repReprogramacionCabeceraPersonal.Update(reprogramacionCabeceraPersonal);
                                        }
                                    }
                                }
                            }
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "jcayo@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = "Error FinalizarYProgramarActividad Transaction",
                                Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    try
                    {
                        //para poder medir la calidadde la llamada//08/08/2019
                        CalidadLlamadaLogRepositorio _repCalidadLlamadaLog = new CalidadLlamadaLogRepositorio(_integraDBContext);
                        var calidadLlamadaLog = new CalidadLlamadaLogBO
                        {
                            IdCalidadLlamada = dto.CalidadLlamada.IdCalidadLlamada,
                            IdProblemaLlamada = dto.CalidadLlamada.IdProblemaLlamada,
                            IdActividadDetalle = dto.ActividadAntigua.Id,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = dto.filtro.Usuario,
                            UsuarioModificacion = dto.filtro.Usuario,
                            Estado = true
                        };
                        _repCalidadLlamadaLog.Insert(calidadLlamadaLog);
                    }
                    catch (Exception ex)
                    {
                        List<string> correos = new List<string>();
                        correos.Add("sistemas@bsginstitute.com");

                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO
                        {
                            Sender = "jcayo@bsginstitute.com",
                            Recipient = string.Join(",", correos),
                            Subject = "Error FinalizarYProgramarActividad CalidadLlamada",
                            Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                            Cc = "",
                            Bcc = "",
                            AttachedFiles = null
                        };

                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }

                CompuestoActividadEjecutadaDTO realizada = new CompuestoActividadEjecutadaDTO();
                try
                {
                    realizada = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(dto.ActividadAntigua.Id);
                }
                catch (Exception ex)
                {
                    realizada = new CompuestoActividadEjecutadaDTO();

                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Jojan 2",
                        Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                return Ok(new { realizada, idHoraBloqueada = horaBloqueada.Id, idOportunidad = Oportunidad.Id, IdReprogramacionCabecera = idReprogramacionCabecera });
            }
            catch (Exception ex)
            {
                try
                {
                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error FinalizarYProgramarActividad General",
                        Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                catch (Exception)
                {
                }

                return BadRequest(ex.Message);
            }
        }
    }
}
