using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.DTOs;
using FluentValidation;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using Newtonsoft.Json;
using System.Net;
using BSI.Integra.Aplicacion.Servicios.SCode.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Servicios/MessengerChat
    /// Autor: Fischer Valdez - Joao Benavente - Carlos Crispin
    /// Fecha: 03/02/2021
    /// <summary>
    /// Gestiona todo lo referente al chat de Messenger
    /// </summary>
    [Route("api/MessengerChat")]
    public class MessengerChatController : BaseController<TMessengerChat, ValidadorMessengerChatDTO>
    {

        private string connectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
        private string containerName = "repositorioweb/Messenger/Enviados";

        public MessengerChatController(IIntegraRepository<TMessengerChat> repositorio, ILogger<BaseController<TMessengerChat, ValidadorMessengerChatDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {

        }
        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerMessengerChatPorPersonal(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MessengerChatRepositorio _repMessenger = new MessengerChatRepositorio();
                var resp = _repMessenger.ObtenerMessengerChatPorPersonal(IdPersonal);
                return Ok(new { data =  resp, Total = resp.Count()});

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
		[Route("[Action]/{IdUsuario}")]
        [HttpGet]
        
        public ActionResult ObtenerMessengerChatDetallePorUsuario(int IdUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MessengerChatRepositorio _repMessengerDetalle = new MessengerChatRepositorio();
                var resp = _repMessengerDetalle.ObtenerMessengerChatDetallePorUsuario(IdUsuario);
                return Ok(new { data = resp, Total = resp.Count() });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerPostComentarioPorPersonal(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var mensajeInicio = "Buen día, para que recibas mayor detalle escríbenos al siguiente enlace: <a href='https://m.me/BSG.Institute.Oficial'>https://m.me/BSG.Institute.Oficial</a> y un asesor especializado se contactará para darte mas detalles.<br/>";
                PostComentarioUsuarioRepositorio _repMessenger = new PostComentarioUsuarioRepositorio();
                var resp = _repMessenger.ObtenerPostComentarioUsuarioPorPersonal(IdPersonal);
                return Ok(new { data = resp, Total = resp.Count(), MensajeInicio = mensajeInicio });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdUsuario}/{IdPostFacebook}")]
        [HttpGet]
        public ActionResult ObtenerPostComentarioDetallePorUsuarioPost(string IdUsuario, string IdPostFacebook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                PostComentarioDetalleRepositorio _repComentarioDetalle = new PostComentarioDetalleRepositorio();
                
                var resp = _repComentarioDetalle.ObtenerPostComentarioUsuarioPorPersonal(IdUsuario, IdPostFacebook);
                
                return Ok(new { data = resp, Last = resp.Last(a => a.Tipo == true)});

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerChatComentarioPorPersonal(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MessengerChatRepositorio _chatRepositorio = new MessengerChatRepositorio();
                PostComentarioUsuarioRepositorio _comentarioRepositorio = new PostComentarioUsuarioRepositorio();

                ChatComentarioPersonalDTO chatComentario = new ChatComentarioPersonalDTO
                {
                    ListaChatMessenger = _chatRepositorio.ObtenerMessengerChatPorPersonal(IdPersonal),
                    ListaComentarioMessenger = _comentarioRepositorio.ObtenerPostComentarioUsuarioPorPersonal(IdPersonal)
                };


                return Ok(chatComentario);
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        
        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerChatMessenger(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MessengerChatRepositorio _chatRepositorio = new MessengerChatRepositorio();

                //var Chats = _chatRepositorio.ObtenerMessengerChatPorPersonal(IdPersonal);
                var ListaChatMessengerEnviado = _chatRepositorio.ObtenerMessengerChatEnviadoPorPersonal(IdPersonal);
                var ListaChatMessenger = _chatRepositorio.ObtenerMessengerChatRecibidosPorPersonal(IdPersonal);

                return Ok(new { ListaChatMessenger, ListaChatMessengerEnviado });
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        
        [Route("[Action]/{IdPersonal}/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerHistorialChatPorPersonal(int IdPersonal, int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MessengerChatRepositorio _chatRepositorio = new MessengerChatRepositorio();
                return Ok(_chatRepositorio.ObtenerHistorialMessengerChatPorPersonal(IdPersonal, IdAlumno));
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }     
        

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDatosFiltroContactoOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nombreOrigen = "LANPERFBK-INB-PV";
                var nombreCategoriaOrigen = "Facebook Inbox";
                PaisRepositorio _repPais = new PaisRepositorio();
                CiudadRepositorio _repCiudad = new CiudadRepositorio();
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                CargoRepositorio _repCargo = new CargoRepositorio();
                AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio();
                AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio();
                IndustriaRepositorio _repIndustria = new IndustriaRepositorio();
				OrigenRepositorio _repOrigen = new OrigenRepositorio();

                var CategoriaOrigenFacebookInbox = _repCategoriaOrigen.FirstBy(x => x.Nombre == "Facebook Inbox");
                var CategoriaOrigenFacebookCorreo = _repCategoriaOrigen.FirstBy(x => x.Nombre == "Facebook Correo");
                var CategoriaOrigenFacebookComentarios = _repCategoriaOrigen.FirstBy(x => x.Nombre == "Facebook Comentarios");

                ContactoOportunidadDTO ContactoOportunidad = new ContactoOportunidadDTO
				{
					Paises = _repPais.ObtenerTodoFiltro(),
					Ciudades = _repCiudad.ObtenerCiudadesPorPais(),
					TipoDatoChats = _repTipoDato.CargarTipoDatoChat(),
					FaseOportunidades = _repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro(),
					CategoriaOrigenes = _repCategoriaOrigen.ObtenerCategoriaOrigenPorNombre(nombreCategoriaOrigen),
					Cargos = _repCargo.ObtenerCargoFiltro(),
					AreasFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro(),
					AreasTrabajo = _repAreaTrabajo.ObtenerTodoAreaTrabajoFiltro(),
					Industrias = _repIndustria.ObtenerIndustriaFiltro(),
					Origenes = _repOrigen.ObtenerOrigenPorCategoriaOrigen(CategoriaOrigenFacebookInbox != null ? CategoriaOrigenFacebookInbox.Id : 0, CategoriaOrigenFacebookCorreo != null ? CategoriaOrigenFacebookCorreo.Id : 0, CategoriaOrigenFacebookComentarios  != null ? CategoriaOrigenFacebookComentarios.Id : 0)
                };

                return Ok(ContactoOportunidad);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerTodoFiltroAlumno([FromBody] Dictionary<string, string> Filtros)
        {
            
                try
                {
                    if (Filtros != null)
                    {

                        AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                        var Alumno = _repAlumno.ObtenerTodoFiltroAutoComplete(Filtros["valor"]);
                        return Ok(Alumno);
                    }
                    else
                    {
                        return Ok();
                    }
                }
                catch (Exception Ex)
                {
                    return BadRequest(Ex.Message);
                }
           
        }

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerTodoFiltroIdReferido([FromBody] Dictionary<string, string> Filtros)
		{

			try
			{
				if (Filtros != null)
				{

					AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
					var Alumno = _repAlumno.ObtenerTodoFiltroAutoCompleteReferido(int.Parse(Filtros["valor"]));
					return Ok(Alumno);
				}
				else
				{
					return Ok();
				}
			}
			catch (Exception Ex)
			{
				return BadRequest(Ex.Message);
			}

		}

		[Route("[Action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerAlumnosMessengerPorId(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                var Alumno = _repAlumno.ObtenerAlumnoInformacionMessengerChatPorId(IdAlumno);
                return Ok(Alumno);
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerUsuarioPorIdAsesor(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio();
                var usuario = _repMessengerUsuario.ObtenerFacebookUsuarioPorAsesor(IdPersonal);
                return Ok(usuario);
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerMessengerPorIdAsesor(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio();
                var usuario = messengerChatRepositorio.ObtenerMessengerChatPorPersonal(IdPersonal);
                return Ok(usuario);
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerDatosAlumnoPorEmail([FromBody]Dictionary<string, string> Filtros)
		{
			try
			{
				if (Filtros != null)
				{

					AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
					var Alumno = _repAlumno.alumnnosTodoFiltroAutoCompletePorEmail(Filtros["valor"]);
					return Ok(Alumno);
				}
				else
				{
					return Ok();
				}
			}
			catch (Exception Ex)
			{
				return BadRequest(Ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ValidarAlumnoExiste([FromBody]AlumnoFiltroAutocompleteEmailDTO Filtros)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
				var res = _repAlumno.ExisteContacto(Filtros.Email1, Filtros.Email2, Filtros.Id);
                int id = 0;
                if(res) id = _repAlumno.FirstBy(x => x.Email1 == Filtros.Email1).Id;
                
                return Ok(id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult InsertarComentarioInicio([FromBody] MensajeInicioDTO [] Objeto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var rpta = new List<ComentarioInicioDTO>();
				foreach (var item in Objeto)
				{
					bool enviado = EnviarFacebook(item);

					var obj = new ComentarioInicioDTO();
					obj.rpta = enviado;
					obj.usuario_id = item.usuario_id;
					rpta.Add(obj);
				}
				return Ok(new { Result = "OK", Records = rpta });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

        /// <summary>
        /// Envia a Facebook el comentario adjunto
        /// </summary>
        /// <param name="objeto">Objeto con el mensaje de inicio</param>
        /// <returns>Objeto de clase MensajeInicioDTO</returns>
        private bool EnviarFacebook(MensajeInicioDTO objeto)
		{
			//var user = System.Web.HttpContext.Current.User.Identity.Name;
			if (objeto == null)
			{
				return false;
			}
			var currentNodeComment = string.Empty;
			if (objeto.post_id == objeto.parent_id)
				currentNodeComment = objeto.comment_id;
			else
				currentNodeComment = objeto.parent_id;

			var comment = "@[" + objeto.usuario_id + "] Buen día, para que recibas mayor detalle escríbenos al siguiente enlace: https://m.me/BSG.Institute.Oficial y un asesor especializado se contactará para darte mas detalles. ";
            objeto.comentario = comment;
			//Objeto.user = user;
			//Access token
			string accessToken = "EAAEdE2PM6o8BAC5foqPInLHHm3OMQJprO6LqPzfPzGm3d11NMv6luHPAqRkklfEYZB2nn2gUp3UZCqCuUQhGdHrm6dQBS6BnZALSIJFeNigjjU3CZAvalM4QN95UfQQdQhEksk0nFpCeZAhiUsARVZBBK6CE5waG7hFrNZBAQuoswZDZD";
			string rpta2;
			RespuestaComentarioDTO response2;
			PostComentarioUsuarioRepositorio _repPostComentarioUsuario = new PostComentarioUsuarioRepositorio();
			using (WebClient wc = new WebClient())
			{
				wc.Encoding = System.Text.Encoding.UTF8;
				var obj = new { message = @comment };
				//rpta = wc.DownloadString(url);//method: GET
				var dataString = JsonConvert.SerializeObject(obj);
				wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
				try
				{
					var url = @"https://graph.facebook.com/v12.0/" + currentNodeComment + "/comments?access_token=" + accessToken;
					rpta2 = wc.UploadString(new Uri(url), "POST", dataString);
					response2 = JsonConvert.DeserializeObject<RespuestaComentarioDTO>(@rpta2);

					IList<PostComentarioDTO> listRpta = _repPostComentarioUsuario.GuardarMensajeInicio(objeto, response2.id);

					return true;
				}
				catch (Exception ex)
				{
					return false;
				}
			}
		}

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenterPlantillaMessenger()
        {
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
                var PlantillaMessenger = _repPlantillaClaveValor.ObtenerPlantillaPorPlantillaBase("Facebook - Messenger").OrderBy(w => w.Nombre);
                var PlantillaComentarios = _repPlantillaClaveValor.ObtenerPlantillaPorPlantillaBase("Facebook - Comentarios").OrderBy(w => w.Nombre);

                return Ok(new { PlantillaMessenger, PlantillaComentarios });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdCentroCosto}/{IdPlantilla}/{PSID}")]
        [HttpGet]
        public ActionResult GenerarPlantilla(int IdCentroCosto, int IdPlantilla, string PSID)
        {
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
                MessengerUsuarioRepositorio messengerUsuarioRepositorio = new MessengerUsuarioRepositorio();
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio();
                PgeneralRepositorio pgeneralRepositorio = new PgeneralRepositorio();
                MontoPagoRepositorio montoPagoRepositorio = new MontoPagoRepositorio();
                AreaCapacitacionRepositorio areaCapacitacionRepositorio = new AreaCapacitacionRepositorio();
                MonedaRepositorio monedaRepositorio = new MonedaRepositorio();
                PostComentarioUsuarioRepositorio postComentarioUsuarioRepositorio = new PostComentarioUsuarioRepositorio();


                var plantilla = _repPlantillaClaveValor.FirstBy(x => x.IdPlantilla == IdPlantilla && x.Clave == "Texto", y=> y.Valor);

                if (plantilla.Contains("{tAlumnos.nombre1}"))
                {
                    var nombre = messengerUsuarioRepositorio.FirstBy(x => x.Psid == PSID, y => y.Nombres);
                    if (nombre != null)plantilla = plantilla.Replace("{tAlumnos.nombre1}", nombre);
                    else
                    {
                        nombre = postComentarioUsuarioRepositorio.FirstBy(x => x.IdUsuarioFacebook == PSID, y => y.Nombres);
                        if (nombre != null) plantilla = plantilla.Replace("{tAlumnos.nombre1}", nombre);
                    }
                }

                if (IdCentroCosto != 0)
                {
                    int idPGeneral = pespecificoRepositorio.FirstBy(x => x.IdCentroCosto == IdCentroCosto).IdProgramaGeneral ?? 0;
                    var pgeneral = pgeneralRepositorio.FirstBy(x => x.Id == idPGeneral);

                    if (pgeneral != null)
                    {
                        if (plantilla.Contains("{tpla_pgeneral.pw_duracion}"))
                        {
                            plantilla = plantilla.Replace("{tpla_pgeneral.pw_duracion}", pgeneral.PwDuracion);
                        }
                        if (plantilla.Contains("{tPLA_PGeneral.Descripcion}"))
                        {
                            plantilla = plantilla.Replace("{tPLA_PGeneral.Descripcion}", pgeneral.PwDescripcionGeneral);
                        }
                        if (plantilla.Contains("{tPLA_PGeneral.UrlPaginaWeb}"))
                        {
                            var area = areaCapacitacionRepositorio.FirstById(pgeneral.IdArea).Descripcion;
                            var url = "https://bsginstitute.com/" + area + "/" + pgeneral.PgTitulo.Replace(" ", "-") + "-" + pgeneral.IdBusqueda;
                            plantilla = plantilla.Replace("{tPLA_PGeneral.UrlPaginaWeb}", url);
                        }
                        if (plantilla.Contains("{tPLA_PGeneral.PrecioCuotas}"))
                        {
                            var montoPago = montoPagoRepositorio.GetBy(x => x.IdPrograma == idPGeneral && x.IdPais == 0 && x.Matricula != 0).OrderBy(x=>x.Cuotas).FirstOrDefault();
                            var textoAReemplazar = "";

                            if (montoPago != null)
                            {
                                textoAReemplazar = "1 Cuota inicial de US$ " + montoPago.Matricula + " y " + montoPago.NroCuotas + " cuotas mensuales desde US$ " + montoPago.Cuotas;
                                if (montoPago.Descripcion != null && montoPago.Descripcion.Contains("Versión")) textoAReemplazar += "(" + montoPago.Descripcion + ")";
                            }

                            else
                            {
                                montoPago = montoPagoRepositorio.GetBy(x => x.IdPrograma == idPGeneral && x.IdPais == 0).OrderBy(x => x.Cuotas).FirstOrDefault();
                                if(montoPago != null) textoAReemplazar = "Al Contado desde US$ " + montoPago.Cuotas;
                                else
                                {
                                    montoPago = montoPagoRepositorio.GetBy(x => x.IdPrograma == idPGeneral).OrderBy(x => x.Cuotas).FirstOrDefault();
                                    if (montoPago != null)
                                    {
                                        var moneda = monedaRepositorio.FirstById(montoPago.IdMoneda);
                                        if (montoPago.Matricula == 0) textoAReemplazar = "Al Contado desde "+ moneda.Simbolo + montoPago.Cuotas;
                                        else textoAReemplazar = "1 Cuota inicial de " + moneda.Simbolo + montoPago.Matricula + " y " + montoPago.NroCuotas + " cuotas mensuales desde " + moneda.Simbolo + montoPago.Cuotas;
                                    }
                                }
                            }

                            plantilla = plantilla.Replace("{tPLA_PGeneral.PrecioCuotas}", textoAReemplazar);
                        }
                    }
                }

                return Ok(plantilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaComentarios(int IdPlantilla)
        {
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();

                var plantilla = _repPlantillaClaveValor.FirstBy(x => x.IdPlantilla == IdPlantilla && x.Clave == "Texto", y => y.Valor);
                return Ok(plantilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                    return Ok(_repCentroCosto.ObtenerTodoFiltroAutoCompletePorFecha(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ResponderComentarioInbox([FromBody] RespuestaComentarioInboxDTO RespuestaComentarioInboxDTO)
        {
            try
            {
                APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                MessengerUsuarioRepositorio messengerUsuarioRepositorio = new MessengerUsuarioRepositorio();
                MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio();

                ApiGraphFacebookResponseMensajeInboxDTO respuestaAPI = aPIGraphFacebook.ResponderComentarioInbox(RespuestaComentarioInboxDTO);

                if (respuestaAPI != null)
                {
                    MessengerUsuarioBO messengerUsuarioBO = messengerUsuarioRepositorio.FirstBy(x => x.Psid == respuestaAPI.recipient_id);
                    if(messengerUsuarioBO.Id == 0)
                    {
                        messengerUsuarioBO = new MessengerUsuarioBO();
                        messengerUsuarioBO.Psid = respuestaAPI.recipient_id;
                        messengerUsuarioBO.IdPersonal = RespuestaComentarioInboxDTO.IdPersonal;
                        messengerUsuarioBO.SeRespondio = true;
                        messengerUsuarioBO.Estado = true;
                        messengerUsuarioBO.FechaCreacion = DateTime.Now;
                        messengerUsuarioBO.FechaModificacion = DateTime.Now;
                        messengerUsuarioBO.UsuarioCreacion = RespuestaComentarioInboxDTO.Usuario;
                        messengerUsuarioBO.UsuarioModificacion = RespuestaComentarioInboxDTO.Usuario;
                        //messengerUsuarioBO.IdFacebookPagina = 1;

                        ApiGraphFacebookResponseUsuarioNombreDTO infoUsario = aPIGraphFacebook.ObtenerInformacionUsuario(respuestaAPI.recipient_id);
                        if(infoUsario != null) {
                            messengerUsuarioBO.Nombres = infoUsario.first_name;
                            messengerUsuarioBO.Apellidos = infoUsario.last_name;
                        }
                        messengerUsuarioRepositorio.Insert(messengerUsuarioBO);
                    }
                    else
                    {
                        messengerUsuarioBO.SeRespondio = true;
                        messengerUsuarioBO.FechaModificacion = DateTime.Now;
                        messengerUsuarioBO.UsuarioModificacion = RespuestaComentarioInboxDTO.Usuario;

                        messengerUsuarioRepositorio.Update(messengerUsuarioBO);
                    }

                    MessengerChatBO messengerChatBO = new MessengerChatBO();
                    messengerChatBO.IdMeseengerUsuario = messengerUsuarioBO.Id;
                    messengerChatBO.IdPersonal = RespuestaComentarioInboxDTO.IdPersonal;
                    messengerChatBO.Mensaje = RespuestaComentarioInboxDTO.Mensaje;
                    messengerChatBO.Tipo = false;
                    messengerChatBO.FacebookId = respuestaAPI.message_id;
                    messengerChatBO.FechaInteraccion = DateTime.Now;
                    messengerChatBO.IdTipoMensajeMessenger = 1;
                    messengerChatBO.Estado = true;
                    messengerChatBO.FechaCreacion = DateTime.Now;
                    messengerChatBO.FechaModificacion = DateTime.Now;
                    messengerChatBO.UsuarioCreacion = RespuestaComentarioInboxDTO.Usuario;
                    messengerChatBO.UsuarioModificacion = RespuestaComentarioInboxDTO.Usuario;

                    messengerChatRepositorio.Insert(messengerChatBO);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EnviarArchivosAdjuntosMessenger(RespuestaInboxDTO respuestaInboxDTO, [FromForm] IFormFile file)
        {
            string respuesta = string.Empty;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio(contexto);
                MessengerUsuarioRepositorio messengerUsuarioRepositorio = new MessengerUsuarioRepositorio(contexto);
                FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);

                FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = facebookPaginaRepositorio.ObtenerTokenAcceso(respuestaInboxDTO.RecipientId, 1);

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);
                blockBlob.Properties.ContentType = file.ContentType;

                Stream stream = file.OpenReadStream();
                var objRegistrado = blockBlob.UploadFromStreamAsync(stream);
                objRegistrado.Wait();

                if (objRegistrado.IsCompletedSuccessfully)
                {
                    ApiGraphFacebookResponseMensajeArchivoAdjuntoDTO respuestaAPI = aPIGraphFacebook.EnviarArchivoAdjuntoMessenger(respuestaInboxDTO.PSID, "https://repositorioweb.blob.core.windows.net/" + containerName + "/" + file.FileName.Replace(" ", "%20"), respuestaInboxDTO.TipoArchivoAdjunto, facebookAplicacionPaginaTokenDTO.Token);

                    MessengerUsuarioBO messengerUsuarioBO = messengerUsuarioRepositorio.FirstBy(x => x.Psid == respuestaInboxDTO.PSID && x.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina);

                    MessengerChatBO messengerChatBO = new MessengerChatBO();
                    messengerChatBO.IdMeseengerUsuario = messengerUsuarioBO.Id;
                    messengerChatBO.IdPersonal = respuestaInboxDTO.IdPersonal;
                    messengerChatBO.Mensaje = respuestaInboxDTO.TipoArchivoAdjunto == "image" ? "Imagen" : file.FileName;
                    messengerChatBO.Tipo = false;
                    messengerChatBO.FacebookId = respuestaAPI.message_id;
                    messengerChatBO.FechaInteraccion = DateTime.Now;
                    messengerChatBO.IdTipoMensajeMessenger = respuestaInboxDTO.TipoArchivoAdjunto == "image" ? 2 : 3;
                    messengerChatBO.UrlArchivoAdjunto = "https://repositorioweb.blob.core.windows.net/" + containerName + "/" + file.FileName.Replace(" ", "%20");
                    messengerChatBO.Estado = true;
                    messengerChatBO.FechaCreacion = DateTime.Now;
                    messengerChatBO.FechaModificacion = DateTime.Now;
                    messengerChatBO.UsuarioCreacion = respuestaInboxDTO.Usuario;
                    messengerChatBO.UsuarioModificacion = respuestaInboxDTO.Usuario;

                    messengerChatRepositorio.Insert(messengerChatBO);

                    string mensaje = "";
                    if (respuestaInboxDTO.TipoArchivoAdjunto == "image") mensaje = "<a href=\"" + messengerChatBO.UrlArchivoAdjunto + "\" download target=\"_blank\"><img src=\"" + messengerChatBO.UrlArchivoAdjunto + "\" height=\"200\" alt=\"\"><a>";
                    else mensaje = "<a href=\"" + messengerChatBO.UrlArchivoAdjunto + "\" download target=\"_blank\"><span style=\"font-size:32px;\" class=\"fa fa-file\" aria-hidden=\"true\"></span><span style=\"display: block;\">" + messengerChatBO.Mensaje + "</span><a>";

                    return Ok(mensaje);
                }
                else
                {
                    return BadRequest("No se pudo subir el archivo a BlobStorage");      
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{ComentarioFacebookId}/{Username}")]
        [HttpGet]
        public ActionResult EliminarComentario(string ComentarioFacebookId, string Username)
        {
            try
            {
                APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                PostComentarioDetalleRepositorio postComentarioDetalleRepositorio = new PostComentarioDetalleRepositorio();

                ApiGraphFacebookEliminarComentarioResponseDTO respuestaAPI = aPIGraphFacebook.EliminarComentario(ComentarioFacebookId);

                if (respuestaAPI != null && respuestaAPI.success == true)
                {
                    var comentarios = postComentarioDetalleRepositorio.GetBy(x => x.IdCommentFacebook == ComentarioFacebookId);

                    foreach(var comentario in comentarios)
                    {
                        comentario.Estado = false;
                        comentario.FechaModificacion = DateTime.Now;
                        comentario.UsuarioModificacion = Username;

                        postComentarioDetalleRepositorio.Update(comentario);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest("No se pudo eliminar el comentario.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdMessengerUsuario}/{Username}")]
        [HttpGet]
        public ActionResult DesuscribirUsuario(int IdMessengerUsuario, string Username)
        {
            try
            {
                MessengerUsuarioRepositorio messengerUsuarioRepositorio = new MessengerUsuarioRepositorio();

                MessengerUsuarioBO messengerUsuarioBO = messengerUsuarioRepositorio.FirstById(IdMessengerUsuario);
                messengerUsuarioBO.Desuscrito = true;
                messengerUsuarioBO.FechaModificacion = DateTime.Now;
                messengerUsuarioBO.UsuarioModificacion = Username;

                messengerUsuarioRepositorio.Update(messengerUsuarioBO);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorMessengerChatDTO : AbstractValidator<TMessengerChat>
    {
        public static ValidadorMessengerChatDTO Current = new ValidadorMessengerChatDTO();
        public ValidadorMessengerChatDTO()
        {

            //RuleFor(objeto => objeto.IdMeseengerUsuario).NotEmpty().WithMessage("IdMeseengerUsuario es Obligatorio");

            //RuleFor(objeto => objeto.IdPersonal).NotEmpty().WithMessage("IdPersonal es Obligatorio");

            RuleFor(objeto => objeto.Mensaje).NotEmpty().WithMessage("Mensaje es Obligatorio")
                                            .Length(1, 100).WithMessage("Mensaje debe tener 1 caracter minimo y 100 maximo");

        }
    }

}
