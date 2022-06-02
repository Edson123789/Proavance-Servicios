using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/SubNivelCC")]
    public class SubNivelCCBOController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public SubNivelCCBOController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRegistrosSubAreaCentroCosto([FromBody]FiltroCompuestroGrillaDTO dtoFiltro)
        {
            try
            {
                SubNivelCcRepositorio _repSubNivelCCosto = new SubNivelCcRepositorio(_integraDBContext);

                FiltroSubNivelCentroCosto filtro = new FiltroSubNivelCentroCosto();

                if (dtoFiltro.filter == null)
                {
                    filtro.SubNivel = "_";
                    filtro.Skip = dtoFiltro.paginador.skip;
                    filtro.Take = dtoFiltro.paginador.take;
                }
                else
                {
                    filtro.SubNivel = dtoFiltro.filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault() == null ? "_" : dtoFiltro.filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault().Value;
                    filtro.Skip = dtoFiltro.paginador.skip;
                    filtro.Take = dtoFiltro.paginador.take;
                }

                var RegistroSubAreas = _repSubNivelCCosto.ObtenerSubNivelCentroCostoLista(filtro);
                return Ok(new { data = RegistroSubAreas, Total = RegistroSubAreas.FirstOrDefault().Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult GetAllAreaCC()
        {
            AreaCcBO Objeto = new AreaCcBO();
            Objeto.GetAllAreaCC();

            return Ok(new {data = Objeto.AreaCC});
        }




        [Route("[Action]/{Id}")]
        [HttpPost]
        public ActionResult ActualizarSubAreaCentroCosto([FromBody]SubNivelCentroCostoDTO Objeto, int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubNivelCcRepositorio _repSubNivelCCosto = new SubNivelCcRepositorio(_integraDBContext);

                SubNivelCcBO SubAreaCentroCosto = new SubNivelCcBO(Id);

                SubAreaCentroCosto.Id = Id;
                SubAreaCentroCosto.IdAreaCc = Objeto.IdAreaCC;
                SubAreaCentroCosto.Nombre = Objeto.Nombre;
                SubAreaCentroCosto.Codigo = Objeto.Codigo;
                SubAreaCentroCosto.FechaModificacion = DateTime.Now;
                SubAreaCentroCosto.UsuarioModificacion = Objeto.UsuarioModificacion;
                SubAreaCentroCosto.UsuarioCreacion = Objeto.UsuarioCreacion;

                if (!SubAreaCentroCosto.HasErrors)
                {
                    var rpta = _repSubNivelCCosto.Update(SubAreaCentroCosto);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(SubAreaCentroCosto.GetErrors(null));
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/")]
        [HttpPost]
        public ActionResult InsertarSubAreaCentroCosto([FromBody]SubNivelCentroCostoDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubNivelCcRepositorio _repSubNivelCCosto = new SubNivelCcRepositorio(_integraDBContext);

                SubNivelCcBO SubAreaCentroCosto = new SubNivelCcBO();
                SubAreaCentroCosto.Id = Objeto.Id;
                SubAreaCentroCosto.IdAreaCc = Objeto.IdAreaCC;
                SubAreaCentroCosto.Nombre = Objeto.Nombre;
                SubAreaCentroCosto.Codigo = Objeto.Codigo;
                SubAreaCentroCosto.Estado = true;
                SubAreaCentroCosto.FechaModificacion = DateTime.Now;
                SubAreaCentroCosto.FechaCreacion = DateTime.Now;
                SubAreaCentroCosto.UsuarioModificacion = Objeto.UsuarioModificacion;
                SubAreaCentroCosto.UsuarioCreacion = Objeto.UsuarioCreacion;


                if (!SubAreaCentroCosto.HasErrors)
                {
                    var rpta = _repSubNivelCCosto.Update(SubAreaCentroCosto);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(SubAreaCentroCosto.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


    }

    public class ValidadorSubNivelCcDTO : AbstractValidator<TSubNivelCc>
    {
        public static ValidadorSubNivelCcDTO Current = new ValidadorSubNivelCcDTO();
        public ValidadorSubNivelCcDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.IdAreaCc).NotEmpty().WithMessage("IdAreaCC es Obligatorio");

            RuleFor(objeto => objeto.Codigo).NotEmpty().WithMessage("Codigo es Obligatorio");

        }
    }
}
