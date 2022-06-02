using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Tag")]
    public class TagController : Controller
    {
        private readonly integraDBContext _integraDBContext ;
        public TagController() {
            _integraDBContext = new integraDBContext();
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltroTags()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TagPwRepositorio _repoTag = new TagPwRepositorio(_integraDBContext);
                var ListaTags = _repoTag.OntenerComboTags();

                return Ok(ListaTags);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltroParametroSeo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ParametroSeoPwRepositorio _repoParametroSeo = new ParametroSeoPwRepositorio(_integraDBContext);
                var Registros = _repoParametroSeo.ObtnerParametroSeoFiltro();

                return Ok(Registros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdTag}")]
        [HttpGet]
        public ActionResult ObtenerParametroSeoAsociados(int IdTag)
        {
            try
            {
                TagPwRepositorio _repoTag = new TagPwRepositorio(_integraDBContext);
                var rpta = _repoTag.ObtenerParametroSeoAsociadosPorIdTag(IdTag);

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #region CRUD
        [Route("[action]")]
        [HttpGet]
        public ActionResult VisualizarTag()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                TagPwRepositorio _repoTag = new TagPwRepositorio(_integraDBContext);
                var RegistrosTag = _repoTag.OntenerListaTags();

                return Ok(RegistrosTag );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarTag([FromBody] TagPwDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TagParametroSeoPwRepositorio _repoTagParametroSeo = new TagParametroSeoPwRepositorio(_integraDBContext);
                TagPwRepositorio _repoTag = new TagPwRepositorio(_integraDBContext);

                TagPwBO Tag = new TagPwBO();
                Tag.Nombre = RequestDTO.Nombre;
                Tag.Descripcion = RequestDTO.Descripcion;
                Tag.Codigo = RequestDTO.Codigo;
                Tag.Estado= true;
                Tag.UsuarioCreacion = RequestDTO.Usuario;
                Tag.UsuarioModificacion = RequestDTO.Usuario;
                Tag.FechaCreacion = DateTime.Now;
                Tag.FechaModificacion = DateTime.Now;
                _repoTag.Insert(Tag);

                TagPwBO Tag2 = _repoTag.GetBy(x => x.Id == Tag.Id).FirstOrDefault();
                Tag2.TagWebId = Tag.Id;
                _repoTag.Update(Tag2);

                List<TagParametroSeoPwBO> ParametroSeoNuevos = new List<TagParametroSeoPwBO>();
                foreach (var Item in RequestDTO.ParametroSeoAsociados)
                    ParametroSeoNuevos.Add(new TagParametroSeoPwBO
                    {
                        IdTagPW = Tag.Id,
                        IdParametroSEOPW = Item.Id,
                        Descripcion = Item.Nombre,
                        UsuarioCreacion = RequestDTO.Usuario,
                        UsuarioModificacion = RequestDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    });

                if (ParametroSeoNuevos.Count > 0)
                    _repoTagParametroSeo.Insert(ParametroSeoNuevos);

                return Ok(Tag2);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarTag([FromBody] TagPwDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TagPwRepositorio _repoTag = new TagPwRepositorio(_integraDBContext);
                TagParametroSeoPwRepositorio _repoTagParametroSeo = new TagParametroSeoPwRepositorio(_integraDBContext);

                TagPwBO Tag = _repoTag.GetBy(x => x.Id == RequestDTO.Id).FirstOrDefault();
                if (Tag == null) throw new Exception("El registro que se desea actualizar no existe ¿Id correcto? (Id=" + RequestDTO.Id +")");

                Tag.Nombre = RequestDTO.Nombre;
                Tag.Descripcion = RequestDTO.Descripcion;
                Tag.Codigo = RequestDTO.Codigo;
                Tag.UsuarioModificacion = RequestDTO.Usuario;
                Tag.FechaModificacion = DateTime.Now;

                List<TagParametroSeoPwBO> ParametroSeoAntiguos = _repoTagParametroSeo.GetBy(x => x.IdTagPw == Tag.Id && x.Estado == true).ToList();
                foreach (var Item in ParametroSeoAntiguos)
                {
                    Item.Estado = false;
                    Item.FechaModificacion = DateTime.Now;
                    Item.UsuarioModificacion = RequestDTO.Usuario;
                }

                if (ParametroSeoAntiguos.Count > 0)
                    _repoTagParametroSeo.Update(ParametroSeoAntiguos);

                List<TagParametroSeoPwBO> ParametroSeoNuevos = new List<TagParametroSeoPwBO>();
                foreach (var Item in RequestDTO.ParametroSeoAsociados)
                    ParametroSeoNuevos.Add(new TagParametroSeoPwBO
                    {
                        IdTagPW = Tag.Id,
                        IdParametroSEOPW = Item.Id,
                        Descripcion = Item.Nombre,
                        UsuarioCreacion = RequestDTO.Usuario,
                        UsuarioModificacion = RequestDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    });

                _repoTag.Update(Tag);
                if (ParametroSeoNuevos.Count > 0)
                    _repoTagParametroSeo.Insert(ParametroSeoNuevos);

                return Ok(Tag);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarTag([FromBody] TagPwDTO eliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TagParametroSeoPwRepositorio _repoTagParametroSeo = new TagParametroSeoPwRepositorio(_integraDBContext);
                TagPwRepositorio _repoTag = new TagPwRepositorio(_integraDBContext);
               
                TagPwBO Tag = _repoTag.GetBy(x => x.Id == eliminarDTO.Id).FirstOrDefault();
                if (Tag == null) throw new Exception("El registro que se desea eliminar no existe ¿Id correcto? (Id=" + eliminarDTO.Id + ")");


                Tag.Estado = false;
                Tag.UsuarioModificacion = eliminarDTO.Usuario;
                Tag.FechaModificacion = DateTime.Now;

                List<TagParametroSeoPwBO> ParametroSeoAntiguos = _repoTagParametroSeo.GetBy(x => x.IdTagPw == Tag.Id && x.Estado == true).ToList();
                foreach (var Item in ParametroSeoAntiguos)
                {
                    Item.Estado = false;
                    Item.FechaModificacion = DateTime.Now;
                    Item.UsuarioModificacion = eliminarDTO.Usuario;
                }

                _repoTag.Update(Tag);
                
                if (ParametroSeoAntiguos.Count > 0)
                    _repoTagParametroSeo.Update(ParametroSeoAntiguos);

                return Ok(Tag);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

    }
    
}
