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
    [Route("api/FeedbackConfigurarGrupoPregunta")]
    public class FeedbackConfigurarGrupoPreguntaController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public FeedbackConfigurarGrupoPreguntaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerListaFeedbackConfigurarGrupoPregunta()
        {
            try
            {
                FeedbackConfigurarGrupoPreguntaRepositorio _repConfigurarGrupoPregunta = new FeedbackConfigurarGrupoPreguntaRepositorio(_integraDBContext);

                var listaConfiguracion = _repConfigurarGrupoPregunta.ObtenerListaFeedbackConfigurarGrupoPregunta();

                return Ok(listaConfiguracion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdConfiguracion}")]
        [HttpGet]
        public IActionResult ObtenerListaProgramasSelecionados(int IdConfiguracion)
        {
            try
            {
                FeedbackGrupoPreguntaProgramaGeneralRepositorio _repGrupoPreguntaProgramaGeneral = new FeedbackGrupoPreguntaProgramaGeneralRepositorio(_integraDBContext);
                FeedbackGrupoPreguntaProgramaEspecificoRepositorio _repGrupoPreguntaProgramaEspecifico = new FeedbackGrupoPreguntaProgramaEspecificoRepositorio(_integraDBContext);

                var listaPGeneral = _repGrupoPreguntaProgramaGeneral.GetBy(x=>x.IdFeedbackConfigurarGrupoPregunta == IdConfiguracion).Select(x=>x.IdPgeneral).ToList();
                var listaPEspecifico = _repGrupoPreguntaProgramaEspecifico.GetBy(x => x.IdFeedbackConfigurarGrupoPregunta == IdConfiguracion).Select(x => x.IdPespecifico).ToList();

                var combosProgramas = new
                {
                    ProgramasGenerales = listaPGeneral,
                    ProgramasEspecificos = listaPEspecifico
                };

                return Ok(combosProgramas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosProgramaConfiguracion()
        {
            try
            {
                PgeneralRepositorio _repPrograma = new PgeneralRepositorio(_integraDBContext);
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                FeedbackConfigurarRepositorio _repFeedbackConfigurar = new FeedbackConfigurarRepositorio(_integraDBContext);

                var combosProgramas = new
                {
                    ProgramasGenerales = _repPrograma.ObtenerProgramasFiltro(),
                    ProgramasEspecificos = _repPEspecifico.ObtenerPEspecificoFiltro(),
                    FeedbackConfigurados = _repFeedbackConfigurar.ObtenerTodoFeedbackConfigurarFiltro()
                };

                return Ok(combosProgramas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarFeedbackConfigurarGrupoPregunta([FromBody] registroFeedbackConfigurarGrupoPreguntaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackConfigurarGrupoPreguntaRepositorio _repConfigurarGrupoPregunta = new FeedbackConfigurarGrupoPreguntaRepositorio(_integraDBContext);
                FeedbackGrupoPreguntaProgramaGeneralRepositorio _repGrupoPreguntaProgramaGeneral = new FeedbackGrupoPreguntaProgramaGeneralRepositorio(_integraDBContext);
                FeedbackGrupoPreguntaProgramaEspecificoRepositorio _repGrupoPreguntaProgramaEspecifico = new FeedbackGrupoPreguntaProgramaEspecificoRepositorio(_integraDBContext);

                FeedbackConfigurarGrupoPreguntaBO NuevaFeedbackConfigurarGrupoPregunta = new FeedbackConfigurarGrupoPreguntaBO
                {
                    IdFeedbackConfigurar = ObjetoDTO.configuracionFeedbackGrupo.IdFeedbackConfigurar,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repConfigurarGrupoPregunta.Insert(NuevaFeedbackConfigurarGrupoPregunta);

                List<FeedbackGrupoPreguntaProgramaGeneralBO> _listaFeedbackGrupoPreguntaProgramaGeneral = new List<FeedbackGrupoPreguntaProgramaGeneralBO>();
                foreach (var detalle in ObjetoDTO.configuracionFeedbackProgramaGeneral)
                {
                    FeedbackGrupoPreguntaProgramaGeneralBO _newFeedbackGrupoPreguntaProgramaGeneral = new FeedbackGrupoPreguntaProgramaGeneralBO
                    {
                        IdFeedbackConfigurarGrupoPregunta = NuevaFeedbackConfigurarGrupoPregunta.Id,
                        IdPgeneral = detalle.Id,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _listaFeedbackGrupoPreguntaProgramaGeneral.Add(_newFeedbackGrupoPreguntaProgramaGeneral);
                }

                List<FeedbackGrupoPreguntaProgramaEspecificoBO> _listaFeedbackGrupoPreguntaProgramaEspecifico = new List<FeedbackGrupoPreguntaProgramaEspecificoBO>();
                foreach (var detalle in ObjetoDTO.configuracionProgramaEspecifico)
                {
                    FeedbackGrupoPreguntaProgramaEspecificoBO _newFeedbackGrupoPreguntaProgramaEspecifico = new FeedbackGrupoPreguntaProgramaEspecificoBO
                    {
                        IdFeedbackConfigurarGrupoPregunta = NuevaFeedbackConfigurarGrupoPregunta.Id,
                        IdPespecifico = detalle.Id,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _listaFeedbackGrupoPreguntaProgramaEspecifico.Add(_newFeedbackGrupoPreguntaProgramaEspecifico);
                }

                if (_listaFeedbackGrupoPreguntaProgramaGeneral.Count > 0)
                {
                    _repGrupoPreguntaProgramaGeneral.Insert(_listaFeedbackGrupoPreguntaProgramaGeneral);
                }
                if (_listaFeedbackGrupoPreguntaProgramaEspecifico.Count > 0)
                {
                    _repGrupoPreguntaProgramaEspecifico.Insert(_listaFeedbackGrupoPreguntaProgramaEspecifico);
                }

                return Ok(NuevaFeedbackConfigurarGrupoPregunta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarFeedbackConfigurar([FromBody] registroFeedbackConfigurarGrupoPreguntaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackConfigurarGrupoPreguntaRepositorio _repConfigurarGrupoPregunta = new FeedbackConfigurarGrupoPreguntaRepositorio(_integraDBContext);
                FeedbackGrupoPreguntaProgramaGeneralRepositorio _repGrupoPreguntaProgramaGeneral = new FeedbackGrupoPreguntaProgramaGeneralRepositorio(_integraDBContext);
                FeedbackGrupoPreguntaProgramaEspecificoRepositorio _repGrupoPreguntaProgramaEspecifico = new FeedbackGrupoPreguntaProgramaEspecificoRepositorio(_integraDBContext);

                FeedbackConfigurarGrupoPreguntaBO ActualizarFeedbackConfigurarGrupoPregunta = _repConfigurarGrupoPregunta.GetBy(x => x.Id == ObjetoDTO.configuracionFeedbackGrupo.Id).FirstOrDefault();
                ActualizarFeedbackConfigurarGrupoPregunta.IdFeedbackConfigurar = ObjetoDTO.configuracionFeedbackGrupo.IdFeedbackConfigurar;
                ActualizarFeedbackConfigurarGrupoPregunta.Estado = true;
                ActualizarFeedbackConfigurarGrupoPregunta.UsuarioModificacion = ObjetoDTO.Usuario;
                ActualizarFeedbackConfigurarGrupoPregunta.FechaModificacion = DateTime.Now;
                _repConfigurarGrupoPregunta.Update(ActualizarFeedbackConfigurarGrupoPregunta);

                var _eliminacionAsociadosPGeneral = _repGrupoPreguntaProgramaGeneral.GetBy(x => x.IdFeedbackConfigurarGrupoPregunta == ActualizarFeedbackConfigurarGrupoPregunta.Id).Select(x=>x.Id);
                var _eliminacionAsociadosPEspecifico = _repGrupoPreguntaProgramaEspecifico.GetBy(x => x.IdFeedbackConfigurarGrupoPregunta == ActualizarFeedbackConfigurarGrupoPregunta.Id).Select(x=>x.Id);

                if (_eliminacionAsociadosPGeneral.Count() > 0)
                {
                    _repGrupoPreguntaProgramaGeneral.Delete(_eliminacionAsociadosPGeneral, ObjetoDTO.Usuario);
                }
                if (_eliminacionAsociadosPEspecifico.Count() > 0)
                {
                    _repGrupoPreguntaProgramaEspecifico.Delete(_eliminacionAsociadosPEspecifico, ObjetoDTO.Usuario);
                }

                List<FeedbackGrupoPreguntaProgramaGeneralBO> _listaFeedbackGrupoPreguntaProgramaGeneral = new List<FeedbackGrupoPreguntaProgramaGeneralBO>();
                foreach (var detalle in ObjetoDTO.configuracionFeedbackProgramaGeneral)
                {
                    FeedbackGrupoPreguntaProgramaGeneralBO _newFeedbackGrupoPreguntaProgramaGeneral = new FeedbackGrupoPreguntaProgramaGeneralBO
                    {
                        IdFeedbackConfigurarGrupoPregunta = ActualizarFeedbackConfigurarGrupoPregunta.Id,
                        IdPgeneral = detalle.Id,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _listaFeedbackGrupoPreguntaProgramaGeneral.Add(_newFeedbackGrupoPreguntaProgramaGeneral);
                }

                List<FeedbackGrupoPreguntaProgramaEspecificoBO> _listaFeedbackGrupoPreguntaProgramaEspecifico = new List<FeedbackGrupoPreguntaProgramaEspecificoBO>();
                foreach (var detalle in ObjetoDTO.configuracionProgramaEspecifico)
                {
                    FeedbackGrupoPreguntaProgramaEspecificoBO _newFeedbackGrupoPreguntaProgramaEspecifico = new FeedbackGrupoPreguntaProgramaEspecificoBO
                    {
                        IdFeedbackConfigurarGrupoPregunta = ActualizarFeedbackConfigurarGrupoPregunta.Id,
                        IdPespecifico = detalle.Id,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _listaFeedbackGrupoPreguntaProgramaEspecifico.Add(_newFeedbackGrupoPreguntaProgramaEspecifico);
                }

                if (_listaFeedbackGrupoPreguntaProgramaGeneral.Count > 0)
                {
                    _repGrupoPreguntaProgramaGeneral.Insert(_listaFeedbackGrupoPreguntaProgramaGeneral);
                }
                if (_listaFeedbackGrupoPreguntaProgramaEspecifico.Count > 0)
                {
                    _repGrupoPreguntaProgramaEspecifico.Insert(_listaFeedbackGrupoPreguntaProgramaEspecifico);
                }

                return Ok(ActualizarFeedbackConfigurarGrupoPregunta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdConfiguracion}/{Usuario}")]
        [HttpPost]
        public ActionResult EliminarFeedbackConfigurar(int IdConfiguracion, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackConfigurarGrupoPreguntaRepositorio _repConfigurarGrupoPregunta = new FeedbackConfigurarGrupoPreguntaRepositorio(_integraDBContext);
                FeedbackGrupoPreguntaProgramaGeneralRepositorio _repGrupoPreguntaProgramaGeneral = new FeedbackGrupoPreguntaProgramaGeneralRepositorio(_integraDBContext);
                FeedbackGrupoPreguntaProgramaEspecificoRepositorio _repGrupoPreguntaProgramaEspecifico = new FeedbackGrupoPreguntaProgramaEspecificoRepositorio(_integraDBContext);

                FeedbackConfigurarGrupoPreguntaBO ActualizarFeedbackConfigurarGrupoPregunta = _repConfigurarGrupoPregunta.GetBy(x => x.Id == IdConfiguracion).FirstOrDefault();

                if (ActualizarFeedbackConfigurarGrupoPregunta != null)
                {
                    _repConfigurarGrupoPregunta.Delete(IdConfiguracion, Usuario);

                    var _eliminacionAsociadosPGeneral = _repGrupoPreguntaProgramaGeneral.GetBy(x => x.IdFeedbackConfigurarGrupoPregunta == IdConfiguracion).Select(x => x.Id);
                    var _eliminacionAsociadosPEspecifico = _repGrupoPreguntaProgramaEspecifico.GetBy(x => x.IdFeedbackConfigurarGrupoPregunta == IdConfiguracion).Select(x => x.Id);

                    if (_eliminacionAsociadosPGeneral.Count() > 0)
                    {
                        _repGrupoPreguntaProgramaGeneral.Delete(_eliminacionAsociadosPGeneral, Usuario);
                    }
                    if (_eliminacionAsociadosPEspecifico.Count() > 0)
                    {
                        _repGrupoPreguntaProgramaEspecifico.Delete(_eliminacionAsociadosPEspecifico, Usuario);
                    }
                }
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
