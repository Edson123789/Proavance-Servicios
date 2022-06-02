using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CertificadoPartnerComplemento")]
    public class CertificadoPartnerComplementoController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public CertificadoPartnerComplementoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                CertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento = new CertificadoPartnerComplementoRepositorio(_integraDBContext);
                return Ok(_repCertificadoPartnerComplemento.ObtenerTodo());
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
        public ActionResult Insertar([FromBody] CertificadoPartnerComplementoDTO CertificadoPartnerComplemento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento = new CertificadoPartnerComplementoRepositorio(_integraDBContext);
                CertificadoPartnerComplementoBO CertificadoPartnerComplementoBO = new CertificadoPartnerComplementoBO()
                {
                    Nombre = CertificadoPartnerComplemento.Nombre,
                    Codigo = CertificadoPartnerComplemento.Codigo,
                    Descripcion = CertificadoPartnerComplemento.Descripcion,
                    MencionEnCertificado = CertificadoPartnerComplemento.MencionEnCertificado,
                    FrontalCentral = CertificadoPartnerComplemento.FrontalCentral,
                    FrontalInferiorIzquierda = CertificadoPartnerComplemento.FrontalInferiorIzquierda,
                    PosteriorCentral = CertificadoPartnerComplemento.PosteriorCentral,
                    PosteriorInferiorIzquierda = CertificadoPartnerComplemento.PosteriorInferiorIzquierda,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = CertificadoPartnerComplemento.NombreUsuario,
                    UsuarioModificacion = CertificadoPartnerComplemento.NombreUsuario
                };
                if (!CertificadoPartnerComplementoBO.HasErrors)
                {
                    _repCertificadoPartnerComplemento.Insert(CertificadoPartnerComplementoBO);
                }
                else
                {
                    return BadRequest(CertificadoPartnerComplementoBO.ActualesErrores);
                }
                return Ok(CertificadoPartnerComplementoBO);
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
                CertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento = new CertificadoPartnerComplementoRepositorio(_integraDBContext);
                if (_repCertificadoPartnerComplemento.Exist(CertificadoPartnerComplemento.Id))
                {
                    var CertificadoPartnerComplementoBO = _repCertificadoPartnerComplemento.FirstById(CertificadoPartnerComplemento.Id);
                    CertificadoPartnerComplementoBO.Nombre = CertificadoPartnerComplemento.Nombre;
                    CertificadoPartnerComplementoBO.Codigo = CertificadoPartnerComplemento.Codigo;
                    CertificadoPartnerComplementoBO.Descripcion = CertificadoPartnerComplemento.Descripcion;
                    CertificadoPartnerComplementoBO.MencionEnCertificado = CertificadoPartnerComplemento.MencionEnCertificado;
                    CertificadoPartnerComplementoBO.FrontalCentral = CertificadoPartnerComplemento.FrontalCentral;
                    CertificadoPartnerComplementoBO.FrontalInferiorIzquierda = CertificadoPartnerComplemento.FrontalInferiorIzquierda;
                    CertificadoPartnerComplementoBO.PosteriorCentral = CertificadoPartnerComplemento.PosteriorCentral;
                    CertificadoPartnerComplementoBO.PosteriorInferiorIzquierda = CertificadoPartnerComplemento.PosteriorInferiorIzquierda;
                    CertificadoPartnerComplementoBO.FechaModificacion = DateTime.Now;
                    CertificadoPartnerComplementoBO.UsuarioModificacion = CertificadoPartnerComplemento.NombreUsuario;
                    if (!CertificadoPartnerComplementoBO.HasErrors)
                    {
                        _repCertificadoPartnerComplemento.Update(CertificadoPartnerComplementoBO);
                    }
                    else
                    {
                        return BadRequest(CertificadoPartnerComplementoBO.ActualesErrores);
                    }
                    return Ok(CertificadoPartnerComplementoBO);
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
                CentroCostoCertificadoRepositorio _repCentroCostoCertificado = new CentroCostoCertificadoRepositorio(_integraDBContext);
                CentroCostoCertificadoBO centroCostoBO = new CentroCostoCertificadoBO();
                if (!_repCentroCostoCertificado.Exist(w => w.IdCentroCosto == CertificadoPartnerComplemento.IdCentroCosto))
                {
                    
                    //var CentroCostoBO = _repCentroCostoCertificado.FirstBy(w=> w.IdCentroCosto == CertificadoBrochure.IdRaCentroCosto && w.IdCertificadoBrochure == CertificadoBrochure.Id);
                    centroCostoBO.IdCentroCosto = CertificadoPartnerComplemento.IdCentroCosto;
                    centroCostoBO.IdCertificadoPartnerComplemento = CertificadoPartnerComplemento.Id;
                    centroCostoBO.Estado = true;
                    centroCostoBO.FechaCreacion = DateTime.Now;
                    centroCostoBO.FechaModificacion = DateTime.Now;
                    centroCostoBO.UsuarioCreacion = CertificadoPartnerComplemento.NombreUsuario;
                    centroCostoBO.UsuarioModificacion = CertificadoPartnerComplemento.NombreUsuario;
                    if (!centroCostoBO.HasErrors)
                    {
                        _repCentroCostoCertificado.Insert(centroCostoBO);
                    }
                    else
                    {
                        return BadRequest(centroCostoBO.ActualesErrores);
                    }
                    return Ok(centroCostoBO);
                }
                else
                {
                    var _centroCostoBO = _repCentroCostoCertificado.GetBy(w=> w.IdCentroCosto == CertificadoPartnerComplemento.IdCentroCosto);
                    foreach (var item in _centroCostoBO)
                    {
                        item.IdCertificadoPartnerComplemento = CertificadoPartnerComplemento.Id;
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = CertificadoPartnerComplemento.NombreUsuario;
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
                CertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento = new CertificadoPartnerComplementoRepositorio(_integraDBContext);
                return Ok(_repCertificadoPartnerComplemento.ObtenerCentroCostoAsociadoPorId(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}