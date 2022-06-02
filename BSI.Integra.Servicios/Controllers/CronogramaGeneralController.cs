using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CronogramaGeneral")]
    public class CronogramaGeneralController : BaseController<TPespecificoSesion, ValidadoCronogramaGeneralDTO>
    {
        public CronogramaGeneralController(IIntegraRepository<TPespecificoSesion> repositorio, ILogger<BaseController<TPespecificoSesion, ValidadoCronogramaGeneralDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        { }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPGeneralIntegra([FromBody] Dictionary<string, string> Filtro)
        {
            try
            {
                TroncalPgeneralRepositorio _repTroncalPgeneralRepositorio = new TroncalPgeneralRepositorio();
                if (Filtro != null  )
                {
                    if (Filtro.Count() > 0 && (Filtro["Valor"] != null))
                    {
                        var lista = _repTroncalPgeneralRepositorio.ObtenerTodoPGeneralIntegra(Filtro["Valor"].ToString());
                        return Ok(lista);
                    }
                }
                return Ok(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public ActionResult ObtenerTodoPEspecificoPorIdPGeneral(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                return Ok(_repPespecifico.ObtenerPEspecificoPorIdPGeneral(IdProgramaGeneral));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoExpositor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio();
                return Ok(_repExpositor.ObtenerExpositoresFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{PEspecifico}/{IdAmbiente}/{IdExpositor}")]
        [HttpGet]
        public ActionResult ObtenerTodoSesionPorAmbienteExpositorPEspecifico(int? PEspecifico, int? IdAmbiente, int? IdExpositor)
        {
            try
            {
                PespecificoSesionRepositorio _repPEspecificoSesion = new PespecificoSesionRepositorio();
                return Ok(_repPEspecificoSesion.ObtenerTodoSesionPorAmbienteExpositorPEspecifico(PEspecifico, IdAmbiente, IdExpositor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoCiudadConLocaciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();
                return Ok(_repRegionCiudad.ObtenerRegionCiudadConLocacion());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoComboAmbiente()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AmbienteRepositorio _repAmbiente = new AmbienteRepositorio();
                return Ok(_repAmbiente.ObtenerAmbientes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoComboLocacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LocacionRepositorio _repLocacion = new LocacionRepositorio();
                return Ok(_repLocacion.ObtenerTodoComboLocacion());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoCiudades()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SedeRepositorio _repSede = new SedeRepositorio();
                return Ok(_repSede.ObtenerTodoSedeCiudad());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdCiudad}/{IdSede}/{IdAmbiente}")]
        [HttpGet]
        public ActionResult ObtenerTodoSesionPorCiudadLocacionAmbiente(int? IdCiudad, int? IdSede, int? IdAmbiente)
        {
            try
            {
                PespecificoSesionRepositorio _repPEspecificoSesion = new PespecificoSesionRepositorio();
                return Ok(_repPEspecificoSesion.ObtenerTodoSesionPorCiudadLocacionAmbiente(IdCiudad, IdSede, IdAmbiente));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodoCentroCosto([FromBody] Dictionary<string, string> Filtro)
        {
            try
            {
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                if (Filtro != null)
                {
                    if (Filtro.Count() > 0 && (Filtro["Valor"] != null))
                    {
                        var lista = _repCentroCosto.ObtenerTodoCentroCostoFiltro(Filtro["Valor"].ToString());
                        return Ok(lista);
                    }
                }
                return Ok(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{CentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerTodoSesionesPorCentroCosto(String CentroCosto)
        {
            try
            {
                PespecificoSesionRepositorio _repPEspecificoSesion = new PespecificoSesionRepositorio();
                var intCentroCosto = Array.ConvertAll(CentroCosto.Split(','), int.Parse);
                return Ok(_repPEspecificoSesion.ObtenerTodoSesionesPorCentroCosto(intCentroCosto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

    public class ValidadoCronogramaGeneralDTO : AbstractValidator<TPespecificoSesion>
    {
        public static ValidadoCronogramaGeneralDTO Current = new ValidadoCronogramaGeneralDTO();
        public ValidadoCronogramaGeneralDTO()
        {
        }
    }
}
