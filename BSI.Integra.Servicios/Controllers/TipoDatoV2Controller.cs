using System;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoDatoV2")]
    
    public class TipoDatoV2Controller : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public TipoDatoV2Controller(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] TipoDatoPrincipalDTO tipoDato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoRepositorio _tipoDatoRepositorio = new TipoDatoRepositorio(_integraDBContext);
                TipoDatoBO tipoDatoBO = new TipoDatoBO
                {
                    Nombre = tipoDato.Nombre,
                    Descripcion = tipoDato.Descripcion,
                    Prioridad = tipoDato.Prioridad,
                    Estado = true,
                    UsuarioCreacion = tipoDato.Usuario,
                    UsuarioModificacion = tipoDato.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                return Ok(_tipoDatoRepositorio.Insert(tipoDatoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] TipoDatoPrincipalDTO tipoDato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoRepositorio _tipoDatoRepositorio = new TipoDatoRepositorio(_integraDBContext);
                TipoDatoBO tipoDatoBO = _tipoDatoRepositorio.FirstById(tipoDato.Id);
                tipoDatoBO.Nombre = tipoDato.Nombre;
                tipoDatoBO.Descripcion = tipoDato.Descripcion;
                tipoDatoBO.Prioridad = tipoDato.Prioridad;
                tipoDatoBO.UsuarioModificacion = tipoDato.Usuario;
                tipoDatoBO.FechaModificacion = DateTime.Now;
                return Ok(_tipoDatoRepositorio.Update(tipoDatoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] TipoDatoPrincipalDTO tipoDato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    TipoDatoRepositorio _tipoDatoRepositorio = new TipoDatoRepositorio(_integraDBContext);

                    if (_tipoDatoRepositorio.Exist(tipoDato.Id))
                    {
                        _tipoDatoRepositorio.Delete(tipoDato.Id, tipoDato.Usuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoRepositorio _tipoDatoRepositorio = new TipoDatoRepositorio(_integraDBContext);
                return Ok(_tipoDatoRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
