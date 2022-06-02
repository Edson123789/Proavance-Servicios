using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Clases;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/GestionAsistenciaOnline")]
    public class GestionAsistenciaOnlineOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListadoEnvio()
        {
            try
            {
                RaCentroCostoRepositorio _repRaCentroCosto = new RaCentroCostoRepositorio();
                var listadoCentroCostoConAsistencia = _repRaCentroCosto.ListadoCentroCostoConAsistenciaMensual();
                var numerador = 1;
                foreach (var item in listadoCentroCostoConAsistencia)
                {
                    item.Numeracion = numerador;
                    numerador++;
                }

                return Ok(listadoCentroCostoConAsistencia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdRaCentroCosto}")]
        [HttpGet]
        public ActionResult EnviarCorreoAsistencia(int IdRaCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaAlumnoRepositorio _repRaAlumno = new RaAlumnoRepositorio();
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();
                RaCentroCostoRepositorio _repRaCentroCosto = new RaCentroCostoRepositorio();
                ReporteBO reporteBO = new ReporteBO();

                int success = 0;
                int error = 0;
                var centroCosto = _repRaCentroCosto.FirstById(IdRaCentroCosto).NombreCentroCosto;
                string mensajeRespuesta = "";

                List<AlumnoPresencialDTO> listadoAlumnos = _repRaAlumno.ListadoAlumnoRegistradoPorCentroCosto(centroCosto);
                if (listadoAlumnos != null)
                {
                    listadoAlumnos = listadoAlumnos.Where(w => w.IdEstadomatricula == ValorEstatico.IdEstadoMatriculaRegular || w.IdEstadomatricula == ValorEstatico.IdEstadoMatriculaBeca || w.IdEstadomatricula == ValorEstatico.IdEstadoMatriculaReincorporado).ToList();
                    foreach (var alumno in listadoAlumnos)
                    {
                        CoordinadoraBO coordinadora = _repCoordinadora.ObtenerCoordinador(alumno.UsuarioCoordinadorAcademico);
                        if (coordinadora != null)
                        {
                            //crea objeto de ExcelBLL
                            List<ReporteAsistenciaOnlineDTO> listadoSesionesAsistencia = new List<ReporteAsistenciaOnlineDTO>();
                            byte[] reporteAsistenciaExcel = reporteBO.ObtenerReporteDetallaPorAlumnoAsistenciaOnlineExcel(alumno.IdRaAlumno, ref listadoSesionesAsistencia);

                            //Envio correo
                            EmailPresencial email = new EmailPresencial();
                            RespuestaWebDTO respuesta = email.EnviarAsistenciaDetalladaOnline(alumno, coordinadora, reporteAsistenciaExcel, listadoSesionesAsistencia);

                            if (respuesta.Estado == true) {
                                success++;
                            }
                            else
                            {
                                error++;
                                mensajeRespuesta = mensajeRespuesta + Environment.NewLine + " Codigo alumno: " + alumno.CodigoAlumno + " Nombre alumno: " + alumno.NombreAlumno;
                            }
                        }
                        else
                        {
                            mensajeRespuesta = "No existe Coordinadora";
                        }
                    }

                    //Calculo retroalimentacion de la respuesta al usuario segun el estado del envio
                    if (success >= 1 && error >= 1)
                    {
                        mensajeRespuesta = "Solo se enviaron " + success + "/" + (success + error) + " correos. " + mensajeRespuesta;
                    }
                    else
                    {
                        if (success == 0)
                        {
                            mensajeRespuesta = "No se envió ningún Correo";
                        }
                        else
                        {
                            mensajeRespuesta = "Se enviaron todos los correos exitosamente";
                        }
                    }
                }
                else
                {
                    mensajeRespuesta = "No existen Alumnos";
                }

                return Ok( mensajeRespuesta );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}