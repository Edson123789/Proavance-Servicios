using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FormularioLandingPage")]
    [ApiController]
    public class FormularioLandingPageController : ControllerBase
    {

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                PlantillaLandingPageRepositorio _repPlantilla = new PlantillaLandingPageRepositorio();

                FormularioLandingPageCombosDTO formularioLandingPageCombos = new FormularioLandingPageCombosDTO();

                formularioLandingPageCombos.PlantillasLandingPage = _repPlantilla.GetFiltroIdNombre();

                return Ok(formularioLandingPageCombos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerFormularios([FromBody]FiltroPaginadorDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio();
                return Ok(_repFormularioLandingPage.ObtenerPagina(filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerFormularioSolicitudFiltro([FromBody]FiltroNombreDTO filtroNombre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioSolicitudRepositorio _repFormulario = new FormularioSolicitudRepositorio();
                if (filtroNombre.Nombre.Length >= 4) return Ok(_repFormulario.GetFiltroIdNombre(filtroNombre.Nombre));
                else return Ok(new { });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerCodigo(int Id)
        {
            try
            {
                FormularioSolicitudRepositorio _repFormulario = new FormularioSolicitudRepositorio();
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio();

                var formularioSolicitud = _repFormulario.FirstById(Id);
                var conjuntoAnuncio = _repConjuntoAnuncio.FirstById(formularioSolicitud.IdConjuntoAnuncio ?? 0);
                int cantidad = _repFormularioLandingPage.GetBy(x => x.Codigo.Contains(conjuntoAnuncio.Nombre)).Count();

                string result = "LP-";
                if (cantidad >= 1) result = result + conjuntoAnuncio.Nombre + cantidad;
                else result = result + conjuntoAnuncio.Nombre;

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPEspecifico([FromBody]FiltroNombreDTO filtroNombre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                if (filtroNombre.Nombre.Length >= 4) return Ok(_repPEspecifico.ObtenerPEspecificoPorCentroCosto(filtroNombre.Nombre));
                else return Ok(new { });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{IdFormulario}/{IdFormularioSolicitud}/{IdPespecifico}")]
        [HttpGet]
        public ActionResult ObtenerAdicionales(int IdFormulario, int IdFormularioSolicitud, int IdPespecifico)
        {
            try
            {
                DatoAdicionalPaginaRepositorio _repDatoAdicional = new DatoAdicionalPaginaRepositorio();
                FormularioSolicitudRepositorio _repFormulario = new FormularioSolicitudRepositorio();
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                AdicionalesFormularioLandingPageDTO _repAdicionales = new AdicionalesFormularioLandingPageDTO();

                _repAdicionales.FormularioSolicitudFiltro = _repFormulario.GetNombrePorId(IdFormularioSolicitud);
                _repAdicionales.PEspecificoCentroCosto = _repPEspecifico.ObtenerPEspecificoCentroCostoPorId(IdPespecifico);
                _repAdicionales.ListaDatoAdicional = _repDatoAdicional.ObtenerAdicionalesPorFormulario(IdFormulario);

                return Ok(_repAdicionales);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAdicionalesPGeneral([FromBody]ParametrosObtenerAdicionalesPGeneralDTO Parametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AdicionalesPGeneralDTO adicionalesPGeneralDTO = new AdicionalesPGeneralDTO();
                AdicionalProgramaGeneralRepositorio _repAdicional = new AdicionalProgramaGeneralRepositorio();
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();

                adicionalesPGeneralDTO.PGeneralNombreDescripcion = _repPEspecifico.ObtenerNombreDescripcion(Parametros.IdPEspecifico);
                if (Parametros.IdPlantilla != null)
                {
                    adicionalesPGeneralDTO.datosAdicionales = _repAdicional.ObtenerAdicionalProgramaPorIdPlantilla(Parametros.IdPlantilla ?? 0, adicionalesPGeneralDTO.PGeneralNombreDescripcion.IdPGeneral);
                    foreach (var item in adicionalesPGeneralDTO.datosAdicionales)
                    {
                        item.Id = 0;
                    }
                }

                return Ok(adicionalesPGeneralDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/")]
        [HttpPost]
        public ActionResult EliminarFormulario([FromBody]FormularioLandingPageDeleteDTO formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio();
                var formularioBO = _repFormularioLandingPage.FirstById(formulario.Id);
                bool eliminado = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    _repFormularioLandingPage.ActualizarFormularioPortal(formulario.Id, formularioBO.IdPlantillaLandingPage);
                    eliminado = _repFormularioLandingPage.Delete(formulario.Id, formulario.Usuario);
                    scope.Complete();

                }
                return Ok(eliminado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/")]
        [HttpPost]
        public ActionResult InsertarFormulario([FromBody] DatosFormularioLandingPageDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio();
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                ListaPlantillaRepositorio _repListaPlantilla = new ListaPlantillaRepositorio();

                var plantilla = _repPlantillaLandingPage.FirstById(Json.formularioLandingPage.IdPlantillaLandingPage);
                var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);

                FormularioLandingPageBO formularioLandingPageBO = new FormularioLandingPageBO();

                formularioLandingPageBO.IdFormularioSolicitud = Json.formularioLandingPage.IdFormularioSolicitud;
                formularioLandingPageBO.Nombre = Json.formularioLandingPage.Nombre;
                formularioLandingPageBO.Codigo = Json.formularioLandingPage.Codigo;
                formularioLandingPageBO.Header = 1;
                formularioLandingPageBO.Footer = 0;
                formularioLandingPageBO.IdPlantillaLandingPage = Json.formularioLandingPage.IdPlantillaLandingPage;
                formularioLandingPageBO.Mensaje = Json.formularioLandingPage.TituloPopup;
                formularioLandingPageBO.TextoPopup = Json.formularioLandingPage.TextoPopup;
                formularioLandingPageBO.TituloPopup = Json.formularioLandingPage.TituloPopup;
                formularioLandingPageBO.ColorPopup = plantilla.ColorPopup;
                formularioLandingPageBO.ColorTitulo = plantilla.ColorTitulo;
                formularioLandingPageBO.ColorTextoBoton = plantilla.ColorTextoBoton;
                formularioLandingPageBO.ColorFondoBoton = plantilla.ColorFondoBoton;
                formularioLandingPageBO.ColorDescripcion = plantilla.ColorDescripcion;

                formularioLandingPageBO.IdPespecifico = Json.formularioLandingPage.IdPespecifico;
                formularioLandingPageBO.ColorFondoHeader = plantilla.ColorFondoHeader;
                formularioLandingPageBO.Tipo = "LPG";
                formularioLandingPageBO.Cita1Texto = "";
                formularioLandingPageBO.Cita1Color = plantilla.Cita1Color;
                formularioLandingPageBO.Cita3Texto = Json.formularioLandingPage.Cita3Texto;
                formularioLandingPageBO.Cita3Color = plantilla.Cita3Color;
                formularioLandingPageBO.Cita4Texto = Json.formularioLandingPage.Cita4Texto;
                formularioLandingPageBO.Cita4Color = plantilla.Cita4Color;
                formularioLandingPageBO.Cita1Despues = "";
                formularioLandingPageBO.MuestraPrograma = plantilla.MuestraPrograma;
                formularioLandingPageBO.UrlImagenPrincipal = plantilla.UrlImagenPrincipal;
                formularioLandingPageBO.ColorPlaceHolder = plantilla.ColorPlaceHolder;
                formularioLandingPageBO.IdGmailClienteRemitente = 0;
                formularioLandingPageBO.IdGmailClienteReceptor = 0;
                formularioLandingPageBO.IdPlantilla = 0;
                formularioLandingPageBO.TesteoAb = Json.formularioLandingPage.TesteoAb;

                formularioLandingPageBO.Estado = true;
                formularioLandingPageBO.UsuarioCreacion = Json.Usuario;
                formularioLandingPageBO.UsuarioModificacion = Json.Usuario;
                formularioLandingPageBO.FechaCreacion = DateTime.Now;
                formularioLandingPageBO.FechaModificacion = DateTime.Now;

                if (listaplantilla != null && (listaplantilla.Nombre.Contains("PCFB") || listaplantilla.Nombre.Contains("PCMVFB"))) formularioLandingPageBO.EstadoPopup = true;
                else formularioLandingPageBO.EstadoPopup = false;

                formularioLandingPageBO.DatoAdicionalPagina = new List<DatoAdicionalPaginaBO>();
                foreach (var item in Json.datosAdicionales)
                {
                    DatoAdicionalPaginaBO adicional = new DatoAdicionalPaginaBO();
                    adicional.IdTitulo = item.IdTitulo;
                    adicional.NombreTitulo = item.NombreTitulo;
                    adicional.Descripcion = item.Descripcion;
                    adicional.NombreImagen = item.NombreImagen;
                    adicional.ColorTitulo = item.ColorTitulo;
                    adicional.ColorDescripcion = item.ColorDescripcion;
                    adicional.Estado = true;
                    adicional.UsuarioCreacion = Json.Usuario;
                    adicional.UsuarioModificacion = Json.Usuario;
                    adicional.FechaCreacion = DateTime.Now;
                    adicional.FechaModificacion = DateTime.Now;

                    formularioLandingPageBO.DatoAdicionalPagina.Add(adicional);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    _repFormularioLandingPage.Insert(formularioLandingPageBO);
                    _repFormularioLandingPage.InsertarFormularioPortal(formularioLandingPageBO.Id, Json.Usuario, formularioLandingPageBO.IdPlantillaLandingPage);
                    scope.Complete();
                }

                return Ok(new { IdInsertado = formularioLandingPageBO.Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/")]
        [HttpPost]
        public ActionResult ActualizarFormulario([FromBody] DatosFormularioLandingPageDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio(contexto);
                DatoAdicionalPaginaRepositorio _repDatoAdicional = new DatoAdicionalPaginaRepositorio(contexto);
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio(contexto);
                ListaPlantillaRepositorio _repListaPlantilla = new ListaPlantillaRepositorio(contexto);


                var plantilla = _repPlantillaLandingPage.FirstById(Json.formularioLandingPage.IdPlantillaLandingPage);
                var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);

                FormularioLandingPageBO formularioLandingPageBO = _repFormularioLandingPage.FirstById(Json.formularioLandingPage.Id);

                formularioLandingPageBO.IdFormularioSolicitud = Json.formularioLandingPage.IdFormularioSolicitud;
                formularioLandingPageBO.Nombre = Json.formularioLandingPage.Nombre;
                formularioLandingPageBO.Codigo = Json.formularioLandingPage.Codigo;
                formularioLandingPageBO.IdPlantillaLandingPage = Json.formularioLandingPage.IdPlantillaLandingPage;
                formularioLandingPageBO.Mensaje = Json.formularioLandingPage.TituloPopup;
                formularioLandingPageBO.TextoPopup = Json.formularioLandingPage.TextoPopup;
                formularioLandingPageBO.TituloPopup = Json.formularioLandingPage.TituloPopup;
                formularioLandingPageBO.ColorPopup = plantilla.ColorPopup;
                formularioLandingPageBO.ColorTitulo = plantilla.ColorTitulo;
                formularioLandingPageBO.ColorTextoBoton = plantilla.ColorTextoBoton;
                formularioLandingPageBO.ColorFondoBoton = plantilla.ColorFondoBoton;
                formularioLandingPageBO.ColorDescripcion = plantilla.ColorDescripcion;

                formularioLandingPageBO.IdPespecifico = Json.formularioLandingPage.IdPespecifico;
                formularioLandingPageBO.ColorFondoHeader = plantilla.ColorFondoHeader;
                formularioLandingPageBO.Cita1Color = plantilla.Cita1Color;
                formularioLandingPageBO.Cita3Texto = Json.formularioLandingPage.Cita3Texto;
                formularioLandingPageBO.Cita3Color = plantilla.Cita3Color;
                formularioLandingPageBO.Cita4Texto = Json.formularioLandingPage.Cita4Texto;
                formularioLandingPageBO.Cita4Color = plantilla.Cita4Color;
                formularioLandingPageBO.MuestraPrograma = plantilla.MuestraPrograma;
                formularioLandingPageBO.UrlImagenPrincipal = plantilla.UrlImagenPrincipal;
                formularioLandingPageBO.ColorPlaceHolder = plantilla.ColorPlaceHolder;
                formularioLandingPageBO.TesteoAb = Json.formularioLandingPage.TesteoAb;

                formularioLandingPageBO.UsuarioModificacion = Json.Usuario;
                formularioLandingPageBO.FechaModificacion = DateTime.Now;

                if (listaplantilla != null && (listaplantilla.Nombre.Contains("PCFB") || listaplantilla.Nombre.Contains("PCMVFB"))) formularioLandingPageBO.EstadoPopup = true;
                else formularioLandingPageBO.EstadoPopup = false;

                formularioLandingPageBO.DatoAdicionalPagina = new List<DatoAdicionalPaginaBO>();
                foreach (var item in Json.datosAdicionales)
                {
                    DatoAdicionalPaginaBO adicional;
                    if (_repDatoAdicional.Exist(item.Id ?? 0))
                    {
                        adicional = _repDatoAdicional.FirstById(item.Id ?? 0);
                        adicional.IdTitulo = item.IdTitulo;
                        adicional.NombreTitulo = item.NombreTitulo;
                        adicional.Descripcion = item.Descripcion;
                        adicional.NombreImagen = item.NombreImagen;
                        adicional.ColorTitulo = item.ColorTitulo;
                        adicional.ColorDescripcion = item.ColorDescripcion;
                        adicional.UsuarioModificacion = Json.Usuario;
                        adicional.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        adicional = new DatoAdicionalPaginaBO();
                        adicional.IdTitulo = item.IdTitulo;
                        adicional.NombreTitulo = item.NombreTitulo;
                        adicional.Descripcion = item.Descripcion;
                        adicional.NombreImagen = item.NombreImagen;
                        adicional.ColorTitulo = item.ColorTitulo;
                        adicional.ColorDescripcion = item.ColorDescripcion;
                        adicional.Estado = true;
                        adicional.UsuarioCreacion = Json.Usuario;
                        adicional.UsuarioModificacion = Json.Usuario;
                        adicional.FechaCreacion = DateTime.Now;
                        adicional.FechaModificacion = DateTime.Now;
                    }

                    formularioLandingPageBO.DatoAdicionalPagina.Add(adicional);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    _repDatoAdicional.DeleteLogicoPorPrograma(Json.formularioLandingPage.Id, Json.Usuario, Json.datosAdicionales);
                    _repFormularioLandingPage.Update(formularioLandingPageBO);
                    _repFormularioLandingPage.ActualizarFormularioPortal(formularioLandingPageBO.Id, formularioLandingPageBO.IdPlantillaLandingPage);
                    scope.Complete();

                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerVistaPreviaPlantilla([FromBody]DatosFormularioLandingPageDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioLandingPageBO formularioLandingPageBO = new FormularioLandingPageBO();
                string vistaPrevia = formularioLandingPageBO.ObtenerVistaPreviaPlantilla(Json.formularioLandingPage, Json.datosAdicionales);
                return Ok(vistaPrevia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]/{IdFormularioLandingPage}")]
        [HttpGet]
        public ActionResult ObtenerTesteoAB(int IdFormularioLandingPage)
        {
            try
            {
                TesteoAbRepositorio testeoAbRepositorio = new TesteoAbRepositorio();

                return Ok(testeoAbRepositorio.ObtenerPorIdFormularioLandingPage(IdFormularioLandingPage));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdTesteoAB}")]
        [HttpGet]
        public ActionResult ObtenerFormularioLandingAB(int IdTesteoAB)
        {
            try
            {
                FormularioLandingAbRepositorio formularioLandingAbRepositorio = new FormularioLandingAbRepositorio();
                SeccionFormularioAbRepositorio seccionFormularioAbRepositorio = new SeccionFormularioAbRepositorio();

                var formularioLandingAB = formularioLandingAbRepositorio.ObtenerPorIdTesteoAB(IdTesteoAB);
                formularioLandingAB.ListaSeccionFormularioAB = seccionFormularioAbRepositorio.ObtenerPorIdFormularioLandingAB(formularioLandingAB.Id ?? 0);

                return Ok(formularioLandingAB);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarTesteoAB([FromBody]TesteoAbDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TesteoAbRepositorio testeoAbRepositorio = new TesteoAbRepositorio();

                TesteoAbBO testeoAbBO = new TesteoAbBO();
                testeoAbBO.IdFormularioLandingPage = Json.IdFormularioLandingPage;
                testeoAbBO.IdPlantillaLandingPage = Json.IdPlantillaLandingPage;
                testeoAbBO.NombrePlantilla = Json.NombrePlantilla;
                testeoAbBO.Cantidad = Json.Cantidad;
                testeoAbBO.Nombre = Json.Nombre;
                testeoAbBO.Porcentaje = Json.Porcentaje;
                testeoAbBO.Estado = true;
                testeoAbBO.UsuarioCreacion = Json.Usuario;
                testeoAbBO.UsuarioModificacion = Json.Usuario;
                testeoAbBO.FechaCreacion = DateTime.Now;
                testeoAbBO.FechaModificacion = DateTime.Now;

                if (testeoAbBO.HasErrors) return BadRequest(testeoAbBO.ActualesErrores);

                FormularioLandingAbBO formularioLandingAbBO = new FormularioLandingAbBO();
                formularioLandingAbBO.TextoFormulario = Json.FormularioLandingAb.TextoFormulario;
                formularioLandingAbBO.NombrePrograma = Json.FormularioLandingAb.NombrePrograma;
                formularioLandingAbBO.Descripcion = Json.FormularioLandingAb.Descripcion;
                formularioLandingAbBO.Estado = true;
                formularioLandingAbBO.UsuarioCreacion = Json.Usuario;
                formularioLandingAbBO.UsuarioModificacion = Json.Usuario;
                formularioLandingAbBO.FechaCreacion = DateTime.Now;
                formularioLandingAbBO.FechaModificacion = DateTime.Now;

                if (formularioLandingAbBO.HasErrors) return BadRequest(formularioLandingAbBO.ActualesErrores);
                else testeoAbBO.FormularioLandingAb = formularioLandingAbBO;

                formularioLandingAbBO.ListaSeccionFormularioAbBO = new List<SeccionFormularioAbBO>();
                SeccionFormularioAbBO seccionFormularioAbBO;
                foreach (var item in Json.FormularioLandingAb.ListaSeccionFormularioAB)
                {
                    seccionFormularioAbBO = new SeccionFormularioAbBO();
                    seccionFormularioAbBO.NombreTitulo = item.NombreTitulo;
                    seccionFormularioAbBO.Descripcion = item.Descripcion;
                    seccionFormularioAbBO.Imagen = item.NombreImagen;
                    seccionFormularioAbBO.ColorTitulo = item.ColorTitulo;
                    seccionFormularioAbBO.ColorDescripcion = item.ColorDescripcion;
                    seccionFormularioAbBO.Estado = true;
                    seccionFormularioAbBO.UsuarioCreacion = Json.Usuario;
                    seccionFormularioAbBO.UsuarioModificacion = Json.Usuario;
                    seccionFormularioAbBO.FechaCreacion = DateTime.Now;
                    seccionFormularioAbBO.FechaModificacion = DateTime.Now;

                    if (seccionFormularioAbBO.HasErrors) return BadRequest(seccionFormularioAbBO.ActualesErrores);
                    else formularioLandingAbBO.ListaSeccionFormularioAbBO.Add(seccionFormularioAbBO);
                }

                testeoAbRepositorio.Insert(testeoAbBO);

                return Ok(testeoAbBO.Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarTesteoAB([FromBody] EliminarDTO Json)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                TesteoAbRepositorio testeoAbRepositorio = new TesteoAbRepositorio(contexto);
                FormularioLandingAbRepositorio formularioLandingAbRepositorio = new FormularioLandingAbRepositorio(contexto);
                SeccionFormularioAbRepositorio seccionFormularioAbRepositorio = new SeccionFormularioAbRepositorio(contexto);

                using (TransactionScope scope = new TransactionScope())
                {
                    testeoAbRepositorio.Delete(Json.Id, Json.NombreUsuario);

                    int idFormularioLandingAB = formularioLandingAbRepositorio.FirstBy(x => x.IdTesteoAb == Json.Id).Id;
                    formularioLandingAbRepositorio.Delete(idFormularioLandingAB, Json.NombreUsuario);

                    List<SeccionFormularioAbBO> listaSeccionFormularioAB = seccionFormularioAbRepositorio.GetBy(x => x.IdFormularioLandingAb == idFormularioLandingAB).ToList();
                    foreach (var item in listaSeccionFormularioAB)
                    {
                        seccionFormularioAbRepositorio.Delete(item.Id, Json.NombreUsuario);
                    }

                    scope.Complete();
                }
                return Ok(Json.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarFormularioLandingAB([FromBody]FormularioLandingAbDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                FormularioLandingAbRepositorio formularioLandingAbRepositorio = new FormularioLandingAbRepositorio(contexto);
                SeccionFormularioAbRepositorio seccionFormularioAbRepositorio = new SeccionFormularioAbRepositorio(contexto);

                using (TransactionScope scope = new TransactionScope())
                {
                    seccionFormularioAbRepositorio.DeleteLogicoPorIdFormularioLanding(Json.Id ?? 0, Json.Usuario, Json.ListaSeccionFormularioAB);

                    FormularioLandingAbBO formularioLandingAbBO = formularioLandingAbRepositorio.FirstById(Json.Id ?? 0);
                    formularioLandingAbBO.TextoFormulario = Json.TextoFormulario;
                    formularioLandingAbBO.NombrePrograma = Json.NombrePrograma;
                    formularioLandingAbBO.Descripcion = Json.Descripcion;
                    formularioLandingAbBO.UsuarioModificacion = Json.Usuario;
                    formularioLandingAbBO.FechaModificacion = DateTime.Now;

                    if (formularioLandingAbBO.HasErrors) return BadRequest(formularioLandingAbBO.ActualesErrores);

                    formularioLandingAbBO.ListaSeccionFormularioAbBO = new List<SeccionFormularioAbBO>();
                    SeccionFormularioAbBO seccionFormularioAbBO;
                    foreach (var item in Json.ListaSeccionFormularioAB)
                    {
                        seccionFormularioAbBO = seccionFormularioAbRepositorio.FirstById(item.Id ?? 0);
                        seccionFormularioAbBO.NombreTitulo = item.NombreTitulo;
                        seccionFormularioAbBO.Descripcion = item.Descripcion;
                        seccionFormularioAbBO.Imagen = item.NombreImagen;
                        seccionFormularioAbBO.ColorTitulo = item.ColorTitulo;
                        seccionFormularioAbBO.ColorDescripcion = item.ColorDescripcion;
                        seccionFormularioAbBO.UsuarioModificacion = Json.Usuario;
                        seccionFormularioAbBO.FechaModificacion = DateTime.Now;

                        if (seccionFormularioAbBO.HasErrors) return BadRequest(seccionFormularioAbBO.ActualesErrores);
                        else formularioLandingAbBO.ListaSeccionFormularioAbBO.Add(seccionFormularioAbBO);
                    }

                    formularioLandingAbRepositorio.Update(formularioLandingAbBO);

                    scope.Complete();
                }

                return Ok(Json.Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}