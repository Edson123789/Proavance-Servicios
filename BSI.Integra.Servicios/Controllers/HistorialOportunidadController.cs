using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/HistorialOportunidad")]
    public class HistorialOportunidadController : Controller
    {

        [Route("[action]/{idContacto}")]
        [HttpGet]
        public ActionResult ObtenerDetalleChatPorIdInteraccion(int idContacto)
        {
            try
            {
                HistorialOportunidadBO historialOportunidad = new HistorialOportunidadBO();
                return Ok(historialOportunidad.ObtenerHistorialOportunidadPorIdContacto(idContacto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("[action]/{idInteraccion}")]
        [HttpGet]
        public ActionResult ObtenerHistorialOportunidadPorIdInteraccion(int idInteraccion)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
            try
            {
				InteraccionChatIntegraRepositorio interaccionChatIntegra = new InteraccionChatIntegraRepositorio();
				OportunidadRepositorio historialOportunidad = new OportunidadRepositorio();
				//InteraccionChatIntegraBO interaccionChatIntegra = new InteraccionChatIntegraBO();
				int idContacto = interaccionChatIntegra.ObtenerIdContactoPorIdInteraccionChatIntegra(idInteraccion);
                //HistorialOportunidadBO historialOportunidad = new HistorialOportunidadBO();
                return Ok(historialOportunidad.ObtenerHistorialOportunidadPorIdContacto(idContacto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
