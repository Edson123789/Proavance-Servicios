using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/Operaciones/Sesion")]
	public class SesionOpeController : Controller
	{


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosFiltros()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio();
                MonedaRepositorio _repMoneda = new MonedaRepositorio();
                RaTipoCursoRepositorio _repTipoCurso = new RaTipoCursoRepositorio();
                RaTipoContratoRepositorio _repTipoContrato = new RaTipoContratoRepositorio();
                var listaExpositor = _repExpositor.ObtenerFiltroExpositor();
                var listaMoneda = _repMoneda.ObtenerFiltroMoneda();
                var listadoTipoCurso = _repTipoCurso.ObtenerFiltroTipoCurso();
                var listadoTipoContrato = _repTipoContrato.ObtenerFiltroTipoContrato();
                return Ok(new { Expositor = listaExpositor, Moneda = listaMoneda, TipoCurso = listadoTipoCurso, TipoContrato = listadoTipoContrato });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


		//por ver?
		// GET: api/Sesion
		[HttpGet]
		public ActionResult ObtenerDocentes()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ToString());
			}
			try
			{
				ExpositorRepositorio _repExpositor = new ExpositorRepositorio();
				var listaDocentes = _repExpositor.GetBy(x=>x.Estado == true).ToList();
				return Ok(listaDocentes);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

	}
}
