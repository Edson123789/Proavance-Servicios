using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Helpers;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AccesoSistemaController
    /// Autor: Edgar Serruto.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Accesos al Sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoSistemaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PersonalRepositorio _repPersonal;
        private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;
        private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;
        private readonly UsuarioRepositorio _repUsuario;
        private readonly IntegraAspNetUsersRepositorio _repIntegraAspNetUsers;
        private readonly CentralLlamadaDireccionRepositorio _repCentralLlamadaDireccion;

        public AccesoSistemaController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
            _repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
            _repUsuario = new UsuarioRepositorio(_integraDBContext);
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
            _repCentralLlamadaDireccion = new CentralLlamadaDireccionRepositorio();
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 20/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de Personal
        /// </summary>
        /// <returns> Lista de Registros de Personal : List<AccesoSistemaDTO> </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerRegistros()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                List<AccesoSistemaDTO> listaPersonal = new List<AccesoSistemaDTO>();
                var listaPersonalUsuario = _repPersonal.ObtenerInformacionPersonalUsuario();                
                return Ok(listaPersonalUsuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 20/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la información de personal y Usuario
        /// </summary>
        /// <returns> Confirmación: Objeto DTO : GestionPersonalUsuarioDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarRegistro([FromBody] GestionPersonalUsuarioDTO PersonalModificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonalRep = new PersonalRepositorio(_integraDBContext);
                GmailClienteRepositorio _repGmailCliente = new GmailClienteRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio _repAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                TCentralLlamada _repCentralLlama = new TCentralLlamada();

                PersonalBO personal = new PersonalBO();
                personal = _repPersonalRep.FirstById(PersonalModificacion.Id);
                var tmp = PersonalModificacion;
                var centralPersonal = "";
                if (PersonalModificacion.IdCentralTelefonica != null)
                {
                    centralPersonal = _repCentralLlamadaDireccion.GetBy(x => x.Id == PersonalModificacion.IdCentralTelefonica).Select(x => x.DireccionIp).FirstOrDefault();
                }
                else
                {
                    centralPersonal = "0";
                }
                
                using (TransactionScope scope = new TransactionScope())
                {
                    personal.Central = centralPersonal;
                    personal.Email = PersonalModificacion.Email;
                    personal.Anexo = PersonalModificacion.Anexo;
                    personal.Anexo3Cx = PersonalModificacion.Anexo;
                    personal.Id3Cx = PersonalModificacion.Id3CX;
                    personal.Password3Cx = PersonalModificacion.Password3CX;
                    personal.UsuarioModificacion = PersonalModificacion.Usuario;
                    personal.FechaModificacion = DateTime.Now;
                    personal.Estado = true;
                    _repPersonalRep.Update(personal);

                    var usuarioPasswordIntegra = _repAspNetUsers.GetBy(x=>x.PerId == PersonalModificacion.Id).FirstOrDefault();
                    if (usuarioPasswordIntegra != null)
                    {
                        var cambiarUsuario = _repUsuario.GetBy(x => x.IdPersonal == PersonalModificacion.Id).FirstOrDefault();
                        if(cambiarUsuario != null)
                        {                                     
                            cambiarUsuario.NombreUsuario = PersonalModificacion.NombreUsuario;
                            cambiarUsuario.IdUsuarioRol = PersonalModificacion.IdUsuarioRol;
                            cambiarUsuario.CodigoAreaTrabajo = personal.AreaAbrev;
                            _repUsuario.Update(cambiarUsuario);
                        }

                        usuarioPasswordIntegra.Email = PersonalModificacion.Email;
                        usuarioPasswordIntegra.UserName = PersonalModificacion.NombreUsuario;
                        usuarioPasswordIntegra.UsClave = PersonalModificacion.ClaveIntegra;
                        usuarioPasswordIntegra.PasswordHash = Crypto.HashPassword(PersonalModificacion.ClaveIntegra);
                        usuarioPasswordIntegra.RolId = PersonalModificacion.IdUsuarioRol;
                        _repAspNetUsers.Update(usuarioPasswordIntegra);
                    }
                    else
                    {
                        var clavegenerada = string.Concat(personal.Nombres.Substring(0, 1).Trim(),
                                                      personal.Apellidos.Trim(),
                                                      personal.Id,
                                                      personal.Apellidos.Substring(0, 4).Trim()
                                                      ).ToUpper();

                        UsuarioBO agregarUsuario = new UsuarioBO()
                        {
                            IdPersonal = personal.Id,
                            NombreUsuario = PersonalModificacion.NombreUsuario,
                            Clave = _repIntegraAspNetUsers.Encriptar(clavegenerada),
                            IdUsuarioRol = PersonalModificacion.IdUsuarioRol,
                            CodigoAreaTrabajo = personal.AreaAbrev,
                            UsuarioCreacion = PersonalModificacion.Usuario,
                            UsuarioModificacion = PersonalModificacion.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,                            
                        };
                        var resUsuario = _repUsuario.Insert(agregarUsuario);

                        if (resUsuario)
                        {
                            IntegraAspNetUsersBO integraAgregar = new IntegraAspNetUsersBO()
                            {
                                Id = Guid.NewGuid().ToString(),
                                PasswordHash = Crypto.HashPassword(PersonalModificacion.ClaveIntegra),
                                UsClave = PersonalModificacion.ClaveIntegra,
                                PerId = personal.Id,
                                AreaTrabajo = personal.AreaAbrev,
                                RolId = PersonalModificacion.IdUsuarioRol,
                                Email = PersonalModificacion.Email,
                                EmailConfirmed = true,
                                Estado = true,
                                UserName = PersonalModificacion.NombreUsuario,
                                UsuarioCreacion = PersonalModificacion.Usuario,
                                UsuarioModificacion = PersonalModificacion.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repIntegraAspNetUsers.Insert(integraAgregar);
                        }                        
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

                return Ok(PersonalModificacion);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 14/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para módulo
        /// </summary>
        /// <returns>Objeto Agrupado</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaCentralLlamadaDireccion = _repCentralLlamadaDireccion.GetBy(x => x.Estado == true, x => new { Id = x.Id, NombreCentral = x.Nombre, Direccion = x.DireccionIp }).ToList();
                var objeto = new
                {
                    listaArea = _repPersonalAreaTrabajo.ObtenerTodoFiltro(),
                    listaPuestoTrabajo = _repPuestoTrabajo.ObtenerFiltroPuestoTrabajo(),
                    listaPersonal = _repPersonal.ObtenerPersonalFiltro(),
                    listaCentralLlamadaDireccion = listaCentralLlamadaDireccion,
                    listaRol = _repUsuario.ObtenerUsuarioRolCombo(),
                };
                return Ok(objeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 20/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de Personal Nuevo
        /// </summary>
        /// <returns> Registros de Personal: Objeto DTO: AccesoSistemaDTO </returns>
        [HttpGet]
        [Route("[action]/{IdPersonal}")]
        public ActionResult ObtenerRegistroNuevoPersonal(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                List<AccesoSistemaDTO> listaPersonal = new List<AccesoSistemaDTO>();
                var listaPersonalUsuarioNuevo = _repPersonal.ObtenerInformacionPersonalUsuarioNuevo(IdPersonal);
                return Ok(listaPersonalUsuarioNuevo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
