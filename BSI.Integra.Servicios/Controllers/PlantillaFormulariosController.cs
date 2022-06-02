using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PlantillaFormulariosController")]    
    public class PlantillaFormulariosController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly FormularioSolicitudTextoBotonRepositorio _repFormularioSolicitudTextoBoton;
        private readonly CampoContactoRepositorio _repCampoContacto;
        private readonly PlantillaLandingPageRepositorio _repPlantilla;
        public PlantillaFormulariosController (integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repFormularioSolicitudTextoBoton = new FormularioSolicitudTextoBotonRepositorio(_integraDBContext);
            _repCampoContacto = new CampoContactoRepositorio(_integraDBContext);
            _repPlantilla = new PlantillaLandingPageRepositorio(_integraDBContext);
        }


        
        [HttpPost]
        [Route ("[action]")]
        public ActionResult ObtenerCombosFormulario()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var textoBoton = _repFormularioSolicitudTextoBoton.ObtenerTextoBotonFiltro();
                var campoContacto = _repCampoContacto.ObtenerCamposContactoFiltro();
                var campoContactoTodo = _repCampoContacto.ObtenerCamposContacto();
                var plantillaPersonalizada = _repPlantilla.GetFiltroIdNombre();
                
                return Ok(new { textoBoton = textoBoton, campoContacto = campoContacto, 
                                campoContactoTodo= campoContactoTodo, plantillaPersonalizada=plantillaPersonalizada });

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarPlantillaFormulario([FromBody] InsertarPlantillaFormularioDTO obj)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio(_integraDBContext);
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio(_integraDBContext);
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio(_integraDBContext);
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio(_integraDBContext);
                FormularioPlantillaRepositorio _repFormularioPlantilla = new FormularioPlantillaRepositorio(_integraDBContext);

                var plantilla = _repPlantillaLandingPage.FirstById(obj.IdPlantillaLandingPage);
                //var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);
                FormularioPlantillaBO formularioPlantilla = new FormularioPlantillaBO();
                FormularioSolicitudBO formularioSolicitud = new FormularioSolicitudBO();
                FormularioLandingPageBO formularioLandingPageBO = new FormularioLandingPageBO();

                if (obj.TituloProgramaAutomatico == null) obj.TituloProgramaAutomatico = false;
                if (obj.DescripcionWebAutomatico == null) obj.TituloProgramaAutomatico = false;

                

                    formularioSolicitud.Nombre = "";
                    formularioSolicitud.Codigo = "";
                    formularioSolicitud.Campanha = "";
                    formularioSolicitud.Proveedor = "";
                    formularioSolicitud.IdFormularioSolicitudTextoBoton = obj.IdFormularioSolicitudTextoBoton;
                    formularioSolicitud.TipoSegmento = 0;
                    formularioSolicitud.CodigoSegmento = obj.CodigoSegmento;
                    formularioSolicitud.TipoEvento = obj.TipoEvento;
                    formularioSolicitud.CodigoSegmento = obj.CodigoSegmento;
                    formularioSolicitud.TipoEvento = obj.TipoEvento;
                    formularioSolicitud.Estado = true;
                    formularioSolicitud.UsuarioCreacion = obj.Usuario;
                    formularioSolicitud.UsuarioModificacion = obj.Usuario;
                    formularioSolicitud.FechaCreacion = DateTime.Now;
                    formularioSolicitud.FechaModificacion = DateTime.Now;
                    _repFormularioSolicitud.Insert(formularioSolicitud);

                    formularioLandingPageBO.IdFormularioSolicitud = formularioSolicitud.Id;
                    formularioLandingPageBO.IdPlantillaLandingPage = obj.IdPlantillaLandingPage;
                    formularioLandingPageBO.Nombre = "";
                    formularioLandingPageBO.Codigo = "";
                    formularioLandingPageBO.Header = 1;
                    formularioLandingPageBO.Footer = 0;
                    formularioLandingPageBO.Mensaje = obj.TituloPopup;                    
                    formularioLandingPageBO.TituloPopup = obj.TituloPopup;
                    formularioLandingPageBO.ColorPopup = plantilla.ColorPopup;
                    formularioLandingPageBO.ColorTitulo = plantilla.ColorTitulo;
                    formularioLandingPageBO.ColorTextoBoton = plantilla.ColorTextoBoton;
                    formularioLandingPageBO.ColorFondoBoton = plantilla.ColorFondoBoton;
                    formularioLandingPageBO.ColorDescripcion = plantilla.ColorDescripcion;
                    formularioLandingPageBO.EstadoPopup = true;

                    formularioLandingPageBO.ColorFondoHeader = plantilla.ColorFondoHeader;
                    formularioLandingPageBO.Tipo = "LPG";
                    formularioLandingPageBO.Cita1Texto = "";
                    formularioLandingPageBO.Cita1Color = plantilla.Cita1Color;
                    formularioLandingPageBO.Cita3Texto = obj.Cita3Texto;
                    formularioLandingPageBO.Cita3Color = plantilla.Cita3Color;
                    formularioLandingPageBO.Cita4Texto = obj.Cita4Texto;
                    formularioLandingPageBO.Cita4Color = plantilla.Cita4Color;
                    formularioLandingPageBO.Cita1Despues = "";
                    formularioLandingPageBO.MuestraPrograma = plantilla.MuestraPrograma;
                    formularioLandingPageBO.UrlImagenPrincipal = plantilla.UrlImagenPrincipal;
                    formularioLandingPageBO.ColorPlaceHolder = plantilla.ColorPlaceHolder;
                    formularioLandingPageBO.IdGmailClienteRemitente = 0;
                    formularioLandingPageBO.IdGmailClienteReceptor = 0;
                    formularioLandingPageBO.IdPlantilla = 0;
                    formularioLandingPageBO.TituloProgramaAutomatico = obj.TituloProgramaAutomatico.Value;
                    formularioLandingPageBO.DescripcionWebAutomatico = obj.DescripcionWebAutomatico.Value;
                    formularioLandingPageBO.Estado = true;
                    formularioLandingPageBO.UsuarioCreacion = obj.Usuario;
                    formularioLandingPageBO.UsuarioModificacion = obj.Usuario;
                    formularioLandingPageBO.FechaCreacion = DateTime.Now;
                    formularioLandingPageBO.FechaModificacion = DateTime.Now;
                    _repFormularioLandingPage.Insert(formularioLandingPageBO);

                    

                    formularioPlantilla.Nombre = obj.Nombre;
                    formularioPlantilla.IdFormularioSolicitud = formularioSolicitud.Id;
                    formularioPlantilla.IdFormularioLandingPage = formularioLandingPageBO.Id;
                    formularioPlantilla.Estado = true;
                    formularioPlantilla.UsuarioCreacion = obj.Usuario;
                    formularioPlantilla.FechaCreacion = DateTime.Now;
                    formularioPlantilla.UsuarioModificacion = obj.Usuario;
                    formularioPlantilla.FechaModificacion = DateTime.Now;

                    _repFormularioPlantilla.Insert(formularioPlantilla);

                    List<CampoFormularioBO> camposnuevos = new List<CampoFormularioBO>();

                    foreach (var item in obj.Campo)
                    {
                        CampoFormularioBO campo = new CampoFormularioBO()
                        {
                            IdFormularioSolicitud = formularioSolicitud.Id,
                            IdCampoContacto = item.Id.Value,
                            NroVisitas = item.NroVisitas,
                            Codigo = "LPG-PB",
                            Campo = item.Nombre,
                            Siempre = item.Siempre,
                            Inteligente = item.Inteligente,
                            Probabilidad = item.Probabilidad,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = obj.Usuario,
                            UsuarioModificacion = obj.Usuario,
                        };

                        camposnuevos.Add(campo);
                    }
                    _repCampoFormulario.Insert(camposnuevos);

                    
                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult ActualizarPlantillaFormulario([FromBody] InsertarPlantillaFormularioDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext _integraDBContext = new integraDBContext();
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio(_integraDBContext);
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio(_integraDBContext);
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio(_integraDBContext);
                FormularioPlantillaRepositorio _repFormularioPlantilla = new FormularioPlantillaRepositorio(_integraDBContext);

                //var listaplantilla = _repListaPlantilla.FirstById(plantilla.IdListaPlantilla ?? 0);
                FormularioPlantillaBO formularioPlantilla = _repFormularioPlantilla.GetBy(x => x.Id == obj.Id).FirstOrDefault();
                FormularioSolicitudBO formularioSolicitud = _repFormularioSolicitud.GetBy(x => x.Id== formularioPlantilla.IdFormularioSolicitud).FirstOrDefault();
                FormularioLandingPageBO formularioLandingPageBO = _repFormularioLandingPage.GetBy(x => x.Id == formularioPlantilla.IdFormularioLandingPage).FirstOrDefault();

                if(formularioPlantilla== null)
                    return BadRequest("Error FormularioPlantilla no encontrado: Id=" + obj.Id);

                if (formularioSolicitud == null)
                    return BadRequest("Error Plantilla de FormularioSolicitado no encontrado: IdFormularioSolicitud=" + formularioPlantilla.IdFormularioSolicitud);

                if (formularioLandingPageBO == null)
                    return BadRequest("Error Plantilla de FormularioLandingPage no encontrado: IdFormularioLandingPage=" + formularioPlantilla.IdFormularioLandingPage);


                if (obj.TituloProgramaAutomatico == null) obj.TituloProgramaAutomatico = false;
                if (obj.DescripcionWebAutomatico == null) obj.TituloProgramaAutomatico = false;

               
                    formularioSolicitud.IdFormularioSolicitudTextoBoton = obj.IdFormularioSolicitudTextoBoton;
                    formularioSolicitud.UsuarioModificacion = obj.Usuario;
                    formularioSolicitud.FechaModificacion = DateTime.Now;
                    _repFormularioSolicitud.Update(formularioSolicitud);

                    formularioLandingPageBO.IdPlantillaLandingPage = obj.IdPlantillaLandingPage;
                    formularioLandingPageBO.Mensaje = obj.TituloPopup;
                    formularioLandingPageBO.TituloPopup = obj.TituloPopup;

                    formularioLandingPageBO.Cita3Texto = obj.Cita3Texto;
                    formularioLandingPageBO.Cita4Texto = obj.Cita4Texto;
                    formularioLandingPageBO.TituloProgramaAutomatico = obj.TituloProgramaAutomatico.Value;
                    formularioLandingPageBO.DescripcionWebAutomatico = obj.DescripcionWebAutomatico.Value;
                    formularioLandingPageBO.UsuarioModificacion = obj.Usuario;
                    formularioLandingPageBO.FechaModificacion = DateTime.Now;
                    _repFormularioLandingPage.Update(formularioLandingPageBO);


                    formularioPlantilla.Nombre = obj.Nombre;
                    formularioPlantilla.UsuarioModificacion = obj.Usuario;
                    formularioPlantilla.FechaModificacion = DateTime.Now;

                    _repFormularioPlantilla.Update(formularioPlantilla);

                    var IdCampos = _repCampoFormulario.GetBy(w => w.Estado == true && w.IdFormularioSolicitud == formularioSolicitud.Id, w => new { w.Id }).Select(x => x.Id);
                    _repCampoFormulario.Delete(IdCampos, obj.Usuario);

                    List<CampoFormularioBO> camposnuevos = new List<CampoFormularioBO>();
                    foreach (var item in obj.Campo)
                    {
                        CampoFormularioBO campo = new CampoFormularioBO()
                        {
                            IdFormularioSolicitud = formularioSolicitud.Id,
                            IdCampoContacto = item.Id.Value,
                            NroVisitas = item.NroVisitas,
                            Codigo = "LPG-PB",
                            Campo = item.Nombre,
                            Siempre = item.Siempre,
                            Inteligente = item.Inteligente,
                            Probabilidad = item.Probabilidad,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = obj.Usuario,
                            UsuarioModificacion = obj.Usuario,
                        };

                        camposnuevos.Add(campo);
                    }
                    _repCampoFormulario.Insert(camposnuevos);

                  
                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult EliminarPlantillaFormulario([FromBody] EliminarDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext _integraDBContext = new integraDBContext();
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio(_integraDBContext);
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio(_integraDBContext);
                FormularioLandingPageRepositorio _repFormularioLandingPage = new FormularioLandingPageRepositorio(_integraDBContext);
                FormularioPlantillaRepositorio _repFormularioPlantilla = new FormularioPlantillaRepositorio(_integraDBContext);

                FormularioPlantillaBO formularioPlantilla = _repFormularioPlantilla.GetBy(x => x.Id == obj.Id).FirstOrDefault();
                FormularioSolicitudBO formularioSolicitud = _repFormularioSolicitud.GetBy(x => x.Id == formularioPlantilla.IdFormularioSolicitud).FirstOrDefault();
                FormularioLandingPageBO formularioLandingPageBO = _repFormularioLandingPage.GetBy(x => x.Id == formularioPlantilla.IdFormularioLandingPage).FirstOrDefault();

                if (formularioPlantilla == null)
                    return BadRequest("Error FormularioPlantilla no encontrado: Id=" + obj.Id);

                if (formularioSolicitud == null)
                    return BadRequest("Error Plantilla de FormularioSolicitado no encontrado: IdFormularioSolicitud=" + formularioPlantilla.IdFormularioSolicitud);

                if (formularioLandingPageBO == null)
                    return BadRequest("Error Plantilla de FormularioLandingPage no encontrado: IdFormularioLandingPage=" + formularioPlantilla.IdFormularioLandingPage);

                var IdCampos = _repCampoFormulario.GetBy(w => w.Estado == true && w.IdFormularioSolicitud == formularioSolicitud.Id, w => new { w.Id }).Select(x => x.Id);
                _repCampoFormulario.Delete(IdCampos, obj.NombreUsuario);

                _repFormularioLandingPage.Delete(formularioLandingPageBO.Id, obj.NombreUsuario);
                _repFormularioSolicitud.Delete(formularioSolicitud.Id, obj.NombreUsuario);
                _repFormularioPlantilla.Delete(formularioPlantilla.Id, obj.NombreUsuario);

                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFormularioPlantilla()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio();
                FormularioPlantillaRepositorio _repFormularioPlantilla = new FormularioPlantillaRepositorio();
                List<FormularioPlantillaDTO> FormulariosPlantillas= _repFormularioPlantilla.ObtenerFormularioPlantilla();

                foreach(FormularioPlantillaDTO FormularioPlantilla in FormulariosPlantillas)
                {
                    List<CampoFormularioBO> CampoFormulario = _repCampoFormulario.GetBy(x => x.IdFormularioSolicitud == FormularioPlantilla.IdFormularioSolicitud && x.Estado == true).ToList();
                    List<CampoFormularioDTO> Campos = new List<CampoFormularioDTO>();
                    foreach(CampoFormularioBO Item in CampoFormulario)
                    {
                        Campos.Add(new CampoFormularioDTO (){
                            IdFormularioSolicitud = Item.IdFormularioSolicitud,
                            IdCampoContacto = Item.IdCampoContacto,
                            NroVisitas = Item.NroVisitas,
                            Campo = Item.Campo,
                            Siempre = Item.Siempre,
                            Inteligente = Item.Inteligente,
                            Probabilidad = Item.Probabilidad,
                        });
                    }

                    FormularioPlantilla.Campos = Campos;
                }

                return Ok(FormulariosPlantillas.OrderByDescending(x => x.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
