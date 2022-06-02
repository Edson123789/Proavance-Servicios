using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SmsConfiguracionEnvio
    /// <summary>
    /// Autor: Gian Miranda
    /// Fecha: 10/12/2021
    /// <summary>
    /// Gestión de la configuracion de envio de SMS
    /// </summary>
    [Route("api/SmsConfiguracionEnvio")]
    public class SmsConfiguracionEnvioController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly SmsConfiguracionEnvioRepositorio _repSmsConfiguracionEnvio;

        public SmsConfiguracionEnvioController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;

            _repSmsConfiguracionEnvio = new SmsConfiguracionEnvioRepositorio(_integraDBContext);
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 10/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta la configuracion Sms
        /// </summary>
        /// <param name="ListaSmsConfiguracionEnvio">Lista de objetos de clase InsertarSmsConfiguracionEnvioDTO</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarSmsConfiguracionEnvio([FromBody] List<InsertarSmsConfiguracionEnvioDTO> ListaSmsConfiguracionEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var smsConfiguracion in ListaSmsConfiguracionEnvio)
                {
                    if (smsConfiguracion.Id != 0)
                    {
                        SmsConfiguracionEnvioBO smsConfiguracionEnvioADesactivar = _repSmsConfiguracionEnvio.FirstById(smsConfiguracion.Id);
                        smsConfiguracionEnvioADesactivar.Activo = false;
                        smsConfiguracionEnvioADesactivar.UsuarioModificacion = smsConfiguracion.Usuario;
                        smsConfiguracionEnvioADesactivar.FechaModificacion = DateTime.Now;
                        _repSmsConfiguracionEnvio.Update(smsConfiguracionEnvioADesactivar);
                    }

                    SmsConfiguracionEnvioBO smsConfiguracionEnvio = new SmsConfiguracionEnvioBO(_integraDBContext)
                    {
                        Nombre = smsConfiguracion.Nombre,
                        Descripcion = smsConfiguracion.Descripcion,
                        IdPersonal = smsConfiguracion.IdPersonal,
                        IdPlantilla = smsConfiguracion.IdPlantilla,
                        IdConjuntoListaDetalle = smsConfiguracion.IdConjuntoListaDetalle,
                        IdPgeneral = smsConfiguracion.IdPGeneral,
                        Activo = true,
                        Estado = true,
                        UsuarioCreacion = smsConfiguracion.Usuario,
                        UsuarioModificacion = smsConfiguracion.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    _repSmsConfiguracionEnvio.Insert(smsConfiguracionEnvio);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
