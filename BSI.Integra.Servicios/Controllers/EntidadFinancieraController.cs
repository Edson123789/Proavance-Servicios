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
    [Route("api/EntidadFinanciera")]
    public class EntidadFinancieraController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerBancosActivos()
        {
            try
            {
                EntidadFinancieraRepositorio repEntidadFinancieraRep = new EntidadFinancieraRepositorio();
                return Ok(repEntidadFinancieraRep.ObtenerEntidadFinanciera());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEntidadesFinancieras()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EntidadFinancieraRepositorio _repEntidadFinancieraRep = new EntidadFinancieraRepositorio();
                return Ok(_repEntidadFinancieraRep.ObtenerEntidadesFinancieras());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarEntidadFinanciera([FromBody] EntidadFinancieraDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EntidadFinancieraRepositorio _repEntidadFinancieraRep = new EntidadFinancieraRepositorio();
                EntidadFinancieraBO entidad = new EntidadFinancieraBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    entidad.Nombre = Json.Nombre;
                    entidad.Descripcion = Json.Descripcion;
                    entidad.IdMoneda = Json.IdMoneda;
                    entidad.Estado = true;
                    entidad.UsuarioCreacion = Json.UsuarioModificacion;
                    entidad.FechaCreacion = DateTime.Now;
                    entidad.UsuarioModificacion = Json.UsuarioModificacion;
                    entidad.FechaModificacion = DateTime.Now;
                    Json.FechaModificacion = entidad.FechaModificacion;

                    _repEntidadFinancieraRep.Insert(entidad);
                    scope.Complete();
                }
                Json.Estado = entidad.Estado;
                Json.Id = entidad.Id;

                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarEntidadFinanciera([FromBody] EntidadFinancieraDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                EntidadFinancieraRepositorio _repEntidadFinancieraRep = new EntidadFinancieraRepositorio();
                EntidadFinancieraBO entidad = new EntidadFinancieraBO();
                entidad = _repEntidadFinancieraRep.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    entidad.Nombre = Json.Nombre;
                    entidad.Descripcion = Json.Descripcion;
                    entidad.IdMoneda = Json.IdMoneda;
                    entidad.Estado = Json.Estado;
                    entidad.UsuarioModificacion = Json.UsuarioModificacion;
                    entidad.FechaModificacion = DateTime.Now;
                    Json.FechaModificacion = entidad.FechaModificacion;
                    _repEntidadFinancieraRep.Update(entidad);
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
        public ActionResult EliminarEntidadFinanciera([FromBody] EliminarDTO EntidadFinancieraDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    EntidadFinancieraRepositorio _repEntidadFinancieraRep = new EntidadFinancieraRepositorio(_integraDBContext);
                    if (_repEntidadFinancieraRep.Exist(EntidadFinancieraDTO.Id))
                    {
                        _repEntidadFinancieraRep.Delete(EntidadFinancieraDTO.Id, EntidadFinancieraDTO.NombreUsuario);
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