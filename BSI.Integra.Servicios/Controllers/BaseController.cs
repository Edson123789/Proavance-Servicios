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
using BSI.Integra.Persistencia.Models.Vistas;
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
    public class BaseController<TEntity, TV> : Controller
        where TEntity : BaseEntity, new() where TV : AbstractValidator<TEntity>, new()
    {
        public IIntegraRepository<TEntity> _repositorio;
        public ILogger<BaseController<TEntity, TV>> _logger;
        public IIntegraRepository<TLog> _loggerRepositorio;
        public string ip;
        public string mensajeExcepcion;
        private IIntegraRepository<TAsignacionAutomatica> repositorio;
        private ILogger<BaseController<TConjuntoAnuncioFacebook, ValidadorConjuntoAnuncioFacebookDTO>> logger;
        private IIntegraRepository<TLog> repositoriologg;

        public BaseController(IIntegraRepository<TEntity> repositorio, ILogger<BaseController<TEntity, TV>> logger, IIntegraRepository<TLog> loggerRepositorio)
        {
            _repositorio = repositorio;
            _logger = logger;
            ip = HttpContext != null ? HttpContext.Connection.RemoteIpAddress.ToString() : "localhost";
            _loggerRepositorio = loggerRepositorio;
        }

        public BaseController(IIntegraRepository<TAsignacionAutomatica> repositorio, ILogger<BaseController<TConjuntoAnuncioFacebook, ValidadorConjuntoAnuncioFacebookDTO>> logger, IIntegraRepository<TLog> repositoriologg)
        {
            this.repositorio = repositorio;
            this.logger = logger;
            this.repositoriologg = repositoriologg;
        }

        [HttpGet()]
        public IActionResult GetTabla()
        {
            try
            {
                var objLista = _repositorio.ObtenerTabla();

                if (!objLista.Any())
                {
                    mensajeExcepcion = $"{"NOMBRE DE LA TABLA"} no esta registrado o no esta activo";
                    _logger.LogInformation(mensajeExcepcion);
                    throw new Exception(mensajeExcepcion);
                    //_logger.LogInformation($"{"NOMBRE DE LA TABLA"} no esta registrado o no esta activo");
                    //return NotFound();
                }
                return Ok(objLista);
            }
            catch (Exception ex)
            {
                //throw;
                InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "....//", Parametros = "parametros", Mensaje = ex.Message, Excepcion = ex.Message, Tipo = "SELECT", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                return NotFound(ex.Message);
            }
        }

        [HttpGet("InsertLog/")]
        protected void InsertLog(TLog _tlog)
        {
            _loggerRepositorio.Insertar(_tlog);
        }

        [HttpGet("{criterio}/{orden?}")]
        public IActionResult GetTablaXCriterio(string criterio, string orden)
        {
            try
            {
                byte[] data = Convert.FromBase64String(criterio);
                //var decodedCriterio = System.Text.ASCIIEncoding.ASCII.GetString(data);
                var decodedCriterio = Encoding.UTF8.GetString(data);


                var query = _repositorio.Obtener().Where(decodedCriterio);

                if (orden != null)
                {
                    byte[] dataOrden = System.Convert.FromBase64String(orden);
                    var decodedOrden = System.Text.ASCIIEncoding.ASCII.GetString(dataOrden);
                    //query = query.OrderBy(decodedOrden);
                }

                if (!query.Any())
                {
                    mensajeExcepcion = $"{"NOMBRE DE LA TABLA"} no esta registrado o no esta activo";
                    throw new Exception(mensajeExcepcion);
                }

                return Ok(query);
            }
            catch (Exception exception)
            {
                //mensajeExcepcion = $"Error en {ControllerContext.ActionDescriptor.ActionName} - Mensaje:{exception.Message} - InnerException: {exception.InnerException.Message}";
                mensajeExcepcion = $"Error en {ControllerContext.ActionDescriptor.ActionName} - Mensaje:{exception.Message}";
                _logger.LogCritical(mensajeExcepcion);
                InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "GetTablaXCriterio", Parametros = "parametros", Mensaje = this.mensajeExcepcion, Excepcion = exception.Message, Tipo = "SELECT", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                return StatusCode(500, "Hubo in problema mientras se procesaba la peticion");
            }
        }

        [HttpGet("id/{id}")]
        public IActionResult GetTablaxID(int id)
        {
            try
            {

                var temp = _repositorio.ObtenerPorId(id);

                if (temp == null)
                {
                    return NotFound();
                }

                return Ok(temp);
            }
            catch (Exception ex)
            {
                //throw;
                mensajeExcepcion = "Hubo in problema mientras se procesaba la peticion";
                InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "GetTablaxID", Parametros = "parametros", Mensaje = mensajeExcepcion, Excepcion = ex.Message, Tipo = "SELECT", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                return StatusCode(500, mensajeExcepcion);
            }
            //var temp = _repositorio.GetObjetoXID(id);

        }

        [HttpPost()]
        public IActionResult InsertarDTO([FromBody] TEntity dto)
        {
            try
            {
                dto["Estado"] = true;
                dto["FechaCreacion"] = DateTime.Now;
                dto["FechaModificacion"] = DateTime.Now;

                dto.Estado = (bool)dto["Estado"];
                dto.FechaCreacion = (DateTime)dto["FechaCreacion"];
                dto.FechaModificacion = (DateTime)dto["FechaModificacion"];
                dto.UsuarioCreacion = dto["UsuarioCreacion"].ToString();
                dto.UsuarioModificacion = dto["UsuarioModificacion"].ToString();



                if (dto == null)
                {
                    mensajeExcepcion = $"Intento de Insertar Nuevo Registro, dto null";
                    _logger.LogError(mensajeExcepcion);

                    throw new Exception(mensajeExcepcion);

                    //_logger.LogError("Intento de Insertar Nuevo Registro");
                    //return BadRequest("dto null");
                }
                if (!ValidadorDTO.Current.Validate(dto).IsValid)
                {
                    var mensaje = string.Empty;
                    foreach (var item in ValidadorDTO.Current.Validate(dto).Errors)
                    {
                        mensaje = mensaje + item.ErrorMessage + "/";

                    }
                    Debug.Write(mensaje);

                    //_logger.LogCritical($"Error en {ControllerContext.ActionDescriptor.ActionName} - Mensaje:{mensaje} - InnerException:Conflict");
                    //return StatusCode(409, mensaje);
                    mensajeExcepcion = $"Error en { ControllerContext.ActionDescriptor.ActionName}-Mensaje:{ mensaje}-InnerException:Conflict";
                    InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "InsertarDTO", Parametros = "parametros", Mensaje = mensajeExcepcion, Excepcion = mensajeExcepcion, Tipo = "INSERT", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });


                    return StatusCode(409, mensaje);

                    //throw new Exception(mensajeExcepcion);
                }

                var tv = new TV();


                if (!tv.Validate(dto as TEntity).IsValid)
                {
                    var mensaje = string.Empty;

                    foreach (var item in tv.Validate(dto as TEntity).Errors)
                    {
                        mensaje = mensaje + item.ErrorMessage + "/";

                    }
                    //_logger.LogCritical($"Error en {ControllerContext.ActionDescriptor.ActionName} - Mensaje:{mensaje} - InnerException:Conflict");
                    mensajeExcepcion = $"Error en { ControllerContext.ActionDescriptor.ActionName}-Mensaje:{ mensaje}-InnerException:Conflict";
                    InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "InsertarDTO", Parametros = "parametros", Mensaje = mensajeExcepcion, Excepcion = mensajeExcepcion, Tipo = "INSERT", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                    //throw new Exception(mensajeExcepcion);
                    return StatusCode(409, mensaje);
                }

                var entidad = new TEntity();

                var listaPropiedades = dto.Propiedades;
                foreach (var prop in listaPropiedades)
                {

                    Debug.WriteLine(prop);
                    if (prop.Contains("Maestro"))
                    {
                        //var a = 1;
                    }
                    if (dto[prop] != null)
                    {
                        if (VerificarSiEsLista(dto[prop].GetType()))
                        {

                        }
                        else
                        {
                            entidad[prop] = dto[prop];
                        }

                    }
                }

                entidad.FechaCreacion = DateTime.Now;


                if (!_repositorio.Insertar(entidad))
                {
                    var mensaje = "Hubo in problema mientras se procesaba la peticion";
                    mensajeExcepcion = $"Error en { ControllerContext.ActionDescriptor.ActionName}-Mensaje:{ mensaje }-InnerException:Conflict";
                    InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "InsertarDTO", Parametros = "parametros", Mensaje = mensajeExcepcion, Excepcion = mensajeExcepcion, Tipo = "INSERT", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                    return StatusCode(500, mensaje);

                    //throw new Exception(mensajeExcepcion);
                }

                mensajeExcepcion = $"Objeto {entidad.GetType().Name} - Insertado: {JsonConvert.SerializeObject(dto)}";
                _logger.LogInformation(mensajeExcepcion);
                InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "InsertarDTO", Parametros = "parametros", Mensaje = mensajeExcepcion, Excepcion = mensajeExcepcion, Tipo = "INSERT", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                return CreatedAtRoute(routeValues: new { id = entidad.Id }, value: entidad);


            }
            catch (Exception exception)
            {
                mensajeExcepcion = $"Error en {ControllerContext.ActionDescriptor.ActionName} - Mensaje:{exception.Message} - InnerException: {exception.InnerException.Message}";
                _logger.LogCritical(mensajeExcepcion);
                InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "InsertarDTO", Parametros = "parametros", Mensaje = mensajeExcepcion, Excepcion = mensajeExcepcion, Tipo = "INSERT", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });

                return StatusCode(500, "Hubo in problema mientras se procesaba la peticion");
            }
        }

        private bool VerificarSiEsLista(Type getType)
        {
            if (getType.IsConstructedGenericType)
            {
                return (getType.GetGenericTypeDefinition() == typeof(List<>) || getType.GetGenericTypeDefinition() == typeof(HashSet<>));
            }
            else if (typeof(BaseEntity).IsAssignableFrom(getType))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        [HttpPut("{id}")]
        public IActionResult ActualizarDTO(int id, [FromBody] Dictionary<string, string> dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogError("Intento de Actualizar Registro");
                    mensajeExcepcion = "dto null";
                    InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "ActualizarDTO", Parametros = "parametros", Mensaje = mensajeExcepcion, Excepcion = mensajeExcepcion, Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });

                    return BadRequest(mensajeExcepcion);
                }

                var entidad = _repositorio.ObtenerPorId(id);
                bool bandera = true;

                //Dictionary < string, string> listaPropiedades = JsonConvert.DeserializeObject<Dictionary<string, string>>(dto); 
                foreach (var prop in dto)
                {
                    if (bandera)
                    {
                        try
                        {
                            int entero = Convert.ToInt32(prop.Value);
                            entidad[prop.Key] = entero;
                            bandera = false;
                        }
                        catch (Exception err)
                        {
                            bandera = true;
                        }
                    }
                    if (bandera)
                    {
                        try
                        {
                            short smalint = Convert.ToInt16(prop.Value);
                            entidad[prop.Key] = smalint;
                            bandera = false;
                        }
                        catch (Exception err)
                        {
                            bandera = true;
                        }
                    }
                    if (bandera)
                    {
                        try
                        {
                            decimal decimales = Convert.ToDecimal(prop.Value);
                            entidad[prop.Key] = decimales;
                            bandera = false;
                        }
                        catch (Exception err)
                        {
                            bandera = true;
                        }
                    }
                    if (bandera)
                    {
                        try
                        {
                            double doubles = Convert.ToDouble(prop.Value);
                            entidad[prop.Key] = doubles;
                            bandera = false;
                        }
                        catch (Exception err)
                        {
                            bandera = true;
                        }
                    }
                    if (bandera)
                    {
                        try
                        {
                            DateTime tiempo = Convert.ToDateTime(prop.Value);
                            entidad[prop.Key] = tiempo;
                            bandera = false;
                        }
                        catch (Exception)
                        {
                            bandera = true;
                        }
                    }
                    if (bandera)
                    {
                        try
                        {
                            bool variable = Convert.ToBoolean(prop.Value);
                            entidad[prop.Key] = variable;
                            bandera = false;
                        }
                        catch (Exception)
                        {
                            bandera = true;
                        }
                    }

                    if (bandera)
                    {
                        entidad[prop.Key] = prop.Value;
                    }
                    bandera = true;
                }

                entidad.FechaCreacion = DateTime.Now;

                if (!_repositorio.Actualizar(entidad))
                {
                    mensajeExcepcion = "Hubo in problema mientras se procesaba la peticion";
                    InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "ActualizarDTO", Parametros = "parametros", Mensaje = this.mensajeExcepcion, Excepcion = this.mensajeExcepcion, Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                    return StatusCode(500, mensajeExcepcion);
                }

                mensajeExcepcion = "SUCCESS";
                _logger.LogInformation($"Objeto {entidad.GetType().Name} - Actualizado: {(JsonConvert.SerializeObject(dto))}");
                this.mensajeExcepcion = ($"Objeto {entidad.GetType().Name} - Actualizado: {(JsonConvert.SerializeObject(dto))}");
                InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "ActualizarDTO", Parametros = "parametros", Mensaje = this.mensajeExcepcion, Excepcion = this.mensajeExcepcion, Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });

                return CreatedAtRoute(routeValues: new { id = entidad.Id }, value: dto);

            }
            catch (Exception exception)
            {
                mensajeExcepcion = $"Error en {ControllerContext.ActionDescriptor.ActionName} - Mensaje:{exception.Message} - InnerException: {exception.InnerException.Message}";
                _logger.LogCritical(mensajeExcepcion);
                InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "....//", Parametros = "parametros", Mensaje = this.mensajeExcepcion, Excepcion = this.mensajeExcepcion, Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                return StatusCode(500, "Hubo in problema mientras se procesaba la peticion");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult GetDeletexID(int id)
        {
            var dto = new TEntity();
            var entidad = _repositorio.Eliminar(id, "test");
            if (entidad == false)
            {
                mensajeExcepcion = "Hubo in problema mientras se procesaba la peticion";
                InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "GetDeletexID", Parametros = "parametros", Mensaje = this.mensajeExcepcion, Excepcion = this.mensajeExcepcion, Tipo = "DELETE", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
                return StatusCode(500, mensajeExcepcion);
            }

            mensajeExcepcion = $"Objeto {entidad} Eliminado: {id} - {DateTime.Now}";
            _logger.LogInformation(mensajeExcepcion);

            InsertLog(new TLog { Ip = this.ip, Usuario = "test", Maquina = "192.160.0.22", Ruta = "GetDeletexID", Parametros = "parametros", Mensaje = this.mensajeExcepcion, Excepcion = this.mensajeExcepcion, Tipo = "DELETE", IdPadre = 0, UsuarioCreacion = "", UsuarioModificacion = "", FechaCreacion = System.DateTime.Now, FechaModificacion = System.DateTime.Now, Estado = true });
            return NoContent();
        }

    }

    /// Validadors
    public class ValidadorDTO : AbstractValidator<BaseEntity>
    {
        public static ValidadorDTO Current = new ValidadorDTO();
        public ValidadorDTO()
        {
            RuleFor(objeto => objeto.Estado).NotEmpty().WithMessage("Estado es Obligatorio");
            RuleFor(objeto => objeto.FechaCreacion).NotEmpty().WithMessage("Fecha Creación es Obligatorio");
            RuleFor(objeto => objeto.FechaModificacion).NotEmpty().WithMessage("Fecha Modificacion es Obligatorio");
            RuleFor(objeto => objeto.UsuarioCreacion).NotEmpty().WithMessage("Usuario Creacion es Obligatorio");
            RuleFor(objeto => objeto.UsuarioModificacion).NotEmpty().WithMessage(" Usuario Modificacion es Obligatorio");
        }
    }
}