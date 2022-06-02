using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;//...
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    public class BaseVistasController<TEntity> : Controller
        where TEntity : BaseVistaEntity
    {
        public IIntegraVistaRepository<TEntity> _repositorio;
        //public IIntegraVistaRepository<TLog> _loggerRepositorio;
        public string ip;
        public string mensajeExcepcion;

        public BaseVistasController(IIntegraVistaRepository<TEntity> repositorio /*, IIntegraVistaRepository<TLog> loggerRepositorio*/)
        {
            _repositorio = repositorio;
            ip = HttpContext != null ? HttpContext.Connection.RemoteIpAddress.ToString() : "localhost";
            //_loggerRepositorio = loggerRepositorio;
        }

        [HttpGet("{criterio}")]
        public IActionResult GetVistaXCriterio(string criterio)
        {
            //try
            //{
            //    byte[] data = Convert.FromBase64String(criterio);
            //    var decodedCriterio = Encoding.UTF8.GetString(data);

            //    //var query = _repositorio.Obtener().Where(decodedCriterio);
            //    //var query = _repositorio.ObtenerTabla();

            //    integraDBContext contexto = new integraDBContext();
            //    var query = contexto.VAgendaActividadProgramada.AsQueryable().Where(decodedCriterio).ToList();

            //    if (!query.Any())
            //    {
            //        mensajeExcepcion = $"{"NOMBRE DE LA TABLA"} no esta registrado o no esta activo";
            //        throw new Exception(mensajeExcepcion);
            //    }

            //    return Ok(query);
            //}
            //catch (Exception exception)
            //{
            //    //mensajeExcepcion = $"Error en {ControllerContext.ActionDescriptor.ActionName} - Mensaje:{exception.Message} - InnerException: {exception.InnerException.Message}";
            //    mensajeExcepcion = $"Error en {ControllerContext.ActionDescriptor.ActionName} - Mensaje:{exception.Message}";
            //    return StatusCode(500, "Hubo in problema mientras se procesaba la peticion");
            //}
            return Ok();
        }

        [HttpGet("InsertLog/")]
        protected void InsertLog(TLog _tlog)
        {
            //_loggerRepositorio.Insertar(_tlog);
        }
    }
}
