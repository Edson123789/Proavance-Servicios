using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
//using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AreaCC")]
    public class AreaCCController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AreaCCController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRegistrosAreaCentroCosto([FromBody]FiltroCompuestroGrillaDTO dtoFiltro)
        {
            try
            {
                AreaCcRepositorio _repAreaCCosto = new AreaCcRepositorio(_integraDBContext);

                AreaFiltroCentroCostoDTO filtro = new AreaFiltroCentroCostoDTO();

                if (dtoFiltro.filter == null)
                {
                    filtro.Area = "_";
                    filtro.Skip = dtoFiltro.paginador.skip;
                    filtro.Take = dtoFiltro.paginador.take;
                }
                else
                {
                    filtro.Area = dtoFiltro.filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault() == null ? "_" : dtoFiltro.filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault().Value;
                    filtro.Skip = dtoFiltro.paginador.skip;
                    filtro.Take = dtoFiltro.paginador.take;
                }

                var RegistroAreas = _repAreaCCosto.ObtenerAreaCentroCostoLista(filtro);
                return Ok(new { data = RegistroAreas, Total = RegistroAreas.FirstOrDefault().Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Route("[Action]/{Id}")]
        [HttpPost]
        public ActionResult ActualizarAreaCentroCosto([FromBody]AreaCentroCostoDTO Objeto, int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCcRepositorio _repAreaCCostoCentroCosto = new AreaCcRepositorio(_integraDBContext);

                AreaCcBO AreaCentroCosto = new AreaCcBO(Id);

                AreaCentroCosto.Id = Id;
                AreaCentroCosto.Nombre = Objeto.Nombre;
                AreaCentroCosto.Codigo = Objeto.Codigo;
                AreaCentroCosto.FechaModificacion = DateTime.Now;
                AreaCentroCosto.UsuarioModificacion = Objeto.UsuarioModificacion;
                AreaCentroCosto.UsuarioCreacion = Objeto.UsuarioCreacion;

                if (!AreaCentroCosto.HasErrors)
                {
                    var rpta = _repAreaCCostoCentroCosto.Update(AreaCentroCosto);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(AreaCentroCosto.GetErrors(null));
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/")]
        [HttpPost]
        public ActionResult EliminarAreaCentroCosto([FromBody]AreaCentroCostoDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCcRepositorio _repAreaCCostoCentroCosto = new AreaCcRepositorio(_integraDBContext);

                AreaCcBO AreaCentroCosto = new AreaCcBO(Objeto.Id);

                AreaCentroCosto.Id = Objeto.Id;
                AreaCentroCosto.FechaModificacion = DateTime.Now;
                AreaCentroCosto.UsuarioModificacion = Objeto.UsuarioModificacion;
                AreaCentroCosto.Estado = false;

                if (!AreaCentroCosto.HasErrors)
                {
                    var rpta = _repAreaCCostoCentroCosto.Update(AreaCentroCosto);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(AreaCentroCosto.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]/")]
        [HttpPost]
        public ActionResult InsertarAreaCentroCosto([FromBody]AreaCentroCostoDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCcRepositorio _repAreaCCostoCentroCosto = new AreaCcRepositorio(_integraDBContext);

                AreaCcBO AreaCentroCosto = new AreaCcBO();
                AreaCentroCosto.Id = Objeto.Id;
                AreaCentroCosto.Nombre = Objeto.Nombre;
                AreaCentroCosto.Codigo = Objeto.Codigo;
                AreaCentroCosto.Estado = true;
                AreaCentroCosto.FechaModificacion = DateTime.Now;
                AreaCentroCosto.FechaCreacion = DateTime.Now;
                AreaCentroCosto.UsuarioModificacion = Objeto.UsuarioModificacion;
                AreaCentroCosto.UsuarioCreacion = Objeto.UsuarioCreacion;

               if (!AreaCentroCosto.HasErrors)
                {
                    var rpta = _repAreaCCostoCentroCosto.Update(AreaCentroCosto);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(AreaCentroCosto.GetErrors(null));
                }



            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
