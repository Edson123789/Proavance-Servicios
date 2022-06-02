using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Locacion")]
    public class LocacionController : BaseController<TLocacion, ValidadorLocacionDTO>
    {
        public LocacionController(IIntegraRepository<TLocacion> repositorio, ILogger<BaseController<TLocacion, ValidadorLocacionDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                LocacionRepositorio locacionRepositorio = new LocacionRepositorio();
                return Ok(locacionRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio();
                CiudadRepositorio _repCiudad = new CiudadRepositorio();
                RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();

                CombosLocacionDTO combos = new CombosLocacionDTO();

                combos.PaisFiltroParaCombo = _repPais.ObtenerPaisFiltro();
                combos.CiudadFiltroCombo = _repCiudad.ObtenerCiudadesFiltro();
                combos.RegionCiudadFiltroCombo = _repRegionCiudad.ObtenerRegionCiudadFiltro();

                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] LocacionFiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LocacionRepositorio locacionRepositorio = new LocacionRepositorio();

                LocacionBO locacionBO = new LocacionBO();
                locacionBO.Nombre = Json.Nombre;
                locacionBO.IdPais = Json.IdPais;
                locacionBO.IdRegion = Json.IdRegion;
                locacionBO.IdCiudad = Json.IdCiudad;
                locacionBO.Direccion = Json.Direccion;
                locacionBO.Estado = true;
                locacionBO.UsuarioCreacion = Json.Usuario;
                locacionBO.UsuarioModificacion = Json.Usuario;
                locacionBO.FechaCreacion = DateTime.Now;
                locacionBO.FechaModificacion = DateTime.Now;

                return Ok(locacionRepositorio.Insert(locacionBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] LocacionFiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LocacionRepositorio locacionRepositorio = new LocacionRepositorio();

                LocacionBO locacionBO = locacionRepositorio.FirstById(Json.Id);
                locacionBO.Nombre = Json.Nombre;
                locacionBO.IdPais = Json.IdPais;
                locacionBO.IdRegion = Json.IdRegion;
                locacionBO.IdCiudad = Json.IdCiudad;
                locacionBO.Direccion = Json.Direccion;
                locacionBO.UsuarioModificacion = Json.Usuario;
                locacionBO.FechaModificacion = DateTime.Now;

                return Ok(locacionRepositorio.Update(locacionBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar([FromBody]LocacionFiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LocacionRepositorio locacionRepositorio = new LocacionRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (locacionRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = locacionRepositorio.Delete(Json.Id, Json.Usuario);
                    }

                    scope.Complete();
                }
                return Ok(estadoEliminacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }

    public class ValidadorLocacionDTO : AbstractValidator<TLocacion>
    {
        public static ValidadorLocacionDTO Current = new ValidadorLocacionDTO();
        public ValidadorLocacionDTO()
        {

            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.IdPais).NotEmpty().WithMessage("IdPais es Obligatorio");

            RuleFor(objeto => objeto.IdRegion).NotEmpty().WithMessage("IdRegion es Obligatorio");

            RuleFor(objeto => objeto.IdCiudad).NotEmpty().WithMessage("IdCiudad es Obligatorio");

            RuleFor(objeto => objeto.Direccion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 100).WithMessage("Descripcion debe tener 1 caracter minimo y 100 maximo");

        }
    }
}
