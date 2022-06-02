using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/ActividadCabecera
    /// Autor: Wilber Choque - Fischer Valdez - Richard Zenteno - Gian Miranda
    /// Fecha: 08/02/2021
    /// <summary>
    /// Configura las actividades automaticas de la interfaz ActividadCabecera
    /// </summary>
    [Route("api/ActividadCabecera")]
    public class ActividadCabeceraController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ActividadCabeceraController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        #region Servicios Adicionales
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCategorias()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaOrigenRepositorio _repoCategoria = new CategoriaOrigenRepositorio(_integraDBContext);
                var Categorias = _repoCategoria.ObtenerCategoriaFiltro();
                return Json(new { Result = "OK", Records = Categorias });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 18/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los tipos de datos para el combo de actividad Cabecera
        /// </summary>
        /// <returns>List<TipoDatoPrincipalDTO></TipoDatoDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoDato()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio(_integraDBContext);
                var TipoDato = _repTipoDato.ObtenerTodoGrid();
                return Json(new { Result = "OK", Records = TipoDato });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 18/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las categorias por los tipos de datos para el combo de actividad Cabecera
        /// </summary>
        /// <returns>List<TipoDatoPrincipalDTO></TipoDatoDTO></returns>
        [Route("[Action]/{IdTipoDato}")]
        [HttpGet]
        public ActionResult ObtenerCategoriaPorTipoDato(int IdTipoDato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaOrigenRepositorio _repoCategoria = new CategoriaOrigenRepositorio(_integraDBContext);
                var Categorias = _repoCategoria.ObtenerCategoriaPorTipoDato(IdTipoDato);
                return Json(new { Result = "OK", Records = Categorias });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPlantillasSpeech()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaRepositorio _repoPlantilla = new PlantillaRepositorio(_integraDBContext);
                var PlantillasSpeech = _repoPlantilla.ObtenerAllPlantillaSpeech();
                return Json(new { Result = "OK", Records = PlantillasSpeech });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPlantillasSpeechDespedida()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaRepositorio _repoPlantilla = new PlantillaRepositorio(_integraDBContext);
                var PlantillasSpeechDespedida = _repoPlantilla.ObtenerAllPlantillaSpeechDespedida();
                return Json(new { Result = "OK", Records = PlantillasSpeechDespedida });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerActividadesBase()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadBaseRepositorio _repoActividadBase = new ActividadBaseRepositorio(_integraDBContext);
                var Actividades = _repoActividadBase.ObtenerActividadesBase();
                return Json(new { Result = "OK", Records = Actividades });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 18/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades bases para las actividades masivas
        /// </summary>
        /// <returns> List<ActividadCabeceraDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerActividadesBaseMasivo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadBaseRepositorio _repoActividadBase = new ActividadBaseRepositorio(_integraDBContext);
                var Actividades = _repoActividadBase.ObtenerActividadesBaseMasivo();
                return Json(new { Result = "OK", Records = Actividades });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerReprogramacionesPorActividad(int IdActividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReprogramacionCabeceraRepositorio _repoReprogramacionCab = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                var reprogramaciones = _repoReprogramacionCab.ObtenerReprogramacionCabPorActividadCab(IdActividad);
                ActividadCabeceraTipoDatoRepositorio _repActividadCabeceraTipoDato = new ActividadCabeceraTipoDatoRepositorio(_integraDBContext);
                var tipoDatos = _repActividadCabeceraTipoDato.ObtenerTipoDatoPorActividadCabecera(IdActividad);
                return Json(new { Result = "OK", Records = reprogramaciones, Records2= tipoDatos});
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        #endregion

        #region CRUD
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarActividadCabeceras()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadCabeceraRepositorio _repActividadCabecera = new ActividadCabeceraRepositorio(_integraDBContext);
                var ActividadCabecera = _repActividadCabecera.ObtenerAllActividadCabecera();
                return Json(new { Result = "OK", Records = ActividadCabecera });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 18/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades automaticas
        /// </summary>
        /// <returns> List<ActividadCabeceraDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarActividadAutomatica()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadCabeceraRepositorio _repActividadCabecera = new ActividadCabeceraRepositorio(_integraDBContext);
                var ActividadCabecera = _repActividadCabecera.ObtenerTodoActividadAutomatica();
                return Json(new { Result = "OK", Records = ActividadCabecera });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarActividadCabecera([FromBody] ListaActividadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ActividadCabeceraRepositorio _repActividadCabecera = new ActividadCabeceraRepositorio(_integraDBContext);
                ReprogramacionCabeceraRepositorio _repoReprogramacionCab = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                ActividadCabeceraBO NuevaActividadCabecera = new ActividadCabeceraBO();
                ActividadCabeceraTipoDatoRepositorio _repActividadCabeceraTipoDato = new ActividadCabeceraTipoDatoRepositorio(_integraDBContext);


                if (ObjetoDTO.ActividadBase == "LLAMADA")
                {
                    NuevaActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraLlamada.Nombre;
                    NuevaActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraLlamada.Descripcion;
                    NuevaActividadCabecera.DuracionEstimada = ObjetoDTO.ActividadCabeceraLlamada.DuracionEstimada;
                    NuevaActividadCabecera.ReproManual = ObjetoDTO.ActividadCabeceraLlamada.ReproManual;
                    NuevaActividadCabecera.ReproAutomatica = ObjetoDTO.ActividadCabeceraLlamada.ReproAutomatica;
                    NuevaActividadCabecera.Idplantilla = ObjetoDTO.ActividadCabeceraLlamada.Idplantilla;
                    NuevaActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraLlamada.IdActividadBase;
                    NuevaActividadCabecera.ValidaLlamada = ObjetoDTO.ActividadCabeceraLlamada.ValidaLlamada;
                    NuevaActividadCabecera.IdPlantillaSpeech = ObjetoDTO.ActividadCabeceraLlamada.IdPlantillaSpeech;
                    NuevaActividadCabecera.NumeroMaximoLlamadas = ObjetoDTO.ActividadCabeceraLlamada.NumeroMaximoLlamadas;
                    NuevaActividadCabecera.FechaCreacion2 = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion2 = DateTime.Now;
                    NuevaActividadCabecera.Activo = false;
                    NuevaActividadCabecera.Estado = true;
                    NuevaActividadCabecera.UsuarioCreacion = ObjetoDTO.Usuario;
                    NuevaActividadCabecera.UsuarioModificacion = ObjetoDTO.Usuario;
                    NuevaActividadCabecera.FechaCreacion = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion = DateTime.Now;
                    NuevaActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraLlamada.IdPersonalAreaTrabajo;
                    NuevaActividadCabecera.EsEnvioMasivo= ObjetoDTO.ActividadCabeceraLlamada.EsEnvioMasivo;

                    _repActividadCabecera.Insert(NuevaActividadCabecera);

                    if (ObjetoDTO.ActividadCabeceraLlamada.TipoDato != null)
                    {
                        var TiposDatos = ObjetoDTO.ActividadCabeceraLlamada.TipoDato;
                        for (var r = 0; r < TiposDatos.Count; ++r)
                        {
                            ActividadCabeceraTipoDatoBO TipoDato = new ActividadCabeceraTipoDatoBO();
                            TipoDato.IdActividadCabecera = NuevaActividadCabecera.Id;
                            TipoDato.IdTipoDato = TiposDatos[r];
                            TipoDato.Estado = true;
                            TipoDato.UsuarioCreacion = ObjetoDTO.Usuario;
                            TipoDato.UsuarioModificacion = ObjetoDTO.Usuario;
                            TipoDato.FechaCreacion = DateTime.Now;
                            TipoDato.FechaModificacion = DateTime.Now;
                            _repActividadCabeceraTipoDato.Insert(TipoDato);
                        }
                    }

                    if (ObjetoDTO.ActividadCabeceraLlamada.Reprogramaciones != null)
                    {
                        var Reprogramaciones = ObjetoDTO.ActividadCabeceraLlamada.Reprogramaciones;
                        for (var r = 0; r < Reprogramaciones.Count; ++r)
                        {
                            ReprogramacionCabeceraBO ReprogramacionNueva = new ReprogramacionCabeceraBO();
                            ReprogramacionNueva.IdActividadCabecera = NuevaActividadCabecera.Id;
                            ReprogramacionNueva.IdCategoriaOrigen = Reprogramaciones[r].IdCategoriaOrigen;
                            ReprogramacionNueva.MaxReproPorDia = Reprogramaciones[r].MaxReproPorDia;
                            ReprogramacionNueva.IntervaloSigProgramacionMin = Reprogramaciones[r].IntervaloSigProgramacionMin;
                            ReprogramacionNueva.Estado = true;
                            ReprogramacionNueva.UsuarioCreacion = ObjetoDTO.Usuario;
                            ReprogramacionNueva.UsuarioModificacion = ObjetoDTO.Usuario;
                            ReprogramacionNueva.FechaCreacion = DateTime.Now;
                            ReprogramacionNueva.FechaModificacion = DateTime.Now;
                            _repoReprogramacionCab.Insert(ReprogramacionNueva);
                        }
                    }

                }
                else if (ObjetoDTO.ActividadBase == "WHATSAPP INDIVIDUAL" || ObjetoDTO.ActividadBase == "CORREO INDIVIDUAL")
                {
                    NuevaActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraIndividual.Nombre;
                    NuevaActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraIndividual.Descripcion;
                    NuevaActividadCabecera.DuracionEstimada = 0;
                    NuevaActividadCabecera.ReproManual = false;
                    NuevaActividadCabecera.ReproAutomatica = false;
                    NuevaActividadCabecera.Idplantilla = ObjetoDTO.ActividadCabeceraIndividual.IdPlantilla;
                    NuevaActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraIndividual.IdActividadBase;
                    NuevaActividadCabecera.ValidaLlamada = false;
                    NuevaActividadCabecera.NumeroMaximoLlamadas = 0;
                    NuevaActividadCabecera.FechaCreacion2 = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion2 = DateTime.Now;

                    NuevaActividadCabecera.IdConjuntoLista = null;
                    NuevaActividadCabecera.IdFrecuencia = null;
                    NuevaActividadCabecera.FechaInicioActividad = null;
                    NuevaActividadCabecera.FechaFinActividad = null;
                    NuevaActividadCabecera.DiaFrecuenciaMensual = null;
                    NuevaActividadCabecera.EsRepetitivo = null;
                    NuevaActividadCabecera.HoraInicio = null;
                    NuevaActividadCabecera.HoraFin = null;
                    NuevaActividadCabecera.CantidadIntevaloTiempo = null;
                    NuevaActividadCabecera.IdTiempoIntervalo = null;
                    NuevaActividadCabecera.Activo = false;

                    NuevaActividadCabecera.Estado = true;
                    NuevaActividadCabecera.UsuarioCreacion = ObjetoDTO.Usuario;
                    NuevaActividadCabecera.UsuarioModificacion = ObjetoDTO.Usuario;
                    NuevaActividadCabecera.FechaCreacion = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion = DateTime.Now;
                    NuevaActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraIndividual.IdPersonalAreaTrabajo;
                    NuevaActividadCabecera.EsEnvioMasivo= ObjetoDTO.ActividadCabeceraIndividual.EsEnvioMasivo;
                    _repActividadCabecera.Insert(NuevaActividadCabecera);

                }
                else if (ObjetoDTO.ActividadBase == "SEGMENTO FACEBOOK" || ObjetoDTO.ActividadBase == "SEGMENTO ADWORDS" || ObjetoDTO.ActividadBase == "MAILING MASIVO OPERACIONES" || ObjetoDTO.ActividadBase == "MAILING MASIVO MARKETING" || ObjetoDTO.ActividadBase == "MAILING" || ObjetoDTO.ActividadBase == "WHATSAPP MASIVO" || ObjetoDTO.ActividadBase == "WHATSAPP MASIVO OPERACIONES" || ObjetoDTO.ActividadBase == "SMS MASIVO")
                {
                    NuevaActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraMasivo.Nombre;
                    NuevaActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraMasivo.Descripcion;
                    NuevaActividadCabecera.DuracionEstimada = 0;
                    NuevaActividadCabecera.ReproManual = false;
                    NuevaActividadCabecera.ReproAutomatica = false;
                    NuevaActividadCabecera.Idplantilla = 1;
                    NuevaActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraMasivo.IdActividadBase;
                    NuevaActividadCabecera.ValidaLlamada = false;
                    NuevaActividadCabecera.NumeroMaximoLlamadas = 0;
                    NuevaActividadCabecera.FechaCreacion2 = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion2 = DateTime.Now;

                    NuevaActividadCabecera.IdConjuntoLista = ObjetoDTO.ActividadCabeceraMasivo.IdConjuntoLista;
                    NuevaActividadCabecera.IdFrecuencia = ObjetoDTO.ActividadCabeceraMasivo.IdFrecuencia;
                    NuevaActividadCabecera.FechaInicioActividad = ObjetoDTO.ActividadCabeceraMasivo.FechaInicioActividad;
                    NuevaActividadCabecera.FechaFinActividad = ObjetoDTO.ActividadCabeceraMasivo.FechaFinActividad;
                    NuevaActividadCabecera.DiaFrecuenciaMensual = ObjetoDTO.ActividadCabeceraMasivo.DiaFrecuenciaMensual;
                    NuevaActividadCabecera.EsRepetitivo = ObjetoDTO.ActividadCabeceraMasivo.EsRepetitivo;
                    NuevaActividadCabecera.HoraInicio = ObjetoDTO.ActividadCabeceraMasivo.HoraInicio;
                    NuevaActividadCabecera.HoraFin = ObjetoDTO.ActividadCabeceraMasivo.HoraFin;
                    NuevaActividadCabecera.CantidadIntevaloTiempo = ObjetoDTO.ActividadCabeceraMasivo.CantidadIntevaloTiempo;
                    NuevaActividadCabecera.IdTiempoIntervalo = ObjetoDTO.ActividadCabeceraMasivo.IdTiempoIntervalo;
                    NuevaActividadCabecera.Activo = ObjetoDTO.ActividadCabeceraMasivo.Activo;

                    NuevaActividadCabecera.Estado = true;
                    NuevaActividadCabecera.UsuarioCreacion = ObjetoDTO.Usuario;
                    NuevaActividadCabecera.UsuarioModificacion = ObjetoDTO.Usuario;
                    NuevaActividadCabecera.FechaCreacion = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion = DateTime.Now;
                    NuevaActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraMasivo.IdPersonalAreaTrabajo;
                    NuevaActividadCabecera.EsEnvioMasivo = ObjetoDTO.ActividadCabeceraMasivo.EsEnvioMasivo;


                    _repActividadCabecera.Insert(NuevaActividadCabecera);

                    ActividadCabeceraDiaSemanaRepositorio _repActividadCabeceraDiaSemana = new ActividadCabeceraDiaSemanaRepositorio(_integraDBContext);

                    if (ObjetoDTO.ActividadCabeceraMasivo.Semanal != null && ObjetoDTO.ActividadCabeceraMasivo.Semanal.Count() != 0)
                    {
                        var Dias = ObjetoDTO.ActividadCabeceraMasivo.Semanal;
                        for (var r = 0; r < Dias.Count; ++r)
                        {
                            ActividadCabeceraDiaSemanaBO actividadCabeceraDiaSemana = new ActividadCabeceraDiaSemanaBO();
                            actividadCabeceraDiaSemana.IdActividadCabecera = NuevaActividadCabecera.Id;
                            actividadCabeceraDiaSemana.IdDiaSemana = Dias[r];
                            actividadCabeceraDiaSemana.Estado = true;
                            actividadCabeceraDiaSemana.UsuarioCreacion = ObjetoDTO.Usuario;
                            actividadCabeceraDiaSemana.UsuarioModificacion = ObjetoDTO.Usuario;
                            actividadCabeceraDiaSemana.FechaCreacion = DateTime.Now;
                            actividadCabeceraDiaSemana.FechaModificacion = DateTime.Now;

                            _repActividadCabeceraDiaSemana.Insert(actividadCabeceraDiaSemana);
                        }
                    }
                }


                return Ok(NuevaActividadCabecera);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarActividadCabecera([FromBody] ListaActividadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadCabeceraRepositorio _repActividadCabecera = new ActividadCabeceraRepositorio(_integraDBContext);
                ReprogramacionCabeceraRepositorio _repoReprogramacionCab = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                ActividadCabeceraDiaSemanaRepositorio _repActividadCabeceraDiaSemana = new ActividadCabeceraDiaSemanaRepositorio(_integraDBContext);
                ActividadCabeceraBO ActividadCabecera = _repActividadCabecera.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                ActividadCabeceraTipoDatoRepositorio _repActividadCabeceraTipoDato = new ActividadCabeceraTipoDatoRepositorio(_integraDBContext);

                if (ActividadCabecera == null) throw new Exception("No se encuentra el registro que se quiere actualizar ¿Id? correcto");

                if (ObjetoDTO.ActividadBase == "LLAMADA")
                {
                    ActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraLlamada.Nombre;
                    ActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraLlamada.Descripcion;
                    ActividadCabecera.DuracionEstimada = ObjetoDTO.ActividadCabeceraLlamada.DuracionEstimada;
                    ActividadCabecera.ReproManual = ObjetoDTO.ActividadCabeceraLlamada.ReproManual;
                    ActividadCabecera.ReproAutomatica = ObjetoDTO.ActividadCabeceraLlamada.ReproAutomatica;
                    ActividadCabecera.Idplantilla = ObjetoDTO.ActividadCabeceraLlamada.Idplantilla;
                    ActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraLlamada.IdActividadBase;
                    ActividadCabecera.ValidaLlamada = ObjetoDTO.ActividadCabeceraLlamada.ValidaLlamada;
                    ActividadCabecera.IdPlantillaSpeech = ObjetoDTO.ActividadCabeceraLlamada.IdPlantillaSpeech;
                    ActividadCabecera.NumeroMaximoLlamadas = ObjetoDTO.ActividadCabeceraLlamada.NumeroMaximoLlamadas;
                    ActividadCabecera.FechaModificacion2 = DateTime.Now;
                    ActividadCabecera.FechaCreacion2 = DateTime.Now;
                    ActividadCabecera.Estado = true;
                    ActividadCabecera.UsuarioModificacion = ObjetoDTO.Usuario;
                    ActividadCabecera.FechaModificacion = DateTime.Now;
                    ActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraLlamada.IdPersonalAreaTrabajo;

                    _repActividadCabecera.Update(ActividadCabecera);

                    //reinsercion de nuevos Tipos Datos
                    var TipoDatosDB = _repActividadCabeceraTipoDato.ObtenerTipoDatoPorActividadCabecera(ActividadCabecera.Id);
                    for (var d = 0; d < TipoDatosDB.Count; ++d)
                        _repActividadCabeceraTipoDato.Delete(TipoDatosDB[d].Id, ObjetoDTO.Usuario);

                    if (ObjetoDTO.ActividadCabeceraLlamada.TipoDato != null)
                    {
                        var TiposDatos = ObjetoDTO.ActividadCabeceraLlamada.TipoDato;
                        for (var r = 0; r < TiposDatos.Count; ++r)
                        {
                            ActividadCabeceraTipoDatoBO TipoDato = new ActividadCabeceraTipoDatoBO();
                            TipoDato.IdActividadCabecera = ActividadCabecera.Id;
                            TipoDato.IdTipoDato = TiposDatos[r];
                            TipoDato.Estado = true;
                            TipoDato.UsuarioCreacion = ObjetoDTO.Usuario;
                            TipoDato.UsuarioModificacion = ObjetoDTO.Usuario;
                            TipoDato.FechaCreacion = DateTime.Now;
                            TipoDato.FechaModificacion = DateTime.Now;
                            _repActividadCabeceraTipoDato.Insert(TipoDato);
                        }
                    }

                    //reinsercion de nuevas Reprogramaciones
                    var ReprogramacionesDB = _repoReprogramacionCab.ObtenerReprogramacionCabPorActividadCab(ActividadCabecera.Id);
                    for (var d = 0; d < ReprogramacionesDB.Count; ++d)
                        _repoReprogramacionCab.Delete(ReprogramacionesDB[d].Id, ObjetoDTO.Usuario);

                    if (ObjetoDTO.ActividadCabeceraLlamada.Reprogramaciones != null)
                    {
                        var Reprogramaciones = ObjetoDTO.ActividadCabeceraLlamada.Reprogramaciones;
                        for (var r = 0; r < Reprogramaciones.Count; ++r)
                        {
                            ReprogramacionCabeceraBO Reprogramacion = new ReprogramacionCabeceraBO();
                            Reprogramacion.IdActividadCabecera = ActividadCabecera.Id;
                            Reprogramacion.IdCategoriaOrigen = Reprogramaciones[r].IdCategoriaOrigen;
                            Reprogramacion.MaxReproPorDia = Reprogramaciones[r].MaxReproPorDia;
                            Reprogramacion.IntervaloSigProgramacionMin = Reprogramaciones[r].IntervaloSigProgramacionMin;
                            Reprogramacion.Estado = true;
                            Reprogramacion.UsuarioCreacion = ObjetoDTO.Usuario;
                            Reprogramacion.UsuarioModificacion = ObjetoDTO.Usuario;
                            Reprogramacion.FechaCreacion = DateTime.Now;
                            Reprogramacion.FechaModificacion = DateTime.Now;

                            _repoReprogramacionCab.Insert(Reprogramacion);
                        }
                    }

                }
                else if (ObjetoDTO.ActividadBase == "WHATSAPP INDIVIDUAL" || ObjetoDTO.ActividadBase == "CORREO INDIVIDUAL")
                {
                    ActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraIndividual.Nombre;
                    ActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraIndividual.Descripcion;
                    ActividadCabecera.DuracionEstimada = 0;
                    ActividadCabecera.ReproManual = false;
                    ActividadCabecera.ReproAutomatica = false;
                    ActividadCabecera.Idplantilla = ObjetoDTO.ActividadCabeceraIndividual.IdPlantilla;
                    ActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraIndividual.IdActividadBase;
                    ActividadCabecera.ValidaLlamada = false;
                    ActividadCabecera.IdPlantillaSpeech = null;
                    ActividadCabecera.NumeroMaximoLlamadas = 0;
                    ActividadCabecera.FechaModificacion2 = DateTime.Now;
                    ActividadCabecera.UsuarioModificacion = ObjetoDTO.Usuario;
                    ActividadCabecera.FechaModificacion = DateTime.Now;
                    ActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraIndividual.IdPersonalAreaTrabajo;

                    _repActividadCabecera.Update(ActividadCabecera);

                }
                else if (ObjetoDTO.ActividadBase == "SEGMENTO FACEBOOK" || ObjetoDTO.ActividadBase == "SEGMENTO ADWORDS" || ObjetoDTO.ActividadBase == "MAILING MASIVO OPERACIONES" || ObjetoDTO.ActividadBase == "MAILING MASIVO MARKETING" || ObjetoDTO.ActividadBase == "MAILING" || ObjetoDTO.ActividadBase == "WHATSAPP MASIVO" || ObjetoDTO.ActividadBase == "WHATSAPP MASIVO OPERACIONES" || ObjetoDTO.ActividadBase == "SMS MASIVO")
                {
                    ActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraMasivo.Nombre;
                    ActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraMasivo.Descripcion;
                    ActividadCabecera.DuracionEstimada = 0;
                    ActividadCabecera.ReproManual = false;
                    ActividadCabecera.ReproAutomatica = false;
                    ActividadCabecera.Idplantilla = 1;
                    ActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraMasivo.IdActividadBase;
                    ActividadCabecera.ValidaLlamada = false;
                    ActividadCabecera.NumeroMaximoLlamadas = 0;
                    ActividadCabecera.FechaCreacion2 = DateTime.Now;
                    ActividadCabecera.FechaModificacion2 = DateTime.Now;

                    ActividadCabecera.IdConjuntoLista = ObjetoDTO.ActividadCabeceraMasivo.IdConjuntoLista;
                    ActividadCabecera.IdFrecuencia = ObjetoDTO.ActividadCabeceraMasivo.IdFrecuencia;
                    ActividadCabecera.FechaInicioActividad = ObjetoDTO.ActividadCabeceraMasivo.FechaInicioActividad;
                    ActividadCabecera.FechaFinActividad = ObjetoDTO.ActividadCabeceraMasivo.FechaFinActividad;
                    ActividadCabecera.DiaFrecuenciaMensual = ObjetoDTO.ActividadCabeceraMasivo.DiaFrecuenciaMensual;
                    ActividadCabecera.EsRepetitivo = ObjetoDTO.ActividadCabeceraMasivo.EsRepetitivo;
                    ActividadCabecera.HoraInicio = ObjetoDTO.ActividadCabeceraMasivo.HoraInicio;
                    ActividadCabecera.HoraFin = ObjetoDTO.ActividadCabeceraMasivo.HoraFin;
                    ActividadCabecera.CantidadIntevaloTiempo = ObjetoDTO.ActividadCabeceraMasivo.CantidadIntevaloTiempo;
                    ActividadCabecera.IdTiempoIntervalo = ObjetoDTO.ActividadCabeceraMasivo.IdTiempoIntervalo;
                    ActividadCabecera.Activo = ObjetoDTO.ActividadCabeceraMasivo.Activo;
                    ActividadCabecera.UsuarioModificacion = ObjetoDTO.Usuario;
                    ActividadCabecera.FechaModificacion = DateTime.Now;
                    ActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraMasivo.IdPersonalAreaTrabajo;


                    _repActividadCabecera.Update(ActividadCabecera);

                    var DiaSemana = _repActividadCabeceraDiaSemana.GetBy(w => w.Estado == true && w.IdActividadCabecera == ActividadCabecera.Id).ToList();
                    for (var d = 0; d < DiaSemana.Count; ++d)
                        _repActividadCabeceraDiaSemana.Delete(DiaSemana[d].Id, ObjetoDTO.Usuario);

                    if (ObjetoDTO.ActividadCabeceraMasivo.Semanal != null && ObjetoDTO.ActividadCabeceraMasivo.Semanal.Count() != 0)
                    {
                        var Dias = ObjetoDTO.ActividadCabeceraMasivo.Semanal;
                        for (var r = 0; r < Dias.Count; ++r)
                        {
                            ActividadCabeceraDiaSemanaBO actividadCabeceraDiaSemana = new ActividadCabeceraDiaSemanaBO();
                            actividadCabeceraDiaSemana.IdActividadCabecera = ActividadCabecera.Id;
                            actividadCabeceraDiaSemana.IdDiaSemana = Dias[r];
                            actividadCabeceraDiaSemana.Estado = true;
                            actividadCabeceraDiaSemana.UsuarioCreacion = ObjetoDTO.Usuario;
                            actividadCabeceraDiaSemana.UsuarioModificacion = ObjetoDTO.Usuario;
                            actividadCabeceraDiaSemana.FechaCreacion = DateTime.Now;
                            actividadCabeceraDiaSemana.FechaModificacion = DateTime.Now;

                            _repActividadCabeceraDiaSemana.Insert(actividadCabeceraDiaSemana);
                        }
                    }

                }

                return Ok(ActividadCabecera);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarActividadCabecera([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReprogramacionCabeceraRepositorio _repoReprogramacionCab = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                ActividadCabeceraRepositorio _repActividadCabecera = new ActividadCabeceraRepositorio(_integraDBContext);
                ActividadCabeceraBO ActividadCabecera = _repActividadCabecera.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                ActividadCabeceraTipoDatoRepositorio _repActividadCabeceraTipoDato = new ActividadCabeceraTipoDatoRepositorio(_integraDBContext);

                var TipoDatosDB = _repActividadCabeceraTipoDato.ObtenerTipoDatoPorActividadCabecera(Eliminar.Id).ToList();
                for (var d = 0; d < TipoDatosDB.Count; ++d)
                    _repActividadCabeceraTipoDato.Delete(TipoDatosDB[d].Id, Eliminar.NombreUsuario);

                var ReprogramacionesDB = _repoReprogramacionCab.ObtenerReprogramacionCabPorActividadCab(Eliminar.Id).ToList();
                for (var d = 0; d < ReprogramacionesDB.Count; ++d)
                    _repoReprogramacionCab.Delete(ReprogramacionesDB[d].Id, Eliminar.NombreUsuario);

                _repActividadCabecera.Delete(Eliminar.Id, Eliminar.NombreUsuario);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        [Route("[action]/{IdConjuntoListaSegmento}")]
        [HttpGet]
        public ActionResult ObtenerConjuntoListaMailing(int IdConjuntoListaSegmento)
        {
            try
            {
                ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                CampaniaMailingDetalleProgramaRepositorio _repCampaniaMailingDetallePrograma = new CampaniaMailingDetalleProgramaRepositorio(_integraDBContext);
                var Respuesta = _repConjuntoListaDetalle.ObtenerListasMailing(IdConjuntoListaSegmento);
                foreach (var item in Respuesta)
                {
                    item.ProgramaPrincipal = _repCampaniaMailingDetallePrograma.ObtenerProgramaPorCamapaniaMailingDetalle(item.Id ?? default(int), "Principales");
                    item.ProgramaSecundario = _repCampaniaMailingDetallePrograma.ObtenerProgramaPorCamapaniaMailingDetalle(item.Id ?? default(int), "Secundarios");
                }


                return Ok(Respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerConjuntoListaMailingMasivo(int IdConjuntoLista)
        {
            try
            {
                ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                return Ok(_repConjuntoListaDetalle.ObtenerListasMailingMasivo(IdConjuntoLista));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdConjuntoListaSegmento}")]
        [HttpGet]
        public ActionResult ObtenerConjuntoListaAudiencia(int IdConjuntoListaSegmento)
        {
            try
            {
                ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                FacebookCuentaPublicitariaRepositorio _repFacebookCuentaPublicitaria = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);

                var respuesta = _repConjuntoListaDetalle.ObtenerListasAudiencia(IdConjuntoListaSegmento);
                foreach (var item in respuesta)
                {
                    item.Cuenta = _repFacebookCuentaPublicitaria.ObtenerCuentaPorIdFacebook(item.Id ?? default(int));
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Route("[action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerConjuntoListaMailingDinamico(int IdConjuntoLista)
        {
            try
            {
                ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                return Ok(_repConjuntoListaDetalle.ObtenerListasMailingDinamico(IdConjuntoLista));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el conjunto lista WhatsApp
        /// </summary>
        /// <param name="IdConjuntoListaSegmento">Id del conjunto lista detalle</param>
        /// <returns>Response 200 con la lista de objetos de clase ConjuntoListaDetalleWhatsAppDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]/{IdConjuntoListaSegmento}")]
        [HttpGet]
        public ActionResult ObtenerConjuntoListaWhatsApp(int IdConjuntoListaSegmento)
        {
            try
            {
                var _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
                var _repWhatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaRepositorio(_integraDBContext);

                var resultado = _repConjuntoListaDetalle.ObtenerListasWhatsApp(IdConjuntoListaSegmento);

                foreach (var item in resultado)
                {
                    item.ProgramaPrincipal = _repWhatsAppConfiguracionEnvioPorPrograma.GetBy(w => w.IdWhatsAppConfiguracionEnvio == item.Id && w.IdTipoEnvioPrograma == 1, y => new WhatsAppConfiguracionEnvioPorProgramaDTO { IdPgeneral = y.IdPgeneral }).ToList();
                    item.ProgramaSecundario = _repWhatsAppConfiguracionEnvioPorPrograma.GetBy(w => w.IdWhatsAppConfiguracionEnvio == item.Id && w.IdTipoEnvioPrograma == 2, y => new WhatsAppConfiguracionEnvioPorProgramaDTO { IdPgeneral = y.IdPgeneral }).ToList();
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 07/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el conjunto lista Sms
        /// </summary>
        /// <param name="IdConjuntoLista">Id del conjunto lista detalle (PK de la tabla mkt.T_ConjuntoListaDetalle)</param>
        /// <returns>Response 200 con la lista de objetos de clase ConjuntoListaDetalleSmsDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerConjuntoListaSms(int IdConjuntoLista)
        {
            try
            {
                var _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);

                var resultado = _repConjuntoListaDetalle.ObtenerListasSms(IdConjuntoLista);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerConjuntoLista()
        {
            try
            {
                ConjuntoListaRepositorio _repConjuntoListaDetalle = new ConjuntoListaRepositorio(_integraDBContext);
                return Ok(_repConjuntoListaDetalle.GetBy(w => w.Estado == true, y => new { Id = y.Id, Nombre = y.Nombre, IdFiltroSegmento = y.IdFiltroSegmento }).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFrecuenciaParaActividades()
        {
            try
            {
                FrecuenciaRepositorio _repFrecuencia = new FrecuenciaRepositorio(_integraDBContext);

                return Ok(new { data = _repFrecuencia.ObtenerListaFrecuenciaActividad() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades a ejecutar
        /// </summary>
        /// <returns>Lista de objetos de clase ActividadParaEjecutarDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerActividadaEjecutar()
        {
            try
            {
                ActividadCabeceraRepositorio repDetalles = new ActividadCabeceraRepositorio(_integraDBContext);
                var lista = repDetalles.ObtenerActividadParaEjecutar();
                return Ok(lista);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{IdActividadCabecera}")]
        [HttpGet]
        public IActionResult ObtenerDiaSemanaActividad(int IdActividadCabecera)
        {

            try
            {
                ActividadCabeceraDiaSemanaRepositorio repActividadCabeceraDiaSemana = new ActividadCabeceraDiaSemanaRepositorio(_integraDBContext);
                var lista = repActividadCabeceraDiaSemana.ObtenerDiaSemanaActividad(IdActividadCabecera);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerPersonal()
        {

            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                var lista = _repPersonal.CargarPersonalParaFiltro();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor:
        /// Fecha: 06/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para Actividad Cabecera
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosActividadCabecera()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
                var personalAreaTrabajo = _repPersonalAreaTrabajo.ObtenerAreaTrabajoPersonalNombre();
                return Ok(personalAreaTrabajo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerMailingMasivoParaSubirListasAutomaticas()
        {

            try
            {
                ConfiguracionEnvioMailingRepositorio _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);
                return Ok(_repConfiguracionEnvioMailing.ObtenerMailingMasivoParaSubirListasAutomaticas());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerActividadesMailingConfiguradasHoy()
        {
            try
            {
                ActividadCabeceraRepositorio repDetalles = new ActividadCabeceraRepositorio(_integraDBContext);
                return Ok(repDetalles.ObtenerActividadesMailingConfiguradasHoy());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerActividadesMailingMasivoMarketing()
        {
            try
            {
                //ActividadCabeceraRepositorio repDetalles = new ActividadCabeceraRepositorio(_integraDBContext);
                //return Ok(repDetalles.ObtenerActividadesMailingConfiguradasHoy());

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque - Fischer Valdez - Gian Miranda
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Metodo llamado desde complementos para la ejecucion automatica de WhatsApp operaciones
        /// </summary>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarActividadesAutomaticasWhatsAppOperaciones([FromBody] List<ActividadParaEjecutarDTO> ListaActividadesAutomaticasWhatsAppOperaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lista = new List<NotificacionActividadParaEjecutarDTO>();
                var listaCorrecto = new List<ActividadParaEjecutarDTO>();
                var listaIncorrecto = new List<ActividadParaEjecutarDTO>();

                // Logica correo de ejecucion whatsapp
                var conjuntoLista = new ConjuntoListaController(_integraDBContext);
                var whatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioController(_integraDBContext);
                var whatsAppEnvioAutomaticoController = new WhatsAppEnvioAutomaticoController(_integraDBContext);

                foreach (var actividadWhatsAppOperaciones in ListaActividadesAutomaticasWhatsAppOperaciones)
                {
                    try
                    {
                        if (actividadWhatsAppOperaciones.ActivoEjecutarFiltro == true)
                        {
                            conjuntoLista.Ejecutar(actividadWhatsAppOperaciones.IdConjuntoLista.Value);
                        }
                        var WhatsAppDetallesAEnviar = whatsAppConfiguracionEnvio.ObtenerListaConfiguracionOperaciones(actividadWhatsAppOperaciones.IdConjuntoLista.Value);
                        whatsAppEnvioAutomaticoController.ProcesarListasWhatsAppEnvioAutomaticoOperaciones(WhatsAppDetallesAEnviar);
                        listaCorrecto.Add(actividadWhatsAppOperaciones);
                    }
                    catch (Exception e)
                    {
                        listaIncorrecto.Add(actividadWhatsAppOperaciones);
                    }
                }
                lista.Add(new NotificacionActividadParaEjecutarDTO() { EsCorrecto = true, ListaActividades = listaCorrecto });
                lista.Add(new NotificacionActividadParaEjecutarDTO() { EsCorrecto = false, ListaActividades = listaIncorrecto });

                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Ejecutar actividades masivas whatsapp ", DateTime.Now.ToString("MM/dd/yyyy h:mm tt")),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(lista)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                mailservice.SetData(mailData);
                mailservice.SendMessageTask();

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque - Fischer Valdez - Gian Miranda
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Metodo llamado desde complementos para la ejecucion automatica de Mailing operaciones
        /// </summary>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarActividadesAutomaticasMailingOperaciones([FromBody] List<ActividadParaEjecutarDTO> ListaActividadesAutomaticasMailingOperaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lista = new List<NotificacionActividadParaEjecutarDTO>();
                var listaCorrecto = new List<ActividadParaEjecutarDTO>();
                var listaIncorrecto = new List<ActividadParaEjecutarDTO>();
                
                var _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);
                var conjuntoLista = new ConjuntoListaController(_integraDBContext);
                //var configuracionEnvioMailing = new ConfiguracionEnvioMailingController(_integraDBContext);
                var mailingEnvioAutomatico = new MailingEnvioAutomaticoController(_integraDBContext);

                foreach (var actividadMailingOperaciones in ListaActividadesAutomaticasMailingOperaciones)
                {
                    try
                    {
                        if (actividadMailingOperaciones.ActivoEjecutarFiltro == true)
                        {
                            conjuntoLista.Ejecutar(actividadMailingOperaciones.IdConjuntoLista.Value);
                        }
                        var mailingDetalleEnviar = _repConfiguracionEnvioMailing.ObtenerConfiguracionPorConjuntoLista(actividadMailingOperaciones.IdConjuntoLista.Value);
                        mailingEnvioAutomatico.ProcesarLista(mailingDetalleEnviar);
                        mailingEnvioAutomatico.EnviarLista(mailingDetalleEnviar);
                        listaCorrecto.Add(actividadMailingOperaciones);
                    }
                    catch (Exception e)
                    {
                        listaIncorrecto.Add(actividadMailingOperaciones);
                    }
                }

                lista.Add(new NotificacionActividadParaEjecutarDTO() { EsCorrecto = true, ListaActividades = listaCorrecto });
                lista.Add(new NotificacionActividadParaEjecutarDTO() { EsCorrecto = false, ListaActividades = listaIncorrecto });

                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Ejecutar actividades masivas mailing ", DateTime.Now.ToString("MM/dd/yyyy h:mm tt")),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(lista)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                mailservice.SetData(mailData);
                mailservice.SendMessageTask();
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarActividadesMailingMasivoMarketing([FromBody] List<ActividadParaEjecutarDTO> ListaActividadesAutomaticasMailingMarketing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DateTime horaEjecucion = DateTime.Now;

                var lista = new List<NotificacionActividadParaEjecutarDTO>();
                var listaCorrecto = new List<ActividadParaEjecutarDTO>();
                var listaIncorrecto = new List<ActividadParaEjecutarDTO>();

                var _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);
                var conjuntoLista = new ConjuntoListaController(_integraDBContext);
                //var configuracionEnvioMailing = new ConfiguracionEnvioMailingController(_integraDBContext);
                var mailingEnvioAutomatico = new MailingEnvioAutomaticoController(_integraDBContext);

                foreach (var actividadMailingMarketing in ListaActividadesAutomaticasMailingMarketing)
                {
                    try
                    {
                        if (actividadMailingMarketing.ActivoEjecutarFiltro == true)
                        {
                            // NOTA: Volver al estado normal
                            conjuntoLista.Ejecutar(actividadMailingMarketing.IdConjuntoLista.Value);
                        }           
                        var mailingDetalleEnviar = _repConfiguracionEnvioMailing.ObtenerConfiguracionPorConjuntoListaDinamica(actividadMailingMarketing.IdConjuntoLista.Value);
                        /*Oportunidad con la configuracion*/
                        mailingEnvioAutomatico.ProcesarListaNuevasOportunidades(mailingDetalleEnviar, horaEjecucion);
                        mailingEnvioAutomatico.EnviarListaDinamica(mailingDetalleEnviar);
                        listaCorrecto.Add(actividadMailingMarketing);       
                    }
                    catch (Exception e)
                    {
                        listaIncorrecto.Add(actividadMailingMarketing);
                    }
                }
                lista.Add(new NotificacionActividadParaEjecutarDTO() { EsCorrecto = true, ListaActividades = listaCorrecto });
                lista.Add(new NotificacionActividadParaEjecutarDTO() { EsCorrecto = false, ListaActividades = listaIncorrecto });

                List<string> correos = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Ejecutar actividades masivas mailing ", DateTime.Now.ToString("MM/dd/yyyy h:mm tt")),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(lista)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
