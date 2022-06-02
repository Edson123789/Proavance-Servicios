using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.DTOs;
using AsignacionAutomaticaBO =BSI.Integra.Aplicacion.Transversal.BO.AsignacionAutomaticaBO;
using System.Transactions;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AsignacionAutomatica")]//Hace referencia el BO principal "AsignacionAutomatica"
    public class AsignacionAutomaticaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AsignacionAutomaticaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
            CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
            CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            PaisRepositorio _repPaisRepositorio = new PaisRepositorio(_integraDBContext);
            IndustriaRepositorio _repIndustria = new IndustriaRepositorio(_integraDBContext);
            CargoRepositorio _repCargo = new CargoRepositorio(_integraDBContext);
            AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
            AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
            ProbabilidadRegistroPwRepositorio _repProbabilidadRegistroPw = new ProbabilidadRegistroPwRepositorio(_integraDBContext);

            try
            {
                FiltrosAsignacionAutomaticaDTO filtroAsignacionAutomatica = new FiltrosAsignacionAutomaticaDTO
                {
                    filtroCiudad = _repCiudad.ObtenerCiudadesPorPais(),
                    filtroCentroCosto = _repCentroCosto.ObtenerCentroCostoParaFiltro(),
                    filtroCategoriaOrigen = _repCategoriaOrigen.ObtenerCategoriaFiltro(),
                    filtroPais = _repPaisRepositorio.ObtenerTodoFiltro(),
                    filtroProbabilidad = _repProbabilidadRegistroPw.ObtenerTodoFiltro(),
                    filtroIndustria = _repIndustria.ObtenerIndustriaFiltro(),
                    filtroCargo = _repCargo.ObtenerCargoFiltro(),
                    filtroAreaFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro(),
                    filtroAreaTrabajo = _repAreaTrabajo.ObtenerAreasTrabajo()
                };


                return Ok(filtroAsignacionAutomatica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Filtros != null)
                {
                    CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
                    var centroCosto = _repCentroCosto.ObtenerTodoFiltroAutoComplete(Filtros["valor"]);
                    return Ok(centroCosto);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRegistrosImportados([FromBody] AsignacionAutomaticaRegistroImportadosFiltroDTO obj)
        {
            try
            {
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
                var RegistroImportados = _repAsignacionAutomatica.ObtenerRegistrosImportados(obj.paginador, obj.filtroRegistroImportado);
                return Ok(RegistroImportados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRegistrosErroneos([FromBody] Paginador paginador)
        {
            try
            {
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
                AsignacionAutomaticaErrorRepositorio _repAsignacionAutomaticaError = new AsignacionAutomaticaErrorRepositorio(_integraDBContext);
                var RegistroErroneos = _repAsignacionAutomatica.ObtenerTodoRegistrosErroneos(paginador);
                int Count = _repAsignacionAutomatica.ObtenerTodoRegistroserroneosCount();


                foreach (var erroneo in RegistroErroneos)
                {
                    erroneo.Errores = _repAsignacionAutomaticaError.ObtenerError(erroneo.Id);
                }
                return Ok(new { data = RegistroErroneos, Total = Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerRegistrosDuplicados()
        {
            try
            {
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
                var RegistroImportados = _repAsignacionAutomatica.ObtenerTodoRegistrosDuplicados();
                return Ok(RegistroImportados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public IActionResult EliminarAsignacionAutomaticaErroneo(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
                AsignacionAutomaticaErrorRepositorio _repAsignacionAutomaticaError = new AsignacionAutomaticaErrorRepositorio(_integraDBContext);

                if (_repAsignacionAutomatica.Exist(Id))
                {
                    var lista = _repAsignacionAutomaticaError.GetBy(o => true, x => new { x.Id, x.IdAsignacionAutomatica }).Where(x => x.IdAsignacionAutomatica == Id).ToList();

                    foreach (var item in lista)
                    {
                        _repAsignacionAutomaticaError.Delete(item.Id, Usuario);
                    }
                    _repAsignacionAutomatica.Delete(Id, Usuario);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
