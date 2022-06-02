using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoImpuesto")]
    public class TipoImpuestoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TipoImpuestoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoTipoImpuesto()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoImpuestoRepositorio _repTipoImpuestoRep = new TipoImpuestoRepositorio(_integraDBContext);
                var Data = _repTipoImpuestoRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, NombreImpuesto = x.Nombre.ToUpper(), x.Descripcion, x.Valor, x.UsuarioModificacion, x.FechaModificacion, x.Estado }).ToList();
                var Total = Data.Count();
                return Ok(new { Data, Total });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarTipoImpuesto([FromBody] TipoImpuestoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoImpuestoRepositorio _repTipoImpuestoRep = new TipoImpuestoRepositorio(_integraDBContext);
                TipoImpuestoBO tipoImpuesto = new TipoImpuestoBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    tipoImpuesto.Nombre = Json.Nombre;
                    tipoImpuesto.Descripcion = Json.Descripcion;
                    tipoImpuesto.Valor = Json.Valor;
                    tipoImpuesto.Estado = true;
                    tipoImpuesto.UsuarioCreacion = Json.UsuarioModificacion;
                    tipoImpuesto.FechaCreacion = DateTime.Now;
                    tipoImpuesto.UsuarioModificacion = Json.UsuarioModificacion;
                    tipoImpuesto.FechaModificacion = DateTime.Now;
                    Json.FechaModificacion = tipoImpuesto.FechaModificacion;


                    _repTipoImpuestoRep.Insert(tipoImpuesto);
                    scope.Complete();
                }
                Json.Estado = tipoImpuesto.Estado;
                Json.Id = tipoImpuesto.Id;

                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarTipoImpuesto([FromBody] TipoImpuestoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                TipoImpuestoRepositorio _repTipoImpuestoRep = new TipoImpuestoRepositorio(_integraDBContext);
                TipoImpuestoBO tipoImpuesto = new TipoImpuestoBO();
                tipoImpuesto = _repTipoImpuestoRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    tipoImpuesto.Nombre = Json.Nombre;
                    tipoImpuesto.Descripcion = Json.Descripcion;
                    tipoImpuesto.Valor = Json.Valor;
                    tipoImpuesto.Estado = Json.Estado;
                    tipoImpuesto.UsuarioModificacion = Json.UsuarioModificacion;
                    tipoImpuesto.FechaModificacion = DateTime.Now;
                    Json.FechaModificacion = tipoImpuesto.FechaModificacion;
                    _repTipoImpuestoRep.Update(tipoImpuesto);
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
        public ActionResult EliminarTipoImpuesto([FromBody] EliminarDTO TipoImpuestoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    TipoImpuestoRepositorio _repTipoImpuestoRep = new TipoImpuestoRepositorio(_integraDBContext);
                    if (_repTipoImpuestoRep.Exist(TipoImpuestoDTO.Id))
                    {
                        _repTipoImpuestoRep.Delete(TipoImpuestoDTO.Id, TipoImpuestoDTO.NombreUsuario);
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
    }
}