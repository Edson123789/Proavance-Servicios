using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/Expositor
    /// Autor: Fischer Valdez - Wilber Choque - Carlos Crispin - Gian Miranda
    /// Fecha: día/mes/año
    /// <summary>
    /// Descripción o resumen del controlador
    /// </summary>
    [Route("api/WhatsAppConfiguracionEnvio")]
    public class WhatsAppConfiguracionEnvioController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public WhatsAppConfiguracionEnvioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// <summary>
        /// Obtiene la configuracion de whatsapp para operaciones
        /// </summary>
        /// <param name="IdConjuntoLista"></param>
        /// <returns></returns>
        // Carlos comentado
        //[Route("[Action]/{IdConjuntoLista}")]
        //[HttpGet]
        public List<ConjuntoListaDetalleWhatsAppDTO> ObtenerListaConfiguracionOperaciones(int IdConjuntoLista)
        {
            try
            {

                WhatsAppConfiguracionEnvioRepositorio _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioRepositorio(_integraDBContext);
                WhatsAppConfiguracionEnvioPorProgramaRepositorio _repWhatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaRepositorio(_integraDBContext);
                var listaResultado = _repWhatsAppConfiguracionEnvio.ObtenerConfiguracionPorIdConjuntoLista(IdConjuntoLista);

                foreach (var resultado in listaResultado)
                {
                    resultado.ProgramaPrincipal = _repWhatsAppConfiguracionEnvioPorPrograma.GetBy(w => w.IdWhatsAppConfiguracionEnvio == resultado.Id && w.IdTipoEnvioPrograma == 1, y => new WhatsAppConfiguracionEnvioPorProgramaDTO { IdPgeneral = y.IdPgeneral }).ToList();
                    resultado.ProgramaSecundario = _repWhatsAppConfiguracionEnvioPorPrograma.GetBy(w => w.IdWhatsAppConfiguracionEnvio == resultado.Id && w.IdTipoEnvioPrograma == 2, y => new WhatsAppConfiguracionEnvioPorProgramaDTO { IdPgeneral = y.IdPgeneral }).ToList();
                }
                return listaResultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Esta funcion Obtiene la configuracion de whatsapp del conjunto de lista
        /// </summary>
        /// <param name="IdConjuntoLista">Id del conjunto lista (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos de clase ConjuntoListaDetalleWhatsAppDTO</returns>
        [Route("[Action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerListaConfiguracion(int IdConjuntoLista)
        {
            try
            {

                var _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioRepositorio(_integraDBContext);
                var _repWhatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaRepositorio(_integraDBContext);
                var resultado = _repWhatsAppConfiguracionEnvio.ObtenerConfiguracionPorIdConjuntoLista(IdConjuntoLista);

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

        /// Autor: Gian Miranda
        /// <summary>
        /// Inserta el registro de la caida del servidor
        /// </summary>
        /// <param name="Servidor">Nombre del servidor que registra la caida</param>
        /// <returns>Response 200 con true, caso contrario 400</returns>
        [Route("[Action]/{Servidor}")]
        [HttpGet]
        public ActionResult InsertarRegistroCaidaServidor(string Servidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioRepositorio(_integraDBContext);

                _repWhatsAppConfiguracionEnvio.InsertarRegistroCaidaServidor(Servidor);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// <summary>
        /// Inserta el registro de la caida del servidor
        /// </summary>
        /// <param name="Servidor">Nombre del servidor que registra la caida</param>
        /// <returns>Response 200 con true, caso contrario 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizaEstadoEnvioAutomaticoWhatsApp([FromBody] WhatsAppHabilitadoRecuperacionDTO EstadoWhatsAppHabilitado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioRepositorio(_integraDBContext);
                var _repRegistroRecuperacionWhatsApp = new RegistroRecuperacionWhatsAppRepositorio(_integraDBContext);

                _repWhatsAppConfiguracionEnvio.ActualizarEstadoWhatsAppRecuperacion(EstadoWhatsAppHabilitado.Tipo, EstadoWhatsAppHabilitado.UsuarioResponsable, EstadoWhatsAppHabilitado.EstadoHabilitado);

                if (!EstadoWhatsAppHabilitado.EstadoHabilitado)
                {
                    _repRegistroRecuperacionWhatsApp.DesactivarCompletadoRegistroWhatsApp(EstadoWhatsAppHabilitado.UsuarioResponsable);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarWhatsAppConfiguracionEnvio([FromBody] List<InsertarWhatsAppConfiguracionEnvioDTO> ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                WhatsAppConfiguracionEnvioRepositorio _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioRepositorio(_integraDBContext);
                WhatsAppConfiguracionEnvioPorProgramaRepositorio _repWhatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaRepositorio(_integraDBContext);

                //WhatsAppConfiguracionEnvioBO whatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioBO();
                //WhatsAppConfiguracionEnvioPorProgramaBO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaBO();

                foreach (var WhatsAppConfiguracion in ObjetoDTO)
                {
                    if (WhatsAppConfiguracion.Id == 0)
                    {
                        WhatsAppConfiguracionEnvioBO whatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioBO();
                        whatsAppConfiguracionEnvio.Nombre = WhatsAppConfiguracion.Nombre;
                        whatsAppConfiguracionEnvio.Descripcion = WhatsAppConfiguracion.Descripcion;
                        whatsAppConfiguracionEnvio.IdPersonal = WhatsAppConfiguracion.IdPersonal;
                        whatsAppConfiguracionEnvio.IdPlantilla = WhatsAppConfiguracion.IdPlantilla;
                        whatsAppConfiguracionEnvio.IdConjuntoListaDetalle = WhatsAppConfiguracion.IdConjuntoListaDetalle;
                        whatsAppConfiguracionEnvio.Activo = true;
                        whatsAppConfiguracionEnvio.Estado = true;
                        whatsAppConfiguracionEnvio.UsuarioCreacion = WhatsAppConfiguracion.Usuario;
                        whatsAppConfiguracionEnvio.UsuarioModificacion = WhatsAppConfiguracion.Usuario;
                        whatsAppConfiguracionEnvio.FechaCreacion = DateTime.Now;
                        whatsAppConfiguracionEnvio.FechaModificacion = DateTime.Now;

                        _repWhatsAppConfiguracionEnvio.Insert(whatsAppConfiguracionEnvio);
                        if (WhatsAppConfiguracion.ProgramaPrincipal != null)
                        {
                            foreach (var programa in WhatsAppConfiguracion.ProgramaPrincipal)
                            {
                                WhatsAppConfiguracionEnvioPorProgramaBO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaBO();

                                whatsAppConfiguracionEnvioPorPrograma.IdWhatsAppConfiguracionEnvio = whatsAppConfiguracionEnvio.Id;
                                whatsAppConfiguracionEnvioPorPrograma.IdPgeneral = programa.Id;
                                whatsAppConfiguracionEnvioPorPrograma.IdTipoEnvioPrograma = 1;
                                whatsAppConfiguracionEnvioPorPrograma.Estado = true;
                                whatsAppConfiguracionEnvioPorPrograma.FechaCreacion = DateTime.Now;
                                whatsAppConfiguracionEnvioPorPrograma.FechaModificacion = DateTime.Now;
                                whatsAppConfiguracionEnvioPorPrograma.UsuarioCreacion = whatsAppConfiguracionEnvio.UsuarioCreacion;
                                whatsAppConfiguracionEnvioPorPrograma.UsuarioModificacion = whatsAppConfiguracionEnvio.UsuarioModificacion;

                                _repWhatsAppConfiguracionEnvioPorPrograma.Insert(whatsAppConfiguracionEnvioPorPrograma);
                            }
                        }

                        if (WhatsAppConfiguracion.ProgramaSecundario != null)
                        {
                            foreach (var programa in WhatsAppConfiguracion.ProgramaSecundario)
                            {
                                WhatsAppConfiguracionEnvioPorProgramaBO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaBO();

                                whatsAppConfiguracionEnvioPorPrograma.IdWhatsAppConfiguracionEnvio = whatsAppConfiguracionEnvio.Id;
                                whatsAppConfiguracionEnvioPorPrograma.IdPgeneral = programa.Id;
                                whatsAppConfiguracionEnvioPorPrograma.IdTipoEnvioPrograma = 2;
                                whatsAppConfiguracionEnvioPorPrograma.Estado = true;
                                whatsAppConfiguracionEnvioPorPrograma.FechaCreacion = DateTime.Now;
                                whatsAppConfiguracionEnvioPorPrograma.FechaModificacion = DateTime.Now;
                                whatsAppConfiguracionEnvioPorPrograma.UsuarioCreacion = whatsAppConfiguracionEnvio.UsuarioCreacion;
                                whatsAppConfiguracionEnvioPorPrograma.UsuarioModificacion = whatsAppConfiguracionEnvio.UsuarioModificacion;

                                _repWhatsAppConfiguracionEnvioPorPrograma.Insert(whatsAppConfiguracionEnvioPorPrograma);
                            }
                        }
                    }
                    else
                    {
                        var WhatsAppEnvio = _repWhatsAppConfiguracionEnvio.FirstById(WhatsAppConfiguracion.Id);
                        WhatsAppEnvio.Activo = false;
                        WhatsAppEnvio.UsuarioModificacion = WhatsAppConfiguracion.Usuario;
                        WhatsAppEnvio.FechaModificacion = DateTime.Now;
                        _repWhatsAppConfiguracionEnvio.Update(WhatsAppEnvio);

                        WhatsAppConfiguracionEnvioBO whatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioBO();
                        whatsAppConfiguracionEnvio.Nombre = WhatsAppConfiguracion.Nombre;
                        whatsAppConfiguracionEnvio.Descripcion = WhatsAppConfiguracion.Descripcion;
                        whatsAppConfiguracionEnvio.IdPersonal = WhatsAppConfiguracion.IdPersonal;
                        whatsAppConfiguracionEnvio.IdPlantilla = WhatsAppConfiguracion.IdPlantilla;
                        whatsAppConfiguracionEnvio.IdConjuntoListaDetalle = WhatsAppConfiguracion.IdConjuntoListaDetalle;
                        whatsAppConfiguracionEnvio.Activo = true;
                        whatsAppConfiguracionEnvio.Estado = true;
                        whatsAppConfiguracionEnvio.UsuarioCreacion = WhatsAppConfiguracion.Usuario;
                        whatsAppConfiguracionEnvio.UsuarioModificacion = WhatsAppConfiguracion.Usuario;
                        whatsAppConfiguracionEnvio.FechaCreacion = DateTime.Now;
                        whatsAppConfiguracionEnvio.FechaModificacion = DateTime.Now;

                        _repWhatsAppConfiguracionEnvio.Insert(whatsAppConfiguracionEnvio);
                        if (WhatsAppConfiguracion.ProgramaPrincipal != null)
                        {
                            foreach (var programa in WhatsAppConfiguracion.ProgramaPrincipal)
                            {
                                WhatsAppConfiguracionEnvioPorProgramaBO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaBO();

                                whatsAppConfiguracionEnvioPorPrograma.IdWhatsAppConfiguracionEnvio = whatsAppConfiguracionEnvio.Id;
                                whatsAppConfiguracionEnvioPorPrograma.IdPgeneral = programa.Id;
                                whatsAppConfiguracionEnvioPorPrograma.IdTipoEnvioPrograma = 1;
                                whatsAppConfiguracionEnvioPorPrograma.Estado = true;
                                whatsAppConfiguracionEnvioPorPrograma.FechaCreacion = DateTime.Now;
                                whatsAppConfiguracionEnvioPorPrograma.FechaModificacion = DateTime.Now;
                                whatsAppConfiguracionEnvioPorPrograma.UsuarioCreacion = whatsAppConfiguracionEnvio.UsuarioCreacion;
                                whatsAppConfiguracionEnvioPorPrograma.UsuarioModificacion = whatsAppConfiguracionEnvio.UsuarioModificacion;

                                _repWhatsAppConfiguracionEnvioPorPrograma.Insert(whatsAppConfiguracionEnvioPorPrograma);
                            }
                        }

                        if (WhatsAppConfiguracion.ProgramaSecundario != null)
                        {
                            foreach (var programa in WhatsAppConfiguracion.ProgramaSecundario)
                            {
                                WhatsAppConfiguracionEnvioPorProgramaBO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaBO();

                                whatsAppConfiguracionEnvioPorPrograma.IdWhatsAppConfiguracionEnvio = whatsAppConfiguracionEnvio.Id;
                                whatsAppConfiguracionEnvioPorPrograma.IdPgeneral = programa.Id;
                                whatsAppConfiguracionEnvioPorPrograma.IdTipoEnvioPrograma = 2;
                                whatsAppConfiguracionEnvioPorPrograma.Estado = true;
                                whatsAppConfiguracionEnvioPorPrograma.FechaCreacion = DateTime.Now;
                                whatsAppConfiguracionEnvioPorPrograma.FechaModificacion = DateTime.Now;
                                whatsAppConfiguracionEnvioPorPrograma.UsuarioCreacion = whatsAppConfiguracionEnvio.UsuarioCreacion;
                                whatsAppConfiguracionEnvioPorPrograma.UsuarioModificacion = whatsAppConfiguracionEnvio.UsuarioModificacion;

                                _repWhatsAppConfiguracionEnvioPorPrograma.Insert(whatsAppConfiguracionEnvioPorPrograma);
                            }
                        }
                    }
                }

                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

    }
}
