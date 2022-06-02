using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Certificacion")]
    public class CertificacionController : BaseController<TCertificacion, ValidadorCertificacionDTO>
    {
        public CertificacionController(IIntegraRepository<TCertificacion> repositorio, ILogger<BaseController<TCertificacion, ValidadorCertificacionDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerProveedores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PartnerPwRepositorio _repoPartnerPw = new PartnerPwRepositorio();
                var Partners = _repoPartnerPw.ObtenerPartnerFiltro();
                return Json(new { Result = "OK", Records = Partners });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPgeneralesPorCertificacion(int IdCertificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CertificacionPGeneralRepositorio _repoCertPGeneral = new CertificacionPGeneralRepositorio();
                var pgenerales = _repoCertPGeneral.ObtenerTodoCertificacionPGeneralPorIdCertificacion(IdCertificacion);
                return Json(new { Result = "OK", Records = pgenerales });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoCertificaciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CertificacionTipoRepositorio _repoCertificacionTipo = new CertificacionTipoRepositorio();
                var CertificacionTipos = _repoCertificacionTipo.ObtenerTodoCertificacionTipoFiltro();
                return Json(new { Result = "OK", Records = CertificacionTipos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoMonedas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                MonedaRepositorio _repoMoneda = new MonedaRepositorio();
                var Monedas = _repoMoneda.ObtenerFiltroMoneda();
                return Json(new { Result = "OK", Records = Monedas });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerProgramasGenerales()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PgeneralRepositorio _repoPGeneral = new PgeneralRepositorio();
                var PGenerales = _repoPGeneral.ObtenerProgramasFiltro();
                return Json(new { Result = "OK", Records = PGenerales });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarCertificaciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CertificacionRepositorio _repoCertificacion = new CertificacionRepositorio();
                var Certificaciones = _repoCertificacion.ObtenerTodoCertificaciones();
                return Json(new { Result = "OK", Records = Certificaciones });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCertificacion([FromBody] CertificacionCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificacionRepositorio _repoCertificacion = new CertificacionRepositorio();
                CertificacionPGeneralRepositorio _repoCertificacionPGeneral = new CertificacionPGeneralRepositorio();

                CertificacionBO NuevaCertificacion = new CertificacionBO();

                NuevaCertificacion.Nombre = ObjetoDTO.Nombre;
                NuevaCertificacion.Descripcion = ObjetoDTO.Descripcion;
                NuevaCertificacion.IdPartner = ObjetoDTO.IdPartner;
                NuevaCertificacion.Costo = ObjetoDTO.Costo;
                NuevaCertificacion.IdMoneda = ObjetoDTO.IdMoneda;
                NuevaCertificacion.IdCertificacionTipo = ObjetoDTO.IdCertificacionTipo;

                NuevaCertificacion.Estado = true;
                NuevaCertificacion.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevaCertificacion.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevaCertificacion.FechaCreacion = DateTime.Now;
                NuevaCertificacion.FechaModificacion = DateTime.Now;

                _repoCertificacion.Insert(NuevaCertificacion);

                if (ObjetoDTO.PGenerales != null)
                { 
                    var PGeneralesAsociados = ObjetoDTO.PGenerales;
                    for (var i=0; i < PGeneralesAsociados.Count; ++i)
                    {
                        CertificacionPGeneralBO CertificacionPGeneralNuevo = new CertificacionPGeneralBO();
                        CertificacionPGeneralNuevo.IdCertificacion = NuevaCertificacion.Id;
                        CertificacionPGeneralNuevo.IdPGeneral = PGeneralesAsociados[i].IdPGeneral;
                        CertificacionPGeneralNuevo.Estado = true;
                        CertificacionPGeneralNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        CertificacionPGeneralNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        CertificacionPGeneralNuevo.FechaCreacion = DateTime.Now;
                        CertificacionPGeneralNuevo.FechaModificacion = DateTime.Now;

                        _repoCertificacionPGeneral.Insert(CertificacionPGeneralNuevo);
                    }
                }

                var CertificacionCreada = _repoCertificacion.ObtenerCertificacionPorId(NuevaCertificacion.Id).ToList();
                if (CertificacionCreada.Count < 1 || CertificacionCreada.Count > 1)
                    throw new Exception("Varios Registros encontrados, o Ninguno");

                return Ok(CertificacionCreada[0]);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCertificacion([FromBody] CertificacionCompuestoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificacionRepositorio _repoCertificacion = new CertificacionRepositorio();
                CertificacionPGeneralRepositorio _repoCertificacionPGeneral = new CertificacionPGeneralRepositorio();

                CertificacionBO Certificacion = _repoCertificacion.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                Certificacion.Nombre = ObjetoDTO.Nombre;
                Certificacion.Descripcion = ObjetoDTO.Descripcion;
                Certificacion.IdPartner = ObjetoDTO.IdPartner;
                Certificacion.Costo = ObjetoDTO.Costo;
                Certificacion.IdMoneda = ObjetoDTO.IdMoneda;
                Certificacion.IdCertificacionTipo = ObjetoDTO.IdCertificacionTipo;

                Certificacion.Estado = true;
                Certificacion.UsuarioModificacion = ObjetoDTO.Usuario;
                Certificacion.FechaModificacion = DateTime.Now;

                _repoCertificacion.Update(Certificacion);


                var PGeneralesAsociadosEnDB = _repoCertificacionPGeneral.GetBy(x => x.IdCertificacion == ObjetoDTO.Id).ToList();
                for (int j = 0; j < PGeneralesAsociadosEnDB.Count; ++j)
                    _repoCertificacionPGeneral.Delete(PGeneralesAsociadosEnDB[j].Id, ObjetoDTO.Usuario);

                if (ObjetoDTO.PGenerales != null)
                {
                    var PGeneralesAsociados = ObjetoDTO.PGenerales;
                    for (var i = 0; i < PGeneralesAsociados.Count; ++i)
                    {
                        CertificacionPGeneralBO CertificacionPGeneralNuevo = new CertificacionPGeneralBO();
                        CertificacionPGeneralNuevo.IdCertificacion = Certificacion.Id;
                        CertificacionPGeneralNuevo.IdPGeneral = PGeneralesAsociados[i].IdPGeneral;
                        CertificacionPGeneralNuevo.Estado = true;
                        CertificacionPGeneralNuevo.UsuarioCreacion = ObjetoDTO.Usuario;
                        CertificacionPGeneralNuevo.UsuarioModificacion = ObjetoDTO.Usuario;
                        CertificacionPGeneralNuevo.FechaCreacion = DateTime.Now;
                        CertificacionPGeneralNuevo.FechaModificacion = DateTime.Now;


                        _repoCertificacionPGeneral.Insert(CertificacionPGeneralNuevo);
                    }
                }

                var CertificacionCreada = _repoCertificacion.ObtenerCertificacionPorId(Certificacion.Id).ToList();
                if (CertificacionCreada.Count < 1 || CertificacionCreada.Count > 1)
                    throw new Exception("Varios Registros encontrados, o Ninguno");

                return Ok(CertificacionCreada[0]);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCertificacion([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificacionRepositorio _repoCertificacion = new CertificacionRepositorio();
                CertificacionPGeneralRepositorio _repoCertificacionPGeneral = new CertificacionPGeneralRepositorio();

                var PGeneralesAsociadosEnDB = _repoCertificacionPGeneral.GetBy(x => x.IdCertificacion == Eliminar.Id).ToList();
                for (int j = 0; j < PGeneralesAsociadosEnDB.Count; ++j)
                    _repoCertificacionPGeneral.Delete(PGeneralesAsociadosEnDB[j].Id, Eliminar.NombreUsuario);

                _repoCertificacion.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }




    public class ValidadorCertificacionDTO : AbstractValidator<TCertificacion>
    {
        public static ValidadorCertificacionDTO Current = new ValidadorCertificacionDTO();
        public ValidadorCertificacionDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
            
        }
    }
}
