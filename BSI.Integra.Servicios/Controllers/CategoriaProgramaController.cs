using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CategoriaPrograma")]
    public class CategoriaProgramaController : BaseController<TCategoriaPrograma, ValidadorCategoriaProgramaDTO>
    {
        public CategoriaProgramaController(IIntegraRepository<TCategoriaPrograma> repositorio, ILogger<BaseController<TCategoriaPrograma, ValidadorCategoriaProgramaDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCategoriaPrograma()
        {
            try
            {
                CategoriaProgramaRepositorio _repCategoriaPrograma = new CategoriaProgramaRepositorio();
                var listaCategoriaPrograma = _repCategoriaPrograma.ListarCategoriasProgramasPanel();

                return Ok(listaCategoriaPrograma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarCategoriaPrograma([FromBody] CategoriaProgramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaProgramaRepositorio repCategoriaPrograma = new CategoriaProgramaRepositorio();

                CategoriaProgramaBO categoriaPrograma = new CategoriaProgramaBO();
                categoriaPrograma.Categoria = Json.Categoria;
                categoriaPrograma.Visible = Json.Visible;
                categoriaPrograma.UsuarioCreacion = Json.Usuario;
                categoriaPrograma.UsuarioModificacion = Json.Usuario;
                categoriaPrograma.FechaCreacion = DateTime.Now;
                categoriaPrograma.FechaModificacion = DateTime.Now;
                categoriaPrograma.Estado = true;

                var idTipoPago = repCategoriaPrograma.Insert(categoriaPrograma);
                return Ok(categoriaPrograma);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarCategoriaPrograma([FromBody] CategoriaProgramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaProgramaRepositorio repCategoriaPrograma = new CategoriaProgramaRepositorio();
                bool result = false;

                if (repCategoriaPrograma.Exist(Json.Id))
                {
                    CategoriaProgramaBO categoriaPrograma = repCategoriaPrograma.FirstById(Json.Id);

                    categoriaPrograma.Categoria = Json.Categoria;
                    categoriaPrograma.Visible = Json.Visible;
                    categoriaPrograma.UsuarioModificacion = Json.Usuario;
                    categoriaPrograma.FechaModificacion = DateTime.Now;

                    result = repCategoriaPrograma.Update(categoriaPrograma);

                }

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarCategoriaPrograma(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaProgramaRepositorio repCategoriaPrograma = new CategoriaProgramaRepositorio();
                bool result = false;
                if (repCategoriaPrograma.Exist(Id))
                {
                    result = repCategoriaPrograma.Delete(Id, Usuario);
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorCategoriaProgramaDTO : AbstractValidator<TCategoriaPrograma>
    {
        public static ValidadorCategoriaProgramaDTO Current = new ValidadorCategoriaProgramaDTO();
        public ValidadorCategoriaProgramaDTO()
        {
            RuleFor(objeto => objeto.Categoria).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
        }
    }
}
