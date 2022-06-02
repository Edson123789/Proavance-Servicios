using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Webinar")]
    public class WebinarController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly WebinarCategoriaConfirmacionAsistenciaRepositorio _repWebinarCategoriaConfirmacionAsistencia;
        private readonly WebinarRepositorio _repWebinar;
        private readonly ExpositorRepositorio _repExpositor;
        private readonly PersonalRepositorio _repPersonal;
        private readonly FrecuenciaRepositorio _repFrecuencia;

        private readonly WebinarCentroCostoRepositorio _repWebinarCentroCosto;
        private readonly WebinarDetalleRepositorio _repWebinarDetalle;
        private readonly MatriculaCabeceraRepositorio _repMatriculaCabecera;
        private readonly WebinarAsistenciaRepositorio _repWebinarAsistencia;
        private readonly CentroCostoRepositorio _repCentroCosto;
        private readonly PespecificoSesionRepositorio _repPespecificoSesion;
        private readonly ConfirmacionWebinarRepositorio _repConfirmacionWebinar;

        public WebinarController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repWebinarCategoriaConfirmacionAsistencia = new WebinarCategoriaConfirmacionAsistenciaRepositorio(_integraDBContext);
            _repWebinar = new WebinarRepositorio(_integraDBContext);
            _repExpositor = new ExpositorRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repFrecuencia = new FrecuenciaRepositorio(_integraDBContext);

            _repWebinarCentroCosto = new WebinarCentroCostoRepositorio(_integraDBContext);
            _repWebinarDetalle = new WebinarDetalleRepositorio(_integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            _repWebinarAsistencia = new WebinarAsistenciaRepositorio(_integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repConfirmacionWebinar = new ConfirmacionWebinarRepositorio(_integraDBContext);
        }

        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repWebinar.Exist(Id))
                {
                    return BadRequest("Webinar no existente");
                }
                return Ok(_repWebinarDetalle.Obtener(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalleCentroCosto(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repWebinar.Exist(Id))
                {
                    return BadRequest("Webinar no existente");
                }
                return Ok(_repWebinarCentroCosto.Obtener(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            try
            {
                var listaWebinarCategoriaConfirmacionAsistencia = _repWebinarCategoriaConfirmacionAsistencia.ObtenerTodoFiltro();
                var listaExpositor = _repExpositor.ObtenerTodoFiltro();
                var listaPersonal = _repPersonal.ObtenerTodoFiltro();
                var listaFrecuencia = _repFrecuencia.ObtenerTodoFiltro();
                var listaCentroCosto = _repCentroCosto.ObtenerTodoFiltro();

                var listaFiltros = new
                {
                    ListaWebinarCategoriaConfirmacionAsistencia = listaWebinarCategoriaConfirmacionAsistencia,
                    ListaExpositor = listaExpositor,
                    ListaPersonal = listaPersonal,
                    ListaFrecuencia = listaFrecuencia,
                    ListaCentroCosto = listaCentroCosto
                };
                return Ok(listaFiltros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repWebinarCategoriaConfirmacionAsistencia.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repWebinar.Obtener());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] WebinarDTO Webinar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var webinar = new WebinarBO()
                {
                    Nombre = Webinar.Nombre,
                    NombreCursoCompleto = Webinar.NombreCursoCompleto,
                    IdExpositor = Webinar.IdExpositor,
                    IdWebinarCategoriaConfirmacionAsistencia = Webinar.IdWebinarCategoriaConfirmacionAsistencia,
                    IdPersonal = Webinar.IdPersonal,
                    IdFrecuencia = Webinar.IdFrecuencia,
                    Usuario = Webinar.Usuario,
                    Clave = Webinar.Clave,
                    LinkAulaVirtual = Webinar.LinkAulaVirtual,
                    Activo = Webinar.Activo,
                    UsuarioCreacion = Webinar.NombreUsuario,
                    UsuarioModificacion = Webinar.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };
                if (!webinar.HasErrors)
                {
                    _repWebinar.Insert(webinar);
                }
                else
                {
                    return BadRequest(webinar.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] WebinarDTO Webinar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repWebinar.Exist(Webinar.Id))
                {
                    return BadRequest("Webinar no existente!");
                }
                var webinar = _repWebinar.FirstById(Webinar.Id);
                webinar.Nombre = Webinar.Nombre;
                webinar.NombreCursoCompleto = Webinar.NombreCursoCompleto;
                webinar.IdExpositor = Webinar.IdExpositor;
                webinar.IdWebinarCategoriaConfirmacionAsistencia = Webinar.IdWebinarCategoriaConfirmacionAsistencia;
                webinar.IdPersonal = Webinar.IdPersonal;
                webinar.IdFrecuencia = Webinar.IdFrecuencia;
                webinar.Usuario = Webinar.Usuario;
                webinar.Clave = Webinar.Clave;
                webinar.LinkAulaVirtual = Webinar.LinkAulaVirtual;
                webinar.Activo = Webinar.Activo;

                webinar.UsuarioModificacion = Webinar.NombreUsuario;
                webinar.FechaModificacion = DateTime.Now;
                if (!webinar.HasErrors)
                {
                    _repWebinar.Update(webinar);
                }
                else
                {
                    return BadRequest(webinar.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarCentroCostoAsociado([FromBody] WebinarDTO Webinar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repWebinar.Exist(Webinar.Id))
                {
                    return BadRequest("Webinar no existente!");
                }
                var webinar = _repWebinar.FirstById(Webinar.Id);

                //borramos existentes
                var listaBorrar = _repWebinarCentroCosto.GetBy(x => x.IdWebinar == Webinar.Id).ToList();
                listaBorrar.RemoveAll(x => Webinar.ListaWebinarCentroCosto.Any(y => y.IdCentroCosto == x.IdCentroCosto));
                _repWebinarCentroCosto.Delete(listaBorrar.Select(x => x.Id).ToList(), Webinar.NombreUsuario);

                WebinarCentroCostoBO webinarCentroCosto;
                foreach (var item in Webinar.ListaWebinarCentroCosto)
                {
                    if (_repWebinarCentroCosto.Exist(x => x.IdCentroCosto == item.IdCentroCosto && x.IdWebinar == Webinar.Id))
                    {
                        webinarCentroCosto = _repWebinarCentroCosto.FirstBy(x => x.IdCentroCosto == item.IdCentroCosto && x.IdWebinar == Webinar.Id);
                        webinarCentroCosto.Activo = item.Activo;
                        webinarCentroCosto.UsuarioModificacion = Webinar.NombreUsuario;
                        webinarCentroCosto.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        webinarCentroCosto = new WebinarCentroCostoBO()
                        {
                            IdCentroCosto = item.IdCentroCosto,
                            Activo = item.Activo,
                            Estado = true,
                            UsuarioCreacion = Webinar.NombreUsuario,
                            UsuarioModificacion = Webinar.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        };
                    }
                    webinar.ListaWebinarCentroCosto.Add(webinarCentroCosto);
                }

                if (!webinar.HasErrors)
                {
                    _repWebinar.Update(webinar);
                }
                else
                {
                    return BadRequest(webinar.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarDetalle([FromBody] WebinarDTO Webinar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repWebinar.Exist(Webinar.Id))
                {
                    return BadRequest("Webinar no existente!");
                }
                var webinar = _repWebinar.FirstById(Webinar.Id);

                //borramos existentes
                var idsDebenContinuar = Webinar.ListaWebinarDetalle.Where(x => x.Id != 0).Select(x => x.Id).ToList();
                var idsExisten = _repWebinarDetalle.GetBy(x => x.IdWebinar == Webinar.Id).Select(x => x.Id).ToList();
                var idsEliminar = idsExisten.Where(x => !idsDebenContinuar.Contains(x));

                //eliminamos los que deben ser eliminados
                _repWebinarDetalle.Delete(idsEliminar, Webinar.NombreUsuario);

                WebinarDetalleBO webinarDetalle;
                foreach (var item in Webinar.ListaWebinarDetalle)
                {
                    if (item.Id == 0)// no existe
                    {
                        webinarDetalle = new WebinarDetalleBO()
                        {
                            FechaInicio = item.FechaInicio,
                            FechaFin = item.FechaFin,
                            Grupo = item.Grupo,
                            Link = item.Link,
                            EsCancelado = item.EsCancelado,
                            UsuarioCreacion = Webinar.NombreUsuario,
                            UsuarioModificacion = Webinar.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                    }
                    else
                    {
                        if (!_repWebinarDetalle.Exist(item.Id))
                        {
                            return BadRequest("Detalle no existe");
                        }

                        webinarDetalle = _repWebinarDetalle.FirstById(item.Id);
                        webinarDetalle.FechaInicio = item.FechaInicio;
                        webinarDetalle.FechaFin = item.FechaFin;
                        webinarDetalle.Grupo = item.Grupo;
                        webinarDetalle.Link = item.Link;
                        webinarDetalle.EsCancelado = item.EsCancelado;
                        webinarDetalle.UsuarioModificacion = Webinar.NombreUsuario;
                        webinarDetalle.FechaModificacion = DateTime.Now;
                    }
                    webinar.ListaWebinarDetalle.Add(webinarDetalle);
                }

                if (!webinar.HasErrors)
                {
                    _repWebinar.Update(webinar);
                }
                else
                {
                    return BadRequest(webinar.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Webinar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repWebinar.Exist(Webinar.Id))
                {
                    return BadRequest("Webinar no existente");
                }
                _repWebinar.Delete(Webinar.Id, Webinar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdWebinarDetalle}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ConfirmarAsistencia(int IdWebinarDetalle, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = new ApiRespuestaDTO();
                if (!_repMatriculaCabecera.Exist(IdMatriculaCabecera))
                {
                    respuesta.EsCorrecto = false;
                    respuesta.Mensaje = "El código de matricula no es válido.";
                    return Ok(respuesta);
                }
                if (!_repWebinarDetalle.Exist(IdWebinarDetalle))
                {
                    respuesta.EsCorrecto = false;
                    respuesta.Mensaje = "El webinar no es válido.";
                    return Ok(respuesta);
                }
                
                if (_repWebinarDetalle.EsWebinarPasado(IdWebinarDetalle))
                {
                    respuesta.EsCorrecto = false;
                    respuesta.Mensaje = "El webinar ya finalizó.";
                    return Ok(respuesta);
                }

                try
                {
                    WebinarAsistenciaBO webinarAsistencia;
                    if (_repWebinarAsistencia.Exist(x => x.IdWebinarDetalle == IdWebinarDetalle && x.IdMatriculaCabecera == IdMatriculaCabecera))
                    {
                        webinarAsistencia = _repWebinarAsistencia.FirstBy(x => x.IdWebinarDetalle == IdWebinarDetalle && x.IdMatriculaCabecera == IdMatriculaCabecera);
                        webinarAsistencia.ConfirmoAsistencia = true;
                        webinarAsistencia.UsuarioModificacion = "system";
                        webinarAsistencia.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        webinarAsistencia = new WebinarAsistenciaBO()
                        {
                            IdMatriculaCabecera = IdMatriculaCabecera,
                            IdWebinarDetalle = IdWebinarDetalle,
                            Asistio = false,
                            ConfirmoAsistencia = true,
                            Estado = true,
                            UsuarioCreacion = "system",
                            UsuarioModificacion = "system",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    if (!webinarAsistencia.HasErrors)
                    {
                        if (webinarAsistencia.Id != 0)
                        {
                            _repWebinarAsistencia.Update(webinarAsistencia);
                        }
                        else
                        {
                            _repWebinarAsistencia.Insert(webinarAsistencia);
                        }
                        respuesta.EsCorrecto = true;
                        respuesta.Mensaje = "Agradecemos su participación.";
                    }
                    else
                    {
                        respuesta.EsCorrecto = false;
                        respuesta.Mensaje = "Ocurrió un inconveniente, por favor intente intente nuevamente.";
                    }
                }
                catch (Exception e)
                {
                    respuesta.EsCorrecto = false;
                    respuesta.Mensaje = "Ocurrió un inconveniente, por favor intente intente nuevamente.";
                }
            
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdWebinarDetalle}")]
        [HttpGet]
        public ActionResult ObtenerWebinarAsistencia(int IdWebinarDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repWebinarDetalle.Exist(IdWebinarDetalle))
                {
                    return BadRequest("Webinar detalle no existente");
                }
                return Ok(_repWebinarAsistencia.Obtener(IdWebinarDetalle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarDetalleAsistencia([FromBody] WebinarDetalleCompuestoDTO WebinarDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repWebinarDetalle.Exist(WebinarDetalle.Id))
                {
                    return BadRequest("Webinar detalle no existente!");
                }
                var webinarDetalle = _repWebinarDetalle.FirstById(WebinarDetalle.Id);

                //borramos existentes
                WebinarAsistenciaBO webinarAsistencia;
                foreach (var item in WebinarDetalle.ListaWebinarAsistencia)
                {
                    if (item.Id == 0)// no existe
                    {
                        webinarAsistencia = new WebinarAsistenciaBO()
                        {
                            IdMatriculaCabecera = item.IdMatriculaCabecera,
                            Asistio = item.Asistio,
                            ConfirmoAsistencia = item.ConfirmoAsistencia,
                            UsuarioCreacion = WebinarDetalle.NombreUsuario,
                            UsuarioModificacion = WebinarDetalle.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                    }
                    else
                    {
                        if (!_repWebinarAsistencia.Exist(item.Id))
                        {
                            return BadRequest("Asistencia no existe");
                        }

                        webinarAsistencia = _repWebinarAsistencia.FirstById(item.Id);
                        webinarAsistencia.Asistio = item.Asistio;
                        webinarAsistencia.UsuarioModificacion = WebinarDetalle.NombreUsuario;
                        webinarAsistencia.FechaModificacion = DateTime.Now;
                    }
                    webinarDetalle.ListaWebinarAsistencia.Add(webinarAsistencia);
                }

                if (!webinarDetalle.HasErrors)
                {
                    _repWebinarDetalle.Update(webinarDetalle);
                }
                else
                {
                    return BadRequest(webinarDetalle.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPEspecificoSesion}/{IdMatriculaCabecera}/{EstadoConfirmacion}")]
        [HttpGet]
        public ActionResult ConfirmarParticipacion(int IdPEspecificoSesion, int IdMatriculaCabecera, bool EstadoConfirmacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var respuesta = new ApiRespuestaDTO();
                if (!_repMatriculaCabecera.Exist(IdMatriculaCabecera))
                {
                    respuesta.EsCorrecto = false;
                    respuesta.Mensaje = "El código de matrícula no es válido.";
                    return Ok(respuesta);
                }

                if (!_repPespecificoSesion.Exist(IdPEspecificoSesion))
                {
                    respuesta.EsCorrecto = false;
                    respuesta.Mensaje = "El webinar no es válido.";
                    return Ok(respuesta);
                }

                if (_repPespecificoSesion.EsWebinarPasado(IdPEspecificoSesion))
                {
                    respuesta.EsCorrecto = false;
                    respuesta.Mensaje = "El webinar ya finalizó.";
                    return Ok(respuesta);
                }

                try
                {
                    ConfirmacionWebinarBO webinarConfirmacion;
                    if (_repConfirmacionWebinar.Exist(x =>
                        x.IdPespecificoSesion == IdPEspecificoSesion && x.IdMatriculaCabecera == IdMatriculaCabecera))
                    {
                        webinarConfirmacion = _repConfirmacionWebinar.FirstBy(x =>
                            x.IdPespecificoSesion == IdPEspecificoSesion &&
                            x.IdMatriculaCabecera == IdMatriculaCabecera);
                        webinarConfirmacion.Confirmo = EstadoConfirmacion;
                        webinarConfirmacion.Asistio = false;
                        webinarConfirmacion.UsuarioModificacion = "system";
                        webinarConfirmacion.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        webinarConfirmacion = new ConfirmacionWebinarBO()
                        {
                            IdMatriculaCabecera = IdMatriculaCabecera,
                            IdPespecificoSesion = IdPEspecificoSesion,
                            Confirmo = EstadoConfirmacion,
                            Asistio = false,
                            Estado = true,
                            UsuarioCreacion = "system",
                            UsuarioModificacion = "system",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }

                    if (!webinarConfirmacion.HasErrors)
                    {
                        if (webinarConfirmacion.Id != 0)
                        {
                            _repConfirmacionWebinar.Update(webinarConfirmacion);
                        }
                        else
                        {
                            _repConfirmacionWebinar.Insert(webinarConfirmacion);
                        }

                        respuesta.EsCorrecto = true;
                        respuesta.Mensaje = "Agradecemos su participación.";
                    }
                    else
                    {
                        respuesta.EsCorrecto = false;
                        respuesta.Mensaje = "Ocurrió un inconveniente, por favor intente intente nuevamente.";
                    }
                }
                catch (Exception e)
                {
                    respuesta.EsCorrecto = false;
                    respuesta.Mensaje = "Ocurrió un inconveniente, por favor intente intente nuevamente.";
                }

                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
