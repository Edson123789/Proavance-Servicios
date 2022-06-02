using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/DocumentacionAlumno")]
    [ApiController]
    public class DocumentacionAlumnoOpeController : Controller
    {
        //private readonly string rutaPlantilla = new HostingEnvironment().WebRootPath + "Content/Attachments/Coordinador/Documentacion/Formato/";
        private readonly string rutaPlantilla = new HostingEnvironment().WebRootPath + @"C:\Proyectos\Servicios\BSI.Integra.Servicios\Content/Attachments/Coordinador/Documentacion/Formato/";

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrado(string TextoBuscar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(TextoBuscar))
                {
                    return Ok();
                }
                else {
                    RaAlumnoRepositorio _repAlumnoRepositorio = new RaAlumnoRepositorio();
                    return Ok(_repAlumnoRepositorio.BusquedaContieneCodigoMatriculaOCentroCosto(TextoBuscar));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFormatoPorCentroCosto(string NombreCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCoordinadorDocumentacionAlumnoRepositorio _repCoordinadorDocumentacionAlumnoRepositorio = new RaCoordinadorDocumentacionAlumnoRepositorio();
                if (!string.IsNullOrEmpty(NombreCentroCosto))
                {
                    return Ok(_repCoordinadorDocumentacionAlumnoRepositorio.ListadoAlumnosSinMatriculaVerificadaPorCentroCosto(NombreCentroCosto.Trim()));
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult DescargarFormatoConvenio(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCoordinadorDocumentacionAlumnoRepositorio _repCoordinadorDocumentacionAlumnoRepositorio = new RaCoordinadorDocumentacionAlumnoRepositorio();
                RaCoordinadorDocumentacionAlumnoBO raCoordinadorDocumentacionAlumnoBO = new RaCoordinadorDocumentacionAlumnoBO();
                if (!string.IsNullOrEmpty(CodigoMatricula)) {
                    var alumno = _repCoordinadorDocumentacionAlumnoRepositorio.AlumnoSinMatriculaVerificadaPorCodigo(CodigoMatricula.Trim());
                    if (alumno != null )
                    {
                        byte[] reportesesionesPorSemana = raCoordinadorDocumentacionAlumnoBO.ObtenerFormatoConvenio(alumno, rutaPlantilla);

                        if (reportesesionesPorSemana != null)
                        {
                            return File(reportesesionesPorSemana, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Convenio - " + alumno.NombreAlumno + " - " + alumno.CodigoMatricula + ".docx");
                        }
                        else {
                            return BadRequest("Ocurrió un error al generar el documento solicitado.");
                        }
                    }
                    else
                    {
                        return BadRequest("No se pudo obtener la información del alumno.");
                    }
                }
                else {
                    return BadRequest("Ocurrio un error!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult DescargarFormatoPagare(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCoordinadorDocumentacionAlumnoRepositorio _repCoordinadorDocumentacionAlumnoRepositorio = new RaCoordinadorDocumentacionAlumnoRepositorio();
                RaCoordinadorDocumentacionAlumnoBO raCoordinadorDocumentacionAlumnoBO = new RaCoordinadorDocumentacionAlumnoBO();
                if (!string.IsNullOrEmpty(CodigoMatricula))
                {
                    var alumno = _repCoordinadorDocumentacionAlumnoRepositorio.AlumnoSinMatriculaVerificadaPorCodigo(CodigoMatricula.Trim());
                    if (alumno != null)
                    {
                        byte[] reportesesionesPorSemana = raCoordinadorDocumentacionAlumnoBO.ObtenerFormatoPagare(alumno, rutaPlantilla);

                        if (reportesesionesPorSemana != null)
                        {
                            return File(reportesesionesPorSemana, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Convenio - " + alumno.NombreAlumno + " - " + alumno.CodigoMatricula + ".docx");
                        }
                        else
                        {
                            return BadRequest("Ocurrió un error al generar el documento solicitado.");
                        }
                    }
                    else
                    {
                        return BadRequest("No se pudo obtener la información del alumno.");
                    }
                }
                else
                {
                    return BadRequest("Ocurrio un error!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult DescargarFormatoFichaMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCoordinadorDocumentacionAlumnoRepositorio _repCoordinadorDocumentacionAlumnoRepositorio = new RaCoordinadorDocumentacionAlumnoRepositorio();
                RaCoordinadorDocumentacionAlumnoBO raCoordinadorDocumentacionAlumnoBO = new RaCoordinadorDocumentacionAlumnoBO();
                if (!string.IsNullOrEmpty(CodigoMatricula))
                {
                    var alumno = _repCoordinadorDocumentacionAlumnoRepositorio.AlumnoSinMatriculaVerificadaPorCodigo(CodigoMatricula.Trim());
                    if (alumno != null)
                    {
                        byte[] reportesesionesPorSemana = raCoordinadorDocumentacionAlumnoBO.ObtenerFormatoFichaMatricula(alumno, rutaPlantilla);

                        if (reportesesionesPorSemana != null)
                        {
                            return File(reportesesionesPorSemana, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Convenio - " + alumno.NombreAlumno + " - " + alumno.CodigoMatricula + ".docx");
                        }
                        else
                        {
                            return BadRequest("Ocurrió un error al generar el documento solicitado.");
                        }
                    }
                    else
                    {
                        return BadRequest("No se pudo obtener la información del alumno.");
                    }
                }
                else
                {
                    return BadRequest("Ocurrio un error!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
