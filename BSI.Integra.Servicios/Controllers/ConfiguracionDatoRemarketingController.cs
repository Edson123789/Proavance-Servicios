using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionDatoRemarketing
    /// <summary>
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// Gestión de la configuración de datos de remarketing
    /// </summary>
    [Route("api/ConfiguracionDatoRemarketing")]
    public class ConfiguracionDatoRemarketingController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        private readonly ConfiguracionDatoRemarketingBO ConfiguracionDatoRemarketing;

        public ConfiguracionDatoRemarketingController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            ConfiguracionDatoRemarketing = new ConfiguracionDatoRemarketingBO(_integraDBContext);
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 20/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de configuracion de remarketing
        /// </summary>
        /// <returns>Response 200 con lista de objetos de clase ConfiguracionDatoRemarketingAgrupadoGrillaDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaConfiguracionRemarketing()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var configuracionesExistentes = ConfiguracionDatoRemarketing.ObtenerConfiguracionesDatoRemarketing();

                return Ok(configuracionesExistentes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 20/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la configuracion de dato remarketing
        /// </summary>
        /// <param name="ConfiguracionDatoRemarketingAActualizar">Objeto de clase ConfiguracionDatoRemarketingDTO</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarConfiguracionDatoRemarketing([FromBody] ConfiguracionDatoRemarketingDTO ConfiguracionDatoRemarketingAActualizar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool resultadoActualizacion = ConfiguracionDatoRemarketing.ActualizarListaConfiguracionDatoRemarketingGeneral(ConfiguracionDatoRemarketingAActualizar);

                return Ok(resultadoActualizacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 20/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina la configuracion de dato remarketing
        /// </summary>
        /// <param name="ConfiguracionDatoRemarketingAEliminar">Objeto de clase ConfiguracionDatoRemarketingAEliminarDTO</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarConfiguracionDatoRemarketing([FromBody] ConfiguracionDatoRemarketingAEliminarDTO ConfiguracionDatoRemarketingAEliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool resultadoActualizacion = ConfiguracionDatoRemarketing.EliminarConfiguracionDatoRemarketingGeneral(ConfiguracionDatoRemarketingAEliminar);

                return Ok(resultadoActualizacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 20/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de combos de configuracion de dato remarketing
        /// </summary>
        /// <returns>Response 200 con lista de objetos de clase ComboConfiguracionDatoRemarketingDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosConfiguracionDatoRemarketing()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultadoCombos = ConfiguracionDatoRemarketing.ObtenerCombosParaConfiguracionDatoRemarketing();

                return Ok(resultadoCombos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
