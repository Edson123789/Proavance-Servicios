using System;
using System.Linq;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AsociarPlanContableRubro")]
    public class AsociarPlanContableRubroController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public AsociarPlanContableRubroController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }



        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosRubro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurTipoSolicitudRepositorio _repoFurTipoSolicitud = new FurTipoSolicitudRepositorio(_integraDBContext);
                return Ok(_repoFurTipoSolicitud.ObtenerFurTipoSolicitud());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarPlanContableRubro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlanContableRepositorio _repoPlanContableRepositorio = new PlanContableRepositorio(_integraDBContext);
                return Ok(_repoPlanContableRepositorio.ObtenerPlanContableConRubro());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAsociarPlanContableRubro([FromBody] PlanContableConRubroRequestDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlanContableRepositorio _repoPlanContable = new PlanContableRepositorio(_integraDBContext);
                var Registros = _repoPlanContable.GetBy(X => X.Id == ObjetoDTO.IdPlanContable).ToList();

                if (Registros == null || Registros.Count<1) throw new Exception("No se encuentra el PlanContable que se desea actualizar ¿Id Correcto?");
                if (Registros.Count > 1) throw new Exception("Mas de Un Registro se actualizara ID duplicado");

                var PlanContable = Registros[0];

                PlanContable.IdFurTipoSolicitud = ObjetoDTO.IdFurTipoSolicitud;
                _repoPlanContable.Update(PlanContable);

                return Ok(_repoPlanContable.ObtenerPlanContableConRubro(PlanContable.Id)[0]);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
