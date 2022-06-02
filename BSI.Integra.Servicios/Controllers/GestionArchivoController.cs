using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Marketing.SCode.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.SCode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/GestionArchivo")]
    public class GestionArchivoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        private readonly RegistroArchivoStorageRepositorio _repRegistroArchivoStorage;
        private readonly UrlSubContenedorRepositorio _repUrlSubContenedor;
        private readonly UrlBlockStorageRepositorio _repUrlBlockStorage;
        private readonly UrlContenedorPermisoRepositorio _repUrlContenedorPermiso;

        public GestionArchivoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repRegistroArchivoStorage = new RegistroArchivoStorageRepositorio(integraDBContext);
            _repUrlSubContenedor = new UrlSubContenedorRepositorio(integraDBContext);
            _repUrlBlockStorage = new UrlBlockStorageRepositorio(integraDBContext);
            _repUrlContenedorPermiso = new UrlContenedorPermisoRepositorio(integraDBContext);
        }

        [Route("[Action]/{IdPersonal}/{IdUrlBlockStorage}/{NombreArchivo}")]
        [HttpGet]
        public ActionResult ObtenerTodoPorPermisos(int IdPersonal, int IdUrlBlockStorage, string NombreArchivo)
        {
            try
            {
                return Ok(_repRegistroArchivoStorage.ObtenerTodoPorPermisos(new RegistroArchivoMostrarFiltroDTO { IdPersonal = IdPersonal, IdUrlBlockStorage = IdUrlBlockStorage, NombreArchivo = NombreArchivo }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerArchivoDetalle(int Id)
        {
            try
            {
                var ArchivoDetalle = _repRegistroArchivoStorage.FirstBy(w => w.Estado == true && w.Id == Id, s => new { Id = s.Id, NombreArchivo = s.NombreArchivo, UsuarioModificacion = s.UsuarioModificacion, FechaModificacion = s.FechaModificacion, Ruta = s.Ruta });

                return Ok(ArchivoDetalle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] RegistroArchivoStorage_RegistrarDTO NuevoRegistro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                /*25: Subcontenedor Mailing*/
                string nombreArchivo = NuevoRegistro.IdUrlSubContenedor != 25 ? NuevoRegistro.NombreArchivo : string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), '-', NuevoRegistro.NombreArchivo);

                var RegistroArchivoStorageNuevo = new RegistroArchivoStorageBO()
                {
                    IdUrlSubContenedor = NuevoRegistro.IdUrlSubContenedor,
                    NombreArchivo = nombreArchivo,
                    Ruta = string.Concat(NuevoRegistro.Ruta, nombreArchivo).Replace(" ", "%20"),

                    Estado = true,
                    UsuarioCreacion = NuevoRegistro.NombreUsuario,
                    UsuarioModificacion = NuevoRegistro.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                if (!RegistroArchivoStorageNuevo.HasErrors)
                {
                    _repRegistroArchivoStorage.Insert(RegistroArchivoStorageNuevo);
                }
                else
                {
                    return BadRequest(RegistroArchivoStorageNuevo.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int IdPersonal)
        {
            try
            {
                var listadoContenedores = _repRegistroArchivoStorage.ObtenerContenedores(IdPersonal);
                var listadoSubContenedores = _repRegistroArchivoStorage.ObtenerSubcontenedores(IdPersonal);
                var listadoTipoSubContenedores = _repRegistroArchivoStorage.ObtenerTipoSubcontenedores(IdPersonal);

                return Ok(new
                {
                    ListadoContenedores = listadoContenedores,
                    ListadoSubContenedores = listadoSubContenedores,
                    ListadoTipoSubContenedores = listadoTipoSubContenedores
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirArchivo([FromForm] IList<IFormFile> Archivos, [FromForm] int IdUrlSubContenedor, [FromForm] string NombreArchivo, [FromForm] string NombreUsuario, [FromForm] string RutaCompleta, [FromForm] string RutaBlob, [FromForm] IList<IFormFile> ArchivoBol, [FromForm] IList<IFormFile> ArchivoCol, [FromForm] IList<IFormFile> ArchivoInt, [FromForm] IList<IFormFile> ArchivoPeLima, [FromForm] IList<IFormFile> ArchivoPeAqp)
        {
            try
            {
                var archivosOperaciones = new Dictionary<string, IList<IFormFile>> { {"-b.", ArchivoBol}, { "-c.", ArchivoCol }, { "-i.", ArchivoInt }, { "-pl.", ArchivoPeLima }, { "-pa.", ArchivoPeAqp } };
                var contenedorSubcontenedor = _repUrlBlockStorage.ObtenerInformacionPorIdUrlSubcontenedor(IdUrlSubContenedor);

                GestionArchivoBO gestionArchivoBo = new GestionArchivoBO();
                CloudflareBO cloudflareBo = new CloudflareBO();
                string varUrl = string.Empty;

                foreach (var archivo in Archivos)
                {
                    NombreArchivo = IdUrlSubContenedor == 25/*Firma Mailing*/ ? string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), '-', NombreArchivo) : NombreArchivo;
                    
                    if (IdUrlSubContenedor == 24 || IdUrlSubContenedor==48 || IdUrlSubContenedor==49)/*Firma Operaciones*/
                        varUrl = gestionArchivoBo.SubirArchivo(archivo.ConvertToByte(), archivo.ContentType, NombreArchivo, RutaCompleta.Replace("V4/", string.Empty), RutaBlob.Replace("V4/", string.Empty));
                    else
                    {
                        varUrl = gestionArchivoBo.SubirArchivo(archivo.ConvertToByte(), archivo.ContentType, NombreArchivo, RutaCompleta, RutaBlob);
                        if (contenedorSubcontenedor.IdProveedorNube == 2)/*Subcontenedores que se guardan en el portal*/
                        {
                            try
                            {
                                WebClient wc = new WebClient();
                                var url = "https://bsginstitute.com/GestionArchivo/AlmacenarArchivo?urlArchivo=" + varUrl + "&rutaBlob=" + RutaBlob + "&subdominio=" + contenedorSubcontenedor.Subdominio + "&nombreArchivo=" + NombreArchivo;

                                string urlimg = wc.DownloadString(url);

                                if (!string.IsNullOrEmpty(urlimg))
                                {
                                    //LlamadaRegularizacionDTO al insert
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        var listadoRegistroExistente = _repRegistroArchivoStorage.GetBy(w => w.Ruta == urlimg);
                                        foreach (var registroArchivo in listadoRegistroExistente)
                                        {
                                            _repRegistroArchivoStorage.Delete(registroArchivo.Id, NombreUsuario);
                                        }
                                        scope.Complete();
                                    }

                                    RegistroArchivoStorageBO nuevoBOImg = new RegistroArchivoStorageBO()
                                    {
                                        IdUrlSubContenedor = IdUrlSubContenedor,
                                        NombreArchivo = NombreArchivo,
                                        Ruta = urlimg,
                                        Estado = true,
                                        UsuarioCreacion = NombreUsuario,
                                        UsuarioModificacion = NombreUsuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now
                                    };

                                    _repRegistroArchivoStorage.Insert(nuevoBOImg);

                                    cloudflareBo.LimpiarCacheBsgInstitute();
                                    return Ok(urlimg);
                                }
                                else
                                {
                                    return BadRequest("No hubo respuesta");
                                }
                            }
                            catch (Exception ex)
                            {
                                return BadRequest(ex.Message);
                            }
                        }
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        var listadoRegistroExistente = _repRegistroArchivoStorage.GetBy(w => w.Ruta == varUrl);
                        
                        foreach (var registroArchivo in listadoRegistroExistente)
                            _repRegistroArchivoStorage.Delete(registroArchivo.Id, NombreUsuario);

                        scope.Complete();
                    }

                    RegistroArchivoStorageBO nuevoBO = new RegistroArchivoStorageBO()
                    {
                        IdUrlSubContenedor = IdUrlSubContenedor == 24/*Firma Operaciones*/ ? 23/*Firma Simple*/ : IdUrlSubContenedor,
                        NombreArchivo = NombreArchivo,
                        Ruta = varUrl,
                        Estado = true,
                        UsuarioCreacion = NombreUsuario,
                        UsuarioModificacion = NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    _repRegistroArchivoStorage.Insert(nuevoBO);
                }

                if (contenedorSubcontenedor.AplicaSubidaMultiple)/*Firma Operaciones*/
                {
                    foreach(var archivo in archivosOperaciones)
                    {
                        if(archivo.Value != null)
                        {
                            foreach(var elemento in archivo.Value)
                            {
                                string nombreArchivoRuta = NombreArchivo.Replace(".", archivo.Key);
                                varUrl = gestionArchivoBo.SubirArchivo(elemento.ConvertToByte(), elemento.ContentType, nombreArchivoRuta, RutaCompleta, RutaBlob);

                                using (TransactionScope scope = new TransactionScope())
                                {
                                    var listadoRegistroExistente = _repRegistroArchivoStorage.GetBy(w => w.Ruta == varUrl);
                                    
                                    foreach (var registroArchivo in listadoRegistroExistente)
                                        _repRegistroArchivoStorage.Delete(registroArchivo.Id, NombreUsuario);

                                    scope.Complete();
                                }

                                RegistroArchivoStorageBO nuevoBO = new RegistroArchivoStorageBO()
                                {
                                    IdUrlSubContenedor = IdUrlSubContenedor,
                                    NombreArchivo = nombreArchivoRuta,
                                    Ruta = varUrl,
                                    Estado = true,
                                    UsuarioCreacion = NombreUsuario,
                                    UsuarioModificacion = NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };

                                _repRegistroArchivoStorage.Insert(nuevoBO);
                            }
                        }
                    }
                }

                cloudflareBo.LimpiarCacheBsgInstitute();

                return Ok(varUrl);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdSubContenedor}")]
        [HttpGet]
        public ActionResult ObtenerRutaSubContenedor(int IdSubContenedor)
        {
            try
            {
                List<UrlSubContenedorDTO> listado = _repUrlSubContenedor.ObtenerRutaSubContenedor(IdSubContenedor);

                if (listado == null || listado.Count == 0)
                {
                    listado = _repUrlSubContenedor.ObtenerRutaSubContenedor(IdSubContenedor);
                    listado.ForEach(f =>
                    {
                        f.Id = IdSubContenedor;
                    });
                }

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
