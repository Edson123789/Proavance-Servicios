using System;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ChatIntegraHistorialAsesor")]
    public class ChatIntegraHistorialAsesorController : Controller
    {
        public ChatIntegraHistorialAsesorController()
        {
        }

        [Route("[action]/{IdAsesorChatDetalle}")]
        [HttpGet]
        public ActionResult ObtenerHistoricoDetallesPorAsesorChatDetalle(int IdAsesorChatDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChatIntegraHistorialAsesorRepositorio chatIntegraHistorialAsesor = new ChatIntegraHistorialAsesorRepositorio();
                return Ok(chatIntegraHistorialAsesor.ObtenerHistoricoDetallesPorAsesorChatDetalle(IdAsesorChatDetalle));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerTodoHistorialChatsPorAsesor(int IdPersonal)
         {
            try
            {
                ChatIntegraHistorialAsesorBO chatIntegraHistorialAsesor = new ChatIntegraHistorialAsesorBO();
                return Ok(chatIntegraHistorialAsesor.ObtenerTodoHistorialChatsPorAsesor(IdPersonal));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 25/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Todo historial chats Por Asesor de Soporte
        /// </summary>
        /// <returns> List<ChatHistorialAsesor> </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerTodoHistorialChatsPorAsesorSoporte(int IdPersonal)
        {
            try
            {
                ChatIntegraHistorialAsesorBO chatIntegraHistorialAsesor = new ChatIntegraHistorialAsesorBO();
                return Ok(chatIntegraHistorialAsesor.ObtenerTodoHistorialChatsPorAsesorSoporte(IdPersonal));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 22/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial chats Por Alumno
        /// </summary>
        /// <returns> List<ChatHistorialAsesor> </returns>
        [Route("[action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerTodoHistorialChatsPorAlumno(int IdAlumno)
        {
            try
            {
                ChatIntegraHistorialAsesorBO chatIntegraHistorialAsesor = new ChatIntegraHistorialAsesorBO();
                return Ok(chatIntegraHistorialAsesor.ObtenerTodoHistorialChatsPorAlumno(IdAlumno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}