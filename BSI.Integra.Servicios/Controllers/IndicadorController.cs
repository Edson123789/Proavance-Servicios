using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Indicador")]
    public class IndicadorController : BaseController<TIndicador, ValidadorIndicadorDTO>
    {
        public IndicadorController(IIntegraRepository<TIndicador> repositorio, ILogger<BaseController<TIndicador, ValidadorIndicadorDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCategoriasIndicador()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CategoriaIndicadorRepositorio _repoCategoriaIndicador = new CategoriaIndicadorRepositorio();
                var CategoriaIndicadores = _repoCategoriaIndicador.ObtenerTodoCategoriaIndicador();
                return Json(new { Result = "OK", Records = CategoriaIndicadores });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarIndicadores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IndicadorRepositorio _repoIndicador = new IndicadorRepositorio();
                var Indicadores = _repoIndicador.ObtenerTodoIndicadores();
                return Json(new { Result = "OK", Records = Indicadores });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarIndicador([FromBody] IndicadorDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndicadorRepositorio _repoIndicador = new IndicadorRepositorio();
                IndicadorBO NuevaIndicador = new IndicadorBO();

                NuevaIndicador.Nombre = ObjetoDTO.Nombre;
                NuevaIndicador.Meta = ObjetoDTO.Meta;
                NuevaIndicador.Verificacion = ObjetoDTO.Verificacion;
                NuevaIndicador.IdCategoriaIndicador = ObjetoDTO.IdCategoriaIndicador;
                NuevaIndicador.Estado = true;
                NuevaIndicador.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevaIndicador.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevaIndicador.FechaCreacion = DateTime.Now;
                NuevaIndicador.FechaModificacion = DateTime.Now;

                _repoIndicador.Insert(NuevaIndicador);

                return Ok(NuevaIndicador);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarIndicador([FromBody] IndicadorDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndicadorRepositorio _repoIndicador = new IndicadorRepositorio();
                IndicadorBO Indicador = _repoIndicador.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                Indicador.Nombre = ObjetoDTO.Nombre;
                Indicador.Meta = ObjetoDTO.Meta;
                Indicador.Verificacion = ObjetoDTO.Verificacion;
                Indicador.IdCategoriaIndicador = ObjetoDTO.IdCategoriaIndicador;
                Indicador.Estado = true;
                Indicador.UsuarioModificacion = ObjetoDTO.Usuario;
                Indicador.FechaModificacion = DateTime.Now;

                _repoIndicador.Update(Indicador);

                return Ok(Indicador);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{UserName}/{Id}")]
        [HttpDelete]
        public ActionResult EliminarIndicador(int Id, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndicadorRepositorio _repoIndicador = new IndicadorRepositorio();
                _repoIndicador.Delete(Id, UserName);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }




    public class ValidadorIndicadorDTO : AbstractValidator<TIndicador>
    {
        public static ValidadorIndicadorDTO Current = new ValidadorIndicadorDTO();
        public ValidadorIndicadorDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
            
        }
    }
}
