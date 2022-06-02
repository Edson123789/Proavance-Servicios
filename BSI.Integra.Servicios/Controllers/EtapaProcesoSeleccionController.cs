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
    [Route("api/EtapaProcesoSeleccion")]
    public class EtapaProcesoSeleccionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public EtapaProcesoSeleccionController()
        {
            _integraDBContext = new integraDBContext();
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerProcesoSeleccionEtapa()
        {
            try
            {
                ProcesoSeleccionEtapaRepositorio repProcesoSeleccionEtapaRep = new ProcesoSeleccionEtapaRepositorio();
                
                return Ok(repProcesoSeleccionEtapaRep.ObtenerProcesoSeleccionEtapa());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                ProcesoSeleccionRepositorio repProcesoRep = new ProcesoSeleccionRepositorio();

                var listaProceso = repProcesoRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre}).ToList();

                return Ok(new { listaProceso= listaProceso });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarProcesoSeleccionEtapa([FromBody] EliminarDTO objeto)
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
                    ProcesoSeleccionEtapaRepositorio repProcesoSeleccionEtapaRep = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
                    if (repProcesoSeleccionEtapaRep.Exist(objeto.Id))
                    {
                        repProcesoSeleccionEtapaRep.Delete(objeto.Id, objeto.NombreUsuario);
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
        public ActionResult InsertarProcesoSeleccionEtapa([FromBody] ProcesoSeleccionEtapaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProcesoSeleccionEtapaRepositorio repProcesoSeleccionEtapaRep = new ProcesoSeleccionEtapaRepositorio();
                ProcesoSeleccionEtapaBO procesoSeleccionEtapa = new ProcesoSeleccionEtapaBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    procesoSeleccionEtapa.Nombre = Json.Nombre;
                    procesoSeleccionEtapa.IdProcesoSeleccion = Json.IdProcesoSeleccion;
                    procesoSeleccionEtapa.NroOrden = Json.NroOrden;
                    procesoSeleccionEtapa.Estado = true;
                    procesoSeleccionEtapa.UsuarioCreacion = Json.Usuario;
                    procesoSeleccionEtapa.FechaCreacion = DateTime.Now;
                    procesoSeleccionEtapa.UsuarioModificacion = Json.Usuario;
                    procesoSeleccionEtapa.FechaModificacion = DateTime.Now;

                    repProcesoSeleccionEtapaRep.Insert(procesoSeleccionEtapa);
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
        public ActionResult ActualizarProcesoSeleccionEtapa([FromBody] ProcesoSeleccionEtapaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProcesoSeleccionEtapaRepositorio repProcesoSeleccionEtapaRep = new ProcesoSeleccionEtapaRepositorio();
                ProcesoSeleccionEtapaBO procesoSeleccionEtapa = new ProcesoSeleccionEtapaBO();
                procesoSeleccionEtapa = repProcesoSeleccionEtapaRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    procesoSeleccionEtapa.Nombre = Json.Nombre;
                    procesoSeleccionEtapa.IdProcesoSeleccion = Json.IdProcesoSeleccion;
                    procesoSeleccionEtapa.NroOrden = Json.NroOrden;
                    procesoSeleccionEtapa.UsuarioModificacion = Json.Usuario;
                    procesoSeleccionEtapa.FechaModificacion = DateTime.Now;
                    repProcesoSeleccionEtapaRep.Update(procesoSeleccionEtapa);
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