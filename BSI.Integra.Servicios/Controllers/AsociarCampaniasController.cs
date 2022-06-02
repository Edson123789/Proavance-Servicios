using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AsociarCampanias")]
    
    public class AsociarCampaniasController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ConjuntoAnuncioFacebookRepositorio _repConjuntoAnuncioFacebookRepositorio;
        private readonly ConjuntoAnuncioRepositorio _repConjuntoAnuncioRepositorio;
        private readonly CategoriaOrigenRepositorio _repCategoriaOrigen;
        public AsociarCampaniasController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repConjuntoAnuncioFacebookRepositorio = new ConjuntoAnuncioFacebookRepositorio(_integraDBContext);
            _repConjuntoAnuncioRepositorio = new ConjuntoAnuncioRepositorio(_integraDBContext);
            _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comboCategoriaOrigen = _repCategoriaOrigen.ObtenerCategoriaFiltro();
                var comboConjuntoAnuncioFacebook = _repConjuntoAnuncioFacebookRepositorio.ObtenerConjuntoAnuncioFBFiltro();
                return Ok(new {ComboCategoriaOrigen= comboCategoriaOrigen, ComboConjuntoAnuncioFacebook= comboConjuntoAnuncioFacebook});
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerConjuntoAnuncioAsociacion()
        {
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncioRepositorio = new ConjuntoAnuncioRepositorio(_integraDBContext);

                return Ok(_repConjuntoAnuncioRepositorio.ObtenerConjuntoAnuncioAsociacion().OrderByDescending(x => x.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ConjuntoAnuncioDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncioRepositorio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                ConjuntoAnuncioBO conjuntoAnuncioBO = new ConjuntoAnuncioBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    conjuntoAnuncioBO.Nombre = Json.Nombre;
                    conjuntoAnuncioBO.IdCategoriaOrigen = Json.IdCategoriaOrigen;
                    conjuntoAnuncioBO.IdConjuntoAnuncioFacebook = Json.IdConjuntoAnuncioFacebook;
                    conjuntoAnuncioBO.FechaCreacionCampania = DateTime.Now;
                    conjuntoAnuncioBO.Estado = true;
                    conjuntoAnuncioBO.UsuarioCreacion = Json.NombreUsuario;
                    conjuntoAnuncioBO.UsuarioModificacion = Json.NombreUsuario;
                    conjuntoAnuncioBO.FechaCreacion = DateTime.Now;
                    conjuntoAnuncioBO.FechaModificacion = DateTime.Now;
                    _repConjuntoAnuncioRepositorio.Insert(conjuntoAnuncioBO);
                    scope.Complete();                   
                }
                string rpta = "INSERTADO CORRECTAMENTE";
                return Ok(new { rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Actualizar([FromBody] ConjuntoAnuncioDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncioRepositorio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                ConjuntoAnuncioBO conjuntoAnuncioBO = new ConjuntoAnuncioBO();

                conjuntoAnuncioBO = _repConjuntoAnuncioRepositorio.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    conjuntoAnuncioBO.Nombre = Json.Nombre;
                    conjuntoAnuncioBO.IdCategoriaOrigen = Json.IdCategoriaOrigen;
                    conjuntoAnuncioBO.IdConjuntoAnuncioFacebook = Json.IdConjuntoAnuncioFacebook;
                    conjuntoAnuncioBO.UsuarioModificacion = Json.NombreUsuario;
                    conjuntoAnuncioBO.FechaModificacion = DateTime.Now;
                    _repConjuntoAnuncioRepositorio.Update(conjuntoAnuncioBO);
                    scope.Complete();
                }
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    ConjuntoAnuncioRepositorio _repConjuntoAnuncioRepositorio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                    
                    if (_repConjuntoAnuncioRepositorio.Exist(Json.Id))
                    {
                        _repConjuntoAnuncioRepositorio.Delete(Json.Id, Json.NombreUsuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
