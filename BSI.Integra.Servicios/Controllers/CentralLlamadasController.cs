using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CentralLlamadaController
    /// Autor: Jashin Salazar
    /// Fecha: 21/07/2021
    /// <summary>
    /// Contiene los endpoints para regularizar y verificar 
    /// </summary>
    [Route("api/CentrallLlamada")]
    public class CentralLlamadasController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly CentralLlamadaRepositorio _repCentralLlamada;
        public CentralLlamadasController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repCentralLlamada = new CentralLlamadaRepositorio();
        }

        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 21/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Verifica que se este descargando las llamadas correctamente.
        /// </summary>
        /// <returns> Status 200 o 400, acorde al flujo.</returns>
        // GET: api/<controller>
        [Route("[action]")]
        [HttpGet]
        public ActionResult VerificarCentralLlamada()
        {
            try
            {
                CentralLlamadaDTO resultado = new CentralLlamadaDTO();
                resultado = _repCentralLlamada.ValidarCentralLlamada();
                if (resultado.Estado == 1)
                {
                    var mensajeErroneo = "";
                    if (resultado.Mensaje == "")
                    {
                        mensajeErroneo = "Sin error especificado";
                    }
                    else
                    {
                        mensajeErroneo = resultado.Mensaje;
                    }
                    //enviarCorreo
                    var correosPersonalizados = new List<string>
                    {
                        "jsalazart@bsginstitute.com"
                    };
                    var MailservicePersonalizado = new TMK_MailServiceImpl();
                    var mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "jsalazart@bsginstitute.com",
                        Recipient = string.Join(",", correosPersonalizados),
                        Subject = string.Concat("Error al descargar llamadas en CentralLlamadas"),
                        Message = $@"
                        <p style='color: red;'><strong>----Error al descargar llamadas----</strong></p>
                        <p>Se encontro un error al procesar las descargas en CentralLLamada y se soluciono automaticamente.</span></strong></p>
                        <p><span>Id de Error: {resultado.IdErroneo}</span></p>
                        <p><span>Error: {mensajeErroneo}</span></p>
                        <p><strong>Hora:</strong></p>
                        <p><span style='color: orange;'>{DateTime.Now}</span></p> 
                        ",
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    MailservicePersonalizado.SetData(mailDataPersonalizado);
                    MailservicePersonalizado.SendMessageTask();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
