using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CertificadoBrochure")]
    public class CertificadoBrochureController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public CertificadoBrochureController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                CertificadoBrochureRepositorio _repCertificadoBrochure = new CertificadoBrochureRepositorio();
                var rpta = _repCertificadoBrochure.ObtenerTodo();
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult obtenerfiltro()
        {
            try
            {
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                var lista = _repCentroCosto.ObtenerCentroCostoParaFiltro();
                return Ok(lista);
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
                CertificadoBrochureRepositorio _repCertificadoBrochure = new CertificadoBrochureRepositorio(_integraDBContext);
                CertificadoBrochureBO CertificadoBrochureBO = new CertificadoBrochureBO()
                {
                    Nombre = CertificadoBrochure.Nombre,
                    NombreEnCertificado = CertificadoBrochure.NombreEnCertificado,
                    TotalHoras = CertificadoBrochure.TotalHoras,
                    Contenido = CertificadoBrochure.Contenido,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = CertificadoBrochure.NombreUsuario,
                    UsuarioModificacion = CertificadoBrochure.NombreUsuario
                };
                if (!CertificadoBrochureBO.HasErrors)
                {
                    _repCertificadoBrochure.Insert(CertificadoBrochureBO);
                }
                else
                {
                    return BadRequest(CertificadoBrochureBO.ActualesErrores);
                }
                return Ok(CertificadoBrochureBO);
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
                CertificadoBrochureRepositorio _repCertificadoBrochure = new CertificadoBrochureRepositorio();
                if (_repCertificadoBrochure.Exist(CertificadoBrochure.Id))
                {
                    var CertificadoBrochureBO = _repCertificadoBrochure.FirstById(CertificadoBrochure.Id);
                    CertificadoBrochureBO.Nombre = CertificadoBrochure.Nombre;
                    CertificadoBrochureBO.NombreEnCertificado = CertificadoBrochure.NombreEnCertificado;
                    CertificadoBrochureBO.TotalHoras = CertificadoBrochure.TotalHoras;
                    CertificadoBrochureBO.Contenido = CertificadoBrochure.Contenido;
                    CertificadoBrochureBO.FechaModificacion = DateTime.Now;
                    CertificadoBrochureBO.UsuarioModificacion = CertificadoBrochure.NombreUsuario;
                    if (!CertificadoBrochureBO.HasErrors)
                    {
                        _repCertificadoBrochure.Update(CertificadoBrochureBO);
                    }
                    else
                    {
                        return BadRequest(CertificadoBrochureBO.ActualesErrores);
                    }
                    return Ok(CertificadoBrochureBO);
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
                CentroCostoCertificadoRepositorio _repCentroCostoCertificado = new CentroCostoCertificadoRepositorio(_integraDBContext);

                if (!_repCentroCostoCertificado.Exist(w=> w.IdCentroCosto == CertificadoBrochure.IdCentroCosto &&  w.IdCertificadoBrochure == CertificadoBrochure.Id))
                {
                    CentroCostoCertificadoBO centroCostoBO = new CentroCostoCertificadoBO();
                    if (!_repCentroCostoCertificado.Exist(w => w.IdCentroCosto == CertificadoBrochure.IdCentroCosto))
                    {
                        //var CentroCostoBO = _repCentroCostoCertificado.FirstBy(w=> w.IdCentroCosto == CertificadoBrochure.IdRaCentroCosto && w.IdCertificadoBrochure == CertificadoBrochure.Id);
                        centroCostoBO.IdCentroCosto = CertificadoBrochure.IdCentroCosto;
                        centroCostoBO.IdCertificadoBrochure = CertificadoBrochure.Id;
                        centroCostoBO.Estado = true;
                        centroCostoBO.FechaCreacion = DateTime.Now;
                        centroCostoBO.FechaModificacion = DateTime.Now;
                        centroCostoBO.UsuarioCreacion = CertificadoBrochure.NombreUsuario;
                        centroCostoBO.UsuarioModificacion = CertificadoBrochure.NombreUsuario;
                        if (!centroCostoBO.HasErrors)
                        {
                            _repCentroCostoCertificado.Update(centroCostoBO);
                        }
                        else
                        {
                            return BadRequest(centroCostoBO.ActualesErrores);
                        }
                    }
                    else
                    {
                        var complementocertificado = _repCentroCostoCertificado.FirstBy(w => w.IdCentroCosto == CertificadoBrochure.IdCentroCosto);

                        centroCostoBO.IdCertificadoPartnerComplemento = complementocertificado.IdCertificadoPartnerComplemento;
                        centroCostoBO.IdCentroCosto = CertificadoBrochure.IdCentroCosto;
                        centroCostoBO.IdCertificadoBrochure = CertificadoBrochure.Id;
                        centroCostoBO.Estado = true;
                        centroCostoBO.FechaCreacion = DateTime.Now;
                        centroCostoBO.FechaModificacion = DateTime.Now;
                        centroCostoBO.UsuarioCreacion = CertificadoBrochure.NombreUsuario;
                        centroCostoBO.UsuarioModificacion = CertificadoBrochure.NombreUsuario;
                        if (!centroCostoBO.HasErrors)
                        {
                            _repCentroCostoCertificado.Update(centroCostoBO);
                        }
                        else
                        {
                            return BadRequest(centroCostoBO.ActualesErrores);
                        }
                    }
                    
                    return Ok(centroCostoBO);
                }
                else
                {
                    var _centroCostoBO = _repCentroCostoCertificado.GetBy(w => w.IdCentroCosto == CertificadoBrochure.IdCentroCosto);
                    foreach (var item in _centroCostoBO)
                    {
                        item.IdCertificadoBrochure = CertificadoBrochure.Id;
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = CertificadoBrochure.NombreUsuario;
                    }

                    _repCentroCostoCertificado.Update(_centroCostoBO);

                    return Ok(_centroCostoBO);
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
                CertificadoBrochureRepositorio _repCertificadoBrochure = new CertificadoBrochureRepositorio(_integraDBContext);
                return Ok(_repCertificadoBrochure.ObtenerCentroCostoAsociadoPorId(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}