using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: LlamadaWebphoneAsterisk
    /// Autor: Ansoli Espinoza
    /// Fecha: 26-02-2021
    /// <summary>
    /// Controlador para el consumo de informacion de llamadas de Asterisk
    /// </summary>
    [Route("api/LlamadaWebphoneAsterisk")]
    public class LlamadaWebphoneAsteriskController : ControllerBase
    {
        /// Tipo Función: GET
        /// Autor: Ansoli Espinoza
        /// Fecha: 26-02-2021
        /// Versión: 1.0
        /// <summary>
        /// Devuelve el listado de llamadas pendientes de respaldar de asterisk
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ListadoPendienteRespaldar()
        {
            try
            {
                LlamadaWebphoneAsteriskRepositorio repo = new LlamadaWebphoneAsteriskRepositorio();
                return Ok(repo.ListadoLlamadaPendienteRespaldar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Ansoli Espinoza
        /// Fecha: 26-02-2021
        /// Versión: 1.0
        /// <summary>
        /// Devuelve el listado de llamadas pendientes de eliminar de disco
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ListadoPendienteEliminar()
        {
            try
            {
                LlamadaWebphoneAsteriskRepositorio repo = new LlamadaWebphoneAsteriskRepositorio();
                return Ok(repo.ListadoLlamadaPendienteEliminar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Ansoli Espinoza
        /// Fecha: 26-02-2021
        /// Versión: 1.0
        /// <summary>
        /// Registra la url del blob
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarUrlBlob(LlamadaAsteriskRegistrarUrlDTO formulario)
        {
            try
            {
                LlamadaWebphoneAsteriskRepositorio repo = new LlamadaWebphoneAsteriskRepositorio();
                var bo = repo.FirstById(formulario.Id);
                bo.IdProveedorNube = formulario.IdProveedorNube;
                bo.Url = formulario.Url;
                bo.NroBytes = formulario.NroBytes;
                bo.FechaSubida = DateTime.Now;

                bo.FechaModificacion = DateTime.Now;
                bo.UsuarioModificacion = formulario.Usuario;
                repo.Update(bo);

                return Ok(bo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Ansoli Espinoza
        /// Fecha: 26-02-2021
        /// Versión: 1.0
        /// <summary>
        /// Registra la eliminacion del archivo de grabacion
        /// </summary>
        /// <returns></returns>
        [Route("[Action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult RegistrarEliminacion(int Id, string Usuario)
        {
            try
            {
                LlamadaWebphoneAsteriskRepositorio repo = new LlamadaWebphoneAsteriskRepositorio();
                var bo = repo.FirstById(Id);
                bo.EsEliminado = true;
                bo.FechaEliminacion = DateTime.Now;
                
                bo.FechaModificacion = DateTime.Now;
                bo.UsuarioModificacion = Usuario;
                repo.Update(bo);

                return Ok(bo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
