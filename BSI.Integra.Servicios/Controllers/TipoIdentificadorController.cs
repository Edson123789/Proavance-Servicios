using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Finanzas.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/TipoIdentificador")]
	public class TipoIdentificadorController : Controller
	{
        private readonly integraDBContext _integraDBContext;
        public TipoIdentificadorController(integraDBContext integraDBContext)
		{
            _integraDBContext = integraDBContext;

        }

		[Route("[action]")]
		[HttpGet]
		public ActionResult ObtenerTodoFiltro()
		{
			try
			{
				TipoIdentificadorRepositorio _repTipoIdentificador = new TipoIdentificadorRepositorio(_integraDBContext);
				return Ok(_repTipoIdentificador.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.IdPais}));
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}


        #region CRUD TipoIdentificador
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPaises()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repoPais = new PaisRepositorio(_integraDBContext);
                var Paises = _repoPais.ObtenerPaisesCombo();
                return Json(new { Result = "OK", Records = Paises });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarTipoIdentificadores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TipoIdentificadorRepositorio _repoTipoIdentificador = new TipoIdentificadorRepositorio(_integraDBContext);
                var TipoIdentificadores = _repoTipoIdentificador.ObtenerTodoTipoIdentificador();
                return Json(new { Result = "OK", Records = TipoIdentificadores });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarTipoIdentificador([FromBody] TipoIdentificadorDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoIdentificadorRepositorio _repoTipoIdentificador = new TipoIdentificadorRepositorio(_integraDBContext);
                TipoIdentificadorBO NuevoTipoIdentificador = new TipoIdentificadorBO();

                NuevoTipoIdentificador.Nombre = ObjetoDTO.Nombre;
                NuevoTipoIdentificador.Descripcion = ObjetoDTO.Descripcion;
                NuevoTipoIdentificador.IdPais = ObjetoDTO.IdPais;
                NuevoTipoIdentificador.Estado = true;
                NuevoTipoIdentificador.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevoTipoIdentificador.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevoTipoIdentificador.FechaCreacion = DateTime.Now;
                NuevoTipoIdentificador.FechaModificacion = DateTime.Now;

                _repoTipoIdentificador.Insert(NuevoTipoIdentificador);

                return Ok(NuevoTipoIdentificador);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarTipoIdentificador([FromBody] TipoIdentificadorDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoIdentificadorRepositorio _repoTipoIdentificador = new TipoIdentificadorRepositorio(_integraDBContext);
                TipoIdentificadorBO TipoIdentificador = _repoTipoIdentificador.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                TipoIdentificador.Nombre = ObjetoDTO.Nombre;
                TipoIdentificador.Descripcion = ObjetoDTO.Descripcion;
                TipoIdentificador.IdPais = ObjetoDTO.IdPais;
                TipoIdentificador.Estado = true;
                TipoIdentificador.UsuarioModificacion = ObjetoDTO.Usuario;
                TipoIdentificador.FechaModificacion = DateTime.Now;

                _repoTipoIdentificador.Update(TipoIdentificador);

                return Ok(TipoIdentificador);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarTipoIdentificador([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoIdentificadorRepositorio _repoTipoIdentificador = new TipoIdentificadorRepositorio(_integraDBContext);
                _repoTipoIdentificador.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }

    public class ValidadorTipoIdentificadorDTO : AbstractValidator<TTipoIdentificador>
	{
		public static ValidadorTipoIdentificadorDTO Current = new ValidadorTipoIdentificadorDTO();
		public ValidadorTipoIdentificadorDTO()
		{
		}
	}
}
