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
    /// Controlador: MaestroGrupoComparacionController
    /// Autor: _ _ _ _ _ _ _ _ _.
    /// Fecha: 29/03/2021
    /// <summary>
    /// Gestión de Grupo de Comparación
    /// </summary>
    [Route("api/MaestroGrupoComparacion")]
    public class MaestroGrupoComparacionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly GrupoComparacionProcesoSeleccionRepositorio repGrupoComparacionRep;
        private readonly PostulanteComparacionRepositorio repPostulanteComparacionRep;
        private readonly SedeTrabajoGrupoComparacionRepositorio repSedeTrabajoGrupoComparacionRep;
        private readonly PuestoTrabajoGrupoComparacionRepositorio repPuestoTrabajoGrupoComparacionRep;
        public MaestroGrupoComparacionController()
        {
            _integraDBContext = new integraDBContext();
            repGrupoComparacionRep = new GrupoComparacionProcesoSeleccionRepositorio(_integraDBContext);
            repPostulanteComparacionRep = new PostulanteComparacionRepositorio(_integraDBContext);
            repSedeTrabajoGrupoComparacionRep = new SedeTrabajoGrupoComparacionRepositorio(_integraDBContext);
            repPuestoTrabajoGrupoComparacionRep = new PuestoTrabajoGrupoComparacionRepositorio(_integraDBContext);
        }

        /// TipoFuncion: GET
        /// Autor: _ _ _ _ _ _ _.
        /// Fecha: 29/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Grupo de Comparación
        /// </summary>
        /// <returns> Lista de ObjetoDTO: List<GrupoComparacionProcesoSeleccionDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerGrupoComparacion()
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GrupoComparacionProcesoSeleccionRepositorio repGrupoComparacionRep = new GrupoComparacionProcesoSeleccionRepositorio();
                var listaComparacion= repGrupoComparacionRep.ObtenerGrupoComparacion();
                var listaGrupoComparacion = listaComparacion.GroupBy(x => new { x.Id, x.Nombre }).Select(g => new GrupoComparacionDTO
                {
                    Id = g.Key.Id,
                    Nombre = g.Key.Nombre,
                    ListaPuestoTrabajo = g.Where(x=>x.IdPuestoTrabajo!=0).GroupBy(y => new { y.IdPuestoTrabajo }).Select(h => h.Key.IdPuestoTrabajo).ToList(),
                    ListaSedeTrabajo = g.Where(x=>x.IdSedeTrabajo!=0).GroupBy(y => new { y.IdSedeTrabajo }).Select(h => h.Key.IdSedeTrabajo).ToList(),
                    ListaPostulante = g.GroupBy(y => new {y.IdPostulante }).Select(h => h.Key.IdPostulante.Value).ToList()
                }).ToList();

                return Ok(listaGrupoComparacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: _ _ _ _ _ _ _.
        /// Fecha: 29/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina logicamente un registro de grupo de comparación
        /// </summary>
        /// <returns> Confirmación de Eliminación:  Bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarGrupoComparacion([FromBody] EliminarDTO objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    GrupoComparacionProcesoSeleccionRepositorio repGrupoComparacionRep = new GrupoComparacionProcesoSeleccionRepositorio(_integraDBContext);
                    
                    if (repGrupoComparacionRep.Exist(objeto.Id))
                    {
                        repGrupoComparacionRep.Delete(objeto.Id, objeto.NombreUsuario);
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

        /// TipoFuncion: POST
        /// Autor: _ _ _ _ _ _ _.
        /// Fecha: 29/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta registro de Grupo de Comparación
        /// </summary>
        /// <returns> GrupoComparacionDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarGrupoComparacion([FromBody] GrupoComparacionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                GrupoComparacionProcesoSeleccionBO grupoComparacion = new GrupoComparacionProcesoSeleccionBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    grupoComparacion.Nombre = Json.Nombre;
                    grupoComparacion.IdPuestoTrabajo = 1;
                    grupoComparacion.IdSedeTrabajo = 1;

                    grupoComparacion.Estado = true;
                    grupoComparacion.UsuarioCreacion = Json.Usuario;
                    grupoComparacion.FechaCreacion = DateTime.Now;
                    grupoComparacion.UsuarioModificacion = Json.Usuario;
                    grupoComparacion.FechaModificacion = DateTime.Now;
                    
                    repGrupoComparacionRep.Insert(grupoComparacion);

                    foreach (var item in Json.ListaPostulante) {
                        PostulanteComparacionBO postulante = new PostulanteComparacionBO();
                        postulante.IdGrupoComparacionProcesoSeleccion = grupoComparacion.Id;
                        postulante.IdPostulante = item;
                        postulante.Estado = true;
                        postulante.UsuarioCreacion = Json.Usuario;
                        postulante.FechaCreacion = DateTime.Now;
                        postulante.UsuarioModificacion = Json.Usuario;
                        postulante.FechaModificacion = DateTime.Now;
                        repPostulanteComparacionRep.Insert(postulante);
                    }

                    foreach (var item in Json.ListaPuestoTrabajo)
                    {
                        PuestoTrabajoGrupoComparacionBO puestoTrabajo = new PuestoTrabajoGrupoComparacionBO();
                        puestoTrabajo.IdGrupoComparacionProcesoSeleccion = grupoComparacion.Id;
                        puestoTrabajo.IdPuestoTrabajo = item;
                        puestoTrabajo.Estado = true;
                        puestoTrabajo.UsuarioCreacion = Json.Usuario;
                        puestoTrabajo.FechaCreacion = DateTime.Now;
                        puestoTrabajo.UsuarioModificacion = Json.Usuario;
                        puestoTrabajo.FechaModificacion = DateTime.Now;
                        repPuestoTrabajoGrupoComparacionRep.Insert(puestoTrabajo);
                    }

                    foreach (var item in Json.ListaSedeTrabajo)
                    {
                        SedeTrabajoGrupoComparacionBO sedeTrabajo = new SedeTrabajoGrupoComparacionBO();
                        sedeTrabajo.IdGrupoComparacionProcesoSeleccion = grupoComparacion.Id;
                        sedeTrabajo.IdSedeTrabajo = item;
                        sedeTrabajo.Estado = true;
                        sedeTrabajo.UsuarioCreacion = Json.Usuario;
                        sedeTrabajo.FechaCreacion = DateTime.Now;
                        sedeTrabajo.UsuarioModificacion = Json.Usuario;
                        sedeTrabajo.FechaModificacion = DateTime.Now;
                        repSedeTrabajoGrupoComparacionRep.Insert(sedeTrabajo);
                    }

                    scope.Complete();
                }
                Json.Id = grupoComparacion.Id;
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: _ _ _ _ _ _ _.
        /// Fecha: 29/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un registro de Grupo de Comparación
        /// </summary>
        /// <returns> GrupoComparacionDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarGrupoComparacion([FromBody] GrupoComparacionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                GrupoComparacionProcesoSeleccionBO grupoComparacion = new GrupoComparacionProcesoSeleccionBO();
                grupoComparacion = repGrupoComparacionRep.FirstById(Json.Id);
                               
                using (TransactionScope scope = new TransactionScope())
                {
                    grupoComparacion.Nombre = Json.Nombre;
                    grupoComparacion.IdPuestoTrabajo =1;
                    grupoComparacion.IdSedeTrabajo = 1;
                    grupoComparacion.UsuarioModificacion = Json.Usuario;
                    grupoComparacion.FechaModificacion = DateTime.Now;
                    repGrupoComparacionRep.Update(grupoComparacion);

                    //Se insertan o Actualizan Postulantes
                    foreach (var item in Json.ListaPostulante) {
                        PostulanteComparacionBO postulante = repPostulanteComparacionRep.FirstBy(x=>x.IdGrupoComparacionProcesoSeleccion==Json.Id && x.IdPostulante==item);

                        if ( postulante == null)
                        {
                            postulante = new PostulanteComparacionBO();
                            postulante.IdGrupoComparacionProcesoSeleccion = Json.Id;
                            postulante.IdPostulante = item;
                            postulante.Estado = true;
                            postulante.UsuarioCreacion = Json.Usuario;
                            postulante.FechaCreacion = DateTime.Now;
                            postulante.UsuarioModificacion = Json.Usuario;
                            postulante.FechaModificacion = DateTime.Now;
                            repPostulanteComparacionRep.Insert(postulante);
                        }
                        else {
                            postulante.IdGrupoComparacionProcesoSeleccion = Json.Id;
                            postulante.IdPostulante = item;
                            repPostulanteComparacionRep.Update(postulante);
                        }
                    }

                    //Se insertan o Actualizan PuestoTrabajo
                    foreach (var item in Json.ListaPuestoTrabajo)
                    {
                        PuestoTrabajoGrupoComparacionBO puestoTrabajo = repPuestoTrabajoGrupoComparacionRep.FirstBy(x => x.IdGrupoComparacionProcesoSeleccion == Json.Id && x.IdPuestoTrabajo == item);

                        if (puestoTrabajo == null)
                        {
                            puestoTrabajo = new PuestoTrabajoGrupoComparacionBO();
                            puestoTrabajo.IdGrupoComparacionProcesoSeleccion = Json.Id;
                            puestoTrabajo.IdPuestoTrabajo = item;
                            puestoTrabajo.Estado = true;
                            puestoTrabajo.UsuarioCreacion = Json.Usuario;
                            puestoTrabajo.FechaCreacion = DateTime.Now;
                            puestoTrabajo.UsuarioModificacion = Json.Usuario;
                            puestoTrabajo.FechaModificacion = DateTime.Now;
                            repPuestoTrabajoGrupoComparacionRep.Insert(puestoTrabajo);
                        }
                        else
                        {
                            puestoTrabajo.IdGrupoComparacionProcesoSeleccion = Json.Id;
                            puestoTrabajo.IdPuestoTrabajo = item;
                            repPuestoTrabajoGrupoComparacionRep.Update(puestoTrabajo);
                        }
                    }

                    //Se insertan o Actualizan Sede de Trabajo
                    foreach (var item in Json.ListaSedeTrabajo)
                    {
                        SedeTrabajoGrupoComparacionBO puestoTrabajo = repSedeTrabajoGrupoComparacionRep.FirstBy(x => x.IdGrupoComparacionProcesoSeleccion == Json.Id && x.IdSedeTrabajo == item);

                        if (puestoTrabajo == null)
                        {
                            puestoTrabajo = new SedeTrabajoGrupoComparacionBO();
                            puestoTrabajo.IdGrupoComparacionProcesoSeleccion = Json.Id;
                            puestoTrabajo.IdSedeTrabajo = item;
                            puestoTrabajo.Estado = true;
                            puestoTrabajo.UsuarioCreacion = Json.Usuario;
                            puestoTrabajo.FechaCreacion = DateTime.Now;
                            puestoTrabajo.UsuarioModificacion = Json.Usuario;
                            puestoTrabajo.FechaModificacion = DateTime.Now;
                            repSedeTrabajoGrupoComparacionRep.Insert(puestoTrabajo);
                        }
                        else
                        {
                            puestoTrabajo.IdGrupoComparacionProcesoSeleccion = Json.Id;
                            puestoTrabajo.IdSedeTrabajo = item;
                            repSedeTrabajoGrupoComparacionRep.Update(puestoTrabajo);
                        }
                    }

                    //Eliminar postulantes repetidos
                    var listaEliminarPostulante = repPostulanteComparacionRep.GetBy(x => x.IdGrupoComparacionProcesoSeleccion == Json.Id && !Json.ListaPostulante.Contains(x.IdPostulante.Value)).ToList();
                    foreach (var eliminar in listaEliminarPostulante) {
                        repPostulanteComparacionRep.Delete(eliminar.Id, Json.Usuario);
                    }

                    //Eliminar Puesto de Trabajo repetidos
                    var listaEliminarPuestoTrabajo = repPuestoTrabajoGrupoComparacionRep.GetBy(x => x.IdGrupoComparacionProcesoSeleccion == Json.Id && !Json.ListaPuestoTrabajo.Contains(x.IdPuestoTrabajo)).ToList();
                    foreach (var eliminar in listaEliminarPuestoTrabajo)
                    {
                        repPuestoTrabajoGrupoComparacionRep.Delete(eliminar.Id, Json.Usuario);
                    }

                    //Eliminar SedeTrabajo repetidos
                    var listaEliminarSedeTrabajo = repSedeTrabajoGrupoComparacionRep.GetBy(x => x.IdGrupoComparacionProcesoSeleccion == Json.Id && !Json.ListaSedeTrabajo.Contains(x.IdSedeTrabajo)).ToList();
                    foreach (var eliminar in listaEliminarSedeTrabajo)
                    {
                        repSedeTrabajoGrupoComparacionRep.Delete(eliminar.Id, Json.Usuario);
                    }

                    scope.Complete();

                }
                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}