using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/CertificadoEnvio")]
    public class CertificadoEnvioController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public CertificadoEnvioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio();
                CertificadoTipoProgramaRepositorio _repCertificadoTipoPrograma = new CertificadoTipoProgramaRepositorio(_integraDBContext);
                CertificadoTipoRepositorio _repCertificadoTipo = new CertificadoTipoRepositorio(_integraDBContext);
                CertificadoFormaEntregaRepositorio _repCertificadoFormaEntrega = new CertificadoFormaEntregaRepositorio(_integraDBContext);
                var rpta = new
                {
                    FormaEntrega = _repCertificadoFormaEntrega.GetBy(w => w.Estado, y => new { y.Id, y.Nombre }),
                    FormaTipoPrograma = _repCertificadoTipoPrograma.GetBy(w => w.Estado, y => new { y.Id, Nombre = y.NombreProgramaCertificado}),
                    FormaTipo = _repCertificadoFormaEntrega.GetBy(w => w.Estado, y => new { y.Id, y.Nombre })
                };
                    
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerTodoEnvioFisico([FromBody] FiltroCertificadoEnvioGrillaDTO Obj)
        {
            try
            {
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio();
                var rpta = _repCertificadoEnvio.ObtenerTodoEnvioFisico(Obj.paginador, Obj.filter);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPendienteEnvio([FromBody] FiltroCertificadoEnvioGrillaDTO Obj)
        {
            try
            {
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio();
                var rpta = _repCertificadoEnvio.ObtenerPendienteEnvio(Obj.paginador, Obj.filter);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPendienteEntrega([FromBody] FiltroCertificadoEnvioGrillaDTO Obj)
        {
            try
            {
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio();
                var rpta = _repCertificadoEnvio.ObtenerPendienteEntrega(Obj.paginador,Obj.filter);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CertificadoEnvioDTO CertificadoEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio(_integraDBContext);
                CertificadoEnvioBO certificadoEnvioBO = new CertificadoEnvioBO()
                {
                    IdCertificadoBrochure = CertificadoEnvio.Id,
                    IdCertificadoDetalle = CertificadoEnvio.IdCertificadoDetalle,
                    IdCertificadoFormaEntrega = CertificadoEnvio.IdCertificadoFormaEntrega,
                    FechaEnvio = CertificadoEnvio.FechaEnvio,
                    FechaRecepcion = CertificadoEnvio.FechaRecepcion,
                    CodigoSeguimiento = CertificadoEnvio.CodigoSeguimiento,
                    Observacion = CertificadoEnvio.Observacion,
                    IdCertificadoFormaEntregaPartner = CertificadoEnvio.IdCertificadoFormaEntregaPartner,
                    FechaEnvioPartner = CertificadoEnvio.FechaEnvioPartner,
                    FechaRecepcionPartner = CertificadoEnvio.FechaRecepcionPartner,
                    CodigoSeguimientoPartner = CertificadoEnvio.CodigoSeguimientoPartner,
                    ObservacionesPartner = CertificadoEnvio.ObservacionesPartner,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = CertificadoEnvio.NombreUsuario,
                    UsuarioModificacion = CertificadoEnvio.NombreUsuario

                };
                if (!certificadoEnvioBO.HasErrors)
                {
                    _repCertificadoEnvio.Insert(certificadoEnvioBO);
                }
                else
                {
                    return BadRequest(certificadoEnvioBO.ActualesErrores);
                }
                return Ok(certificadoEnvioBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CertificadoEnvioDTO CertificadoEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio(_integraDBContext);

                CertificadoEnvioBO certificadoEnvioBO = _repCertificadoEnvio.FirstBy(w => w.IdCertificadoDetalle == CertificadoEnvio.IdCertificadoDetalle);
                
                //certificadoEnvioBO.IdCertificadoBrochure = CertificadoEnvio.Id;
                certificadoEnvioBO.IdCertificadoFormaEntrega = CertificadoEnvio.IdCertificadoFormaEntrega;
                certificadoEnvioBO.FechaEnvio = CertificadoEnvio.FechaEnvio;
                certificadoEnvioBO.FechaRecepcion = CertificadoEnvio.FechaRecepcion;
                certificadoEnvioBO.CodigoSeguimiento = CertificadoEnvio.CodigoSeguimiento;
                certificadoEnvioBO.Observacion = CertificadoEnvio.Observacion;
                certificadoEnvioBO.IdCertificadoFormaEntregaPartner = CertificadoEnvio.IdCertificadoFormaEntregaPartner;
                certificadoEnvioBO.FechaEnvioPartner = CertificadoEnvio.FechaEnvioPartner;
                certificadoEnvioBO.FechaRecepcionPartner = CertificadoEnvio.FechaRecepcionPartner;
                certificadoEnvioBO.CodigoSeguimientoPartner = CertificadoEnvio.CodigoSeguimientoPartner;
                certificadoEnvioBO.ObservacionesPartner = CertificadoEnvio.ObservacionesPartner;
                certificadoEnvioBO.FechaModificacion = DateTime.Now;
                certificadoEnvioBO.UsuarioModificacion = CertificadoEnvio.NombreUsuario;

                
                if (!certificadoEnvioBO.HasErrors)
                {
                    _repCertificadoEnvio.Update(certificadoEnvioBO);
                }
                else
                {
                    return BadRequest(certificadoEnvioBO.ActualesErrores);
                }
                return Ok(certificadoEnvioBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
