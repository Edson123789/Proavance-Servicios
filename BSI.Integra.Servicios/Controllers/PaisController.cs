using System;
using System.Linq;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Configuracion/Pais
    /// Autor: Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Configura las acciones para todo lo relacionado con el Pais
    /// </summary>
    [Route("api/Pais")]
    public class PaisController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public PaisController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPaisIdNombre()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                return Ok(_repPais.ObtenerListaPais());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los paises habilitados en una lista de objetos de clase PaisFiltroDTO
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase lista de objetos de clase PaisFiltroDTO, caso contrario response 400 con el mensaje de error respectivo</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodosPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                return Ok(_repPais.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		[Route("[action]")]
		[HttpGet]
		public ActionResult ObtenerTodoFiltro()
		{
			try
			{
				PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
				return Ok(_repPais.GetBy(x =>x.Estado == true, x => new { x.CodigoPais, x.NombrePais }));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                var Pais = _repPais.ObtenerTodoPaises();
                return Json(new { Result = "OK", Records = Pais });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarPais([FromBody] PaisDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                PaisBO NuevoPais = new PaisBO
                {
                    CodigoPais = ObjetoDTO.CodigoPais,
                    CodigoIso = ObjetoDTO.CodigoIso,
                    NombrePais = ObjetoDTO.NombrePais,
                    Moneda = ObjetoDTO.Moneda,
                    ZonaHoraria = ObjetoDTO.ZonaHoraria,
                    EstadoPublicacion = ObjetoDTO.EstadoPublicacion,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };

                _repPais.Insert(NuevoPais);

                return Ok(NuevoPais);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarPais([FromBody] PaisDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                PaisBO Pais = _repPais.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                Pais.CodigoPais = ObjetoDTO.CodigoPais;
                Pais.CodigoIso = ObjetoDTO.CodigoIso;
                Pais.NombrePais = ObjetoDTO.NombrePais;
                Pais.Moneda = ObjetoDTO.Moneda;
                Pais.ZonaHoraria = ObjetoDTO.ZonaHoraria;
                Pais.EstadoPublicacion = ObjetoDTO.EstadoPublicacion;
                Pais.UsuarioModificacion = ObjetoDTO.Usuario;
                Pais.FechaModificacion = DateTime.Now;
                Pais.Estado = true;

                //_repPais.Update(Pais);
                _repPais.ActualizarPais(Pais);

                return Ok(Pais);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarPais([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                PaisBO Pais = _repPais.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                //_repPais.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                _repPais.EliminarPais(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
