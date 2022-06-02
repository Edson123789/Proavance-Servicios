using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: IndustriController
    /// Autor: ----------------
    /// Fecha: 04/03/2021
    /// <summary>
    /// Industria
    /// </summary>
    [Route("api/Industria")]
    public class IndustriaController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public IndustriaController(integraDBContext integraDBContext) {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Industria Filtro
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerIndustria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndustriaRepositorio repIndustria = new IndustriaRepositorio(_integraDBContext);
                return Ok(repIndustria.ObtenerIndustriaFiltro());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarIndustrias()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndustriaRepositorio _repIndustria = new IndustriaRepositorio(_integraDBContext);
                return Ok(_repIndustria.ObtenerAllIndustria());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarIndustria([FromBody] IndustriaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndustriaRepositorio _repIndustria = new IndustriaRepositorio(_integraDBContext);
                IndustriaBO NuevaIndustria = new IndustriaBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Descripcion = ObjetoDTO.Descripcion,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repIndustria.Insert(NuevaIndustria);
                return Ok(NuevaIndustria);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarIndustria([FromBody] IndustriaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndustriaRepositorio _repIndustria = new IndustriaRepositorio(_integraDBContext);
                IndustriaBO Industria = _repIndustria.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                Industria.Nombre = ObjetoDTO.Nombre;
                Industria.Descripcion = ObjetoDTO.Descripcion;
                Industria.Estado = true;
                Industria.UsuarioModificacion = ObjetoDTO.Usuario;
                Industria.FechaModificacion = DateTime.Now;

                _repIndustria.Update(Industria);

                return Ok(Industria);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarIndustria([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IndustriaRepositorio _repIndustria = new IndustriaRepositorio(_integraDBContext);
                IndustriaBO Industria = _repIndustria.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repIndustria.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
