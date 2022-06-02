using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.BO.AsteriskCdr;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/Scraping
    /// Autor: Ansoli Espinoza
    /// Fecha: 26-01-2021
    /// <summary>
    /// Controlador para el consumo de informacion de Asterisk
    /// </summary>
    [Route("api/AsteriskCdr")]
    public class AsteriskCdrController : Controller
    {
        /// Tipo Función: GET
        /// Autor: Ansoli Espinoza
        /// Fecha: 26-01-2021
        /// Versión: 1.0
        /// <summary>
        /// Importa las llamadas pendientes de asterisk a v4
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ImportarLlamadas()
        {
            try
            {
                AsteriskCdrBO bo = new AsteriskCdrBO();
                return Ok(bo.ImportarLlamadasPendientes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
