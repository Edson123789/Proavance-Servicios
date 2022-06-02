using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/Operaciones/Flujo")]
    [ApiController]
    public class FlujoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly FlujoRepositorio _repFlujo;
        private readonly FlujoFaseRepositorio _repFlujoFase;
        private readonly FlujoActividadRepositorio _repFlujoActividad;
        private readonly FlujoOcurrenciaRepositorio _repFlujoOcurrencia;
        private readonly FlujoPorPespecificoRepositorio _repFlujoPorPespecifico;

        public FlujoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repFlujo = new FlujoRepositorio(_integraDBContext);
            _repFlujoFase = new FlujoFaseRepositorio(_integraDBContext);
            _repFlujoActividad = new FlujoActividadRepositorio(_integraDBContext);
            _repFlujoOcurrencia = new FlujoOcurrenciaRepositorio(_integraDBContext);
            _repFlujoPorPespecifico = new FlujoPorPespecificoRepositorio(_integraDBContext);
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
                return Ok(_repFlujo.ObtenerTodo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] FlujoDTO flujo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var flujoNuevo = new FlujoBO()
                {
                    Nombre = flujo.Nombre,
                    IdModalidadCurso = flujo.IdModalidadCurso,
                    IdClasificacionUbicacionDocente = flujo.IdClasificacionUbicacionDocente,

                    Estado = true,
                    UsuarioCreacion = flujo.NombreUsuario,
                    UsuarioModificacion = flujo.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                if (!flujoNuevo.HasErrors)
                {
                    _repFlujo.Insert(flujoNuevo);
                }
                else
                {
                    return BadRequest(flujoNuevo.GetErrors());
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
        public ActionResult Actualizar([FromBody] FlujoDTO flujo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFlujo.Exist(flujo.Id))
                {
                    return BadRequest("Flujo no existente!");
                }
                var tipoServicio = _repFlujo.FirstById(flujo.Id);
                tipoServicio.Nombre = flujo.Nombre;
                tipoServicio.IdModalidadCurso = flujo.IdModalidadCurso;
                tipoServicio.IdClasificacionUbicacionDocente = flujo.IdClasificacionUbicacionDocente;
                
                tipoServicio.UsuarioModificacion = flujo.NombreUsuario;
                tipoServicio.FechaModificacion = DateTime.Now;
                if (!tipoServicio.HasErrors)
                {
                    _repFlujo.Update(tipoServicio);
                }
                else
                {
                    return BadRequest(tipoServicio.GetErrors());
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
        public ActionResult Eliminar([FromBody] EliminarDTO flujo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFlujo.Exist(flujo.Id))
                {
                    return BadRequest("Flujo no existente");
                }
                _repFlujo.Delete(flujo.Id, flujo.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #region Gestion Fase
        [Route("[Action]/{idFlujo}")]
        [HttpGet]
        public ActionResult FaseObtener(int idFlujo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repFlujoFase.GetBy(x => x.IdFlujo == idFlujo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult FaseInsertar([FromBody] FlujoFaseDTO fase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var faseNuevo = new FlujoFaseBO()
                {
                    IdFlujo = fase.IdFlujo,
                    Nombre = fase.Nombre,
                    Orden = fase.Orden,

                    Estado = true,
                    UsuarioCreacion = fase.NombreUsuario,
                    UsuarioModificacion = fase.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                if (!faseNuevo.HasErrors)
                {
                    _repFlujoFase.Insert(faseNuevo);
                }
                else
                {
                    return BadRequest(faseNuevo.GetErrors());
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
        public ActionResult FaseActualizar([FromBody] FlujoFaseDTO fase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFlujoFase.Exist(fase.Id))
                {
                    return BadRequest("Fase no existente!");
                }
                var tipoServicio = _repFlujoFase.FirstById(fase.Id);
                tipoServicio.Nombre = fase.Nombre;
                tipoServicio.Orden = fase.Orden;

                tipoServicio.UsuarioModificacion = fase.NombreUsuario;
                tipoServicio.FechaModificacion = DateTime.Now;
                if (!tipoServicio.HasErrors)
                {
                    _repFlujoFase.Update(tipoServicio);
                }
                else
                {
                    return BadRequest(tipoServicio.GetErrors());
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
        public ActionResult FaseEliminar([FromBody] EliminarDTO fase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFlujoFase.Exist(fase.Id))
                {
                    return BadRequest("Fase no existente");
                }
                _repFlujo.Delete(fase.Id, fase.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion


        #region Gestion Actividad
        [Route("[Action]/{idFlujoFase}")]
        [HttpGet]
        public ActionResult ActividadObtener(int idFlujoFase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repFlujoActividad.GetBy(x => x.IdFlujoFase == idFlujoFase));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActividadInsertar([FromBody] FlujoActividadDTO actividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var faseNuevo = new FlujoActividadBO()
                {
                    IdFlujoFase = actividad.IdFlujoFase,
                    Nombre = actividad.Nombre,
                    Orden = actividad.Orden,

                    Estado = true,
                    UsuarioCreacion = actividad.NombreUsuario,
                    UsuarioModificacion = actividad.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                if (!faseNuevo.HasErrors)
                {
                    _repFlujoActividad.Insert(faseNuevo);
                }
                else
                {
                    return BadRequest(faseNuevo.GetErrors());
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
        public ActionResult ActividadActualizar([FromBody] FlujoActividadDTO actividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFlujoActividad.Exist(actividad.Id))
                {
                    return BadRequest("Actividad no existente!");
                }
                var tipoServicio = _repFlujoActividad.FirstById(actividad.Id);
                tipoServicio.Nombre = actividad.Nombre;
                tipoServicio.Orden = actividad.Orden;

                tipoServicio.UsuarioModificacion = actividad.NombreUsuario;
                tipoServicio.FechaModificacion = DateTime.Now;
                if (!tipoServicio.HasErrors)
                {
                    _repFlujoActividad.Update(tipoServicio);
                }
                else
                {
                    return BadRequest(tipoServicio.GetErrors());
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
        public ActionResult ActividadEliminar([FromBody] EliminarDTO fase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFlujoActividad.Exist(fase.Id))
                {
                    return BadRequest("Actividad no existente");
                }
                _repFlujo.Delete(fase.Id, fase.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Gestion Ocurrencia
        [Route("[Action]/{idFlujoActividad}")]
        [HttpGet]
        public ActionResult OcurrenciaObtener(int idFlujoActividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repFlujoOcurrencia.ListadoPorIdFlujoActividad(idFlujoActividad));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult OcurrenciaInsertar([FromBody] FlujoOcurrenciaDTO ocurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var faseNuevo = new FlujoOcurrenciaBO()
                {
                    IdFlujoActividad = ocurrencia.IdFlujoActividad,
                    Nombre = ocurrencia.Nombre,
                    Orden = ocurrencia.Orden,
                    CerrarSeguimiento = ocurrencia.CerrarSeguimiento,
                    IdFaseDestino = ocurrencia.IdFaseDestino,
                    IdFlujoActividadSiguiente = ocurrencia.IdFlujoActividadSiguiente,

                    Estado = true,
                    UsuarioCreacion = ocurrencia.NombreUsuario,
                    UsuarioModificacion = ocurrencia.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                if (!faseNuevo.HasErrors)
                {
                    _repFlujoOcurrencia.Insert(faseNuevo);
                }
                else
                {
                    return BadRequest(faseNuevo.GetErrors());
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
        public ActionResult OcurrenciaActualizar([FromBody] FlujoOcurrenciaDTO ocurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFlujoOcurrencia.Exist(ocurrencia.Id))
                {
                    return BadRequest("Ocurrencia no existente!");
                }
                var tipoServicio = _repFlujoOcurrencia.FirstById(ocurrencia.Id);
                tipoServicio.Nombre = ocurrencia.Nombre;
                tipoServicio.Orden = ocurrencia.Orden;
                tipoServicio.CerrarSeguimiento = ocurrencia.CerrarSeguimiento;
                tipoServicio.IdFaseDestino = ocurrencia.IdFaseDestino;
                tipoServicio.IdFlujoActividadSiguiente = ocurrencia.IdFlujoActividadSiguiente;

                tipoServicio.UsuarioModificacion = ocurrencia.NombreUsuario;
                tipoServicio.FechaModificacion = DateTime.Now;
                if (!tipoServicio.HasErrors)
                {
                    _repFlujoOcurrencia.Update(tipoServicio);
                }
                else
                {
                    return BadRequest(tipoServicio.GetErrors());
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
        public ActionResult OcurrenciaEliminar([FromBody] EliminarDTO fase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFlujoOcurrencia.Exist(fase.Id))
                {
                    return BadRequest("Ocurrencia no existente");
                }
                _repFlujo.Delete(fase.Id, fase.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        [Route("[Action]/{idClasificacionPersona}/{idPespecifico}")]
        [HttpGet]
        public ActionResult ObtenerDetalleAgenda(int idClasificacionPersona, int idPespecifico)
        {
            try
            {
                FlujoBO bo = new FlujoBO(_integraDBContext);
                int idFlujo = bo.CalcularIdFlujo(idClasificacionPersona, idPespecifico);

                List<FlujoDetalleAgendaDTO> listado = _repFlujo.ObtenerDetalleAgenda(idFlujo, idClasificacionPersona, idPespecifico);

                if (listado == null || listado.Count == 0)
                {
                    listado = _repFlujo.ObtenerPrimeraActividad(idFlujo);
                    listado.ForEach(f =>
                    {
                        f.Ocurrencia = null;
                        f.IdFlujoOcurrencia = null;
                        f.IdPespecifico = idPespecifico;
                        f.IdClasificacionPersona = idClasificacionPersona;
                    });
                }

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistrarProgramacion([FromBody] FlujoPorPespecificoRegistrarDTO detalle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime fechaSeguimiento = detalle.FechaSeguimiento
                        .AddHours(detalle.HoraSeguimiento.Hours)
                        .AddMinutes(detalle.HoraSeguimiento.Minutes);

                    var ocurrencia = _repFlujoOcurrencia.FirstById(detalle.IdFlujoOcurrencia.Value);
                    if (ocurrencia == null)
                        return BadRequest("La ocurrencia indicada no existe");
                    FlujoPorPespecificoBO bo;

                    if (detalle.IdFlujoPorPEspecifico != null && detalle.IdFlujoPorPEspecifico != 0)
                    {
                        bo = _repFlujoPorPespecifico.FirstById(detalle.IdFlujoPorPEspecifico.Value);

                        bo.IdFlujoOcurrencia = detalle.IdFlujoOcurrencia;
                        bo.FechaEjecucion = DateTime.Now;

                        bo.UsuarioModificacion = detalle.NombreUsuario;
                        bo.FechaModificacion = DateTime.Now;

                        _repFlujoPorPespecifico.Update(bo);
                    }
                    else
                    {
                        //primera actividad
                        bo = new FlujoPorPespecificoBO()
                        {
                            IdPespecifico = detalle.IdPespecifico,
                            IdFlujoActividad = detalle.IdFlujoActividad,
                            IdFlujoOcurrencia = detalle.IdFlujoOcurrencia,
                            IdClasificacionPersona = detalle.IdClasificacionPersona,
                            FechaEjecucion = DateTime.Now,
                            //FechaSeguimiento = detalle.FechaSeguimiento.AddHours(detalle.HoraSeguimiento.Hours).AddHours(detalle.HoraSeguimiento.Minutes),

                            Estado = true,
                            UsuarioCreacion = detalle.NombreUsuario,
                            UsuarioModificacion = detalle.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        _repFlujoPorPespecifico.Insert(bo);
                    }

                    //aplica si tiene siguiente aplicación
                    if (ocurrencia.IdFlujoActividadSiguiente != null || ocurrencia.CerrarSeguimiento)
                    {
                        if (ocurrencia.CerrarSeguimiento == false)
                        {
                            FlujoPorPespecificoBO boOcurrenciaAutomatica = new FlujoPorPespecificoBO()
                            {
                                IdPespecifico = bo.IdPespecifico,
                                IdFlujoActividad = ocurrencia.IdFlujoActividadSiguiente.Value,
                                IdClasificacionPersona = detalle.IdClasificacionPersona,
                                FechaSeguimiento = fechaSeguimiento,

                                Estado = true,
                                UsuarioCreacion = detalle.NombreUsuario,
                                UsuarioModificacion = detalle.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            
                            _repFlujoPorPespecifico.Insert(boOcurrenciaAutomatica);
                        }
                    }
                    else
                    {
                        //inserta la misma actividad para su seguimiento si no tiene ninguna programada como siguiente
                        FlujoPorPespecificoBO boOcurrenciaAutomatica = new FlujoPorPespecificoBO()
                        {
                            IdPespecifico = bo.IdPespecifico,
                            IdFlujoActividad = bo.IdFlujoActividad,
                            IdClasificacionPersona = bo.IdClasificacionPersona,
                            FechaSeguimiento = fechaSeguimiento,

                            Estado = true,
                            UsuarioCreacion = detalle.NombreUsuario,
                            UsuarioModificacion = detalle.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        _repFlujoPorPespecifico.Insert(boOcurrenciaAutomatica);
                    }

                    return Ok(bo);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                EstadoPespecificoRepositorio _repoEstado = new EstadoPespecificoRepositorio();
                var listadoEstado = _repoEstado.GetAll();

                PespecificoRepositorio _repoPEspecifico = new PespecificoRepositorio();
                var listaProgramasPadre = _repoPEspecifico.ObtenerProgramasEspecificosPadresV2(null);
                var listaProgramasHijos = _repoPEspecifico.ObtenerHijos();

                return Ok(new
                {
                    ListadoEstado = listadoEstado, ListadoProgramasPadre = listaProgramasPadre,
                    ListadoProgramasHijos = listaProgramasHijos
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerClasificacionUbicacionDocente()
        {
            try
            {
                var listado = new[]
                {
                    new {Id = Enums.CalsificacionUbicacionDocente.DocenteLocal, Nombre = "Docente Local"},
                    new {Id = Enums.CalsificacionUbicacionDocente.DocenteNacional, Nombre = "Docente Nacional"},
                    new {Id = Enums.CalsificacionUbicacionDocente.DocenteExtranjero, Nombre = "Docente Extranjero"}
                }.ToList();

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idClasificacionPersona}")]
        [HttpGet]
        public ActionResult ObtenerFlujoDetalleProgramadoPendiente(int idClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<FlujoDetalleProgramadoDTO> listado = _repFlujo.ObtenerFlujoDetalleProgramadoPendiente(idClasificacionPersona);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
