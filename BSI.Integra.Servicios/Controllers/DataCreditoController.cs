using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTOsComercial;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/DataCredito")]
    [ApiController]
    public class DataCreditoController : ControllerBase
    {
        // GET api/values
        [Route("[Action]/{numero}/{apellido}/{usuario}")]
        [HttpGet]
        public ActionResult Get(string numero, string apellido, string usuario)
        {
            try
            {
                DataCreditoBO bo = new DataCreditoBO();
                var resultado = bo.ConsultarAlumnoColombia(numero, apellido, 1, usuario);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conteo de Oportunidades Cerradas por el Asesor por Grupos
        /// </summary>
        /// <returns> Objeto DTO </returns>
        /// <returns> objetoDTO: SeguimientoAsesorDTO </returns>
        /// [Route("[Action]/{numero}/{apellido}/{usuario}")]
        [Route("[Action]/{idAlumno}/{usuario}")]
        [HttpGet]
        public ActionResult ObtenerInformacionDataCredito(int idAlumno, string usuario)
        {
            try
            {
                DataCreditoBO bo = new DataCreditoBO();
                DataCreditoBusquedaRepositorio _repDataCreditoBusqueda = new DataCreditoBusquedaRepositorio();
                var idDataCredito = _repDataCreditoBusqueda.ObtenerIDDataCredito(idAlumno);
                DataCreditoRespuestaDTO respuesta = new DataCreditoRespuestaDTO();

                if (idDataCredito.Id != null)
                {
                    //bo.ConsultarAlumnoColombia(idDataCredito.NroDocumento, idDataCredito.ApellidoPaterno, 1, usuario);
                    //idDataCredito = _repDataCreditoBusqueda.ObtenerIDDataCredito(idAlumno);
                    var informacionPersonal = _repDataCreditoBusqueda.ObtenerInformacionDataCredito((int)idDataCredito.Id);
                    var tarjetas = _repDataCreditoBusqueda.ObtenerHistorialTarjetasDataCredito((int)idDataCredito.Id);
                    var deudas = _repDataCreditoBusqueda.ObtenerHistorialDeudasDataCredito((int)idDataCredito.Id);

                    respuesta.Informacion = informacionPersonal;
                    respuesta.Tarjeta = tarjetas;
                    respuesta.Credito = deudas;
                }
                else
                {
                    respuesta = null;
                }
                
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conteo de Oportunidades Cerradas por el Asesor por Grupos
        /// </summary>
        /// <returns> Objeto DTO </returns>
        /// <returns> objetoDTO: SeguimientoAsesorDTO </returns>
        /// [Route("[Action]/{numero}/{apellido}/{usuario}")]
        [Route("[Action]/{documento}/{idAlumno}/{usuario}")]
        [HttpGet]
        public ActionResult ActualizarInformacionDataCredito(string documento, int idAlumno, string usuario)
        {
            try
            {
                DataCreditoBO bo = new DataCreditoBO();
                DataCreditoBusquedaRepositorio _repDataCreditoBusqueda = new DataCreditoBusquedaRepositorio();
                var idDataCredito = _repDataCreditoBusqueda.ObtenerIDDataCredito(idAlumno);
                DataCreditoRespuestaDTO respuesta = new DataCreditoRespuestaDTO();

                bo.ConsultarAlumnoColombia(documento, idDataCredito.ApellidoPaterno, 1, usuario);
                idDataCredito = _repDataCreditoBusqueda.ObtenerIDDataCredito(idAlumno);

                return Ok(idDataCredito);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
