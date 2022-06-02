using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
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
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/DocumentoPw")]
    public class DocumentoPwController : BaseController<TDocumentoPw, ValidadoDocumentoPwDTO>
    {
        public DocumentoPwController(IIntegraRepository<TDocumentoPw> repositorio, ILogger<BaseController<TDocumentoPw, ValidadoDocumentoPwDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
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
                DocumentoPwRepositorio _repDocumentoPw = new DocumentoPwRepositorio();
                return Ok(_repDocumentoPw.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPlantilla()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaPwRepositorio _repPlantillaPw = new PlantillaPwRepositorio();
                return Ok(_repPlantillaPw.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoTipoRevision()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRevisionPwRepositorio _repTipoRevisionPw = new TipoRevisionPwRepositorio();
                return Ok(_repTipoRevisionPw.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult ObtenerRevisionNivelPorIdPlantilla(int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RevisionNivelPwRepositorio _repRevisionNivelPw = new RevisionNivelPwRepositorio();
                return Ok(_repRevisionNivelPw.ObtenerRevisionNivelPorIdPlantilla(IdPlantilla));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult ObtenerPlantillaSeccionMaestraPorIdPlantilla(int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaPlantillaMaestroPwRepositorio _repPlantillaPlantillaMaestroPw = new PlantillaPlantillaMaestroPwRepositorio();
                SeccionPwRepositorio _repSeccionPw = new SeccionPwRepositorio();
                List<SeccionPwFiltroPlantillaPwListaSubSeccionesDTO> ListaSeccionSubSeccion = new List<SeccionPwFiltroPlantillaPwListaSubSeccionesDTO>();
                var ListaSeccionMaestra = _repPlantillaPlantillaMaestroPw.ObtenerPlantillaMaestroPorIdPlantillaPw(IdPlantilla);
                var ListaSeccion = _repSeccionPw.ObtenerSeccionesPorIdPlantillaPW(IdPlantilla);
                var ListaUnida = ListaSeccionMaestra.Concat(ListaSeccion).ToList();

                ListaSeccionSubSeccion = ListaUnida.GroupBy(u => (u.IdPlantilla, u.Id,u.Nombre))
                                    .Select(group => 
                                    new SeccionPwFiltroPlantillaPwListaSubSeccionesDTO
                                    {Id=group.Key.Id
                                    ,IdPlantilla=group.Key.IdPlantilla
                                    ,IdPlantillaPw=group.Key.IdPlantilla
                                    ,Nombre=group.Key.Nombre
                                    ,Descripcion=group.Select(x=>x.Descripcion).FirstOrDefault()
                                    ,Contenido=group.Select(x=>x.Contenido).FirstOrDefault()
                                    ,VisibleWeb=group.Select(x=>x.VisibleWeb).FirstOrDefault()
                                    ,ZonaWeb=group.Select(x=>x.ZonaWeb).FirstOrDefault()
                                    ,OrdenEeb=group.Select(x=>x.OrdenEeb).FirstOrDefault()
                                    ,Titulo=group.Select(x=>x.Titulo).FirstOrDefault()
                                    ,Posicion=group.Select(x=>x.Posicion).FirstOrDefault()
                                    ,Tipo=group.Select(x=>x.Tipo).FirstOrDefault()
                                    ,IdSeccionMaestraPw=group.Select(x=>x.IdSeccionMaestraPw).FirstOrDefault()
                                    ,IdSeccionTipoContenido=group.Select(x=>x.IdSeccionTipoContenido).FirstOrDefault()
                                    ,NombreSeccionTipoContenido=group.Select(x=>x.NombreSeccionTipoContenido).FirstOrDefault()                                    
                                    , ListaSubSeccionesPw = group.Select(x=> new SubSeccionTipoDetallePwDTO { IdSeccionTipoDetallePw=x.IdSeccionTipoDetallePw, NombreSubSeccion=x.NombreSubSeccion, IdSubSeccionTipoContenido=x.IdSubSeccionTipoContenido }).ToList() }).ToList();


                return Ok(ListaSeccionSubSeccion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult ObtenerSeccionPorIdPlantilla(int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SeccionPwRepositorio _repSeccionPw = new SeccionPwRepositorio();
                return Ok(_repSeccionPw.ObtenerSeccionesPorIdPlantillaPW(IdPlantilla));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdDocumentoSeccion}")]
        [HttpGet]
        public ActionResult ObtenerDocumentoSeccion(int IdDocumentoSeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
                var listaDocumentoSeccion = _repDocumentoSeccionPw.ObtenerDocumentoSeccionPorId(IdDocumentoSeccion);
                foreach (var item in listaDocumentoSeccion)
                {
                    if(item.Tipo == 1) //Maestra
                    {
                        PlantillaPlantillaMaestroPwRepositorio _repPlantillaPlantillaMaestro = new PlantillaPlantillaMaestroPwRepositorio();
                        var objetoTemporal = _repPlantillaPlantillaMaestro.GetBy(x => x.IdPlantillaPw == item.IdPlantillaPw && x.IdSeccionMaestraPw == item.IdSeccionPW).FirstOrDefault();
                        item.Contenido = objetoTemporal.Contenido;
                    }
                }
                return Ok(listaDocumentoSeccion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //revisar lo que devuelve es un entero??
        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDatosSeccion(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SeccionPwRepositorio _repSeccionPw = new SeccionPwRepositorio();
                return Ok(_repSeccionPw.FirstById(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //revisar lo que debe retornar
        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDatosSeccionMaestra(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SeccionMaestraPwRepositorio _repSeccionMaestraPw = new SeccionMaestraPwRepositorio();
                return Ok(_repSeccionMaestraPw.FirstById(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarDocumento([FromBody] CompuestoDocumentoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoPwRepositorio _repDocumentoPW = new DocumentoPwRepositorio();
                DocumentoPwBO documentoBO = new DocumentoPwBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    documentoBO.Nombre = Json.ObjetoDocumento.Nombre;
                    documentoBO.IdPlantillaPw = Json.ObjetoDocumento.IdPlantillaPw;
                    documentoBO.EstadoFlujo = Json.ObjetoDocumento.EstadoFlujo;
                    documentoBO.Asignado = Json.ObjetoDocumento.Asignado;
                    documentoBO.Estado = true;
                    documentoBO.UsuarioCreacion = Json.Usuario;
                    documentoBO.UsuarioModificacion = Json.Usuario;
                    documentoBO.FechaCreacion = DateTime.Now;
                    documentoBO.FechaModificacion = DateTime.Now;

                    documentoBO.DocumentoSeccion = new List<DocumentoSeccionPwBO>();
                    documentoBO.BandejaPendiente = new List<BandejaPendientePwBO>();

                    foreach (var item in Json.Lista)
                    {
                        if (item.listaGridListaSecciones.Count() > 0)
                        {
                            foreach (var subseccion in item.listaGridListaSecciones) {
                                DocumentoSeccionPwBO documentoSeccionBO = new DocumentoSeccionPwBO();
                                documentoSeccionBO.IdDocumentoPw = Json.ObjetoDocumento.Id;
                                if (item.Tipo == 1)
                                {
                                    documentoSeccionBO.IdSeccionPw = item.IdSeccionMaestraPw;
                                    documentoSeccionBO.Titulo = "Titulo Maestra";
                                }
                                else
                                {
                                    documentoSeccionBO.IdSeccionPw = item.Id;
                                    if (string.IsNullOrEmpty(item.Titulo))
                                        documentoSeccionBO.Titulo = "Titulo";
                                    else
                                        documentoSeccionBO.Titulo = item.Titulo;
                                }
                                documentoSeccionBO.IdPlantillaPw = item.IdPlantillaPw;
                                documentoSeccionBO.Posicion = item.Posicion;
                                documentoSeccionBO.Tipo = item.Tipo;
                                documentoSeccionBO.Cabecera = item.Cabecera;
                                documentoSeccionBO.PiePagina = item.PiePagina;
                                documentoSeccionBO.Contenido = subseccion.Valor;
                                documentoSeccionBO.IdSeccionTipoDetallePw = Int32.Parse(subseccion.Clave.Substring(1, subseccion.Clave.IndexOf("_", 1) - 1));
                                documentoSeccionBO.NumeroFila = subseccion.NumeroFila;

                                documentoSeccionBO.VisibleWeb = item.VisibleWeb;
                                documentoSeccionBO.ZonaWeb = item.ZonaWeb;
                                documentoSeccionBO.OrdenWeb = item.OrdenEeb;

                                documentoSeccionBO.Estado = true;
                                documentoSeccionBO.UsuarioCreacion = Json.Usuario;
                                documentoSeccionBO.UsuarioModificacion = Json.Usuario;
                                documentoSeccionBO.FechaCreacion = DateTime.Now;
                                documentoSeccionBO.FechaModificacion = DateTime.Now;

                                documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                            }

                        }
                        else if(item.IdSeccionTipoContenido!=1)
                        {

                            byte[] _base64 = Convert.FromBase64String(item.Contenido);
                            var _contenido = Encoding.UTF8.GetString(_base64);
                            DocumentoSeccionPwBO documentoSeccionBO = new DocumentoSeccionPwBO();
                            documentoSeccionBO.IdDocumentoPw = Json.ObjetoDocumento.Id;
                            if (item.Tipo == 1)
                            {
                                documentoSeccionBO.IdSeccionPw = item.IdSeccionMaestraPw;
                                documentoSeccionBO.Titulo = "Titulo Maestra";
                            }
                            else
                            {
                                documentoSeccionBO.IdSeccionPw = item.Id;
                                if (string.IsNullOrEmpty(item.Titulo))
                                    documentoSeccionBO.Titulo = "Titulo";
                                else
                                    documentoSeccionBO.Titulo = item.Titulo;
                            }
                            documentoSeccionBO.IdPlantillaPw = item.IdPlantillaPw;
                            documentoSeccionBO.Posicion = item.Posicion;
                            documentoSeccionBO.Tipo = item.Tipo;
                            documentoSeccionBO.Cabecera = item.Cabecera;
                            documentoSeccionBO.PiePagina = item.PiePagina;
                            documentoSeccionBO.Contenido = _contenido;

                            documentoSeccionBO.VisibleWeb = item.VisibleWeb;
                            documentoSeccionBO.ZonaWeb = item.ZonaWeb;
                            documentoSeccionBO.OrdenWeb = item.OrdenEeb;

                            documentoSeccionBO.Estado = true;
                            documentoSeccionBO.UsuarioCreacion = Json.Usuario;
                            documentoSeccionBO.UsuarioModificacion = Json.Usuario;
                            documentoSeccionBO.FechaCreacion = DateTime.Now;
                            documentoSeccionBO.FechaModificacion = DateTime.Now;

                            documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                        }
                       
                    }

                    if (Json.ListaRevision.Count > 0)
                    {
                        Json.ListaRevision = Json.ListaRevision.OrderByDescending(x => x.Prioridad).ToList();
                        int NumeroRegistros = Json.ListaRevision.Count;
                        int contador = 1;

                        foreach(var item in Json.ListaRevision)
                        {
                            BandejaPendientePwRepositorio _repBandejaPendiente = new BandejaPendientePwRepositorio();
                            BandejaPendientePwBO bandejaPendienteBO = new BandejaPendientePwBO();

                            bandejaPendienteBO.IdDocumentoPw = Json.ObjetoDocumento.Id;
                            bandejaPendienteBO.IdRevisionNivelPw = item.IdRevisionNivelPw;
                            bandejaPendienteBO.Secuencia = item.Prioridad;
                            if (contador != NumeroRegistros)
                                bandejaPendienteBO.EsFinal = 0; //NO
                            else
                                bandejaPendienteBO.EsFinal = 1; // si
                            if (contador == 1)
                                bandejaPendienteBO.EsInicio = 1; // si
                            else
                                bandejaPendienteBO.EsInicio = 0; //NO
                            bandejaPendienteBO.IdPersonal = item.Identificador;
                            if (contador == 1) // si es el primero
                                bandejaPendienteBO.EstadoRevisar = 1;// pendiente
                            else
                                bandejaPendienteBO.EstadoRevisar = 0; // Asigando

                            bandejaPendienteBO.Estado = true;
                            bandejaPendienteBO.UsuarioCreacion = Json.Usuario;
                            bandejaPendienteBO.UsuarioModificacion = Json.Usuario;
                            bandejaPendienteBO.FechaCreacion = DateTime.Now;
                            bandejaPendienteBO.FechaModificacion = DateTime.Now;

                            documentoBO.BandejaPendiente.Add(bandejaPendienteBO);

                            //if (bandejaPendienteBO.EsInicio == 1)
                            //    _repBandejaPendiente.GetBy(x => x.IdPersonal == bandejaPendienteBO.IdPersonal).FirstOrDefault();

                            contador++;
                        }
                    }
                    _repDocumentoPW.Insert(documentoBO);
                    scope.Complete();
                }
                return Ok(documentoBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarDocumento([FromBody] CompuestoDocumentoPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                DocumentoPwRepositorio _repDocumentoPw = new DocumentoPwRepositorio(contexto);
                DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(contexto);
                BandejaPendientePwRepositorio _repBandejaPendiente = new BandejaPendientePwRepositorio(contexto);
                PGeneralCriterioEvaluacionRepositorio _repCriterioEvaluacion = new PGeneralCriterioEvaluacionRepositorio(contexto);

                DocumentoPwBO documentoBO = new DocumentoPwBO();
                List<DocumentoSeccionPwBO> documentoSeccionBOAuxiliar = new List<DocumentoSeccionPwBO>(); ;

                using (TransactionScope scope = new TransactionScope())
                {
                    _repDocumentoSeccionPw.EliminacionLogicoPorIdDocumento(Json.ObjetoDocumento.Id, Json.Usuario, Json.Lista);
                    _repBandejaPendiente.EliminacionLogicoPorIdDocumento(Json.ObjetoDocumento.Id, Json.Usuario, Json.ListaRevision);

                    documentoBO = _repDocumentoPw.FirstById(Json.ObjetoDocumento.Id);
                    documentoBO.Nombre = Json.ObjetoDocumento.Nombre;
                    documentoBO.IdPlantillaPw = Json.ObjetoDocumento.IdPlantillaPw;
                    documentoBO.EstadoFlujo = Json.ObjetoDocumento.EstadoFlujo;
                    documentoBO.Asignado = Json.ObjetoDocumento.Asignado;
                    documentoBO.UsuarioModificacion = Json.Usuario;
                    documentoBO.FechaModificacion = DateTime.Now;

                    documentoBO.DocumentoSeccion = new List<DocumentoSeccionPwBO>();
                    documentoBO.BandejaPendiente = new List<BandejaPendientePwBO>();


                    List<DocumentoSeccionPwBO> documentos = new List<DocumentoSeccionPwBO>();
                    foreach (var item in Json.Lista)
                    {
                        //byte[] _base64 = Convert.FromBase64String(item.Contenido);
                        //var _contenido = Encoding.UTF8.GetString(_base64);
                        DocumentoSeccionPwBO documentoSeccionBO;
                        //var temporal = _repDocumentoSeccionPw.FirstBy(w=>w.IdSeccionPw== item.IdSeccionPW && w.IdDocumentoPw == Json.ObjetoDocumento.Id);
                        var temporal2 = _repDocumentoSeccionPw.Exist(w => w.IdSeccionPw == item.IdSeccionPW && w.IdDocumentoPw == Json.ObjetoDocumento.Id);
                        
                        if (temporal2)
                        {

                            if (item.listaGridListaSecciones.Count() > 0)  //&& item.IdSeccionPW == 91
                            {
                                //documentoSeccionBO = _repDocumentoSeccionPw.FirstBy(x => x.IdSeccionPw == item.Id && x.IdDocumentoPw == Json.ObjetoDocumento.Id);
                                var filanueva = item.listaGridListaSecciones.Count();
                                foreach (var item2 in item.listaGridListaSecciones)
                                {
                                    var benExist = _repDocumentoSeccionPw.Exist(w => w.IdSeccionPw == item.IdSeccionPW && w.IdDocumentoPw == Json.ObjetoDocumento.Id && w.Contenido == item2.Valor);
                                    filanueva = filanueva + 1;
                                    
                                    if (!benExist)
                                    {
                                        documentoSeccionBO = new DocumentoSeccionPwBO();
                                        if (string.IsNullOrEmpty(item.Titulo))
                                            documentoSeccionBO.Titulo = "Titulo";
                                        else
                                            documentoSeccionBO.Titulo = item.Titulo;
                                        documentoSeccionBO.Cabecera = item.Cabecera;
                                        documentoSeccionBO.PiePagina = item.PiePagina;
                                        documentoSeccionBO.Contenido = item2.Valor;
                                        foreach  (var cont in item.listaGridListaSecciones)
                                        {
                                            if (filanueva == cont.NumeroFila) filanueva++;
                                        }
                                        documentoSeccionBO.NumeroFila = filanueva;
                                        documentoSeccionBO.IdSeccionTipoDetallePw = int.Parse(item2.Clave.Substring(1, item2.Clave.IndexOf("_", 1) - 1));
                                        documentoSeccionBO.IdPlantillaPw = item.IdPlantillaPw;
                                        documentoSeccionBO.Posicion = item.Posicion;
                                        documentoSeccionBO.Tipo = item.Tipo;
                                        documentoSeccionBO.IdDocumentoPw = item.IdDocumentoPW;
                                        documentoSeccionBO.IdSeccionPw = item.IdSeccionPW;
                                        documentoSeccionBO.VisibleWeb = item.VisibleWeb;
                                        documentoSeccionBO.ZonaWeb = item.ZonaWeb;
                                        documentoSeccionBO.OrdenWeb = item.OrdenEeb;
                                        documentoSeccionBO.UsuarioCreacion = Json.Usuario;
                                        documentoSeccionBO.UsuarioModificacion = Json.Usuario;
                                        documentoSeccionBO.FechaCreacion = DateTime.Now;
                                        documentoSeccionBO.FechaModificacion = DateTime.Now;
                                        documentoSeccionBO.Estado = true;
                                        documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                                    }
                                    else
                                    {
                                        documentoSeccionBO = _repDocumentoSeccionPw.FirstBy(x => x.IdSeccionPw == item.IdSeccionPW && x.IdDocumentoPw == Json.ObjetoDocumento.Id && x.Contenido == item2.Valor && x.Estado == true);
                                        bool actualizarCabecera = false, actualizarPie = false;

                                        if (documentoSeccionBO != null)
                                        {
                                            if (documentoSeccionBO.Cabecera != item.Cabecera)
                                            {
                                                documentoSeccionBO.Cabecera = item.Cabecera;
                                                actualizarCabecera = true;
                                            }
                                            if (documentoSeccionBO.PiePagina != item.PiePagina)
                                            {
                                                documentoSeccionBO.PiePagina = item.PiePagina;
                                                actualizarPie = true;
                                            }

                                            if (actualizarCabecera || actualizarPie)
                                            {
                                                documentoSeccionBO.UsuarioModificacion = Json.Usuario;
                                                documentoSeccionBO.FechaModificacion = DateTime.Now;
                                                documentoSeccionBOAuxiliar.Add(documentoSeccionBO);
                                            }
                                            
                                        }
                                    }
                                    //else
                                    //{
                                    //    //documentoSeccionBO.UsuarioCreacion = Json.Usuario;
                                    //    documentoSeccionBO = _repDocumentoSeccionPw.FirstBy(x => x.IdSeccionPw == item.Id && x.Contenido == item2.Valor && x.Estado ==true && x.IdDocumentoPw == Json.ObjetoDocumento.Id);
                                    //    //documentoSeccionBO = _repDocumentoSeccionPw.GetBy(beneficioget);
                                    //    documentoSeccionBO.UsuarioModificacion = Json.Usuario;
                                    //    documentoSeccionBO.FechaCreacion = DateTime.Now;
                                    //    documentoSeccionBO.FechaModificacion = DateTime.Now;
                                    //    documentoSeccionBO.Estado = true;
                                    //    documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                                    //}
                                }
                            }
                            else
                            {
                                documentoSeccionBO = _repDocumentoSeccionPw.FirstBy(x => x.IdSeccionPw == item.Id && x.IdDocumentoPw == Json.ObjetoDocumento.Id);
                                if (string.IsNullOrEmpty(item.Titulo))
                                    documentoSeccionBO.Titulo = "Titulo";
                                else
                                    documentoSeccionBO.Titulo = item.Titulo;
                                documentoSeccionBO.Cabecera = item.Cabecera;
                                documentoSeccionBO.PiePagina = item.PiePagina;
                                documentoSeccionBO.Contenido = item.Contenido;
                                documentoSeccionBO.IdSeccionTipoDetallePw = item.IdSeccionTipoDetallePw;
                                documentoSeccionBO.IdPlantillaPw = item.IdPlantillaPw;
                                documentoSeccionBO.Posicion = item.Posicion;
                                documentoSeccionBO.Tipo = item.Tipo;
                                documentoSeccionBO.IdSeccionPw = item.IdSeccionPW;
                                documentoSeccionBO.VisibleWeb = item.VisibleWeb;
                                documentoSeccionBO.ZonaWeb = item.ZonaWeb;
                                documentoSeccionBO.OrdenWeb = item.OrdenEeb;
                                documentoSeccionBO.UsuarioModificacion = Json.Usuario;
                                documentoSeccionBO.FechaModificacion = DateTime.Now;
                                documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                            }

                        }
                        else
                        {
                            if (item.listaGridListaSecciones.Count() > 0)
                            {
                                if (item.Titulo == "Estructura Curricular")
                                {
                                    item.listaGridListaSecciones = item.listaGridListaSecciones.OrderBy(x => x.NumeroFila).ToList();
                                }


                                foreach (var item2 in item.listaGridListaSecciones)
                                {
                                    documentoSeccionBO = new DocumentoSeccionPwBO();

                                    if (string.IsNullOrEmpty(item.Titulo))
                                        documentoSeccionBO.Titulo = "Titulo";
                                    else
                                        documentoSeccionBO.Titulo = item.Titulo;
                                    documentoSeccionBO.Cabecera = item.Cabecera;
                                    documentoSeccionBO.PiePagina = item.PiePagina;
                                    documentoSeccionBO.Contenido = item2.Valor;
                                    documentoSeccionBO.NumeroFila = item2.NumeroFila;
                                    documentoSeccionBO.IdSeccionTipoDetallePw = int.Parse(item2.Clave.Substring(1, item2.Clave.IndexOf("_", 1) - 1));
                                    documentoSeccionBO.IdPlantillaPw = item.IdPlantillaPw;
                                    documentoSeccionBO.Posicion = item.Posicion;
                                    documentoSeccionBO.Tipo = item.Tipo;
                                    documentoSeccionBO.IdDocumentoPw = item.IdDocumentoPW;
                                    documentoSeccionBO.IdSeccionPw = item.IdSeccionPW;
                                    documentoSeccionBO.VisibleWeb = item.VisibleWeb;
                                    documentoSeccionBO.ZonaWeb = item.ZonaWeb;
                                    documentoSeccionBO.OrdenWeb = item.OrdenEeb;
                                    documentoSeccionBO.UsuarioCreacion = Json.Usuario;
                                    documentoSeccionBO.UsuarioModificacion = Json.Usuario;
                                    documentoSeccionBO.FechaCreacion = DateTime.Now;
                                    documentoSeccionBO.FechaModificacion = DateTime.Now;
                                    documentoSeccionBO.Estado = true;
                                    documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                                }

                            }
                            else if(item.IdSeccionTipoContenido!=1)
                            {
                                byte[] _base64 = Convert.FromBase64String(item.Contenido);
                                var _contenido = Encoding.UTF8.GetString(_base64);
                                documentoSeccionBO = new DocumentoSeccionPwBO();
                                if (string.IsNullOrEmpty(item.Titulo))
                                    documentoSeccionBO.Titulo = "Titulo";
                                else
                                    documentoSeccionBO.Titulo = item.Titulo;                               
                                documentoSeccionBO.Cabecera = item.Cabecera;
                                documentoSeccionBO.PiePagina = item.PiePagina;
                                documentoSeccionBO.Contenido = _contenido;
                                documentoSeccionBO.IdSeccionTipoDetallePw = item.IdSeccionTipoDetallePw;
                                documentoSeccionBO.IdPlantillaPw = item.IdPlantillaPw;
                                documentoSeccionBO.Posicion = item.Posicion;
                                documentoSeccionBO.Tipo = item.Tipo;
                                documentoSeccionBO.IdSeccionPw = item.IdSeccionPW;
                                documentoSeccionBO.VisibleWeb = item.VisibleWeb;
                                documentoSeccionBO.ZonaWeb = item.ZonaWeb;
                                documentoSeccionBO.OrdenWeb = item.OrdenEeb;
                                documentoSeccionBO.UsuarioCreacion = Json.Usuario;
                                documentoSeccionBO.UsuarioModificacion = Json.Usuario;
                                documentoSeccionBO.FechaCreacion = DateTime.Now;
                                documentoSeccionBO.FechaModificacion = DateTime.Now;
                                documentoSeccionBO.Estado = true;
                                documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                            }
                        }
                        //foreach (var x in documentos) {
                        //    documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                        //}
                        ////     documentoBO.DocumentoSeccion.Add(documentoSeccionBO);
                        //documentos = null;
                    }

                    foreach (var item in Json.ListaRevision)
                    {
                        var entidad = _repBandejaPendiente.GetBy(x => x.IdDocumentoPw == Json.ObjetoDocumento.Id && x.IdRevisionNivelPw == item.IdRevisionNivelPw && x.Estado == true).FirstOrDefault();

                        if ( entidad != null)
                        {
                            entidad.IdPersonal = item.Identificador;

                            BandejaPendientePwBO bandejaPendientePwBO;
                            if (_repBandejaPendiente.Exist(x => x.IdRevisionNivelPw == item.IdRevisionNivelPw && x.IdDocumentoPw == Json.ObjetoDocumento.Id))
                            {
                                bandejaPendientePwBO = _repBandejaPendiente.FirstBy(x => x.IdRevisionNivelPw == item.IdRevisionNivelPw && x.IdDocumentoPw == Json.ObjetoDocumento.Id);
                                bandejaPendientePwBO.IdDocumentoPw = item.IdDocumento;
                                bandejaPendientePwBO.Secuencia = item.Prioridad;
                                bandejaPendientePwBO.UsuarioModificacion = Json.Usuario;
                                bandejaPendientePwBO.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                bandejaPendientePwBO = new BandejaPendientePwBO();

                                bandejaPendientePwBO.IdDocumentoPw = item.IdDocumento;
                                bandejaPendientePwBO.Secuencia = item.Prioridad;
                                bandejaPendientePwBO.UsuarioCreacion = Json.Usuario;
                                bandejaPendientePwBO.UsuarioModificacion = Json.Usuario;
                                bandejaPendientePwBO.FechaCreacion = DateTime.Now;
                                bandejaPendientePwBO.FechaModificacion = DateTime.Now;
                                bandejaPendientePwBO.Estado = true;

                                documentoBO.BandejaPendiente.Add(bandejaPendientePwBO);
                            }
                        }
                    }

                    if(Json.ListaCriterioEvaluacionPresencial != null && Json.ListaCriterioEvaluacionPresencial.Count > 0)
                    {
                        foreach (var item in _repCriterioEvaluacion.GetBy(x => x.IdModalidadCurso == 0))
                        {
                            item.Estado = false;
                            item.UsuarioModificacion = Json.Usuario;
                            item.FechaModificacion = DateTime.Now;

                            _repCriterioEvaluacion.Update(item);
                        }
                        foreach (var item in Json.ListaCriterioEvaluacionPresencial)
                        {
                            PGeneralCriterioEvaluacionBO criterioBO = new PGeneralCriterioEvaluacionBO();
                            criterioBO.IdPgeneral = item.IdPgeneral;
                            criterioBO.IdModalidadCurso = item.IdModalidadCurso;
                            criterioBO.Nombre = item.Nombre;
                            criterioBO.Porcentaje = item.Porcentaje;
                            criterioBO.Estado = true;
                            criterioBO.UsuarioCreacion = Json.Usuario;
                            criterioBO.UsuarioModificacion = Json.Usuario;
                            criterioBO.FechaCreacion = DateTime.Now;
                            criterioBO.FechaModificacion = DateTime.Now;

                            _repCriterioEvaluacion.Insert(criterioBO);
                        }
                    }

                    if (Json.ListaCriterioEvaluacionAOnline != null && Json.ListaCriterioEvaluacionAOnline.Count > 0)
                    {
                        foreach (var item in _repCriterioEvaluacion.GetBy(x => x.IdModalidadCurso == 1))
                        {
                            item.Estado = false;
                            item.UsuarioModificacion = Json.Usuario;
                            item.FechaModificacion = DateTime.Now;

                            _repCriterioEvaluacion.Update(item);
                        }
                        foreach (var item in Json.ListaCriterioEvaluacionAOnline)
                        {
                            PGeneralCriterioEvaluacionBO criterioBO = new PGeneralCriterioEvaluacionBO();
                            criterioBO.IdPgeneral = item.IdPgeneral;
                            criterioBO.IdModalidadCurso = item.IdModalidadCurso;
                            criterioBO.Nombre = item.Nombre;
                            criterioBO.Porcentaje = item.Porcentaje;
                            criterioBO.Estado = true;
                            criterioBO.UsuarioCreacion = Json.Usuario;
                            criterioBO.UsuarioModificacion = Json.Usuario;
                            criterioBO.FechaCreacion = DateTime.Now;
                            criterioBO.FechaModificacion = DateTime.Now;

                            _repCriterioEvaluacion.Insert(criterioBO);
                        }
                    }

                    if (Json.ListaCriterioEvaluacionOnline != null && Json.ListaCriterioEvaluacionOnline.Count > 0)
                    {
                        foreach (var item in _repCriterioEvaluacion.GetBy(x => x.IdModalidadCurso == 2))
                        {
                            item.Estado = false;
                            item.UsuarioModificacion = Json.Usuario;
                            item.FechaModificacion = DateTime.Now;

                            _repCriterioEvaluacion.Update(item);
                        }
                        foreach (var item in Json.ListaCriterioEvaluacionOnline)
                        {
                            PGeneralCriterioEvaluacionBO criterioBO = new PGeneralCriterioEvaluacionBO();
                            criterioBO.IdPgeneral = item.IdPgeneral;
                            criterioBO.IdModalidadCurso = item.IdModalidadCurso;
                            criterioBO.Nombre = item.Nombre;
                            criterioBO.Porcentaje = item.Porcentaje;
                            criterioBO.Estado = true;
                            criterioBO.UsuarioCreacion = Json.Usuario;
                            criterioBO.UsuarioModificacion = Json.Usuario;
                            criterioBO.FechaCreacion = DateTime.Now;
                            criterioBO.FechaModificacion = DateTime.Now;

                            _repCriterioEvaluacion.Insert(criterioBO);
                        }
                    }

                    _repDocumentoPw.Update(documentoBO);
                    scope.Complete();
                }


                using (TransactionScope scopeAct = new TransactionScope())
                {
                    if (documentoSeccionBOAuxiliar.Count>0)
                    {
                        foreach (var item in documentoSeccionBOAuxiliar)
                        {
                            _repDocumentoSeccionPw.Update(item);
                        }
                    }
                    scopeAct.Complete();
                }

                return Ok(documentoBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarDocumento(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                DocumentoPwRepositorio _repDocumentoPw = new DocumentoPwRepositorio(contexto);
                DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(contexto);
                BandejaPendientePwRepositorio _repBandejaPendiente = new BandejaPendientePwRepositorio(contexto);

                DocumentoPwBO documentoBO = new DocumentoPwBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repDocumentoPw.Exist(Id))
                    {
                        _repDocumentoPw.Delete(Id, Usuario);
                        var hijosDocumentoSeccion = _repDocumentoSeccionPw.ObtenerDocumentoSeccionPorId(Id);
                        //var hijosPlataforma = _repDocumentoSeccionPw.GetBy(x => x.IdDocumentoPw == Id);
                        foreach (var hijo in hijosDocumentoSeccion)
                        {
                            _repDocumentoSeccionPw.Delete(hijo.Id, Usuario);
                        }
                       // var listaBandejaPendiente = _repBandejaPendiente.GetBy(x => x.IdDocumentoPw == Id && x.Estado == true).ToList();
                        var hijosBandejaPendiente = _repBandejaPendiente.GetBy(x => x.IdDocumentoPw == Id);
                        foreach (var hijo in hijosBandejaPendiente)
                        {
                            _repBandejaPendiente.Delete(hijo.Id, Usuario);
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

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodoUsuariosPorApellidoAutocomplete([FromBody] PersonalApellidoAutocompleteDTO ApellidoPaterno)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                return Ok(_repPersonal.ObtenerPersonalAutocompletoPorApellidoPaterno(ApellidoPaterno.ApellidoPaterno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{idDocumento}")]
        [HttpGet]
        public ActionResult ObtenerDocumentoSeccionEditar(int IdDocumento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
                List<DocumentoSeccionPwFiltroAgrupadoDTO> agrupado = new List<DocumentoSeccionPwFiltroAgrupadoDTO>();
                var listaDocumentoSeccion = _repDocumentoSeccionPw.ObtenerDocumentoSeccionPorId(IdDocumento);
                foreach (var item in listaDocumentoSeccion)
                {
                    if (item.Tipo == 1) //SeccionMaestra
                    {
                        PlantillaPlantillaMaestroPwRepositorio _repPlantillaPlantillaMaestro = new PlantillaPlantillaMaestroPwRepositorio();
                        var objetoTemporal = _repPlantillaPlantillaMaestro.GetBy(x => x.IdPlantillaPw == item.IdPlantillaPw && x.IdSeccionMaestraPw == item.IdSeccionPW).FirstOrDefault();
                        item.Contenido = objetoTemporal.Contenido;
                    }
                }

                agrupado = listaDocumentoSeccion.GroupBy(u => (u.IdDocumentoPW, u.IdSeccionPW, u.IdSeccionTipoContenido))
                                    .Select(group =>
                                    new DocumentoSeccionPwFiltroAgrupadoDTO
                                    {
                                        Id=group.Select(x=>x.Id).FirstOrDefault()
                                        ,Titulo= group.Select(x => x.Titulo).FirstOrDefault()
                                        ,VisibleWeb= group.Select(x => x.VisibleWeb).FirstOrDefault()
                                        ,ZonaWeb= group.Select(x => x.ZonaWeb).FirstOrDefault()
                                        ,OrdenEeb= group.Select(x => x.OrdenEeb).FirstOrDefault()
                                        ,Contenido= group.Select(x => x.Contenido).FirstOrDefault()
                                        ,IdPlantillaPW= group.Select(x => x.IdPlantillaPw).FirstOrDefault()
                                        ,Posicion= group.Select(x => x.Posicion).FirstOrDefault()
                                        ,Tipo= group.Select(x => x.Tipo).FirstOrDefault()
                                        ,IdDocumentoPW=group.Key.IdDocumentoPW
                                        ,IdSeccionPW=group.Key.IdSeccionPW
                                        ,IdSeccionTipoContenido = group.Key.IdSeccionTipoContenido
                                        ,Cabecera = group.Select(x => x.Cabecera).FirstOrDefault()
                                        ,PiePagina = group.Select(x => x.PiePagina).FirstOrDefault()
                                        ,ListaSubSeccionesPw = group.Select(x => new SubSeccionTipoDetallePwDTO { IdSeccionTipoDetallePw = x.IdSeccionTipoDetallePw, NombreSubSeccion = x.NombreSubSeccion, IdSubSeccionTipoContenido = x.IdSubSeccionTipoContenido, ContenidoSubSeccion=x.ContenidoSubSeccion, NumeroFila = x.NumeroFila}).OrderBy(x=>x.NumeroFila).ToList()
                                    }).ToList();

                foreach (var item in agrupado) {
                    List<SubSeccionTipoDetallePwDTO> eliminar = new List<SubSeccionTipoDetallePwDTO>();
                    foreach (var itemInterno in item.ListaSubSeccionesPw) {
                        if (itemInterno.IdSeccionTipoDetallePw == null) {
                            eliminar.Add(itemInterno);
                        }
                    }
                    foreach (var itemEliminar in eliminar) {
                        item.ListaSubSeccionesPw.Remove(itemEliminar);
                    }
                }

                return Ok(agrupado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{IdPEspecificoGlobal}")]
        [HttpGet]
        public ActionResult ObtenerDocumentoSeccionCalificarPortalEditar(int IdPEspecificoGlobal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
                List<DocumentoSeccionPwFiltroAgrupadoDTO> agrupado = new List<DocumentoSeccionPwFiltroAgrupadoDTO>();
                var listaDocumentoSeccion = _repDocumentoSeccionPw.ObtenerDocumentoSeccionPorIdPEspecifico(IdPEspecificoGlobal);
                foreach (var item in listaDocumentoSeccion)
                {
                    if (item.Tipo == 1) //SeccionMaestra
                    {
                        PlantillaPlantillaMaestroPwRepositorio _repPlantillaPlantillaMaestro = new PlantillaPlantillaMaestroPwRepositorio();
                        var objetoTemporal = _repPlantillaPlantillaMaestro.GetBy(x => x.IdPlantillaPw == item.IdPlantillaPw && x.IdSeccionMaestraPw == item.IdSeccionPW).FirstOrDefault();
                        item.Contenido = objetoTemporal.Contenido;
                    }
                }

                agrupado = listaDocumentoSeccion.GroupBy(u => (u.IdDocumentoPW, u.IdSeccionPW, u.IdSeccionTipoContenido))
                                    .Select(group =>
                                    new DocumentoSeccionPwFiltroAgrupadoDTO
                                    {
                                        Id = group.Select(x => x.Id).FirstOrDefault()
                                        ,
                                        Titulo = group.Select(x => x.Titulo).FirstOrDefault()
                                        ,
                                        VisibleWeb = group.Select(x => x.VisibleWeb).FirstOrDefault()
                                        ,
                                        ZonaWeb = group.Select(x => x.ZonaWeb).FirstOrDefault()
                                        ,
                                        OrdenEeb = group.Select(x => x.OrdenEeb).FirstOrDefault()
                                        ,
                                        Contenido = group.Select(x => x.Contenido).FirstOrDefault()
                                        ,
                                        IdPlantillaPW = group.Select(x => x.IdPlantillaPw).FirstOrDefault()
                                        ,
                                        Posicion = group.Select(x => x.Posicion).FirstOrDefault()
                                        ,
                                        Tipo = group.Select(x => x.Tipo).FirstOrDefault()
                                        ,
                                        IdDocumentoPW = group.Key.IdDocumentoPW
                                        ,
                                        IdSeccionPW = group.Key.IdSeccionPW
                                        ,
                                        IdSeccionTipoContenido = group.Key.IdSeccionTipoContenido
                                        ,
                                        Cabecera = group.Select(x => x.Cabecera).FirstOrDefault()
                                        ,
                                        PiePagina = group.Select(x => x.PiePagina).FirstOrDefault()
                                        ,
                                        ListaSubSeccionesPw = group.Select(x => new SubSeccionTipoDetallePwDTO { IdSeccionTipoDetallePw = x.IdSeccionTipoDetallePw, NombreSubSeccion = x.NombreSubSeccion, IdSubSeccionTipoContenido = x.IdSubSeccionTipoContenido, ContenidoSubSeccion = x.ContenidoSubSeccion, NumeroFila = x.NumeroFila }).ToList()
                                    }).ToList();

                foreach (var item in agrupado)
                {
                    List<SubSeccionTipoDetallePwDTO> eliminar = new List<SubSeccionTipoDetallePwDTO>();
                    foreach (var itemInterno in item.ListaSubSeccionesPw)
                    {
                        if (itemInterno.IdSeccionTipoDetallePw == null)
                        {
                            eliminar.Add(itemInterno);
                        }
                    }
                    foreach (var itemEliminar in eliminar)
                    {
                        item.ListaSubSeccionesPw.Remove(itemEliminar);
                    }
                }

                return Ok(agrupado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdAlumno}")]
        [HttpPost]
        public ActionResult ObtenerDocumentoSeccionesPortalAlumno(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
                List<DocumentoSeccionPwFiltroAgrupadoPortalDTO> agrupado = new List<DocumentoSeccionPwFiltroAgrupadoPortalDTO>();
                var listaDocumentoSeccion = _repDocumentoSeccionPw.ObtenerDocumentoSeccionPorIdAlumnoPortal(IdAlumno);
                
                agrupado = listaDocumentoSeccion.GroupBy(u => (u.IdDocumentoPW, u.IdSeccionPW, u.IdSeccionTipoContenido))
                                    .Select(group =>
                                    new DocumentoSeccionPwFiltroAgrupadoPortalDTO
                                    {
                                        Id = group.Select(x => x.Id).FirstOrDefault()
                                        ,
                                        Titulo = group.Select(x => x.Titulo).FirstOrDefault()
                                        ,
                                        VisibleWeb = group.Select(x => x.VisibleWeb).FirstOrDefault()
                                        ,
                                        ZonaWeb = group.Select(x => x.ZonaWeb).FirstOrDefault()
                                        ,
                                        OrdenEeb = group.Select(x => x.OrdenEeb).FirstOrDefault()
                                        ,
                                        Contenido = group.Select(x => x.Contenido).FirstOrDefault()
                                        ,
                                        IdPlantillaPW = group.Select(x => x.IdPlantillaPW).FirstOrDefault()
                                        ,
                                        Posicion = group.Select(x => x.Posicion).FirstOrDefault()
                                        ,
                                        Tipo = group.Select(x => x.Tipo).FirstOrDefault()
                                        ,
                                        IdDocumentoPW = group.Key.IdDocumentoPW
                                        ,
                                        IdSeccionPW = group.Key.IdSeccionPW
                                        ,
                                        IdSeccionTipoContenido = group.Key.IdSeccionTipoContenido
                                        ,
                                        Cabecera = group.Select(x => x.Cabecera).FirstOrDefault()
                                        ,
                                        PiePagina = group.Select(x => x.PiePagina).FirstOrDefault()
                                        ,
                                        IdAlumno = group.Select(x => x.IdAlumno).FirstOrDefault()
                                        ,
                                        IdMatriculaCabecera = group.Select(x => x.IdMatriculaCabecera).FirstOrDefault()
                                        ,
                                        IdPEspecifico = group.Select(x => x.IdPEspecifico).FirstOrDefault()
                                        ,
                                        IdPlantillaMaestroPw = group.Select(x => x.IdPlantillaMaestroPw).FirstOrDefault()
                                        ,
                                        Version = group.Select(x => x.Version).FirstOrDefault()
                                        ,
                                        ListaSubSeccionesPw = group.Select(x => new SubSeccionTipoDetallePwDTO { IdSeccionTipoDetallePw = x.IdSeccionTipoDetallePw, NombreSubSeccion = x.NombreSubSeccion, IdSubSeccionTipoContenido = x.IdSubSeccionTipoContenido, ContenidoSubSeccion = x.ContenidoSubSeccion, NumeroFila = x.NumeroFila }).ToList()
                                    }).ToList();

                foreach (var item in agrupado)
                {
                    List<SubSeccionTipoDetallePwDTO> eliminar = new List<SubSeccionTipoDetallePwDTO>();
                    foreach (var itemInterno in item.ListaSubSeccionesPw)
                    {
                        if (itemInterno.IdSeccionTipoDetallePw == null)
                        {
                            eliminar.Add(itemInterno);
                        }
                    }
                    foreach (var itemEliminar in eliminar)
                    {
                        item.ListaSubSeccionesPw.Remove(itemEliminar);
                    }
                }

                return Ok(agrupado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{IdAlumno}")]
        [HttpPost]
        public ActionResult ObtenerSeccionProyectoAplicacionPorIdAlumno(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SeccionPwRepositorio _repSeccionPw = new SeccionPwRepositorio();
                return Ok(_repSeccionPw.ObtenerSeccionesProyectoAplicacionPorIdAlumno(IdAlumno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


    public class ValidadoDocumentoPwDTO : AbstractValidator<TDocumentoPw>
    {
        public static ValidadoDocumentoPwDTO Current = new ValidadoDocumentoPwDTO();
        public ValidadoDocumentoPwDTO()
        {
        }
    }
}
