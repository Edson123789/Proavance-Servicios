using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/CertificadoPartnerComplemento")]
    public class CertificadoPartnerComplementoOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                RaCertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento = new RaCertificadoPartnerComplementoRepositorio();
                return Ok(_repCertificadoPartnerComplemento.GetBy(x => x.Estado, x => new {
                    x.Id,
                    x.Codigo,
                    x.Categoria,
                    x.Nombre,
                    x.Descripcion,
                    x.MencionEnCertificado,
                    x.FrontalCentral,
                    x.FrontalInferiorIzquierda,
                    x.PosteriorCentral,
                    x.PosteriorInferiorIzquierda,
                    x.Activo,
                    x.UsuarioCreacion,
                    x.UsuarioModificacion,
                    x.FechaCreacion,
                    x.FechaModificacion
                }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CertificadoPartnerComplementoDTO CertificadoPartnerComplemento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento = new RaCertificadoPartnerComplementoRepositorio();
                RaCertificadoPartnerComplementoBO raCertificadoPartnerComplementoBO = new RaCertificadoPartnerComplementoBO()
                {
                    Nombre = CertificadoPartnerComplemento.Nombre,
                    Codigo = CertificadoPartnerComplemento.Codigo,
                    Categoria = CertificadoPartnerComplemento.Categoria,
                    Descripcion = CertificadoPartnerComplemento.Descripcion,
                    MencionEnCertificado = CertificadoPartnerComplemento.MencionEnCertificado,
                    FrontalCentral = CertificadoPartnerComplemento.FrontalCentral,
                    FrontalInferiorIzquierda = CertificadoPartnerComplemento.FrontalInferiorIzquierda,
                    PosteriorCentral = CertificadoPartnerComplemento.PosteriorCentral,
                    PosteriorInferiorIzquierda = CertificadoPartnerComplemento.PosteriorInferiorIzquierda,
                    Activo = true,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = CertificadoPartnerComplemento.NombreUsuario,
                    UsuarioModificacion = CertificadoPartnerComplemento.NombreUsuario
                };
                if (!raCertificadoPartnerComplementoBO.HasErrors)
                {
                    _repCertificadoPartnerComplemento.Insert(raCertificadoPartnerComplementoBO);
                }
                else
                {
                    return BadRequest(raCertificadoPartnerComplementoBO.ActualesErrores);
                }
                return Ok( raCertificadoPartnerComplementoBO );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CertificadoPartnerComplementoDTO CertificadoPartnerComplemento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaCertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento = new RaCertificadoPartnerComplementoRepositorio();
                if (_repCertificadoPartnerComplemento.Exist(CertificadoPartnerComplemento.Id))
                {
                    var raCertificadoPartnerComplementoBO = _repCertificadoPartnerComplemento.FirstById(CertificadoPartnerComplemento.Id);
                    raCertificadoPartnerComplementoBO.Nombre = CertificadoPartnerComplemento.Nombre;
                    raCertificadoPartnerComplementoBO.Codigo = CertificadoPartnerComplemento.Codigo;
                    raCertificadoPartnerComplementoBO.Categoria = CertificadoPartnerComplemento.Categoria;
                    raCertificadoPartnerComplementoBO.Descripcion = CertificadoPartnerComplemento.Descripcion;
                    raCertificadoPartnerComplementoBO.MencionEnCertificado = CertificadoPartnerComplemento.MencionEnCertificado;
                    raCertificadoPartnerComplementoBO.FrontalCentral = CertificadoPartnerComplemento.FrontalCentral;
                    raCertificadoPartnerComplementoBO.FrontalInferiorIzquierda = CertificadoPartnerComplemento.FrontalInferiorIzquierda;
                    raCertificadoPartnerComplementoBO.PosteriorCentral = CertificadoPartnerComplemento.PosteriorCentral;
                    raCertificadoPartnerComplementoBO.PosteriorInferiorIzquierda = CertificadoPartnerComplemento.PosteriorInferiorIzquierda;
                    raCertificadoPartnerComplementoBO.Activo = CertificadoPartnerComplemento.Activo;
                    raCertificadoPartnerComplementoBO.FechaModificacion = DateTime.Now;
                    raCertificadoPartnerComplementoBO.UsuarioModificacion = CertificadoPartnerComplemento.NombreUsuario;
                    if (!raCertificadoPartnerComplementoBO.HasErrors)
                    {
                        _repCertificadoPartnerComplemento.Update(raCertificadoPartnerComplementoBO);
                    }
                    else
                    {
                        return BadRequest(raCertificadoPartnerComplementoBO.ActualesErrores);
                    }
                    return Ok( raCertificadoPartnerComplementoBO );
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
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Asignar([FromBody] AsignarCertificadoPartnerComplementoDTO CertificadoPartnerComplemento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext integraDB = new integraDBContext();
                RaCentroCostoRepositorio _repRaCentroCosto = new RaCentroCostoRepositorio(integraDB);
                if (_repRaCentroCosto.Exist(CertificadoPartnerComplemento.IdCentroCosto))
                {
                    var raCentroCostoBO = _repRaCentroCosto.FirstById(CertificadoPartnerComplemento.IdCentroCosto);
                    raCentroCostoBO.IdRaCertificadoPartnerComplemento = CertificadoPartnerComplemento.Id;
                    raCentroCostoBO.FechaModificacion = DateTime.Now;
                    raCentroCostoBO.UsuarioModificacion = CertificadoPartnerComplemento.NombreUsuario;
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
                RaCertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento = new RaCertificadoPartnerComplementoRepositorio();
                return Ok(_repCertificadoPartnerComplemento.ObtenerCentroCostoAsociadoPorId(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}