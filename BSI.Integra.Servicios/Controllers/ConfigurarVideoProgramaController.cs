using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/ConfigurarVideoPrograma
    /// Autor: Priscila Pacsi - Jorge Rivera - Gian Miranda
    /// Fecha: 26/02/2021
    /// <summary>
    /// Gestiona las acciones para la configuracion de los videos por programa del portal web
    /// </summary>
    [Route("api/ConfigurarVideoPrograma")]
    public class ConfigurarVideoProgramaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private ConfigurarVideoProgramaBO ConfigurarVideoPrograma;
        private SesionConfigurarVideoBO SesionConfigurarVideo;
        private ConfigurarVideoProgramaRepositorio _repConfigurarVideoPrograma;
        private SesionConfigurarVideoRepositorio _repSesionConfigurarVideo;

        public ConfigurarVideoProgramaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repConfigurarVideoPrograma = new ConfigurarVideoProgramaRepositorio(integraDBContext);
            _repSesionConfigurarVideo = new SesionConfigurarVideoRepositorio(integraDBContext);
            ConfigurarVideoPrograma = new ConfigurarVideoProgramaBO(integraDBContext);
            SesionConfigurarVideo = new SesionConfigurarVideoBO(integraDBContext);
        }

        // FiltrosSesionCapitulo
        /// Tipo Función: GET
        /// Autor: Priscila Pacsi - Jorge Rivera
        /// Fecha: 21/02/2021
        /// Versión: 1
        /// <summary>
        /// Obtiene los combos necesarios para el funcionamiento del modulo
        /// </summary>
        /// <returns>Objeto anonimo { Capitulo (Lista de objeto de clase PreEstructuraCapituloProgramaBO), Sesion (Lista de objeto de clase PreEstructuraCapituloProgramaBO) }</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCombosFiltroProgramaVideo()
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _repfiltros = new ConfigurarVideoProgramaRepositorio();
                var combosProgramaGeneral = new
                {
                    Capitulo = _repfiltros.ObtenerCapituloProgramaFiltro(),
                    Sesion = _repfiltros.ObtenerSesionProgramaFiltro(),
                };

                return Ok(combosProgramaGeneral);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionVideoPrograma(int IdPGeneral)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                var respuesta = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoPrograma(IdPGeneral);
                var _listadoEstructura = (from x in respuesta
                                          group x by x.NumeroFila into newGroup
                                          select newGroup).ToList();
                List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();
                foreach (var item in _listadoEstructura)
                {
                    EstructuraCapituloProgramaBO objeto = new EstructuraCapituloProgramaBO();
                    objeto.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                objeto.Nombre = itemRegistros.Nombre;
                                objeto.Capitulo = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                break;
                            case "Sesion":
                                objeto.Sesion = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                break;
                            case "SubSeccion":
                                objeto.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(objeto.SubSesion))
                                {
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                }
                                break;
                            default:
                                objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    lista.Add(objeto);
                }

                var rpta = lista.OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionExamenPrograma(int IdPGeneral)
        {
            try
            {
                string _auxCapitulo = string.Empty; ;
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                var respuesta = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoPrograma(IdPGeneral);
                var _listadoEstructura = (from x in respuesta
                                          group x by x.NumeroFila into newGroup
                                          select newGroup).ToList();
                List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();
                foreach (var item in _listadoEstructura)
                {
                    EstructuraCapituloProgramaBO objeto = new EstructuraCapituloProgramaBO();
                    objeto.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                objeto.Nombre = itemRegistros.Nombre;
                                objeto.Capitulo = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                break;
                            case "Sesion":
                            case "SubSeccion":
                                break;
                            default:
                                objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    if (_auxCapitulo != objeto.Capitulo)
                    {
                        _auxCapitulo = objeto.Capitulo;
                        lista.Add(objeto);
                    }

                }

                var rpta = lista.OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistrarConfiguracionVideoPrograma([FromBody] DatosConfigurarVideoProgramaDTO objeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(_integraDBContext);
                SesionConfigurarVideoRepositorio _configurarSesionVideoRepositorio = new SesionConfigurarVideoRepositorio(_integraDBContext);

                ConfigurarVideoProgramaBO configurarVideoBO;

                if (objeto.ConfigurarVideoPrograma.Id != 0)
                {
                    configurarVideoBO = _configurarVideoProgramaRepositorio.FirstById(objeto.ConfigurarVideoPrograma.Id);
                    configurarVideoBO.IdPgeneral = objeto.ConfigurarVideoPrograma.IdPGeneral;
                    configurarVideoBO.IdDocumentoSeccionPw = objeto.ConfigurarVideoPrograma.IdDocumentoSeccionPw;
                    configurarVideoBO.VideoId = objeto.ConfigurarVideoPrograma.VideoId;
                    configurarVideoBO.VideoIdBrightcove = objeto.ConfigurarVideoPrograma.VideoIdBrightcove;
                    configurarVideoBO.TotalMinutos = objeto.ConfigurarVideoPrograma.TotalMinutos;
                    configurarVideoBO.Archivo = objeto.ConfigurarVideoPrograma.Archivo;
                    configurarVideoBO.NroDiapositivas = objeto.ConfigurarVideoPrograma.NroDiapositivas;
                    configurarVideoBO.Configurado = objeto.ConfigurarVideoPrograma.Configurado;
                    configurarVideoBO.ConImagenVideo = objeto.ConfigurarVideoPrograma.ConImagenVideo;
                    configurarVideoBO.ImagenVideoNombre = objeto.ConfigurarVideoPrograma.ImagenVideoNombre;
                    configurarVideoBO.ImagenVideoAlto = objeto.ConfigurarVideoPrograma.ImagenVideoAlto;
                    configurarVideoBO.ImagenVideoAncho = objeto.ConfigurarVideoPrograma.ImagenVideoAncho;
                    configurarVideoBO.ImagenVideoPosicionX = objeto.ConfigurarVideoPrograma.ImagenVideoPosicionX;
                    configurarVideoBO.ImagenVideoPosicionY = objeto.ConfigurarVideoPrograma.ImagenVideoPosicionY;

                    configurarVideoBO.ConImagenDiapositiva = objeto.ConfigurarVideoPrograma.ConImagenDiapositiva;
                    configurarVideoBO.ImagenDiapositivaNombre = objeto.ConfigurarVideoPrograma.ImagenDiapositivaNombre;
                    configurarVideoBO.ImagenDiapositivaAlto = objeto.ConfigurarVideoPrograma.ImagenDiapositivaAlto;
                    configurarVideoBO.ImagenDiapositivaAncho = objeto.ConfigurarVideoPrograma.ImagenDiapositivaAncho;
                    configurarVideoBO.ImagenDiapositivaPosicionX = objeto.ConfigurarVideoPrograma.ImagenDiapositivaPosicionX;
                    configurarVideoBO.ImagenDiapositivaPosicionY = objeto.ConfigurarVideoPrograma.ImagenDiapositivaPosicionY;

                    configurarVideoBO.NumeroFila = objeto.ConfigurarVideoPrograma.NumeroFila;
                    configurarVideoBO.FechaModificacion = DateTime.Now;
                    configurarVideoBO.UsuarioModificacion = objeto.Usuario;
                }
                else
                {
                    configurarVideoBO = new ConfigurarVideoProgramaBO();
                    configurarVideoBO.IdPgeneral = objeto.ConfigurarVideoPrograma.IdPGeneral;
                    configurarVideoBO.IdDocumentoSeccionPw = objeto.ConfigurarVideoPrograma.IdDocumentoSeccionPw;
                    configurarVideoBO.VideoId = objeto.ConfigurarVideoPrograma.VideoId;
                    configurarVideoBO.TotalMinutos = objeto.ConfigurarVideoPrograma.TotalMinutos;
                    configurarVideoBO.Archivo = objeto.ConfigurarVideoPrograma.Archivo;
                    configurarVideoBO.NroDiapositivas = objeto.ConfigurarVideoPrograma.NroDiapositivas;
                    configurarVideoBO.Configurado = objeto.ConfigurarVideoPrograma.Configurado;
                    configurarVideoBO.ConImagenVideo = objeto.ConfigurarVideoPrograma.ConImagenVideo;
                    configurarVideoBO.ImagenVideoNombre = objeto.ConfigurarVideoPrograma.ImagenVideoNombre;
                    configurarVideoBO.ImagenVideoAlto = objeto.ConfigurarVideoPrograma.ImagenVideoAlto;
                    configurarVideoBO.ImagenVideoAncho = objeto.ConfigurarVideoPrograma.ImagenVideoAncho;
                    configurarVideoBO.ImagenVideoPosicionX = objeto.ConfigurarVideoPrograma.ImagenVideoPosicionX;
                    configurarVideoBO.ImagenVideoPosicionY = objeto.ConfigurarVideoPrograma.ImagenVideoPosicionY;

                    configurarVideoBO.ConImagenDiapositiva = objeto.ConfigurarVideoPrograma.ConImagenDiapositiva;
                    configurarVideoBO.ImagenDiapositivaNombre = objeto.ConfigurarVideoPrograma.ImagenDiapositivaNombre;
                    configurarVideoBO.ImagenDiapositivaAlto = objeto.ConfigurarVideoPrograma.ImagenDiapositivaAlto;
                    configurarVideoBO.ImagenDiapositivaAncho = objeto.ConfigurarVideoPrograma.ImagenDiapositivaAncho;
                    configurarVideoBO.ImagenDiapositivaPosicionX = objeto.ConfigurarVideoPrograma.ImagenDiapositivaPosicionX;
                    configurarVideoBO.ImagenDiapositivaPosicionY = objeto.ConfigurarVideoPrograma.ImagenDiapositivaPosicionY;

                    configurarVideoBO.NumeroFila = objeto.ConfigurarVideoPrograma.NumeroFila;
                    configurarVideoBO.Estado = true;
                    configurarVideoBO.FechaCreacion = DateTime.Now;
                    configurarVideoBO.FechaModificacion = DateTime.Now;
                    configurarVideoBO.UsuarioCreacion = objeto.Usuario;
                    configurarVideoBO.UsuarioModificacion = objeto.Usuario;
                }

                _configurarVideoProgramaRepositorio.Update(configurarVideoBO);

                foreach (var item in objeto.SesionesConfiguradasPrograma)
                {
                    SesionConfigurarVideoBO sesionConfigurar;
                    if (item.Id != 0)
                    {
                        sesionConfigurar = _configurarSesionVideoRepositorio.FirstById(item.Id);
                        sesionConfigurar.IdConfigurarVideoPrograma = configurarVideoBO.Id;
                        sesionConfigurar.Minuto = item.Minuto;
                        sesionConfigurar.IdTipoVista = item.IdTipoVista;
                        sesionConfigurar.NroDiapositiva = item.NroDiapositiva;
                        sesionConfigurar.IdEvaluacion = item.IdEvaluacion;
                        sesionConfigurar.FechaModificacion = DateTime.Now;
                        sesionConfigurar.UsuarioModificacion = objeto.Usuario;
                        sesionConfigurar.ConLogoVideo = item.ConLogoVideo;
                        sesionConfigurar.ConLogoDiapositiva = item.ConLogoDiapositiva;
                    }
                    else
                    {
                        sesionConfigurar = new SesionConfigurarVideoBO();
                        sesionConfigurar.IdConfigurarVideoPrograma = configurarVideoBO.Id;
                        sesionConfigurar.Minuto = item.Minuto;
                        sesionConfigurar.IdTipoVista = item.IdTipoVista;
                        sesionConfigurar.NroDiapositiva = item.NroDiapositiva;
                        sesionConfigurar.IdEvaluacion = item.IdEvaluacion;
                        sesionConfigurar.Estado = true;
                        sesionConfigurar.FechaCreacion = DateTime.Now;
                        sesionConfigurar.FechaModificacion = DateTime.Now;
                        sesionConfigurar.UsuarioCreacion = objeto.Usuario;
                        sesionConfigurar.UsuarioModificacion = objeto.Usuario;
                        sesionConfigurar.ConLogoVideo = item.ConLogoVideo;
                        sesionConfigurar.ConLogoDiapositiva = item.ConLogoDiapositiva;
                    }

                    _configurarSesionVideoRepositorio.Update(sesionConfigurar);
                }

                if (objeto.SesionesConfiguradasEliminadas != null)
                {
                    foreach (var itemEliminar in objeto.SesionesConfiguradasEliminadas)
                    {

                        if (_configurarSesionVideoRepositorio.Exist(itemEliminar.Id))
                        {
                            _configurarSesionVideoRepositorio.Delete(itemEliminar.Id, objeto.Usuario);
                        }
                    }
                    //var listaEliminacion = objeto.SesionesConfiguradasEliminadas.Select(x => x.Id);
                    //_configurarSesionVideoRepositorio.Delete(listaEliminacion, objeto.Usuario);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(true);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Rivera - Priscila Pacsi - Gian Miranda
        /// Fecha: 26/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion de sesiones y programas
        /// </summary>
        /// <param name="IdPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="IdDocumentoSeccionPw">Id de la seccion del documento (PK de la tabla pla.T_DocumentoSeccion_PW)</param>
        /// <param name="NumeroFila">Numero de fila de la configuracion del programa general</param>
        /// <returns>Response 200 (objeto de clase RegistroVideoProgramaBO) o 200 sin objeto</returns>
        [Route("[Action]/{IdPGeneral}/{IdDocumentoSeccionPw}/{NumeroFila}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionSesionPrograma(int IdPGeneral, int IdDocumentoSeccionPw, int NumeroFila)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                SesionConfigurarVideoRepositorio _configurarSesionVideoRepositorio = new SesionConfigurarVideoRepositorio();

                var configuracionVideoPrograma = _configurarVideoProgramaRepositorio.ObtenerConfigurarVideoPrograma(IdPGeneral, IdDocumentoSeccionPw, NumeroFila);

                if (configuracionVideoPrograma != null)
                {
                    var registros = _configurarSesionVideoRepositorio.ObtenerConfigurarSesionVideoPrograma(configuracionVideoPrograma.Id);
                    configuracionVideoPrograma.RegistroSesionConfigurar = registros;
                }

                return Ok(configuracionVideoPrograma);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }


        [Route("[action]/{IdPGeneral}")]
        [HttpPost]
        public ActionResult ConultarProgramaPadrePorIdPGeneral(int IdPGeneral)
        {
            try
            {
                bool esPadre = false;

                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                List<ListaCursosPorProgramaBO> ListaCursos = new List<ListaCursosPorProgramaBO>();

                var rpta = _configurarVideoProgramaRepositorio.ConultarProgramaPadrePorIdPGeneral(IdPGeneral);

                if (rpta != null)
                {
                    if (rpta.NroHijos > 0)
                    {
                        ListaCursos = _configurarVideoProgramaRepositorio.ListaCursosPorProgramaId(IdPGeneral);
                        esPadre = true;
                    }
                    else
                    {
                        ListaCursos = _configurarVideoProgramaRepositorio.RegistroCursoPorProgramaId(IdPGeneral);
                        esPadre = false;
                    }
                }

                if (ListaCursos != null)
                {
                    foreach (var itemLista in ListaCursos)
                    {
                        var respuesta = _configurarVideoProgramaRepositorio.ListaCapitulosEstructuraPrograma(itemLista.IdHijo);
                        if (respuesta != null)
                        {
                            itemLista.EstructuraCapitulos = respuesta;
                        }
                    }

                }


                return Ok(ListaCursos);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [Route("[action]/{IdPGeneral}")]
        [HttpPost]
        public ActionResult ConultarProgramaPadrePorIdPGeneralCapitulos(int IdPGeneral)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();

                var respuesta = _configurarVideoProgramaRepositorio.ListaCapitulosEstructuraPrograma(IdPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [Route("[action]/{IdPGeneral}")]
        [HttpPost]
        public ActionResult ObtenerProgramaPadrePorIdPGeneral(int IdPGeneral)
        {
            try
            {
                bool esPadre = false;

                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                List<ListaCursosPorProgramaBO> ListaCursos = new List<ListaCursosPorProgramaBO>();

                var rpta = _configurarVideoProgramaRepositorio.ConultarProgramaPadrePorIdPGeneral(IdPGeneral);

                if (rpta != null)
                {
                    if (rpta.NroHijos > 0)
                    {
                        ListaCursos = _configurarVideoProgramaRepositorio.ListaCursosPorProgramaId(IdPGeneral);
                        esPadre = true;
                    }
                    else
                    {
                        ListaCursos = _configurarVideoProgramaRepositorio.RegistroCursoPorProgramaId(IdPGeneral);
                        esPadre = false;
                    }
                }

                return Ok(ListaCursos);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [Route("[action]/{IdPGeneral}/{IdCapitulo}")]
        [HttpPost]
        public ActionResult ObtenerConfiguracionCronoPrograma(int IdPGeneral, int IdCapitulo) // validar uso
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                var respuesta = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoPrograma(IdPGeneral);
                var _listadoEstructura = (from x in respuesta
                                          group x by x.NumeroFila into newGroup
                                          select newGroup).ToList();


                List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();



                foreach (var item in _listadoEstructura)
                {
                    EstructuraCapituloProgramaBO objeto = new EstructuraCapituloProgramaBO();
                    objeto.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                objeto.Nombre = itemRegistros.Nombre;
                                objeto.Capitulo = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                break;
                            case "Sesion":
                                objeto.Sesion = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                break;
                            case "SubSeccion":
                                objeto.SubSesion = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                if (!string.IsNullOrEmpty(objeto.SubSesion))
                                {
                                    objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                }
                                break;
                            default:
                                objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    lista.Add(objeto);
                }

                var rpta = lista.OrderBy(x => x.OrdenFila).ToList();

                var _listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo });

                List<CapitulosSesionesProgramaBO> _listaRegistro = new List<CapitulosSesionesProgramaBO>();

                foreach (var capitulo in _listas)
                {
                    CapitulosSesionesProgramaBO _registro = new CapitulosSesionesProgramaBO();
                    _registro.IdPgeneral = capitulo.Key.IdPgeneral;
                    _registro.Nombre = capitulo.Key.Nombre;
                    _registro.Capitulo = capitulo.Key.Capitulo;

                    _registro.ListaSesiones = new List<EstructuraCapituloProgramaBO>();

                    foreach (var sesiones in capitulo)
                    {
                        EstructuraCapituloProgramaBO _sesion = new EstructuraCapituloProgramaBO();
                        _sesion.Sesion = sesiones.Sesion;
                        _sesion.OrdenFila = sesiones.OrdenFila;
                        _sesion.OrdenCapitulo = sesiones.OrdenCapitulo;
                        _sesion.OrdenSeccion = sesiones.OrdenSeccion;

                        _registro.ListaSesiones.Add(_sesion);
                    }
                    _listaRegistro.Add(_registro);
                }

                return Ok(_listaRegistro);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{IdPGeneral}")]
        [HttpPost]
        public ActionResult ObtenerConfiguracionCapituloSesionPrograma(int IdPGeneral)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                var respuesta = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoPrograma(IdPGeneral);
                var _listadoEstructura = (from x in respuesta
                                          group x by x.NumeroFila into newGroup
                                          select newGroup).ToList();


                List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();



                foreach (var item in _listadoEstructura)
                {
                    EstructuraCapituloProgramaBO objeto = new EstructuraCapituloProgramaBO();
                    objeto.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                objeto.Nombre = itemRegistros.Nombre;
                                objeto.Capitulo = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                break;
                            case "Sesion":
                                objeto.Sesion = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                break;
                            case "SubSeccion":
                                objeto.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(objeto.SubSesion))
                                {
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                }
                                break;
                            default:
                                objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                objeto.TotalSegundos = itemRegistros.TotalSegundos;
                                break;
                        }
                    }
                    lista.Add(objeto);
                }

                var rpta = lista.OrderBy(x => x.OrdenFila).ToList();

                var _listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo, x.OrdenCapitulo });

                List<CapitulosSesionesProgramaBO> _listaRegistro = new List<CapitulosSesionesProgramaBO>();

                foreach (var capitulo in _listas)
                {
                    CapitulosSesionesProgramaBO _registro = new CapitulosSesionesProgramaBO();
                    _registro.IdPgeneral = capitulo.Key.IdPgeneral;
                    _registro.Nombre = capitulo.Key.Nombre;
                    _registro.Capitulo = capitulo.Key.Capitulo;
                    _registro.OrdenFila = capitulo.Key.OrdenCapitulo;

                    _registro.ListaSesiones = new List<EstructuraCapituloProgramaBO>();

                    foreach (var sesiones in capitulo)
                    {
                        EstructuraCapituloProgramaBO _sesion = new EstructuraCapituloProgramaBO();
                        _sesion.Sesion = sesiones.Sesion;
                        _sesion.OrdenFila = sesiones.OrdenFila;
                        _sesion.OrdenCapitulo = sesiones.OrdenCapitulo;
                        _sesion.OrdenSeccion = sesiones.OrdenSeccion;
                        _sesion.TotalSegundos = sesiones.TotalSegundos;
                        _sesion.SubSesion = sesiones.SubSesion;

                        _registro.ListaSesiones.Add(_sesion);
                    }
                    _listaRegistro.Add(_registro);


                }

                var _respuestaPreFinal = _listaRegistro.OrderBy(x => x.Capitulo).ToList();

                foreach (var item in _respuestaPreFinal)
                {
                    var _rptaEvaluacion = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoProgramaEvaluaciones(item.IdPgeneral, item.OrdenFila);
                    var _rptaExamenes = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoProgramaEncuestas(item.IdPgeneral, item.OrdenFila);

                    if (_rptaEvaluacion != null && _rptaEvaluacion.Count > 0)
                    {
                        foreach (var itemEvaluacion in _rptaEvaluacion)
                        {
                            CapitulosSesionesProgramaBO _registroEvaluacion = new CapitulosSesionesProgramaBO();
                            _registroEvaluacion.Id = itemEvaluacion.Id;
                            _registroEvaluacion.IdDocumentoSeccionPw = itemEvaluacion.IdSeccionTipoDetalle_PW;
                            _registroEvaluacion.IdPgeneral = itemEvaluacion.IdPgeneral;
                            _registroEvaluacion.Nombre = itemEvaluacion.Nombre;
                            _registroEvaluacion.Capitulo = itemEvaluacion.Contenido;
                            _registroEvaluacion.OrdenFila = itemEvaluacion.NumeroFila;

                            _registroEvaluacion.ListaSesiones = new List<EstructuraCapituloProgramaBO>();
                            _listaRegistro.Add(_registroEvaluacion);
                        }

                    }

                    if (_rptaExamenes != null && _rptaExamenes.Count > 0)
                    {
                        foreach (var itemEvaluacion in _rptaExamenes)
                        {
                            CapitulosSesionesProgramaBO _registroEvaluacion = new CapitulosSesionesProgramaBO();
                            _registroEvaluacion.Id = itemEvaluacion.Id;
                            _registroEvaluacion.IdDocumentoSeccionPw = itemEvaluacion.IdSeccionTipoDetalle_PW;
                            _registroEvaluacion.IdPgeneral = itemEvaluacion.IdPgeneral;
                            _registroEvaluacion.Nombre = itemEvaluacion.Nombre;
                            _registroEvaluacion.Capitulo = itemEvaluacion.Contenido;
                            _registroEvaluacion.OrdenFila = itemEvaluacion.NumeroFila;

                            _registroEvaluacion.ListaSesiones = new List<EstructuraCapituloProgramaBO>();
                            _listaRegistro.Add(_registroEvaluacion);
                        }

                    }
                }

                return Ok(_listaRegistro.OrderBy(x => x.Capitulo).ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{IdPGeneral}/{Seccion}/{Fila}")]
        [HttpPost]
        public ActionResult obtenerConfiguracionVideoSesion(int IdPGeneral, int Seccion, int Fila)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();

                var respuesta = _configurarVideoProgramaRepositorio.obtenerConfiguracionVideoSesion(IdPGeneral, Seccion, Fila);

                configuracionVideoProgramaBO _registro = new configuracionVideoProgramaBO();
                _registro.NombCurso = respuesta.Nombre;
                _registro.IdVideo = respuesta.IdVideo;
                _registro.Archivo = respuesta.Archivo;
                _registro.Ubicacion = respuesta.Ubicacion;
                _registro.UltimaSesion = respuesta.UltimaSesion;

                _registro.Configuracion = new List<configuracionSecuenciaVideoBO>();

                var secuenciaRpt = _configurarVideoProgramaRepositorio.obtenerConfiguracionSecuenciaVideoSesion(IdPGeneral, Seccion, Fila);

                if (secuenciaRpt != null && secuenciaRpt.Count > 0)
                {
                    _registro.Configuracion = secuenciaRpt;
                }
                else
                {
                    var _sumaTiempo = 0;

                    for (int i = 1; i <= respuesta.Cantidad; i++)
                    {

                        configuracionSecuenciaVideoBO _item = new configuracionSecuenciaVideoBO();
                        _item.NroDiapositiva = i.ToString();
                        if (i == 1)
                        {
                            //_sumaTiempo += respuesta.intervalo;
                            _item.tiempo = 0;
                            _item.tipoVista = "3";
                            _item.Evaluacion = "0";
                            _item.UrlEvaluacion = "UrlArmada";
                        }
                        else
                        {
                            _sumaTiempo += respuesta.intervalo;
                            _item.tiempo = _sumaTiempo;
                            _item.tipoVista = "3";
                            _item.Evaluacion = "0";
                            _item.UrlEvaluacion = "UrlArmada";
                        }

                        _registro.Configuracion.Add(_item);
                    }
                }


                _registro.OverlayVideo = new List<configuracionSelloVideoBO>();

                configuracionSelloVideoBO _itemVideo = new configuracionSelloVideoBO();
                _itemVideo.n_imag = respuesta.ImagenVideoNombre;
                _itemVideo.n_coorx = "10";
                _itemVideo.n_coory = "10";
                _itemVideo.n_sizew = respuesta.ImagenVideoAncho.ToString();
                _itemVideo.n_sizeh = respuesta.ImagenVideoAlto.ToString();
                _registro.OverlayVideo.Add(_itemVideo);

                _registro.OverlaySlide = new List<configuracionSelloVideoBO>();

                configuracionSelloVideoBO _itemDiapo = new configuracionSelloVideoBO();
                _itemDiapo.n_imag = respuesta.ImagenDiapositivaNombre;
                _itemDiapo.n_coorx = "10";
                _itemDiapo.n_coory = "10";
                _itemDiapo.n_sizew = respuesta.ImagenDiapositivaAncho.ToString();
                _itemDiapo.n_sizeh = respuesta.ImagenDiapositivaAlto.ToString();
                _registro.OverlaySlide.Add(_itemDiapo);


                return Ok(_registro);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [Route("[action]/{IdPGeneral}/{Seccion}/{Fila}/{Capitulo}/{EvaluacionHabilitada?}")]
        [HttpPost]
        public ActionResult obtenerConfiguracionVideoSesionConPreguntas(int IdPGeneral, int Seccion, int Fila, int Capitulo, bool EvaluacionHabilitada = true)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();

                var respuesta = _configurarVideoProgramaRepositorio.obtenerConfiguracionVideoSesion(IdPGeneral, Seccion, Fila);

                configuracionVideoProgramaBO _registro = new configuracionVideoProgramaBO();
                _registro.NombCurso = respuesta.Nombre;
                _registro.IdVideo = respuesta.IdVideo;
                _registro.Archivo = respuesta.Archivo;
                _registro.Ubicacion = respuesta.Ubicacion;
                _registro.UltimaSesion = respuesta.UltimaSesion;
                _registro.IdVideoBrightcove = respuesta.IdVideoBrightcove;

                _registro.Configuracion = new List<configuracionSecuenciaVideoBO>();

                var secuenciaRpt = _configurarVideoProgramaRepositorio.obtenerConfiguracionSecuenciaVideoSesion(IdPGeneral, Seccion, Fila, Capitulo);

                if (secuenciaRpt != null && secuenciaRpt.Count > 0)
                {
                    foreach (var itemRegistro in secuenciaRpt)
                    {
                        if (itemRegistro.tipoVista == "4" && itemRegistro.Evaluacion == "1" && itemRegistro.NombEvaluacion == "Evaluacion")
                        {
                            int numeroDiapositivaSiguiente = Convert.ToInt32(itemRegistro.NroDiapositiva) + 1;
                            var _Siguiente = secuenciaRpt.Where(x => x.NroDiapositiva == numeroDiapositivaSiguiente.ToString()).Select(x => x).FirstOrDefault();
                            if (_Siguiente != null)
                            {
                                int _tiempo = Convert.ToInt32(_Siguiente.tiempo) - 1;
                                itemRegistro.tiempo = _tiempo;
                            }
                            else
                            {
                                int _tiempo = respuesta.Segundos - 1;
                                itemRegistro.tiempo = _tiempo;
                            }
                        }
                    }
                    var _resultadoProcesado = secuenciaRpt.OrderBy(x => x.tiempo).ToList();
                    foreach (var itemRegistro in _resultadoProcesado)
                    {
                        if (itemRegistro.tipoVista == "4" && itemRegistro.Evaluacion == "1" && itemRegistro.NombEvaluacion == "Evaluacion")
                        {
                            int _tiempo = itemRegistro.tiempo + 1;
                            itemRegistro.NroDiapositiva = "0";
                            itemRegistro.tiempo = _tiempo;
                            itemRegistro.Evaluacion = "0";
                        }
                        else if (itemRegistro.tipoVista == "4" && itemRegistro.NombEvaluacion == "Crucigrama")
                        {
                            itemRegistro.NroDiapositiva = "0";
                            itemRegistro.Evaluacion = "0";
                        }
                    }

                    if (!EvaluacionHabilitada)
                    {
                        _resultadoProcesado = _resultadoProcesado.Where(x => x.tipoVista != "4" && x.Evaluacion != "1" && x.NombEvaluacion != "Evaluacion" && x.NombEvaluacion != "Crucigrama").ToList();
                    }

                    _registro.Configuracion = _resultadoProcesado;
                }
                else
                {
                    var _sumaTiempo = 0;

                    for (int i = 1; i <= respuesta.Cantidad; i++)
                    {

                        configuracionSecuenciaVideoBO _item = new configuracionSecuenciaVideoBO();
                        _item.NroDiapositiva = i.ToString();
                        if (i == 1)
                        {
                            //_sumaTiempo += respuesta.intervalo;
                            _item.tiempo = 0;
                            _item.tipoVista = "3";
                            _item.Evaluacion = "0";
                            _item.UrlEvaluacion = "UrlArmada";
                        }
                        else
                        {
                            _sumaTiempo += respuesta.intervalo;
                            _item.tiempo = _sumaTiempo;
                            _item.tipoVista = "3";
                            _item.Evaluacion = "0";
                            _item.UrlEvaluacion = "UrlArmada";
                        }

                        _registro.Configuracion.Add(_item);
                    }
                }


                _registro.OverlayVideo = new List<configuracionSelloVideoBO>();

                configuracionSelloVideoBO _itemVideo = new configuracionSelloVideoBO();
                _itemVideo.n_imag = respuesta.ImagenVideoNombre;
                _itemVideo.n_coorx = respuesta.ImagenVideoPosicionX.ToString();
                _itemVideo.n_coory = respuesta.ImagenVideoPosicionY.ToString();
                _itemVideo.n_sizew = respuesta.ImagenVideoAncho.ToString();
                _itemVideo.n_sizeh = respuesta.ImagenVideoAlto.ToString();
                _registro.OverlayVideo.Add(_itemVideo);

                _registro.OverlaySlide = new List<configuracionSelloVideoBO>();

                configuracionSelloVideoBO _itemDiapo = new configuracionSelloVideoBO();
                _itemDiapo.n_imag = respuesta.ImagenDiapositivaNombre;
                _itemDiapo.n_coorx = respuesta.ImagenDiapositivaPosicionX.ToString();
                _itemDiapo.n_coory = respuesta.ImagenDiapositivaPosicionY.ToString();
                _itemDiapo.n_sizew = respuesta.ImagenDiapositivaAncho.ToString();
                _itemDiapo.n_sizeh = respuesta.ImagenDiapositivaAlto.ToString();
                _registro.OverlaySlide.Add(_itemDiapo);


                return Ok(_registro);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [Route("[Action]/{IdPrograma}")]
        [HttpPost]
        public ActionResult ObtenerPreguntasFrecuentes(int IdPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PreguntaFrecuentePgeneralRepositorio _repPreguntaFrecuentePgeneral = new PreguntaFrecuentePgeneralRepositorio(contexto);

                var repositorioPreguntaFrecuente = _repPreguntaFrecuentePgeneral.ObtenerPreguntaFrecuentePorPrograma(IdPrograma);
                return Ok(repositorioPreguntaFrecuente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPGeneral}/{Seccion}/{Fila}")]
        [HttpGet]
        public ActionResult obtenerConfiguracionPreguntasEstructura(int IdPGeneral, int Seccion, int Fila)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                var secuenciaRpt = _configurarVideoProgramaRepositorio.obtenerConfiguracionGrupoPreguntasEstructura(IdPGeneral, Seccion, Fila);

                return Ok(secuenciaRpt);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[action]/{IdPGeneral}/{IdSexo}/{Puntaje}")]
        [HttpPost]
        public ActionResult obtenerFeedbackResultadoObtenido(int IdPGeneral, int IdSexo, int Puntaje)
        {
            try
            {
                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
                var secuenciaRpt = _configurarVideoProgramaRepositorio.obtenerFeedbackResultadoObtenido(IdPGeneral, IdSexo, Puntaje);

                return Ok(secuenciaRpt);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[Action]/{IdPGeneral}/{IdModalidadCurso}")]
        [HttpPost]
        public ActionResult ObtenerCriterioEvaluacionProgramaAulaVirtual(int IdPGeneral, int IdModalidadCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(contexto);

                var repositorioPreguntaFrecuente = _configurarVideoProgramaRepositorio.obtenerConfiguracionSecuenciaVideoSesion(IdPGeneral, IdModalidadCurso);
                return Ok(repositorioPreguntaFrecuente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPGeneral}/{NombreEsquemaEvaluacionPGeneralDetalle}/{Valor}/{IdMatriculaCabecera}")]
        [HttpPost]
        public ActionResult obtenerParametroEvaluacionEscalaCalificacionDetalle(int IdPGeneral, string NombreEsquemaEvaluacionPGeneralDetalle, int Valor, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(contexto);

                var _matricula = _configurarVideoProgramaRepositorio.registroMatriculaCabeceraAlumno(IdMatriculaCabecera);

                var respuesta = _configurarVideoProgramaRepositorio.obtenerParametroEvaluacionEscalaCalificacionDetalle(IdPGeneral, NombreEsquemaEvaluacionPGeneralDetalle, Valor, _matricula.FechaMatricula.ToString("dd/MM/yyyy"));
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPGeneral}/{NombreEsquemaEvaluacionPGeneralDetalle}/{IdMatriculaCabecera}")]
        [HttpPost]
        public ActionResult obtenerParametroEvaluacionEscalaProgramaGeneral(int IdPGeneral, string NombreEsquemaEvaluacionPGeneralDetalle, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(contexto);

                var _matricula = _configurarVideoProgramaRepositorio.registroMatriculaCabeceraAlumno(IdMatriculaCabecera);

                var respuesta = _configurarVideoProgramaRepositorio.obtenerParametroEvaluacionEscalaProgramaGeneral(IdPGeneral, NombreEsquemaEvaluacionPGeneralDetalle, _matricula.FechaMatricula.ToString("dd/MM/yyyy"));
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPGeneral}/{NombreEsquemaEvaluacionPGeneralDetalle}/{IdMatriculaCabecera}")]
        [HttpPost]
        public ActionResult obtenerParametroEvaluacionEscalaProgramaGeneralConCalificacionDetalle(int IdPGeneral, string NombreEsquemaEvaluacionPGeneralDetalle, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(contexto);

                var _matricula = _configurarVideoProgramaRepositorio.registroMatriculaCabeceraAlumno(IdMatriculaCabecera);

                var respuesta = _configurarVideoProgramaRepositorio.obtenerregistroParametroEscalaEvaluacion(IdPGeneral, NombreEsquemaEvaluacionPGeneralDetalle, _matricula.FechaMatricula.ToString("dd/MM/yyyy"));

                if (respuesta != null)
                {
                    var detalle = _configurarVideoProgramaRepositorio.obtenerregistroParametroEscalaEvaluacionDetalle(respuesta.IdEscalaCalificacion);
                    respuesta.listaParametroEscalaEvaluacion = detalle;
                }

                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPGeneral}/{NombreEsquemaEvaluacionPGeneralDetalle}/{IdMatriculaCabecera}")]
        [HttpPost]
        public ActionResult listaParametroEvaluacionEscalaProgramaGeneralConCalificacionDetalle(int IdPGeneral, string NombreEsquemaEvaluacionPGeneralDetalle, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(contexto);

                var _matricula = _configurarVideoProgramaRepositorio.registroMatriculaCabeceraAlumno(IdMatriculaCabecera);

                var respuesta = _configurarVideoProgramaRepositorio.listaRegistroParametroEscalaEvaluacion(IdPGeneral, NombreEsquemaEvaluacionPGeneralDetalle, _matricula.FechaMatricula.ToString("dd/MM/yyyy"));

                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var itemCriterio in respuesta)
                    {
                        var detalle = _configurarVideoProgramaRepositorio.obtenerregistroParametroEscalaEvaluacionDetalle(itemCriterio.IdEscalaCalificacion);
                        itemCriterio.listaParametroEscalaEvaluacion = detalle;
                    }
                }

                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPGeneral}/{TipoPrograma}")]
        [HttpPost]
        public ActionResult generarNotasRankingPorPrograma(int IdPGeneral, int TipoPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();


                ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(contexto);

                EsquemaEvaluacionBO esquemaBO = new EsquemaEvaluacionBO(_integraDBContext);
                RegistroRankingProgramaRepositorio _configurarRanking = new RegistroRankingProgramaRepositorio(contexto);


                List<int> _listaProgramas = new List<int>();
                _listaProgramas.Add(587);
                _listaProgramas.Add(585);

                var _eliminacion = _configurarRanking.reiniciarTablaRankingAlumno();

                foreach (var itemPGeneral in _listaProgramas)
                {
                    List<registroRankingMatriculaNotaProgramaGeneralBO> _listaNotas = new List<registroRankingMatriculaNotaProgramaGeneralBO>();
                    List<registroRankingMatriculaNotaProgramaGeneralBO> _listaNotasFinal = new List<registroRankingMatriculaNotaProgramaGeneralBO>();

                    var _matriculas = _configurarVideoProgramaRepositorio.listaMatriculaPorProgramaGeneralRegistroActivo(itemPGeneral, TipoPrograma);

                    if (_matriculas != null && _matriculas.Count > 0)
                    {
                        foreach (var item in _matriculas)
                        {
                            bool bandera = true;

                            registroRankingMatriculaNotaProgramaGeneralBO _registro = new registroRankingMatriculaNotaProgramaGeneralBO();
                            _registro.IdMatriculaCabecera = item.IdMatriculaCabecera;
                            _registro.IdAlumno = item.IdAlumno;
                            _registro.IdPEspecifico = item.IdPEspecifico;
                            _registro.IdPGeneral = itemPGeneral;
                            _registro.Tipo = item.Tipo;
                            _registro.TipoId = item.TipoId;
                            _registro.CodigoMatricula = item.CodigoMatricula;

                            var _notaRegistro = _configurarVideoProgramaRepositorio.listaNotasPromedioPorMatriculaMatricula(item.IdMatriculaCabecera);

                            if (_notaRegistro == null)
                            {
                                try
                                {
                                    EsquemaEvaluacion_NotaCursoDTO detalleNota = esquemaBO.ObtenerDetalleCalificacionPorCurso(item.IdMatriculaCabecera, item.IdPEspecifico, 1);
                                    if (detalleNota != null)
                                    {
                                        _registro.EscalaCalificacion = 60;
                                        _registro.NotaFinal = Convert.ToInt32(detalleNota.NotaCurso);
                                        _registro.Promedio = Convert.ToInt32(detalleNota.NotaCurso);
                                    }
                                    else
                                    {
                                        _registro.EscalaCalificacion = 0;
                                        _registro.NotaFinal = 0;
                                        _registro.Promedio = 0;
                                    }
                                    bandera = true;
                                }
                                catch (Exception ex)
                                {
                                    bandera = false;
                                }

                            }
                            else
                            {
                                _registro.EscalaCalificacion = _notaRegistro.EscalaCalificacion;
                                _registro.NotaFinal = _notaRegistro.NotaFinal;
                                _registro.Promedio = _notaRegistro.Promedio;
                                bandera = true;
                            }
                            if (bandera)
                            {
                                _listaNotas.Add(_registro);
                            }

                        }

                        var _ordenRegistro = _listaNotas.OrderByDescending(x => x.Promedio).ToList();

                        int contador = 0, auxinico = 0;
                        int totalRegistros = _ordenRegistro.Count();
                        //int totalRegistros = 968;

                        var _listaPorcentaje = _configurarRanking.listaRegistroRankingPrograma(TipoPrograma);

                        foreach (var item in _ordenRegistro)
                        {
                            contador++;
                            item.Orden = contador;
                        }

                        foreach (var item in _listaPorcentaje)
                        {
                            var _porcentaje = Math.Round((Convert.ToDecimal(Convert.ToDecimal(item.Porcentaje) / 100)), 2);
                            var _Cantidad = Math.Floor(Convert.ToDecimal(totalRegistros) * (_porcentaje));
                            item.Cantidad = Convert.ToInt32(_Cantidad);
                            item.CantidadInicio = auxinico + 1;
                            auxinico = item.Cantidad - auxinico;
                            item.CantidadFinal = auxinico;

                            var registroTop = _ordenRegistro.Where(x => x.Orden >= item.CantidadInicio && x.Orden <= item.CantidadFinal).Select(c =>
                            {
                                c.TopPorcentaje = item.Porcentaje;
                                return c;
                            }).ToList();

                            _listaNotasFinal.AddRange(registroTop);
                        }
                        var _registroTop = _ordenRegistro.Where(x => x.Orden >= (auxinico + 1) && x.Orden <= totalRegistros).Select(c =>
                        {
                            c.TopPorcentaje = 100;
                            return c;
                        }).ToList();

                        _listaNotasFinal.AddRange(_registroTop);



                        foreach (var item in _listaNotasFinal)
                        {
                            var respuestaRanking = _configurarRanking.registrarTablaRankingAlumno(item);
                        }

                        //return Ok(_listaNotasFinal);
                    }
                }


                //else
                //{
                //    return Ok("No Se proceso");
                //}

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region ImportacionExcel
        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 26/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga la plantilla para llenar de ConfigurarSecuenciaVideo
        /// </summary>
        /// <param name="IdPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Response 200 (Archivo Excel) o 400, dependiendo del flujo</returns>
        [Route("[Action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult DescargarPlantillaExcelConfigurarSecuenciaVideo(int IdPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                byte[] archivo = ConfigurarVideoPrograma.ObtenerPlantillaExcelConfigurarSecuenciaVideo(IdPGeneral);
                string nombreArchivo = string.Concat("PlantillaExcelConfigurarSecuenciaVideo-", IdPGeneral, ".xlsx");

                return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 26/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga la plantilla para llenar de ConfiguracionDeVideo
        /// </summary>
		/// <param name="IdPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult DescargarPlantillaExcelConfiguracionDeVideo(int IdPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                byte[] archivo = SesionConfigurarVideo.ObtenerPlantillaExcelConfiguracionDeVideo(IdPGeneral);
                string nombreArchivo = string.Concat("PlantillaExcelConfiguracionDeVideo-", IdPGeneral, ".xlsx");

                return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 26/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Importa el archivo Excel de la seccion ConfigurarSecuenciaVideo
        /// </summary>
		/// <param name="ArchivoExcel">IFormFile a importar de formato Excel</param>
        /// <param name="NombreUsuario">Nombre Usuario</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ImportarExcelConfigurarSecuenciaVideo([FromForm] IFormFile ArchivoExcel, [FromForm] string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int cantidadCorrecto = 0;
                int cantidadIncorrecto = 0;

                using (var paquete = new ExcelPackage(ArchivoExcel.OpenReadStream()))
                {
                    var worksheet = paquete.Workbook.Worksheets[0];

                    var inicio = worksheet.Dimension.Start;
                    var final = worksheet.Dimension.End;
                    #region Inicializacion Valores
                    List<CampoObligatorioCeldaDTO> listaValoresExcel = new List<CampoObligatorioCeldaDTO>();
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Programa", Columna = 0, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "ID del Programa", Columna = 1, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "NroCap", Columna = 2, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Capitulo", Columna = 3, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Sesion", Columna = 4, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Subsesion", Columna = 5, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Orden Fila", Columna = 6, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Id Video", Columna = 7, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Total segundos", Columna = 8, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Archivo de diapositiva", Columna = 9, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Nro. de diapositivas", Columna = 10, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Habilitar sello en video", Columna = 11, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Nombre de imagen - ImgVideo", Columna = 12, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Ancho (px) - ImgVideo", Columna = 13, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Alto (px) - ImgVideo", Columna = 14, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Posicion X - ImgVideo", Columna = 15, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Posicion Y - ImgVideo", Columna = 16, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Habilitar sello en diapositivas", Columna = 17, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Nombre de imagen - ImgDiapositiva", Columna = 18, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Ancho (px) - ImgDiapositiva", Columna = 19, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Alto (px) - ImgDiapositiva", Columna = 20, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Posicion X - ImgDiapositiva", Columna = 21, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Posicion Y - ImgDiapositiva", Columna = 22, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Id Brightcove", Columna = 23, FlagObligatorio = false });
                    #endregion

                    object[,] valoresExcel = worksheet.Cells.GetValue<object[,]>();
                    var idPGeneralExcel = Convert.ToInt32(valoresExcel[1,1]);
                    List<ConfigurarVideoProgramaBO> listaConfigurarVideo = new List<ConfigurarVideoProgramaBO>();
                    var listaConfigurarVideoExistente = _repConfigurarVideoPrograma.GetBy(x => x.IdPgeneral == idPGeneralExcel).ToList();
                    for (int i = inicio.Row; i < final.Row; i++)
                    {
                        try
                        {
                            ConfigurarVideoPrograma = new ConfigurarVideoProgramaBO(_integraDBContext);

                            ConfigurarVideoPrograma.IdPgeneral = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "ID del Programa").Columna]);
                            ConfigurarVideoPrograma.IdDocumentoSeccionPw = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Subsesion").Columna] != null && valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Subsesion").Columna] != string.Empty ? 14 : 13;
                            ConfigurarVideoPrograma.VideoId = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Id Video").Columna].ToString();
                            ConfigurarVideoPrograma.VideoIdBrightcove = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Id Brightcove").Columna].ToString();
                            ConfigurarVideoPrograma.TotalMinutos = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Total segundos").Columna].ToString();
                            ConfigurarVideoPrograma.Archivo = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Archivo de diapositiva").Columna].ToString();
                            ConfigurarVideoPrograma.NroDiapositivas = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Nro. de diapositivas").Columna].ToString();
                            ConfigurarVideoPrograma.Configurado = true;

                            int altoPxImgVideo = 0, altoPyImgVideo = 0, posicionXImgVideo = 0, posicionYImgVideo = 0;
                            int altoPxImgDiapositiva = 0, altoPyImgDiapositiva = 0, posicionXImgDiapositiva = 0, posicionYImgDiapositiva = 0;

                            ConfigurarVideoPrograma.ConImagenVideo = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Habilitar sello en video").Columna] ?? "no").ToString().ToLower().Trim() == "si" ? true : false;
                            ConfigurarVideoPrograma.ImagenVideoNombre = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Nombre de imagen - ImgVideo").Columna] ?? string.Empty).ToString();
                            ConfigurarVideoPrograma.ImagenVideoAlto = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Alto (px) - ImgVideo").Columna] ?? "0").ToString(), out altoPxImgVideo) ? altoPxImgVideo.ToString() : "0";
                            ConfigurarVideoPrograma.ImagenVideoAncho = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Ancho (px) - ImgVideo").Columna] ?? "0").ToString(), out altoPyImgVideo) ? altoPyImgVideo.ToString() : "0";
                            ConfigurarVideoPrograma.ImagenVideoPosicionX = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Posicion X - ImgVideo").Columna] ?? "0").ToString(), out posicionXImgVideo) ? posicionXImgVideo.ToString() : "0";
                            ConfigurarVideoPrograma.ImagenVideoPosicionY = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Posicion Y - ImgVideo").Columna] ?? "0").ToString(), out posicionYImgVideo) ? posicionYImgVideo.ToString() : "0";

                            ConfigurarVideoPrograma.ConImagenDiapositiva = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Habilitar sello en diapositivas").Columna] ?? "no").ToString().ToLower().Trim() == "si" ? true : false;
                            ConfigurarVideoPrograma.ImagenDiapositivaNombre = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Nombre de imagen - ImgDiapositiva").Columna] ?? string.Empty).ToString();
                            ConfigurarVideoPrograma.ImagenDiapositivaAlto = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Alto (px) - ImgDiapositiva").Columna] ?? "0").ToString(), out altoPxImgDiapositiva) ? altoPxImgDiapositiva.ToString() : "0";
                            ConfigurarVideoPrograma.ImagenDiapositivaAncho = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Ancho (px) - ImgDiapositiva").Columna] ?? "0").ToString(), out altoPyImgDiapositiva) ? altoPyImgDiapositiva.ToString() : "0";
                            ConfigurarVideoPrograma.ImagenDiapositivaPosicionX = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Posicion X - ImgDiapositiva").Columna] ?? "0").ToString(), out posicionXImgDiapositiva) ? posicionXImgDiapositiva.ToString() : "0";
                            ConfigurarVideoPrograma.ImagenDiapositivaPosicionY = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Posicion Y - ImgDiapositiva").Columna] ?? "0").ToString(), out posicionYImgDiapositiva) ? posicionYImgDiapositiva.ToString() : "0";

                            ConfigurarVideoPrograma.NumeroFila = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Orden Fila").Columna]);
                            ConfigurarVideoPrograma.Estado = true;
                            ConfigurarVideoPrograma.FechaCreacion = DateTime.Now;
                            ConfigurarVideoPrograma.FechaModificacion = DateTime.Now;
                            ConfigurarVideoPrograma.UsuarioCreacion = NombreUsuario;
                            ConfigurarVideoPrograma.UsuarioModificacion = NombreUsuario;

                            listaConfigurarVideo.Add(ConfigurarVideoPrograma);

                            cantidadCorrecto++;
                        }
                        catch (Exception e)
                        {
                            cantidadIncorrecto++;
                            continue;
                        }
                    }

                    if (listaConfigurarVideo.Count>0)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            _repConfigurarVideoPrograma.EliminarConfiguracionVideo(idPGeneralExcel);
                            foreach (var item in listaConfigurarVideo)
                            {
                                _repConfigurarVideoPrograma.Insert(item);
                                var idantiguo = listaConfigurarVideoExistente.Where(x => x.NumeroFila == item.NumeroFila).FirstOrDefault();
                                if (idantiguo != null)
                                {
                                    _repSesionConfigurarVideo.ActualizarPadreSesionConfiguracionVideo(idantiguo.Id, item.Id);
                                }


                            }

                            scope.Complete();
                        }
                    }
                }

                return Ok(new { cantidadCorrecto, cantidadIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 26/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Importa el archivo Excel de la seccion ConfiguracionDeVideo
        /// </summary>
		/// <param name="ArchivoExcel">IFormFile a importar de formato Excel</param>
        /// <param name="NombreUsuario">Nombre Usuario</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ImportarExcelConfiguracionDeVideo([FromForm] IFormFile ArchivoExcel, [FromForm] string NombreUsuario)
        {
            int? temporal = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int cantidadCorrecto = 0;
                int cantidadIncorrecto = 0;
                SesionConfigurarVideoRepositorio _repSesionConfigurarVideo = new SesionConfigurarVideoRepositorio(_integraDBContext);
                var listaTipoVista = _repSesionConfigurarVideo.ObtenerTipoVistaParaFiltro();

                using (var paquete = new ExcelPackage(ArchivoExcel.OpenReadStream()))
                {

                    var worksheet = paquete.Workbook.Worksheets[0];

                    var inicio = worksheet.Dimension.Start;
                    var final = worksheet.Dimension.End;

                    #region Inicializacion Valores
                    List<CampoObligatorioCeldaDTO> listaValoresExcel = new List<CampoObligatorioCeldaDTO>();
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Programa", Columna = 0, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "ID del Programa", Columna = 1, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "NroCap", Columna = 2, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Capitulo", Columna = 3, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Sesion", Columna = 4, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Subsesion", Columna = 5, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Orden Fila", Columna = 6, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Id Configuracion", Columna = 7, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Duracion segundos", Columna = 8, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Nro. de diapositivas", Columna = 9, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Segundo", Columna = 10, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Tipo Vista", Columna = 11, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "NroDiapositiva", Columna = 12, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Logo video", Columna = 13, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Logo diapositiva", Columna = 14, FlagObligatorio = false });
                    #endregion

                    object[,] valoresExcel = worksheet.Cells.GetValue<object[,]>();
                    var idPGeneralExcel = Convert.ToInt32(valoresExcel[1, 1]);
                    List<SesionConfigurarVideoBO> listaSesionConfigurarVideo = new List<SesionConfigurarVideoBO>();
                    var listaSesionConfigurarVideoExistente = _repSesionConfigurarVideo.ObtenerInformacionVideoExcel(idPGeneralExcel);

                    for (int i = inicio.Row; i < final.Row; i++)
                    {
                        try
                        {
                            SesionConfigurarVideo = new SesionConfigurarVideoBO(_integraDBContext);

                            int minuto = 0, nroDiapositiva = 0;

                            SesionConfigurarVideo.IdConfigurarVideoPrograma = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Id Configuracion").Columna]);
                            SesionConfigurarVideo.Minuto = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Segundo").Columna] ?? 0).ToString(), out minuto) ? minuto : 0;
                            SesionConfigurarVideo.IdTipoVista = listaTipoVista.First(x => x.Id.ToString() == (valoresExcel[i, listaValoresExcel.First(y => y.Campo == "Tipo Vista").Columna] ?? "video/diapositiva").ToString().ToLower().Trim()).Id;
                            SesionConfigurarVideo.NroDiapositiva = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "NroDiapositiva").Columna] ?? 0).ToString(), out nroDiapositiva) ? nroDiapositiva : 0;
                            SesionConfigurarVideo.ConLogoVideo = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Logo video").Columna] ?? "no").ToString().Trim().ToLower() == "si" ? true : false;
                            SesionConfigurarVideo.ConLogoDiapositiva = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Logo diapositiva").Columna] ?? "no").ToString().Trim().ToLower() == "si" ? true : false;

                            SesionConfigurarVideo.Estado = true;
                            SesionConfigurarVideo.FechaCreacion = DateTime.Now;
                            SesionConfigurarVideo.FechaModificacion = DateTime.Now;
                            SesionConfigurarVideo.UsuarioCreacion = "Importacion Excel";
                            SesionConfigurarVideo.UsuarioModificacion = "Importacion Excel";

                            listaSesionConfigurarVideo.Add(SesionConfigurarVideo);
                            cantidadCorrecto++;
                        }
                        catch (Exception e)
                        {
                            cantidadIncorrecto++;
                            continue;
                        }
                    }

                    if (listaSesionConfigurarVideo.Count > 0)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            var eliminarSesionVideo = _repSesionConfigurarVideo.EliminarSesionConfiguracionVideoNuevo(idPGeneralExcel);
                            _repSesionConfigurarVideo.Insert(listaSesionConfigurarVideo);
                            scope.Complete();
                        }
                    }
                }
                return Ok(new { cantidadCorrecto, cantidadIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        /// Tipo Función: POST
        /// Autor: Jose Villena
        /// Fecha: 04/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Configuracion del Video
        /// </summary>
        /// <param name="videoId">Id de Video</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarConfiguracionVideo([FromBody] EliminarConfiguracionVideoDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var configurarVideo = _repConfigurarVideoPrograma.GetBy(x => x.VideoId == obj.VideoId);
                    if (configurarVideo != null)
                    {
                        foreach (var detalle in configurarVideo)
                        {
                            var configurarPrograma = _repSesionConfigurarVideo.GetBy(x => x.IdConfigurarVideoPrograma == detalle.Id);
                            foreach (var item in configurarPrograma)
                            {
                                _repSesionConfigurarVideo.Delete(item.Id, obj.NombreUsuario);
                            }
                        }
                    }

                    scope.Complete();
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Villena
        /// Fecha: 04/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Configuracion del Video
        /// </summary>
        /// <param name="obj">Objeto Eliminacion</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarConfiguracionPrograma([FromBody] EliminarConfiguracionProgramaDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _repSesionConfigurarVideo.EliminarSesionesConfiguracionVideo(obj.IdPGeneral, obj.NombreUsuario);

                    scope.Complete();
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
