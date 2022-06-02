using BSI.Integra.Aplicacion.DTOs;
using CsvHelper;
using Google.Api.Ads.Common.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ImportarAutoEvaluacionPreguntaController
    /// Autor: Abelson Quiñones Gutiérrez
    /// Fecha: 05/06/2021
    /// <summary>
    /// Contiene los controladores necesarios para importar preguntas de autoevaluacion segun el formato en CSV cargado
    /// </summary>
    [Route("api/ImportarAutoEvaluacionPregunta")]
    public class ImportarAutoEvaluacionPreguntaController : Controller
    {
        /// Tipo Función: GET
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 05/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Importar preguntas de Autoevaluaciones de docentes del PortalWeb y devolver un objeto con las preguntas y respuestas
        /// </summary>
        /// <param name="files">Archivo CSV que contiene la preguntas de las autoevaluaciones a cargar</param>
        /// <returns>Lista de los nombres en un List<RegistroAutoevaluacionPreguntaDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult MostrarPreguntasImportadas([FromForm] IFormFile files)
        {
            var ListaImportado = new List<ImportarPreguntasAutoevaluacionDTO>();
            CsvFile file = new CsvFile();
            try
            {
                int index = 0;
                using (var cvs = new CsvReader(new StreamReader(files.OpenReadStream())))
                {
                    cvs.Configuration.Delimiter = ";";
                    cvs.Configuration.MissingFieldFound = null;
                    cvs.Configuration.BadDataFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        index++;
                        ImportarPreguntasAutoevaluacionDTO pregunta = new ImportarPreguntasAutoevaluacionDTO();
                        pregunta.PREGUNTA = cvs.GetField<string>("PREGUNTA");
                        pregunta.TIPOPREGUNTA = cvs.GetField<int>("TIPOPREGUNTA") == 2 ? 12:5;
                        pregunta.TIEMPOLIMITE = cvs.GetField<int>("TIEMPOLIMITE");
                        pregunta.RESPUESTAALEATORIA = cvs.GetField<int>("RESPUESTAALEATORIA") == 1? true:false;
                        pregunta.INTENTOABIERTO = cvs.GetField<int>("INTENTOABIERTO");
                        pregunta.PUNTAJEABIERTO = cvs.GetField<int>("PUNTAJEABIERTO");
                        pregunta.INTENTOCERRADA= cvs.GetField<int>("INTENTOCERRADA");
                        pregunta.PUNTAJECERRADA = cvs.GetField<int>("PUNTAJECERRADA");
                        pregunta.RESPUESTAORDEN = cvs.GetField<int>("RESPUESTAORDEN");
                        pregunta.RESPUESTADESCRIPCION = cvs.GetField<string>("RESPUESTADESCRIPCION") == "" ? "" : cvs.GetField<string>("RESPUESTADESCRIPCION");
                        pregunta.RESPUESTACORRECTA = cvs.GetField<string>("RESPUESTACORRECTA")==""?false: cvs.GetField<int>("RESPUESTACORRECTA") == 1?true:false;
                        pregunta.RESPUESTAFEEDBACK = cvs.GetField<string>("RESPUESTAFEEDBACK")==""?"": cvs.GetField<string>("RESPUESTAFEEDBACK");

                        RegistroAutoevaluacionRespuestaDTO respuesta = new RegistroAutoevaluacionRespuestaDTO();
                        respuesta.Orden= cvs.GetField<int>("RESPUESTAORDEN");
                        respuesta.Descripcion = cvs.GetField<string>("RESPUESTADESCRIPCION");
                        respuesta.Correcta = cvs.GetField<string>("RESPUESTACORRECTA")== "" ? false : cvs.GetField<int>("RESPUESTACORRECTA") == 1 ? true : false; 

                        ListaImportado.Add(pregunta);
                    }
                }

                var ListaPreguntas = new List<RegistroAutoevaluacionPreguntaDTO>();

                var preguntas = from pregunta in ListaImportado
                                group pregunta by pregunta.PREGUNTA into newGroup
                                orderby newGroup.Key
                                select newGroup;

                foreach (var grupoPregunta in preguntas)
                {
                    List<RegistroAutoevaluacionIntentoDTO> listaIntento = new List<RegistroAutoevaluacionIntentoDTO>();
                    List<RegistroAutoevaluacionRespuestaDTO> listaRespuesta = new List<RegistroAutoevaluacionRespuestaDTO>();


                    var importado = ListaImportado.Find(x => x.PREGUNTA == grupoPregunta.Key);
                    //crearpregunta
                    RegistroAutoevaluacionPreguntaDTO preguntaNueva = new RegistroAutoevaluacionPreguntaDTO();
                    preguntaNueva.IdPreguntaTipo = importado.TIPOPREGUNTA;
                    preguntaNueva.Descripcion = importado.PREGUNTA;
                    preguntaNueva.Tiempo = importado.TIEMPOLIMITE;
                    preguntaNueva.RespuestaAleatoria = importado.RESPUESTAALEATORIA;
                    if (importado.INTENTOABIERTO== 0)
                    {
                        //Agregar lista de intentos
                        var intentos = from intento in grupoPregunta
                                       group intento by intento.INTENTOABIERTO into newIntento
                                       orderby newIntento.Key
                                       select newIntento;
                        foreach(var grupoIntento in intentos)
                        {
                            foreach(var intento in grupoIntento)
                            {
                                if (intento.INTENTOCERRADA != 0)
                                {
                                    RegistroAutoevaluacionIntentoDTO objIntento = new RegistroAutoevaluacionIntentoDTO();
                                    objIntento.Intento = intento.INTENTOCERRADA;
                                    objIntento.Puntaje = intento.PUNTAJECERRADA;

                                    listaIntento.Add(objIntento);
                                }
                            }
                        }

                    }
                    else
                    {
                        preguntaNueva.NumeroIntento = importado.INTENTOABIERTO;
                        preguntaNueva.Puntaje = importado.PUNTAJEABIERTO;
                    }


                    var respuestas = from respuesta in grupoPregunta
                                     group respuesta by respuesta.RESPUESTADESCRIPCION into newRespuestas
                                     orderby newRespuestas.Key
                                     select newRespuestas;
                    
                    foreach (var gruporespuesta in respuestas)
                    {
                        foreach(var respuesta in gruporespuesta)
                        {
                            if (respuesta.RESPUESTADESCRIPCION != "")
                            {
                                RegistroAutoevaluacionRespuestaDTO objRespuesta = new RegistroAutoevaluacionRespuestaDTO();
                                objRespuesta.Orden = respuesta.RESPUESTAORDEN;
                                objRespuesta.Descripcion = respuesta.RESPUESTADESCRIPCION;
                                objRespuesta.Correcta = respuesta.RESPUESTACORRECTA;
                                objRespuesta.Feedback = respuesta.RESPUESTAFEEDBACK;

                                listaRespuesta.Add(objRespuesta);
                            }
                        }
                    }

                    if (listaIntento.Count > 0)
                    {
                        preguntaNueva.listaIntentoAutoevaluacion = listaIntento;
                    }
                    preguntaNueva.listaRespuestasAutoEvaluacion = listaRespuesta;

                    ListaPreguntas.Add(preguntaNueva);
                }

                //int i = ListaPreguntas.IndexOf(pregunta);
                //if (i != -1)
                //{
                //    ListaPreguntas[i].listaRespuestasAutoEvaluacion.Add(respuesta);
                //}
                //else
                //{
                //    pregunta.listaRespuestasAutoEvaluacion.Add(respuesta);
                //    ListaPreguntas.Add(pregunta);
                //}


                var Nregistros = index;
                return Ok(new { ListaPreguntas, Nregistros });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
