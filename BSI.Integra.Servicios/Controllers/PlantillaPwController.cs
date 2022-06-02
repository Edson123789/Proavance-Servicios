using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Servicios.Controllers.PartnerPwController;
using static BSI.Integra.Servicios.Controllers.PlantillaPwController;
using static BSI.Integra.Servicios.Controllers.ProveedorCampaniaIntegraController;
using static BSI.Integra.Servicios.Controllers.TerminoUsoSitioWebPwController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PlantillaPw")]
    [ApiController]
    public class PlantillaPwController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public PlantillaPwController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                PlantillaPwRepositorio plantillaPwRepositorio = new PlantillaPwRepositorio();
                return Ok(plantillaPwRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPaises()
        {
            try
            {
                PaisRepositorio paisRepositorio = new PaisRepositorio();
                return Ok(paisRepositorio.ObtenerPaisFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoSeccionTipoContenido()
        {
            try
            {
                SeccionTipoContenidoPwRepositorio seccionTipoContenidoPwRepositorio = new SeccionTipoContenidoPwRepositorio();
                return Ok(seccionTipoContenidoPwRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPlantillaMaestro()
        {
            try
            {
                PlantillaMaestroPwRepositorio plantillaMaestroPwRepositorio = new PlantillaMaestroPwRepositorio();
                return Ok(plantillaMaestroPwRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoRevisiones()
        {
            try
            {
                RevisionPwRepositorio revisionPwRepositorio = new RevisionPwRepositorio();
                return Ok(revisionPwRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoTipoRevisiones()
        {
            try
            {
                TipoRevisionPwRepositorio tipoRevisionPwRepositorio = new TipoRevisionPwRepositorio();
                return Ok(tipoRevisionPwRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPlantilla}")]
        [HttpPost]
        public ActionResult ObtenerPaisesPorPlantilla(int IdPlantilla)
        {
            try
            {
                PaisRepositorio paisRepositorio = new PaisRepositorio();
                return Ok(paisRepositorio.ObtenerPaisesPorIdPlantilla(IdPlantilla));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult ObtenerSeccionesPlantillaPorIdPlantillaPW(int IdPlantilla)
        {
            try
            {
                SeccionPwRepositorio seccionPwRepositorio = new SeccionPwRepositorio();
                var ListaUnida = seccionPwRepositorio.ObtenerSeccionesPorIdPlantillaPW_Plantilla(IdPlantilla);
                List<SeccionPwPlantillaPwAgrupadoDTO> ListaSeccionSubSeccion = new List<SeccionPwPlantillaPwAgrupadoDTO>();
                ListaSeccionSubSeccion = ListaUnida.GroupBy(u => (u.IdPlantilla, u.Id, u.Nombre))
                                    .Select(group =>
                                    new SeccionPwPlantillaPwAgrupadoDTO
                                    {
                                        Id = group.Key.Id
                                    ,
                                        IdPlantilla = group.Key.IdPlantilla
                                    ,
                                        IdPlantillaPw = group.Key.IdPlantilla
                                    ,
                                        Nombre = group.Key.Nombre
                                    ,
                                        Descripcion = group.Select(x => x.Descripcion).FirstOrDefault()
                                    ,
                                        Contenido = group.Select(x => x.Contenido).FirstOrDefault()
                                    ,
                                        VisibleWeb = group.Select(x => x.VisibleWeb).FirstOrDefault()
                                    ,
                                        ZonaWeb = group.Select(x => x.ZonaWeb).FirstOrDefault()
                                    ,
                                        OrdenEeb = group.Select(x => x.OrdenEeb).FirstOrDefault()
                                    ,
                                        Titulo = group.Select(x => x.Titulo).FirstOrDefault()
                                    ,
                                        Posicion = group.Select(x => x.Posicion).FirstOrDefault()
                                    ,
                                        Tipo = group.Select(x => x.Tipo).FirstOrDefault()
                                    ,
                                        IdSeccionTipoContenido = group.Select(x => x.IdSeccionTipoContenido).FirstOrDefault()
                                    ,
                                        NombreSeccionTipoContenido = group.Select(x => x.NombreSeccionTipoContenido).FirstOrDefault()
                                    ,
                                        listaGridSeccionTipoContenido = group.Select(x => new listaGridSeccionTipoContenidoDTO { IdSeccionTipoDetallePw = x.IdSeccionTipoDetallePw, NombreSubSeccion = x.NombreSubSeccion, IdSubSeccionTipoContenido = x.IdSubSeccionTipoContenido }).ToList()
                                    }).ToList();


                return Ok(ListaSeccionSubSeccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdSeccion}")]
        [HttpGet]
        public ActionResult ObtenerSeccionesTipoContenidoPorIdSeccionPW(int IdSeccion)
        {
            try
            {
                SeccionTipoDetallePwRepositorio seccionTipoDetallePwRepositorio = new SeccionTipoDetallePwRepositorio();
                return Ok(seccionTipoDetallePwRepositorio.ObtenerSeccionesTipoDetallePorIdSeccionPW(IdSeccion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodosUsuariosAutoCompleto([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                PersonalRepositorio personalRepositorio = new PersonalRepositorio();
                if (Filtros.Count() > 0 && (Filtros != null && Filtros["Valor"] != null))
                {
                    var lista = personalRepositorio.ObtenerUsuariosAutoCompleto(Filtros["Valor"].ToString());
                    return Ok(lista);
                }
                return Ok(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPlantillaMaestro}")]
        [HttpPost]
        public ActionResult ObtenerTodosSeccionMaestraPorPlantillaMaestro (int IdPlantillaMaestro)
        {
            try
            {
                SeccionMaestraPwRepositorio seccionMaestraPwRepositorio = new SeccionMaestraPwRepositorio();
                return Ok(seccionMaestraPwRepositorio.ObtenerSeccionMaestraPorIdPlantillaMaestra(IdPlantillaMaestro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdRevision}")]
        [HttpGet]
        public ActionResult ObtenerTodoRevisionNivelPorIdRevision(int IdRevision)
        {
            try
            {
                RevisionNivelPwRepositorio revisionNivelPwRepositorio = new RevisionNivelPwRepositorio();
                return Ok(revisionNivelPwRepositorio.ObtenerRevisionNivelPorIdRevision(IdRevision));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult ObtenerTodoPlantillaMaestroPorIdPlantillaPw(int IdPlantilla)
        {
            try
            {
                PlantillaPlantillaMaestroPwRepositorio plantillaPlantillaMaestroPwRepositorio = new PlantillaPlantillaMaestroPwRepositorio();
                return Ok(plantillaPlantillaMaestroPwRepositorio.ObtenerPlantillaMaestroPorIdPlantillaPw(IdPlantilla));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPlantilla}")]
        [HttpPost]
        public ActionResult ObtenerTodoPlantillaRevisionNivelPorIdPlantillaPW(int IdPlantilla)
        {
            try
            {
                PlantillaRevisionPwRepositorio plantillaRevisionPwRepositorio = new PlantillaRevisionPwRepositorio();
                return Ok(plantillaRevisionPwRepositorio.ObtenerPlantillaRevisionNivelPorIdPlantillaPW(IdPlantilla));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarPlantillaPw([FromBody] CompuestoPlantilaPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaPwRepositorio plantillaPwRepositorio = new PlantillaPwRepositorio(_integraDBContext);
                PlantillaPwBO plantillaPw = new PlantillaPwBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    plantillaPw.Id = Json.PlantillaPw.Id;
                    plantillaPw.Nombre = Json.PlantillaPw.Nombre;
                    plantillaPw.Descripcion = Json.PlantillaPw.Descripcion;
                    plantillaPw.IdPlantillaMaestroPw = Json.PlantillaPw.IdPlantillaMaestroPw;
                    plantillaPw.IdRevisionPw = Json.PlantillaPw.IdRevisionPw;
                    plantillaPw.Estado = true;
                    plantillaPw.UsuarioCreacion = Json.Usuario;
                    plantillaPw.UsuarioModificacion = Json.Usuario;
                    plantillaPw.FechaCreacion = DateTime.Now;
                    plantillaPw.FechaModificacion = DateTime.Now;

                    plantillaPw.PlantillaRevisionPw = new List <PlantillaRevisionPwBO>();

                    foreach (var item in Json.PlantillaRevisionPw)
                    {
                        PlantillaRevisionPwBO plantillaRevision = new PlantillaRevisionPwBO();

                        plantillaRevision.IdRevisionNivelPw = item.IdRevisionNivelPw;
                        plantillaRevision.IdPersonal = item.IdPersonal;
                        plantillaRevision.UsuarioCreacion = Json.Usuario;
                        plantillaRevision.UsuarioModificacion = Json.Usuario;
                        plantillaRevision.FechaCreacion = DateTime.Now;
                        plantillaRevision.FechaModificacion = DateTime.Now;
                        plantillaRevision.Estado = true;

                        plantillaPw.PlantillaRevisionPw.Add(plantillaRevision);
                    }

                    plantillaPw.PlantillaPlantillaMaestroPw = new List <PlantillaPlantillaMaestroPwBO>();

                    if (Json.PlantillaPlantillaMaestroPw != null || Json.PlantillaPlantillaMaestroPw.Count > 0)
                    {
                        foreach (var item in Json.PlantillaPlantillaMaestroPw)
                        {
                            PlantillaPlantillaMaestroPwBO plantillaPlantillaMaestro = new PlantillaPlantillaMaestroPwBO();

                            plantillaPlantillaMaestro.IdSeccionMaestraPw = item.IdSeccionMaestraPw;
                            plantillaPlantillaMaestro.Contenido = item.Contenido;
                            plantillaPlantillaMaestro.UsuarioCreacion = Json.Usuario;
                            plantillaPlantillaMaestro.UsuarioModificacion = Json.Usuario;
                            plantillaPlantillaMaestro.FechaCreacion = DateTime.Now;
                            plantillaPlantillaMaestro.FechaModificacion = DateTime.Now;
                            plantillaPlantillaMaestro.Estado = true;

                            plantillaPw.PlantillaPlantillaMaestroPw.Add(plantillaPlantillaMaestro);
                        }
                    }

                    plantillaPw.PlantillaPais = new List<PlantillaPaisBO>();

                    foreach (var item in Json.ListaPaises)
                    {
                        PlantillaPaisBO plantillaPais = new PlantillaPaisBO();

                        plantillaPais.IdPais = item;
                        plantillaPais.UsuarioCreacion = Json.Usuario;
                        plantillaPais.UsuarioModificacion = Json.Usuario;
                        plantillaPais.FechaCreacion = DateTime.Now;
                        plantillaPais.FechaModificacion = DateTime.Now;
                        plantillaPais.Estado = true;

                        plantillaPw.PlantillaPais.Add(plantillaPais);
                    }

                    plantillaPw.SeccionPw = new List<SeccionPwBO>();

                    foreach (var item in Json.SeccionPw)
                    {
                        SeccionPwBO seccionPw = new SeccionPwBO();

                        seccionPw.Nombre = item.Nombre;
                        seccionPw.Descripcion = item.Descripcion;
                        seccionPw.Contenido = item.Contenido;
                        seccionPw.VisibleWeb = item.VisibleWeb;
                        seccionPw.ZonaWeb = item.ZonaWeb;
                        seccionPw.OrdenEeb = item.OrdenEeb;
                        seccionPw.UsuarioCreacion = Json.Usuario;
                        seccionPw.UsuarioModificacion = Json.Usuario;
                        seccionPw.FechaCreacion = DateTime.Now;
                        seccionPw.FechaModificacion = DateTime.Now;
                        seccionPw.Estado = true;
                        seccionPw.IdSeccionTipoContenido = item.IdSeccionTipoContenido;

                        seccionPw.SeccionTipoDetallePw = new List<SeccionTipoDetallePwBO>();
                        foreach (var item2 in item.listaGridSeccionTipoContenido)
                        {
                            SeccionTipoDetallePwBO seccionTipoDetallePw = new SeccionTipoDetallePwBO();

                            seccionTipoDetallePw.IdSeccionPw = item.Id;
                            seccionTipoDetallePw.NombreTitulo = item2.Nombre;
                            seccionTipoDetallePw.IdSeccionTipoContenido = item2.IdSeccionTipoContenido;
                            seccionTipoDetallePw.UsuarioCreacion = Json.Usuario;
                            seccionTipoDetallePw.UsuarioModificacion = Json.Usuario;
                            seccionTipoDetallePw.FechaCreacion = DateTime.Now;
                            seccionTipoDetallePw.FechaModificacion = DateTime.Now;
                            seccionTipoDetallePw.Estado = true;

                            seccionPw.SeccionTipoDetallePw.Add(seccionTipoDetallePw);

                        }
                        plantillaPw.SeccionPw.Add(seccionPw);
                    }

                    plantillaPwRepositorio.Insert(plantillaPw);
                    scope.Complete();
                }

                return Ok(plantillaPw);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarPlantillaPw([FromBody] CompuestoPlantilaActualizarPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                PlantillaPwRepositorio plantillaPwRepositorio = new PlantillaPwRepositorio(_integraDBContext);
                PlantillaRevisionPwRepositorio plantillaRevisionPwRepositorio = new PlantillaRevisionPwRepositorio(_integraDBContext);
                PlantillaPlantillaMaestroPwRepositorio plantillaPlantillaMaestroRepositorio = new PlantillaPlantillaMaestroPwRepositorio(_integraDBContext);
                PlantillaPaisRepositorio plantillaPaisRepositorio = new PlantillaPaisRepositorio(_integraDBContext);
                SeccionPwRepositorio seccionPwRepositorio = new SeccionPwRepositorio(_integraDBContext);
                SeccionTipoDetallePwRepositorio seccionTipoDetallePwRepositorio = new SeccionTipoDetallePwRepositorio(_integraDBContext);
                PlantillaPwBO plantillaPw = new PlantillaPwBO();
                List<SeccionPwBO> listaSeccionNuevasId = new List<SeccionPwBO>();
                DocumentoSeccionPwRepositorio documentoSeccionPwRepositorio = new DocumentoSeccionPwRepositorio(_integraDBContext);
                DocumentoPwRepositorio documentoPwRepositorio = new DocumentoPwRepositorio(_integraDBContext); 

                //using (TransactionScope scope = new TransactionScope())
                //{
                    if (plantillaPwRepositorio.Exist(Json.PlantillaPw.Id))
                    {
                        plantillaRevisionPwRepositorio.EliminacionLogicoPorPlantillaPw(Json.PlantillaPw.Id, Json.Usuario, Json.PlantillaRevisionPw);
                        plantillaPlantillaMaestroRepositorio.EliminacionLogicoPorPlantillaPw(Json.PlantillaPw.Id, Json.Usuario, Json.PlantillaPlantillaMaestroPw);
                        // Eliminar SeccionPw
                        foreach (var elemEliminar in Json.ListaEliminarSeccionPw)
                        {
                            seccionPwRepositorio.Delete(elemEliminar.Id, Json.Usuario);
                        }
                        //seccionPwRepositorio.EliminacionLogicoPorPlantillaPw(Json.PlantillaPw.Id, Json.Usuario, Json.ListaEliminarSeccionPw);
                        plantillaPaisRepositorio.EliminacionLogicoPorPlantillaPw(Json.PlantillaPw.Id, Json.Usuario, Json.ListaPaises);
                        //foreach (var eliminarSesion in Json.ListaActualizarSeccionPw)
                        //{
                        //    seccionTipoDetallePwRepositorio.EliminacionLogicoPorSesionPw(eliminarSesion.Id, Json.Usuario, Json.ListaEliminarSeccionTipoDetallePw);
                        //}


                        plantillaPw = plantillaPwRepositorio.FirstById(Json.PlantillaPw.Id);
                        plantillaPw.Nombre = Json.PlantillaPw.Nombre;
                        plantillaPw.Descripcion = Json.PlantillaPw.Descripcion;
                        plantillaPw.IdPlantillaMaestroPw = Json.PlantillaPw.IdPlantillaMaestroPw;
                        plantillaPw.IdRevisionPw = Json.PlantillaPw.IdRevisionPw;
                        plantillaPw.UsuarioModificacion = Json.Usuario;
                        plantillaPw.FechaModificacion = DateTime.Now;

                        plantillaPw.SeccionPw = new List<SeccionPwBO>();

                        foreach (var item in Json.ListaNuevasSeccionPw)
                        {
                            SeccionPwBO seccionPw = new SeccionPwBO();

                            seccionPw.Nombre = item.Nombre;
                            seccionPw.Descripcion = item.Descripcion;
                            seccionPw.Contenido = item.Contenido;
                            seccionPw.IdPlantillaPw = Json.PlantillaPw.Id;
                            seccionPw.VisibleWeb = item.VisibleWeb;
                            seccionPw.ZonaWeb = item.ZonaWeb;
                            seccionPw.OrdenEeb = item.OrdenEeb;
                            seccionPw.Estado = true;
                            seccionPw.UsuarioCreacion = Json.Usuario;
                            seccionPw.UsuarioModificacion = Json.Usuario;
                            seccionPw.FechaCreacion = DateTime.Now;
                            seccionPw.FechaModificacion = DateTime.Now;
                            seccionPw.IdSeccionTipoContenido = item.IdSeccionTipoContenido;

                            seccionPw.SeccionTipoDetallePw = new List<SeccionTipoDetallePwBO>();
                            foreach (var item2 in item.listaGridSeccionTipoContenido)
                            {
                                if (item2.IdSeccionTipoDetallePw != null)
                                {
                                    SeccionTipoDetallePwBO seccionTipoDetallePw = new SeccionTipoDetallePwBO();

                                    //seccionTipoDetallePw.IdSeccionPw = item.Id;
                                    seccionTipoDetallePw.NombreTitulo = item2.NombreSubSeccion;
                                    seccionTipoDetallePw.IdSeccionTipoContenido = item2.IdSubSeccionTipoContenido;
                                    seccionTipoDetallePw.UsuarioCreacion = Json.Usuario;
                                    seccionTipoDetallePw.UsuarioModificacion = Json.Usuario;
                                    seccionTipoDetallePw.FechaCreacion = DateTime.Now;
                                    seccionTipoDetallePw.FechaModificacion = DateTime.Now;
                                    seccionTipoDetallePw.Estado = true;

                                    seccionPw.SeccionTipoDetallePw.Add(seccionTipoDetallePw);
                                }
                            }
                            plantillaPw.SeccionPw.Add(seccionPw);
                            listaSeccionNuevasId.Add(seccionPw);
                        }

                        foreach (var item in Json.ListaActualizarSeccionPw)
                        {
                            //SeccionPwBO seccionPw;

                            var seccionPw = seccionPwRepositorio.FirstBy(x => x.Id == item.Id && x.IdPlantillaPw == item.IdPlantillaPw);
                            //var seccionPw = seccionPwRepositorio.ObtenerSeccionesPorId_IdPlantillaPw(item.Id, item.IdPlantillaPw);
                            seccionPw.Nombre = item.Nombre;
                            seccionPw.Descripcion = item.Descripcion;
                            seccionPw.Contenido = item.Contenido;
                            seccionPw.IdPlantillaPw = Json.PlantillaPw.Id;
                            seccionPw.VisibleWeb = item.VisibleWeb;
                            seccionPw.ZonaWeb = item.ZonaWeb;
                            seccionPw.OrdenEeb = item.OrdenEeb;
                            seccionPw.UsuarioModificacion = Json.Usuario;
                            seccionPw.FechaModificacion = DateTime.Now;
                            seccionPw.IdSeccionTipoContenido = item.IdSeccionTipoContenido;

                            seccionPw.SeccionTipoDetallePw = new List<SeccionTipoDetallePwBO>();

                            foreach (var item2 in item.listaGridSeccionTipoContenido)
                            {
                                if (item2.IdSeccionTipoDetallePw!=null)
                                {
                                    SeccionTipoDetallePwBO seccionTipoDetallePw;
                                    if (seccionTipoDetallePwRepositorio.ExisteIdSesionTipoDetalle(item2.IdSeccionTipoDetallePw.Value, item.Id))
                                    {
                                        seccionTipoDetallePw = seccionTipoDetallePwRepositorio.FirstBy(x => x.Id == item2.IdSeccionTipoDetallePw.Value && x.IdSeccionPw == item.Id);
                                        //seccionTipoDetallePw = seccionTipoDetallePwRepositorio.ObtenerSeccionesTipoDetalle(item2.IdSeccionTipoDetallePw.Value, item.Id);
                                        seccionTipoDetallePw.NombreTitulo = item2.NombreSubSeccion;
                                        seccionTipoDetallePw.IdSeccionTipoContenido = item2.IdSubSeccionTipoContenido.Value;
                                        seccionTipoDetallePw.UsuarioModificacion = Json.Usuario;
                                        seccionTipoDetallePw.FechaModificacion = DateTime.Now;
                                        
                                    }
                                    else
                                    {
                                        seccionTipoDetallePw = new SeccionTipoDetallePwBO
                                        {
                                            NombreTitulo = item2.NombreSubSeccion,
                                            IdSeccionTipoContenido = item2.IdSubSeccionTipoContenido,
                                            UsuarioCreacion = Json.Usuario,
                                            UsuarioModificacion = Json.Usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        };

                                    }
                                    seccionPw.SeccionTipoDetallePw.Add(seccionTipoDetallePw);
                                   
                                }
                                
                            }
                            plantillaPw.SeccionPw.Add(seccionPw);
                        }

                        //Asociar todos los Documentos por Plantilla
                        //if (Json.ListaNuevasSeccionPw.Count > 0 || Json.ListaEliminarSeccionPw.Count > 0)
                        //{
                        //    var listaDocs = documentoPwRepositorio.ObtenerDocumentoPorIdPlantilla(Json.PlantillaPw.Id);
                        //    if (listaDocs != null)
                        //    {
                        //        foreach (var itemDoc in listaDocs)
                        //        {
                        //            List<DocumentoSeccionPwBO> listaSecciones = documentoSeccionPwRepositorio.ObtenerNoMaestraPorIdDoc(itemDoc.Id);
                        //            if (listaSecciones != null)
                        //            {
                        //                //documentoSeccionPwRepositorio.EliminacionLogicoPorDocumentoPw(itemDoc.Id, Json.Usuario, Json.ListaEliminarSeccionPw);
                                        
                        //                //foreach (var itemSeccionInsertar in listaSeccionNuevasId)
                        //                //{
                        //                //    DocumentoSeccionPwBO documentoSeccionPw = new DocumentoSeccionPwBO();
                        //                //    documentoSeccionPw.IdDocumentoPw = itemDoc.Id;
                        //                //    documentoSeccionPw.IdSeccionPw = itemSeccionInsertar.Id;
                        //                //    documentoSeccionPw.Titulo = itemSeccionInsertar.Nombre;
                        //                //    documentoSeccionPw.IdPlantillaPw = Json.PlantillaPw.Id;
                        //                //    documentoSeccionPw.Posicion = 99;
                        //                //    documentoSeccionPw.Tipo = 0;
                        //                //    documentoSeccionPw.Contenido = "<!--Vacio-->";
                        //                //    documentoSeccionPw.Estado = true;
                        //                //    documentoSeccionPw.UsuarioCreacion = Json.Usuario;
                        //                //    documentoSeccionPw.UsuarioModificacion = Json.Usuario;
                        //                //    documentoSeccionPw.FechaCreacion = DateTime.Now;
                        //                //    documentoSeccionPw.FechaModificacion = DateTime.Now;

                        //                //    //var docSecInsertado = documentoSeccionPwRepositorio.Insert(documentoSeccionPw);
                        //                //    //plantillaPw.DocumentoSeccionPw.Add(documentoSeccionPw);
                        //                //    listaSecciones.Add(documentoSeccionPw);
                        //                //}

                        //                List<DocumentoSeccionPwBO> listaSeccionFinal = new List<DocumentoSeccionPwBO>();
                        //                //var listaSeccionesMaestras = documentoSeccionPwRepositorio.ObtenerNoMaestraPorIdDoc(itemDoc.Id);

                        //                //if (listaSeccionesMaestras != null)
                        //                //{
                        //                //    listaSeccionFinal = listaSeccionesMaestras.Concat(listaSecciones).ToList();
                        //                //}
                        //                //else
                        //                //{
                        //                //    listaSeccionFinal = listaSecciones.Concat(listaSeccionesMaestras).ToList();
                        //                //}

                        //                //listaSeccionFinal = listaSeccionFinal.OrderBy(x => x.Posicion).ToList();
                        //                int contador = 1;

                        //                foreach (var item in listaSeccionFinal)
                        //                {
                        //                    DocumentoSeccionPwBO insertdocumento;

                        //                    if (item.Id != 0)
                        //                    {
                        //                        insertdocumento = new DocumentoSeccionPwBO();
                        //                        insertdocumento.IdDocumentoPw = item.IdDocumentoPw;
                        //                        insertdocumento.IdSeccionPw = item.IdSeccionPw;
                        //                        insertdocumento.Titulo = item.Titulo;
                        //                        insertdocumento.IdPlantillaPw = item.IdPlantillaPw;
                        //                        insertdocumento.Posicion = contador;
                        //                        insertdocumento.Tipo = item.Tipo;
                        //                        insertdocumento.Contenido = item.Contenido;
                        //                        insertdocumento.Estado = item.Estado;
                        //                        insertdocumento.UsuarioCreacion = Json.Usuario;
                        //                        insertdocumento.UsuarioModificacion = Json.Usuario;
                        //                        insertdocumento.FechaCreacion = DateTime.Now;
                        //                        insertdocumento.FechaModificacion = DateTime.Now;
                        //                        var docSecInsertado = documentoSeccionPwRepositorio.Insert(insertdocumento);

                        //                    }
                        //                    else
                        //                    {
                        //                        insertdocumento = documentoSeccionPwRepositorio.FirstBy(x => x.IdDocumentoPw == item.IdDocumentoPw && x.IdPlantillaPw == item.IdPlantillaPw);
                        //                        insertdocumento.IdDocumentoPw = item.IdDocumentoPw;
                        //                        insertdocumento.IdSeccionPw = item.IdSeccionPw;
                        //                        insertdocumento.Titulo = item.Titulo;
                        //                        insertdocumento.IdPlantillaPw = item.IdPlantillaPw;
                        //                        insertdocumento.Posicion = contador;
                        //                        insertdocumento.Tipo = item.Tipo;
                        //                        insertdocumento.Contenido = item.Contenido;
                        //                        insertdocumento.UsuarioModificacion = Json.Usuario;
                        //                        insertdocumento.FechaModificacion = DateTime.Now;

                        //                        var docSecInsertado = documentoSeccionPwRepositorio.Update(insertdocumento);

                        //                        //Insertar docuemnto
                        //                        //    insertdocumento = new DocumentoSeccionPwBO();
                        //                        //    insertdocumento.IdDocumentoPw = item.IdDocumentoPw;
                        //                        //    insertdocumento.IdSeccionPw = item.IdSeccionPw;
                        //                        //    insertdocumento.Titulo = item.Titulo;
                        //                        //    insertdocumento.IdPlantillaPw = item.IdPlantillaPw;
                        //                        //    insertdocumento.Posicion = contador;
                        //                        //    insertdocumento.Tipo = item.Tipo;
                        //                        //    insertdocumento.Contenido = item.Contenido;
                        //                        //    insertdocumento.Estado = item.Estado;
                        //                        //    insertdocumento.UsuarioCreacion = Json.Usuario;
                        //                        //    insertdocumento.UsuarioModificacion = Json.Usuario;
                        //                        //    insertdocumento.FechaCreacion = DateTime.Now;
                        //                        //    insertdocumento.FechaModificacion = DateTime.Now;
                        //                        //    var docSecInsertado = documentoSeccionPwRepositorio.Insert(insertdocumento);

                        //                    }
                        //                    contador++;
                        //                }

                        //            }
                        //        }
                        //    }
                        //}

                        plantillaPw.PlantillaRevisionPw = new List<PlantillaRevisionPwBO>();

                        foreach (var item in Json.PlantillaRevisionPw)
                        {
                            PlantillaRevisionPwBO plantillaRevision;
                            if (plantillaRevisionPwRepositorio.Exist(x => x.IdRevisionNivelPw == item.IdRevisionNivelPw && x.IdPlantillaPw == Json.PlantillaPw.Id))
                            {
                                plantillaRevision = plantillaRevisionPwRepositorio.FirstBy(x => x.IdRevisionNivelPw == item.IdRevisionNivelPw && x.IdPlantillaPw == Json.PlantillaPw.Id);
                                plantillaRevision.IdRevisionNivelPw = item.IdRevisionNivelPw;
                                plantillaRevision.IdPersonal = item.IdPersonal;
                                plantillaRevision.UsuarioModificacion = Json.Usuario;
                                plantillaRevision.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                plantillaRevision = new PlantillaRevisionPwBO();
                                plantillaRevision.IdRevisionNivelPw = item.IdRevisionNivelPw;
                                plantillaRevision.IdPersonal = item.IdPersonal;
                                plantillaRevision.UsuarioCreacion = Json.Usuario;
                                plantillaRevision.UsuarioModificacion = Json.Usuario;
                                plantillaRevision.FechaCreacion = DateTime.Now;
                                plantillaRevision.FechaModificacion = DateTime.Now;
                                plantillaRevision.Estado = true;
                            }
                            plantillaPw.PlantillaRevisionPw.Add(plantillaRevision);
                        }

                        plantillaPw.PlantillaPlantillaMaestroPw = new List<PlantillaPlantillaMaestroPwBO>();

                        if (Json.PlantillaPlantillaMaestroPw != null || Json.PlantillaPlantillaMaestroPw.Count > 0)
                        {
                            foreach (var item in Json.PlantillaPlantillaMaestroPw)
                            {
                                PlantillaPlantillaMaestroPwBO plantillaPlantillaMaestro;
                                if (plantillaPlantillaMaestroRepositorio.Exist(x => x.IdSeccionMaestraPw == item.IdSeccionMaestraPw && x.IdPlantillaPw == Json.PlantillaPw.Id))
                                {
                                    plantillaPlantillaMaestro = plantillaPlantillaMaestroRepositorio.FirstBy(x => x.IdSeccionMaestraPw == item.IdSeccionMaestraPw && x.IdPlantillaPw == Json.PlantillaPw.Id);
                                    plantillaPlantillaMaestro.IdSeccionMaestraPw = item.IdSeccionMaestraPw;
                                    plantillaPlantillaMaestro.Contenido = item.Contenido;
                                    plantillaPlantillaMaestro.UsuarioModificacion = Json.Usuario;
                                    plantillaPlantillaMaestro.FechaModificacion = DateTime.Now;
                                }
                                else
                                {
                                    plantillaPlantillaMaestro = new PlantillaPlantillaMaestroPwBO();
                                    plantillaPlantillaMaestro.IdSeccionMaestraPw = item.IdSeccionMaestraPw;
                                    plantillaPlantillaMaestro.Contenido = item.Contenido;
                                    plantillaPlantillaMaestro.UsuarioCreacion = Json.Usuario;
                                    plantillaPlantillaMaestro.UsuarioModificacion = Json.Usuario;
                                    plantillaPlantillaMaestro.FechaCreacion = DateTime.Now;
                                    plantillaPlantillaMaestro.FechaModificacion = DateTime.Now;
                                    plantillaPlantillaMaestro.Estado = true;
                                }
                                plantillaPw.PlantillaPlantillaMaestroPw.Add(plantillaPlantillaMaestro);
                            }
                        }
                        
                        plantillaPw.PlantillaPais = new List<PlantillaPaisBO>();

                        foreach (var item in Json.ListaPaises)
                        {
                            PlantillaPaisBO plantillaPais;
                            if (plantillaPlantillaMaestroRepositorio.Exist(x => x.Id == item && x.IdPlantillaPw == Json.PlantillaPw.Id))
                            {
                                plantillaPais = plantillaPaisRepositorio.FirstBy(x => x.Id == item && x.IdPlantilla == Json.PlantillaPw.Id);
                                plantillaPais.IdPlantilla = Json.PlantillaPw.Id;
                                plantillaPais.IdPais = item;
                                plantillaPais.UsuarioModificacion = Json.Usuario;
                                plantillaPais.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                plantillaPais = new PlantillaPaisBO();
                                plantillaPais.IdPlantilla = Json.PlantillaPw.Id;
                                plantillaPais.IdPais = item;
                                plantillaPais.UsuarioCreacion = Json.Usuario;
                                plantillaPais.UsuarioModificacion = Json.Usuario;
                                plantillaPais.FechaCreacion = DateTime.Now;
                                plantillaPais.FechaModificacion = DateTime.Now;
                                plantillaPais.Estado = true;
                            }
                            plantillaPw.PlantillaPais.Add(plantillaPais);
                        }

                        plantillaPwRepositorio.Update(plantillaPw);
                        //scope.Complete();
                    }
                //}
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarPlantillaPw(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PlantillaPwRepositorio plantillaPwRepositorio = new PlantillaPwRepositorio(contexto);
                PlantillaPlantillaMaestroPwRepositorio plantillaPlantillaMaestroRepositorio = new PlantillaPlantillaMaestroPwRepositorio(contexto);
                PlantillaPaisRepositorio plantillaPaisRepositorio = new PlantillaPaisRepositorio(contexto);
                PlantillaRevisionPwRepositorio plantillaRevisionPwRepositorio = new PlantillaRevisionPwRepositorio(contexto);
                SeccionPwRepositorio seccionPwRepositorio = new SeccionPwRepositorio(contexto);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (plantillaPwRepositorio.Exist(Id))
                    {
                        plantillaPwRepositorio.Delete(Id, Usuario);

                        var hijosPlantillaPlantillaMaestro = plantillaPlantillaMaestroRepositorio.GetBy(x => x.IdPlantillaPw == Id);
                        foreach (var hijo in hijosPlantillaPlantillaMaestro)
                        {
                            plantillaPlantillaMaestroRepositorio.Delete(hijo.Id, Usuario);
                        }

                        var hijosPlantillaPais = plantillaPaisRepositorio.GetBy(x => x.IdPlantilla == Id);
                        foreach (var hijo in hijosPlantillaPais)
                        {
                            plantillaPaisRepositorio.Delete(hijo.Id, Usuario);
                        }

                        var hijosPlantillaRevision = plantillaRevisionPwRepositorio.GetBy(x => x.IdPlantillaPw == Id);
                        foreach (var hijo in hijosPlantillaRevision)
                        {
                            plantillaRevisionPwRepositorio.Delete(hijo.Id, Usuario);
                        }

                        var hijosSeccionPw = seccionPwRepositorio.GetBy(x => x.IdPlantillaPw == Id);
                        foreach (var hijo in hijosSeccionPw)
                        {
                            seccionPwRepositorio.Delete(hijo.Id, Usuario);
                        }

                    }

                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public class ValidadorPlantillaPwDTO : AbstractValidator<TPlantillaPw>
        {
            public static ValidadorPlantillaPwDTO Current = new ValidadorPlantillaPwDTO();
            public ValidadorPlantillaPwDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");

            }
        }
    }
}
