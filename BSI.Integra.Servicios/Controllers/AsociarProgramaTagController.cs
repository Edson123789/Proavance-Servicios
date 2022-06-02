using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.SCode.Repositorio;
using System.Transactions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AsociarProgramaTag")]
    public class AsociarProgramaTagController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public AsociarProgramaTagController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
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
                PgeneralRepositorio _pgeneralRepositorio = new PgeneralRepositorio(_integraDBContext);
                return Ok(_pgeneralRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoArea()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCapacitacionRepositorio _areaCapacitacionRepositorio = new AreaCapacitacionRepositorio(_integraDBContext);
                return Ok(_areaCapacitacionRepositorio.ObtenerTodoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoSubArea()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubAreaCapacitacionRepositorio _subAreaCapacitacionRepositorio = new SubAreaCapacitacionRepositorio(_integraDBContext);
                return Ok(_subAreaCapacitacionRepositorio.ObtenerTodoFiltroAutoSelect());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoCategoriaPrograma()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaProgramaRepositorio _categoriaProgramaRepositorio = new CategoriaProgramaRepositorio(_integraDBContext);
                return Ok(_categoriaProgramaRepositorio.ObtenerCategoriasNombrePrograma());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public ActionResult ObtenerTodoTagPorPrograma(int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralTagsPwRepositorio _pGeneralTagsRepositorio = new PGeneralTagsPwRepositorio(_integraDBContext);
                TagPwRepositorio _tagRepositorio = new TagPwRepositorio();
                var idsTags = _pGeneralTagsRepositorio.GetBy(x => x.IdPgeneral == IdProgramaGeneral && x.Estado == true).Select(x => x.IdTagPW).ToList();
                var objetoTag = _tagRepositorio.GetBy(x => idsTags.Contains(x.Id) && x.Estado == true).Select(x => new { x.Id, x.Nombre, x.Descripcion, x.Estado, x.FechaCreacion, x.FechaModificacion, x.UsuarioCreacion, x.UsuarioModificacion });

                return Ok(objetoTag);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarTag([FromBody] CompuestoTagDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TagPwRepositorio _tagPwRepositorio = new TagPwRepositorio(_integraDBContext);
                TagPwBO tagBO = new TagPwBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    tagBO.Nombre = Json.ObjetoTag.Nombre;
                    tagBO.Descripcion = Json.ObjetoTag.Descripcion;
                    tagBO.TagWebId = Json.ObjetoTag.TagWebId;
                    tagBO.Estado = true;
                    tagBO.UsuarioCreacion = Json.Usuario;
                    tagBO.UsuarioModificacion = Json.Usuario;
                    tagBO.FechaCreacion = DateTime.Now;
                    tagBO.FechaModificacion = DateTime.Now;

                    tagBO.TagParametroSeo = new List<TagParametroSeoPwBO>();
                    tagBO.PGeneralTag = new List<PGeneralTagPwBO>();

                    PGeneralTagPwBO pGeneralTagsBO = new PGeneralTagPwBO();
                    pGeneralTagsBO.IdPgeneral =Json.IdPGeneral;
                    pGeneralTagsBO.Estado = true;
                    pGeneralTagsBO.UsuarioCreacion = Json.Usuario;
                    pGeneralTagsBO.UsuarioModificacion = Json.Usuario;
                    pGeneralTagsBO.FechaCreacion = DateTime.Now;
                    pGeneralTagsBO.FechaModificacion = DateTime.Now;

                    tagBO.PGeneralTag.Add(pGeneralTagsBO);

                    foreach (var item in Json.ListaParametro)
                    {
                        TagParametroSeoPwBO tagParametroSeo = new TagParametroSeoPwBO();
                        if(String.IsNullOrEmpty(item.Contenido))
                            tagParametroSeo.Descripcion = "<!--vacio-->";
                        else
                            tagParametroSeo.Descripcion = item.Contenido;
                        tagParametroSeo.IdParametroSEOPW = item.Id;
                        tagParametroSeo.Estado = true;
                        tagParametroSeo.UsuarioCreacion = Json.Usuario;
                        tagParametroSeo.UsuarioModificacion = Json.Usuario;
                        tagParametroSeo.FechaCreacion = DateTime.Now;
                        tagParametroSeo.FechaModificacion = DateTime.Now;

                        tagBO.TagParametroSeo.Add(tagParametroSeo);
                    }
                    _tagPwRepositorio.Insert(tagBO);
                    scope.Complete();
                }
                return Ok(tagBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarTag([FromBody] CompuestoTagDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TagPwRepositorio _repTagPw = new TagPwRepositorio(_integraDBContext);
                TagParametroSeoPwRepositorio _repTagParametroSeo = new TagParametroSeoPwRepositorio(_integraDBContext);

                TagPwBO tagPwBO = new TagPwBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    _repTagParametroSeo.EliminacionLogicoPorTagPw(Json.ObjetoTag.Id, Json.Usuario, Json.ListaParametro);

                    tagPwBO = _repTagPw.FirstById(Json.ObjetoTag.Id);

                    tagPwBO.Nombre = Json.ObjetoTag.Nombre;
                    tagPwBO.Descripcion = Json.ObjetoTag.Descripcion;
                    tagPwBO.TagWebId = Json.ObjetoTag.TagWebId;
                    tagPwBO.UsuarioModificacion = Json.Usuario;
                    tagPwBO.FechaModificacion = DateTime.Now;

                    tagPwBO.TagParametroSeo = new List<TagParametroSeoPwBO>();

                    foreach (var item in Json.ListaParametro)
                    {
                        TagParametroSeoPwBO tagParametroSeoBO;
                        if (_repTagParametroSeo.Exist(x => x.IdParametroSeopw == item.Id && x.IdTagPw == Json.ObjetoTag.Id))
                        {
                            tagParametroSeoBO = _repTagParametroSeo.FirstBy(x => x.IdParametroSeopw == item.Id && x.IdTagPw == Json.ObjetoTag.Id);
                            tagParametroSeoBO.IdParametroSEOPW = item.Id;
                            if (String.IsNullOrEmpty(item.Contenido))
                                tagParametroSeoBO.Descripcion = "<!--vacio-->";
                            else
                                tagParametroSeoBO.Descripcion = item.Contenido;
                            tagParametroSeoBO.UsuarioModificacion = Json.Usuario;
                            tagParametroSeoBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            tagParametroSeoBO = new TagParametroSeoPwBO();
                            tagParametroSeoBO.IdParametroSEOPW = item.Id;
                            if (String.IsNullOrEmpty(item.Contenido))
                                tagParametroSeoBO.Descripcion = "<!--vacio-->";
                            else
                                tagParametroSeoBO.Descripcion = item.Contenido;
                            tagParametroSeoBO.UsuarioCreacion = Json.Usuario;
                            tagParametroSeoBO.UsuarioModificacion = Json.Usuario;
                            tagParametroSeoBO.FechaCreacion = DateTime.Now;
                            tagParametroSeoBO.FechaModificacion = DateTime.Now;
                            tagParametroSeoBO.Estado = true;
                        }
                        tagPwBO.TagParametroSeo.Add(tagParametroSeoBO);
                    }
                    _repTagPw.Update(tagPwBO);
                    scope.Complete();
                }
                return Ok(tagPwBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarTag([FromBody] TagPwDTO ObjetoTagPw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TagPwRepositorio _tagPwRepositorio = new TagPwRepositorio(_integraDBContext);
                TagParametroSeoPwRepositorio _tagParametroSeo = new TagParametroSeoPwRepositorio(_integraDBContext);
                PGeneralTagsPwRepositorio _pGeneralTag = new PGeneralTagsPwRepositorio(_integraDBContext);

                TagPwBO tagPwBO = new TagPwBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_tagPwRepositorio.Exist(ObjetoTagPw.Id))
                    {
                        _tagPwRepositorio.Delete(ObjetoTagPw.Id, ObjetoTagPw.Usuario);
                        var hijosTagParametroSeo= _tagParametroSeo.GetBy(x => x.IdTagPw == ObjetoTagPw.Id);
                        foreach (var hijo in hijosTagParametroSeo)
                        {
                            _tagParametroSeo.Delete(hijo.Id, ObjetoTagPw.Usuario);
                        }
                        var hijosPGeneralTag = _pGeneralTag.GetBy(x => x.IdTagPw == ObjetoTagPw.Id);
                        foreach (var hijo in hijosPGeneralTag)
                        {
                            _pGeneralTag.Delete(hijo.Id, ObjetoTagPw.Usuario);
                        }
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoParametroSeo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ParametroSeoPwRepositorio _parametroSeoRepositorio = new ParametroSeoPwRepositorio(_integraDBContext);
                return Ok(_parametroSeoRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdTag}")]
        [HttpGet]
        public ActionResult ObtenerTodoParametroContenido(int IdTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TagParametroSeoPwRepositorio _repTagParametroSeo = new TagParametroSeoPwRepositorio(_integraDBContext);
                return Ok(_repTagParametroSeo.ObtenerTodoPorIdTag(IdTag));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerTagSinAsociar(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralTagsPwRepositorio _pGeneralTagsRepositorio = new PGeneralTagsPwRepositorio(_integraDBContext);
                TagPwRepositorio _tagRepositorio = new TagPwRepositorio(_integraDBContext);
                var idsTags = _pGeneralTagsRepositorio.GetBy(x => x.IdPgeneral == Id && x.Estado == true).Select(x => x.IdTagPW).ToList();
                var objetoTag = _tagRepositorio.GetBy(x => !idsTags.Contains(x.Id)  && x.Estado == true).Select(x => new { x.Id, x.Nombre, x.Estado, x.FechaCreacion, x.FechaModificacion, x.UsuarioCreacion, x.UsuarioModificacion });
                
                return Ok(objetoTag);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarTag([FromBody] CompuestoTagPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PGeneralTagsPwRepositorio _pGeneralTagRepositorio = new PGeneralTagsPwRepositorio(_integraDBContext);

                    foreach (var item in Json.ListaTag)
                    {
                        PGeneralTagPwBO pGeneralTagBO = new PGeneralTagPwBO();
                        pGeneralTagBO.IdPgeneral = Json.Id;
                        pGeneralTagBO.IdTagPW = item.Id;

                        pGeneralTagBO.Estado = true;
                        pGeneralTagBO.UsuarioCreacion = Json.Usuario;
                        pGeneralTagBO.UsuarioModificacion = Json.Usuario;
                        pGeneralTagBO.FechaCreacion = DateTime.Now;
                        pGeneralTagBO.FechaModificacion = DateTime.Now;

                        _pGeneralTagRepositorio.Insert(pGeneralTagBO);
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPGeneral}/{IdTag}")]
        [HttpGet]
        public ActionResult Desasociar(int IdPGeneral, int IdTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PGeneralTagsPwRepositorio _pGeneralTagsRepositorio = new PGeneralTagsPwRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    var objetoPGeneralTag = _pGeneralTagsRepositorio.GetBy(x => x.IdPgeneral == IdPGeneral && x.IdTagPw == IdTag && x.Estado == true).Select(x => new {x.Id, x.IdPgeneral, x.IdTagPW, x.Estado, x.UsuarioCreacion, x.UsuarioModificacion, x.FechaCreacion, x.FechaModificacion }).FirstOrDefault();
                    _pGeneralTagsRepositorio.Delete(objetoPGeneralTag.Id, objetoPGeneralTag.UsuarioModificacion);
                    scope.Complete();
                }
                return Ok(true);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    
    }
}
