using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ElementoSubCategoria")]
    public class ElementoSubCategoriaController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ElementoSubCategoriaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerRegistrosElementoSubCategoria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoSubCategoriaRepositorio _repElementoSubCategoria = new ElementoSubCategoriaRepositorio(_integraDBContext);
                return Ok(_repElementoSubCategoria.ObtenerTodoElementoSubCategoria());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerComboElementoSubCategoria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoSubCategoriaRepositorio _repElementoSubCategoria = new ElementoSubCategoriaRepositorio(_integraDBContext);
                return Ok(_repElementoSubCategoria.ObtenerElementoSubCategoriaFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarElementoSubCategoria([FromBody] ElementoSubCategoriaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoSubCategoriaRepositorio _repElementoSubCategoria = new ElementoSubCategoriaRepositorio(_integraDBContext);
                ElementoSubCategoriaBO ElementoSubCategoria = new ElementoSubCategoriaBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Descripcion = ObjetoDTO.Descripcion,
                    IdElementoCategoria = ObjetoDTO.IdElementoCategoria,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repElementoSubCategoria.Insert(ElementoSubCategoria);
                return Ok(_repElementoSubCategoria.ObtenerRegistroElementoSubCategoria(ElementoSubCategoria.Id).FirstOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarElementoSubCategoria([FromBody] ElementoSubCategoriaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoSubCategoriaRepositorio _repElementoSubCategoria = new ElementoSubCategoriaRepositorio(_integraDBContext);
                ElementoSubCategoriaBO ElementoSubCategoria = _repElementoSubCategoria.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                ElementoSubCategoria.Nombre = ObjetoDTO.Nombre;
                ElementoSubCategoria.Descripcion = ObjetoDTO.Descripcion;
                ElementoSubCategoria.IdElementoCategoria = ObjetoDTO.IdElementoCategoria;
                ElementoSubCategoria.Estado = true;
                ElementoSubCategoria.UsuarioModificacion = ObjetoDTO.Usuario;
                ElementoSubCategoria.FechaModificacion = DateTime.Now;

                _repElementoSubCategoria.Update(ElementoSubCategoria);

                return Ok(_repElementoSubCategoria.ObtenerRegistroElementoSubCategoria(ElementoSubCategoria.Id).FirstOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarElementoSubCategoria([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoSubCategoriaRepositorio _repElementoSubCategoria = new ElementoSubCategoriaRepositorio(_integraDBContext);
                ElementoSubCategoriaBO ElementoSubCategoria = _repElementoSubCategoria.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repElementoSubCategoria.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
