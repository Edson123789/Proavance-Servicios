using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using System.Transactions;
//using BSI.Integra.Aplicacion.Planificacion.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CentroCosto")]
    public class CentroCostoController : BaseController<TCentroCosto, ValidadorCentroCostoDTO>
    {
        public CentroCostoController(IIntegraRepository<TCentroCosto> repositorio, ILogger<BaseController<TCentroCosto, ValidadorCentroCostoDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetAllIdNombreCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (Filtros != null && Filtros["Valor"] != null)
                {
                    CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio();
                    return Ok(repCentroCostoRep.GetBy(x => x.Estado == true && x.Nombre.Contains(Filtros["Valor"].ToString()), x => new { x.Id, x.Nombre }).ToList());
                }
                else {

                    return Ok(new { });
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetAllCentroCosto([FromBody] FiltroCompuestroGrillaDTO dtoFiltro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(contexto);

                FiltroCentroCostoDTO filtro = new FiltroCentroCostoDTO();
                if (dtoFiltro.filter == null)
                {
                    filtro.CentroCosto = "_";
                    filtro.Skip = dtoFiltro.paginador.skip;
                    filtro.Take = dtoFiltro.paginador.take;
                }
                else
                {
                    filtro.CentroCosto = dtoFiltro.filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault() == null ? "_" : dtoFiltro.filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault().Value;
                    filtro.Skip = dtoFiltro.paginador.skip;
                    filtro.Take = dtoFiltro.paginador.take;
                }
                List<CentroCostoCompuestoDTO> Objeto = _repCentroCosto.ObtenerCentroCostoParaTabla(filtro);

                return Ok(new { data = Objeto, Objeto.FirstOrDefault().Total });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult GetAllTroncalCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TroncalPgeneralRepositorio _repTroncalCiudad = new TroncalPgeneralRepositorio();

                return Ok(new { data = _repTroncalCiudad.ObtenerTroncalCiudad() });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult GetAllAreaCC()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AreaCcRepositorio _repAreaCentroCiudad = new AreaCcRepositorio();

                return Ok(new { data = _repAreaCentroCiudad.ObtenerAreaCentroCosto() });
                //AreaCcBO Objeto = new AreaCcBO();
                //Objeto.GetAllAreaCC();

                //return Ok(new { data = Objeto.AreaCC });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdAreaCC}")]
        [HttpGet]
        public ActionResult GetSubNivelCCByAreaCC(int IdAreaCC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                SubNivelCcRepositorio _repSubNivelCCPorArea = new SubNivelCcRepositorio();

                return Ok(new { data = _repSubNivelCCPorArea.ObtenerSubNivelCCPorAreaCC(IdAreaCC) });
                //SubNivelCcBO Objeto = new SubNivelCcBO();
                //Objeto.GetSubNivelCCByAreaCC(IdAreaCC);

                //return Ok(new { data = Objeto.SubNivelCCbyAreaCC });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdArea}")]
        [HttpGet]
        public ActionResult GetSubAreaByArea(int IdArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                SubAreaRepositorio _repSubArea = new SubAreaRepositorio();

                return Ok(new { data = _repSubArea.ObtenerSubAreaPorArea(IdArea) });
                //SubAreaBO Objeto = new SubAreaBO();
                //Objeto.GetSubAreaByArea(IdArea);

                //return Ok(new { data = Objeto.SubAreaByArea });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdSubArea}")]
        [HttpGet]
        public ActionResult GetTroncalPgeneralBySubArea(int IdSubArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TroncalPgeneralRepositorio _repTroncalPorSubArea = new TroncalPgeneralRepositorio();

                return Ok(new { data = _repTroncalPorSubArea.ObtenerTroncalPgeneralPorSubArea(IdSubArea) });
                //TroncalPgeneralBO Objeto = new TroncalPgeneralBO();
                //Objeto.GetTroncalPgeneralBySubArea(IdSubArea);

                //return Ok(new { data = Objeto.TroncalPgenerealBySubArea });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult GetAllArea()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaRepositorio _repArea = new AreaRepositorio();

                return Ok(new { data = _repArea.ObtenerAreas() });
                //AreaBO Objeto = new AreaBO();
                //Objeto.GetAllArea();

                //return Ok(new { data = Objeto.Area });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{UserName}")]
        [HttpPost]
        public ActionResult InsertCentroCosto([FromBody]CentroCostoDTO Objeto, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(contexto);

                CentroCostoBO CentroCosto = new CentroCostoBO();
                CentroCosto.IdArea = Objeto.IdArea;
                CentroCosto.IdSubArea = Objeto.IdSubArea;
                CentroCosto.IdPgeneral = Objeto.IdPgeneral;
                CentroCosto.Nombre = Objeto.Nombre;
                CentroCosto.Codigo = Objeto.Codigo;
                CentroCosto.IdAreaCc = Objeto.IdAreaCc;
                CentroCosto.Ismtotales = Objeto.Ismtotales;
                CentroCosto.Icpftotales = Objeto.Icpftotales;
                CentroCosto.Estado = true;
                CentroCosto.FechaCreacion = DateTime.Now;
                CentroCosto.FechaModificacion = DateTime.Now;
                CentroCosto.UsuarioCreacion = UserName;
                CentroCosto.UsuarioModificacion = UserName;

                if (!CentroCosto.HasErrors)
                {
                    var rpta = _repCentroCosto.Insert(CentroCosto);
                    return Ok(new { data = CentroCosto.Id });
                }
                else
                {
                    return BadRequest(CentroCosto.GetErrors(null));
                }


            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{UserName}")]
        [HttpPost]
        public ActionResult ActualizarCentroCosto([FromBody]CentroCostoDTO Objeto, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(contexto);

                CentroCostoBO CentroCosto = _repCentroCosto.FirstById(Objeto.Id);

                CentroCosto.IdArea = Objeto.IdArea;
                CentroCosto.IdSubArea = Objeto.IdSubArea;
                CentroCosto.IdPgeneral = Objeto.IdPgeneral;
                CentroCosto.Nombre = Objeto.Nombre;
                CentroCosto.Codigo = Objeto.Codigo;
                CentroCosto.IdAreaCc = Objeto.IdAreaCc;
                CentroCosto.FechaModificacion = DateTime.Now;
                CentroCosto.UsuarioModificacion = UserName;
                if (!CentroCosto.HasErrors)
                {
                    var rpta = _repCentroCosto.Update(CentroCosto);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(CentroCosto.GetErrors(null));
                }



            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdCentroCosto}/{UserName}")]
        [HttpGet]
        public ActionResult EliminarCentroCosto(int IdCentroCosto, string UserName)
        {
            try
            {
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio();
                PespecificoRepositorio repPEspecifico = new PespecificoRepositorio();                
                var idPespecifico = repPEspecifico.ObtenerPespecificoPorCentroCosto(IdCentroCosto);
                if (repPEspecifico.Exist(idPespecifico.Id))
                {
                    repCentroCosto.Delete(IdCentroCosto, UserName);
                    repPEspecifico.Delete(idPespecifico.Id, UserName);
                    
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        repCentroCosto.Delete(IdCentroCosto, UserName);
                    }
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoAutocomplete([FromBody] FiltroDinamicoDTO CentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            try
            {
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                return Ok(_repCentroCosto.ObtenerDatosCentroCostoPorNombre(CentroCosto.Nombre).Select(x => new { Id = x.NombreCentroCosto, Nombre = x.NombreCentroCosto }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorCentroCostoDTO : AbstractValidator<TCentroCosto>
    {
        public static ValidadorCentroCostoDTO Current = new ValidadorCentroCostoDTO();
        public ValidadorCentroCostoDTO()
        {
            RuleFor(objeto => objeto.Codigo).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
        }
    }
}
