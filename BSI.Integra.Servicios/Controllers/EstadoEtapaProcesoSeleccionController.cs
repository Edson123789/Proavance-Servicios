using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/EstadoEtapaProcesoSeleccion")]
    public class EstadoEtapaProcesoSeleccionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly EstadoEtapaProcesoSeleccionRepositorio _repEstadoEtapaProcesoSeleccion;
        public EstadoEtapaProcesoSeleccionController()
        {
            _integraDBContext = new integraDBContext();
            _repEstadoEtapaProcesoSeleccion = new EstadoEtapaProcesoSeleccionRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoEtapaProcesoSeleccion()
        {
            try
            {
                var listaNivelCurso = _repEstadoEtapaProcesoSeleccion.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = x.Nombre }).ToList();

                return Ok(listaNivelCurso);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarEstadoEtapaProcesoSeleccion([FromBody] EliminarDTO objeto)
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
                    if (_repEstadoEtapaProcesoSeleccion.Exist(objeto.Id))
                    {
                        _repEstadoEtapaProcesoSeleccion.Delete(objeto.Id, objeto.NombreUsuario);
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

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarEstadoEtapaProcesoSeleccion([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoEtapaProcesoSeleccionBO estadoEtapa = new EstadoEtapaProcesoSeleccionBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    estadoEtapa.Nombre = Json.Nombre;
                    estadoEtapa.Estado = true;
                    estadoEtapa.UsuarioCreacion = Json.Usuario;
                    estadoEtapa.FechaCreacion = DateTime.Now;
                    estadoEtapa.UsuarioModificacion = Json.Usuario;
                    estadoEtapa.FechaModificacion = DateTime.Now;

                    _repEstadoEtapaProcesoSeleccion.Insert(estadoEtapa);
                    scope.Complete();
                }
                string rpta = "INSERTADO CORRECTAMENTE";
                return Ok(new { rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarEstadoEtapaProcesoSeleccion([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoEtapaProcesoSeleccionBO estadoEtapa = new EstadoEtapaProcesoSeleccionBO();
                estadoEtapa = _repEstadoEtapaProcesoSeleccion.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    estadoEtapa.Nombre = Json.Nombre;
                    estadoEtapa.UsuarioModificacion = Json.Usuario;
                    estadoEtapa.FechaModificacion = DateTime.Now;
                    _repEstadoEtapaProcesoSeleccion.Update(estadoEtapa);
                    scope.Complete();
                }
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}