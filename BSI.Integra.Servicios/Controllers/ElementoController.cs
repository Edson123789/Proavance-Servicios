using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Elemento")]
    public class ElementoController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ElementoController(integraDBContext integraDBContext) {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerRegistrosElemento()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoRepositorio _repElemento = new ElementoRepositorio(_integraDBContext);
                return Ok(_repElemento.ObtenerTodoElemento());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarElemento([FromBody] ElementoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoRepositorio _repElemento = new ElementoRepositorio(_integraDBContext);
                ElementoBO Elemento = new ElementoBO
                {
                    Codigo = ObjetoDTO.Codigo,
                    Nombre = ObjetoDTO.Nombre,
                    Descripcion = ObjetoDTO.Descripcion,
                    IdElementoSubCategoria = ObjetoDTO.IdElementoSubCategoria,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repElemento.Insert(Elemento);
                return Ok(_repElemento.ObtenerRegistroElemento(Elemento.Id).FirstOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarElemento([FromBody] ElementoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoRepositorio _repElemento = new ElementoRepositorio(_integraDBContext);
                ElementoBO Elemento = _repElemento.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                Elemento.Nombre = ObjetoDTO.Nombre;
                Elemento.Codigo = ObjetoDTO.Codigo;
                Elemento.Descripcion = ObjetoDTO.Descripcion;
                Elemento.IdElementoSubCategoria = ObjetoDTO.IdElementoSubCategoria;
                Elemento.Estado = true;
                Elemento.UsuarioModificacion = ObjetoDTO.Usuario;
                Elemento.FechaModificacion = DateTime.Now;

                _repElemento.Update(Elemento);

                return Ok(_repElemento.ObtenerRegistroElemento(Elemento.Id).FirstOrDefault());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarElemento([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ElementoRepositorio _repElemento = new ElementoRepositorio(_integraDBContext);
                ElementoBO Elemento = _repElemento.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repElemento.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
