using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Servicios.Controllers
{

    /// Controlador: Personal
    /// Autor: Carlos Crispin
    /// Fecha: 27/01/2020
    /// <summary>
    /// Gestiona todas la propiedades de la tabla t_personal y permite insertar,actualizar e eliminar datos 
    /// </summary>

    [Route("api/Personal")]
    [ApiController]
    public class PersonalController : ControllerBase
    {

        private readonly integraDBContext _integraDBContext;
        public PersonalController(integraDBContext _integraDBContexto)
        {
            _integraDBContext = _integraDBContexto;
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.  
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        ///Obtiene los datos del personal segun el id enviado
        /// </summary>
        /// <returns>retorna un objeto tipo PersonalInformacionAgendaDTO</returns>

        [Route("[action]/{IdPersonal}")]
        [HttpPost]
        public IActionResult ObtenerDatosPersonal(int IdPersonal)
        {
            if (IdPersonal <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                PersonalRepositorio _personalRepositorio = new PersonalRepositorio();
                PersonalInformacionAgendaDTO personalInformacionAgendaDTO = new PersonalInformacionAgendaDTO();
                personalInformacionAgendaDTO.DatosPersonal = _personalRepositorio.ObtenerDatosPersonalAgenda(IdPersonal);
                personalInformacionAgendaDTO.Asignados = _personalRepositorio.GetPersonalAsignado(IdPersonal);
                return Ok(personalInformacionAgendaDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPersonal}")]
        [HttpPost]
        public IActionResult GetRolAreaPersonal(int IdPersonal)
        {
            if (IdPersonal <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                PersonalRolAreaDTO datosPersonalDTO = new PersonalRolAreaDTO();
                PersonalRepositorio _personalRepositorio = new PersonalRepositorio();
                PersonalBO personalBO = _personalRepositorio.FirstById(IdPersonal);
                var area = personalBO.AreaAbrev.ToUpper().Equals("MK") ? "MKT" : personalBO.AreaAbrev;
                PersonalAreaTrabajoRepositorio repTrabajoRep = new PersonalAreaTrabajoRepositorio();
                var IdArea = repTrabajoRep.GetBy(x => x.Estado == true && x.Codigo.ToUpper().Equals(area), x => new { x.Id }).FirstOrDefault();
                datosPersonalDTO.IdArea = IdArea.Id;
                IntegraAspNetUsersRepositorio repUsersRep = new IntegraAspNetUsersRepositorio();
                var rol = repUsersRep.GetBy(x => x.Estado == true && x.PerId == IdPersonal, x => new { x.RolId }).FirstOrDefault();
                datosPersonalDTO.IdRol = rol.RolId;
                return Ok(datosPersonalDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPersonal}")]
        [HttpPost]
        public IActionResult GetRolAreaTipoPersonal(int IdPersonal)
        {
            if (IdPersonal <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                PersonalRolAreaTipoPersonalDTO datosPersonalDTO = new PersonalRolAreaTipoPersonalDTO();
                PersonalRepositorio _personalRepositorio = new PersonalRepositorio();
                PersonalBO personalBO = _personalRepositorio.FirstById(IdPersonal);
                var area = personalBO.AreaAbrev.ToUpper().Equals("MK") ? "MKT" : personalBO.AreaAbrev;
                PersonalAreaTrabajoRepositorio repTrabajoRep = new PersonalAreaTrabajoRepositorio();
                var IdArea = repTrabajoRep.GetBy(x => x.Estado == true && x.Codigo.ToUpper().Equals(area), x => new { x.Id }).FirstOrDefault();
                datosPersonalDTO.IdArea = IdArea.Id;
                IntegraAspNetUsersRepositorio repUsersRep = new IntegraAspNetUsersRepositorio();
                var rol = repUsersRep.GetBy(x => x.Estado == true && x.PerId == IdPersonal, x => new { x.RolId }).FirstOrDefault();
                datosPersonalDTO.IdRol = rol.RolId;
                datosPersonalDTO.Area = area;
                datosPersonalDTO.Rol = personalBO.Rol;
                datosPersonalDTO.TipoPersonal = personalBO.TipoPersonal;


                return Ok(datosPersonalDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerEmailPersonalPorId(int id)
        {
            try
            {
                PersonalBO personal = new PersonalBO();
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                var res = _repPersonal.ObtenerEmailPersonalPorId(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Wilber Choque - Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1
        /// <summary>
        /// Obtiene todos el personal que sea de ventas y sea asesor o coordinador
        /// </summary>
        /// <returns>Response 200 con objeto de clase AsesorNombreFiltroDTO, response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodosAsesor()
        {
            try
            {
                PersonalRepositorio _repPesonal = new PersonalRepositorio();
                return Ok(_repPesonal.ObtenerTodoAsesorCoordinadorVentas());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAreaTrabajoPersonal()
        {
            try
            {
                PersonalAreaTrabajoRepositorio repTrabajoRep = new PersonalAreaTrabajoRepositorio();
                return Ok(repTrabajoRep.ObtenerAreaTrabajoPersonal());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAreaTrabajoPersonalParaOperaciones()
        {
            try
            {
                PersonalAreaTrabajoRepositorio repTrabajoRep = new PersonalAreaTrabajoRepositorio();
                return Ok(repTrabajoRep.ObtenerAreaTrabajoPersonalParaOperaciones());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAreaTrabajoPersonalNombreArea()
        {
            try
            {
                PersonalAreaTrabajoRepositorio repTrabajoRep = new PersonalAreaTrabajoRepositorio();
                return Ok(repTrabajoRep.ObtenerAreaTrabajoPersonalNombre());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio();
                CiudadRepositorio _repCiudad = new CiudadRepositorio();
                EstadoCivilRepositorio _repEstado = new EstadoCivilRepositorio();
                SexoRepositorio _repSexo = new SexoRepositorio();
                SistemaPensionarioRepositorio _repSistemaPensionario = new SistemaPensionarioRepositorio();
                EntidadSistemaPensionarioRepositorio _repEntidad = new EntidadSistemaPensionarioRepositorio();
                TipoDocumentoPersonalRepositorio _repTipoDocumento = new TipoDocumentoPersonalRepositorio();
                MotivoCeseRepositorio _repMotivoCese = new MotivoCeseRepositorio();


                PersonalCombosDTO personalCombo = new PersonalCombosDTO();

                personalCombo.ListaCiudad = _repCiudad.ObtenerCiudadesPorPais();
                personalCombo.ListaPais = _repPais.ObtenerPaisesCombo();
                personalCombo.ListaEstadoCivil = _repEstado.GetFiltroIdNombre();
                personalCombo.ListaSexo = _repSexo.GetFiltroIdNombre();
                personalCombo.ListaSistemaPensionario = _repSistemaPensionario.GetFiltroIdNombre();
                personalCombo.ListaEntidad = _repEntidad.GetFiltroIdNombre();
                personalCombo.ListaTipoDocumento = _repTipoDocumento.GetFiltroIdNombre();
                personalCombo.ListaMotivoCese = _repTipoDocumento.GetFiltroIdNombre();

                return Ok(personalCombo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPersonalGrid()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();

                return Ok(_repPersonal.ObtenerGrid());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerPersonalPorId(int Id)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                PersonalCeseRepositorio _repCese = new PersonalCeseRepositorio();

                DatosPersonalDTO datosPersonal = new DatosPersonalDTO();
                datosPersonal.Personal = _repPersonal.ObtenerDatosPersonal(Id);
                if (datosPersonal.Personal.Activo == false)
                {
                    datosPersonal.Cese = _repCese.ObtenerMotivoFecha(Id);
                }

                return Ok(datosPersonal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Luis Huallpa - Carlos Crispin
        /// Fecha: 23/03/2021
        /// Versión: 1
        /// <summary>
        /// Obtiene el horario por idPersonal
        /// </summary>
        /// <param name="Id">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <returns>Response 200 con la lista de objetos de clase Horario DTO o response 400 con el mensaje de error</returns>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObteneHorarioPorId(int Id)
        {
            try
            {
                PersonalHorarioRepositorio _repPersonalHorario = new PersonalHorarioRepositorio();

                return Ok(_repPersonalHorario.ObtenerHorario(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult CalcularFirmaHTML(int IdPersonal)
        {
            try
            {
                var _repPersonal = new PersonalRepositorio();

                var listaPersonal = new List<PersonalBO>();
                if (IdPersonal != 0)
                {
                    listaPersonal = _repPersonal.GetBy(x => x.Id == IdPersonal).ToList();
                }
                else
                {
                    //todos los que aplican
                    listaPersonal = _repPersonal.GetBy(x => x.AplicaFirmaHtml == true).ToList();
                }

                foreach (var item in listaPersonal)
                {
                    item.FirmaHtml = item.FirmaCorreoHTML;
                }
                _repPersonal.Update(listaPersonal);
                return Ok(listaPersonal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/")]
        [HttpDelete]
        public ActionResult Eliminar([FromBody] PersonalDeleteDTO Personal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repFormularioLandingPage = new PersonalRepositorio();
                return Ok(_repFormularioLandingPage.Delete(Personal.Id, Personal.Usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/03/2021
        /// Versión: 1
        /// <summary>
        /// Obtiene la configuracion OpenVox por el Id del personal
        /// </summary>
        /// <param name="IdPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <returns>Response 200 con lista de objetos de clase PersonalConfiguracionOpenVoxDTO o response 400 con mensaje de error</returns>
        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionOpenVox(int IdPersonal)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);

                return Ok(_repPersonal.ObtenerConfiguracionOpenVoxPorIdPersonal(IdPersonal));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Luis Huallpa - Carlos Crispin - Britsel C.
        /// Fecha: 23/03/2021
        /// Versión: 1
        /// <summary>
        /// Guardar el horario por personal
        /// </summary>
        /// <param name="Json">Objeto de clase HorarioDTO para la asignacion de horarios por personal</param>
        /// <returns>Response 200 o response 400 con mensaje de error</returns>
        [Route("[action]/")]
        [HttpPost]
        public ActionResult GuardarHorario([FromBody] HorarioDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalHorarioRepositorio _repPersonalHorario = new PersonalHorarioRepositorio();
                PersonalHorarioBO personalHorarioBO = new PersonalHorarioBO(); ;
                List<PersonalHorarioBO> listaHorario = _repPersonalHorario.GetBy(x => x.IdPersonal == Json.IdPersonal && x.Estado == true).OrderByDescending(x => x.FechaCreacion).ToList();
                var i = 1;
                foreach (var item in listaHorario)
                {
                    if (i == 1)
                    {
                        item.FechaFin = DateTime.Now;
                    }
                    item.Activo = false;
                    _repPersonalHorario.Update(item);
                    i = i + 1;
                }

                personalHorarioBO.IdPersonal = Json.IdPersonal ?? 0;
                personalHorarioBO.Estado = true;
                personalHorarioBO.Activo = true;
                personalHorarioBO.FechaCreacion = DateTime.Now;
                personalHorarioBO.UsuarioCreacion = Json.Usuario;

                personalHorarioBO.Lunes1 = Json.Lunes1;
                personalHorarioBO.Lunes2 = Json.Lunes2;
                personalHorarioBO.Lunes3 = Json.Lunes3;
                personalHorarioBO.Lunes4 = Json.Lunes4;
                personalHorarioBO.Martes1 = Json.Martes1;
                personalHorarioBO.Martes2 = Json.Martes2;
                personalHorarioBO.Martes3 = Json.Martes3;
                personalHorarioBO.Martes4 = Json.Martes4;
                personalHorarioBO.Miercoles1 = Json.Miercoles1;
                personalHorarioBO.Miercoles2 = Json.Miercoles2;
                personalHorarioBO.Miercoles3 = Json.Miercoles3;
                personalHorarioBO.Miercoles4 = Json.Miercoles4;
                personalHorarioBO.Jueves1 = Json.Jueves1;
                personalHorarioBO.Jueves2 = Json.Jueves2;
                personalHorarioBO.Jueves3 = Json.Jueves3;
                personalHorarioBO.Jueves4 = Json.Jueves4;
                personalHorarioBO.Viernes1 = Json.Viernes1;
                personalHorarioBO.Viernes2 = Json.Viernes2;
                personalHorarioBO.Viernes3 = Json.Viernes3;
                personalHorarioBO.Viernes4 = Json.Viernes4;
                personalHorarioBO.Sabado1 = Json.Sabado1;
                personalHorarioBO.Sabado2 = Json.Sabado2;
                personalHorarioBO.Sabado3 = Json.Sabado3;
                personalHorarioBO.Sabado4 = Json.Sabado4;
                personalHorarioBO.Domingo1 = Json.Domingo1;
                personalHorarioBO.Domingo2 = Json.Domingo2;
                personalHorarioBO.Domingo3 = Json.Domingo3;
                personalHorarioBO.Domingo4 = Json.Domingo4;
                personalHorarioBO.FechaInicio = DateTime.Now;
                personalHorarioBO.FechaFin = null;
                personalHorarioBO.FechaModificacion = DateTime.Now;
                personalHorarioBO.UsuarioModificacion = Json.Usuario;

                //if (personalHorarioBO.Id == 0)
                _repPersonalHorario.Insert(personalHorarioBO);
                //else _repPersonalHorario.Update(personalHorarioBO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/")]
        [HttpPost]
        public ActionResult Insertar([FromBody] DatosPersonalDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                PersonalAreaTrabajoRepositorio _area = new PersonalAreaTrabajoRepositorio();
                var area = _area.FirstBy(x => x.Nombre.Contains("rol"));
                int? IdPersonaClasificacion = null;

                var ListEmailRepetidoValido = _repPersonal.GetBy(x => x.Estado == true && x.Email.Equals(Json.Personal.Email), x => new { x.Email, x.Id }).ToList();
                var IdPersonalEmailRepetido = _repPersonal.ObtenerPersonalEliminadoEmailRepetido(Json.Personal.Email);

                PersonalBO personalBO = new PersonalBO();
                PersonaBO persona = new PersonaBO(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (ListEmailRepetidoValido.Count == 0 && (IdPersonalEmailRepetido == null || IdPersonalEmailRepetido == 0))
                    {
                        if (!string.IsNullOrEmpty(Json.Personal.ApellidoPaterno))
                        {
                            personalBO.Apellidos = Json.Personal.ApellidoPaterno + " " + Json.Personal.ApellidoMaterno;
                        }

                        personalBO.Nombres = Json.Personal.Nombres;
                        personalBO.TipoPersonal = Json.Personal.TipoPersonal;
                        personalBO.Email = Json.Personal.Email;
                        personalBO.AreaAbrev = area != null ? area.Codigo : null;
                        personalBO.IdJefe = Json.Personal.IdJefe;
                        personalBO.Central = Json.Personal.Central;
                        personalBO.Activo = Json.Personal.Activo;
                        personalBO.ApellidoPaterno = Json.Personal.ApellidoPaterno;
                        personalBO.ApellidoMaterno = Json.Personal.ApellidoMaterno;
                        personalBO.IdSexo = Json.Personal.IdSexo;
                        personalBO.IdEstadocivil = Json.Personal.IdEstadocivil;
                        personalBO.FechaNacimiento = Json.Personal.FechaNacimiento;
                        personalBO.IdPaisNacimiento = Json.Personal.IdPaisNacimiento;
                        personalBO.IdRegion = Json.Personal.IdRegion;
                        personalBO.IdTipoDocumento = Json.Personal.IdTipoDocumento;
                        personalBO.NumeroDocumento = Json.Personal.NumeroDocumento;
                        personalBO.UrlFirmaCorreos = Json.Personal.UrlFirmaCorreos;
                        personalBO.IdPaisDireccion = Json.Personal.IdPaisDireccion;
                        personalBO.IdRegionDireccion = Json.Personal.IdRegionDireccion;
                        personalBO.CiudadDireccion = Json.Personal.CiudadDireccion;
                        personalBO.NombreDireccion = Json.Personal.NombreDireccion;
                        personalBO.FijoReferencia = Json.Personal.FijoReferencia;
                        personalBO.MovilReferencia = Json.Personal.MovilReferencia;
                        personalBO.EmailReferencia = Json.Personal.EmailReferencia;
                        personalBO.IdSistemaPensionario = Json.Personal.IdSistemaPensionario;
                        personalBO.IdEntidadSistemaPensionario = Json.Personal.IdEntidadSistemaPensionario;
                        personalBO.NombreCuspp = Json.Personal.NombreCuspp;
                        personalBO.DistritoDireccion = Json.Personal.DistritoDireccion;

                        personalBO.Estado = true;
                        personalBO.UsuarioCreacion = Json.Usuario;
                        personalBO.UsuarioModificacion = Json.Usuario;
                        personalBO.FechaCreacion = DateTime.Now;
                        personalBO.FechaModificacion = DateTime.Now;

                        _repPersonal.Insert(personalBO);
                        Json.Personal.Id = personalBO.Id;
                        IdPersonaClasificacion = persona.InsertarPersona(Json.Personal.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, Json.Usuario);
                    }
                    else if (ListEmailRepetidoValido.Count == 0 && (IdPersonalEmailRepetido != null || IdPersonalEmailRepetido != 0))
                    {
                        _repPersonal.ActivarPersonal(IdPersonalEmailRepetido.Value);
                        personalBO = _repPersonal.FirstById(IdPersonalEmailRepetido.Value);

                        if (!string.IsNullOrEmpty(Json.Personal.ApellidoPaterno))
                        {
                            personalBO.Apellidos = Json.Personal.ApellidoPaterno + " " + Json.Personal.ApellidoMaterno;
                        }

                        personalBO.Nombres = Json.Personal.Nombres;
                        personalBO.TipoPersonal = Json.Personal.TipoPersonal;
                        personalBO.Email = Json.Personal.Email;
                        personalBO.AreaAbrev = area != null ? area.Codigo : null;
                        personalBO.IdJefe = Json.Personal.IdJefe;
                        personalBO.Central = Json.Personal.Central;
                        personalBO.Activo = Json.Personal.Activo;
                        personalBO.ApellidoPaterno = Json.Personal.ApellidoPaterno;
                        personalBO.ApellidoMaterno = Json.Personal.ApellidoMaterno;
                        personalBO.IdSexo = Json.Personal.IdSexo;
                        personalBO.IdEstadocivil = Json.Personal.IdEstadocivil;
                        personalBO.FechaNacimiento = Json.Personal.FechaNacimiento;
                        personalBO.IdPaisNacimiento = Json.Personal.IdPaisNacimiento;
                        personalBO.IdRegion = Json.Personal.IdRegion;
                        personalBO.IdTipoDocumento = Json.Personal.IdTipoDocumento;
                        personalBO.NumeroDocumento = Json.Personal.NumeroDocumento;
                        personalBO.UrlFirmaCorreos = Json.Personal.UrlFirmaCorreos;
                        personalBO.IdPaisDireccion = Json.Personal.IdPaisDireccion;
                        personalBO.IdRegionDireccion = Json.Personal.IdRegionDireccion;
                        personalBO.CiudadDireccion = Json.Personal.CiudadDireccion;
                        personalBO.NombreDireccion = Json.Personal.NombreDireccion;
                        personalBO.FijoReferencia = Json.Personal.FijoReferencia;
                        personalBO.MovilReferencia = Json.Personal.MovilReferencia;
                        personalBO.EmailReferencia = Json.Personal.EmailReferencia;
                        personalBO.IdSistemaPensionario = Json.Personal.IdSistemaPensionario;
                        personalBO.IdEntidadSistemaPensionario = Json.Personal.IdEntidadSistemaPensionario;
                        personalBO.NombreCuspp = Json.Personal.NombreCuspp;
                        personalBO.DistritoDireccion = Json.Personal.DistritoDireccion;

                        personalBO.Estado = true;
                        personalBO.UsuarioCreacion = Json.Usuario;
                        personalBO.UsuarioModificacion = Json.Usuario;
                        personalBO.FechaCreacion = DateTime.Now;
                        personalBO.FechaModificacion = DateTime.Now;

                        _repPersonal.Update(personalBO);
                        Json.Personal.Id = personalBO.Id;
                        IdPersonaClasificacion = persona.InsertarPersona(Json.Personal.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, Json.Usuario);

                    }
                    else
                    {
                        Json = null;
                    }
                    if (IdPersonaClasificacion == null && Json != null)
                    {
                        throw new Exception("Error al insertar el Tipo Persona Clasificacion");
                    }
                    scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] DatosPersonalDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                PersonalCeseRepositorio _repCese = new PersonalCeseRepositorio();
                PersonalAreaTrabajoRepositorio _area = new PersonalAreaTrabajoRepositorio();
                var area = _area.FirstBy(x => x.Nombre.Contains("rol"));

                PersonalBO personalBO = _repPersonal.FirstById(Json.Personal.Id);

                personalBO.Nombres = Json.Personal.Nombres;
                personalBO.Apellidos = Json.Personal.Apellidos;
                personalBO.TipoPersonal = Json.Personal.TipoPersonal;
                personalBO.Email = Json.Personal.Email;
                personalBO.AreaAbrev = area != null ? area.Codigo : null;
                personalBO.Anexo = Json.Personal.Anexo;
                personalBO.IdJefe = Json.Personal.IdJefe;
                personalBO.Central = Json.Personal.Central;
                personalBO.Activo = Json.Personal.Activo;
                personalBO.ApellidoPaterno = Json.Personal.ApellidoPaterno;
                personalBO.ApellidoMaterno = Json.Personal.ApellidoMaterno;
                personalBO.IdSexo = Json.Personal.IdSexo;
                personalBO.IdEstadocivil = Json.Personal.IdEstadocivil;
                personalBO.FechaNacimiento = Json.Personal.FechaNacimiento;
                personalBO.IdPaisNacimiento = Json.Personal.IdPaisNacimiento;
                personalBO.IdRegion = Json.Personal.IdRegion;
                personalBO.IdTipoDocumento = Json.Personal.IdTipoDocumento;
                personalBO.NumeroDocumento = Json.Personal.NumeroDocumento;
                personalBO.UrlFirmaCorreos = Json.Personal.UrlFirmaCorreos;
                personalBO.IdPaisDireccion = Json.Personal.IdPaisDireccion;
                personalBO.IdRegionDireccion = Json.Personal.IdRegionDireccion;
                personalBO.CiudadDireccion = Json.Personal.CiudadDireccion;
                personalBO.NombreDireccion = Json.Personal.NombreDireccion;
                personalBO.FijoReferencia = Json.Personal.FijoReferencia;
                personalBO.MovilReferencia = Json.Personal.MovilReferencia;
                personalBO.EmailReferencia = Json.Personal.EmailReferencia;
                personalBO.IdSistemaPensionario = Json.Personal.IdSistemaPensionario;
                personalBO.IdEntidadSistemaPensionario = Json.Personal.IdEntidadSistemaPensionario;
                personalBO.NombreCuspp = Json.Personal.NombreCuspp;
                personalBO.DistritoDireccion = Json.Personal.DistritoDireccion;

                personalBO.Estado = true;
                personalBO.UsuarioModificacion = Json.Usuario;
                personalBO.FechaModificacion = DateTime.Now;

                _repPersonal.Update(personalBO);

                if (personalBO.Activo == true)
                {
                    PersonalCeseBO cese = _repCese.FirstBy(x => x.IdPersonal == personalBO.Id);
                    if (cese != null)
                    {
                        cese.Estado = false;
                        cese.UsuarioModificacion = Json.Usuario;
                        cese.FechaModificacion = DateTime.Now;
                        _repCese.Update(cese);
                    }
                }
                else if (Json.Cese != null)
                {
                    if (_repCese.Exist(Json.Cese.Id ?? 0))
                    {
                        PersonalCeseBO cese = _repCese.FirstBy(x => x.IdPersonal == personalBO.Id);
                        cese.IdMotivoCese = Json.Cese.IdMotivoCese;
                        cese.FechaCese = Json.Cese.FechaCese;

                        cese.UsuarioModificacion = Json.Usuario;
                        cese.FechaModificacion = DateTime.Now;

                        _repCese.Update(cese);
                    }
                    else
                    {
                        PersonalCeseBO cese = new PersonalCeseBO();
                        cese.IdPersonal = personalBO.Id;
                        cese.IdMotivoCese = Json.Cese.IdMotivoCese;
                        cese.FechaCese = Json.Cese.FechaCese;

                        cese.Estado = true;
                        cese.UsuarioCreacion = Json.Usuario;
                        cese.UsuarioModificacion = Json.Usuario;
                        cese.FechaCreacion = DateTime.Now;
                        cese.FechaModificacion = DateTime.Now;

                        _repCese.Insert(cese);
                    }
                }


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}/{Usuario}/{Discador}")]
        [HttpGet]
        public ActionResult ActualizarDiscador(int Id,string Usuario,bool Discador)
        {
            try
            {
                var _repPersonal = new PersonalRepositorio();

                PersonalBO Personal = _repPersonal.FirstById(Id);
                Personal.UsuarioModificacion = Usuario;
                Personal.DiscadorActivo = Discador;
                Personal.FechaModificacion = DateTime.Now;

                _repPersonal.Update(Personal);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
