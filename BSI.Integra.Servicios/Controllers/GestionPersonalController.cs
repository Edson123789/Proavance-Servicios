using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GestionPersonal
    /// Autor: Luis H, Edgar S.
    /// Fecha: 25/01/2021        
    /// <summary>
    /// Controlador de Gestión de Personal
    /// </summary>
    [Route("api/GestionPersonal")]
    public class GestionPersonalController : Controller
    {
        
        private readonly integraDBContext _integraDBContext;
        public GestionPersonalController(integraDBContext _integraDBContexto) {
            _integraDBContext = _integraDBContexto;
        }



        /// TipoFuncion: GET
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informción del Personal en el sistema
        /// </summary>
        /// <returns> Lista de Personal en Sistema : List<GestionPersonalDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPersonal()
        {
            try
            {
                PersonalRepositorio _repPersonalRep = new PersonalRepositorio();
                var data = _repPersonalRep.ObtenerTodoPersonal();
                var total = data.Count();
                return Ok(new { Data = data, Total = total });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        /// TipoFuncion: GET
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de un determinado personal
        /// </summary>
        /// <returns> Información de personal : PersonalBO </returns>
        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult GetByIdPersonal(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonalRep = new PersonalRepositorio();
                return Ok(_repPersonalRep.GetBy(x => x.Estado == true && x.Id == IdPersonal, x => new { Id = x.Id, Nombres = x.Apellidos + " " + x.Nombres, x.Apellidos, x.Rol, x.TipoPersonal, x.Email, x.Anexo, x.Central, x.IdJefe }).FirstOrDefault());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de personal AutoComplete
        /// </summary>
        /// <returns> Id, Nombre de personal : List</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPersonalPorValor([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    PersonalRepositorio _repPersonalRep = new PersonalRepositorio();
                    var PersonalTemp = _repPersonalRep.ObtenerNombresFiltroAutoComplete(Valor["filtro"]);

                    return Ok(PersonalTemp);
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



        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Area, Nombre y Area de Trabajo
        /// </summary>
        /// <returns> Información de Area por Personal : PersonalAreaTrabajoBO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAreasPersonal()
        {
            try
            {
                PersonalAreaTrabajoRepositorio _repAreaTrabajoRep = new PersonalAreaTrabajoRepositorio();
                return Ok(_repAreaTrabajoRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, NombreCompleto = x.Nombre, NomAbrv = x.Codigo }).ToList());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S, Jose V.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Direcciones de Central de Llamadas
        /// </summary>
        /// <returns> Registro de Direcciones de Centrales de llamada: List</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerDireccionCentralLlamadas()
        {
            try
            {
                CentralLlamadaDireccionRepositorio _repCentralLLamadaDireccionRep = new CentralLlamadaDireccionRepositorio();
                return Ok(_repCentralLLamadaDireccionRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, NombreCentral = x.Nombre, Direccion = x.DireccionIp }).ToList());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Luis Huallpa
        /// Fecha: 04/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta un Personal
        /// </summary>
        /// <param name="Json">Información Compuesta de Personal</param>
        /// <returns>GestionPersonalDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarPersonal([FromBody] GestionPersonalDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonalRep = new PersonalRepositorio(_integraDBContext);
                PersonalLogRepositorio _repPersonalLog = new PersonalLogRepositorio(_integraDBContext);
				GmailClienteRepositorio _repGmailCliente = new GmailClienteRepositorio(_integraDBContext);
                var listEmailRepetidoValido = _repPersonalRep.GetBy(x => x.Estado == true && x.Email.Equals(Json.Email), x => new { x.Email, x.Id }).ToList();
                var idPersonalEmailRepetido = _repPersonalRep.ObtenerPersonalEliminadoEmailRepetido(Json.Email);
                int? idClasificacionPersona = null;
                PersonalBO personal = new PersonalBO();
                PersonaBO persona = new PersonaBO(_integraDBContext);
				var tmp = Json;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (listEmailRepetidoValido.Count == 0 && (idPersonalEmailRepetido == null || idPersonalEmailRepetido == 0))
                    {
                        personal.Nombres = Json.Nombres;
                        personal.Apellidos = Json.Apellidos;
                        personal.Rol = Json.Area;
                        personal.AreaAbrev = Json.AreaAbrev;
                        personal.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                        personal.IdJefe = Json.IdJefe;
                        personal.Central = Json.Central;
                        personal.TipoPersonal = Json.AsesorCoordinador;
                        personal.Email = Json.Email;
                        personal.Anexo = Json.Anexo;
						personal.Anexo3Cx = Json.Anexo;
						personal.UsuarioCreacion = Json.UsuarioCreacion;
                        personal.UsuarioModificacion = Json.UsuarioModificacion;
                        personal.FechaCreacion = DateTime.Now;
                        personal.FechaModificacion = DateTime.Now;
                        personal.Activo = Json.Activo;
						personal.UsuarioAsterisk = Json.UsuarioAsterisk;
						personal.ContrasenaAsterisk = Json.ContrasenaAsterisk;
                        Json.FechaCreacion = personal.FechaCreacion;
                        Json.FechaModificacion = personal.FechaModificacion;
                        personal.Estado = true;
                        Json.Estado = true;
                        _repPersonalRep.Insert(personal);
                        Json.Id = personal.Id;
                        idClasificacionPersona=persona.InsertarPersona(Json.Id,Aplicacion.Base.Enums.Enums.TipoPersona.Personal,Json.UsuarioCreacion);
                    }
                    else if (listEmailRepetidoValido.Count == 0 && (idPersonalEmailRepetido != null || idPersonalEmailRepetido != 0))
                    {
                        _repPersonalRep.ActivarPersonal(idPersonalEmailRepetido.Value);
                        personal = _repPersonalRep.FirstById(idPersonalEmailRepetido.Value);
                        personal.Nombres = Json.Nombres;
                        personal.Apellidos = Json.Apellidos;
                        personal.Rol = Json.Area;
                        personal.AreaAbrev = Json.AreaAbrev;
                        personal.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                        personal.IdJefe = Json.IdJefe;
                        personal.Central = Json.Central;
                        personal.TipoPersonal = Json.AsesorCoordinador;
                        personal.Email = Json.Email;
                        personal.Anexo = Json.Anexo;
						personal.Anexo3Cx = Json.Anexo;
						personal.UsuarioCreacion = Json.UsuarioCreacion;
                        personal.UsuarioModificacion = Json.UsuarioModificacion;
                        personal.FechaCreacion = DateTime.Now;
                        personal.FechaModificacion = DateTime.Now;
                        Json.FechaCreacion = personal.FechaCreacion;
                        Json.FechaModificacion = personal.FechaModificacion;
                        personal.Estado = true;
                        personal.Activo = Json.Activo;
						personal.UsuarioAsterisk = Json.UsuarioAsterisk;
						personal.ContrasenaAsterisk = Json.ContrasenaAsterisk;
						_repPersonalRep.Update(personal);
                        Json.Id = personal.Id;
                        idClasificacionPersona=persona.InsertarPersona(Json.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, Json.UsuarioCreacion);
                    }

                    else
                    {
                        Json = null;
                    }
                    if (idClasificacionPersona == null && Json!=null ) {
                        throw new Exception("Error al insertar el Tipo Persona Clasificacion");
                    }
					if (Json != null)
					{
						PersonalLogBO personalLogBO = new PersonalLogBO();
						personalLogBO.IdPersonal = personal.Id;
						personalLogBO.Rol = personal.Rol;
						personalLogBO.TipoPersonal = personal.TipoPersonal;
						personalLogBO.IdJefe = personal.IdJefe;
						personalLogBO.EstadoRol = true;
						personalLogBO.EstadoTipoPersonal = true;
						personalLogBO.EstadoIdJefe = true;
						personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
						personalLogBO.FechaFin = null;
						personalLogBO.Estado = true;
						personalLogBO.UsuarioModificacion = personal.UsuarioModificacion;
						personalLogBO.UsuarioCreacion = personal.UsuarioModificacion;
						personalLogBO.FechaCreacion = DateTime.Now;
						personalLogBO.FechaModificacion = DateTime.Now;

						_repPersonalLog.Insert(personalLogBO);

						GmailClienteBO gmailClienteBO;
						gmailClienteBO = _repGmailCliente.FirstBy(x => x.IdAsesor == personal.Id);
						if (gmailClienteBO == null)
						{
							gmailClienteBO = new GmailClienteBO();
							gmailClienteBO.IdAsesor = personal.Id;
							gmailClienteBO.EmailAsesor = personal.Email;
							gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
							gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
							gmailClienteBO.IdClient = "-";
							gmailClienteBO.ClientSecret = tmp.PasswordCorreo;

							gmailClienteBO.Estado = true;
							gmailClienteBO.UsuarioCreacion = personal.UsuarioModificacion;
							gmailClienteBO.UsuarioModificacion = personal.UsuarioModificacion;
							gmailClienteBO.FechaCreacion = DateTime.Now;
							gmailClienteBO.FechaModificacion = DateTime.Now;

							_repGmailCliente.Insert(gmailClienteBO);
						}
						else
						{
							gmailClienteBO.IdAsesor = personal.Id;
							gmailClienteBO.EmailAsesor = personal.Email;
							gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
							gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
							gmailClienteBO.IdClient = "-";
							gmailClienteBO.ClientSecret = tmp.PasswordCorreo;
							gmailClienteBO.UsuarioModificacion = personal.UsuarioModificacion;
							gmailClienteBO.FechaModificacion = DateTime.Now;

							_repGmailCliente.Update(gmailClienteBO);
						}
					}	
                    scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Luis Huallpa
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza datos de Personal
        /// </summary>
        /// <param name="Json">Información Compuesta de Personal</param>
        /// <returns>GestionPersonalDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarPersonal([FromBody] GestionPersonalDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonalRep = new PersonalRepositorio(_integraDBContext);
				GmailClienteRepositorio _repGmailCliente = new GmailClienteRepositorio(_integraDBContext);
				PersonalBO personal = new PersonalBO();
                personal = _repPersonalRep.FirstById(Json.Id);
				var tmp = Json;

				PersonalLogRepositorio _repPersonalLog = new PersonalLogRepositorio(_integraDBContext);                
                var rolAnterior = personal.Rol;
                var tipoPersonalAnterior = personal.TipoPersonal==null?"": personal.TipoPersonal;
                int? idJefeAnterior = personal.IdJefe;
                var estadoCambioRolJefe = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    personal.Nombres = Json.Nombres;
                    personal.Apellidos = Json.Apellidos;
                    personal.Rol = Json.Area;
                    personal.AreaAbrev = Json.AreaAbrev;
                    personal.IdJefe = Json.IdJefe;
                    personal.Central = Json.Central;
                    personal.TipoPersonal = Json.AsesorCoordinador;
                    personal.Email = Json.Email;
                    personal.Anexo = Json.Anexo;
					personal.Anexo3Cx = Json.Anexo;
					personal.UsuarioAsterisk = Json.UsuarioAsterisk;
					personal.ContrasenaAsterisk = Json.ContrasenaAsterisk;
					personal.UsuarioModificacion = Json.UsuarioModificacion;
                    personal.FechaModificacion = DateTime.Now;
                    Json.FechaModificacion = personal.FechaModificacion;
                    personal.Estado = true;
                    personal.Activo = Json.Activo;
                    personal.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                    _repPersonalRep.Update(personal);

                    if (!(rolAnterior.ToUpper().Equals(personal.Rol.ToUpper())) || !(tipoPersonalAnterior.ToUpper().Equals(personal.TipoPersonal.ToUpper()))) {
                        var personalLogUpdate = _repPersonalLog.FirstBy(x=>x.IdPersonal== personal.Id &&(x.EstadoRol==true || x.EstadoTipoPersonal==true) && x.FechaFin==null);
                        var personalCambioJefe = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true && x.EstadoRol == false && x.EstadoTipoPersonal == false) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                        estadoCambioRolJefe = personalLogUpdate.EstadoIdJefe == true && personalLogUpdate.EstadoRol == true && personalLogUpdate.EstadoTipoPersonal == true;
                        if (estadoCambioRolJefe && personalCambioJefe==null)
                        {
                            PersonalLogBO personalLog = new PersonalLogBO();
                            personalLog.IdPersonal = personal.Id;
                            personalLog.Rol = personal.Rol;
                            personalLog.TipoPersonal = personal.TipoPersonal;
                            personalLog.IdJefe = idJefeAnterior;
                            personalLog.EstadoRol = false;
                            personalLog.EstadoTipoPersonal = false;
                            personalLog.EstadoIdJefe = true;
                            personalLog.FechaInicio = personalLogUpdate.FechaInicio;
                            personalLog.FechaFin = null;
                            personalLog.Estado = true;
                            personalLog.UsuarioModificacion = personal.UsuarioModificacion;
                            personalLog.UsuarioCreacion = personal.UsuarioModificacion;
                            personalLog.FechaCreacion = DateTime.Now;
                            personalLog.FechaModificacion = DateTime.Now;
                            _repPersonalLog.Insert(personalLog);
                        }
                        personalLogUpdate.FechaFin =new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day,23,59,59);                        
                        personalLogUpdate.UsuarioModificacion = personal.UsuarioModificacion;
                        personalLogUpdate.FechaModificacion= DateTime.Now;
                        _repPersonalLog.Update(personalLogUpdate);

                        PersonalLogBO personalLogBO = new PersonalLogBO();
                        personalLogBO.IdPersonal = personal.Id;
                        personalLogBO.Rol = personal.Rol;
                        personalLogBO.TipoPersonal = personal.TipoPersonal;
                        personalLogBO.IdJefe = personal.IdJefe;
                        personalLogBO.EstadoRol = rolAnterior != personal.Rol;
                        personalLogBO.EstadoTipoPersonal = tipoPersonalAnterior != personal.TipoPersonal;
                        personalLogBO.EstadoIdJefe = false;
                        personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;
                        personalLogBO.FechaFin = null;
                        personalLogBO.Estado = true;
                        personalLogBO.UsuarioModificacion = personal.UsuarioModificacion;
                        personalLogBO.UsuarioCreacion= personal.UsuarioModificacion;
                        personalLogBO.FechaCreacion = DateTime.Now;
                        personalLogBO.FechaModificacion = DateTime.Now;

                        _repPersonalLog.Insert(personalLogBO);
                    }                    
                    if (idJefeAnterior!=personal.IdJefe)
                    {
                        if (estadoCambioRolJefe==false)
                        {                            
                            var personalLogUpdate = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                            var personalCambioJefe= _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true && x.EstadoRol==false && x.EstadoTipoPersonal==false) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                            estadoCambioRolJefe = personalLogUpdate.EstadoIdJefe == true && personalLogUpdate.EstadoRol == true && personalLogUpdate.EstadoTipoPersonal == true;
                            if (estadoCambioRolJefe && personalCambioJefe==null)
                            {

                                PersonalLogBO personalLog = new PersonalLogBO();
                                personalLog.IdPersonal = personal.Id;
                                personalLog.Rol = personal.Rol;
                                personalLog.TipoPersonal = personal.TipoPersonal;
                                personalLog.IdJefe = idJefeAnterior;
                                personalLog.EstadoRol = false;
                                personalLog.EstadoTipoPersonal = false;
                                personalLog.EstadoIdJefe = true;
                                personalLog.FechaInicio = personalLogUpdate.FechaInicio;
                                personalLog.FechaFin = null;
                                personalLog.Estado = true;
                                personalLog.UsuarioModificacion = personal.UsuarioModificacion;
                                personalLog.UsuarioCreacion = personal.UsuarioModificacion;
                                personalLog.FechaCreacion = DateTime.Now;
                                personalLog.FechaModificacion = DateTime.Now;
                                _repPersonalLog.Insert(personalLog);
                            }
                        }

                        var personalLogUpdate2 = _repPersonalLog.GetBy(x => x.IdPersonal == personal.Id && (x.EstadoIdJefe == true) && x.FechaFin == null).OrderByDescending(x => x.Id).FirstOrDefault();
                        personalLogUpdate2.FechaFin = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day,23,59,59);
                        personalLogUpdate2.UsuarioModificacion = personal.UsuarioModificacion;
                        personalLogUpdate2.FechaModificacion = DateTime.Now;
                        _repPersonalLog.Update(personalLogUpdate2);

                        PersonalLogBO personalLogBO = new PersonalLogBO();
                        personalLogBO.IdPersonal = personal.Id;
                        personalLogBO.Rol = personal.Rol;
                        personalLogBO.TipoPersonal = personal.TipoPersonal;
                        personalLogBO.IdJefe = personal.IdJefe;
                        personalLogBO.EstadoRol = false;
                        personalLogBO.EstadoTipoPersonal = false;
                        personalLogBO.EstadoIdJefe = idJefeAnterior != personal.IdJefe;
                        personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                        personalLogBO.FechaFin = null;
                        personalLogBO.Estado=true;
                        personalLogBO.UsuarioModificacion = personal.UsuarioModificacion;
                        personalLogBO.UsuarioCreacion = personal.UsuarioModificacion;
                        personalLogBO.FechaCreacion = DateTime.Now;
                        personalLogBO.FechaModificacion = DateTime.Now;
                        _repPersonalLog.Insert(personalLogBO);
                    }
					GmailClienteBO gmailClienteBO;
					gmailClienteBO = _repGmailCliente.FirstBy(x => x.IdAsesor == personal.Id);
					if (gmailClienteBO == null)
					{
						gmailClienteBO = new GmailClienteBO();
						gmailClienteBO.IdAsesor = personal.Id;
						gmailClienteBO.EmailAsesor = personal.Email;
						gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
						gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
						gmailClienteBO.IdClient = "-";
						gmailClienteBO.ClientSecret = tmp.PasswordCorreo;

						gmailClienteBO.Estado = true;
						gmailClienteBO.UsuarioCreacion = personal.UsuarioModificacion;
						gmailClienteBO.UsuarioModificacion = personal.UsuarioModificacion;
						gmailClienteBO.FechaCreacion = DateTime.Now;
						gmailClienteBO.FechaModificacion = DateTime.Now;

						_repGmailCliente.Insert(gmailClienteBO);
					}
					else
					{
						gmailClienteBO.IdAsesor = personal.Id;
						gmailClienteBO.EmailAsesor = personal.Email;
						gmailClienteBO.PasswordCorreo = tmp.PasswordCorreo;
						gmailClienteBO.NombreAsesor = string.Concat(personal.Nombres, " ", personal.Apellidos);
						gmailClienteBO.IdClient = "-";
						gmailClienteBO.ClientSecret = tmp.PasswordCorreo;
						gmailClienteBO.UsuarioModificacion = personal.UsuarioModificacion;
						gmailClienteBO.FechaModificacion = DateTime.Now;
						_repGmailCliente.Update(gmailClienteBO);
					}
					scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Informacion de Personal por Id
        /// </summary>
        /// <returns> Retorna información de personal : PersonalBO </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerPersonalId(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonalRep = new PersonalRepositorio();
                return Ok(_repPersonalRep.GetBy(x => x.Estado == true && x.Id == IdPersonal, x => new { Id = x.Id, Nombres = x.Nombres, x.Apellidos, x.Rol, x.TipoPersonal, x.Email, x.Anexo, x.Central, x.IdJefe }).FirstOrDefault());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: Luis H, Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza validaciones de Acceso
        /// </summary>
        /// <returns> Obtiene confirmación de envio: Bool </returns>
        [Route("[action]")]
		[HttpPost]
		public IActionResult EnviarMensajeValidacionAcceso([FromBody]EnvioCorreoValidacionAccesoDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				GmailClienteRepositorio _repGmailCliente = new GmailClienteRepositorio(_integraDBContext);
				IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
				GmailCorreoBO gmailCorreoBO = new GmailCorreoBO();

				var emailDestinatario = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(Json.Usuario);
				var mensaje = "Envio exitoso, la clave de aplicación es correcta!.";
				var asunto = "Validación clave de aplicación " + Json.EmailRemitente;
				bool res = gmailCorreoBO.EnviarCorreoGmail(emailDestinatario, Json.EmailRemitente, Json.PersonalRemitente, Json.PasswordCorreo, mensaje, asunto);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

    }
}
