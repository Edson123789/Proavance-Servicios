using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AvatarController
    /// <summary>
    /// Autor: Jashin Salazar
    /// Fecha: 28/07/2021
    /// <summary>
    /// Gestión de modificacion de avatar
    /// </summary>
    [Route("api/Avatar")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AvatarController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 28/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para la modificacion de avatar
        /// </summary>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                AvatarBO avatar = new AvatarBO(_integraDBContext);
                var resultado = avatar.ObtenerCaracteristicas();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 28/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene avatar por usuario
        /// </summary>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        [Route("[action]/{Usuario}")]
        [HttpGet]
        public ActionResult ObtenerAvatar(string Usuario)
        {
            try
            {
                AvatarBO avatar = new AvatarBO(_integraDBContext);
                var resultado = avatar.ObtenerAvatar(Usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 28/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene avatar por usuario
        /// </summary>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarAvatar([FromBody] AvatarDTO Avatar)
        {
            try
            {
                AvatarBO avatar = new AvatarBO(_integraDBContext);
                var resultado = avatar.ActualizarAvatar(Avatar);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
