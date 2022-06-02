using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/NotaAulaVirtual")]
    [ApiController]
    public class NotaAulaVirtualOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Buscar(string CodigoAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(CodigoAlumno))
                {
                    return BadRequest("No se envío el código del alumno.");
                }
                CodigoAlumno = CodigoAlumno.Trim();
                RaAlumnoRepositorio _repRaAlumno = new RaAlumnoRepositorio();
                ReporteNotaRepositorio _repReporteNota = new ReporteNotaRepositorio();
                AutoevaluacionPorCursoMoodleRepositorio _repAutoevaluacionPorCursoMoodle = new AutoevaluacionPorCursoMoodleRepositorio();
                CursoMoodleRepositorio _repCursoMoodle = new CursoMoodleRepositorio();
                List<ListadoNotaAulaVirtualDTO> listadoDatosInicial = new List<ListadoNotaAulaVirtualDTO>();
                var alumno = _repRaAlumno.ObtenerAlumnoPorCodigoMatricula(CodigoAlumno);

                if (alumno == null)
                {
                    return BadRequest("No se pudo encontrar al alumno por el código enviado o el alumno no pertenece a la modalidad Presencial.");
                }
                if (alumno.NombreCentrocosto.Contains("ONLINE") || alumno.NombreCentrocosto.Contains("INSTITUTO"))
                {
                    return BadRequest("El alumno no pertenece a la modalidad Presencial.");
                }
                var usuarioMoodle = _repRaAlumno.ObtenerIdUsuarioMoodlePorCodigoAlumno(CodigoAlumno);
                if (usuarioMoodle == null)
                {
                    return BadRequest("No se puede determinar el Usuario de Moodle asociado, favor consultar con el área respectiva.");
                }

                //Logica para obtener todo el listado de notas de aula virtual
                NotaAulaVirtualDTO notaAulaVirtual = new NotaAulaVirtualDTO();
                var listadoMatriculas = _repRaAlumno.ObtenerMatriculasPorCodigoMatricula(alumno.CodigoAlumno, usuarioMoodle.IdUsuarioMoodle);
                foreach (var matricula in listadoMatriculas)
                {
                    List<int> listadoIdCurso = listadoMatriculas.Select(s => s.IdCursoMoodle).ToList();
                    var listadoNotasMoodle = _repReporteNota.ObtenerNotasPorUsuarioMoodleCurso(usuarioMoodle.IdUsuarioMoodle, listadoIdCurso);
                    foreach (var autoEvaluacion in _repAutoevaluacionPorCursoMoodle.ObtenerPorCurso(matricula.IdCursoMoodle))
                    {
                        var autoEvaluacionTemp = new ListadoNotaAulaVirtualDTO
                        {
                            CodigoAlumno = CodigoAlumno,
                            IdCursoMoodle = (int)autoEvaluacion.IdCurso.Value,
                            Curso = autoEvaluacion.NombreCurso,
                            IdEvaluacionMoodle = (int)autoEvaluacion.IdEvaluacion.Value,
                            Evaluacion = autoEvaluacion.Evaluacion,
                            IdUsuarioMoodle = usuarioMoodle.IdUsuarioMoodle,
                            IdSeccion = autoEvaluacion.IdSeccion.Value,
                            Seccion = autoEvaluacion.NombreSeccion
                        };
                        var nota = listadoNotasMoodle.FirstOrDefault(w => w.IdCursoMoodle == autoEvaluacion.IdCurso && w.NombreAutoevaluacion == autoEvaluacion.Evaluacion);
                        if (nota != null)
                        {
                            autoEvaluacionTemp.EscalaCalificacion = nota.EscalaCalificacion;
                            if (nota.Fecha != "Pendiente")
                            {
                                if (DateTime.TryParse(nota.Fecha, out DateTime fechaConvertida))
                                {
                                    autoEvaluacionTemp.Fecha = fechaConvertida;
                                }
                            }
                            //evalua si la nota en no pendiente
                            if (nota.Nota != "Pendiente")
                            {
                                if (decimal.TryParse(nota.Nota, out decimal notaConvertida))
                                {
                                    autoEvaluacionTemp.Nota = notaConvertida;
                                }
                            }
                        }

                        //determina el tipo de curso
                        if (_repCursoMoodle.Exist(w => w.IdCategoriaMoodle == 5 && w.IdCursoMoodle == autoEvaluacion.IdCurso))
                        {
                            autoEvaluacionTemp.TipoCurso = "Presencial";
                        }
                        else {
                            autoEvaluacionTemp.TipoCurso = "AONLINE";
                        }
                        listadoDatosInicial.Add(autoEvaluacionTemp);
                    }
                }

                notaAulaVirtual.DatoAlumno = new AlumnoNotaAulaVirtualDTO()
                {
                    CentroCosto = alumno.NombreCentrocosto,
                    CodigoAlumno = alumno.CodigoAlumno,
                    NombreCompleto = alumno.NombreAlumno
                };

                if (listadoDatosInicial != null && listadoDatosInicial.Count > 0)
                {
                    foreach (var curso_agrupado in listadoDatosInicial.OrderBy(o => o.TipoCurso).ThenBy(o => o.Curso).GroupBy(g => g.IdCursoMoodle))
                    {
                        var primerCursoAgrupado = listadoDatosInicial.FirstOrDefault(w => w.IdCursoMoodle == curso_agrupado.Key);

                        var primerCurso = new DatoNotaAulaVirtualDTO()
                        {
                            IdCurso = primerCursoAgrupado.IdCursoMoodle,
                            NombreCurso = primerCursoAgrupado.Curso,
                            TipoCurso = primerCursoAgrupado.TipoCurso,
                            Promedio = 0,
                            ListadoEvaluaciones = new List<DatoEvaluacionNotaAulaVirtualDTO>()
                        };

                        if (primerCursoAgrupado.TipoCurso == "Presencial")
                        {
                            foreach (var seccion_agrupado in listadoDatosInicial.Where(w => w.IdCursoMoodle == curso_agrupado.Key).GroupBy(g => g.IdSeccion))
                            {
                                var primera_seccion_agrupado = listadoDatosInicial.FirstOrDefault(w => w.IdCursoMoodle == curso_agrupado.Key && w.IdSeccion == seccion_agrupado.Key);
                                var listado_evaluaciones = listadoDatosInicial.Where(w => w.IdCursoMoodle == curso_agrupado.Key && w.IdSeccion == seccion_agrupado.Key).Where(w => w.Evaluacion.Contains("utoevaluac")).OrderBy(o => o.Evaluacion);
                                decimal sumatoria = 0;
                                int total_evaluaciones = listado_evaluaciones.Count();
                                decimal promedio = 0;
                                primerCurso.Seccion = primera_seccion_agrupado.Seccion;
                                foreach (var evaluacion in listado_evaluaciones)
                                {
                                    //incrementa la sumatoria
                                    if (evaluacion.Nota != null)
                                    {
                                        sumatoria += evaluacion.Nota.Value;
                                    }
                                    var datoTemp = new DatoEvaluacionNotaAulaVirtualDTO()
                                    {
                                        IdEvaluacion = evaluacion.IdEvaluacionMoodle,
                                        NombreEvaluacion = evaluacion.Evaluacion,
                                        EscalaCalificacion = evaluacion.EscalaCalificacion,
                                        Fecha = evaluacion.Fecha,
                                        Nota = evaluacion.Nota
                                    };
                                    if (total_evaluaciones > 0)
                                    {
                                        promedio = Math.Round(Convert.ToDecimal(Convert.ToDouble(sumatoria) / (total_evaluaciones * 1.00)), 2, MidpointRounding.AwayFromZero);
                                    }
                                    primerCurso.ListadoEvaluaciones.Add(datoTemp);
                                }
                                primerCurso.Promedio = promedio;
                                notaAulaVirtual.ListadoNotas.Add(primerCurso);
                            }
                        }

                        if (primerCursoAgrupado.TipoCurso == "AONLINE")
                        {
                            var listado_evaluaciones = listadoDatosInicial.Where(w => w.IdCursoMoodle == curso_agrupado.Key).Where(w => w.Evaluacion.Contains("utoevaluac")).OrderBy(o => o.Evaluacion);
                            decimal sumatoria = 0;
                            int totalEvaluaciones = listado_evaluaciones.Count();
                            decimal promedio = 0;

                            foreach (var evaluacion in listado_evaluaciones)
                            {
                                if (evaluacion.Nota != null)
                                {
                                    sumatoria += evaluacion.Nota.Value;
                                };
                                var datoTemp = new DatoEvaluacionNotaAulaVirtualDTO()
                                {
                                    IdEvaluacion = evaluacion.IdEvaluacionMoodle,
                                    NombreEvaluacion = evaluacion.Evaluacion,
                                    EscalaCalificacion = evaluacion.EscalaCalificacion,
                                    Fecha = evaluacion.Fecha == null ? evaluacion.Fecha.Value : evaluacion.Fecha,
                                    Nota = evaluacion.Nota
                                };
                                if (totalEvaluaciones > 0)
                                {
                                    promedio = Math.Round(Convert.ToDecimal(Convert.ToDouble(sumatoria) / (totalEvaluaciones * 1.00)), 2, MidpointRounding.AwayFromZero);
                                };
                                primerCurso.Promedio = promedio;
                                primerCurso.ListadoEvaluaciones.Add(datoTemp);
                                notaAulaVirtual.ListadoNotas.Add(primerCurso);
                            }
                        }
                    }
                    return Ok(notaAulaVirtual);
                }
                else {
                    return BadRequest("No se encontraron datos de notas!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
