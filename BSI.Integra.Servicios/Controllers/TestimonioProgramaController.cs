using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TestimonioPrograma")]
    public class TestimonioProgramaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TestimonioProgramaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] TestimonioProgramaDTO testimonioPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TestimonioProgramaRepositorio _repTestimonioPrograma = new TestimonioProgramaRepositorio(_integraDBContext);

                TestimonioProgramaBO testimonioProgramaBO = _repTestimonioPrograma.FirstById(testimonioPrograma.Id);
                testimonioProgramaBO.IdPgeneral = testimonioPrograma.IdPgeneral;
                testimonioProgramaBO.CursoMoodleId = testimonioPrograma.CursoMoodleId;
                testimonioProgramaBO.UsuarioMoodleId = testimonioPrograma.UsuarioMoodleId;
                testimonioProgramaBO.Alumno = testimonioPrograma.Alumno;
                testimonioProgramaBO.Testimonio = testimonioPrograma.Testimonio;
                testimonioProgramaBO.Pregunta = testimonioPrograma.Pregunta;
                testimonioProgramaBO.Autoriza = testimonioPrograma.Autoriza;
                testimonioProgramaBO.UsuarioModificacion = testimonioPrograma.Usuario;
                testimonioProgramaBO.FechaModificacion = DateTime.Now;

                return Ok(_repTestimonioPrograma.Update(testimonioProgramaBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] TestimonioProgramaDTO testimonioPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    TestimonioProgramaRepositorio _repTestimonioPrograma = new TestimonioProgramaRepositorio(_integraDBContext);

                    if (_repTestimonioPrograma.Exist(testimonioPrograma.Id))
                    {
                        _repTestimonioPrograma.Delete(testimonioPrograma.Id, testimonioPrograma.Usuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoArea()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCapacitacionRepositorio _areaCapacitacionRepositorio = new AreaCapacitacionRepositorio(_integraDBContext);
                return Ok(_areaCapacitacionRepositorio.ObtenerTodoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoSubArea()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubAreaCapacitacionRepositorio _subAreaCapacitacionRepositorio = new SubAreaCapacitacionRepositorio(_integraDBContext);
                return Ok(_subAreaCapacitacionRepositorio.ObtenerTodoFiltroAutoSelect());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public ActionResult ObtenerTestimonioPorPGeneral(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TestimonioProgramaRepositorio _repTestimonioPrograma = new TestimonioProgramaRepositorio(_integraDBContext);
                return Ok(_repTestimonioPrograma.ObtenerTestimonioPorPGeneral(IdProgramaGeneral));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPGeneral()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralRepositorio _pgeneralRepositorio = new PgeneralRepositorio(_integraDBContext);
                return Ok(_pgeneralRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarTestimonioPrograma([FromBody] TestimonioProgramaCompuestoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    TestimonioProgramaRepositorio _repTestimonioPrograma = new TestimonioProgramaRepositorio(_integraDBContext);

                    foreach (var item in Json.ListaTestimoniosAsociados)
                    {
                        var testimomioPrograma = _repTestimonioPrograma.GetBy(x => x.Testimonio == item.Testimonio && x.Estado == true).ToList();

                        if(testimomioPrograma.Count() == 0)
                        {
                            TestimonioProgramaBO testimonioProgramaBO = new TestimonioProgramaBO();
                            testimonioProgramaBO.IdPgeneral = Json.IdProgramaGeneral;
                            testimonioProgramaBO.CursoMoodleId = item.CursoMoodleId;
                            testimonioProgramaBO.UsuarioMoodleId = item.UsuarioMoodleId;
                            testimonioProgramaBO.Alumno = item.Alumno;
                            testimonioProgramaBO.Testimonio = item.Testimonio;
                            testimonioProgramaBO.Pregunta = item.Pregunta;
                            testimonioProgramaBO.Autoriza = item.Autoriza;

                            testimonioProgramaBO.Estado = true;
                            testimonioProgramaBO.UsuarioCreacion = Json.Usuario;
                            testimonioProgramaBO.UsuarioModificacion = Json.Usuario;
                            testimonioProgramaBO.FechaCreacion = DateTime.Now;
                            testimonioProgramaBO.FechaModificacion = DateTime.Now;

                            _repTestimonioPrograma.Insert(testimonioProgramaBO);
                        }
                        
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerTodoTestimonioPrograma(int IdPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //TestimonioProgramaRepositorio _repTestimonioPrograma = new TestimonioProgramaRepositorio(_integraDBContext);
                TestimonioEncuestaRepositorio _repTestimonioEncuesta = new TestimonioEncuestaRepositorio();
                MatriculaAlumnoMoodleRepositorio _repMatriculaAlumnoMoodle = new MatriculaAlumnoMoodleRepositorio(_integraDBContext);
                var listaCursos = _repMatriculaAlumnoMoodle.ObtenerTodoDatoTestimonioPrograma(IdPGeneral);
                var listaIdCursos = listaCursos.Select(x => x.IdCursoMoodle ).ToList();
                return Ok(_repTestimonioEncuesta.ObtenerTodoAlumnoTestimonios(listaIdCursos));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdCurso}/{IdUsuario}")]
        [HttpGet]
        public ActionResult ObtenerTodoTestimonioAlumno(int IdCurso, int IdUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //TestimonioProgramaRepositorio _repTestimonioPrograma = new TestimonioProgramaRepositorio(_integraDBContext);
                TestimonioEncuestaRepositorio _repTestimonioEncuesta = new TestimonioEncuestaRepositorio();
                return Ok(_repTestimonioEncuesta.ObtenerTodoTestimonios(IdCurso, IdUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadoTestimonioProgramalDTO : AbstractValidator<TTestimonioPrograma>
    {
        public static ValidadoTestimonioProgramalDTO Current = new ValidadoTestimonioProgramalDTO();
        public ValidadoTestimonioProgramalDTO()
        {
        }
    }
}
