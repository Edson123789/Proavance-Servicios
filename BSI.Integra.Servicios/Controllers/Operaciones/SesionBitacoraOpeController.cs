using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/SesionBitacora")]
    public class SesionBitacoraOpeController : Controller
    {

        //[Route("[Action]")]
        //[HttpGet]
        //public ActionResult Obtener()
        //{
        //    try
        //    {
        //        RaSesionBitacoraRepositorio _repSesionBitacora = new RaSesionBitacoraRepositorio();
        //        return Ok(_repSesionBitacora.GetBy(x => x.Estado, x => new { x.Id, x.UsuarioCreacion, x.UsuarioModificacion, x.FechaCreacion, x.FechaModificacion }));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [Route("[Action]")]
        [HttpGet]   
        public ActionResult ObtenerFiltrado(DateTime? Fecha, int? IdRaCentroCosto, int? IdRaCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaSesionBitacoraRepositorio _repSesionBitacora = new RaSesionBitacoraRepositorio();
                if (Fecha == null && IdRaCentroCosto == null && IdRaCurso == null) {
                    Fecha = DateTime.Now;
                }
                return Ok(_repSesionBitacora.ObtenerListadoFiltradoCabecera(Fecha, IdRaCentroCosto, IdRaCurso));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{IdRaCentroCosto}/{IdRaCurso}")]
        [HttpGet]
        public ActionResult ObtenerFiltradoDetalle(int IdRaCentroCosto, int IdRaCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaSesionBitacoraRepositorio _repSesionBitacora = new RaSesionBitacoraRepositorio();
                return Ok(_repSesionBitacora.ObtenerListadoFiltradoDetalle(IdRaCentroCosto, IdRaCurso));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CertificadoBrochureDTO CertificadoBrochure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCertificadoBrochureRepositorio _repCertificadoBrochure = new RaCertificadoBrochureRepositorio();
                RaCertificadoBrochureBO raCertificadoBrochureBO = new RaCertificadoBrochureBO()
                { 
                    Nombre = CertificadoBrochure.Nombre,
                    NombreEnCertificado = CertificadoBrochure.NombreEnCertificado,
                    TotalHoras = CertificadoBrochure.TotalHoras,
                    Contenido = CertificadoBrochure.Contenido,
                    Activo = CertificadoBrochure.Activo,
                    IdRaCertificadoBrochure = CertificadoBrochure.IdCertificadoBrochure,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = CertificadoBrochure.NombreUsuario,
                    UsuarioModificacion = CertificadoBrochure.NombreUsuario
                };
                if (!raCertificadoBrochureBO.HasErrors)
                {
                    _repCertificadoBrochure.Insert(raCertificadoBrochureBO);
                }
                else
                {
                    return BadRequest(raCertificadoBrochureBO.ActualesErrores);
                }
                return Ok(new { raCertificadoBrochureBO });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CertificadoBrochureDTO CertificadoBrochure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCertificadoBrochureRepositorio _repCertificadoBrochure = new RaCertificadoBrochureRepositorio();
                if (_repCertificadoBrochure.Exist(CertificadoBrochure.Id))
                {
                    var raCertificadoBrochureBO = _repCertificadoBrochure.FirstById(CertificadoBrochure.Id);
                    raCertificadoBrochureBO.Nombre = CertificadoBrochure.Nombre;
                    raCertificadoBrochureBO.NombreEnCertificado = CertificadoBrochure.NombreEnCertificado;
                    raCertificadoBrochureBO.TotalHoras = CertificadoBrochure.TotalHoras;
                    raCertificadoBrochureBO.Contenido = CertificadoBrochure.Contenido;
                    raCertificadoBrochureBO.Activo = CertificadoBrochure.Activo;
                    raCertificadoBrochureBO.IdRaCertificadoBrochure = CertificadoBrochure.IdCertificadoBrochure;
                    raCertificadoBrochureBO.FechaModificacion = DateTime.Now;
                    raCertificadoBrochureBO.UsuarioModificacion = CertificadoBrochure.NombreUsuario;
                    if (!raCertificadoBrochureBO.HasErrors)
                    {
                        _repCertificadoBrochure.Update(raCertificadoBrochureBO);
                    }
                    else
                    {
                        return BadRequest(raCertificadoBrochureBO.ActualesErrores);
                    }
                    return Ok(new { raCertificadoBrochureBO });
                }
                else
                {
                    return BadRequest("No se pudo identificar el Brochure.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}