using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using Newtonsoft.Json;
using static BSI.Integra.Aplicacion.Base.BO.Enum;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/CampaniaMailing
    /// Autor: Gian Miranda
    /// Fecha: 15/01/2021
    /// <summary>
    /// Configura y procesa los elementos de una campania mailing (listas, filtro asociado, areas y prioridades)
    /// </summary>
    [Route("api/CampaniaMailing")]
    public class CampaniaMailingController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public CampaniaMailingController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de categoria origen
        /// </summary>
        /// <returns>Lista de objetos de clase CategoriaOrigenFiltroDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCategoriaOrigen()
        {
            try
            {
                var _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
                
                return Ok(_repCategoriaOrigen.ObtenerListaCategoriaOrigen());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de todas las horas registradas en mkt.T_Hora
        /// </summary>
        /// <returns>Lista de DTO de objetos HoraDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaHora()
        {
            try
            {
                var _repHora = new HoraRepositorio();
                return Ok(_repHora.ObtenerListaHora());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Todos los filtros activos para el modulo
        /// </summary>
        /// <returns>Objeto de clase FiltroDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                CampaniaMailingRepositorio _campaniaMailingRepositorio = new CampaniaMailingRepositorio();
                return Ok(_campaniaMailingRepositorio.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la grilla de las listas campanias mailing del modulo
        /// </summary>
        /// <returns>Lista de objetos de clase CampaniaMailingDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCampaniaMailingGrid()
        {
            try
            {
                CampaniaMailingRepositorio _campaniaMailingRepositorio = new CampaniaMailingRepositorio();
                return Ok(_campaniaMailingRepositorio.ObtenerListaCampaniaMailing());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las campanias mailing detalle asociadas a una campania mailing
        /// </summary>
        /// <param name="Id">Id de la lista campania mailing detalle</param>
        /// <returns>Lista de DTO para todos los filtros</returns>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerListaCampaniaMailingDetalle(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaMailingBO CampaniaMailing = new CampaniaMailingBO(_integraDBContext)
                {
                    Id = Id
                };
                return Ok(CampaniaMailing.ObtenerDetalle());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las listas con todas las areas, subareas y pgeneral para la carga inicial
        /// </summary>
        /// <returns>Objeto de clase CombosAreasSubAreasMailchimpDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObteneAreasSubAreas()
        {
            try
            {
                CombosAreasSubAreasMailchimpDTO combo = new CombosAreasSubAreasMailchimpDTO();
                AreaCapacitacionRepositorio repAreaCapacitacion = new AreaCapacitacionRepositorio();
                SubAreaCapacitacionRepositorio repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio();
                PgeneralRepositorio _pgeneralRepositorio = new PgeneralRepositorio();

                combo.Areas = repAreaCapacitacion.ObtenerTodoFiltro();
                combo.SubAreas = repSubAreaCapacitacion.ObtenerSubAreasParaFiltro();
                combo.ProgramaGeneral = _pgeneralRepositorio.ObtenerProgramaSubAreaFiltro();
                return Ok(combo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista con todas las plantillas activas
        /// </summary>
        /// <returns>Lista de objetos de clase PlantillaDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPlantilla()
        {
            try
            {
                PlantillaRepositorio _plantillaRepositorio = new PlantillaRepositorio();
                return Ok(_plantillaRepositorio.ObtenerListaPlantillaMarketing());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de todos los remitentes activos para la configuracion de la lista a procesar
        /// </summary>
        /// <returns>Lista de DTO de objetos RemitenteMailingDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaRemitenteMailing()
        {
            try
            {
                var _repRemitenteMailing = new RemitenteMailingRepositorio();
                return Ok(_repRemitenteMailing.ObtenerListaRemitenteMailing());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de todos los remitentes activos asesores para la configuracion de la lista a procesar
        /// </summary>
        /// <returns>Lista de objetos de clase RemitenteMailingAsesorDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaRemitenteMailingAsesor()
        {
            try
            {
                RemitenteMailingAsesorRepositorio _repRemitenteMailingAsesor = new RemitenteMailingAsesorRepositorio();

                return Ok(_repRemitenteMailingAsesor.ObtenerListaRemitenteMailingAsesor());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de todos los programas generales a usar en el procesamiento
        /// </summary>
        /// <returns>Lista de objetos de tipo PGeneralFiltroDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPGeneral()
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

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion necesaria de todos los filtros segmento
        /// </summary>
        /// <returns>Objeto de clase FiltroIdNombreDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroSegmento()
        {
            try
            {
                FiltroSegmentoRepositorio _filtroSegmentoRepositorio = new FiltroSegmentoRepositorio();
                return Ok(_filtroSegmentoRepositorio.GetFiltroIdNombre());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los centros de costo activos
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCentroCosto()
        {
            try
            {
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                return Ok(_repCentroCosto.ObtenerParaFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerResponsables()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                return Ok(_repPersonal.ObtenerPersonalMarketingFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de probabilidades
        /// </summary>
        /// <returns>Lista de objetos de clase ProbabilidadRegistroPwFiltroDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerProbabilidades()
        {
            try
            {
                ProbabilidadRegistroPwRepositorio _repProbabilidadRegistroPW = new ProbabilidadRegistroPwRepositorio();
                return Ok(_repProbabilidadRegistroPW.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCategoriaObjetoFiltro()
        {
            try
            {
                var _repCategoriaObjetoFiltro = new CategoriaObjetoFiltroRepositorio(_integraDBContext);
                var listaCategoriaObjeto = _repCategoriaObjetoFiltro.ObtenerParaConjuntoAnuncio();
                var listaCategoriaObjetoValida = new List<int>() { 1, 2, 3 };
                listaCategoriaObjeto = listaCategoriaObjeto.Where(x => listaCategoriaObjetoValida.Contains(x.Id)).ToList();
                return Ok(listaCategoriaObjeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina una campania mailing especificada, guardando el usuario que efectuo tal accion
        /// </summary>
        /// <param name="IdCampaniaMailing">Id de la Campania Mailing a eliminar</param>
        /// <param name="Usuario">Usuario que ejecuta el eliminado</param>
        /// <returns>DTO con informacion basica de filtros segmento</returns>
        [Route("[Action]/{IdCampaniaMailing}/{Usuario}")]
        [HttpGet]
        public ActionResult Eliminar(int IdCampaniaMailing, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaMailingRepositorio _campaniaMailingRepositorio = new CampaniaMailingRepositorio();
                _campaniaMailingRepositorio.Delete(IdCampaniaMailing, Usuario);
                return Ok(new { Resultado = "Se elimino correctamente", Id = IdCampaniaMailing });
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = e.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta una campania mailing
        /// </summary>
        /// <param name="CampaniaMailingDTO">DTO con la informacion necesaria de la campania mailing</param>
        /// <returns>Mensaje de confirmacion y el id de la campania mailing que se inserto</returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CampaniaMailingDTO CampaniaMailingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaMailingBO campaniaMailingBO = new CampaniaMailingBO(_integraDBContext);
                CampaniaMailingRepositorio _campaniaMailingRepositorio = new CampaniaMailingRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    campaniaMailingBO.InsertarOActualizar(CampaniaMailingDTO);
                    if (campaniaMailingBO.HasErrors)
                    {
                        return BadRequest(campaniaMailingBO.GetErrors(null));
                    }
                    _campaniaMailingRepositorio.Insert(campaniaMailingBO);
                    scope.Complete();
                    return Ok(new { Resultado = "Se Inserto Correctamente", campaniaMailingBO.Id });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = "ERROR", e.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una campania mailing especifica
        /// </summary>
        /// <param name="CampaniaMailingDTO">DTO de la campania mailing a actualizar</param>
        /// <returns>Mensaje de confirmacion y el id de la campania mailing que se inserto</returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CampaniaMailingDTO CampaniaMailingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaMailingBO campaniaMailingBO = new CampaniaMailingBO(_integraDBContext);
                CampaniaMailingRepositorio _campaniaMailingRepositorio = new CampaniaMailingRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    campaniaMailingBO.InsertarOActualizar(CampaniaMailingDTO);
                    if (campaniaMailingBO.HasErrors)
                    {
                        return BadRequest(campaniaMailingBO.GetErrors(null));
                    }
                    _campaniaMailingRepositorio.Update(campaniaMailingBO);
                    scope.Complete();
                    return Ok(new { Resultado = "Se Actualizo Correctamente", CampaniaMailingDTO.Id });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = "ERROR", e.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Envia las listas ya procesadas a las listas Mailchimp ya configuradas
        /// </summary>
        /// <param name="IdCampaniaMailing">Id de la campania mailing que se va a enviar</param>
        /// <param name="Usuario">Usuario que ejecuta dicha accion</param>
        /// <returns>Mensaje con la cantidad de listas detalle que se envio</returns>
        [Route("[Action]/{IdCampaniaMailing}/{Usuario}")]
        [HttpGet]
        public ActionResult EnviarListas(int IdCampaniaMailing, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio();
                var listaCampaniaMailingDetalle = _repCampaniaMailingDetalle.ObtenerListaCampaniaMailingDetalle(IdCampaniaMailing);

                foreach (var campaniaMailingDetalle in listaCampaniaMailingDetalle)
                {
                    campaniaMailingDetalle.EstadoEnvio = 0;
                    campaniaMailingDetalle.FechaModificacion = DateTime.Now;
                    campaniaMailingDetalle.UsuarioModificacion = Usuario;
                    if (campaniaMailingDetalle.HasErrors)
                    {
                        return BadRequest(campaniaMailingDetalle.GetErrors(null));
                    }
                    _repCampaniaMailingDetalle.Update(campaniaMailingDetalle);
                }

                int cantidadCampaniaMailingDetalle = _repCampaniaMailingDetalle.ObtenerCampaniaMailingDetalleEstadoCero(IdCampaniaMailing).Count;
                return Ok(new { Resultado = cantidadCampaniaMailingDetalle });
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = "ERROR", Message = e.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 15/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa las listas dependiendo de prioridades configuradas
        /// </summary>
        /// <param name="prioridadesAsesorDTO">Objeto de clase prioridadesAsesorDTO</param>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult ProcesarPrioridades([FromBody] ProcesarPrioridadesDTO prioridadesAsesorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio(_integraDBContext);
            try
            {
                var listaCampaniaCorrecta = new List<PrioridadMailChimpListaBO>();
                var listaCampaniaError = new List<ErrorPrioridadMailChimpListaBO>();
                var filtroEjecutadoCorrectamente = false;

                var campaniaMailing = new CampaniaMailingBO();
                var campaniaMailingDetalle = new CampaniaMailingDetalleBO();

                List<CampaniaMailingDetalleConCantidadDTO> cantidades = new List<CampaniaMailingDetalleConCantidadDTO>();

                var _repCampaniaMailing = new CampaniaMailingRepositorio(_integraDBContext);
                var _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio(_integraDBContext);
                var _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio(_integraDBContext);
                var _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
                var filtroSegmentoValorTipoRepositorio = new FiltroSegmentoValorTipoRepositorio(_integraDBContext);

                var _campaniaMailingDetalleProgramaRepositorio = new CampaniaMailingDetalleProgramaRepositorio(_integraDBContext);
                var _repAreaCampaniaMailingDetalle = new AreaCampaniaMailingDetalleRepositorio(_integraDBContext);
                var _repSubAreaCampaniaMailingDetalle = new SubAreaCampaniaMailingDetalleRepositorio(_integraDBContext);
                var _repCampaniaMailingValorTipo = new CampaniaMailingValorTipoRepositorio(_integraDBContext);

                var listaPrioridad = prioridadesAsesorDTO.ListaPrioridades.FirstOrDefault();
                campaniaMailingDetalle = _repCampaniaMailingDetalle.FirstById(listaPrioridad.Id);
                campaniaMailing = _repCampaniaMailing.FirstById(campaniaMailingDetalle.IdCampaniaMailing);

                var listaExcluirPorCampaniaMailing = _repCampaniaMailingValorTipo.ObtenerPorIdCampaniaMailing(campaniaMailing.Id).Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing).ToList();

                _repPrioridadMailChimpLista.EliminarListaMailChimpListaCorreos(campaniaMailing.Id);

                var listaOrdenada = prioridadesAsesorDTO.ListaPrioridades.OrderBy(x => x.Prioridad).ToList();

                foreach (var prioridad in listaOrdenada)
                {
                    // Inicializamos el flag
                    filtroEjecutadoCorrectamente = false;

                    var plantillas = campaniaMailingDetalle.ProcesarPrioridadMailchimp(prioridad, prioridadesAsesorDTO.Usuario);
                    var listaEtiquetas = campaniaMailingDetalle.ObtenerEtiquetas(plantillas.Contenido);

                    _repPrioridadMailChimpLista.EliminarListaMailchimpSinEnviar(prioridad.Id);

                    // Insercion por SP para replica
                    PrioridadMailChimpListaInsercionDTO prioridadMailChimpListaInsercion = new PrioridadMailChimpListaInsercionDTO()
                    {
                        IdCampaniaMailing = plantillas.IdCampaniaMailing,
                        IdCampaniaMailingDetalle = plantillas.IdCampaniaMailingDetalle,
                        Asunto = plantillas.Asunto + plantillas.Subject,
                        Contenido = plantillas.Contenido,
                        AsuntoLista = plantillas.NombreCampania,
                        IdPersonal = plantillas.IdPersonal,
                        NombreAsesor = plantillas.NombreCompletoPersonal,
                        Alias = plantillas.CorreoElectronico,
                        Etiquetas = string.Join<string>(",", listaEtiquetas),
                        Enviado = false,
                        UsuarioCreacion = prioridadesAsesorDTO.Usuario,
                        UsuarioModificacion = prioridadesAsesorDTO.Usuario
                    };

                    prioridadMailChimpListaInsercion.Id = _repPrioridadMailChimpLista.InsertarPrioridadMailChimpListaFiltro(prioridadMailChimpListaInsercion);

                    var _nuevoPrioridadMailChimpLista = new PrioridadMailChimpListaBO
                    {
                        Id = prioridadMailChimpListaInsercion.Id,
                        IdCampaniaMailing = plantillas.IdCampaniaMailing,
                        IdCampaniaMailingDetalle = plantillas.IdCampaniaMailingDetalle,
                        Asunto = plantillas.Asunto + plantillas.Subject,
                        Contenido = plantillas.Contenido,
                        AsuntoLista = plantillas.NombreCampania,
                        IdPersonal = plantillas.IdPersonal,
                        NombreAsesor = plantillas.NombreCompletoPersonal,
                        Alias = plantillas.CorreoElectronico,
                        Etiquetas = string.Join<string>(",", listaEtiquetas),
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = prioridadesAsesorDTO.Usuario,
                        UsuarioModificacion = prioridadesAsesorDTO.Usuario
                    };

                    // Insercion Linq comentada para reversion
                    //_repPrioridadMailChimpLista.Insert(_nuevoPrioridadMailChimpLista);//FALTA

                    int contactosInsertados = 0;
                    if (prioridad.IdFiltroSegmento == 201)
                    {
                        contactosInsertados = _repPrioridadMailChimpListaCorreo.EjecutarFiltroCampaniaMailchimpPrueba(prioridad.IdFiltroSegmento, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, prioridadesAsesorDTO.Usuario);
                    }
                    else
                    {
                        ValorTipoDTO valorTipo = filtroSegmentoValorTipoRepositorio.ObtenerFiltrosCampaniaMailchimp(prioridad.IdFiltroSegmento);
                        valorTipo.IdFiltroSegmento = prioridad.IdFiltroSegmento;

                        var filtroSegmento = _repFiltroSegmento.FirstById(prioridad.IdFiltroSegmento);
                        valorTipo.FechaInicioOportunidad = null;

                        var listaProgramas = _campaniaMailingDetalleProgramaRepositorio.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle && x.Tipo == "Filtro",
                            y => new FiltroSegmentoValorTipoDTO
                            {
                                Valor = y.IdPgeneral,
                                IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral
                            }).ToList();
                        var listaAreas = _repAreaCampaniaMailingDetalle.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle, y => new FiltroSegmentoValorTipoDTO
                        {
                            Valor = y.IdAreaCapacitacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea
                        }).ToList();
                        var listaSubAreas = _repSubAreaCampaniaMailingDetalle.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle, y => new FiltroSegmentoValorTipoDTO
                        {
                            Valor = y.IdSubAreaCapacitacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea
                        }).ToList();

                        var filtro = new FiltroSegmentoBO(_integraDBContext)
                        {
                            Id = prioridad.IdFiltroSegmento
                        };

                        //while (!filtroEjecutadoCorrectamente) {
                        while (filtroEjecutadoCorrectamente is false)
                        {
                            try
                            {
                                contactosInsertados = filtro.EjecutarFiltroMailchimp(listaAreas, listaSubAreas, listaProgramas, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, campaniaMailing.FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal, campaniaMailing.FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal, listaExcluirPorCampaniaMailing);
                                listaCampaniaCorrecta.Add(_nuevoPrioridadMailChimpLista);
                                filtroEjecutadoCorrectamente = true;
                            }
                            catch (Exception e)
                            {
                                listaCampaniaError.Add(new ErrorPrioridadMailChimpListaBO() { PrioridadMailChimpLista = _nuevoPrioridadMailChimpLista, Exception = e });
                                filtroEjecutadoCorrectamente = false;
                            }
                        }
                        //contactosInsertados = prioridadMailChimpListaCorreoRepositorio.EjecutarFiltroCampaniaMailchimp(valorTipo, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, prioridadesAsesorDTO.Usuario);
                    }

                    // Datos comentados para actualizacion Linq, comentada para reversion
                    //CampaniaMailingDetalleBO campaniaDetalleBO = _repCampaniaMailingDetalle.FirstById(prioridad.Id);
                    //campaniaDetalleBO.CantidadContactos = contactosInsertados;
                    //campaniaDetalleBO.FechaModificacion = DateTime.Now;
                    //campaniaDetalleBO.UsuarioModificacion = prioridadesAsesorDTO.Usuario;

                    //_repCampaniaMailingDetalle.Update(campaniaDetalleBO);//FALTA

                    // Datos para actualizacion por SP, direccion a replica
                    CampaniaMailingDetalleActualizacionDTO campaniaMailingDetalleActualizacion = new CampaniaMailingDetalleActualizacionDTO()
                    {
                        CantidadContactos = contactosInsertados,
                        UsuarioModificacion = prioridadesAsesorDTO.Usuario,
                        Id = prioridad.Id
                    };

                    _repCampaniaMailingDetalle.ActualizarDatosFiltroMailchimp(campaniaMailingDetalleActualizacion);

                    cantidades.Add(new CampaniaMailingDetalleConCantidadDTO
                    {
                        Id = prioridad.Id,
                        Cantidad = contactosInsertados
                    });
                }
                // Enviar correo
                var result = new
                {
                    listaCampaniaError,
                    listaCampaniaCorrecta
                };
                // Enviar mensaje sistemas
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar oportunidades - Correcto ", campaniaMailing.Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(result)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                //mensaje a usuario final
                var mensajeFinal = new List<MensajeProcesarDTO>();
                var correcto = new MensajeProcesarDTO()
                {
                    Nombre = "CORRECTO",
                    ListaDetalle =
                       listaCampaniaCorrecta.GroupBy(x => x.AsuntoLista).Select(X => new MensajeProcesarDetalleDTO
                       {
                           NombreCampania = campaniaMailing.Nombre,
                           NombreLista = X.Key,
                           NroIntentos = X.ToList().Count()
                       }).ToList()
                };
                var error = new MensajeProcesarDTO()
                {
                    Nombre = "ERROR",
                    ListaDetalle =
                        listaCampaniaError.Select(x => x.PrioridadMailChimpLista).GroupBy(x => x.AsuntoLista).Select(X => new MensajeProcesarDetalleDTO
                        {
                            NombreCampania = campaniaMailing.Nombre,
                            NombreLista = X.Key,
                            NroIntentos = X.ToList().Count()
                        }).ToList()
                };

                mensajeFinal.Add(error);
                mensajeFinal.Add(correcto);

                List<string> correosPersonalizados = new List<string>
                {
                    "mvillaroel@bsginstitute.com",
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl MailservicePersonalizado = new TMK_MailServiceImpl();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correosPersonalizados),
                    Subject = string.Concat("Procesar oportunidades - Correcto ", campaniaMailing.Nombre),
                    //Message = string.Concat("Message: ", JsonConvert.SerializeObject(mensajeFinal)),
                    Message = campaniaMailingDetalle.GenerarPlantillaNotificacionProcesamientoCorrecto(mensajeFinal),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();

                return Ok(new { Result = "OK", Records = cantidades });
            }
            catch (Exception e)
            {
                var listaPrioridad = prioridadesAsesorDTO.ListaPrioridades.FirstOrDefault();
                var campaniaMailing = _repCampaniaMailingDetalle.FirstById(listaPrioridad.Id);
                var idMailing = campaniaMailing.IdCampaniaMailing;

                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "mvillaroel@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar oportunidades - Error ", campaniaMailing.Campania),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                return BadRequest(new { Resultado = "ERROR", e.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa las listas de forma automatica dependiendo de prioridades configuradas
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[Action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ProcesarPrioridadesActividadAutomatica(int IdConjuntoLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaMailingDetalleBO campaniaMailingDetalleBO = new CampaniaMailingDetalleBO();
                List<CampaniaMailingDetalleConCantidadDTO> cantidades = new List<CampaniaMailingDetalleConCantidadDTO>();

                integraDBContext contexto = new integraDBContext();
                CampaniaMailingDetalleRepositorio campaniaMailingDetalleRepositorio = new CampaniaMailingDetalleRepositorio(contexto);
                PrioridadMailChimpListaRepositorio prioridadMailChimpListaRepositorio = new PrioridadMailChimpListaRepositorio(contexto);
                PrioridadMailChimpListaCorreoRepositorio prioridadMailChimpListaCorreoRepositorio = new PrioridadMailChimpListaCorreoRepositorio(contexto);
                FiltroSegmentoRepositorio filtroSegmentoRepositorio = new FiltroSegmentoRepositorio(contexto);
                ConjuntoListaRepositorio _repConjuntoLista = new ConjuntoListaRepositorio(contexto);


                List<PrioridadesDTO> ListaPrioridades = campaniaMailingDetalleRepositorio.ObtenerListaPrioridades(IdConjuntoLista);
                var listaPrioridad = ListaPrioridades.FirstOrDefault();
                var idMailing = campaniaMailingDetalleRepositorio.FirstById(listaPrioridad.Id).IdCampaniaMailing;
                //var idMailing = listaPrioridad.IdCampaniaMailing ?? default(int);
                prioridadMailChimpListaRepositorio.EliminarListaMailChimpListaCorreos(idMailing);

                var listaOrdenada = ListaPrioridades.OrderBy(x => x.Prioridad).ToList();

                foreach (var prioridad in listaOrdenada)
                {
                    var plantillas = campaniaMailingDetalleBO.ProcesarPrioridadMailchimp(prioridad, "ActividadAutomatica");
                    var listaEtiquetas = campaniaMailingDetalleBO.ObtenerEtiquetas(plantillas.Contenido);

                    prioridadMailChimpListaRepositorio.EliminarListaMailchimpSinEnviar(prioridad.Id);

                    PrioridadMailChimpListaBO _nuevoPrioridadMailChimpLista = new PrioridadMailChimpListaBO();
                    _nuevoPrioridadMailChimpLista.IdCampaniaMailing = plantillas.IdCampaniaMailing;
                    _nuevoPrioridadMailChimpLista.IdCampaniaMailingDetalle = plantillas.IdCampaniaMailingDetalle;
                    _nuevoPrioridadMailChimpLista.Asunto = plantillas.Asunto + plantillas.Subject;
                    _nuevoPrioridadMailChimpLista.Contenido = plantillas.Contenido;
                    _nuevoPrioridadMailChimpLista.AsuntoLista = plantillas.NombreCampania;
                    _nuevoPrioridadMailChimpLista.IdPersonal = plantillas.IdPersonal;
                    _nuevoPrioridadMailChimpLista.NombreAsesor = plantillas.NombreCompletoPersonal;
                    _nuevoPrioridadMailChimpLista.Alias = plantillas.CorreoElectronico;
                    _nuevoPrioridadMailChimpLista.Etiquetas = string.Join<string>(",", listaEtiquetas);
                    _nuevoPrioridadMailChimpLista.Estado = true;
                    _nuevoPrioridadMailChimpLista.FechaCreacion = DateTime.Now;
                    _nuevoPrioridadMailChimpLista.FechaModificacion = DateTime.Now;
                    _nuevoPrioridadMailChimpLista.UsuarioCreacion = "ActividadAutomatica";
                    _nuevoPrioridadMailChimpLista.UsuarioModificacion = "ActividadAutomatica";
                    prioridadMailChimpListaRepositorio.Insert(_nuevoPrioridadMailChimpLista);

                    int contactosInsertados = 0;
                    if (prioridad.IdFiltroSegmento == 201)
                    {
                        contactosInsertados = prioridadMailChimpListaCorreoRepositorio.EjecutarFiltroCampaniaMailchimpPrueba(prioridad.IdFiltroSegmento, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, "ActividadAutomatica");
                    }
                    else
                    {
                        //ValorTipoDTO valorTipo = filtroSegmentoValorTipoRepositorio.ObtenerFiltrosCampaniaMailchimp(prioridad.IdFiltroSegmento);
                        //valorTipo.IdFiltroSegmento = prioridad.IdFiltroSegmento;

                        //var filtroSegmento = filtroSegmentoRepositorio.FirstById(prioridad.IdFiltroSegmento);
                        //valorTipo.FechaInicioOportunidad = null;
                        ////if (filtroSegmento.FechainicioOportunidadCantidad > 0) valorTipo.FechaInicioOportunidad = campaniaMailingDetalleBO.GetFechaFiltro(filtroSegmento.FechainicioOportunidadCantidad, filtroSegmento.FechainicioOportunidad);
                        ////valorTipo.FechaFinOportunidad = DateTime.Now;
                        ////if (filtroSegmento.FechaCreacionAlumnoCantidad > 0) valorTipo.FechaFinOportunidad = campaniaMailingDetalleBO.GetFechaFiltro(filtroSegmento.FechaCreacionAlumnoCantidad, filtroSegmento.FechaCreacionAlumnoPeriodo);
                        ////valorTipo.SinActividadAlumno = DateTime.Now;
                        ////if (filtroSegmento.SinActividadAlumnoCantidad > 0) valorTipo.SinActividadAlumno = campaniaMailingDetalleBO.GetFechaFiltro(filtroSegmento.SinActividadAlumnoCantidad, filtroSegmento.SinActividadAlumnoPeriodo);

                        //var programas = _campaniaMailingDetalleProgramaRepositorio.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle && x.Tipo == "Filtro", 
                        //    y => new FiltroSegmentoValorTipoDTO
                        //    {
                        //        Valor = y.IdPgeneral,
                        //        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral
                        //    }).ToList();
                        //var areas = _areaCampaniaMailingDetalle.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle, y => new FiltroSegmentoValorTipoDTO {
                        //    Valor = y.IdAreaCapacitacion,
                        //    IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea
                        //}).ToList();
                        //var subAreas = _subAreaCampaniaMailingDetalle.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle, y => new FiltroSegmentoValorTipoDTO
                        //{
                        //    Valor = y.IdSubAreaCapacitacion,
                        //    IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea
                        //}).ToList();
                        FiltroSegmentoBO filtro = new FiltroSegmentoBO(contexto);
                        filtro.Id = prioridad.IdFiltroSegmento;
                        //contactosInsertados = filtro.EjecutarFiltroMailchimp(areas, subAreas, programas, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing);
                        contactosInsertados = _repConjuntoLista.ProcesarListasCorreoMailing(_nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, prioridad.IdConjuntoListaDetalle ?? default(int));
                        //contactosInsertados = prioridadMailChimpListaCorreoRepositorio.EjecutarFiltroCampaniaMailchimp(valorTipo, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, prioridadesAsesorDTO.Usuario);
                    }

                    CampaniaMailingDetalleBO campaniaDetalleBO = campaniaMailingDetalleRepositorio.FirstById(prioridad.Id);
                    campaniaDetalleBO.CantidadContactos = contactosInsertados;
                    campaniaDetalleBO.FechaModificacion = DateTime.Now;
                    campaniaDetalleBO.UsuarioModificacion = "ActividadAutomatica";
                    campaniaMailingDetalleRepositorio.Update(campaniaDetalleBO);

                    cantidades.Add(new CampaniaMailingDetalleConCantidadDTO
                    {
                        Id = prioridad.Id,
                        Cantidad = contactosInsertados
                    });
                }

                return Ok(new { Result = "OK", Records = cantidades });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Resultado = "ERROR", Message = ex.Message });
            }
        }

        [Route("[Action]/{IdConjuntoLista}/{IdConjuntoListaDetalle}")]
        [HttpGet]
        public ActionResult ProcesarPrioridadesAutomaticaporConjuntoListaDetalle(int IdConjuntoLista, int IdConjuntoListaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaMailingDetalleBO campaniaMailingDetalleBO = new CampaniaMailingDetalleBO();
                List<CampaniaMailingDetalleConCantidadDTO> cantidades = new List<CampaniaMailingDetalleConCantidadDTO>();

                integraDBContext contexto = new integraDBContext();
                CampaniaMailingDetalleRepositorio campaniaMailingDetalleRepositorio = new CampaniaMailingDetalleRepositorio(contexto);
                PrioridadMailChimpListaRepositorio prioridadMailChimpListaRepositorio = new PrioridadMailChimpListaRepositorio(contexto);
                PrioridadMailChimpListaCorreoRepositorio prioridadMailChimpListaCorreoRepositorio = new PrioridadMailChimpListaCorreoRepositorio(contexto);
                FiltroSegmentoRepositorio filtroSegmentoRepositorio = new FiltroSegmentoRepositorio(contexto);
                ConjuntoListaRepositorio _repConjuntoLista = new ConjuntoListaRepositorio(contexto);


                List<PrioridadesDTO> ListaPrioridades = campaniaMailingDetalleRepositorio.ObtenerListaPrioridades(IdConjuntoLista);
                var listaPrioridad = ListaPrioridades.FirstOrDefault();
                //var idMailing = campaniaMailingDetalleRepositorio.FirstById(listaPrioridad.Id).IdCampaniaMailing;
                var idMailing = listaPrioridad.IdCampaniaMailing ?? default(int);
                prioridadMailChimpListaRepositorio.EliminarListaMailChimpListaCorreos(idMailing);

                var listaOrdenada = ListaPrioridades.Where(w => w.IdConjuntoListaDetalle == IdConjuntoListaDetalle).OrderBy(x => x.Prioridad).ToList();

                foreach (var prioridad in listaOrdenada)
                {
                    var plantillas = campaniaMailingDetalleBO.ProcesarPrioridadMailchimp(prioridad, "ActividadAutomatica");
                    var listaEtiquetas = campaniaMailingDetalleBO.ObtenerEtiquetas(plantillas.Contenido);

                    prioridadMailChimpListaRepositorio.EliminarListaMailchimpSinEnviar(prioridad.Id);

                    PrioridadMailChimpListaBO _nuevoPrioridadMailChimpLista = new PrioridadMailChimpListaBO();
                    _nuevoPrioridadMailChimpLista.IdCampaniaMailing = plantillas.IdCampaniaMailing;
                    _nuevoPrioridadMailChimpLista.IdCampaniaMailingDetalle = plantillas.IdCampaniaMailingDetalle;
                    _nuevoPrioridadMailChimpLista.Asunto = plantillas.Asunto + plantillas.Subject;
                    _nuevoPrioridadMailChimpLista.Contenido = plantillas.Contenido;
                    _nuevoPrioridadMailChimpLista.AsuntoLista = plantillas.NombreCampania;
                    _nuevoPrioridadMailChimpLista.IdPersonal = plantillas.IdPersonal;
                    _nuevoPrioridadMailChimpLista.NombreAsesor = plantillas.NombreCompletoPersonal;
                    _nuevoPrioridadMailChimpLista.Alias = plantillas.CorreoElectronico;
                    _nuevoPrioridadMailChimpLista.Etiquetas = string.Join<string>(",", listaEtiquetas);
                    _nuevoPrioridadMailChimpLista.Estado = true;
                    _nuevoPrioridadMailChimpLista.FechaCreacion = DateTime.Now;
                    _nuevoPrioridadMailChimpLista.FechaModificacion = DateTime.Now;
                    _nuevoPrioridadMailChimpLista.UsuarioCreacion = "ActividadAutomatica";
                    _nuevoPrioridadMailChimpLista.UsuarioModificacion = "ActividadAutomatica";
                    prioridadMailChimpListaRepositorio.Insert(_nuevoPrioridadMailChimpLista);

                    int contactosInsertados = 0;
                    if (prioridad.IdFiltroSegmento == 201)
                    {
                        contactosInsertados = prioridadMailChimpListaCorreoRepositorio.EjecutarFiltroCampaniaMailchimpPrueba(prioridad.IdFiltroSegmento, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, "ActividadAutomatica");
                    }
                    else
                    {
                        //ValorTipoDTO valorTipo = filtroSegmentoValorTipoRepositorio.ObtenerFiltrosCampaniaMailchimp(prioridad.IdFiltroSegmento);
                        //valorTipo.IdFiltroSegmento = prioridad.IdFiltroSegmento;

                        //var filtroSegmento = filtroSegmentoRepositorio.FirstById(prioridad.IdFiltroSegmento);
                        //valorTipo.FechaInicioOportunidad = null;
                        ////if (filtroSegmento.FechainicioOportunidadCantidad > 0) valorTipo.FechaInicioOportunidad = campaniaMailingDetalleBO.GetFechaFiltro(filtroSegmento.FechainicioOportunidadCantidad, filtroSegmento.FechainicioOportunidad);
                        ////valorTipo.FechaFinOportunidad = DateTime.Now;
                        ////if (filtroSegmento.FechaCreacionAlumnoCantidad > 0) valorTipo.FechaFinOportunidad = campaniaMailingDetalleBO.GetFechaFiltro(filtroSegmento.FechaCreacionAlumnoCantidad, filtroSegmento.FechaCreacionAlumnoPeriodo);
                        ////valorTipo.SinActividadAlumno = DateTime.Now;
                        ////if (filtroSegmento.SinActividadAlumnoCantidad > 0) valorTipo.SinActividadAlumno = campaniaMailingDetalleBO.GetFechaFiltro(filtroSegmento.SinActividadAlumnoCantidad, filtroSegmento.SinActividadAlumnoPeriodo);

                        //var programas = _campaniaMailingDetalleProgramaRepositorio.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle && x.Tipo == "Filtro", 
                        //    y => new FiltroSegmentoValorTipoDTO
                        //    {
                        //        Valor = y.IdPgeneral,
                        //        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral
                        //    }).ToList();
                        //var areas = _areaCampaniaMailingDetalle.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle, y => new FiltroSegmentoValorTipoDTO {
                        //    Valor = y.IdAreaCapacitacion,
                        //    IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea
                        //}).ToList();
                        //var subAreas = _subAreaCampaniaMailingDetalle.GetBy(x => x.IdCampaniaMailingDetalle == plantillas.IdCampaniaMailingDetalle, y => new FiltroSegmentoValorTipoDTO
                        //{
                        //    Valor = y.IdSubAreaCapacitacion,
                        //    IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea
                        //}).ToList();
                        FiltroSegmentoBO filtro = new FiltroSegmentoBO(contexto);
                        filtro.Id = prioridad.IdFiltroSegmento;
                        //contactosInsertados = filtro.EjecutarFiltroMailchimp(areas, subAreas, programas, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing);
                        contactosInsertados = _repConjuntoLista.ProcesarListasCorreoMailing(_nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, prioridad.IdConjuntoListaDetalle ?? default(int));
                        //contactosInsertados = prioridadMailChimpListaCorreoRepositorio.EjecutarFiltroCampaniaMailchimp(valorTipo, _nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaMailing, prioridadesAsesorDTO.Usuario);
                    }

                    CampaniaMailingDetalleBO campaniaDetalleBO = campaniaMailingDetalleRepositorio.FirstById(prioridad.Id);
                    campaniaDetalleBO.CantidadContactos = contactosInsertados;
                    campaniaDetalleBO.FechaModificacion = DateTime.Now;
                    campaniaDetalleBO.UsuarioModificacion = "ActividadAutomatica";
                    campaniaMailingDetalleRepositorio.Update(campaniaDetalleBO);

                    cantidades.Add(new CampaniaMailingDetalleConCantidadDTO
                    {
                        Id = prioridad.Id,
                        Cantidad = contactosInsertados
                    });
                }

                return Ok(new { Result = "OK", Records = cantidades });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Resultado = "ERROR", Message = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las campanias detalle desde un conjunto lista detalle
        /// </summary>
        /// <param name="IdConjuntoLista">Id del conjunto lista a consultar para la campania</param>
        /// <returns>Retorna una lista de DTO de las campanias mailing detalle resultante de la consulta</returns>
        [Route("[Action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerCamapaniaPorConjuntoLista(int IdConjuntoLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaMailingDetalleRepositorio _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio();
                var Respuesta = _repCampaniaMailingDetalle.ObtenerListaCampaniaMailingDetallePorConjuntoLista(IdConjuntoLista);

                return Ok(Respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Resultado = "ERROR", Message = ex.Message });
            }
        }

        [Route("[Action]/{IdCampaniaGeneral}")]
        [HttpGet]
        public ActionResult ObtenerCampaniaDetalleCampaniaGeneral(int IdCampaniaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaGeneralDetalleRepositorio _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio();
                var Respuesta = _repCampaniaGeneralDetalle.GetBy(w => w.IdCampaniaGeneral == IdCampaniaGeneral);

                return Ok(Respuesta.Select(s => new { IdCampaniaGeneralDetalle = s.Id }));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Resultado = "ERROR", Message = ex.Message });
            }
        }

        [Route("[Action]/{IdCampaniaGeneral}/{IdEstadoEnvio}")]
        [HttpGet]
        public ActionResult ActualizarEstadoCampaniaGeneral(int IdCampaniaGeneral, int IdEstadoEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaGeneralBO campaniaGeneralBO = new CampaniaGeneralBO(_integraDBContext);
                CampaniaGeneralRepositorio _campaniaGeneralRepositorio = new CampaniaGeneralRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    campaniaGeneralBO = _campaniaGeneralRepositorio.FirstById(IdCampaniaGeneral);
                    campaniaGeneralBO.IdEstadoEnvioMailing = IdEstadoEnvio;
                    campaniaGeneralBO.FechaModificacion = DateTime.Now;
                    _campaniaGeneralRepositorio.Update(campaniaGeneralBO);
                    scope.Complete();
                    return Ok(new { Resultado = "Se Actualizo Correctamente " });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { Resultado = "ERROR" });
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos del resultado de la ejecucion las campanias mailing
        /// </summary>
        /// <returns>Retorna la plantilla indicando los intentos que se realizaron en un procesamiento</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerEmail()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CampaniaMailingDetalleBO campaniaMailingDetalle = new CampaniaMailingDetalleBO();
                List<MensajeProcesarDTO> listaMensaje = new List<MensajeProcesarDTO>
                {
                    {
                        new MensajeProcesarDTO() { Nombre = "Lista error", ListaDetalle = new List<MensajeProcesarDetalleDTO>{
                            new MensajeProcesarDetalleDTO (){ NombreCampania = "1.1", NombreLista = "1.1", NroIntentos = 0 },
                            new MensajeProcesarDetalleDTO (){ NombreCampania = "1.2", NombreLista = "1.2", NroIntentos = 0 }
                            }
                        }
                    },
                    {
                        new MensajeProcesarDTO() { Nombre = "ListaCorrecto", ListaDetalle = new List<MensajeProcesarDetalleDTO>
                            {
                                new MensajeProcesarDetalleDTO (){ NombreCampania = "2.1", NombreLista = "2.1", NroIntentos = 1 },
                                new MensajeProcesarDetalleDTO (){ NombreCampania = "2.2", NombreLista = "2.2", NroIntentos = 1 },
                                new MensajeProcesarDetalleDTO (){ NombreCampania = "2.3", NombreLista = "2.3", NroIntentos = 1 },
                                new MensajeProcesarDetalleDTO (){ NombreCampania = "2.4", NombreLista = "2.4", NroIntentos = 1 }
                            }
                        }
                    }
                };
                return Ok(campaniaMailingDetalle.GenerarPlantillaNotificacionProcesamientoCorrecto(listaMensaje));
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = "ERROR", e.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga los contactos y lo almacena en un archivo xlsx o csv dependiendo de la eleccion
        /// </summary>
        /// <param name="IdCampaniaMailingDetalle">Id de la campania mailing detalle que se va a descargar</param>
        /// <param name="IdTipoArchivo">Tipo de archivo que se va a generar (1: xlsx, 2: csv)</param>
        /// <returns>Retorna el archivo generado</returns>
        [Route("[Action]/{IdCampaniaMailingDetalle}/{IdTipoArchivo}")]
        [HttpGet]
        public ActionResult DescargarContactos(int IdCampaniaMailingDetalle, int IdTipoArchivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaMailingDetalleRepositorio _repcampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio(_integraDBContext);
                if (!_repcampaniaMailingDetalle.Exist(IdCampaniaMailingDetalle))
                {
                    return BadRequest("No existe la campaña detalle seleccionada");
                }
                var campaniaMailingDetalle = _repcampaniaMailingDetalle.FirstById(IdCampaniaMailingDetalle);
                var contactos = _repcampaniaMailingDetalle.ObtenerContactos(IdCampaniaMailingDetalle);
                contactos = contactos.OrderBy(x => x.IdPrioridadMailChimpListaCorreo).ToList();
                ReporteBO reporte = new ReporteBO();
                var nombreArchivo = "";
                byte[] archivo = null;
                if (IdTipoArchivo == 1)
                {
                    archivo = reporte.ObtenerContactosCampaniaMalingDetalle(contactos, TipoArchivo.Excel);
                    nombreArchivo = string.Concat("Contactos - ", campaniaMailingDetalle.Id, " - ", campaniaMailingDetalle.Campania, ".xlsx");
                    return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
                }
                else
                {
                    archivo = reporte.ObtenerContactosCampaniaMalingDetalle(contactos, TipoArchivo.Csv);
                    nombreArchivo = string.Concat("Contactos - ", campaniaMailingDetalle.Id, " - ", campaniaMailingDetalle.Campania, ".csv");
                    return File(archivo, "text/csv", nombreArchivo);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = "ERROR", e.Message });
            }
        }

        private class ErrorPrioridadMailChimpListaBO
        {
            public PrioridadMailChimpListaBO PrioridadMailChimpLista { get; set; }
            public Exception Exception { get; set; }
        }
    }
}
