using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ElementoCategoria")]
    public class ElementoCategoriaController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ElementoCategoriaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerComboElementoCategoria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoCategoriaRepositorio _repElementoCategoria = new ElementoCategoriaRepositorio(_integraDBContext);
                return Ok(_repElementoCategoria.ObtenerElementoCategoriaFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerRegistrosElementoCategoria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoCategoriaRepositorio _repElementoCategoria = new ElementoCategoriaRepositorio(_integraDBContext);
                return Ok(_repElementoCategoria.ObtenerTodoElementoCategoria());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarElementoCategoria([FromBody] ElementoCategoriaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoCategoriaRepositorio _repElementoCategoria = new ElementoCategoriaRepositorio(_integraDBContext);
                ElementoCategoriaBO ElementoCategoria = new ElementoCategoriaBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Descripcion = ObjetoDTO.Descripcion,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repElementoCategoria.Insert(ElementoCategoria);
                return Ok(ElementoCategoria);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarElementoCategoria([FromBody] ElementoCategoriaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoCategoriaRepositorio _repElementoCategoria = new ElementoCategoriaRepositorio(_integraDBContext);
                ElementoCategoriaBO ElementoCategoria = _repElementoCategoria.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                ElementoCategoria.Nombre = ObjetoDTO.Nombre;
                ElementoCategoria.Descripcion = ObjetoDTO.Descripcion;
                ElementoCategoria.Estado = true;
                ElementoCategoria.UsuarioModificacion = ObjetoDTO.Usuario;
                ElementoCategoria.FechaModificacion = DateTime.Now;

                _repElementoCategoria.Update(ElementoCategoria);

                return Ok(ElementoCategoria);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarElementoCategoria([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoCategoriaRepositorio _repElementoCategoria = new ElementoCategoriaRepositorio(_integraDBContext);
                ElementoCategoriaBO ElementoCategoria = _repElementoCategoria.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repElementoCategoria.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
