using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using System.Net;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace serviciosproavance.GlobalErrorHandling.CustomExceptionMiddleware
{
   
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ExceptionMiddleware
    {
        private LogRepositorio _logRepositorio;
        private readonly RequestDelegate _next;
        //private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next/*, ILoggerManager logger*/)
        {
            //_logger = logger;
            _next = next;
            _logRepositorio = new LogRepositorio();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Something went wrong: {ex}");
                _logRepositorio.Insert(new BSI.Integra.Aplicacion.Transversal.BO.LogBO()
                {
                    Ip = "-",
                    Usuario = "wchoque",
                    Maquina = "-",
                    Ruta = "-",
                    Parametros = "",
                    Mensaje = "-",
                    Excepcion = JsonConvert.SerializeObject(e),
                    Tipo = "",
                    IdPadre = null,
                    Estado = true,
                    UsuarioCreacion = "wchoque",
                    UsuarioModificacion = "wchoque",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                });
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = JsonConvert.SerializeObject(exception)//"Internal Server Error from the custom middleware."
            }.ToString());
        }
    }
}
