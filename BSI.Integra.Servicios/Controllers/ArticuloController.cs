using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ArticuloController
    /// Autor: 
    /// Fecha: 18/02/2021
    /// <summary>
    /// Configuracion Cerrar BNC a RN5
    /// </summary>
    [Route("api/Articulo")]
    public class ArticuloController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ArticuloController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener toda la configuracion del modulo articulo
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodoArticulo([FromBody] FiltroGridDTO filtro)
        {
            try
            {
                ArticuloRepositorio repAreaCapacitacion = new ArticuloRepositorio(_integraDBContext);
                var rpta = repAreaCapacitacion.ObtenerRegistroArticulo(filtro.paginador, filtro.filter);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener filtros del modulos
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                AreaCapacitacionRepositorio repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
                SubAreaCapacitacionRepositorio repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
                ExpositorRepositorio repExpositor = new ExpositorRepositorio(_integraDBContext);
                CategoriaProgramaRepositorio repCategoriaPrograma = new CategoriaProgramaRepositorio(_integraDBContext);
                ArticuloSeoRepositorio repArticuloSeo = new ArticuloSeoRepositorio(_integraDBContext);

                SubAreaCapacitacionBO subAreaCapacitacion = new SubAreaCapacitacionBO();
                var rpta = new
                {
                    filtroAreaCapacitacion = repAreaCapacitacion.ObtenerAreaCapacitacionFiltro(),
                    filtroSubAreaCapacitacion = repSubAreaCapacitacion.ObtenerSubAreasParaFiltro(),
                    filtroExpositor = repExpositor.ObtenerExpositoresFiltro(),
                    filtroCategoriaPrograma = repCategoriaPrograma.ObtenerCategoriasPrograma(),
                    filtroParametroSeo = repArticuloSeo.ObtnerParametroSeoFiltro(),
                };

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Parametros Seo Por Articulo
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]/{IdArticulo}")]
        [HttpGet]
        public ActionResult ObtenerParametroSeoPorArticulo(int IdArticulo)
        {
            try
            {
                
                ArticuloSeoRepositorio repArticuloSeo = new ArticuloSeoRepositorio(_integraDBContext);

                SubAreaCapacitacionBO subAreaCapacitacion = new SubAreaCapacitacionBO();
                var rpta = repArticuloSeo.ObtenerArticuloSeoParametro(IdArticulo);

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Insertar Articulo
        /// </summary>
        /// <returns>Objeto<returns>articuloBO
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody]ArticuloInsertarDTO objeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                ArticuloRepositorio repArticulo = new ArticuloRepositorio(_integraDBContext);
                ArticuloSeoRepositorio repArticuloSeo = new ArticuloSeoRepositorio(_integraDBContext);

                byte[] dataContenido = Convert.FromBase64String(objeto.Contenido);
                var decodedContenido = Encoding.UTF8.GetString(dataContenido);
                byte[] dataDescripcion = Convert.FromBase64String(objeto.DescripcionGeneral);
                var decodedDescripcion = Encoding.UTF8.GetString(dataDescripcion);

                ArticuloBO articuloBO = new ArticuloBO();
                articuloBO.IdWeb = repArticulo.ObtenerMaximaIdWeb() +1;
                articuloBO.Nombre = objeto.Nombre;
                articuloBO.Titulo = objeto.Titulo;
                articuloBO.ImgPortada = objeto.ImgPortada;
                articuloBO.ImgPortadaAlt = objeto.ImgPortadaAlt;
                articuloBO.ImgSecundaria = objeto.ImgSecundaria;
                articuloBO.ImgSecundariaAlt = objeto.ImgSecundariaAlt;
                articuloBO.Autor = objeto.Autor;
                articuloBO.IdTipoArticulo = objeto.IdTipoArticulo;
                articuloBO.Contenido = decodedContenido;
                articuloBO.IdArea = objeto.IdArea;
                articuloBO.IdSubArea = objeto.IdSubArea;
                articuloBO.IdExpositor = objeto.IdExpositor;
                articuloBO.IdCategoria = objeto.IdCategoria;
                articuloBO.UrlWeb = objeto.UrlWeb;
                articuloBO.Estado = true;
                articuloBO.FechaCreacion = DateTime.Now;
                articuloBO.FechaModificacion = DateTime.Now;
                articuloBO.UsuarioCreacion = objeto.Usuario;
                articuloBO.UsuarioModificacion = objeto.Usuario;
                articuloBO.UrlDocumento = objeto.UrlDocumento;
                articuloBO.DescripcionGeneral = decodedDescripcion;

                repArticulo.Insert(articuloBO);

                List<ArticuloSeoBO> listaArticuloSeoBO = new List<ArticuloSeoBO>();
                foreach (var Item in objeto.listaArticuloSeo)
                {
                    ArticuloSeoBO articuloSeoBO = new ArticuloSeoBO()
                    {
                        Descripcion = Item.Descripcion,
                        IdArticulo = articuloBO.Id,
                        IdParametroSeo = Item.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = objeto.Usuario,
                        UsuarioModificacion = objeto.Usuario
                    };
                    listaArticuloSeoBO.Add(articuloSeoBO);
                }

                repArticuloSeo.Insert(listaArticuloSeoBO);

                return Ok(articuloBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Articulo
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody]ArticuloInsertarDTO objeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                ArticuloRepositorio repArticulo = new ArticuloRepositorio(_integraDBContext);
                ArticuloSeoRepositorio repArticuloSeo = new ArticuloSeoRepositorio(_integraDBContext);

                byte[] dataContenido = Convert.FromBase64String(objeto.Contenido);
                var decodedContenido = Encoding.UTF8.GetString(dataContenido);
                byte[] dataDescripcion = Convert.FromBase64String(objeto.DescripcionGeneral);
                var decodedDescripcion = Encoding.UTF8.GetString(dataDescripcion);

                var articuloBO = repArticulo.FirstById(objeto.Id);
                //articuloBO.IdWeb = objeto.IdWeb;
                articuloBO.Nombre = objeto.Nombre;
                articuloBO.Titulo = objeto.Titulo;
                articuloBO.ImgPortada = objeto.ImgPortada;
                articuloBO.ImgPortadaAlt = objeto.ImgPortadaAlt;
                articuloBO.ImgSecundaria = objeto.ImgSecundaria;
                articuloBO.ImgSecundariaAlt = objeto.ImgSecundariaAlt;
                articuloBO.Autor = objeto.Autor;
                articuloBO.IdTipoArticulo = objeto.IdTipoArticulo;
                articuloBO.Contenido = decodedContenido;
                articuloBO.IdArea = objeto.IdArea;
                articuloBO.IdSubArea = objeto.IdSubArea;
                articuloBO.IdExpositor = objeto.IdExpositor;
                articuloBO.IdCategoria = objeto.IdCategoria;
                articuloBO.UrlWeb = objeto.UrlWeb;
                articuloBO.UrlDocumento = objeto.UrlDocumento;
                articuloBO.DescripcionGeneral = decodedDescripcion;
                articuloBO.FechaModificacion = DateTime.Now;
                articuloBO.UsuarioModificacion = objeto.Usuario;

                repArticulo.Update(articuloBO);

                var listaId = repArticuloSeo.GetBy(w => w.IdArticulo == objeto.Id).Select(y=>y.Id).ToList();

                if (listaId.Count > 0)
                {
                    repArticuloSeo.Delete(listaId,objeto.Usuario);
                }

                List<ArticuloSeoBO> listaArticuloSeoBO = new List<ArticuloSeoBO>();
                foreach (var Item in objeto.listaArticuloSeo)
                {
                    ArticuloSeoBO articuloSeoBO = new ArticuloSeoBO()
                    {
                        Descripcion = Item.Descripcion,
                        IdArticulo = articuloBO.Id,
                        IdParametroSeo = Item.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = objeto.Usuario,
                        UsuarioModificacion = objeto.Usuario
                    };
                    listaArticuloSeoBO.Add(articuloSeoBO);
                }

                repArticuloSeo.Insert(listaArticuloSeoBO);

                return Ok(articuloBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Eliminar Articulo
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]/{IdArticulo}/{Usuario}")]
        [HttpGet]
        public ActionResult Eliminar(int IdArticulo, string Usuario)
        {
            try
            {
                
                ArticuloRepositorio repArticulo = new ArticuloRepositorio(_integraDBContext);
                ArticuloSeoRepositorio repArticuloSeo = new ArticuloSeoRepositorio(_integraDBContext);

                var articuloBO = repArticulo.FirstById(IdArticulo);               

                repArticulo.Delete(articuloBO.Id,Usuario);

                var listaId = repArticuloSeo.GetBy(w => w.IdArticulo == IdArticulo).Select(y=>y.Id).ToList();

                if (listaId.Count > 0)
                {
                    repArticuloSeo.Delete(listaId,Usuario);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Programas Asociados
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]/{IdArticulo}")]
        [HttpGet]
        public ActionResult ObtenerProgramasAsociados(int IdArticulo)
        {
            try
            {

                ArticuloRepositorio repArticulo = new ArticuloRepositorio(_integraDBContext);
                var rpta = repArticulo.ObtenerProgramasAsociadosArticulo(IdArticulo);

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Programas No Asociados
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]/{IdArticulo}")]
        [HttpGet]
        public ActionResult ObtenerProgramasNoAsociados(int IdArticulo)
        {
            try
            {

                ArticuloRepositorio repArticulo = new ArticuloRepositorio(_integraDBContext);
                var rpta = repArticulo.ObtenerProgramasNoAsociadosArticulo(IdArticulo);

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Tags Asociados
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]/{IdArticulo}")]
        [HttpGet]
        public ActionResult ObtenerTagsAsociados(int IdArticulo)
        {
            try
            {
                ArticuloSeoRepositorio repArticuloSeo = new ArticuloSeoRepositorio(_integraDBContext);
                var rpta = repArticuloSeo.ObtenerTagsAsociadosArticulo(IdArticulo);

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Asociar Programas
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarProgramas([FromBody] ArticuloAsociadosDTO RequestDTO)
        {
            try
            {

                ArticuloPgeneralRepositorio repArticuloPgenaral = new ArticuloPgeneralRepositorio(_integraDBContext);
                List<ArticuloPgeneralBO> asociadosActualmente = (List < ArticuloPgeneralBO >) repArticuloPgenaral.GetBy(x => x.IdArticulo==RequestDTO.IdArticulo && x.Estado==true);
                List<ArticuloPgeneralBO> listaEliminar = new List<ArticuloPgeneralBO>();
                List<ArticuloPgeneralBO> listaInsertar = new List<ArticuloPgeneralBO>();

                foreach (ArticuloPgeneralBO item in asociadosActualmente)
                    if (!RequestDTO.IdsAsociados.Contains(item.IdPgeneral))
                    {
                        item.Estado = false;
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = RequestDTO.Usuario;
                        listaEliminar.Add(item);
                    }

               
                foreach (int IdPGeneralItem in RequestDTO.IdsAsociados)
                    if (asociadosActualmente.SingleOrDefault(x => x.IdPgeneral== IdPGeneralItem) ==null)
                        listaInsertar.Add(new ArticuloPgeneralBO {
                            IdArticulo=RequestDTO.IdArticulo,
                            IdPgeneral= IdPGeneralItem,
                            Estado=true,
                            FechaCreacion=DateTime.Now,
                            FechaModificacion=DateTime.Now,
                            UsuarioCreacion=RequestDTO.Usuario,
                            UsuarioModificacion= RequestDTO.Usuario
                        });

                if (listaEliminar.Count > 0)
                    repArticuloPgenaral.Update(listaEliminar);
                
                if (listaInsertar.Count > 0)
                    repArticuloPgenaral.Insert(listaInsertar);
                
                return Ok(new {Mensaje="Eliminados:" + listaEliminar.Count + "  Insertados:" + listaInsertar.Count, listaEliminar, listaInsertar});
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 18/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Asociar Tags
        /// </summary>
        /// <returns>Listas<returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarTags([FromBody] ArticuloAsociadosDTO RequestDTO)
        {
            try
            {

                ArticuloTagRepositorio repArticuloTag = new ArticuloTagRepositorio(_integraDBContext);
                
                List<ArticuloTagBO> asociadosActualmente = (List<ArticuloTagBO>)repArticuloTag.GetBy(x => x.IdArticulo == RequestDTO.IdArticulo && x.Estado == true);
                List<ArticuloTagBO> listaEliminar = new List<ArticuloTagBO>();
                List<ArticuloTagBO> listaInsertar = new List<ArticuloTagBO>();

                foreach (ArticuloTagBO item in asociadosActualmente)
                    if (!RequestDTO.IdsAsociados.Contains(item.IdTag))
                    {
                        item.Estado = false;
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = RequestDTO.Usuario;
                        listaEliminar.Add(item);
                    }


                foreach (int IdTagItem in RequestDTO.IdsAsociados)
                    if (asociadosActualmente.SingleOrDefault(x => x.IdTag == IdTagItem) == null)
                        listaInsertar.Add(new ArticuloTagBO
                        {
                            IdArticulo = RequestDTO.IdArticulo,
                            IdTag = IdTagItem,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = RequestDTO.Usuario,
                            UsuarioModificacion = RequestDTO.Usuario
                        });

                if (listaEliminar.Count > 0)
                    repArticuloTag.Update(listaEliminar);

                if (listaInsertar.Count > 0)
                    repArticuloTag.Insert(listaInsertar);

                return Ok(new { Mensaje = "Eliminados:" + listaEliminar.Count + "  Insertados:" + listaInsertar.Count, listaEliminar, listaInsertar });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
