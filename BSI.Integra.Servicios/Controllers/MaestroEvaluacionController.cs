using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaestroEvaluacion")]
    public class MaestroEvaluacionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public MaestroEvaluacionController()
        {
            _integraDBContext = new integraDBContext();
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionPersona()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenRepositorio repExamenFeedbackRep = new ExamenRepositorio();

                return Ok(repExamenFeedbackRep.ObtenerEvaluacionPersonaCompleto().OrderByDescending(x=>x.IdExamen));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosEvaluacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenFeedbackRepositorio repExamenFeedbackRep = new ExamenFeedbackRepositorio(_integraDBContext);
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio(_integraDBContext);
                CriterioEvaluacionProcesoRepositorio repCriterioEvaluacionProcesoRep = new CriterioEvaluacionProcesoRepositorio(_integraDBContext);
                SexoRepositorio repSexo= new SexoRepositorio(_integraDBContext);
                FormulaPuntajeRepositorio repFormula = new FormulaPuntajeRepositorio(_integraDBContext);

                var listaFeedback= repExamenFeedbackRep.GetBy(x => x.Estado == true , x => new { Id=x.Id, Nombre=x.Nombre }).ToList();
                var listaTest = repExamenTestRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.NombreAbreviado }).ToList();
                var listaCriterio = repCriterioEvaluacionProcesoRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre }).ToList();
                var listaSexo = repSexo.GetFiltroIdNombre();
                var listaFormula = repFormula.GetFiltroIdNombre();

                return Ok(new { listaFeedback , listaTest, listaCriterio, listaSexo, listaFormula });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarExamen([FromBody] EvaluacionPersonaCompletoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ExamenRepositorio repExamenRep = new ExamenRepositorio(_integraDBContext);
                ExamenComportamientoRepositorio repExamenComportamiento = new ExamenComportamientoRepositorio(_integraDBContext);
                ExamenConfiguracionFormatoRepositorio repExamenConfiguracionFormato = new ExamenConfiguracionFormatoRepositorio(_integraDBContext);
                ExamenConfigurarResultadoRepositorio repExamenConfigurarResultadoRep = new ExamenConfigurarResultadoRepositorio(_integraDBContext);
                CentilRepositorio repCentilRep = new CentilRepositorio(_integraDBContext);
                FormulaPuntajeRepositorio repFormula = new FormulaPuntajeRepositorio(_integraDBContext);
                ExamenBO examen = new ExamenBO();
                ExamenConfiguracionFormatoBO examenConfiguracionFormato = new ExamenConfiguracionFormatoBO();
                ExamenComportamientoBO examenComportamiento = new ExamenComportamientoBO();
                ExamenConfigurarResultadoBO examenConfigurarResultado = new ExamenConfigurarResultadoBO();
                FormulaPuntajeBO formulaPuntaje = new FormulaPuntajeBO();

                examen = repExamenRep.FirstById(Json.IdExamen);

                using (TransactionScope scope = new TransactionScope())                {
                    

                    if (Json.IdExamenConfiguracionFormato == null)
                    {
                        examenConfiguracionFormato.Activo = true;
                        examenConfiguracionFormato.ColorFondoEnunciado = Json.ColorFondoEnunciado;
                        examenConfiguracionFormato.ColorTextoEnunciado = Json.ColorTextoEnunciado;
                        examenConfiguracionFormato.TamanioTextoEnunciado = Json.TamanioTextoEnunciado;
                        examenConfiguracionFormato.TipoLetraEnunciado = Json.TipoLetraEnunciado;
                        examenConfiguracionFormato.ColorFondoTitulo = Json.ColorFondoTitulo;
                        examenConfiguracionFormato.ColorTextoTitulo = Json.ColorTextoTitulo;
                        examenConfiguracionFormato.TamanioTextoTitulo = Json.TamanioTextoTitulo;
                        examenConfiguracionFormato.TipoLetraTitulo = Json.TipoLetraTitulo;
                        examenConfiguracionFormato.ColorFondoRespuesta = Json.ColorFondoRespuesta;
                        examenConfiguracionFormato.ColorTextoRespuesta = Json.ColorTextoRespuesta;
                        examenConfiguracionFormato.TamanioTextoRespuesta = Json.TamanioTextoRespuesta;
                        examenConfiguracionFormato.TipoLetraRespuesta = Json.TipoLetraRespuesta;
                        examenConfiguracionFormato.Estado = true;
                        examenConfiguracionFormato.UsuarioCreacion = Json.UsuarioModificacion;
                        examenConfiguracionFormato.FechaCreacion = DateTime.Now;
                        examenConfiguracionFormato.UsuarioModificacion = Json.UsuarioModificacion;
                        examenConfiguracionFormato.FechaModificacion = DateTime.Now;
                        repExamenConfiguracionFormato.Insert(examenConfiguracionFormato);
                    }
                    else {
                        examenConfiguracionFormato= repExamenConfiguracionFormato.FirstById(Json.IdExamenConfiguracionFormato.Value);
                        examenConfiguracionFormato.Activo = true;
                        examenConfiguracionFormato.ColorFondoEnunciado = Json.ColorFondoEnunciado;
                        examenConfiguracionFormato.ColorTextoEnunciado = Json.ColorTextoEnunciado;
                        examenConfiguracionFormato.TamanioTextoEnunciado = Json.TamanioTextoEnunciado;
                        examenConfiguracionFormato.TipoLetraEnunciado = Json.TipoLetraEnunciado;
                        examenConfiguracionFormato.ColorFondoTitulo = Json.ColorFondoTitulo;
                        examenConfiguracionFormato.ColorTextoTitulo = Json.ColorTextoTitulo;
                        examenConfiguracionFormato.TamanioTextoTitulo = Json.TamanioTextoTitulo;
                        examenConfiguracionFormato.TipoLetraTitulo = Json.TipoLetraTitulo;
                        examenConfiguracionFormato.ColorFondoRespuesta = Json.ColorFondoRespuesta;
                        examenConfiguracionFormato.ColorTextoRespuesta = Json.ColorTextoRespuesta;
                        examenConfiguracionFormato.TamanioTextoRespuesta = Json.TamanioTextoRespuesta;
                        examenConfiguracionFormato.TipoLetraRespuesta = Json.TipoLetraRespuesta;
                        examenConfiguracionFormato.UsuarioModificacion = Json.UsuarioModificacion;
                        examenConfiguracionFormato.FechaModificacion = DateTime.Now;
                        repExamenConfiguracionFormato.Update(examenConfiguracionFormato);
                    }

                    if (Json.IdExamenComportamiento == null)
                    {
                        examenComportamiento.PreguntaObligatoria = Json.ResponderTodasLasPreguntas.Value;
                        examenComportamiento.IdEvaluacionFeedbackAprobado = Json.IdEvaluacionFeedBackAprobado;
                        examenComportamiento.IdEvaluacionFeedbackDesaprobado = Json.IdEvaluacionFeedBackDesaprobado;
                        examenComportamiento.IdEvaluacionFeedbackCancelado = Json.IdEvaluacionFeedBackCancelado;
                        examenComportamiento.Estado = true;
                        examenComportamiento.UsuarioCreacion = Json.UsuarioModificacion;
                        examenComportamiento.FechaCreacion = DateTime.Now;
                        examenComportamiento.UsuarioModificacion = Json.UsuarioModificacion;
                        examenComportamiento.FechaModificacion = DateTime.Now;
                        repExamenComportamiento.Insert(examenComportamiento);
                    }
                    else
                    {
                        examenComportamiento = repExamenComportamiento.FirstById(Json.IdExamenComportamiento.Value);
                        examenComportamiento.PreguntaObligatoria = Json.ResponderTodasLasPreguntas.Value;
                        examenComportamiento.IdEvaluacionFeedbackAprobado = Json.IdEvaluacionFeedBackAprobado;
                        examenComportamiento.IdEvaluacionFeedbackDesaprobado = Json.IdEvaluacionFeedBackDesaprobado;
                        examenComportamiento.IdEvaluacionFeedbackCancelado = Json.IdEvaluacionFeedBackCancelado;
                        examenComportamiento.UsuarioModificacion = Json.UsuarioModificacion;
                        examenComportamiento.FechaModificacion = DateTime.Now;
                        repExamenComportamiento.Update(examenComportamiento);
                    }

                    if (Json.IdExamenConfigurarResultado == null)
                    {
                        examenConfigurarResultado.ExamenCalificado = Json.CalificarExamen.Value;
                        examenConfigurarResultado.PuntajeExamen = Json.PuntajeExamen;
                        examenConfigurarResultado.PuntajeAprobacion = Json.PuntajeAprobacion;
                        examenConfigurarResultado.MostrarResultado = Json.MostrarResultado.Value;
                        examenConfigurarResultado.MostrarPuntajeTotal = Json.MostrarPuntajeTotal.Value;
                        examenConfigurarResultado.MostrarPuntajePregunta = Json.MostrarPuntajePregunta.Value;
                        examenConfigurarResultado.Estado = true;
                        examenConfigurarResultado.UsuarioCreacion = Json.UsuarioModificacion;
                        examenConfigurarResultado.FechaCreacion = DateTime.Now;
                        examenConfigurarResultado.UsuarioModificacion = Json.UsuarioModificacion;
                        examenConfigurarResultado.FechaModificacion = DateTime.Now;
                        repExamenConfigurarResultadoRep.Insert(examenConfigurarResultado);
                    }
                    else
                    {
                        examenConfigurarResultado = repExamenConfigurarResultadoRep.FirstById(Json.IdExamenConfigurarResultado.Value);
                        examenConfigurarResultado.ExamenCalificado = Json.CalificarExamen.Value;
                        examenConfigurarResultado.PuntajeExamen = Json.PuntajeExamen;
                        examenConfigurarResultado.PuntajeAprobacion = Json.PuntajeAprobacion;
                        examenConfigurarResultado.MostrarResultado = Json.MostrarResultado.Value;
                        examenConfigurarResultado.MostrarPuntajeTotal = Json.MostrarPuntajeTotal.Value;
                        examenConfigurarResultado.MostrarPuntajePregunta = Json.MostrarPuntajePregunta.Value;
                        examenConfigurarResultado.UsuarioModificacion = Json.UsuarioModificacion;
                        examenConfigurarResultado.FechaModificacion = DateTime.Now;
                        repExamenConfigurarResultadoRep.Update(examenConfigurarResultado);
                    }

                    byte[] _base64 = Convert.FromBase64String(Json.Instrucciones);
                    var _contenido = Encoding.UTF8.GetString(_base64);

                    var listaCentiles = repCentilRep.ObtenerCentilesAsignados(Json.IdExamen);
                    foreach(var item in listaCentiles)
                    {
                        if(!Json.ListaCentiles.Any(x => x.Id == item.Id)){
                            repCentilRep.Delete(item.Id, Json.UsuarioModificacion);
                        }                            
                    }
                    foreach (var item in Json.ListaCentiles)
                    {
                        if (item.Id > 0)
                        {
                            var centil = repCentilRep.FirstById(item.Id);
                            centil.IdExamen = Json.IdExamen;
                            centil.Centil = item.Centil;
                            centil.ValorMinimo = item.ValorMinimo;
                            centil.ValorMaximo = item.ValorMaximo;
                            centil.CentilLetra = item.CentilLetra;
                            centil.IdSexo = item.IdSexo;                            
                            centil.UsuarioModificacion = Json.UsuarioModificacion;                            
                            centil.FechaModificacion = DateTime.Now;
                            repCentilRep.Update(centil);
                        }
                        else
                        {
                            CentilBO centil = new CentilBO();
                            centil.IdExamen = Json.IdExamen;
                            centil.Centil = item.Centil;
                            centil.ValorMinimo = item.ValorMinimo;
                            centil.ValorMaximo = item.ValorMaximo;
                            centil.CentilLetra = item.CentilLetra;
                            centil.IdSexo = item.IdSexo;
                            centil.UsuarioCreacion = Json.UsuarioModificacion;
                            centil.UsuarioModificacion = Json.UsuarioModificacion;
                            centil.Estado = true;
                            centil.FechaCreacion = DateTime.Now;
                            centil.FechaModificacion = DateTime.Now;
                            repCentilRep.Insert(centil);
                        }
                    }


                    examen.Nombre = Json.NombreEvaluacion;
                    examen.Titulo = Json.TituloEvaluacion;
                    examen.RequiereTiempo = Json.CronometrarExamen;
                    examen.DuracionMinutos = Json.TiempoLimiteExamen;
                    examen.Instrucciones = _contenido;
                    examen.IdExamenTest = Json.IdExamenTest;
                    examen.IdCriterioEvaluacionProceso = Json.IdCriterioEvaluacionProceso;
                    examen.IdExamenConfiguracionFormato = examenConfiguracionFormato.Id;
                    examen.IdExamenComportamiento = examenComportamiento.Id;
                    examen.IdExamenConfigurarResultado = examenConfigurarResultado.Id;
                    examen.UsuarioModificacion = Json.UsuarioModificacion;
                    examen.FechaModificacion = DateTime.Now;
                    examen.IdFormulaPuntaje = Json.IdFormulaPuntaje;
					examen.RequiereCentil = Json.RequiereCentil;
					examen.Factor = Json.Factor;

                    repExamenRep.Update(examen);

                    Json.IdExamenComportamiento = examenComportamiento.Id;
                    Json.IdExamenConfiguracionFormato = examenConfiguracionFormato.Id;
                    Json.IdExamenConfigurarResultado = examenConfigurarResultado.Id;

                    scope.Complete();
                }
                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]

        public ActionResult InsertarCentil([FromBody] List<CentilDTO> DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarExamen([FromBody] EvaluacionPersonaCompletoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenRepositorio repExamenRep = new ExamenRepositorio(_integraDBContext);
                ExamenBO examen = new ExamenBO();
                ExamenComportamientoRepositorio repExamenComportamiento = new ExamenComportamientoRepositorio(_integraDBContext);
                ExamenConfiguracionFormatoRepositorio repExamenConfiguracionFormato = new ExamenConfiguracionFormatoRepositorio(_integraDBContext);
                ExamenConfigurarResultadoRepositorio repExamenConfigurarResultadoRep = new ExamenConfigurarResultadoRepositorio(_integraDBContext);
                CentilRepositorio repCentilRep = new CentilRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    ExamenConfiguracionFormatoBO examenConfiguracionFormato = new ExamenConfiguracionFormatoBO();
                    examenConfiguracionFormato.Activo = true;
                    examenConfiguracionFormato.ColorFondoEnunciado=Json.ColorFondoEnunciado;
                    examenConfiguracionFormato.ColorTextoEnunciado=Json.ColorTextoEnunciado;
                    examenConfiguracionFormato.TamanioTextoEnunciado=Json.TamanioTextoEnunciado;
                    examenConfiguracionFormato.TipoLetraEnunciado=Json.TipoLetraEnunciado;
                    examenConfiguracionFormato.ColorFondoTitulo=Json.ColorFondoTitulo;
                    examenConfiguracionFormato.ColorTextoTitulo=Json.ColorTextoTitulo;
                    examenConfiguracionFormato.TamanioTextoTitulo=Json.TamanioTextoTitulo;
                    examenConfiguracionFormato.TipoLetraTitulo=Json.TipoLetraTitulo;
                    examenConfiguracionFormato.ColorFondoRespuesta=Json.ColorFondoRespuesta;
                    examenConfiguracionFormato.ColorTextoRespuesta=Json.ColorTextoRespuesta;
                    examenConfiguracionFormato.TamanioTextoRespuesta=Json.TamanioTextoRespuesta;
                    examenConfiguracionFormato.TipoLetraRespuesta=Json.TipoLetraRespuesta;
                    examenConfiguracionFormato.Estado = true;
                    examenConfiguracionFormato.UsuarioCreacion = Json.UsuarioModificacion;
                    examenConfiguracionFormato.FechaCreacion = DateTime.Now;
                    examenConfiguracionFormato.UsuarioModificacion = Json.UsuarioModificacion;
                    examenConfiguracionFormato.FechaModificacion = DateTime.Now;
                    repExamenConfiguracionFormato.Insert(examenConfiguracionFormato);


                    ExamenComportamientoBO examenComportamiento = new ExamenComportamientoBO();
                    examenComportamiento.PreguntaObligatoria = Json.ResponderTodasLasPreguntas.Value;
                    examenComportamiento.IdEvaluacionFeedbackAprobado = Json.IdEvaluacionFeedBackAprobado;
                    examenComportamiento.IdEvaluacionFeedbackDesaprobado = Json.IdEvaluacionFeedBackDesaprobado;
                    examenComportamiento.IdEvaluacionFeedbackCancelado=Json.IdEvaluacionFeedBackCancelado;
                    examenComportamiento.Estado = true;
                    examenComportamiento.UsuarioCreacion = Json.UsuarioModificacion;
                    examenComportamiento.FechaCreacion = DateTime.Now;
                    examenComportamiento.UsuarioModificacion = Json.UsuarioModificacion;
                    examenComportamiento.FechaModificacion = DateTime.Now;
                    repExamenComportamiento.Insert(examenComportamiento);

                    ExamenConfigurarResultadoBO examenConfigurarResultado = new ExamenConfigurarResultadoBO();
                    examenConfigurarResultado.ExamenCalificado = Json.CalificarExamen.Value;
                    examenConfigurarResultado.PuntajeExamen=Json.PuntajeExamen;
                    examenConfigurarResultado.PuntajeAprobacion=Json.PuntajeAprobacion;
                    examenConfigurarResultado.MostrarResultado=Json.MostrarResultado.Value;
                    examenConfigurarResultado.MostrarPuntajeTotal = Json.MostrarPuntajeTotal.Value;
                    examenConfigurarResultado.MostrarPuntajePregunta = Json.MostrarPuntajePregunta.Value;
                    examenConfigurarResultado.Estado = true;
                    examenConfigurarResultado.UsuarioCreacion = Json.UsuarioModificacion;
                    examenConfigurarResultado.FechaCreacion = DateTime.Now;
                    examenConfigurarResultado.UsuarioModificacion = Json.UsuarioModificacion;
                    examenConfigurarResultado.FechaModificacion = DateTime.Now;
                    repExamenConfigurarResultadoRep.Insert(examenConfigurarResultado);

                    byte[] _base64 = Convert.FromBase64String(Json.Instrucciones);
                    var _contenido = Encoding.UTF8.GetString(_base64);
                    examen.Nombre = Json.NombreEvaluacion;
                    examen.Titulo = Json.TituloEvaluacion;
                    examen.RequiereTiempo = Json.CronometrarExamen;
                    examen.DuracionMinutos = Json.TiempoLimiteExamen;
                    examen.Instrucciones = _contenido;
                    examen.IdExamenTest = Json.IdExamenTest;
                    examen.IdCriterioEvaluacionProceso = Json.IdCriterioEvaluacionProceso;
                    examen.IdExamenConfiguracionFormato = examenConfiguracionFormato.Id;
                    examen.IdExamenComportamiento= examenComportamiento.Id;
                    examen.IdExamenConfigurarResultado= examenConfigurarResultado.Id;
                    examen.Estado = true;
                    examen.UsuarioCreacion = Json.UsuarioModificacion;
                    examen.FechaCreacion = DateTime.Now;
                    examen.UsuarioModificacion = Json.UsuarioModificacion;
                    examen.FechaModificacion = DateTime.Now;
                    examen.RequiereCentil = Json.RequiereCentil;
                    examen.IdFormulaPuntaje = Json.IdFormulaPuntaje;
					examen.Factor = Json.Factor;
					repExamenRep.Insert(examen);

                    Json.IdExamen = examen.Id;
                    Json.IdExamenComportamiento = examenComportamiento.Id;
                    Json.IdExamenConfiguracionFormato = examenConfiguracionFormato.Id;
                    Json.IdExamenConfigurarResultado = examenConfigurarResultado.Id;

                    foreach (var item in Json.ListaCentiles)
                    {
                        CentilBO centil = new CentilBO();          
                        centil.IdExamen = Json.IdExamen;
                        centil.Centil = item.Centil;
                        centil.ValorMinimo = item.ValorMinimo;
                        centil.ValorMaximo = item.ValorMaximo;
                        centil.CentilLetra = item.CentilLetra;
                        centil.IdSexo = item.IdSexo;
                        centil.UsuarioCreacion = Json.UsuarioModificacion;
                        centil.UsuarioModificacion = Json.UsuarioModificacion;
                        centil.Estado = true;
                        centil.FechaCreacion = DateTime.Now;
                        centil.FechaModificacion = DateTime.Now;
                        repCentilRep.Insert(centil);
                    }

                    scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdExamen}")]
        [HttpGet]
        public ActionResult ObtenerPreguntasNoAsociados(int IdExamen)
        {
            try
            {
                AsignacionPreguntaExamenRepositorio repAsignacionPreguntaExamenRep = new AsignacionPreguntaExamenRepositorio();

                var listaExamen = repAsignacionPreguntaExamenRep.ObtenerPreguntaNoAsignadoExamen(IdExamen);

                return Ok(listaExamen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdExamen}")]
        [HttpGet]
        public ActionResult ObtenerPreguntasAsociados(int IdExamen)
        {
            try
            {
                AsignacionPreguntaExamenRepositorio repAsignacionPreguntaExamenRep = new AsignacionPreguntaExamenRepositorio();

                var listaExamen = repAsignacionPreguntaExamenRep.ObtenerPreguntaAsignadoExamen(IdExamen);

                return Ok(listaExamen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarAsociacionPreguntas([FromBody] ConsolidadoPreguntaAsociado Json) {
            try
            {
                AsignacionPreguntaExamenRepositorio repAsignacionPreguntaExamenRep = new AsignacionPreguntaExamenRepositorio(_integraDBContext);
                List<AsignacionPreguntaExamenBO> ListaPregunta = repAsignacionPreguntaExamenRep.GetBy(x => x.IdExamen == Json.IdExamen).ToList();
                List<AsignacionPreguntaExamenBO> EliminarExamen = new List<AsignacionPreguntaExamenBO>();
                List<int> IdExistente = new List<int>();
                if (ListaPregunta.Count() <= 0) {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (var item in Json.ListaPreguntasAsignadas)
                        {
                            AsignacionPreguntaExamenBO asignacion = new AsignacionPreguntaExamenBO();
                            asignacion.IdExamen = item.IdExamen;
                            asignacion.IdPregunta = item.IdPregunta;
                            asignacion.NroOrden = item.NroOrden;
                            asignacion.Puntaje = item.Puntaje;
                            asignacion.Estado = true;
                            asignacion.UsuarioCreacion = Json.Usuario;
                            asignacion.UsuarioModificacion = Json.Usuario;
                            asignacion.FechaCreacion = DateTime.Now;
                            asignacion.FechaModificacion = DateTime.Now;
                            repAsignacionPreguntaExamenRep.Insert(asignacion);
                        }
                        scope.Complete();
                    }
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (var item in Json.ListaPreguntasAsignadas)
                        {
                            var PreguntaNuevo = ListaPregunta.Where(x => x.Id == item.Id).FirstOrDefault();
                            if (PreguntaNuevo != null && item.Id != 0)
                            {
                                AsignacionPreguntaExamenBO pregunta = repAsignacionPreguntaExamenRep.FirstById(item.Id); ;
                                pregunta.IdPregunta = item.IdPregunta;
                                pregunta.IdExamen = item.IdExamen;
                                pregunta.NroOrden = item.NroOrden;
                                pregunta.Puntaje = item.Puntaje;

                                pregunta.UsuarioModificacion = Json.Usuario;
                                pregunta.FechaModificacion = DateTime.Now;
                                repAsignacionPreguntaExamenRep.Update(pregunta);
                                IdExistente.Add(pregunta.Id);
                            }
                            else
                            {
                                AsignacionPreguntaExamenBO pregunta = new AsignacionPreguntaExamenBO();
                                pregunta.IdExamen = item.IdExamen;
                                pregunta.IdPregunta = item.IdPregunta;
                                pregunta.NroOrden = item.NroOrden;
                                pregunta.Puntaje = item.Puntaje;

                                pregunta.Estado = true;
                                pregunta.UsuarioCreacion = Json.Usuario;
                                pregunta.UsuarioModificacion = Json.Usuario;
                                pregunta.FechaCreacion = DateTime.Now;
                                pregunta.FechaModificacion = DateTime.Now;
                                repAsignacionPreguntaExamenRep.Insert(pregunta);
                                IdExistente.Add(pregunta.Id);
                            }

                        }
                        EliminarExamen = repAsignacionPreguntaExamenRep.GetBy(x => !IdExistente.Contains(x.Id) && x.IdExamen == Json.IdExamen).ToList();
                        foreach (var eliminar in EliminarExamen)
                        {
                            repAsignacionPreguntaExamenRep.Delete(eliminar.Id, Json.Usuario);
                        }
                        scope.Complete();
                    }

                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Britsel C. - Edgar S.
        /// Fecha: 30/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene centiles asociados
        /// </summary>
        /// <param name="IdExamen"> Id de Examen </param>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCentilesAsociados([FromBody] int IdExamen)
        {
            try
            {
                CentilRepositorio _repCentil = new CentilRepositorio();
                ExamenRepositorio _repExamen = new ExamenRepositorio();

                var listaCentil = _repCentil.ObtenerCentilesAsignados(IdExamen);
                var notaExamen = _repExamen.ObtenerPuntajeCalificacion(IdExamen);
                return Ok(new { listaCentil, NotaExamen = notaExamen.PuntajeTipoRespuesta, cantidadExamen = notaExamen.CantidadPreguntas, sumaExamen = notaExamen.SumaPuntaje });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarExamen([FromBody] EliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenRepositorio repExamenRep = new ExamenRepositorio(_integraDBContext);
                ExamenComportamientoRepositorio repExamenComportamiento = new ExamenComportamientoRepositorio(_integraDBContext);
                ExamenConfiguracionFormatoRepositorio repExamenConfiguracionFormato = new ExamenConfiguracionFormatoRepositorio(_integraDBContext);
                ExamenConfigurarResultadoRepositorio repExamenConfigurarResultadoRep = new ExamenConfigurarResultadoRepositorio(_integraDBContext);
                CentilRepositorio repCentilRep = new CentilRepositorio(_integraDBContext);
				var examen = repExamenRep.FirstById(Json.Id);
                var listaCentiles = repCentilRep.ObtenerCentilesAsignados(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
					if(examen.IdExamenConfiguracionFormato.HasValue)
						repExamenConfiguracionFormato.Delete(examen.IdExamenConfiguracionFormato.Value, Json.NombreUsuario);
					if(examen.IdExamenComportamiento.HasValue)
						repExamenComportamiento.Delete(examen.IdExamenComportamiento.Value, Json.NombreUsuario);
					if(examen.IdExamenConfigurarResultado.HasValue)
						repExamenConfigurarResultadoRep.Delete(examen.IdExamenConfigurarResultado.Value, Json.NombreUsuario);
                    foreach (var item in listaCentiles)
                    {
                        repCentilRep.Delete(item.Id, Json.NombreUsuario);
                    }
                    repExamenRep.Delete(Json.Id, Json.NombreUsuario);

                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}