using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaestroExamen")]
    public class MaestroExamenController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public MaestroExamenController()
        {
            _integraDBContext = new integraDBContext();
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerExamen()
        {
            try
            {
                ExamenRepositorio repExamenRep = new ExamenRepositorio();

                var listaExamen = repExamenRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre, Titulo = x.Titulo,RequiereTiempo=x.RequiereTiempo,DuracionMinutos=x.DuracionMinutos,Instrucciones=x.Instrucciones }).OrderByDescending(w => w.Id).ToList();

                return Ok(listaExamen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarExamen([FromBody] EliminarDTO eliminar)
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
                    ExamenRepositorio examenRep = new ExamenRepositorio(_integraDBContext);
                    if (examenRep.Exist(eliminar.Id))
                    {
                        examenRep.Delete(eliminar.Id, eliminar.NombreUsuario);
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

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarExamen([FromBody] ExamenDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenRepositorio repCajaRep = new ExamenRepositorio();
                ExamenBO examen = new ExamenBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    examen.Nombre = Json.Nombre;
                    examen.Titulo = Json.Titulo;
                    examen.RequiereTiempo = Json.RequiereTiempo;
                    examen.DuracionMinutos = Json.DuracionMinutos;
                    examen.Instrucciones = Json.Instrucciones;
                    examen.Estado = true;
                    examen.UsuarioCreacion = Json.Usuario;
                    examen.FechaCreacion = DateTime.Now;
                    examen.UsuarioModificacion = Json.Usuario;
                    examen.FechaModificacion = DateTime.Now;

                    repCajaRep.Insert(examen);
                    Json.Id = examen.Id;
                    scope.Complete();
                }

                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarExamen([FromBody] ExamenDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenRepositorio repExamenRep = new ExamenRepositorio();
                ExamenBO examen = new ExamenBO();
                examen = repExamenRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    examen.Nombre = Json.Nombre;
                    examen.Titulo = Json.Titulo;
                    examen.RequiereTiempo = Json.RequiereTiempo;
                    examen.DuracionMinutos = Json.DuracionMinutos;
                    examen.Instrucciones = Json.Instrucciones;

                    examen.UsuarioModificacion = Json.Usuario;
                    examen.FechaModificacion = DateTime.Now;
                    repExamenRep.Update(examen);
                    scope.Complete();
                }
                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarFactorComponente([FromBody] FactorComponenteDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);
				var examen = _repExamen.FirstById(Json.IdExamen);
				examen.Factor = Json.FactorComponente;
				examen.UsuarioModificacion = Json.Usuario;
				examen.FechaModificacion = DateTime.Now;
				var estado = _repExamen.Update(examen);
				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerComponentesPorEvaluacion([FromBody] int IdEvaluacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<ComponenteAsociadoEvaluacion> listaExamen;
				if (IdEvaluacion > 0)
				{
					ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);
					listaExamen = _repExamen.ObtenerComponentesAsociadosEvaluacion(IdEvaluacion);
				}
				else
				{
					listaExamen = new List<ComponenteAsociadoEvaluacion>();
				}

				return Ok(listaExamen);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

	}
}