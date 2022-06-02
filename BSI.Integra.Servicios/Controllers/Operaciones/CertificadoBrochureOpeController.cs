using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/CertificadoBrochure")]
    public class CertificadoBrochureOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                RaCertificadoBrochureRepositorio _repCertificadoBrochure = new RaCertificadoBrochureRepositorio();
                return Ok(_repCertificadoBrochure.GetBy(x => x.Estado, x => new { x.Id, x.Nombre, x.NombreEnCertificado, x.Contenido, x.TotalHoras,x.IdRaCertificadoBrochure, x.Activo, x.UsuarioCreacion, x.UsuarioModificacion, x.FechaCreacion, x.FechaModificacion }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                RaCertificadoBrochureRepositorio _repCertificadoBrochure = new RaCertificadoBrochureRepositorio();
                return Ok(_repCertificadoBrochure.ObtenerFiltro());
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
                    Activo = true,
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
                return Ok( raCertificadoBrochureBO );
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
                    return Ok( raCertificadoBrochureBO );
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


        [Route("[Action]")]
        [HttpPost]
        public ActionResult Asignar([FromBody] AsignarCertificadoBrochureDTO CertificadoBrochure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext integraDB = new integraDBContext();
                RaCentroCostoRepositorio _repRaCentroCosto = new RaCentroCostoRepositorio(integraDB);
                if (_repRaCentroCosto.Exist(CertificadoBrochure.IdCentroCosto))
                {
                    var raCentroCostoBO = _repRaCentroCosto.FirstById(CertificadoBrochure.IdCentroCosto);
                    raCentroCostoBO.IdRaCertificadoBrochure = CertificadoBrochure.Id;
                    raCentroCostoBO.FechaModificacion = DateTime.Now;
                    raCentroCostoBO.UsuarioModificacion = CertificadoBrochure.NombreUsuario;
                    if (!raCentroCostoBO.HasErrors)
                    {
                        _repRaCentroCosto.Update(raCentroCostoBO);
                    }
                    else
                    {
                        return BadRequest(raCentroCostoBO.ActualesErrores);
                    }
                    return Ok(raCentroCostoBO);
                }
                else
                {
                    return BadRequest("El registro no existe!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerCentroCostoAsignado(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCertificadoBrochureRepositorio _repCertificadoBrochure = new RaCertificadoBrochureRepositorio();
                return Ok(_repCertificadoBrochure.ObtenerCentroCostoAsociadoPorId(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}