using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models.AulaVirtual;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TestimonioEncuestaRepositorio
    {
        /// <summary>
        /// Se obtiene registros  UsuarioId, CursoId, Titulo,Alumno, Autoriza filtrado por el curso
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public List<TestimonioProgramaMoodleDTO> ObtenerTodoAlumnoTestimonios(List<int> idCurso)
        {
            try
            {
                List<long> listadoIdCurso = new List<long>();
                idCurso.ForEach(f => { listadoIdCurso.Add(long.Parse(f.ToString())); });

                AulaVirtualContext contexto = new AulaVirtualContext();
                List<TestimonioProgramaMoodleDTO> obtenerTestimonioPrograma = new List<TestimonioProgramaMoodleDTO>();
                var _query = (from respuestaTexto in contexto.MdlQuestionnaireResponseText
                              join respuesta in contexto.MdlQuestionnaireResponse on respuestaTexto.ResponseId equals respuesta.Id
                              join encuesta in contexto.MdlQuestionnaireSurvey on respuesta.SurveyId equals encuesta.Id
                              join cuestionario in contexto.MdlQuestionnaire on encuesta.Id equals cuestionario.Sid
                              join usuario in contexto.MdlUser on respuesta.Username equals usuario.Id.ToString()
                              join respuestaBool in contexto.MdlQuestionnaireResponseBool on respuesta.Id equals respuestaBool.ResponseId

                              where encuesta.Title.Contains("Testimonio") && respuesta.Complete.Contains("y") && listadoIdCurso.Contains(cuestionario.Course)
                              select new TestimonioProgramaMoodleDTO()
                              {
                                  UsuarioId = usuario.Id,
                                  CursoId = cuestionario.Course,
                                  Titulo = encuesta.Title,
                                  Alumno = usuario.Firstname + " " + usuario.Lastname,
                                  Autoriza = respuestaBool.ChoiceId.Contains("y") ? "Si" : "No"
                              }).ToList();
                return _query;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene registros de UsuarioId, CursoId, Respuesta, Completo, Pregunta filtrado por el curso
        /// </summary>
        /// <param name="idCurso"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<TestimonioAlumnoMoodleDTO> ObtenerTodoTestimonios(int idCurso, int idUsuario)
        {
            try
            {
                AulaVirtualContext contexto = new AulaVirtualContext();
                List<TestimonioAlumnoMoodleDTO> obtenerTestimonioAlumno = new List<TestimonioAlumnoMoodleDTO>();
                var _query = (from respuestaTexto in contexto.MdlQuestionnaireResponseText
                              join respuesta in contexto.MdlQuestionnaireResponse on respuestaTexto.ResponseId equals respuesta.Id
                              join encuesta in contexto.MdlQuestionnaireSurvey on respuesta.SurveyId equals encuesta.Id
                              join cuestionario in contexto.MdlQuestionnaire on encuesta.Id equals cuestionario.Sid
                              join usuario in contexto.MdlUser on respuesta.Username equals usuario.Id.ToString()
                              join pregunta in contexto.MdlQuestionnaireQuestion on respuestaTexto.QuestionId equals pregunta.Id

                              where encuesta.Title.Contains("Testimonio") &&
                                    respuesta.Complete.Contains("y") && 
                                    (pregunta.Name.Contains( "¿Qué beneficio pudo obtener") || pregunta.Name.Contains("¿Cuál cree que es la principal")) &&
                                    cuestionario.Course.Equals(idCurso) &&
                                    usuario.Id.Equals(idUsuario)

                            select new TestimonioAlumnoMoodleDTO()
                            {
                                UsuarioId = usuario.Id,
                                CursoId = cuestionario.Course,
                                Respuesta = respuestaTexto.Response,
                                Completo = respuesta.Complete,
                                Pregunta = ((pregunta.Name.Contains("¿Qué beneficio pudo obtener") ? "¿Qué beneficio pudo obtener a partir de su participación en el programa?" : pregunta.Name.Contains("¿Cuál cree que es la principal") ? "¿Cuál cree que es la principal fortaleza del programa?" : "NULL"))
                            }).ToList();
                return _query;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
