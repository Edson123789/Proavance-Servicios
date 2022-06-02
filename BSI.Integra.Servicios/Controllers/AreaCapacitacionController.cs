using System;
using System.Collections.Generic;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/AreaCapacitacion
    /// Autor: Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Configura las acciones para todo lo relacionado con las areas de capacitacion
    /// </summary>
    [Route("api/AreaCapacitacion")]
    public class AreaCapacitacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AreaCapacitacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las areas de capacitacion habilitadas en una lista de objetos de clase FiltroDTO
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase lista de objetos de clase FiltroDTO, caso contrario response 400 con el mensaje de error respectivo</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                AreaCapacitacionRepositorio areaCapacitacionRepositorio = new AreaCapacitacionRepositorio(_integraDBContext);
                return Ok(areaCapacitacionRepositorio.ObtenerTodoFiltro());
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
            try
            {
                AreaCapacitacionRepositorio areaCapacitacionRepositorio = new AreaCapacitacionRepositorio(_integraDBContext);
                return Ok(areaCapacitacionRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoParametroSEO()
        {
            try
            {
                ParametroSeoPwRepositorio parametroSeoPwRepositorio = new ParametroSeoPwRepositorio(_integraDBContext);
                return Ok(parametroSeoPwRepositorio.ObtenerTodoParametroGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdTag}")]
        [HttpGet]
        public ActionResult ObtenerContenidoParametroSEO(int IdTag)
        {
            try
            {
                AreaParametroSeoPwRepositorio areaParametroSeoPwRepositorio = new AreaParametroSeoPwRepositorio(_integraDBContext);
                return Ok(areaParametroSeoPwRepositorio.ObtenerTodoPorIdTag(IdTag));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarArea([FromBody] CompuestoAreaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCapacitacionRepositorio areaCapacitacionRepositorio = new AreaCapacitacionRepositorio(_integraDBContext);
                AreaCapacitacionBO areaCapacitacionBO = new AreaCapacitacionBO();

                areaCapacitacionBO.AreaParametroSeoPw = new List<AreaParametroSeoPwBO>();

                using (TransactionScope scope = new TransactionScope())
                {
                    areaCapacitacionBO.Nombre = Json.AreaCapacitacion.Nombre;
                    areaCapacitacionBO.Descripcion = Json.AreaCapacitacion.Descripcion;
                    areaCapacitacionBO.ImgPortada = Json.AreaCapacitacion.ImgPortada;
                    areaCapacitacionBO.ImgSecundaria = Json.AreaCapacitacion.ImgSecundaria;
                    areaCapacitacionBO.ImgPortadaAlt = Json.AreaCapacitacion.ImgPortadaAlt;
                    areaCapacitacionBO.ImgSecundariaAlt = Json.AreaCapacitacion.ImgSecundariaAlt;
                    areaCapacitacionBO.EsVisibleWeb = Json.AreaCapacitacion.EsVisibleWeb;
                    areaCapacitacionBO.IdArea = Json.AreaCapacitacion.IdArea;
                    areaCapacitacionBO.EsWeb = Json.AreaCapacitacion.EsWeb;
                    areaCapacitacionBO.DescripcionHtml = Json.AreaCapacitacion.DescripcionHtml;
                    areaCapacitacionBO.IdAreaCapacitacionFacebook = Json.AreaCapacitacion.IdAreaCapacitacionFacebook;
                    areaCapacitacionBO.Estado = true;
                    areaCapacitacionBO.UsuarioCreacion = Json.Usuario;
                    areaCapacitacionBO.UsuarioModificacion = Json.Usuario;
                    areaCapacitacionBO.FechaCreacion = DateTime.Now;
                    areaCapacitacionBO.FechaModificacion = DateTime.Now;


                    foreach (var item in Json.ListaParametro)
                    {
                        AreaParametroSeoPwBO areaParametroSeo = new AreaParametroSeoPwBO();

                        if (String.IsNullOrEmpty(item.ContenidoParametroSeo))
                            areaParametroSeo.Descripcion = "<!--vacio-->";
                        else
                            areaParametroSeo.Descripcion = item.ContenidoParametroSeo;
                        areaParametroSeo.IdAreaCapacitacion = Json.AreaCapacitacion.Id;
                        areaParametroSeo.IdParametroSeopw = item.Id;
                        areaParametroSeo.Estado = true;
                        areaParametroSeo.UsuarioCreacion = Json.Usuario;
                        areaParametroSeo.UsuarioModificacion = Json.Usuario;
                        areaParametroSeo.FechaCreacion = DateTime.Now;
                        areaParametroSeo.FechaModificacion = DateTime.Now;

                        areaCapacitacionBO.AreaParametroSeoPw.Add(areaParametroSeo);
                    }
                    areaCapacitacionRepositorio.Insert(areaCapacitacionBO);
                    scope.Complete();
                }

                    return Ok(areaCapacitacionBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarArea([FromBody] CompuestoAreaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCapacitacionRepositorio areaCapacitacionRepositorio = new AreaCapacitacionRepositorio(_integraDBContext);
                AreaParametroSeoPwRepositorio areaParametroSeoPwRepositorio = new AreaParametroSeoPwRepositorio(_integraDBContext);

                AreaCapacitacionBO areaCapacitacionBO = new AreaCapacitacionBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (areaCapacitacionRepositorio.Exist(Json.AreaCapacitacion.Id))
                    {
                        areaParametroSeoPwRepositorio.EliminacionLogicoPorArea(Json.AreaCapacitacion.Id, Json.Usuario, Json.ListaParametro);

                        areaCapacitacionBO = areaCapacitacionRepositorio.FirstById(Json.AreaCapacitacion.Id);
                        areaCapacitacionBO.Nombre = Json.AreaCapacitacion.Nombre;
                        areaCapacitacionBO.Descripcion = Json.AreaCapacitacion.Descripcion;
                        areaCapacitacionBO.ImgPortada = Json.AreaCapacitacion.ImgPortada;
                        areaCapacitacionBO.ImgSecundaria = Json.AreaCapacitacion.ImgSecundaria;
                        areaCapacitacionBO.ImgPortadaAlt = Json.AreaCapacitacion.ImgPortadaAlt;
                        areaCapacitacionBO.ImgSecundariaAlt = Json.AreaCapacitacion.ImgSecundariaAlt;
                        areaCapacitacionBO.EsVisibleWeb = Json.AreaCapacitacion.EsVisibleWeb;
                        areaCapacitacionBO.IdArea = Json.AreaCapacitacion.IdArea;
                        areaCapacitacionBO.EsWeb = Json.AreaCapacitacion.EsWeb;
                        areaCapacitacionBO.DescripcionHtml = Json.AreaCapacitacion.DescripcionHtml;
                        areaCapacitacionBO.IdAreaCapacitacionFacebook = Json.AreaCapacitacion.IdAreaCapacitacionFacebook;
                        areaCapacitacionBO.UsuarioModificacion = Json.Usuario;
                        areaCapacitacionBO.FechaModificacion = DateTime.Now;

                        areaCapacitacionBO.AreaParametroSeoPw = new List<AreaParametroSeoPwBO>();

                        foreach (var item in Json.ListaParametro)
                        {
                            AreaParametroSeoPwBO areaParametroSeo;

                            if (areaParametroSeoPwRepositorio.Exist(x => x.IdAreaCapacitacion == Json.AreaCapacitacion.Id && x.IdParametroSeopw == item.Id))
                            {

                                areaParametroSeo = areaParametroSeoPwRepositorio.FirstBy(x => x.IdAreaCapacitacion == Json.AreaCapacitacion.Id && x.IdParametroSeopw == item.Id);
                                if (String.IsNullOrEmpty(item.ContenidoParametroSeo))
                                    areaParametroSeo.Descripcion = "<!--vacio-->";
                                else
                                    areaParametroSeo.Descripcion = item.ContenidoParametroSeo;
                                areaParametroSeo.IdAreaCapacitacion = Json.AreaCapacitacion.Id;
                                areaParametroSeo.IdParametroSeopw = item.Id;
                                areaParametroSeo.UsuarioModificacion = Json.Usuario;
                                areaParametroSeo.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                areaParametroSeo = new AreaParametroSeoPwBO();
                                if (String.IsNullOrEmpty(item.ContenidoParametroSeo))
                                    areaParametroSeo.Descripcion = "<!--vacio-->";
                                else
                                    areaParametroSeo.Descripcion = item.ContenidoParametroSeo;
                                areaParametroSeo.IdAreaCapacitacion = Json.AreaCapacitacion.Id;
                                areaParametroSeo.IdParametroSeopw = item.Id;
                                areaParametroSeo.Estado = true;
                                areaParametroSeo.UsuarioCreacion = Json.Usuario;
                                areaParametroSeo.UsuarioModificacion = Json.Usuario;
                                areaParametroSeo.FechaCreacion = DateTime.Now;
                                areaParametroSeo.FechaModificacion = DateTime.Now;
                            }

                            areaCapacitacionBO.AreaParametroSeoPw.Add(areaParametroSeo);
                        }
                        areaCapacitacionRepositorio.Update(areaCapacitacionBO);
                        scope.Complete();
                    }
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarArea(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCapacitacionRepositorio _areaCapacitacionRepositorio = new AreaCapacitacionRepositorio(_integraDBContext);
                AreaParametroSeoPwRepositorio _areaParametroSeo = new AreaParametroSeoPwRepositorio(_integraDBContext);

                AreaCapacitacionBO areaCapacitacionBO = new AreaCapacitacionBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_areaCapacitacionRepositorio.Exist(Id))
                    {
                        _areaCapacitacionRepositorio.Delete(Id, Usuario);

                        var hijosTagParametroSeo = _areaParametroSeo.GetBy(x => x.IdAreaCapacitacion == Id);
                        foreach (var hijo in hijosTagParametroSeo)
                        {
                            _areaParametroSeo.Delete(hijo.Id, Usuario);
                        }
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
