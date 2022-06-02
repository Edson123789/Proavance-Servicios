using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/SentinelSueldoIndividual")]
    public class SentinelSueldoIndividualController : Controller
    {
        public SentinelSueldoIndividualController()
        {
        }

        #region MetodosAdicionales

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCategoriaTamanioEmpresa()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TamanioEmpresaRepositorio _repoTamanioEmpresa = new TamanioEmpresaRepositorio();
                var lista = _repoTamanioEmpresa.ObtenerTodoTamanioEmpresaes();
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCargo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CargoRepositorio _repoCargo = new CargoRepositorio();
                var Cargos = _repoCargo.ObtenerCargoFiltro();
                return Json(new { Result = "OK", Records = Cargos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaAreaTrabajo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AreaTrabajoRepositorio _repoAreaTrabajo = new AreaTrabajoRepositorio();
                var AreaTrabajos = _repoAreaTrabajo.ObtenerTodoAreaTrabajoFiltro();
                return Json(new { Result = "OK", Records = AreaTrabajos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaAreaFormacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AreaFormacionRepositorio _repoAreaFormacion = new AreaFormacionRepositorio();
                var AreaFormacions = _repoAreaFormacion.ObtenerAreaFormacionFiltro();
                return Json(new { Result = "OK", Records = AreaFormacions });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaIndustria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IndustriaRepositorio _repoIndustria = new IndustriaRepositorio();
                var Industrias = _repoIndustria.ObtenerIndustriaFiltro();
                return Json(new { Result = "OK", Records = Industrias });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerEmpresasPorNombre(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                // evita que se devuelva todas las empresas competidoras (  todas encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Json(new { Result = "OK", Records = ListaVacia });
                }

                EmpresaRepositorio _repoEmpresa = new EmpresaRepositorio();
                var listaEmpresa = _repoEmpresa.CargarEmpresasAutoComplete(NombreParcial);
                return Json(new { Result = "OK", Records = listaEmpresa });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodasEmpresas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                EmpresaRepositorio _repoEmpresa = new EmpresaRepositorio();
                var Empresas = _repoEmpresa.ObtenerTodoEmpresasFiltro();
                return Json(new { Result = "OK", Records = Empresas });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repoPais = new PaisRepositorio();
                var listaPais = _repoPais.ObtenerPaisesCombo();
                return Json(new { Result = "OK", Records = listaPais });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        #endregion






        [Route("[action]")]
        [HttpPost]
        public ActionResult VisualizarSentinelSueldoIndividual([FromBody] SentinelSueldoIndividualListasFiltrosDTO ObjetoDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SentinelSueldoIndividualRepositorio _repSentinelSueldoIndividual = new SentinelSueldoIndividualRepositorio();
                var listaSentinelSueldoIndividual = _repSentinelSueldoIndividual.ObtenerTodosSentinelSueldoIdividualesPorFiltro(ObjetoDTO.Industrias, ObjetoDTO.Categorias, ObjetoDTO.Empresas, ObjetoDTO.Cargos, ObjetoDTO.AreaTrabajos, ObjetoDTO.AreaFormaciones, ObjetoDTO.Paises);

               
                return Json(new { Result = "OK", Records = listaSentinelSueldoIndividual });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


  
        

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarSentinelSueldoIndividual([FromBody] SentinelSueldoIndividualIncluirDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            { 
                
                SentinelSueldoIndividualRepositorio _repoSentinelSueldoIndividual = new SentinelSueldoIndividualRepositorio();
                SentinelSueldoIndividualBO sentinelSueldoIndividual = _repoSentinelSueldoIndividual.FirstById(ObjetoDTO.Id);

                sentinelSueldoIndividual.Id = ObjetoDTO.Id;
                sentinelSueldoIndividual.Incluir = ObjetoDTO.Incluir;
                sentinelSueldoIndividual.Estado = true;
                sentinelSueldoIndividual.UsuarioModificacion = ObjetoDTO.Usuario;
                sentinelSueldoIndividual.FechaModificacion = DateTime.Now;

                _repoSentinelSueldoIndividual.Update(sentinelSueldoIndividual);

                return Ok(sentinelSueldoIndividual);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
