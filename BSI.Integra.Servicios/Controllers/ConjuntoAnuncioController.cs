using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConjuntoAnuncio")]
    public class ConjuntoAnuncioController : BaseController<ConjuntoAnuncioBO, ValidadorConjuntoAnuncioDTO>
    {
        public ConjuntoAnuncioController(IIntegraRepository<ConjuntoAnuncioBO> repositorio, ILogger<BaseController<ConjuntoAnuncioBO, ValidadorConjuntoAnuncioDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerConjuntoAnuncioPanel([FromBody] FiltroPaginadorDTO Filtro) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncios = new ConjuntoAnuncioRepositorio();
                var valor = true;
                var lista = _repConjuntoAnuncios.ListarConjuntoAnuncios(Filtro);
                var registro = lista.FirstOrDefault();

                return Ok(new { Data = lista, Total = registro.Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosConjuntoAnuncio()
        {

            try
            {
                CategoriaOrigenRepositorio repCategoriaOrigen = new CategoriaOrigenRepositorio();
                PgeneralBO pgeneral = new PgeneralBO();
                ComboConjuntoAnuncioDTO combos = new ComboConjuntoAnuncioDTO();
                combos.ProgramasGenerales = pgeneral.ObtenerProgramaConUrlPortal();
                combos.CategoriasOrigen = repCategoriaOrigen.ObtenerCategoriaFiltro();
                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPut]
        public IActionResult ActualizarConjuntoAnuncio([FromBody] CompuestoConjuntoAnuncioDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
                ConjuntoAnuncioBO conjuntoAnuncio = new ConjuntoAnuncioBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    if(repConjuntoAnuncio.Exist(Json.ConjuntoAnuncio.Id)){
                        conjuntoAnuncio = repConjuntoAnuncio.FirstById(Json.ConjuntoAnuncio.Id);
                        conjuntoAnuncio.Nombre = Json.ConjuntoAnuncio.Nombre;
                        conjuntoAnuncio.IdCategoriaOrigen = Json.ConjuntoAnuncio.IdProveedor;
                        conjuntoAnuncio.IdConjuntoAnuncioFacebook = Json.ConjuntoAnuncio.IdCampaniaFacebook;
                        conjuntoAnuncio.UsuarioModificacion = Json.Usuario;
                        conjuntoAnuncio.FechaModificacion = DateTime.Now;
                        repConjuntoAnuncio.Update(conjuntoAnuncio);
                    }
                    scope.Complete();
                    Json.ConjuntoAnuncio.Id = conjuntoAnuncio.Id;
                }
                

                return Ok(Json.ConjuntoAnuncio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{IdConjuntoAnuncio}/{Usuario}")]
        [HttpDelete]
        public IActionResult EliminarConjuntoAnuncio(int IdConjuntoAnuncio, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (repConjuntoAnuncio.Exist(IdConjuntoAnuncio))
                    {
                        repConjuntoAnuncio.Delete(IdConjuntoAnuncio, Usuario);
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarConjuntoAnuncio([FromBody] CompuestoConjuntoAnuncioDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
                ConjuntoAnuncioBO conjuntoAnuncio = new ConjuntoAnuncioBO();
                using (TransactionScope scope = new TransactionScope())
                {

                    conjuntoAnuncio.Nombre = Json.ConjuntoAnuncio.Nombre;
                    conjuntoAnuncio.IdCategoriaOrigen = Json.ConjuntoAnuncio.IdProveedor;
                    conjuntoAnuncio.IdConjuntoAnuncioFacebook = Json.ConjuntoAnuncio.IdCampaniaFacebook;
                    conjuntoAnuncio.FechaCreacionCampania = DateTime.Now;
                    conjuntoAnuncio.UsuarioCreacion = Json.Usuario;
                    conjuntoAnuncio.UsuarioModificacion = Json.Usuario;
                    conjuntoAnuncio.FechaCreacion = DateTime.Now;
                    conjuntoAnuncio.FechaModificacion = DateTime.Now;
                    conjuntoAnuncio.Estado = true;
                    repConjuntoAnuncio.Insert(conjuntoAnuncio);
                    scope.Complete();
                }
                Json.ConjuntoAnuncio.Id = conjuntoAnuncio.Id;

                return Ok(Json.ConjuntoAnuncio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
    public class ValidadorConjuntoAnuncioDTO : AbstractValidator<ConjuntoAnuncioBO>
    {
        public static ValidadorConjuntoAnuncioDTO Current = new ValidadorConjuntoAnuncioDTO();
        public ValidadorConjuntoAnuncioDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
        }
    }
}
