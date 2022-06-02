using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PreguntaFrecuente")]
    public class PreguntaFrecuenteController : BaseController<TPreguntaFrecuente, ValidadorPreguntaFrecuenteDTO>
    {
        public PreguntaFrecuenteController(IIntegraRepository<TPreguntaFrecuente> repositorio, ILogger<BaseController<TPreguntaFrecuente, ValidadorPreguntaFrecuenteDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                PreguntaFrecuenteRepositorio preguntaFrecuenteRepositorio = new PreguntaFrecuenteRepositorio();
                return Ok(preguntaFrecuenteRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerSeccionPreguntaFrecuente()
        {
            try
            {
                SeccionPreguntaFrecuenteRepositorio seccionPreguntaFrecuenteRepositorio = new SeccionPreguntaFrecuenteRepositorio();
                
                return Ok(seccionPreguntaFrecuenteRepositorio.ObtenerSeccionPreguntaFrecuenteFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAreaCapacitacion()
        {
            try
            {
                AreaCapacitacionRepositorio areaCapacitacionRepositorio = new AreaCapacitacionRepositorio();
                return Ok(areaCapacitacionRepositorio.ObtenerAreaCapacitacionFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerSubAreaCapacitacion()
        {
            try
            {
                SubAreaCapacitacionRepositorio subAreaCapacitacionRepositorio = new SubAreaCapacitacionRepositorio();
                return Ok(subAreaCapacitacionRepositorio.ObtenerTodoFiltroAutoSelect());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTroncalPgeneral()
        {
            try
            {
                TroncalPgeneralRepositorio troncalPgeneralRepositorio = new TroncalPgeneralRepositorio();

                return Ok(troncalPgeneralRepositorio.ObtenerTroncalPgeneralFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerModalidadCurso()
        {
            try
            {
                ModalidadCursoRepositorio modalidadCursoRepositorio = new ModalidadCursoRepositorio();
                return Ok(modalidadCursoRepositorio.ObtenerModalidadCursoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarListaPreguntaFrecuente([FromBody] PreguntaFrecuenteFiltroDTO filtro)
        {
            try
            {
                //filtro._areas = ListIntToString(filtro.areas);
                //filtro._subareas = ListIntToString(filtro.subareas);
                //filtro._pgenerales = ListIntToString(filtro.pgenerales);
                //filtro._tipos = ListIntToString(filtro.tipos);

                PreguntaFrecuenteRepositorio preguntaFrecuenteRepositorio = new PreguntaFrecuenteRepositorio();
                return Ok(preguntaFrecuenteRepositorio.ObtenerListaPreguntaFrecuente(filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
   
        private string ListIntToString(List<int> datos)
        {
            if (datos == null)
            {
                datos = new List<int>();
                datos.Add(3);
            }
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += datos[i] + ",";
            if (NumberElements > 0)
                rptaCadena += datos[NumberElements - 1];
            return rptaCadena;
        }

        [Route("[action]")]
        [HttpPost]

        public ActionResult InsertarPreguntaFrecuente([FromBody] CompuestoPreguntaFrecuenteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PreguntaFrecuenteRepositorio repPreguntaFrecuente = new PreguntaFrecuenteRepositorio();
                PreguntaFrecuenteBO preguntaFrecuente = new PreguntaFrecuenteBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    preguntaFrecuente.Id = Json.PreguntaFrecuente.Id;
                    preguntaFrecuente.IdSeccionPreguntaFrecuente = Json.PreguntaFrecuente.IdSeccionPreguntaFrecuente;
                    preguntaFrecuente.Pregunta = Json.PreguntaFrecuente.Pregunta;
                    preguntaFrecuente.Respuesta = Json.PreguntaFrecuente.Respuesta;
                    preguntaFrecuente.Tipo = Json.PreguntaFrecuente.Tipo;
                    preguntaFrecuente.UsuarioCreacion = Json.Usuario;
                    preguntaFrecuente.UsuarioModificacion = Json.Usuario;
                    preguntaFrecuente.FechaCreacion = DateTime.Now;
                    preguntaFrecuente.FechaModificacion = DateTime.Now;
                    preguntaFrecuente.Estado = true;

                    preguntaFrecuente.PreguntaFrecuentePgeneral = new List<PreguntaFrecuentePgeneralBO>();

                    foreach (var item in Json.ListaPGenerales)
                    {
                        PreguntaFrecuentePgeneralBO preguntaFrecuentePgeneral = new PreguntaFrecuentePgeneralBO();
                        preguntaFrecuentePgeneral.IdPreguntaFrecuente = Json.PreguntaFrecuente.Id;
                        preguntaFrecuentePgeneral.IdPgeneral = item;
                        preguntaFrecuentePgeneral.UsuarioCreacion = Json.Usuario;
                        preguntaFrecuentePgeneral.UsuarioModificacion = Json.Usuario;
                        preguntaFrecuentePgeneral.FechaCreacion = DateTime.Now;
                        preguntaFrecuentePgeneral.FechaModificacion = DateTime.Now;
                        preguntaFrecuentePgeneral.Estado = true;

                        preguntaFrecuente.PreguntaFrecuentePgeneral.Add(preguntaFrecuentePgeneral);
                    }

                    preguntaFrecuente.PreguntaFrecuenteArea = new List<PreguntaFrecuenteAreaBO>();

                    foreach (var item in Json.ListaAreas)
                    {
                        PreguntaFrecuenteAreaBO preguntaFrecuenteArea = new PreguntaFrecuenteAreaBO();
                        preguntaFrecuenteArea.IdPreguntaFrecuente = Json.PreguntaFrecuente.Id;
                        preguntaFrecuenteArea.IdArea = item;
                        preguntaFrecuenteArea.UsuarioCreacion = Json.Usuario;
                        preguntaFrecuenteArea.UsuarioModificacion = Json.Usuario;
                        preguntaFrecuenteArea.FechaCreacion = DateTime.Now;
                        preguntaFrecuenteArea.FechaModificacion = DateTime.Now;
                        preguntaFrecuenteArea.Estado = true;

                        preguntaFrecuente.PreguntaFrecuenteArea.Add(preguntaFrecuenteArea);
                    }

                    preguntaFrecuente.PreguntaFrecuenteSubArea = new List<PreguntaFrecuenteSubAreaBO>();

                    foreach (var item in Json.ListaSubAreas)
                    {
                        PreguntaFrecuenteSubAreaBO preguntaFrecuenteSubArea = new PreguntaFrecuenteSubAreaBO();
                        preguntaFrecuenteSubArea.IdPreguntaFrecuente = Json.PreguntaFrecuente.Id;
                        preguntaFrecuenteSubArea.IdSubArea = item;
                        preguntaFrecuenteSubArea.UsuarioCreacion = Json.Usuario;
                        preguntaFrecuenteSubArea.UsuarioModificacion = Json.Usuario;
                        preguntaFrecuenteSubArea.FechaCreacion = DateTime.Now;
                        preguntaFrecuenteSubArea.FechaModificacion = DateTime.Now;
                        preguntaFrecuenteSubArea.Estado = true;

                        preguntaFrecuente.PreguntaFrecuenteSubArea.Add(preguntaFrecuenteSubArea);
                    }

                    preguntaFrecuente.PreguntaFrecuenteTipo = new List<PreguntaFrecuenteTipoBO>();

                    foreach (var item in Json.ListaTipos)
                    {
                        PreguntaFrecuenteTipoBO preguntaFrecuenteTipo = new PreguntaFrecuenteTipoBO();
                        preguntaFrecuenteTipo.IdPreguntaFrecuente = Json.PreguntaFrecuente.Id;
                        preguntaFrecuenteTipo.IdTipo = item;
                        preguntaFrecuenteTipo.UsuarioCreacion = Json.Usuario;
                        preguntaFrecuenteTipo.UsuarioModificacion = Json.Usuario;
                        preguntaFrecuenteTipo.FechaCreacion = DateTime.Now;
                        preguntaFrecuenteTipo.FechaModificacion = DateTime.Now;
                        preguntaFrecuenteTipo.Estado = true;

                        preguntaFrecuente.PreguntaFrecuenteTipo.Add(preguntaFrecuenteTipo);
                    }

                    repPreguntaFrecuente.Insert(preguntaFrecuente);
                    scope.Complete();

                }

                return Ok(preguntaFrecuente);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarPreguntaFrecuente([FromBody] CompuestoPreguntaFrecuenteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PreguntaFrecuenteRepositorio repPreguntaFrecuente = new PreguntaFrecuenteRepositorio(contexto);
                PreguntaFrecuentePgeneralRepositorio repPreguntaFrecuentePgeneral = new PreguntaFrecuentePgeneralRepositorio(contexto);
                PreguntaFrecuenteAreaRepositorio repPreguntaFrecuenteArea = new PreguntaFrecuenteAreaRepositorio(contexto);
                PreguntaFrecuenteSubAreaRepositorio repPreguntaFrecuenteSubArea = new PreguntaFrecuenteSubAreaRepositorio(contexto);
                PreguntaFrecuenteTipoRepositorio repPreguntaFrecuenteTipo = new PreguntaFrecuenteTipoRepositorio(contexto);

                PreguntaFrecuenteBO preguntaFrecuente = new PreguntaFrecuenteBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (repPreguntaFrecuente.Exist(Json.PreguntaFrecuente.Id))
                    {
                        repPreguntaFrecuentePgeneral.EliminacionLogicoPorPreguntaFrecuente(Json.PreguntaFrecuente.Id, Json.Usuario, Json.ListaPGenerales);
                        repPreguntaFrecuenteArea.EliminacionLogicoPorPreguntaFrecuente(Json.PreguntaFrecuente.Id, Json.Usuario, Json.ListaAreas);
                        repPreguntaFrecuenteSubArea.EliminacionLogicoPorPreguntaFrecuente(Json.PreguntaFrecuente.Id, Json.Usuario, Json.ListaSubAreas);
                        repPreguntaFrecuenteTipo.EliminacionLogicoPorPreguntaFrecuente(Json.PreguntaFrecuente.Id, Json.Usuario, Json.ListaTipos);

                        preguntaFrecuente = repPreguntaFrecuente.FirstById(Json.PreguntaFrecuente.Id);
                        preguntaFrecuente.IdSeccionPreguntaFrecuente = Json.PreguntaFrecuente.IdSeccionPreguntaFrecuente;
                        preguntaFrecuente.Pregunta = Json.PreguntaFrecuente.Pregunta;
                        preguntaFrecuente.Respuesta = Json.PreguntaFrecuente.Respuesta;
                        preguntaFrecuente.Tipo = Json.PreguntaFrecuente.Tipo;
                        preguntaFrecuente.UsuarioModificacion = Json.Usuario;
                        preguntaFrecuente.FechaModificacion = DateTime.Now;

                        preguntaFrecuente.PreguntaFrecuentePgeneral = new List<PreguntaFrecuentePgeneralBO>();
                        foreach (var item in Json.ListaPGenerales)
                        {
                            PreguntaFrecuentePgeneralBO preguntaFrecuentePgeneral = new PreguntaFrecuentePgeneralBO();
                            if (repPreguntaFrecuentePgeneral.Exist(x => x.IdPgeneral == item && x.IdPreguntaFrecuente == Json.PreguntaFrecuente.Id))
                            {
                                preguntaFrecuentePgeneral = repPreguntaFrecuentePgeneral.FirstBy(x => x.IdPgeneral == item && x.IdPreguntaFrecuente == Json.PreguntaFrecuente.Id);
                                preguntaFrecuentePgeneral.IdPgeneral = item;
                                preguntaFrecuentePgeneral.UsuarioModificacion = Json.Usuario;
                                preguntaFrecuentePgeneral.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                preguntaFrecuentePgeneral = new PreguntaFrecuentePgeneralBO();
                                preguntaFrecuentePgeneral.IdPgeneral = item;
                                preguntaFrecuentePgeneral.UsuarioCreacion = Json.Usuario;
                                preguntaFrecuentePgeneral.UsuarioModificacion = Json.Usuario;
                                preguntaFrecuentePgeneral.FechaCreacion = DateTime.Now;
                                preguntaFrecuentePgeneral.FechaModificacion = DateTime.Now;
                                preguntaFrecuentePgeneral.Estado = true;
                            }

                            preguntaFrecuente.PreguntaFrecuentePgeneral.Add(preguntaFrecuentePgeneral);
                        }

                        preguntaFrecuente.PreguntaFrecuenteArea = new List<PreguntaFrecuenteAreaBO>();
                        foreach (var item in Json.ListaAreas)
                        {
                            PreguntaFrecuenteAreaBO preguntaFrecuenteArea = new PreguntaFrecuenteAreaBO();
                            if (repPreguntaFrecuenteArea.Exist(x => x.IdArea == item && x.IdPreguntaFrecuente == Json.PreguntaFrecuente.Id))
                            {
                                preguntaFrecuenteArea = repPreguntaFrecuenteArea.FirstBy(x => x.IdArea == item && x.IdPreguntaFrecuente == Json.PreguntaFrecuente.Id);
                                preguntaFrecuenteArea.IdArea = item;
                                preguntaFrecuenteArea.UsuarioModificacion = Json.Usuario;
                                preguntaFrecuenteArea.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                preguntaFrecuenteArea = new PreguntaFrecuenteAreaBO();
                                preguntaFrecuenteArea.IdArea = item;
                                preguntaFrecuenteArea.UsuarioCreacion = Json.Usuario;
                                preguntaFrecuenteArea.UsuarioModificacion = Json.Usuario;
                                preguntaFrecuenteArea.FechaCreacion = DateTime.Now;
                                preguntaFrecuenteArea.FechaModificacion = DateTime.Now;
                                preguntaFrecuenteArea.Estado = true;
                            }

                            preguntaFrecuente.PreguntaFrecuenteArea.Add(preguntaFrecuenteArea);
                        }

                        preguntaFrecuente.PreguntaFrecuenteSubArea = new List<PreguntaFrecuenteSubAreaBO>();
                        foreach (var item in Json.ListaSubAreas)
                        {
                            PreguntaFrecuenteSubAreaBO preguntaFrecuenteSubArea = new PreguntaFrecuenteSubAreaBO();
                            if (repPreguntaFrecuenteSubArea.Exist(x => x.IdSubArea == item && x.IdPreguntaFrecuente == Json.PreguntaFrecuente.Id))
                            {
                                preguntaFrecuenteSubArea = repPreguntaFrecuenteSubArea.FirstBy(x => x.IdSubArea == item && x.IdPreguntaFrecuente == Json.PreguntaFrecuente.Id);
                                preguntaFrecuenteSubArea.IdSubArea = item;
                                preguntaFrecuenteSubArea.UsuarioModificacion = Json.Usuario;
                                preguntaFrecuenteSubArea.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                preguntaFrecuenteSubArea = new PreguntaFrecuenteSubAreaBO();
                                preguntaFrecuenteSubArea.IdSubArea = item;
                                preguntaFrecuenteSubArea.UsuarioCreacion = Json.Usuario;
                                preguntaFrecuenteSubArea.UsuarioModificacion = Json.Usuario;
                                preguntaFrecuenteSubArea.FechaCreacion = DateTime.Now;
                                preguntaFrecuenteSubArea.FechaModificacion = DateTime.Now;
                                preguntaFrecuenteSubArea.Estado = true;
                            }

                            preguntaFrecuente.PreguntaFrecuenteSubArea.Add(preguntaFrecuenteSubArea);
                        }

                        preguntaFrecuente.PreguntaFrecuenteTipo = new List<PreguntaFrecuenteTipoBO>();
                        foreach (var item in Json.ListaTipos)
                        {
                            PreguntaFrecuenteTipoBO preguntaFrecuenteTipo = new PreguntaFrecuenteTipoBO();
                            if (repPreguntaFrecuenteTipo.Exist(x => x.IdTipo == item && x.IdPreguntaFrecuente == Json.PreguntaFrecuente.Id))
                            {
                                preguntaFrecuenteTipo = repPreguntaFrecuenteTipo.FirstBy(x => x.IdTipo == item && x.IdPreguntaFrecuente == Json.PreguntaFrecuente.Id);
                                preguntaFrecuenteTipo.IdTipo = item;
                                preguntaFrecuenteTipo.UsuarioModificacion = Json.Usuario;
                                preguntaFrecuenteTipo.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                preguntaFrecuenteTipo = new PreguntaFrecuenteTipoBO();
                                preguntaFrecuenteTipo.IdTipo = item;
                                preguntaFrecuenteTipo.UsuarioCreacion = Json.Usuario;
                                preguntaFrecuenteTipo.UsuarioModificacion = Json.Usuario;
                                preguntaFrecuenteTipo.FechaCreacion = DateTime.Now;
                                preguntaFrecuenteTipo.FechaModificacion = DateTime.Now;
                                preguntaFrecuenteTipo.Estado = true;
                            }

                            preguntaFrecuente.PreguntaFrecuenteTipo.Add(preguntaFrecuenteTipo);
                        }

                        repPreguntaFrecuente.Update(preguntaFrecuente);
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

        [Route("[action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarPreguntaFrecuente(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PreguntaFrecuenteRepositorio repPreguntaFrecuente = new PreguntaFrecuenteRepositorio();
                PreguntaFrecuentePgeneralRepositorio repPreguntaFrecuentePgeneral = new PreguntaFrecuentePgeneralRepositorio();
                PreguntaFrecuenteAreaRepositorio repPreguntaFrecuenteArea = new PreguntaFrecuenteAreaRepositorio();
                PreguntaFrecuenteSubAreaRepositorio repPreguntaFrecuenteSubArea = new PreguntaFrecuenteSubAreaRepositorio();
                PreguntaFrecuenteTipoRepositorio repPreguntaFrecuenteTipo = new PreguntaFrecuenteTipoRepositorio();

                if (repPreguntaFrecuente.Exist(Id))
                {
                    repPreguntaFrecuente.Delete(Id, Usuario);

                    var hijosPreguntaFrecuentePgeneral = repPreguntaFrecuentePgeneral.GetBy(x => x.IdPreguntaFrecuente == Id);
                    foreach (var hijo in hijosPreguntaFrecuentePgeneral)
                    {
                        repPreguntaFrecuentePgeneral.Delete(hijo.Id, Usuario);
                    }

                    var hijosPreguntaFrecuenteArea = repPreguntaFrecuenteArea.GetBy(x => x.IdPreguntaFrecuente == Id);
                    foreach (var hijo in hijosPreguntaFrecuenteArea)
                    {
                        repPreguntaFrecuenteArea.Delete(hijo.Id, Usuario);
                    }

                    var hijosPreguntaFrecuenteSubArea = repPreguntaFrecuenteSubArea.GetBy(x => x.IdPreguntaFrecuente == Id);
                    foreach (var hijo in hijosPreguntaFrecuenteSubArea)
                    {
                        repPreguntaFrecuenteSubArea.Delete(hijo.Id, Usuario);
                    }

                    var hijosPreguntaFrecuenteTipo = repPreguntaFrecuenteTipo.GetBy(x => x.IdPreguntaFrecuente == Id);
                    foreach (var hijo in hijosPreguntaFrecuenteTipo)
                    {
                        repPreguntaFrecuenteTipo.Delete(hijo.Id, Usuario);
                    }

                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }

    public class ValidadorPreguntaFrecuenteDTO : AbstractValidator<TPreguntaFrecuente>
    {
        public static ValidadorPreguntaFrecuenteDTO Current = new ValidadorPreguntaFrecuenteDTO();
        public ValidadorPreguntaFrecuenteDTO()
        {
            RuleFor(objeto => objeto.Pregunta).NotEmpty().WithMessage("Pregunta es Obligatorio")
                                            .Length(1, 100).WithMessage("Pregunta debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.Respuesta).NotEmpty().WithMessage("Respuesta es Obligatorio")
                                            .Length(1, 100).WithMessage("Respuesta debe tener 1 caracter minimo y 100 maximo");
        }
    }
}
