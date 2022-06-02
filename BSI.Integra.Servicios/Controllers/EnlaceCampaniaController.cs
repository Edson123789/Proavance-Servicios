using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/EnlaceCampania")]
    public class EnlaceCampaniaController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public EnlaceCampaniaController(integraDBContext integraDBContext) {
            _integraDBContext = integraDBContext;
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCategoriaOrigenFiltro(int IdConjuntoAnuncioFuente)
        {
            try
            {
                CategoriaOrigenRepositorio repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
                return Ok(repCategoriaOrigen.ObtenerCategoriaPorTipoFuente(IdConjuntoAnuncioFuente));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerListaPaisesCodigos()
        {
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                return Ok(_repPais.ObtenerPaisNombreYCodigoIso());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerListaGeneros()
        {
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                return Ok(_repConjuntoAnuncio.ObtenerConjuntoAnuncioListaGeneros());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerListaSegmentos()
        {
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                return Ok(_repConjuntoAnuncio.ObtenerConjuntoAnuncioListaSegmentos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCentroCostoFiltro(string Indicio)
        {
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (Indicio == null || Indicio.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Ok(ListaVacia);
                }

                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                List<PEspecificoCentroCostoDTO> ListaInicial = _repPEspecifico.ObtenerPEspecificoPorCentroCosto2UltimosAnios(Indicio);

                List<FiltroDTO> ListaFinal = new List<FiltroDTO>();
                foreach(PEspecificoCentroCostoDTO Item in ListaInicial)
                {
                    ListaFinal.Add(new FiltroDTO(){ Id = Item.IdPEspecifico, Nombre = Item.Nombre });
                }

                return Ok(ListaFinal); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoCreativoPublicidadFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CreativoPublicidadRepositorio _repCreativoPublicidad = new CreativoPublicidadRepositorio(_integraDBContext);
                return Ok(_repCreativoPublicidad.ObtenerTodoCreativoPublicidadFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFormularioPlantillaFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                FormularioRespuestaPlantillaRepositorio _repFormularioPlantilla = new FormularioRespuestaPlantillaRepositorio(_integraDBContext);
                return Ok(_repFormularioPlantilla.ObtenerTodoFormularioPlantillaFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerConjuntoAnuncioTipoObjetivoFacebookFiltro(int IdConjuntoAnuncioFuente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioTipoObjetivoRepositorio _repConjuntoAnuncioTipoObjetivo = new ConjuntoAnuncioTipoObjetivoRepositorio(_integraDBContext);
                return Ok(_repConjuntoAnuncioTipoObjetivo.ObtenerTodoConjuntoAnuncioTipoObjetivoFacebookFiltro(IdConjuntoAnuncioFuente));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerConjuntoAnuncioTipoObjetivoAdwordsFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioTipoObjetivoRepositorio _repConjuntoAnuncioTipoObjetivo = new ConjuntoAnuncioTipoObjetivoRepositorio(_integraDBContext);
                return Ok(_repConjuntoAnuncioTipoObjetivo.ObtenerTodoConjuntoAnuncioTipoObjetivoAdwordsFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDatosConjuntoAnuncio(int IdConjuntoAnuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                return Ok(_repConjuntoAnuncio.ObtenerDatosConjuntoAnuncio(IdConjuntoAnuncio));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerRegistrosEnlacesCampaniaFacebook(int IdConjuntoAnuncioFuente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AnuncioRepositorio _repAnuncio = new AnuncioRepositorio(_integraDBContext);
                return Ok(_repAnuncio.ObtenerTodoAnuncioPorConjuntoAnuncio(IdConjuntoAnuncioFuente));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerRegistroConjuntoAnuncioYAsociaciones(int IdConjuntoAnuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                AnuncioRepositorio _repAnuncio = new AnuncioRepositorio(_integraDBContext);
                AnuncioElementoRepositorio _repAnuncioElemento = new AnuncioElementoRepositorio(_integraDBContext);

                ConjuntoAnuncioDTO ConjuntoAnuncio = _repConjuntoAnuncio.ObtenerDatosConjuntoAnuncio(IdConjuntoAnuncio).FirstOrDefault();
                ConjuntoAnuncio.Anuncios = _repAnuncio.ObtenerAnunciosPorConjuntoAnuncio(ConjuntoAnuncio.Id);

                foreach (AnuncioDTO Anuncio in ConjuntoAnuncio.Anuncios)
                {
                    Anuncio.Kpis = new List<int>();
                    var ListaKpis = _repAnuncioElemento.ObtenerElementosPorAnuncio(Anuncio.Id);
                    foreach (AnuncioElementoDTO Kpi in ListaKpis)
                    {
                        Anuncio.Kpis.Add(Kpi.IdElemento);

                    }


                }

                FormularioSolicitudRepositorio _repFormulariosolicitud = new FormularioSolicitudRepositorio();
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio();
                if (ConjuntoAnuncio.IdConjuntoAnuncioFuente.Value==1) {
                    foreach (AnuncioDTO Anuncio in ConjuntoAnuncio.Anuncios)
                    {
                        FormularioSolicitudBO FormularioSolicitud = _repFormulariosolicitud.GetBy(x => x.IdConjuntoAnuncio==ConjuntoAnuncio.Id && x.Estado==true ).FirstOrDefault();
                        if (FormularioSolicitud == null) return BadRequest("No se encontro FormularioSolicitud para ConjuntoAnuncio Id=" + ConjuntoAnuncio.Id);

                        FormularioLandingPageBO FormularioLandingPage = _repFormularioLandingPage.GetBy(x => x.IdFormularioSolicitud== FormularioSolicitud.Id && x.Estado==true && x.Nombre.Contains(Anuncio.Nombre) ).FirstOrDefault();
                        if (FormularioLandingPage == null) return BadRequest("No se encontro FormularioLandingPage perteneciente a Anuncio Id=" + Anuncio.Id);
                        
                        ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                        var RegistroEnlace = _repReportesRepositorio.ObtenerListaEnlacesLandingPagePorNombreLandingPage(FormularioLandingPage.Nombre).FirstOrDefault();

                        if (RegistroEnlace != null)
                            Anuncio.EnlaceFormulario = RegistroEnlace.DireccionUrl;
                        else
                            Anuncio.EnlaceFormulario = "";
                    }
                }
                else {
                    if (ConjuntoAnuncio.Nombre != null && !ConjuntoAnuncio.Nombre.Trim().Equals(""))
                    {
                        ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                        var RegistroEnlace = _repReportesRepositorio.ObtenerListaEnlacesLandingPage(ConjuntoAnuncio.Nombre).FirstOrDefault();

                        if (RegistroEnlace != null)
                            ConjuntoAnuncio.EnlaceFormulario = RegistroEnlace.DireccionUrl;
                        else
                            ConjuntoAnuncio.EnlaceFormulario = "";
                    }
                }

                return Ok(ConjuntoAnuncio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarConjuntoAnuncioGrupoAnuncioFacebook([FromBody] ConjuntoAnuncioDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (ObjetoDTO.EsGrupal == null) ObjetoDTO.EsGrupal = false;
                if (ObjetoDTO.EsPaginaWeb == null) ObjetoDTO.EsPaginaWeb = false;
                if (ObjetoDTO.EsPrelanzamiento == null) ObjetoDTO.EsPrelanzamiento = false;

                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
                ConjuntoAnuncioBO ConjuntoAnuncio = new ConjuntoAnuncioBO
                {
                    EsGrupal = ObjetoDTO.EsGrupal,
                    EsPaginaWeb = ObjetoDTO.EsPaginaWeb,
                    EsPrelanzamiento = ObjetoDTO.EsPrelanzamiento,
                    IdCategoriaOrigen = ObjetoDTO.IdCategoriaOrigen,
                    IdConjuntoAnuncioTipoObjetivo = ObjetoDTO.IdConjuntoAnuncioTipoObjetivo,
                    IdFormularioPlantilla = ObjetoDTO.IdFormularioPlantilla,
                    Adicional = ObjetoDTO.Adicional,
                    IdConjuntoAnuncioFuente = ObjetoDTO.IdConjuntoAnuncioFuente,
                    IdCentroCosto = (ObjetoDTO.EsGrupal.Value ? null : ObjetoDTO.IdCentroCosto), // Es NULL cuando el conjunto de anuncios se creo para dos Anuncios con Centro de costo Diferente, entonces el Centro de Costo se Guarda en los Anuncios Directamente
                    Nombre = ObjetoDTO.Nombre,
                    IdConjuntoAnuncioSegmento = ObjetoDTO.IdConjuntoAnuncioSegmento,
                    IdConjuntoAnuncioTipoGenero = ObjetoDTO.IdConjuntoAnuncioTipoGenero,
                    IdPais = ObjetoDTO.IdPais,
                    NumeroAnuncio = ObjetoDTO.NroAnuncio,
                    NumeroSemana = ObjetoDTO.NroSemana,
                    DiaEnvio = ObjetoDTO.DiaEnvio,
                    Propietario = ObjetoDTO.Propietario,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = ObjetoDTO.NombreUsuario,
                    UsuarioModificacion = ObjetoDTO.NombreUsuario
                };

                _repConjuntoAnuncio.Insert(ConjuntoAnuncio);

                ConjuntoAnuncioCodigosDTO Codigos = _repConjuntoAnuncio.ObtenerCodigos(ConjuntoAnuncio.Id).FirstOrDefault();
                if (Codigos == null) throw new Exception("No se encontro el conjunto de anuncio ni sus codigos Id=" + ConjuntoAnuncio.Id);

                if (ConjuntoAnuncio.EsGrupal.Value) Codigos.CentroCosto = "GRUPAL";
                if (ConjuntoAnuncio.EsPaginaWeb.Value) Codigos.Plantilla = "P.WEB";
                if (ConjuntoAnuncio.EsPrelanzamiento.Value) Codigos.CategoriaOrigen = "PRELAN";
                ConjuntoAnuncioBO ConjuntoAnuncioGuardado = _repConjuntoAnuncio.GetBy(x => x.Id == ConjuntoAnuncio.Id).FirstOrDefault();

                if (ConjuntoAnuncioGuardado.IdConjuntoAnuncioFuente == 1)
                {

                    ConjuntoAnuncioGuardado.Nombre =
                        Codigos.CentroCosto + "-" +
                        Codigos.Pais + "-" +
                        Codigos.Plantilla + "-" +
                        Codigos.CategoriaOrigen + "-" +
                        DateTime.Now.Year + "-" +
                        DateTime.Now.Month + "-" +
                        DateTime.Now.Day + "-" +
                        ConjuntoAnuncio.Id + "-" +
                        Codigos.Segmento + "-" +
                        Codigos.Sexo + "-" +
                        ConjuntoAnuncio.Adicional;

                } else if (ConjuntoAnuncioGuardado.IdConjuntoAnuncioFuente == 2)
                {
                    ConjuntoAnuncioGuardado.Nombre =
                        Codigos.CentroCosto + "-" +
                        Codigos.Pais + "-" +
                        ObjetoDTO.Propietario + "-" +
                        Codigos.Plantilla + "-" +
                        Codigos.CategoriaOrigen + "-" +
                        DateTime.Now.Year + "-" +
                        DateTime.Now.Month + "-" +
                        DateTime.Now.Day + "-" +
                        ConjuntoAnuncio.Id + "-" +
                        ConjuntoAnuncio.Adicional + "-" +
                        ObjetoDTO.NroAnuncio;

                } else if (ConjuntoAnuncioGuardado.IdConjuntoAnuncioFuente == 3)
                {
                    ConjuntoAnuncioGuardado.Nombre =
                        ObjetoDTO.NroSemana + "-" + 
                        Codigos.CentroCosto + "-" +
                        ObjetoDTO.DiaEnvio + "-" +
                        Codigos.Pais + "-" +
                        Codigos.Plantilla + "-" +
                        Codigos.CategoriaOrigen + "-" +
                        ConjuntoAnuncio.Adicional;
                } else {
                    throw new Exception("No se pudo identificar la fuente del Conjunto de Anuncio [Facebbok, Addwords, Mailing] + IdConjuntoAnuncio=" + ConjuntoAnuncioGuardado.Id);
                }

                _repConjuntoAnuncio.Update(ConjuntoAnuncioGuardado);


                AnuncioRepositorio _repAnuncio = new AnuncioRepositorio(_integraDBContext);
                AnuncioElementoRepositorio _repAnuncioElemento = new AnuncioElementoRepositorio(_integraDBContext);
                for (int a=0; a< ObjetoDTO.Anuncios.Count; ++a)
                {
                    AnuncioBO Anuncio = new AnuncioBO() {
                        IdConjuntoAnuncio = ConjuntoAnuncioGuardado.Id,
                        IdCreativoPublicidad = ObjetoDTO.Anuncios[a].IdCreativoPublicidad,
                        Nombre = ObjetoDTO.Anuncios[a].Nombre,
                        EnlaceFormulario = ObjetoDTO.Anuncios[a].EnlaceFormulario,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = ObjetoDTO.NombreUsuario,
                        UsuarioModificacion = ObjetoDTO.NombreUsuario,
                        NroAnuncioCorrelativo=ObjetoDTO.Anuncios[a].NroAnuncioCorrelativo
                    };

                    //if (ObjetoDTO.EsGrupal == null)
                    //    Anuncio.IdCentroCosto = ObjetoDTO.Anuncios[a].IdCentroCosto;

                    //else
                    Anuncio.IdCentroCosto = (ObjetoDTO.EsGrupal.Value==false? ObjetoDTO.IdCentroCosto.Value : ObjetoDTO.Anuncios[a].IdCentroCosto);

                    _repAnuncio.Insert(Anuncio);


                    if (ConjuntoAnuncio.EsGrupal.Value) {
                        PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                        PEspecificoCentroCostoDTO Dato = _repPEspecifico.ObtenerCentroCostoPorPEspecifico(Anuncio.IdCentroCosto).FirstOrDefault();
                        if (Dato == null)
                            return BadRequest("Error no se pudo identificar el Centro de costo para el Anuncio Id=" + Anuncio.Id);

                        Codigos.CentroCosto = Dato.Nombre;
                    }

                    AnuncioBO AnuncioGuardado = _repAnuncio.GetBy(x => x.Id == Anuncio.Id).FirstOrDefault();
                    AnuncioGuardado.Nombre =
                        Codigos.CentroCosto + "-" +
                        Codigos.Pais + "-" +
                        Codigos.Plantilla + "-" +
                        Codigos.CategoriaOrigen + "-" +
                        DateTime.Now.Year + "-" +
                        DateTime.Now.Month + "-" +
                        DateTime.Now.Day + "-" +
                        //AnuncioGuardado.Id + "-" +
                        ConjuntoAnuncio.Id + "-" +
                        AnuncioGuardado.NroAnuncioCorrelativo + "-" +
                        Codigos.Segmento + "-" +
                        Codigos.Sexo + "-" +
                        ConjuntoAnuncio.Adicional;

                    ObjetoDTO.Anuncios[a].Id = AnuncioGuardado.Id;

                    ElementoRepositorio _repElemento = new ElementoRepositorio();

                    if (ObjetoDTO.Anuncios[a].Kpis.Count > 0) AnuncioGuardado.Nombre += "~";
                    for (int k = 0; k < ObjetoDTO.Anuncios[a].Kpis.Count; ++k)
                    {
                        AnuncioElementoBO AnuncioElemento = new AnuncioElementoBO
                        {
                            IdAnuncio = Anuncio.Id,
                            IdElemento = ObjetoDTO.Anuncios[a].Kpis[k],
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ObjetoDTO.NombreUsuario,
                            UsuarioModificacion = ObjetoDTO.NombreUsuario
                        };

                        _repAnuncioElemento.Insert(AnuncioElemento);
                        if (ObjetoDTO.Anuncios[a].Kpis.Count == k+1 ) 
                            AnuncioGuardado.Nombre += _repElemento.GetBy(x => x.Id == ObjetoDTO.Anuncios[a].Kpis[k]).FirstOrDefault().Codigo;
                        else
                            AnuncioGuardado.Nombre += (_repElemento.GetBy(x => x.Id == ObjetoDTO.Anuncios[a].Kpis[k]).FirstOrDefault().Codigo + "-");
                    }

                    _repAnuncio.Update(AnuncioGuardado);
                    ObjetoDTO.Anuncios[a].Nombre = AnuncioGuardado.Nombre; // se lee los nombres de anuncio del  ObjetoDTO.Anuncios mas adelante para crear nombres de landing page
                }
                if (ObjetoDTO.IdConjuntoAnuncioTipoObjetivo == 2)
                {
                    ObjetoDTO.IdFormularioPlantilla = 12;                    
                }
                /************** INSERCION DE DATOS QUE DEPENDEN DEL CJTO ANUNCIO A FORM. SOLICITUD Y FORM. LANDING PAGE RESPECTIVAMENTE ****************/
                FormularioPlantillaRepositorio _repFormularioPlantilla = new FormularioPlantillaRepositorio(_integraDBContext);
                FormularioPlantillaBO FormularioPlantilla = _repFormularioPlantilla.GetBy(x => x.Id == ObjetoDTO.IdFormularioPlantilla).FirstOrDefault();

                /***** FORMULARIO Solicitud ******/
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio();
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio(_integraDBContext);
                

                var data = _repFormularioSolicitud.ObtenerConjuntoAnunciosFiltro(ConjuntoAnuncioGuardado.Id).FirstOrDefault();
                FormularioSolicitudBO FormularioSolicitud = _repFormularioSolicitud.GetBy(x => x.Id == FormularioPlantilla.IdFormularioSolicitud.Value).FirstOrDefault();

                FormularioSolicitudBO NuevoFormularioSolicitud = new FormularioSolicitudBO();
                NuevoFormularioSolicitud.IdFormularioRespuesta = 223; // hardcodeo: en la creacion de anuncios de fb no afecta este campo (siempre eligen cualquier formulario)
                NuevoFormularioSolicitud.Nombre = data.Codigo;
                NuevoFormularioSolicitud.Codigo = data.Codigo;
                NuevoFormularioSolicitud.Campanha = ConjuntoAnuncioGuardado.Nombre;
                NuevoFormularioSolicitud.IdConjuntoAnuncio = ConjuntoAnuncioGuardado.Id;
                NuevoFormularioSolicitud.Proveedor = data.NombreProveedor;
                NuevoFormularioSolicitud.IdFormularioSolicitudTextoBoton = FormularioSolicitud.IdFormularioSolicitudTextoBoton;
                NuevoFormularioSolicitud.TipoSegmento = FormularioSolicitud.TipoSegmento;
                NuevoFormularioSolicitud.CodigoSegmento = FormularioSolicitud.CodigoSegmento;
                NuevoFormularioSolicitud.TipoEvento = FormularioSolicitud.TipoEvento;
                NuevoFormularioSolicitud.UrlbotonInvitacionPagina = FormularioSolicitud.UrlbotonInvitacionPagina;
                NuevoFormularioSolicitud.Estado = true;
                NuevoFormularioSolicitud.FechaCreacion = DateTime.Now;
                NuevoFormularioSolicitud.FechaModificacion = DateTime.Now;
                NuevoFormularioSolicitud.UsuarioCreacion = ObjetoDTO.NombreUsuario;
                NuevoFormularioSolicitud.UsuarioModificacion = ObjetoDTO.NombreUsuario;

                _repFormularioSolicitud.Insert(NuevoFormularioSolicitud);

                List<CampoFormularioBO> CamposPlantilla = _repCampoFormulario.GetBy(w => w.IdFormularioSolicitud == FormularioSolicitud.Id).ToList();
                List<CampoFormularioBO> camposnuevos = new List<CampoFormularioBO>();
                foreach (var CampoFormulario in CamposPlantilla)
                {
                    CampoFormularioBO Campo = new CampoFormularioBO()
                    {
                        IdFormularioSolicitud = NuevoFormularioSolicitud.Id,
                        IdCampoContacto = CampoFormulario.IdCampoContacto,
                        NroVisitas = CampoFormulario.NroVisitas,
                        Codigo = CampoFormulario.Codigo,
                        Campo = CampoFormulario.Campo,
                        Siempre = CampoFormulario.Siempre,
                        Inteligente = CampoFormulario.Inteligente,
                        Probabilidad = CampoFormulario.Probabilidad,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = ObjetoDTO.NombreUsuario,
                        UsuarioModificacion = ObjetoDTO.NombreUsuario

                    };

                    camposnuevos.Add(Campo);
                }
                _repCampoFormulario.Insert(camposnuevos);

                /***** FORMULARIO LANDING PAGE ******/

                if (ObjetoDTO.IdConjuntoAnuncioFuente != 1) // en caso no se tenga que registrar LP por anuncio  (generalmente Adwrods y Mailing)
                {
                    AdicionalesPGeneralDTO adicionalesPGeneralDTO = new AdicionalesPGeneralDTO();
                    PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);

                    adicionalesPGeneralDTO.PGeneralNombreDescripcion = _repPEspecifico.ObtenerNombreDescripcion(ConjuntoAnuncioGuardado.IdCentroCosto.Value);

                    FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio(_integraDBContext);
                    FormularioLandingPageBO FormularioLandingPage = _repFormularioLandingPage.GetBy(x => x.Id == FormularioPlantilla.IdFormularioLandingPage.Value).FirstOrDefault();
                    FormularioLandingPageBO NuevoFormularioLandingPage = new FormularioLandingPageBO();

                    // no trabajar con valores null solo evaluar los son true o false
                    if (FormularioLandingPage.TituloProgramaAutomatico != null && FormularioLandingPage.DescripcionWebAutomatico != null)
                    {

                        NuevoFormularioLandingPage.IdFormularioSolicitud = NuevoFormularioSolicitud.Id;
                        //NuevoFormularioLandingPage.Nombre = FormularioLandingPage.Nombre;
                        //NuevoFormularioLandingPage.Codigo = FormularioLandingPage.Codigo;
                        NuevoFormularioLandingPage.Header = FormularioLandingPage.Header;
                        NuevoFormularioLandingPage.Footer = FormularioLandingPage.Footer;
                        NuevoFormularioLandingPage.IdPlantillaLandingPage = FormularioLandingPage.IdPlantillaLandingPage;
                        NuevoFormularioLandingPage.Mensaje = FormularioLandingPage.Mensaje;
                        NuevoFormularioLandingPage.TextoPopup = FormularioLandingPage.TextoPopup;
                        NuevoFormularioLandingPage.TituloPopup = FormularioLandingPage.TituloPopup;
                        NuevoFormularioLandingPage.ColorPopup = FormularioLandingPage.ColorPopup;
                        NuevoFormularioLandingPage.ColorTitulo = FormularioLandingPage.ColorTitulo;
                        NuevoFormularioLandingPage.ColorTextoBoton = FormularioLandingPage.ColorTextoBoton;
                        NuevoFormularioLandingPage.ColorFondoBoton = FormularioLandingPage.ColorFondoBoton;
                        NuevoFormularioLandingPage.ColorDescripcion = FormularioLandingPage.ColorDescripcion;
                        NuevoFormularioLandingPage.ColorFondoHeader = FormularioLandingPage.ColorFondoHeader;
                        NuevoFormularioLandingPage.Tipo = FormularioLandingPage.Tipo;
                        NuevoFormularioLandingPage.Cita1Texto = FormularioLandingPage.Cita1Texto;
                        NuevoFormularioLandingPage.Cita1Color = FormularioLandingPage.Cita1Color;
                        NuevoFormularioLandingPage.Cita3Texto = FormularioLandingPage.Cita3Texto;
                        NuevoFormularioLandingPage.Cita3Color = FormularioLandingPage.Cita3Color;
                        NuevoFormularioLandingPage.Cita4Texto = FormularioLandingPage.Cita4Texto;
                        NuevoFormularioLandingPage.Cita4Color = FormularioLandingPage.Cita4Color;
                        NuevoFormularioLandingPage.Cita1Despues = FormularioLandingPage.Cita1Despues;
                        NuevoFormularioLandingPage.MuestraPrograma = FormularioLandingPage.MuestraPrograma;
                        NuevoFormularioLandingPage.UrlImagenPrincipal = FormularioLandingPage.UrlImagenPrincipal;
                        NuevoFormularioLandingPage.ColorPlaceHolder = FormularioLandingPage.ColorPlaceHolder;
                        NuevoFormularioLandingPage.IdGmailClienteRemitente = FormularioLandingPage.IdGmailClienteRemitente;
                        NuevoFormularioLandingPage.IdGmailClienteReceptor = FormularioLandingPage.IdGmailClienteReceptor;
                        NuevoFormularioLandingPage.IdPlantilla = FormularioLandingPage.IdPlantilla;
                        NuevoFormularioLandingPage.TesteoAb = FormularioLandingPage.TesteoAb;
                        NuevoFormularioLandingPage.Estado = true;
                        NuevoFormularioLandingPage.UsuarioCreacion = ObjetoDTO.NombreUsuario;
                        NuevoFormularioLandingPage.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                        NuevoFormularioLandingPage.FechaCreacion = DateTime.Now;
                        NuevoFormularioLandingPage.FechaModificacion = DateTime.Now;

                        PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                        ListaPlantillaRepositorio _repListaPlantilla = new ListaPlantillaRepositorio();

                        var plantilla = _repPlantillaLandingPage.FirstById(FormularioLandingPage.IdPlantillaLandingPage);
                        var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);

                        if (listaplantilla != null && (listaplantilla.Nombre.Contains("PCFB") || listaplantilla.Nombre.Contains("PCMVFB"))) NuevoFormularioLandingPage.EstadoPopup = true;
                        else NuevoFormularioLandingPage.EstadoPopup = false;

                        if (FormularioLandingPage.TituloProgramaAutomatico.Value)
                            if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                NuevoFormularioLandingPage.TituloPopup = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Nombre ?? "");
                            else
                                NuevoFormularioLandingPage.TituloPopup = "";


                        if (FormularioLandingPage.DescripcionWebAutomatico.Value)
                            if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                NuevoFormularioLandingPage.Cita3Texto = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Descripcion ?? "");
                            else
                                NuevoFormularioLandingPage.Cita3Texto = "";

                        NuevoFormularioLandingPage.IdPespecifico = ConjuntoAnuncioGuardado.IdCentroCosto.Value;


                        int cantidad = _repFormularioLandingPage.GetBy(x => x.Codigo.Contains(ConjuntoAnuncioGuardado.Nombre)).Count();

                        string Codigo = "LP-";
                        if (cantidad >= 1) Codigo = Codigo + ConjuntoAnuncioGuardado.Nombre + cantidad;
                        else Codigo = Codigo + ConjuntoAnuncioGuardado.Nombre;
                        NuevoFormularioLandingPage.Codigo = Codigo;
                        NuevoFormularioLandingPage.Nombre = Codigo;

                        _repFormularioLandingPage.Insert(NuevoFormularioLandingPage);

                        AdicionalProgramaGeneralRepositorio _repAdicional = new AdicionalProgramaGeneralRepositorio(_integraDBContext);

                        if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                            adicionalesPGeneralDTO.datosAdicionales = _repAdicional.ObtenerAdicionalProgramaPorIdPlantilla(FormularioLandingPage.IdPlantillaLandingPage, adicionalesPGeneralDTO.PGeneralNombreDescripcion.IdPGeneral);
                        

                        if (adicionalesPGeneralDTO.datosAdicionales != null && adicionalesPGeneralDTO.datosAdicionales.Count > 0)
                        {
                            List<DatoAdicionalPaginaBO> ListaDatosAdicionales = new List<DatoAdicionalPaginaBO>();
                            foreach (var item in adicionalesPGeneralDTO.datosAdicionales)
                            {
                                ListaDatosAdicionales.Add(new DatoAdicionalPaginaBO()
                                {
                                    IdFormularioLandingPage = NuevoFormularioLandingPage.Id,
                                    IdTitulo = item.IdTitulo,
                                    NombreTitulo = item.NombreTitulo,
                                    Descripcion = item.Descripcion,
                                    NombreImagen = item.NombreImagen,
                                    ColorTitulo = item.ColorTitulo,
                                    ColorDescripcion = item.ColorDescripcion,
                                    Estado = true,
                                    UsuarioCreacion = ObjetoDTO.NombreUsuario,
                                    UsuarioModificacion = ObjetoDTO.NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                });
                            }
                            DatoAdicionalPaginaRepositorio _repDatoAdicionalPagina = new DatoAdicionalPaginaRepositorio(_integraDBContext);
                            _repDatoAdicionalPagina.Insert(ListaDatosAdicionales);
                        }
                        using (TransactionScope scope = new TransactionScope())
                        {
                            _repFormularioLandingPage.InsertarFormularioPortal(NuevoFormularioLandingPage.Id, ObjetoDTO.NombreUsuario, NuevoFormularioLandingPage.IdPlantillaLandingPage);
                            scope.Complete();
                        }
                    }
                    else
                    {
                        return BadRequest("La plantilla de Form. Landing Page tiene valores NULL no permitidos IdFormularioLandingPage:" + FormularioLandingPage.Id);
                    }


                }
                else
                {
                    FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio(_integraDBContext);
                    FormularioLandingPageBO FormularioLandingPagePlantilla = _repFormularioLandingPage.GetBy(x => x.Id == FormularioPlantilla.IdFormularioLandingPage.Value).FirstOrDefault();
                    
                    foreach (AnuncioDTO Anuncio in ObjetoDTO.Anuncios)
                    {
                        AdicionalesPGeneralDTO adicionalesPGeneralDTO = new AdicionalesPGeneralDTO();
                        PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);

                        adicionalesPGeneralDTO.PGeneralNombreDescripcion = _repPEspecifico.ObtenerNombreDescripcion(Anuncio.IdCentroCosto);

                        FormularioLandingPageBO NuevoFormularioLandingPage = new FormularioLandingPageBO();

                        // no trabajar con valores null solo evaluar los son true o false
                        if (FormularioLandingPagePlantilla.TituloProgramaAutomatico != null && FormularioLandingPagePlantilla.DescripcionWebAutomatico != null)
                        {

                            NuevoFormularioLandingPage.IdFormularioSolicitud = NuevoFormularioSolicitud.Id;
                            NuevoFormularioLandingPage.Nombre = FormularioLandingPagePlantilla.Nombre;
                            NuevoFormularioLandingPage.Codigo = FormularioLandingPagePlantilla.Codigo;
                            NuevoFormularioLandingPage.Header = FormularioLandingPagePlantilla.Header;
                            NuevoFormularioLandingPage.Footer = FormularioLandingPagePlantilla.Footer;
                            NuevoFormularioLandingPage.IdPlantillaLandingPage = FormularioLandingPagePlantilla.IdPlantillaLandingPage;
                            NuevoFormularioLandingPage.Mensaje = FormularioLandingPagePlantilla.Mensaje;
                            NuevoFormularioLandingPage.TextoPopup = FormularioLandingPagePlantilla.TextoPopup;
                            NuevoFormularioLandingPage.TituloPopup = FormularioLandingPagePlantilla.TituloPopup;
                            NuevoFormularioLandingPage.ColorPopup = FormularioLandingPagePlantilla.ColorPopup;
                            NuevoFormularioLandingPage.ColorTitulo = FormularioLandingPagePlantilla.ColorTitulo;
                            NuevoFormularioLandingPage.ColorTextoBoton = FormularioLandingPagePlantilla.ColorTextoBoton;
                            NuevoFormularioLandingPage.ColorFondoBoton = FormularioLandingPagePlantilla.ColorFondoBoton;
                            NuevoFormularioLandingPage.ColorDescripcion = FormularioLandingPagePlantilla.ColorDescripcion;
                            NuevoFormularioLandingPage.ColorFondoHeader = FormularioLandingPagePlantilla.ColorFondoHeader;
                            NuevoFormularioLandingPage.Tipo = FormularioLandingPagePlantilla.Tipo;
                            NuevoFormularioLandingPage.Cita1Texto = FormularioLandingPagePlantilla.Cita1Texto;
                            NuevoFormularioLandingPage.Cita1Color = FormularioLandingPagePlantilla.Cita1Color;
                            NuevoFormularioLandingPage.Cita3Texto = FormularioLandingPagePlantilla.Cita3Texto;
                            NuevoFormularioLandingPage.Cita3Color = FormularioLandingPagePlantilla.Cita3Color;
                            NuevoFormularioLandingPage.Cita4Texto = FormularioLandingPagePlantilla.Cita4Texto;
                            NuevoFormularioLandingPage.Cita4Color = FormularioLandingPagePlantilla.Cita4Color;
                            NuevoFormularioLandingPage.Cita1Despues = FormularioLandingPagePlantilla.Cita1Despues;
                            NuevoFormularioLandingPage.MuestraPrograma = FormularioLandingPagePlantilla.MuestraPrograma;
                            NuevoFormularioLandingPage.UrlImagenPrincipal = FormularioLandingPagePlantilla.UrlImagenPrincipal;
                            NuevoFormularioLandingPage.ColorPlaceHolder = FormularioLandingPagePlantilla.ColorPlaceHolder;
                            NuevoFormularioLandingPage.IdGmailClienteRemitente = FormularioLandingPagePlantilla.IdGmailClienteRemitente;
                            NuevoFormularioLandingPage.IdGmailClienteReceptor = FormularioLandingPagePlantilla.IdGmailClienteReceptor;
                            NuevoFormularioLandingPage.IdPlantilla = FormularioLandingPagePlantilla.IdPlantilla;
                            NuevoFormularioLandingPage.TesteoAb = FormularioLandingPagePlantilla.TesteoAb;
                            NuevoFormularioLandingPage.Estado = true;
                            NuevoFormularioLandingPage.UsuarioCreacion = ObjetoDTO.NombreUsuario;
                            NuevoFormularioLandingPage.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                            NuevoFormularioLandingPage.FechaCreacion = DateTime.Now;
                            NuevoFormularioLandingPage.FechaModificacion = DateTime.Now;

                            PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                            ListaPlantillaRepositorio _repListaPlantilla = new ListaPlantillaRepositorio();

                            var plantilla = _repPlantillaLandingPage.FirstById(FormularioLandingPagePlantilla.IdPlantillaLandingPage);
                            var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);

                            if (listaplantilla != null && (listaplantilla.Nombre.Contains("PCFB") || listaplantilla.Nombre.Contains("PCMVFB"))) NuevoFormularioLandingPage.EstadoPopup = true;
                            else NuevoFormularioLandingPage.EstadoPopup = false;

                            if (FormularioLandingPagePlantilla.TituloProgramaAutomatico.Value)
                                if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                    NuevoFormularioLandingPage.TituloPopup = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Nombre ?? "");
                                else
                                    NuevoFormularioLandingPage.TituloPopup = "";


                            if (FormularioLandingPagePlantilla.DescripcionWebAutomatico.Value)
                                if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                    NuevoFormularioLandingPage.Cita3Texto = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Descripcion ?? "");
                                else
                                    NuevoFormularioLandingPage.Cita3Texto = "";

                            NuevoFormularioLandingPage.IdPespecifico = Anuncio.IdCentroCosto;


                            int cantidad = _repFormularioLandingPage.GetBy(x => x.Codigo.Contains(Anuncio.Nombre)).Count();

                            string Codigo = "LP-";
                            if (cantidad >= 1) Codigo = Codigo + Anuncio.Nombre + cantidad;
                            else Codigo = Codigo + Anuncio.Nombre;
                            NuevoFormularioLandingPage.Codigo = Codigo;
                            NuevoFormularioLandingPage.Nombre = Codigo;

                            _repFormularioLandingPage.Insert(NuevoFormularioLandingPage);

                            AdicionalProgramaGeneralRepositorio _repAdicional = new AdicionalProgramaGeneralRepositorio(_integraDBContext);

                            if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                adicionalesPGeneralDTO.datosAdicionales = _repAdicional.ObtenerAdicionalProgramaPorIdPlantilla(NuevoFormularioLandingPage.IdPlantillaLandingPage, adicionalesPGeneralDTO.PGeneralNombreDescripcion.IdPGeneral);
                            

                            if (adicionalesPGeneralDTO.datosAdicionales != null && adicionalesPGeneralDTO.datosAdicionales.Count > 0)
                            {
                                List<DatoAdicionalPaginaBO> ListaDatosAdicionales = new List<DatoAdicionalPaginaBO>();
                                foreach (var item in adicionalesPGeneralDTO.datosAdicionales)
                                {
                                    ListaDatosAdicionales.Add(new DatoAdicionalPaginaBO()
                                    {
                                        IdFormularioLandingPage = NuevoFormularioLandingPage.Id,
                                        IdTitulo = item.IdTitulo,
                                        NombreTitulo = item.NombreTitulo,
                                        Descripcion = item.Descripcion,
                                        NombreImagen = item.NombreImagen,
                                        ColorTitulo = item.ColorTitulo,
                                        ColorDescripcion = item.ColorDescripcion,
                                        Estado = true,
                                        UsuarioCreacion = ObjetoDTO.NombreUsuario,
                                        UsuarioModificacion = ObjetoDTO.NombreUsuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now
                                    });
                                }
                                DatoAdicionalPaginaRepositorio _repDatoAdicionalPagina = new DatoAdicionalPaginaRepositorio(_integraDBContext);
                                _repDatoAdicionalPagina.Insert(ListaDatosAdicionales);
                            }
                            using (TransactionScope scope = new TransactionScope())
                            {
                                if (ObjetoDTO.IdConjuntoAnuncioTipoObjetivo != 2)
                                {
                                    _repFormularioLandingPage.InsertarFormularioPortal(NuevoFormularioLandingPage.Id, ObjetoDTO.NombreUsuario, NuevoFormularioLandingPage.IdPlantillaLandingPage);
                                    scope.Complete();
                                }
                            }
                        } else
                        {
                            return BadRequest("La plantilla de Form. Landing Page tiene valores NULL no permitidos IdFormularioLandingPage:" + FormularioLandingPagePlantilla.Id);
                        }
                    }
                }


                /************** FIN: INSERCION DE DATOS QUE DEPENDEN DEL CJTO ANUNCIO A FORM. SOLICITUD Y FORM. LANDING PAGE RESPECTIVAMENTE ****************/

                return Ok(new { IdConjuntoAnuncio = ConjuntoAnuncioGuardado.Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarConjuntoAnuncioGrupoAnuncioFacebook([FromBody] ConjuntoAnuncioDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (ObjetoDTO.EsGrupal == null) ObjetoDTO.EsGrupal = false;
                if (ObjetoDTO.EsPaginaWeb == null) ObjetoDTO.EsPaginaWeb = false;
                if (ObjetoDTO.EsPrelanzamiento == null) ObjetoDTO.EsPrelanzamiento = false;
                bool CentroCostoCambiado = false;
                int IdFormularioPlantillaAnterior;

                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                ConjuntoAnuncioBO ConjuntoAnuncio = _repConjuntoAnuncio.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                if (ConjuntoAnuncio.IdCentroCosto != ObjetoDTO.IdCentroCosto) CentroCostoCambiado = true;
                //IdFormularioPlantillaAnterior = ConjuntoAnuncio.IdFormularioPlantilla == (int?)null ? ObjetoDTO.IdFormularioPlantilla.Value : ConjuntoAnuncio.IdFormularioPlantilla.Value;
                if (ObjetoDTO.IdConjuntoAnuncioTipoObjetivo != 2)
                {
                    IdFormularioPlantillaAnterior = ConjuntoAnuncio.IdFormularioPlantilla.Value;
                }
                ConjuntoAnuncio.EsGrupal = ObjetoDTO.EsGrupal;
                ConjuntoAnuncio.EsPaginaWeb = ObjetoDTO.EsPaginaWeb;
                ConjuntoAnuncio.EsPrelanzamiento = ObjetoDTO.EsPrelanzamiento;
                ConjuntoAnuncio.IdCentroCosto = (ObjetoDTO.EsGrupal.Value ? null : ObjetoDTO.IdCentroCosto);
                ConjuntoAnuncio.IdCategoriaOrigen = ObjetoDTO.IdCategoriaOrigen;
                ConjuntoAnuncio.IdConjuntoAnuncioTipoObjetivo = ObjetoDTO.IdConjuntoAnuncioTipoObjetivo;
                ConjuntoAnuncio.IdFormularioPlantilla = ObjetoDTO.IdFormularioPlantilla;
                ConjuntoAnuncio.Adicional = ObjetoDTO.Adicional;
                ConjuntoAnuncio.IdConjuntoAnuncioSegmento = ObjetoDTO.IdConjuntoAnuncioSegmento;
                ConjuntoAnuncio.IdConjuntoAnuncioTipoGenero = ObjetoDTO.IdConjuntoAnuncioTipoGenero;
                ConjuntoAnuncio.IdPais = ObjetoDTO.IdPais;
                ConjuntoAnuncio.NumeroAnuncio = ObjetoDTO.NroAnuncio;
                ConjuntoAnuncio.NumeroSemana = ObjetoDTO.NroSemana;
                ConjuntoAnuncio.DiaEnvio = ObjetoDTO.DiaEnvio;
                ConjuntoAnuncio.Propietario = ObjetoDTO.Propietario;
                ConjuntoAnuncio.FechaModificacion = DateTime.Now;
                ConjuntoAnuncio.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                _repConjuntoAnuncio.Update(ConjuntoAnuncio);

                ConjuntoAnuncio = _repConjuntoAnuncio.GetBy(x => x.Id == ConjuntoAnuncio.Id).FirstOrDefault() ;
                ConjuntoAnuncioCodigosDTO Codigos = _repConjuntoAnuncio.ObtenerCodigos(ConjuntoAnuncio.Id).FirstOrDefault();
                if (Codigos == null) throw new Exception("No se encontro el conjunto de anuncio ni sus codigos Id=" + ConjuntoAnuncio.Id);

                if (ConjuntoAnuncio.EsGrupal.Value) Codigos.CentroCosto = "GRUPAL";
                if (ConjuntoAnuncio.EsPaginaWeb.Value) Codigos.Plantilla = "P.WEB";
                if (ConjuntoAnuncio.EsPrelanzamiento.Value) Codigos.CategoriaOrigen = "PRELAN";

                if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1)
                {
                    ConjuntoAnuncio.Nombre =
                        Codigos.CentroCosto + "-" +
                        Codigos.Pais + "-" +
                        Codigos.Plantilla + "-" +
                        Codigos.CategoriaOrigen + "-" +
                        DateTime.Now.Year + "-" +
                        DateTime.Now.Month + "-" +
                        DateTime.Now.Day + "-" +
                        ConjuntoAnuncio.Id + "-" +
                        Codigos.Segmento + "-" +
                        Codigos.Sexo + "-" +
                        ConjuntoAnuncio.Adicional;
                }
                else if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 2)
                {
                    ConjuntoAnuncio.Nombre =
                        Codigos.CentroCosto + "-" +
                        Codigos.Pais + "-" +
                        ObjetoDTO.Propietario + "-" +
                        Codigos.Plantilla + "-" +
                        Codigos.CategoriaOrigen + "-" +
                        DateTime.Now.Year + "-" +
                        DateTime.Now.Month + "-" +
                        DateTime.Now.Day + "-" +
                        ConjuntoAnuncio.Id + "-" +
                        ConjuntoAnuncio.Adicional + "-" +
                        ObjetoDTO.NroAnuncio;

                }
                else if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 3)
                {
                    ConjuntoAnuncio.Nombre =
                        ObjetoDTO.NroSemana + "-" +
                        Codigos.CentroCosto + "-" +
                        ObjetoDTO.DiaEnvio + "-" +
                        Codigos.Pais + "-" +
                        Codigos.Plantilla + "-" +
                        Codigos.CategoriaOrigen + "-" +
                        ConjuntoAnuncio.Adicional;
                }
                else
                {
                    throw new Exception("No se pudo identificar la fuente del Conjunto de Anuncio [Facebbok, Addwords, Mailing] + IdConjuntoAnuncio=" + ConjuntoAnuncio.Id);
                }
                _repConjuntoAnuncio.Update(ConjuntoAnuncio);

                AnuncioRepositorio _repAnuncio = new AnuncioRepositorio(_integraDBContext);
                AnuncioElementoRepositorio _repAnuncioElemento = new AnuncioElementoRepositorio(_integraDBContext);
               
                List<AnuncioBO> AnunciosOriginales = _repAnuncio.GetBy(x=>x.IdConjuntoAnuncio == ConjuntoAnuncio.Id && x.Estado==true).ToList();
                //List<int> FormularioSolicitudEliminar = new List<int>();
                //foreach(AnuncioBO Anuncio in AnunciosOriginales)
                //{
                //    FormularioSolicitudEliminar.Add(Anuncio.Id)
                //}
                
                List<EliminarDTO> AnunciosEliminar = new List<EliminarDTO>();
                List<AnuncioBO> AnunciosAgregar = new List<AnuncioBO>();
                List<AnuncioElementoBO> KpisAgregar2 = new List<AnuncioElementoBO>();
                List<List<AnuncioElementoBO>> KpisAgregar = new List<List<AnuncioElementoBO>>();
                List<AnuncioBO> AnunciosActualizar = new List<AnuncioBO>();


                foreach(AnuncioDTO AnuncioRecibido in ObjetoDTO.Anuncios)
                {
                    bool AnuncioIncluido = false;
                    foreach(AnuncioBO AnuncioOrginal in AnunciosOriginales)
                    {
                        if (AnuncioOrginal.Id == AnuncioRecibido.Id)
                        {
                            AnuncioIncluido = true;
                            AnuncioOrginal.FechaModificacion = DateTime.Now;
                            AnuncioOrginal.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                            AnuncioOrginal.IdCentroCosto = (ObjetoDTO.EsGrupal.Value ==false? ObjetoDTO.IdCentroCosto.Value : AnuncioRecibido.IdCentroCosto);
                            AnuncioOrginal.IdCreativoPublicidad = AnuncioRecibido.IdCreativoPublicidad;
                            AnuncioOrginal.NroAnuncioCorrelativo = AnuncioRecibido.NroAnuncioCorrelativo;
                            if (ConjuntoAnuncio.EsGrupal.Value)
                            {
                                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                                PEspecificoCentroCostoDTO Dato = _repPEspecifico.ObtenerCentroCostoPorPEspecifico(AnuncioOrginal.IdCentroCosto).FirstOrDefault();
                                if (Dato == null)
                                    return BadRequest("Error no se pudo identificar el Centro de costo para el Anuncio Id=" + AnuncioOrginal.Id);

                                Codigos.CentroCosto = Dato.Nombre;
                            }

                            AnuncioOrginal.Nombre =
                                Codigos.CentroCosto + "-" +
                                Codigos.Pais + "-" +
                                Codigos.Plantilla + "-" +
                                Codigos.CategoriaOrigen + "-" +
                                DateTime.Now.Year + "-" +
                                DateTime.Now.Month + "-" +
                                DateTime.Now.Day + "-" +
                                ConjuntoAnuncio.Id + "-" +
                                AnuncioOrginal.NroAnuncioCorrelativo + "-" +
                                Codigos.Segmento + "-" +
                                Codigos.Sexo + "-" +
                                ConjuntoAnuncio.Adicional;
                                
                            List<AnuncioElementoBO> AnuncioElementoAnteriores = _repAnuncioElemento.GetBy(x => x.IdAnuncio == AnuncioRecibido.Id && x.Estado == true).ToList();
                            foreach (AnuncioElementoBO AnuncioElemento in AnuncioElementoAnteriores)
                            {
                                AnuncioElemento.Estado = false;
                                AnuncioElemento.FechaModificacion = DateTime.Now;
                                AnuncioElemento.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                            }
                            _repAnuncioElemento.Update(AnuncioElementoAnteriores);

                            ElementoRepositorio _repElemento = new ElementoRepositorio();

                            List<AnuncioElementoBO> AnuncioElementoNuevos = new List<AnuncioElementoBO>();
                            if (AnuncioRecibido.Kpis.Count > 0) AnuncioOrginal.Nombre += "~" ;
                            for (int k = 0; k < AnuncioRecibido.Kpis.Count; ++k)
                            {
                                AnuncioElementoNuevos.Add(new AnuncioElementoBO
                                {
                                    IdAnuncio = AnuncioRecibido.Id,
                                    IdElemento = AnuncioRecibido.Kpis[k],
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = ObjetoDTO.NombreUsuario,
                                    UsuarioModificacion = ObjetoDTO.NombreUsuario                                   
                                });

                                if (AnuncioRecibido.Kpis.Count == k + 1)
                                    AnuncioOrginal.Nombre += _repElemento.GetBy(x => x.Id == AnuncioRecibido.Kpis[k]).FirstOrDefault().Codigo;
                                else
                                    AnuncioOrginal.Nombre += (_repElemento.GetBy(x => x.Id == AnuncioRecibido.Kpis[k]).FirstOrDefault().Codigo + "-");
                            }

                            _repAnuncioElemento.Insert(AnuncioElementoNuevos);
                            AnunciosOriginales.Remove(AnuncioOrginal);
                            
                            AnunciosActualizar.Add(AnuncioOrginal);
                            break;
                        }
                    }

                    if (!AnuncioIncluido)
                    {
                        AnunciosAgregar.Add(new AnuncioBO()
                        {
                            IdConjuntoAnuncio = ConjuntoAnuncio.Id,
                            IdCentroCosto = (ObjetoDTO.EsGrupal.Value==false ? ObjetoDTO.IdCentroCosto.Value : AnuncioRecibido.IdCentroCosto),
                            IdCreativoPublicidad = AnuncioRecibido.IdCreativoPublicidad,
                            Nombre = AnuncioRecibido.Nombre,
                            EnlaceFormulario = AnuncioRecibido.EnlaceFormulario,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ObjetoDTO.NombreUsuario,
                            UsuarioModificacion = ObjetoDTO.NombreUsuario,
                            NroAnuncioCorrelativo = AnuncioRecibido.NroAnuncioCorrelativo
                        });

                        if (AnuncioRecibido.Kpis.Count == 0)
                            KpisAgregar.Add(new List<AnuncioElementoBO>());
                        else
                        {
                            List<AnuncioElementoBO> kpis = new List<AnuncioElementoBO>();
                            foreach (int IdElemento in AnuncioRecibido.Kpis)
                                kpis.Add(new AnuncioElementoBO() { 
                                    IdElemento = IdElemento,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = ObjetoDTO.NombreUsuario,
                                    UsuarioModificacion = ObjetoDTO.NombreUsuario
                                });
                            KpisAgregar.Add(kpis);
                        }
                    }   
                }

                foreach(AnuncioBO AnuncioEleminar in AnunciosOriginales)
                {
                    AnunciosEliminar.Add(new EliminarDTO()
                    {
                        Id = AnuncioEleminar.Id,
                        NombreUsuario = ObjetoDTO.NombreUsuario
                    }) ;
                }
                

                _repAnuncio.Update(AnunciosActualizar);

                for (int a = 0; a<AnunciosAgregar.Count; ++a)
                {
                    _repAnuncio.Insert(AnunciosAgregar[a]);

                    if (ConjuntoAnuncio.EsGrupal.Value)
                    {
                        PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                        PEspecificoCentroCostoDTO Dato = _repPEspecifico.ObtenerCentroCostoPorPEspecifico(AnunciosAgregar[a].IdCentroCosto).FirstOrDefault();
                        if (Dato == null)
                            return BadRequest("Error no se pudo identificar el Centro de costo para el Anuncio Id=" + AnunciosAgregar[a].Id);

                        Codigos.CentroCosto = Dato.Nombre;
                    }

                    AnuncioBO AnuncioGuardado = _repAnuncio.GetBy(x => x.Id == AnunciosAgregar[a].Id).FirstOrDefault();
                    AnuncioGuardado.Nombre =
                        Codigos.CentroCosto + "-" +
                        Codigos.Pais + "-" +
                        Codigos.Plantilla + "-" +
                        Codigos.CategoriaOrigen + "-" +
                        DateTime.Now.Year + "-" +
                        DateTime.Now.Month + "-" +
                        DateTime.Now.Day + "-" +
                        ConjuntoAnuncio.Id + "-" +
                        AnuncioGuardado.NroAnuncioCorrelativo + "-" +
                        Codigos.Segmento + "-" +
                        Codigos.Sexo + "-" +
                        ConjuntoAnuncio.Adicional;


                    ElementoRepositorio _repElemento = new ElementoRepositorio();
                    if (KpisAgregar[a].Count > 0) AnuncioGuardado.Nombre += "~";
                    for (int k=0; k < KpisAgregar[a].Count; ++k)
                    {
                        KpisAgregar[a][k].IdAnuncio = AnunciosAgregar[a].Id;

                        if (KpisAgregar[a].Count == k + 1)
                            AnuncioGuardado.Nombre += _repElemento.GetBy(x => x.Id == KpisAgregar[a][k].IdElemento).FirstOrDefault().Codigo;
                        else
                            AnuncioGuardado.Nombre += (_repElemento.GetBy(x => x.Id == KpisAgregar[a][k].IdElemento).FirstOrDefault().Codigo + "-");
                    }

                    _repAnuncio.Update(AnuncioGuardado);

                }
                foreach(List<AnuncioElementoBO> listaKpi in KpisAgregar)
                {
                    _repAnuncioElemento.Insert(listaKpi);
                }
                foreach (EliminarDTO Anuncio in AnunciosEliminar)
                {
                    List<AnuncioElementoBO> AnuncioElementos = _repAnuncioElemento.GetBy(x => x.IdAnuncio == Anuncio.Id && x.Estado == true).ToList();
                    foreach (AnuncioElementoBO AnuncioElemento in AnuncioElementos)
                    {
                        _repAnuncioElemento.Delete(AnuncioElemento.Id, Anuncio.NombreUsuario);
                    }
                    _repAnuncio.Delete(Anuncio.Id, Anuncio.NombreUsuario);
                }
                if (ObjetoDTO.IdConjuntoAnuncioTipoObjetivo == 2)
                {
                    ObjetoDTO.IdFormularioPlantilla = 12;
                }
                /***************** ACTUALIZACION SEGUN PLANTILLAS A F. SOLICITUD Y F LANDING PAGE *******************/
                FormularioPlantillaRepositorio _repFormularioPlantilla = new FormularioPlantillaRepositorio(_integraDBContext);
                FormularioPlantillaBO FormularioPlantilla = _repFormularioPlantilla.GetBy(x => x.Id == ObjetoDTO.IdFormularioPlantilla).FirstOrDefault();
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio();
                FormularioSolicitudBO FormularioSolicitudPlantilla = _repFormularioSolicitud.GetBy(x => x.Id == FormularioPlantilla.IdFormularioSolicitud.Value && x.Estado == true).FirstOrDefault();
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio();
                FormularioLandingPageBO FormularioLandigPagePlantilla = _repFormularioLandingPage.GetBy(x => x.Id == FormularioPlantilla.IdFormularioLandingPage.Value).FirstOrDefault();
                
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio();
               
                /* ELIMINACION F SOLICITUD Y F LANDING PAGE */
                if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1 || ConjuntoAnuncio.IdConjuntoAnuncioFuente == 3)
                {
                    var FormularioSolicitudEliminar = _repFormularioSolicitud.GetBy(x => x.IdConjuntoAnuncio == ConjuntoAnuncio.Id && x.Estado == true, x => new { x.Id }).Select(w => w.Id);
                    List<FormularioLandingPageBO> FormularioLandingPageEliminar = new List<FormularioLandingPageBO>();
                    foreach (int IdFormularioSolicitud in FormularioSolicitudEliminar)
                    {
                        List<FormularioLandingPageBO> FormulariosLandingPage = _repFormularioLandingPage.GetBy(x => x.IdFormularioSolicitud == IdFormularioSolicitud && x.Estado == true).ToList();
                        foreach (FormularioLandingPageBO FormularioLandingPage in FormulariosLandingPage)
                        {
                            FormularioLandingPageEliminar.Add(FormularioLandingPage);
                        }
                    }
                    
                        _repFormularioLandingPage.Delete(FormularioLandingPageEliminar.Select(x => x.Id), ObjetoDTO.NombreUsuario);
                        _repFormularioSolicitud.Delete(FormularioSolicitudEliminar, ObjetoDTO.NombreUsuario);
                }
                //foreach (FormularioLandingPageBO FormularioLandingPage in FormularioLandingPageEliminar)
                //{
                    // tmb bloquea el enlace creado, es mejor q el enlace persista, descomentar si se desea ese comportamiento
                    //using (TransactionScope scope = new TransactionScope())
                    //{
                    //    _repFormularioLandingPage.ActualizarFormularioPortal(FormularioLandingPage.Id, FormularioLandingPage.IdPlantillaLandingPage); 
                    //    scope.Complete();

                    //}
                //}

                /************** FORMULARIO SOLICITUD  *****************/  // solo insertar uno por ConjuntoAnuncio
                List<AnuncioBO> AnunciosActualizados = _repAnuncio.GetBy(x => x.IdConjuntoAnuncio == ConjuntoAnuncio.Id && x.Estado == true).ToList();
                var data = _repFormularioSolicitud.ObtenerConjuntoAnunciosFiltro(ConjuntoAnuncio.Id).FirstOrDefault();

                FormularioSolicitudBO NuevoFormularioSolicitud = new FormularioSolicitudBO();
                
                if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1 || ConjuntoAnuncio.IdConjuntoAnuncioFuente == 3)
                {
                    NuevoFormularioSolicitud.IdFormularioRespuesta = 223; // hardcodeo: en la creacion de anuncios de fb no afecta este campo (siempre eligen cualquier formulario)
                    if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1 || ConjuntoAnuncio.IdConjuntoAnuncioFuente == 3)
                    {
                        NuevoFormularioSolicitud.Nombre = data.Codigo;
                        NuevoFormularioSolicitud.Codigo = data.Codigo;
                    }
                    else
                    {
                        string dataAdw = "FS-";
                        NuevoFormularioSolicitud.Nombre = dataAdw + ConjuntoAnuncio.Nombre;
                        NuevoFormularioSolicitud.Codigo = dataAdw + ConjuntoAnuncio.Nombre;
                    }
                    NuevoFormularioSolicitud.Campanha = ConjuntoAnuncio.Nombre;
                    NuevoFormularioSolicitud.IdConjuntoAnuncio = ConjuntoAnuncio.Id;
                    NuevoFormularioSolicitud.Proveedor = data.NombreProveedor;
                    NuevoFormularioSolicitud.IdFormularioSolicitudTextoBoton = FormularioSolicitudPlantilla.IdFormularioSolicitudTextoBoton;
                    NuevoFormularioSolicitud.TipoSegmento = FormularioSolicitudPlantilla.TipoSegmento;
                    NuevoFormularioSolicitud.CodigoSegmento = FormularioSolicitudPlantilla.CodigoSegmento;
                    NuevoFormularioSolicitud.TipoEvento = FormularioSolicitudPlantilla.TipoEvento;
                    NuevoFormularioSolicitud.UrlbotonInvitacionPagina = FormularioSolicitudPlantilla.UrlbotonInvitacionPagina;
                    NuevoFormularioSolicitud.Estado = true;
                    NuevoFormularioSolicitud.FechaCreacion = DateTime.Now;
                    NuevoFormularioSolicitud.FechaModificacion = DateTime.Now;
                    NuevoFormularioSolicitud.UsuarioCreacion = ObjetoDTO.NombreUsuario;
                    NuevoFormularioSolicitud.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                    _repFormularioSolicitud.Insert(NuevoFormularioSolicitud);
                }
                else
                {
                    NuevoFormularioSolicitud = _repFormularioSolicitud.FirstBy(w => w.IdConjuntoAnuncio == ConjuntoAnuncio.Id);
                    NuevoFormularioSolicitud.IdFormularioRespuesta = 223; // hardcodeo: en la creacion de anuncios de fb no afecta este campo (siempre eligen cualquier formulario)
                    if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1 || ConjuntoAnuncio.IdConjuntoAnuncioFuente == 3)
                    {
                        NuevoFormularioSolicitud.Nombre = data.Codigo;
                        NuevoFormularioSolicitud.Codigo = data.Codigo;
                    }
                    else
                    {
                        string dataAdw = "FS-";
                        NuevoFormularioSolicitud.Nombre = dataAdw + ConjuntoAnuncio.Nombre;
                        NuevoFormularioSolicitud.Codigo = dataAdw + ConjuntoAnuncio.Nombre;
                    }
                    NuevoFormularioSolicitud.Campanha = ConjuntoAnuncio.Nombre;
                    NuevoFormularioSolicitud.IdConjuntoAnuncio = ConjuntoAnuncio.Id;
                    NuevoFormularioSolicitud.Proveedor = data.NombreProveedor;
                    NuevoFormularioSolicitud.IdFormularioSolicitudTextoBoton = FormularioSolicitudPlantilla.IdFormularioSolicitudTextoBoton;
                    NuevoFormularioSolicitud.TipoSegmento = FormularioSolicitudPlantilla.TipoSegmento;
                    NuevoFormularioSolicitud.CodigoSegmento = FormularioSolicitudPlantilla.CodigoSegmento;
                    NuevoFormularioSolicitud.TipoEvento = FormularioSolicitudPlantilla.TipoEvento;
                    NuevoFormularioSolicitud.UrlbotonInvitacionPagina = FormularioSolicitudPlantilla.UrlbotonInvitacionPagina;
                    NuevoFormularioSolicitud.Estado = true;
                    NuevoFormularioSolicitud.FechaCreacion = DateTime.Now;
                    NuevoFormularioSolicitud.FechaModificacion = DateTime.Now;
                    NuevoFormularioSolicitud.UsuarioCreacion = ObjetoDTO.NombreUsuario;
                    NuevoFormularioSolicitud.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                    _repFormularioSolicitud.Update(NuevoFormularioSolicitud);
                }
                List<CampoFormularioBO> CamposPlantilla = _repCampoFormulario.GetBy(w => w.IdFormularioSolicitud == FormularioSolicitudPlantilla.Id).ToList();
                List<CampoFormularioBO> camposnuevos = new List<CampoFormularioBO>();
                List<CampoFormularioBO> CamposAdwors = _repCampoFormulario.GetBy(w => w.IdFormularioSolicitud == NuevoFormularioSolicitud.Id).ToList();
                if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1 || ConjuntoAnuncio.IdConjuntoAnuncioFuente == 3)
                {
                    //CampoFormularioBO campos = new CampoFormularioBO();
                    foreach (var CampoFormulario in CamposPlantilla)
                    {
                        CampoFormularioBO campo = new CampoFormularioBO() {
                            IdFormularioSolicitud = NuevoFormularioSolicitud.Id,
                            IdCampoContacto = CampoFormulario.IdCampoContacto,
                            NroVisitas = CampoFormulario.NroVisitas,
                            Codigo = CampoFormulario.Codigo,
                            Campo = CampoFormulario.Campo,
                            Siempre = CampoFormulario.Siempre,
                            Inteligente = CampoFormulario.Inteligente,
                            Probabilidad = CampoFormulario.Probabilidad,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ObjetoDTO.NombreUsuario,
                            UsuarioModificacion = ObjetoDTO.NombreUsuario
                        };

                        camposnuevos.Add(campo);
                    }
                    _repCampoFormulario.Insert(camposnuevos);
                }
                else
                {                   
                    foreach (var CampoFormulario in CamposAdwors)
                    {                        
                        CampoFormulario.IdFormularioSolicitud = NuevoFormularioSolicitud.Id;
                        CampoFormulario.IdCampoContacto = CampoFormulario.IdCampoContacto;
                        CampoFormulario.NroVisitas = CampoFormulario.NroVisitas;
                        CampoFormulario.Codigo = CampoFormulario.Codigo;
                        CampoFormulario.Campo = CampoFormulario.Campo;
                        CampoFormulario.Siempre = CampoFormulario.Siempre;
                        CampoFormulario.Inteligente = CampoFormulario.Inteligente;
                        CampoFormulario.Probabilidad = CampoFormulario.Probabilidad;
                        CampoFormulario.Estado = true;                        
                        CampoFormulario.FechaModificacion = DateTime.Now;
                        CampoFormulario.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                    }
                    _repCampoFormulario.Update(CamposAdwors);
                }

                /*Formulario Landing Page*/
                if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1)
                {
                    foreach (AnuncioBO Anuncio in AnunciosActualizados) // crear un landing por Anuncio (Util cuando cada anuncio tiene diferente centro de costo)
                    {

                        AdicionalesPGeneralDTO adicionalesPGeneralDTO = new AdicionalesPGeneralDTO();
                        PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);

                        adicionalesPGeneralDTO.PGeneralNombreDescripcion = _repPEspecifico.ObtenerNombreDescripcion(Anuncio.IdCentroCosto);

                        FormularioLandingPageBO NuevoFormularioLandingPage = new FormularioLandingPageBO();

                        // no trabajar con valores null solo evaluar los son true o false  // si todo se maneja bien desde el modulo de creacion de FormularioPlantilla siempre entrara dentro del IF
                        if (FormularioLandigPagePlantilla.TituloProgramaAutomatico != null && FormularioLandigPagePlantilla.DescripcionWebAutomatico != null)
                        {

                            NuevoFormularioLandingPage.IdFormularioSolicitud = NuevoFormularioSolicitud.Id;
                            //NuevoFormularioLandingPage.Nombre = FormularioLandigPagePlantilla.Nombre;
                            //NuevoFormularioLandingPage.Codigo = FormularioLandigPagePlantilla.Codigo;
                            NuevoFormularioLandingPage.Header = FormularioLandigPagePlantilla.Header;
                            NuevoFormularioLandingPage.Footer = FormularioLandigPagePlantilla.Footer;
                            NuevoFormularioLandingPage.IdPlantillaLandingPage = FormularioLandigPagePlantilla.IdPlantillaLandingPage;
                            NuevoFormularioLandingPage.Mensaje = FormularioLandigPagePlantilla.Mensaje;
                            NuevoFormularioLandingPage.TextoPopup = FormularioLandigPagePlantilla.TextoPopup;
                            NuevoFormularioLandingPage.TituloPopup = FormularioLandigPagePlantilla.TituloPopup;
                            NuevoFormularioLandingPage.ColorPopup = FormularioLandigPagePlantilla.ColorPopup;
                            NuevoFormularioLandingPage.ColorTitulo = FormularioLandigPagePlantilla.ColorTitulo;
                            NuevoFormularioLandingPage.ColorTextoBoton = FormularioLandigPagePlantilla.ColorTextoBoton;
                            NuevoFormularioLandingPage.ColorFondoBoton = FormularioLandigPagePlantilla.ColorFondoBoton;
                            NuevoFormularioLandingPage.ColorDescripcion = FormularioLandigPagePlantilla.ColorDescripcion;
                            NuevoFormularioLandingPage.ColorFondoHeader = FormularioLandigPagePlantilla.ColorFondoHeader;
                            NuevoFormularioLandingPage.Tipo = FormularioLandigPagePlantilla.Tipo;
                            NuevoFormularioLandingPage.Cita1Texto = FormularioLandigPagePlantilla.Cita1Texto;
                            NuevoFormularioLandingPage.Cita1Color = FormularioLandigPagePlantilla.Cita1Color;
                            NuevoFormularioLandingPage.Cita3Texto = FormularioLandigPagePlantilla.Cita3Texto;
                            NuevoFormularioLandingPage.Cita3Color = FormularioLandigPagePlantilla.Cita3Color;
                            NuevoFormularioLandingPage.Cita4Texto = FormularioLandigPagePlantilla.Cita4Texto;
                            NuevoFormularioLandingPage.Cita4Color = FormularioLandigPagePlantilla.Cita4Color;
                            NuevoFormularioLandingPage.Cita1Despues = FormularioLandigPagePlantilla.Cita1Despues;
                            NuevoFormularioLandingPage.MuestraPrograma = FormularioLandigPagePlantilla.MuestraPrograma;
                            NuevoFormularioLandingPage.UrlImagenPrincipal = FormularioLandigPagePlantilla.UrlImagenPrincipal;
                            NuevoFormularioLandingPage.ColorPlaceHolder = FormularioLandigPagePlantilla.ColorPlaceHolder;
                            NuevoFormularioLandingPage.IdGmailClienteRemitente = FormularioLandigPagePlantilla.IdGmailClienteRemitente;
                            NuevoFormularioLandingPage.IdGmailClienteReceptor = FormularioLandigPagePlantilla.IdGmailClienteReceptor;
                            NuevoFormularioLandingPage.IdPlantilla = FormularioLandigPagePlantilla.IdPlantilla;
                            NuevoFormularioLandingPage.TesteoAb = FormularioLandigPagePlantilla.TesteoAb;
                            NuevoFormularioLandingPage.Estado = true;
                            NuevoFormularioLandingPage.UsuarioCreacion = ObjetoDTO.NombreUsuario;
                            NuevoFormularioLandingPage.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                            NuevoFormularioLandingPage.FechaCreacion = DateTime.Now;
                            NuevoFormularioLandingPage.FechaModificacion = DateTime.Now;

                            PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                            ListaPlantillaRepositorio _repListaPlantilla = new ListaPlantillaRepositorio();

                            var plantilla = _repPlantillaLandingPage.FirstById(NuevoFormularioLandingPage.IdPlantillaLandingPage);
                            var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);

                            if (listaplantilla != null && (listaplantilla.Nombre.Contains("PCFB") || listaplantilla.Nombre.Contains("PCMVFB"))) NuevoFormularioLandingPage.EstadoPopup = true;
                            else NuevoFormularioLandingPage.EstadoPopup = false;

                            if (FormularioLandigPagePlantilla.TituloProgramaAutomatico.Value)
                                if  (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                    NuevoFormularioLandingPage.TituloPopup = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Nombre ?? "");
                                else
                                    NuevoFormularioLandingPage.TituloPopup =  "";


                            if (FormularioLandigPagePlantilla.DescripcionWebAutomatico.Value)
                                if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                    NuevoFormularioLandingPage.Cita3Texto = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Descripcion ?? "");
                                else
                                    NuevoFormularioLandingPage.Cita3Texto = "";

                            NuevoFormularioLandingPage.IdPespecifico = Anuncio.IdCentroCosto;


                            int cantidad = _repFormularioLandingPage.GetBy(x => x.Codigo.Contains(Anuncio.Nombre)).Count();

                            string Codigo = "LP-";
                            if (cantidad >= 1) Codigo = Codigo + Anuncio.Nombre + cantidad;
                            else Codigo = Codigo + Anuncio.Nombre;
                            NuevoFormularioLandingPage.Codigo = Codigo;
                            NuevoFormularioLandingPage.Nombre = Codigo;

                            _repFormularioLandingPage.Insert(NuevoFormularioLandingPage);

                            AdicionalProgramaGeneralRepositorio _repAdicional = new AdicionalProgramaGeneralRepositorio(_integraDBContext);

                            if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                adicionalesPGeneralDTO.datosAdicionales = _repAdicional.ObtenerAdicionalProgramaPorIdPlantilla(NuevoFormularioLandingPage.IdPlantillaLandingPage, adicionalesPGeneralDTO.PGeneralNombreDescripcion.IdPGeneral);
                           

                            if (adicionalesPGeneralDTO.datosAdicionales != null && adicionalesPGeneralDTO.datosAdicionales.Count > 0)
                            {
                                List<DatoAdicionalPaginaBO> ListaDatosAdicionales = new List<DatoAdicionalPaginaBO>();
                                foreach (var item in adicionalesPGeneralDTO.datosAdicionales)
                                {
                                    ListaDatosAdicionales.Add(new DatoAdicionalPaginaBO()
                                    {
                                        IdFormularioLandingPage = NuevoFormularioLandingPage.Id,
                                        IdTitulo = item.IdTitulo,
                                        NombreTitulo = item.NombreTitulo,
                                        Descripcion = item.Descripcion,
                                        NombreImagen = item.NombreImagen,
                                        ColorTitulo = item.ColorTitulo,
                                        ColorDescripcion = item.ColorDescripcion,
                                        Estado = true,
                                        UsuarioCreacion = ObjetoDTO.NombreUsuario,
                                        UsuarioModificacion = ObjetoDTO.NombreUsuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now
                                    });
                                }
                                DatoAdicionalPaginaRepositorio _repDatoAdicionalPagina = new DatoAdicionalPaginaRepositorio(_integraDBContext);
                                _repDatoAdicionalPagina.Insert(ListaDatosAdicionales);
                            }
                        }
                        else
                        {
                            return BadRequest("La plantilla de Form. Landing Page tiene valores NULL no permitidos IdFormularioLandingPage:" + FormularioLandigPagePlantilla.Id);
                        }

                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (ObjetoDTO.IdConjuntoAnuncioTipoObjetivo != 2)
                            {
                                _repFormularioLandingPage.InsertarFormularioPortal(NuevoFormularioLandingPage.Id, ObjetoDTO.NombreUsuario, NuevoFormularioLandingPage.IdPlantillaLandingPage);
                                scope.Complete();
                            }
                        }
                    }

                } 
                else
                {
                    AdicionalesPGeneralDTO adicionalesPGeneralDTO = new AdicionalesPGeneralDTO();
                    PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);

                    adicionalesPGeneralDTO.PGeneralNombreDescripcion = _repPEspecifico.ObtenerNombreDescripcion(ConjuntoAnuncio.IdCentroCosto.Value);

                    FormularioLandingPageBO NuevoFormularioLandingPage = new FormularioLandingPageBO();

                    // no trabajar con valores null solo evaluar los son true o false  // si todo se maneja bien desde el modulo de creacion de FormularioPlantilla siempre entrara dentro del IF
                    if (FormularioLandigPagePlantilla.TituloProgramaAutomatico != null && FormularioLandigPagePlantilla.DescripcionWebAutomatico != null)
                    {                        
                        if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1 || ConjuntoAnuncio.IdConjuntoAnuncioFuente == 3)
                        {
                            NuevoFormularioLandingPage.IdFormularioSolicitud = NuevoFormularioSolicitud.Id;
                            //NuevoFormularioLandingPage.Nombre = FormularioLandigPagePlantilla.Nombre;
                            //NuevoFormularioLandingPage.Codigo = FormularioLandigPagePlantilla.Codigo;
                            NuevoFormularioLandingPage.Header = FormularioLandigPagePlantilla.Header;
                            NuevoFormularioLandingPage.Footer = FormularioLandigPagePlantilla.Footer;
                            NuevoFormularioLandingPage.IdPlantillaLandingPage = FormularioLandigPagePlantilla.IdPlantillaLandingPage;
                            NuevoFormularioLandingPage.Mensaje = FormularioLandigPagePlantilla.Mensaje;
                            NuevoFormularioLandingPage.TextoPopup = FormularioLandigPagePlantilla.TextoPopup;
                            NuevoFormularioLandingPage.TituloPopup = FormularioLandigPagePlantilla.TituloPopup;
                            NuevoFormularioLandingPage.ColorPopup = FormularioLandigPagePlantilla.ColorPopup;
                            NuevoFormularioLandingPage.ColorTitulo = FormularioLandigPagePlantilla.ColorTitulo;
                            NuevoFormularioLandingPage.ColorTextoBoton = FormularioLandigPagePlantilla.ColorTextoBoton;
                            NuevoFormularioLandingPage.ColorFondoBoton = FormularioLandigPagePlantilla.ColorFondoBoton;
                            NuevoFormularioLandingPage.ColorDescripcion = FormularioLandigPagePlantilla.ColorDescripcion;
                            NuevoFormularioLandingPage.ColorFondoHeader = FormularioLandigPagePlantilla.ColorFondoHeader;
                            NuevoFormularioLandingPage.Tipo = FormularioLandigPagePlantilla.Tipo;
                            NuevoFormularioLandingPage.Cita1Texto = FormularioLandigPagePlantilla.Cita1Texto;
                            NuevoFormularioLandingPage.Cita1Color = FormularioLandigPagePlantilla.Cita1Color;
                            NuevoFormularioLandingPage.Cita3Texto = FormularioLandigPagePlantilla.Cita3Texto;
                            NuevoFormularioLandingPage.Cita3Color = FormularioLandigPagePlantilla.Cita3Color;
                            NuevoFormularioLandingPage.Cita4Texto = FormularioLandigPagePlantilla.Cita4Texto;
                            NuevoFormularioLandingPage.Cita4Color = FormularioLandigPagePlantilla.Cita4Color;
                            NuevoFormularioLandingPage.Cita1Despues = FormularioLandigPagePlantilla.Cita1Despues;
                            NuevoFormularioLandingPage.MuestraPrograma = FormularioLandigPagePlantilla.MuestraPrograma;
                            NuevoFormularioLandingPage.UrlImagenPrincipal = FormularioLandigPagePlantilla.UrlImagenPrincipal;
                            NuevoFormularioLandingPage.ColorPlaceHolder = FormularioLandigPagePlantilla.ColorPlaceHolder;
                            NuevoFormularioLandingPage.IdGmailClienteRemitente = FormularioLandigPagePlantilla.IdGmailClienteRemitente;
                            NuevoFormularioLandingPage.IdGmailClienteReceptor = FormularioLandigPagePlantilla.IdGmailClienteReceptor;
                            NuevoFormularioLandingPage.IdPlantilla = FormularioLandigPagePlantilla.IdPlantilla;
                            NuevoFormularioLandingPage.TesteoAb = FormularioLandigPagePlantilla.TesteoAb;
                            NuevoFormularioLandingPage.Estado = true;
                            NuevoFormularioLandingPage.UsuarioCreacion = ObjetoDTO.NombreUsuario;
                            NuevoFormularioLandingPage.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                            NuevoFormularioLandingPage.FechaCreacion = DateTime.Now;
                            NuevoFormularioLandingPage.FechaModificacion = DateTime.Now;

                            PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                            ListaPlantillaRepositorio _repListaPlantilla = new ListaPlantillaRepositorio();

                            var plantilla = _repPlantillaLandingPage.FirstById(NuevoFormularioLandingPage.IdPlantillaLandingPage);
                            var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);

                            if (listaplantilla != null && (listaplantilla.Nombre.Contains("PCFB") || listaplantilla.Nombre.Contains("PCMVFB"))) NuevoFormularioLandingPage.EstadoPopup = true;
                            else NuevoFormularioLandingPage.EstadoPopup = false;

                            if (FormularioLandigPagePlantilla.TituloProgramaAutomatico.Value)
                                if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                    NuevoFormularioLandingPage.TituloPopup = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Nombre ?? "");
                                else
                                    NuevoFormularioLandingPage.TituloPopup = "";


                            if (FormularioLandigPagePlantilla.DescripcionWebAutomatico.Value)
                                if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                    NuevoFormularioLandingPage.Cita3Texto = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Descripcion ?? "");
                                else
                                    NuevoFormularioLandingPage.Cita3Texto = "";

                            NuevoFormularioLandingPage.IdPespecifico = ConjuntoAnuncio.IdCentroCosto.Value;


                            int cantidad = _repFormularioLandingPage.GetBy(x => x.Codigo.Contains(ConjuntoAnuncio.Nombre)).Count();

                            string Codigo = "LP-";
                            if (cantidad >= 1) Codigo = Codigo + ConjuntoAnuncio.Nombre + cantidad;
                            else Codigo = Codigo + ConjuntoAnuncio.Nombre;
                            NuevoFormularioLandingPage.Codigo = Codigo;
                            NuevoFormularioLandingPage.Nombre = Codigo;
                            _repFormularioLandingPage.Insert(NuevoFormularioLandingPage);
                        }
                        else
                        {
                            NuevoFormularioLandingPage = _repFormularioLandingPage.FirstBy(w => w.IdFormularioSolicitud == NuevoFormularioSolicitud.Id);
                            NuevoFormularioLandingPage.IdFormularioSolicitud = NuevoFormularioSolicitud.Id;
                            //NuevoFormularioLandingPage.Nombre = FormularioLandigPagePlantilla.Nombre;
                            //NuevoFormularioLandingPage.Codigo = FormularioLandigPagePlantilla.Codigo;
                            NuevoFormularioLandingPage.Header = FormularioLandigPagePlantilla.Header;
                            NuevoFormularioLandingPage.Footer = FormularioLandigPagePlantilla.Footer;
                            NuevoFormularioLandingPage.IdPlantillaLandingPage = FormularioLandigPagePlantilla.IdPlantillaLandingPage;
                            NuevoFormularioLandingPage.Mensaje = FormularioLandigPagePlantilla.Mensaje;
                            NuevoFormularioLandingPage.TextoPopup = FormularioLandigPagePlantilla.TextoPopup;
                            NuevoFormularioLandingPage.TituloPopup = FormularioLandigPagePlantilla.TituloPopup;
                            NuevoFormularioLandingPage.ColorPopup = FormularioLandigPagePlantilla.ColorPopup;
                            NuevoFormularioLandingPage.ColorTitulo = FormularioLandigPagePlantilla.ColorTitulo;
                            NuevoFormularioLandingPage.ColorTextoBoton = FormularioLandigPagePlantilla.ColorTextoBoton;
                            NuevoFormularioLandingPage.ColorFondoBoton = FormularioLandigPagePlantilla.ColorFondoBoton;
                            NuevoFormularioLandingPage.ColorDescripcion = FormularioLandigPagePlantilla.ColorDescripcion;
                            NuevoFormularioLandingPage.ColorFondoHeader = FormularioLandigPagePlantilla.ColorFondoHeader;
                            NuevoFormularioLandingPage.Tipo = FormularioLandigPagePlantilla.Tipo;
                            NuevoFormularioLandingPage.Cita1Texto = FormularioLandigPagePlantilla.Cita1Texto;
                            NuevoFormularioLandingPage.Cita1Color = FormularioLandigPagePlantilla.Cita1Color;
                            NuevoFormularioLandingPage.Cita3Texto = FormularioLandigPagePlantilla.Cita3Texto;
                            NuevoFormularioLandingPage.Cita3Color = FormularioLandigPagePlantilla.Cita3Color;
                            NuevoFormularioLandingPage.Cita4Texto = FormularioLandigPagePlantilla.Cita4Texto;
                            NuevoFormularioLandingPage.Cita4Color = FormularioLandigPagePlantilla.Cita4Color;
                            NuevoFormularioLandingPage.Cita1Despues = FormularioLandigPagePlantilla.Cita1Despues;
                            NuevoFormularioLandingPage.MuestraPrograma = FormularioLandigPagePlantilla.MuestraPrograma;
                            NuevoFormularioLandingPage.UrlImagenPrincipal = FormularioLandigPagePlantilla.UrlImagenPrincipal;
                            NuevoFormularioLandingPage.ColorPlaceHolder = FormularioLandigPagePlantilla.ColorPlaceHolder;
                            NuevoFormularioLandingPage.IdGmailClienteRemitente = FormularioLandigPagePlantilla.IdGmailClienteRemitente;
                            NuevoFormularioLandingPage.IdGmailClienteReceptor = FormularioLandigPagePlantilla.IdGmailClienteReceptor;
                            NuevoFormularioLandingPage.IdPlantilla = FormularioLandigPagePlantilla.IdPlantilla;
                            NuevoFormularioLandingPage.TesteoAb = FormularioLandigPagePlantilla.TesteoAb;
                            NuevoFormularioLandingPage.Estado = true;
                            //NuevoFormularioLandingPage.UsuarioCreacion = ObjetoDTO.NombreUsuario;
                            NuevoFormularioLandingPage.UsuarioModificacion = ObjetoDTO.NombreUsuario;
                            //NuevoFormularioLandingPage.FechaCreacion = DateTime.Now;
                            NuevoFormularioLandingPage.FechaModificacion = DateTime.Now;

                            PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                            ListaPlantillaRepositorio _repListaPlantilla = new ListaPlantillaRepositorio();

                            var plantilla = _repPlantillaLandingPage.FirstById(NuevoFormularioLandingPage.IdPlantillaLandingPage);
                            var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);

                            if (listaplantilla != null && (listaplantilla.Nombre.Contains("PCFB") || listaplantilla.Nombre.Contains("PCMVFB"))) NuevoFormularioLandingPage.EstadoPopup = true;
                            else NuevoFormularioLandingPage.EstadoPopup = false;

                            if (FormularioLandigPagePlantilla.TituloProgramaAutomatico.Value)
                                if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                    NuevoFormularioLandingPage.TituloPopup = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Nombre ?? "");
                                else
                                    NuevoFormularioLandingPage.TituloPopup = "";


                            if (FormularioLandigPagePlantilla.DescripcionWebAutomatico.Value)
                                if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                                    NuevoFormularioLandingPage.Cita3Texto = (adicionalesPGeneralDTO.PGeneralNombreDescripcion.Descripcion ?? "");
                                else
                                    NuevoFormularioLandingPage.Cita3Texto = "";

                            NuevoFormularioLandingPage.IdPespecifico = ConjuntoAnuncio.IdCentroCosto.Value;


                            int cantidad = _repFormularioLandingPage.GetBy(x => x.Codigo.Contains(ConjuntoAnuncio.Nombre)).Count();

                            string Codigo = "LP-";
                            if (cantidad >= 1) Codigo = Codigo + ConjuntoAnuncio.Nombre + cantidad;
                            else Codigo = Codigo + ConjuntoAnuncio.Nombre;
                            NuevoFormularioLandingPage.Codigo = Codigo;
                            NuevoFormularioLandingPage.Nombre = Codigo;
                            _repFormularioLandingPage.Update(NuevoFormularioLandingPage);
                        }
                        AdicionalProgramaGeneralRepositorio _repAdicional = new AdicionalProgramaGeneralRepositorio(_integraDBContext);
                        
                        if (adicionalesPGeneralDTO.PGeneralNombreDescripcion != null)
                            adicionalesPGeneralDTO.datosAdicionales = _repAdicional.ObtenerAdicionalProgramaPorIdPlantilla(NuevoFormularioLandingPage.IdPlantillaLandingPage, adicionalesPGeneralDTO.PGeneralNombreDescripcion.IdPGeneral);
                        

                        if (adicionalesPGeneralDTO.datosAdicionales != null && adicionalesPGeneralDTO.datosAdicionales.Count > 0)
                        {
                            List<DatoAdicionalPaginaBO> ListaDatosAdicionales = new List<DatoAdicionalPaginaBO>();
                            foreach (var item in adicionalesPGeneralDTO.datosAdicionales)
                            {
                                ListaDatosAdicionales.Add(new DatoAdicionalPaginaBO()
                                {
                                    IdFormularioLandingPage = NuevoFormularioLandingPage.Id,
                                    IdTitulo = item.IdTitulo,
                                    NombreTitulo = item.NombreTitulo,
                                    Descripcion = item.Descripcion,
                                    NombreImagen = item.NombreImagen,
                                    ColorTitulo = item.ColorTitulo,
                                    ColorDescripcion = item.ColorDescripcion,
                                    Estado = true,
                                    UsuarioCreacion = ObjetoDTO.NombreUsuario,
                                    UsuarioModificacion = ObjetoDTO.NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                });
                            }
                            DatoAdicionalPaginaRepositorio _repDatoAdicionalPagina = new DatoAdicionalPaginaRepositorio(_integraDBContext);
                            if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 1 || ConjuntoAnuncio.IdConjuntoAnuncioFuente == 3)
                            {
                                _repDatoAdicionalPagina.Insert(ListaDatosAdicionales);
                            }
                            else
                            {
                                _repDatoAdicionalPagina.Update(ListaDatosAdicionales);
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("La plantilla de Form. Landing Page tiene valores NULL no permitidos IdFormularioLandingPage:" + FormularioLandigPagePlantilla.Id);
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (ConjuntoAnuncio.IdConjuntoAnuncioFuente == 2) //Modulo Enlaces Adwors
                        {                            
                            var datoLandingPage = _repConjuntoAnuncio.obtenerDatoLandingPagePorIdConjuntoAnuncio(ObjetoDTO.Id);
                            if (datoLandingPage != null)
                            {
                                _repFormularioLandingPage.ActualizarFormularioPortal(datoLandingPage.IdFormularioLandingPage, NuevoFormularioLandingPage.IdPlantillaLandingPage);
                                //_repFormularioLandingPage.InsertarFormularioPortal(NuevoFormularioLandingPage.Id, ObjetoDTO.NombreUsuario, NuevoFormularioLandingPage.IdPlantillaLandingPage);
                            }
                            else
                            {
                                return BadRequest("No se encontro dato LandingPage a modificar");
                            }
                        }
                        else
                        {
                            _repFormularioLandingPage.InsertarFormularioPortal(NuevoFormularioLandingPage.Id, ObjetoDTO.NombreUsuario, NuevoFormularioLandingPage.IdPlantillaLandingPage);
                        }
                        scope.Complete();
                    }
                }




                /***************** FIN: ACTUALIZACION SEGUN PLANTILLAS A F. SOLICITUD Y F LANDING PAGE *******************/

                //ConjuntoAnuncioBO ConjuntoAnuncioDefinitivo = _repConjuntoAnuncio.GetBy(x => x.Id == ConjuntoAnuncio.Id).FirstOrDefault();
                //if (ConjuntoAnuncioDefinitivo.Nombre != null && !ConjuntoAnuncioDefinitivo.Nombre.Trim().Equals(""))
                //{
                //    ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                //    var RegistroEnlace = _repReportesRepositorio.ObtenerListaEnlacesLandingPage(ConjuntoAnuncioDefinitivo.Nombre).FirstOrDefault();

                //    if (RegistroEnlace != null)
                //    {
                //        ConjuntoAnuncioDefinitivo.EnlaceFormulario = RegistroEnlace.DireccionUrl;
                //        _repConjuntoAnuncio.Update(ConjuntoAnuncioDefinitivo);
                //    }

                //}


                return Ok(new { IdConjuntoAnuncio= ConjuntoAnuncio.Id});

                //return Ok(new { IdConjuntoAnuncio = ConjuntoAnuncioDefinitivo.Id, NombreConjuntoAnuncio= ConjuntoAnuncioDefinitivo.Nombre, EnlaceFormulario=ConjuntoAnuncioDefinitivo.EnlaceFormulario});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarConjuntoAnuncioGrupoAnuncioFacebook([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(_integraDBContext);
                AnuncioRepositorio _repAnuncio = new AnuncioRepositorio(_integraDBContext);
                AnuncioElementoRepositorio _repAnuncioElemento = new AnuncioElementoRepositorio(_integraDBContext);

                ConjuntoAnuncioBO conjuntoAnuncio = _repConjuntoAnuncio.GetBy(x=>x.Id == Eliminar.Id).FirstOrDefault();
                List<AnuncioBO> Anuncios = _repAnuncio.GetBy(x => x.IdConjuntoAnuncio == Eliminar.Id && x.Estado == true).ToList();

                foreach (AnuncioBO Anuncio in Anuncios)
                {
                    List<AnuncioElementoBO> AnuncioElementos = _repAnuncioElemento.GetBy(x => x.IdAnuncio == Anuncio.Id && x.Estado == true).ToList();
                    foreach(AnuncioElementoBO AnuncioElemento in AnuncioElementos)
                    {
                        _repAnuncioElemento.Delete(AnuncioElemento.Id, Eliminar.NombreUsuario);
                    }
                    _repAnuncio.Delete(Anuncio.Id, Eliminar.NombreUsuario);
                }



                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio();
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio();

                FormularioSolicitudBO FormularioSolicitud = _repFormularioSolicitud.GetBy(x => x.IdConjuntoAnuncio == conjuntoAnuncio.Id && x.Estado == true).FirstOrDefault();

                bool Eliminado = false;
                if (FormularioSolicitud != null)
                {
                    FormularioLandingPageBO formulario = _repFormularioLandingPage.GetBy(x => x.IdFormularioSolicitud == FormularioSolicitud.Id && x.Estado == true).FirstOrDefault();
                    if (formulario != null)
                        using (TransactionScope scope = new TransactionScope())
                        {
                            Eliminado = _repFormularioLandingPage.Delete(formulario.Id, Eliminar.NombreUsuario);
                            //_repFormularioLandingPage.ActualizarFormularioPortal(formulario.Id, formulario.IdPlantillaLandingPage); // tmb bloquea el enlace creado, es mejor q el enlace persista, descomentar si se desea ese comportamiento
                            scope.Complete();

                        }
                }
                _repConjuntoAnuncio.Delete(conjuntoAnuncio.Id, Eliminar.NombreUsuario);
                return Ok(Eliminado);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
