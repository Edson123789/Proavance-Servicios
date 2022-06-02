using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConfigurarEvaluacionTrabajo")]
    public class ConfigurarEvaluacionTrabajoController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ConfigurarEvaluacionTrabajoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarConfigurarEvaluacionTrabajo([FromBody] configurarEvaluacionDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfigurarEvaluacionTrabajoRepositorio _repConfigurarEvaluacionTrabajo = new ConfigurarEvaluacionTrabajoRepositorio(_integraDBContext);
                PreguntaEvaluacionTrabajoRepositorio _repPreguntaEvaluacionTrabajo = new PreguntaEvaluacionTrabajoRepositorio(_integraDBContext);

                if (ObjetoDTO.ConfigurarEvaluacion.Id == 0)
                {
                    ConfigurarEvaluacionTrabajoBO NuevaConfigurarEvaluacionTrabajo = new ConfigurarEvaluacionTrabajoBO
                    {
                        IdTipoEvaluacionTrabajo = ObjetoDTO.ConfigurarEvaluacion.IdTipoEvaluacionTrabajo,
                        Nombre = ObjetoDTO.ConfigurarEvaluacion.Nombre,
                        Descripcion = ObjetoDTO.ConfigurarEvaluacion.Descripcion,
                        IdDocumentoPw = ObjetoDTO.ConfigurarEvaluacion.IdDocumentoPw,
                        ArchivoNombre = ObjetoDTO.ConfigurarEvaluacion.ArchivoNombre,
                        ArchivoCarpeta = ObjetoDTO.ConfigurarEvaluacion.ArchivoCarpeta,
                        IdPgeneral = ObjetoDTO.ConfigurarEvaluacion.IdPgeneral,
                        IdSeccion = ObjetoDTO.ConfigurarEvaluacion.IdSeccion,
                        Fila = ObjetoDTO.ConfigurarEvaluacion.Fila,
                        DescripcionPregunta = ObjetoDTO.ConfigurarEvaluacion.DescripcionPregunta,

                        OrdenCapitulo=ObjetoDTO.ConfigurarEvaluacion.OrdenCapitulo,
                        HabilitarInstrucciones = ObjetoDTO.ConfigurarEvaluacion.HabilitarInstrucciones,
                        HabilitarArchivo = ObjetoDTO.ConfigurarEvaluacion.HabilitarArchivo,
                        HabilitarPreguntas = ObjetoDTO.ConfigurarEvaluacion.HabilitarPreguntas,

                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _repConfigurarEvaluacionTrabajo.Insert(NuevaConfigurarEvaluacionTrabajo);

                    List<PreguntaEvaluacionTrabajoBO> _listaRegistros = new List<PreguntaEvaluacionTrabajoBO>();
                    foreach (var item in ObjetoDTO.listaPreguntas)
                    {
                        PreguntaEvaluacionTrabajoBO NuevaPregunta = new PreguntaEvaluacionTrabajoBO
                        {
                            IdConfigurarEvaluacionTrabajo = NuevaConfigurarEvaluacionTrabajo.Id,
                            IdPregunta = item.Id,
                            Estado = true,
                            UsuarioCreacion = ObjetoDTO.Usuario,
                            UsuarioModificacion = ObjetoDTO.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _listaRegistros.Add(NuevaPregunta);
                    }
                    _repPreguntaEvaluacionTrabajo.Insert(_listaRegistros);

                    return Ok(NuevaConfigurarEvaluacionTrabajo);
                }
                else
                {
                    ConfigurarEvaluacionTrabajoBO _ConfigurarEvaluacionTrabajo = _repConfigurarEvaluacionTrabajo.GetBy(x => x.Id == ObjetoDTO.ConfigurarEvaluacion.Id).FirstOrDefault();
                    _ConfigurarEvaluacionTrabajo.IdTipoEvaluacionTrabajo = ObjetoDTO.ConfigurarEvaluacion.IdTipoEvaluacionTrabajo;
                    _ConfigurarEvaluacionTrabajo.Nombre = ObjetoDTO.ConfigurarEvaluacion.Nombre;
                    _ConfigurarEvaluacionTrabajo.Descripcion = ObjetoDTO.ConfigurarEvaluacion.Descripcion;
                    if (ObjetoDTO.ConfigurarEvaluacion.HabilitarInstrucciones)
                    {
                        _ConfigurarEvaluacionTrabajo.IdDocumentoPw = ObjetoDTO.ConfigurarEvaluacion.IdDocumentoPw;
                    }
                    else
                    {
                        _ConfigurarEvaluacionTrabajo.IdDocumentoPw = null;

                    }
                    
                    _ConfigurarEvaluacionTrabajo.ArchivoNombre = ObjetoDTO.ConfigurarEvaluacion.ArchivoNombre;
                    _ConfigurarEvaluacionTrabajo.ArchivoCarpeta = ObjetoDTO.ConfigurarEvaluacion.ArchivoCarpeta;
                    _ConfigurarEvaluacionTrabajo.DescripcionPregunta = ObjetoDTO.ConfigurarEvaluacion.DescripcionPregunta;

                    _ConfigurarEvaluacionTrabajo.OrdenCapitulo = ObjetoDTO.ConfigurarEvaluacion.OrdenCapitulo;
                    _ConfigurarEvaluacionTrabajo.HabilitarInstrucciones = ObjetoDTO.ConfigurarEvaluacion.HabilitarInstrucciones;
                    _ConfigurarEvaluacionTrabajo.HabilitarArchivo = ObjetoDTO.ConfigurarEvaluacion.HabilitarArchivo;
                    _ConfigurarEvaluacionTrabajo.HabilitarPreguntas = ObjetoDTO.ConfigurarEvaluacion.HabilitarPreguntas;

                    _ConfigurarEvaluacionTrabajo.Estado = true;
                    _ConfigurarEvaluacionTrabajo.FechaModificacion = DateTime.Now;
                    _ConfigurarEvaluacionTrabajo.UsuarioModificacion = ObjetoDTO.Usuario;
                    _repConfigurarEvaluacionTrabajo.Update(_ConfigurarEvaluacionTrabajo);

                    

                    if (_ConfigurarEvaluacionTrabajo.HabilitarPreguntas)
                    {
                        if (ObjetoDTO.listaPreguntas != null && ObjetoDTO.listaPreguntas.Count > 0)
                        {
                            var _listaRegistro = _repPreguntaEvaluacionTrabajo.GetBy(x => x.IdConfigurarEvaluacionTrabajo == _ConfigurarEvaluacionTrabajo.Id).Select(x => x.Id);
                            //var _listaEliminar = ObjetoDTO.listaPreguntas.Select(x => x.Id);
                            _repPreguntaEvaluacionTrabajo.Delete(_listaRegistro, ObjetoDTO.Usuario);
                        }

                        List<PreguntaEvaluacionTrabajoBO> _listaRegistros = new List<PreguntaEvaluacionTrabajoBO>();
                        foreach (var item in ObjetoDTO.listaPreguntas)
                        {
                            PreguntaEvaluacionTrabajoBO NuevaPregunta = new PreguntaEvaluacionTrabajoBO
                            {
                                IdConfigurarEvaluacionTrabajo = _ConfigurarEvaluacionTrabajo.Id,
                                IdPregunta = item.Id,
                                Estado = true,
                                UsuarioCreacion = ObjetoDTO.Usuario,
                                UsuarioModificacion = ObjetoDTO.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _listaRegistros.Add(NuevaPregunta);
                        }
                        _repPreguntaEvaluacionTrabajo.Insert(_listaRegistros);
                    }
                    else
                    {
                        if (ObjetoDTO.listaPreguntas != null && ObjetoDTO.listaPreguntas.Count > 0)
                        {
                            var _listaRegistro = _repPreguntaEvaluacionTrabajo.GetBy(x => x.IdConfigurarEvaluacionTrabajo == _ConfigurarEvaluacionTrabajo.Id).Select(x => x.Id);
                            //var _listaEliminar = ObjetoDTO.listaPreguntas.Select(x => x.Id);
                            _repPreguntaEvaluacionTrabajo.Delete(_listaRegistro, ObjetoDTO.Usuario);
                        }
                    }

                    

                    return Ok(_ConfigurarEvaluacionTrabajo);
                }

                

                //List<PreguntaEvaluacionTrabajoBO> _listaRegistros = new List<PreguntaEvaluacionTrabajoBO>();
                //foreach (var item in ObjetoDTO.listaPreguntas)
                //{
                //    PreguntaEvaluacionTrabajoBO NuevaPregunta = new PreguntaEvaluacionTrabajoBO
                //    {
                //        IdConfigurarEvaluacionTrabajo = item.IdConfigurarEvaluacionTrabajo,
                //        IdPregunta = item.IdPregunta,
                //        Estado = true,
                //        UsuarioCreacion = ObjetoDTO.Usuario,
                //        UsuarioModificacion = ObjetoDTO.Usuario,
                //        FechaCreacion = DateTime.Now,
                //        FechaModificacion = DateTime.Now
                //    };
                //    _listaRegistros.Add(NuevaPregunta);
                //}

                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarConfigurarEvaluacionTrabajo([FromBody] registroConfigurarEvaluacionTrabajoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfigurarEvaluacionTrabajoRepositorio _repConfigurarEvaluacionTrabajo = new ConfigurarEvaluacionTrabajoRepositorio(_integraDBContext);
                ConfigurarEvaluacionTrabajoBO _ConfigurarEvaluacionTrabajo = _repConfigurarEvaluacionTrabajo.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                _ConfigurarEvaluacionTrabajo.IdTipoEvaluacionTrabajo = ObjetoDTO.IdTipoEvaluacionTrabajo;
                _ConfigurarEvaluacionTrabajo.Nombre = ObjetoDTO.Nombre;
                _ConfigurarEvaluacionTrabajo.Descripcion = ObjetoDTO.Descripcion;
                _ConfigurarEvaluacionTrabajo.IdDocumentoPw = ObjetoDTO.IdDocumentoPw;
                _ConfigurarEvaluacionTrabajo.ArchivoNombre = ObjetoDTO.ArchivoNombre;
                _ConfigurarEvaluacionTrabajo.ArchivoCarpeta = ObjetoDTO.ArchivoCarpeta;
                _ConfigurarEvaluacionTrabajo.Estado = true;
                _ConfigurarEvaluacionTrabajo.FechaModificacion = DateTime.Now;
                _repConfigurarEvaluacionTrabajo.Update(_ConfigurarEvaluacionTrabajo);
                return Ok(_ConfigurarEvaluacionTrabajo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdRegistro}/{NombreUsuario}")]
        [HttpPost]
        public ActionResult EliminarConfigurarEvaluacionTrabajo(int IdRegistro, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfigurarEvaluacionTrabajoRepositorio _repConfigurarEvaluacion = new ConfigurarEvaluacionTrabajoRepositorio(_integraDBContext);
                var AreaTrabajo = _repConfigurarEvaluacion.GetBy(x => x.Id == IdRegistro).FirstOrDefault();
                _repConfigurarEvaluacion.Delete(IdRegistro, NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPGeneral}/{IdSeccion}/{Fila}")]
        [HttpGet]
        public ActionResult ObtenerConfigurarEvaluacionTrabajoPorConfiguracion(int IdPGeneral, int IdSeccion, int Fila)
        {
            try
            {
                ConfigurarEvaluacionTrabajoRepositorio _configurarEvaluacionTrabajoRepositorio = new ConfigurarEvaluacionTrabajoRepositorio();

                var respuesta = _configurarEvaluacionTrabajoRepositorio.ObtenerConfigurarEvaluacionTrabajoFiltro(IdPGeneral, IdSeccion, Fila);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdEvaluacion}")]
        [HttpPost]
        public ActionResult ObtenerConfigurarEvaluacionTrabajoPorIdEvaluacion(int IdEvaluacion)
        {
            try
            {
                ConfigurarEvaluacionTrabajoRepositorio _configurarEvaluacionTrabajoRepositorio = new ConfigurarEvaluacionTrabajoRepositorio();

                var respuesta = _configurarEvaluacionTrabajoRepositorio.ObtenerConfigurarEvaluacionTrabajoPorIdEvaluacion(IdEvaluacion);

                if (respuesta != null)
                {
                    if (respuesta.HabilitarPreguntas)
                    {
                        respuesta.PreguntasEvaluacion = _configurarEvaluacionTrabajoRepositorio.ObtenerPreguntasAsignadosConfiguracionTipoEvaluacion(respuesta.Id);
                    }
                    respuesta.InstruccionesEvaluacion = _configurarEvaluacionTrabajoRepositorio.ObtenerInstruccionEvaluacionTrabajoPorId(respuesta.IdPgeneral.Value, respuesta.IdDocumentoPw);
                }

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdConfigurarEvaluacionTrabajo}")]
        [HttpGet]
        public ActionResult ObtenerPreguntaEvaluacionTrabajoPorConfiguacion(int IdConfigurarEvaluacionTrabajo)
        {
            try
            {
                PreguntaEvaluacionTrabajoRepositorio _repPreguntaEvaluacionTrabajo = new PreguntaEvaluacionTrabajoRepositorio(_integraDBContext);

                var respuesta = _repPreguntaEvaluacionTrabajo.ObtenerPreguntaEvaluacionTrabajoPorConfiguacion(IdConfigurarEvaluacionTrabajo);

                return Ok(respuesta.Select(x=>x.IdPregunta).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerPreguntasPorProgramaEstructuraFiltro(int IdPGeneral)
        {
            try
            {
                ConfigurarEvaluacionTrabajoRepositorio _configurarEvaluacionTrabajoRepositorio = new ConfigurarEvaluacionTrabajoRepositorio();

                var respuesta = _configurarEvaluacionTrabajoRepositorio.ObtenerPreguntasPorProgramaEstructuraFiltro(IdPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerDocumentoProgramaGeneralTrabajosEvaluacionFiltro(int IdPGeneral)
        {
            try
            {
                ConfigurarEvaluacionTrabajoRepositorio _configurarEvaluacionTrabajoRepositorio = new ConfigurarEvaluacionTrabajoRepositorio();

                var respuesta = _configurarEvaluacionTrabajoRepositorio.ObtenerDocumentoProgramaGeneralTrabajosEvaluacionFiltro(IdPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerConfigurarProyectoPorConfiguracion(int IdPGeneral)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                var videos = _configurarVideoProgramaRepositorio.ValidarPRogramaPadreParaProyectoAPlicacion(IdPGeneral);
                if (videos)
                {
                    ConfigurarEvaluacionTrabajoRepositorio _configurarEvaluacionTrabajoRepositorio = new ConfigurarEvaluacionTrabajoRepositorio();

                    var respuesta = _configurarEvaluacionTrabajoRepositorio.ObtenerConfigurarProyectoFiltro(IdPGeneral);

                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest("El programa es hijo");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
