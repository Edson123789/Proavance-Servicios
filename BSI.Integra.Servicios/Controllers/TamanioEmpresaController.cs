using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/TamanioEmpresa")]
	public class TamanioEmpresaController : Controller
	{
        private readonly integraDBContext _integraDBContext;
        public TamanioEmpresaController(integraDBContext integraDBContext)
		{
            _integraDBContext = integraDBContext;

        }

		[Route("[action]")]
		[HttpGet]
		public ActionResult ObtenerTodoFiltro()
		{
			try
			{
				TamanioEmpresaRepositorio _repTamanioEmpresa = new TamanioEmpresaRepositorio(_integraDBContext);
				return Ok(_repTamanioEmpresa.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre }));
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

		}


        #region CRUD
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarTamanioEmpresas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TamanioEmpresaRepositorio _repoTamanioEmpresa = new TamanioEmpresaRepositorio(_integraDBContext);
                var TamanioEmpresas = _repoTamanioEmpresa.ObtenerTodoTamanioEmpresaes();
                return Json(new { Result = "OK", Records = TamanioEmpresas });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarTamanioEmpresa([FromBody] TamanioEmpresaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TamanioEmpresaRepositorio _repoTamanioEmpresa = new TamanioEmpresaRepositorio(_integraDBContext);
                TamanioEmpresaBO NuevoTamanioEmpresa = new TamanioEmpresaBO();

                NuevoTamanioEmpresa.Nombre = ObjetoDTO.Nombre;
                NuevoTamanioEmpresa.Descripcion = ObjetoDTO.Descripcion;
                NuevoTamanioEmpresa.Estado = true;
                NuevoTamanioEmpresa.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevoTamanioEmpresa.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevoTamanioEmpresa.FechaCreacion = DateTime.Now;
                NuevoTamanioEmpresa.FechaModificacion = DateTime.Now;

                _repoTamanioEmpresa.Insert(NuevoTamanioEmpresa);

                return Ok(NuevoTamanioEmpresa);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarTamanioEmpresa([FromBody] TamanioEmpresaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TamanioEmpresaRepositorio _repoTamanioEmpresa = new TamanioEmpresaRepositorio(_integraDBContext);
                TamanioEmpresaBO TamanioEmpresa = _repoTamanioEmpresa.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                TamanioEmpresa.Nombre = ObjetoDTO.Nombre;
                TamanioEmpresa.Descripcion = ObjetoDTO.Descripcion;
                TamanioEmpresa.Estado = true;
                TamanioEmpresa.UsuarioModificacion = ObjetoDTO.Usuario;
                TamanioEmpresa.FechaModificacion = DateTime.Now;

                _repoTamanioEmpresa.Update(TamanioEmpresa);

                return Ok(TamanioEmpresa);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarTamanioEmpresa([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TamanioEmpresaRepositorio _repoTamanioEmpresa = new TamanioEmpresaRepositorio(_integraDBContext);
                _repoTamanioEmpresa.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }

    public class ValidadorTamanioEmpresaDTO : AbstractValidator<TTamanioEmpresa>
	{
		public static ValidadorTamanioEmpresaDTO Current = new ValidadorTamanioEmpresaDTO();
		public ValidadorTamanioEmpresaDTO()
		{
		}
	}
}

