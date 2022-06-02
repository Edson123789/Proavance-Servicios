using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/RegistrarOportunidad")]
    public class RegsitrarOportunidadController : Controller
    {
        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerDatosFiltroRegistrarOportunidad(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio();
                CiudadRepositorio _repCiudad = new CiudadRepositorio();
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                OrigenRepositorio _repOrigen = new OrigenRepositorio();
                CargoRepositorio _repCargo = new CargoRepositorio();
                AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio();
                AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio();
                IndustriaRepositorio _repIndustria = new IndustriaRepositorio();
                PersonalRepositorio _repPersonal = new PersonalRepositorio();

                var Area = _repPersonal.GetBy(w => w.Id == IdPersonal, y => new { y.AreaAbrev }).FirstOrDefault();
                string Variable = ""; 
                if (Area.AreaAbrev != null && Area != null)
                {
                    Variable = Area.AreaAbrev.ToString();
                }
                ContactoOportunidadDTO ContactoOportunidad = new ContactoOportunidadDTO
                {
                    Paises = _repPais.ObtenerTodoFiltro(),
                    Ciudades = _repCiudad.ObtenerCiudadesPorPais(),
                    TipoDatoChats = _repTipoDato.ObtenerFiltro(),
                    FaseOportunidades = _repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro(),
                    CategoriaOrigenes = _repOrigen.ObtenerOrigeneParaRegistrarOportunidad(Variable),
                    Cargos = _repCargo.ObtenerCargoFiltro(),
                    AreasFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro(),
                    AreasTrabajo = _repAreaTrabajo.ObtenerTodoAreaTrabajoFiltro(),
                    Industrias = _repIndustria.ObtenerIndustriaFiltro()
                };

                return Ok(ContactoOportunidad);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidades([FromBody]RegistrarOportunidadFitroGrillaDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();

                var OportunidadManual = _repOportunidad.ObtenerPorFiltroRegistrarOportunidad(obj.filtro, obj.paginador);

                return Ok(OportunidadManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
