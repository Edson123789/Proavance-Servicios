using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Web.Helpers;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;

namespace serviciosproavance.Controllers
{
    /// Controlador: IntegraAspNetUsersController
    /// Autor: Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Usuarios y Accesos al sistema
    /// </summary>
    [Route("api/IntegraAspNetUsers")]
    public class IntegraAspNetUsersController : BaseController<TIntegraAspNetUsers, ValidadorIntegraAspNetUsersDTO>
    {
        public IntegraAspNetUsersController(IIntegraRepository<TIntegraAspNetUsers> repositorio, ILogger<BaseController<TIntegraAspNetUsers, ValidadorIntegraAspNetUsersDTO>> logger, IIntegraRepository<BSI.Integra.Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Usuarios
        /// </summary>
        /// <returns>Obtiene Lista de Registros de Usuarios y cantidad de registros </returns>
        /// <returns> objetoDTO: List<GestionUsuariosDTO>, int </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRegistrosGestionUsuario()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);
                var resgistrosIntegra = _repRegistrosIntegra.ObtenerGestionUsuarioLista();
                return Ok(new { Data = resgistrosIntegra, Total = resgistrosIntegra.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Módulos Asignados por Usuario
        /// </summary>
        /// <returns>Lista de módulos asignados</returns>
        [Route("[Action]/{IdUsuario}")]
        [HttpPost]
        public ActionResult ObtenerModulosAsignados(int IdUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);
                var resgistrosIntegra = _repRegistrosIntegra.ObtenerModulosAsignados(IdUsuario);
                return Ok(new { Data = resgistrosIntegra, Total = resgistrosIntegra.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.2
        /// <summary>
        /// Obtiene cadena html de menú por módulos asignados
        /// </summary>
        /// <param name="NombreUsuario"> Nombre Usuario </param>
        /// <param name="Dominio"> Dominio Modulo </param>
        /// <returns>Retorna html menú <returns>
        /// <returns> string </returns>
        [Route("[Action]/{NombreUsuario}/{Dominio}")]
        [HttpGet]
        public ActionResult ObtenerModuloporUsuario(string NombreUsuario, string Dominio)
        {
            try
            {
                Dominio = Dominio.Replace("*","/");
                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);
                UsuarioRepositorio _repUsuario = new UsuarioRepositorio();
                var validacion = _repUsuario.GetBy(o => true, x => new { x.Id, x.NombreUsuario }).Where(x => x.NombreUsuario == NombreUsuario).ToList();

                if (string.IsNullOrEmpty(NombreUsuario))
                {
                    return Ok("");
                }
                if (validacion.Count() == 0)
                {
                    return Ok("");
                }
                //var modulo = _repRegistrosIntegra.ObtenerDatosParaModulo(NombreUsuario);
                var modulo = _repRegistrosIntegra.ObtenerDatosParaModuloAgrupado(NombreUsuario);
                int count = modulo.Count();
                string[] grupo = new string[count];
                string[] tipo = new string[count];
                string[] moduloLista = new string[count];
                string[] urlLista = new string[count];
                string[] idLista = new string[count];

                for (int i = 0; i < count; i++)
                {
                    grupo[i] = modulo[i].NombreGrupo.ToString();
                }
                for (int i = 0; i < count; i++)
                {
                    if (modulo[i].NombreModuloSistemaTipo != null)
                    {
                        tipo[i] = modulo[i].NombreModuloSistemaTipo.ToString();
                    }
                    else
                    {
                        tipo[i] = "Sin Agrupación";
                    }
                }
                for (int i = 0; i < count; i++)
                {
                    moduloLista[i] = modulo[i].NombreModulo.ToString();
                }
                for (int i = 0; i < count; i++)
                {
                    urlLista[i] = modulo[i].URL.ToString();
                }
                for (int i = 0; i < count; i++)
                {
                    idLista[i] = modulo[i].IdModulo.ToString();
                }


                string[] moduloLi = new string[count];
                for (int i = 0; i < grupo.Length; i++)
                {
                    moduloLi[i] = "<li><a href = '"+Dominio+ urlLista[i] + "?IdModulo="+ idLista[i] + "' style='padding-left:40px'>" + moduloLista[i] + " </a></li> ";
                }

                string[] subGrupoLi = new string[count];
                for (int i = 0; i < tipo.Length; i++)
                {
                    if (i == 0)
                    {
                        subGrupoLi[i] = "<li class='iconoMenuAgrupado'><span id = 'iconoMenu' class='k-icon k-i-arrow-60-right'></span>"
                                       + tipo[i] + "<ul>" + moduloLi[i];
                    }
                    else
                    {
                        if (tipo[i].Contains(tipo[i - 1]) && grupo[i].Contains(grupo[i - 1]))
                        {
                            subGrupoLi[i] = moduloLi[i];
                        }
                        else
                        {
                            if (tipo[i].Contains(tipo[i - 1]))
                            {
                                subGrupoLi[i] = "<li class='iconoMenuAgrupado'><span id ='iconoMenu' class='k-icon k-i-arrow-60-right'></span>"
                                      + tipo[i] + "<ul>" + moduloLi[i];
                            }
                            else
                            {
                                if (grupo[i].Contains(grupo[i - 1])) 
                                {
                                    subGrupoLi[i] = "</ul><li class='iconoMenuAgrupado'><span id ='iconoMenu' class='k-icon k-i-arrow-60-right'></span>"
                                      + tipo[i] + "<ul>" + moduloLi[i];
                                }
                                else
                                {
                                    subGrupoLi[i] = "<li class='iconoMenuAgrupado'><span id ='iconoMenu' class='k-icon k-i-arrow-60-right'></span>"
                                      + tipo[i] + "<ul>" + moduloLi[i];
                                }
                            }                            
                        }
                    }
                }

                string[] grupoLi = new string[count];
                for (int i = 0; i < grupo.Length; i++)
                {
                    if (i == 0)
                    {
                        if (grupo[i] == "COMERCIAL") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-globe iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "PLANIFICACION") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-saturation iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "MARKETING") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-share iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "FINANZAS") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-dollar iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "GESTIÓN DE PERSONAS") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-myspace iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "REPORTES") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-paste-plain-text iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "OPERACIONES") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-page-properties iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "FUR") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-percent iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "CAJAS") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-inbox iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "INGRESOS") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-validation-data iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else if (grupo[i] == "MAESTROS GENERALES") { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-thumbnails-down iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        else { grupoLi[i] = "<li><span id = 'iconoMenu' class='k-icon k-i-track-changes-accept iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                    }
                    else
                    {
                        if (grupo[i].Contains(grupo[i - 1]))
                        {
                            grupoLi[i] = subGrupoLi[i];
                        }
                        else
                        {
                            if (grupo[i] == "COMERCIAL") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-globe iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "PLANIFICACION") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-saturation iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "MARKETING") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-share iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "FINANZAS") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-dollar iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "GESTIÓN DE PERSONAS") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-myspace iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "REPORTES") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-paste-plain-text iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "OPERACIONES") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-page-properties iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "FUR") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-percent iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "CAJAS") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-inbox iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "INGRESOS") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-validation-data iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else if (grupo[i] == "MAESTROS GENERALES") { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-thumbnails-down iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                            else { grupoLi[i] = "</ul></ul><li><span id = 'iconoMenu' class='k-icon k-i-track-changes-accept iconoMenuFont' style='padding:5px'></span>" + grupo[i] + "<ul>" + subGrupoLi[i]; }
                        }
                    }                    
                }

                string concats = String.Concat(grupoLi);
                string moduloFinal = "<div id='MenuHorizontal'><!--<input type='checkbox' id='abrir-cerrar' name='abrir-cerrar' value=''><label for= 'abrir-cerrar'> <i class='fa fa-bars'  style='font - size: 23px;'></i> <span class='abrir'></span><span class='cerrar'></span></label><div class='logo' style='display: inline-block;width: 85.5%;'><img class='img-fluid' src='" + Dominio+"Images/Sistema/bsginstitute.png'/></div>--><ul id='panelbar'>"
                           + concats + "</ul></ul></li></ul></div>";
                return Ok(moduloFinal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida accesos de Usuario a módulo
        /// </summary>
        /// <param name="NombreUsuario">Nombre Usuario</param>
        /// <param name="IdModulo">Id Modulo</param>
        /// <returns>Obtiene confirmación de acceso</returns>
        /// <returns> Retorna Validación de Confirmación: ResultadoFinaltextoDTO </returns>
        [Route("[Action]/{NombreUsuario}/{IdModulo}")]
        [HttpGet]
        public ActionResult ValidarUsuarioModulo(string NombreUsuario, int IdModulo)
        {
            try
            {

                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);
               
                var modulo = _repRegistrosIntegra.ValidarUsuarioModulo(NombreUsuario, IdModulo);
                
                return Ok(modulo.Valor);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{NombreUsuario}")]
        [HttpGet]
        public ActionResult ValidarReLogin(string NombreUsuario)
        {
            try
            {

                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);

                var modulo = _repRegistrosIntegra.ValidarReLogin(NombreUsuario);

                return Ok(modulo.Valor);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{NombreUsuario}")]
        [HttpGet]
        public ActionResult ActualizarReLogin(string NombreUsuario)
        {
            try
            {

                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);

                var modulo = _repRegistrosIntegra.ActualizarReLogin(NombreUsuario);

                return Ok(modulo.Valor);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta Usuario y acceso denegado a módulo
        /// </summary>
        /// <param name="NombreUsuario">Nombre Usuario</param>
        /// <param name="IdModulo">Id Modulo</param>
        /// <returns>Obtiene confirmación de inserción:ResultadoFinalDTO</returns>
        [Route("[Action]/{NombreUsuario}/{IdModulo}")]
        [HttpGet]
        public ActionResult InsertarAccesoDenegado(string NombreUsuario, int IdModulo)
        {
            try
            {

                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);

                var respuesta = _repRegistrosIntegra.InsertarAccesoDenegado(NombreUsuario, IdModulo);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Módulos no asignados a Usuario
        /// </summary>
        /// <returns>Obtiene lista de Módulos no asignados a Usuario</returns>
        /// <returns> Lista de objetosDTO: List<AsignarModuloDTO>, Cantidad de Registros: int </returns>
        [Route("[Action]/{IdUsuario}")]
        [HttpPost]
        public ActionResult ObtenerModulosnoAsignados(int IdUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);
                var resgistrosIntegra = _repRegistrosIntegra.ObtenerModulosnoAsignados(IdUsuario);
                return Ok(new { Data = resgistrosIntegra, Total = resgistrosIntegra.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Usuario y Rol por Filtro
        /// </summary>
        /// <returns>Obtiene información de Usuario y Rol</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerUsuarioRolFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio();
                var usuarioRol = _repRegistrosIntegra.ObtenerUsuarioRolFiltro();
                return Ok(new { data = usuarioRol });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza datos de Usuario y accesos
        /// </summary>
        /// <returns>Confirmación de actualización</returns>
        /// <returns> bool </returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult ActualizarAspNetUser([FromBody]IntegraAspNetUsersDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);
                UsuarioRepositorio _repUsuario = new UsuarioRepositorio();
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                WhatsAppUsuarioRepositorio _repWhatsAppUsuario = new WhatsAppUsuarioRepositorio();
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio(contexto);
                CoordinadoraBO coordinadora = new CoordinadoraBO();
                var usuarioWhatsApp = _repWhatsAppUsuario.UsuarioWhatsAppValido(Objeto.PerId);
                var existeCoordinador = _repCoordinadora.ExistePorNombreUsuario(Objeto.UserName);
                if (Objeto.RolId == 17 && usuarioWhatsApp == null) //Coordinadora Academica - crea accesos Whatsapp
                {
                    WhatsAppUsuarioBO credecialOperaciones = new WhatsAppUsuarioBO();
                    WhatsAppDatoUsuarioDTO DatoUsuario = new WhatsAppDatoUsuarioDTO
                    {
                        Id = 0,
                        IdPersonal = Objeto.PerId,
                        RolUser = "Rol_User",
                        UserUsername = Objeto.UserName,
                        UserPassword = "BSGInstitute2019#",
                        UsuarioSistema = Objeto.Usuario
                    };
                    credecialOperaciones.GenerarCredencialesWhatsAppOperaciones(DatoUsuario);
                }

                if (Objeto.RolId == 17 && !existeCoordinador)
                {
                    var datosCoordinadora = _repPersonal.ObtenerDatosPersonalAgenda(Objeto.PerId);
                    coordinadora.IdPersonal = datosCoordinadora.Id;
                    coordinadora.AliasCorreo = datosCoordinadora.Email;
                    coordinadora.Clave = "";
                    coordinadora.Firma = Objeto.UserName + ".png";
                    coordinadora.Usuario = Objeto.UserName;
                    coordinadora.Anexo = Int32.Parse(datosCoordinadora.Anexo);
                    coordinadora.Modalidad = "AONLINE";
                    coordinadora.Genero = false;
                    coordinadora.IdSede = 4;
                    coordinadora.Htmlnumero = "";
                    coordinadora.Htmlhorario = "";
                    coordinadora.Iniciales = "";
                    coordinadora.Estado = true;
                    coordinadora.UsuarioCreacion = "system";
                    coordinadora.UsuarioModificacion = "system";
                    coordinadora.FechaCreacion = DateTime.Now;
                    coordinadora.FechaModificacion = DateTime.Now;
                    _repCoordinadora.Insert(coordinadora);
                }

                var lista = _repPersonal.GetBy(o => true, x => new { x.Id, x.AreaAbrev }).Where(x => x.Id == Objeto.PerId).FirstOrDefault();
                var listaIdUsuario = _repUsuario.GetBy(o => true, x => new { x.Id, x.IdPersonal }).Where(x => x.IdPersonal == Objeto.PerId).FirstOrDefault();

                UsuarioBO usuarioBO = new UsuarioBO();
                usuarioBO = _repUsuario.FirstById(listaIdUsuario.Id);
                usuarioBO.IdPersonal = Objeto.PerId;
                usuarioBO.NombreUsuario = Objeto.UserName;
                usuarioBO.Clave = _repRegistrosIntegra.Encriptar(Objeto.UsClave);
                usuarioBO.IdUsuarioRol = Objeto.RolId;
                usuarioBO.CodigoAreaTrabajo = lista.AreaAbrev;
                usuarioBO.FechaModificacion = DateTime.Now;
                usuarioBO.Estado = true;
                usuarioBO.UsuarioModificacion = Objeto.Usuario;

                _repUsuario.Update(usuarioBO);

                IntegraAspNetUsersBO integraBO = new IntegraAspNetUsersBO();
                integraBO = _repRegistrosIntegra.ObtenerDatosParaActualizar(Objeto.Id);

                integraBO.Id = Objeto.Id;
                integraBO.PasswordHash = Crypto.HashPassword(Objeto.UsClave);
                integraBO.UsClave = Objeto.UsClave;
                integraBO.PerId = Objeto.PerId;
                integraBO.AreaTrabajo = lista.AreaAbrev;
                integraBO.RolId = Objeto.RolId;
                integraBO.Email = Objeto.Email;
                integraBO.Estado = true;
                integraBO.UserName = Objeto.UserName;
                integraBO.UsuarioModificacion = Objeto.Usuario;
                integraBO.FechaModificacion = DateTime.Now;

                if (!integraBO.HasErrors)
                {
                    var rpta = _repRegistrosIntegra.Update(integraBO);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(integraBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza contraseña de Usuarios
        /// </summary>
        /// <returns>Confirmación de actualización</returns>
        /// <returns> bool </returns>
        [Route("[Action]/{Prueba}")]
        [HttpPost]
        public ActionResult ActualizarTodasContrasenas(int Prueba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);
                UsuarioRepositorio _repUsuario = new UsuarioRepositorio();
                PersonalRepositorio _repPersonal = new PersonalRepositorio();

                var listafinal =_repRegistrosIntegra.ListaUsuariosActualizar(Prueba);
                GmailCorreoBO gmailCorreoBO = new GmailCorreoBO();

                List<string> errores = new List<string>();

                foreach (var item in listafinal)
                {
                    try
                    {
                        var lista = _repPersonal.GetBy(o => true, x => new { x.Id, x.AreaAbrev }).Where(x => x.Id == item.PerId).FirstOrDefault();
                        var listaIdUsuario = _repUsuario.GetBy(o => true, x => new { x.Id, x.IdPersonal }).Where(x => x.IdPersonal == item.PerId).FirstOrDefault();

                        UsuarioBO usuarioBO = new UsuarioBO();
                        usuarioBO = _repUsuario.FirstById(listaIdUsuario.Id);
                        usuarioBO.IdPersonal = item.PerId;
                        usuarioBO.NombreUsuario = item.Usuario;
                        usuarioBO.Clave = _repRegistrosIntegra.Encriptar(item.NuevaContrasena);
                        usuarioBO.IdUsuarioRol = item.RolId;
                        usuarioBO.CodigoAreaTrabajo = lista.AreaAbrev;
                        usuarioBO.FechaModificacion = DateTime.Now;
                        usuarioBO.Estado = true;
                        usuarioBO.UsuarioModificacion = "ccrispinsistemas";

                        _repUsuario.Update(usuarioBO);

                        IntegraAspNetUsersBO integraBO = new IntegraAspNetUsersBO();
                        integraBO = _repRegistrosIntegra.ObtenerDatosParaActualizar(item.IdIntegraAspNetUsers);

                        integraBO.Id = item.IdIntegraAspNetUsers;
                        integraBO.PasswordHash = Crypto.HashPassword(item.NuevaContrasena);
                        integraBO.UsClave = item.NuevaContrasena;
                        integraBO.PerId = item.PerId;
                        integraBO.AreaTrabajo = lista.AreaAbrev;
                        integraBO.RolId = item.RolId;
                        integraBO.Email = item.Email;
                        integraBO.Estado = true;
                        integraBO.UserName = item.Usuario;
                        integraBO.UsuarioModificacion = "ccrispinsistemas";
                        integraBO.FechaModificacion = DateTime.Now;

                        if (!integraBO.HasErrors)
                        {
                            var rpta = _repRegistrosIntegra.Update(integraBO);
                            string mensaje = "<p>Estimado(a) por protocolos de seguridad dentro de la empresa periodicamente se estaran cambiando las contraseñas de sus accesos a integra para lo cual les enviamos su nueva contraseña.</p><ul><li>Usuario:" + item.Usuario + "</li><li>Nueva Contraseña:" + item.NuevaContrasena + "</li></ul>Soporte BSG Institute";
                            bool res = gmailCorreoBO.EnviarCorreoGmail(item.Email, "ccrispin@bsginstitute.com", "Soporte", "lwcgjufgpftosebo", mensaje, "Cambio Contraseña Integra");
                            //return Ok(new { data = rpta });
                        }
                        else
                        {
                            //return BadRequest(IntegraBO.GetErrors(null));
                        }
                    }
                    catch (Exception e)
                    {
                        errores.Add(item.Usuario);
                        //siga noma
                    }
                    
                }

                return Ok(errores);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta información de Usuarios y datos de acceso
        /// </summary>
        /// <returns>Confirmación de inserción</returns>
        /// <returns> bool </returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult InsertarAspNetUser([FromBody]IntegraAspNetUsersDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);
                UsuarioRepositorio _repUsuario = new UsuarioRepositorio(contexto);
                PersonalRepositorio _repPersonal = new PersonalRepositorio(contexto);
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio(contexto);
                CoordinadoraBO coordinadora = new CoordinadoraBO();

                var lista = _repPersonal.GetBy(o => true, x => new { x.Id, x.AreaAbrev }).Where(x => x.Id == Objeto.PerId).FirstOrDefault();
                var validacion = _repUsuario.GetBy(o => true, x => new { x.Id, x.NombreUsuario }).Where(x => x.NombreUsuario == Objeto.UserName).ToList();
                var existeCoordinador = _repCoordinadora.ExistePorNombreUsuario(Objeto.UserName);
                if (validacion.Count()==1)
                {
                    throw new System.Exception("El nombre de usuario ya existe");
                }
                if (Objeto.RolId == 17) //Coordinadora Academica - crea accesos Whatsapp
                {
                    if (!existeCoordinador)
                    {
                        var datosCoordinadora = _repPersonal.ObtenerDatosPersonalAgenda(Objeto.PerId);
                        coordinadora.IdPersonal = datosCoordinadora.Id;
                        coordinadora.AliasCorreo = datosCoordinadora.Email;
                        coordinadora.Clave = "";
                        coordinadora.Firma = Objeto.UserName+".png";
                        coordinadora.Usuario = Objeto.UserName;
                        coordinadora.Anexo = Int32.Parse(datosCoordinadora.Anexo);
                        coordinadora.Modalidad = "AONLINE";
                        coordinadora.Genero = false;
                        coordinadora.IdSede = 4;
                        coordinadora.Htmlnumero = "";
                        coordinadora.Htmlhorario = "";
                        coordinadora.Iniciales = "";
                        coordinadora.Estado = true;
                        coordinadora.UsuarioCreacion = "system";
                        coordinadora.UsuarioModificacion = "system";
                        coordinadora.FechaCreacion = DateTime.Now;
                        coordinadora.FechaModificacion = DateTime.Now;
                        _repCoordinadora.Insert(coordinadora);
                    }
                    WhatsAppUsuarioBO credecialOperaciones = new WhatsAppUsuarioBO();
                    WhatsAppDatoUsuarioDTO DatoUsuario = new WhatsAppDatoUsuarioDTO
                    {
                        Id = 0,
                        IdPersonal = Objeto.PerId,
                        RolUser = "Rol_User",
                        UserUsername = Objeto.UserName,
                        UserPassword = "BSGInstitute2019#",
                        UsuarioSistema = Objeto.Usuario
                    };
                    credecialOperaciones.GenerarCredencialesWhatsAppOperaciones(DatoUsuario);
                }

                var resultado = _repPersonal.InsertarUsuarioNuevaContraseña(Objeto.UserName, Objeto.UsClave);

                UsuarioBO usuarioBO = new UsuarioBO();

                usuarioBO.IdPersonal = Objeto.PerId;
                usuarioBO.NombreUsuario = Objeto.UserName;
                usuarioBO.Clave = _repRegistrosIntegra.Encriptar(Objeto.UsClave);
                usuarioBO.IdUsuarioRol = Objeto.RolId;
                usuarioBO.CodigoAreaTrabajo = lista.AreaAbrev;
                usuarioBO.FechaCreacion = DateTime.Now;
                usuarioBO.FechaModificacion = DateTime.Now;
                usuarioBO.Estado = true;
                usuarioBO.UsuarioCreacion = Objeto.Usuario;
                usuarioBO.UsuarioModificacion = Objeto.Usuario;

                _repUsuario.Insert(usuarioBO);
                    
                IntegraAspNetUsersBO integraBO = new IntegraAspNetUsersBO();
                integraBO.Id = Guid.NewGuid().ToString();
                integraBO.PasswordHash = Crypto.HashPassword(Objeto.UsClave);
                integraBO.UsClave = Objeto.UsClave;
                integraBO.PerId = Objeto.PerId;
                integraBO.AreaTrabajo = lista.AreaAbrev;
                integraBO.RolId = Objeto.RolId;
                integraBO.Email = Objeto.Email;
                integraBO.EmailConfirmed = true;
                integraBO.Estado = true;
                integraBO.UserName = Objeto.UserName;
                integraBO.UsuarioCreacion = Objeto.Usuario;
                integraBO.UsuarioModificacion = Objeto.Usuario;
                integraBO.FechaCreacion = DateTime.Now;
                integraBO.FechaModificacion = DateTime.Now;
                
             
                if (!integraBO.HasErrors)
                {
                    var rpta = _repRegistrosIntegra.Insert(integraBO);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(integraBO.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Quita el acceso a módulos por Usuario
        /// </summary>
        /// <returns>Confirmación de eliminación de acceso</returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult QuitarModulos([FromBody]ModuloSistemaAccesoDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ModuloSistemaAccesoRepositorio _repModuloAcceso = new ModuloSistemaAccesoRepositorio(contexto);
                var lista2 = _repModuloAcceso.GetBy(o => true, x => new { x.Id, x.IdUsuario, x.IdModuloSistema, x.Estado }).Where(x => x.IdUsuario == Objeto.IdUsuario && x.IdModuloSistema == Objeto.IdModuloSistema).FirstOrDefault();
                ModuloSistemaAccesoBO moduloAcceso = new ModuloSistemaAccesoBO();
                moduloAcceso = _repModuloAcceso.FirstById(lista2.Id);
                moduloAcceso.FechaModificacion = DateTime.Now;
                moduloAcceso.UsuarioModificacion = Objeto.UsuarioIntegra;
                moduloAcceso.Estado = false;

                if (!moduloAcceso.HasErrors)
                {
                    var rpta1 = _repModuloAcceso.Update(moduloAcceso);
                    return Ok(new { data = rpta1 });
                }
                else
                {
                    return BadRequest(moduloAcceso.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el acceso a módulos a un Usuario
        /// </summary>
        /// <returns>Confirmación de actualización de acceso</returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult ActualizarModulos([FromBody]ModuloSistemaAccesoDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ModuloSistemaAccesoRepositorio _repModuloAcceso = new ModuloSistemaAccesoRepositorio(contexto);
                var lista = _repModuloAcceso.GetBy(o => false, x => new { x.Id, x.IdUsuario, x.IdModuloSistema, x.Estado }).Where(x => x.IdUsuario == Objeto.IdUsuario && x.IdModuloSistema == Objeto.IdModuloSistema).ToList();
                if (lista.Count() == 0)
                {
                    UsuarioRepositorio _repUsuario = new UsuarioRepositorio();
                    var rol = _repUsuario.GetBy(o => true, x => new { x.Id, x.IdUsuarioRol }).Where(x => x.Id == Objeto.IdUsuario).FirstOrDefault();
                    ModuloSistemaAccesoBO moduloAcceso = new ModuloSistemaAccesoBO();
                    moduloAcceso.IdUsuario = Objeto.IdUsuario;
                    moduloAcceso.IdModuloSistema = Objeto.IdModuloSistema;
                    moduloAcceso.IdUsuarioRol = rol.IdUsuarioRol;
                    moduloAcceso.Estado = true;
                    moduloAcceso.FechaModificacion = DateTime.Now;
                    moduloAcceso.FechaCreacion = DateTime.Now;
                    moduloAcceso.UsuarioCreacion = Objeto.UsuarioIntegra;
                    moduloAcceso.UsuarioModificacion = Objeto.UsuarioIntegra;

                    if (!moduloAcceso.HasErrors)
                    {
                        var rpta = _repModuloAcceso.Insert(moduloAcceso);
                        return Ok(new { data = rpta });
                    }
                    else
                    {
                        return BadRequest(moduloAcceso.GetErrors(null));
                    }
                }
                else
                {
                    var lista2 = _repModuloAcceso.GetBy(o => true, x => new { x.Id, x.IdUsuario, x.IdModuloSistema, x.Estado }).Where(x => x.IdUsuario == Objeto.IdUsuario && x.IdModuloSistema == Objeto.IdModuloSistema).FirstOrDefault();
                    ModuloSistemaAccesoBO moduloAcceso = new ModuloSistemaAccesoBO();
                    moduloAcceso = _repModuloAcceso.FirstById(lista2.Id);
                    moduloAcceso.FechaModificacion = DateTime.Now;
                    moduloAcceso.UsuarioModificacion = Objeto.UsuarioIntegra;
                    moduloAcceso.Estado = true;

                    if (!moduloAcceso.HasErrors)
                    {
                        var rpta1 = _repModuloAcceso.Update(moduloAcceso);
                        return Ok(new { data = rpta1 });
                    }
                    else
                    {
                        return BadRequest(moduloAcceso.GetErrors(null));
                    }
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id, Nombre de personal
        /// </summary>
        /// <returns>Registro de Id, Nombre de Personal</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPersonalAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(_repRegistrosIntegra.ObtenerPersonalAutocomplete(Filtros["Valor"].ToString()));
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida el acceso a módulo
        /// </summary>
        /// <returns>Url de módulo y Id de Personal</returns>
        [Route("[Action]/{Url}/{IdPersonal}")]
        [HttpGet]
        public ActionResult TieneAcceso(string Url, int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Url = Url.Replace("*", "/");
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio();
                return Ok(new { TieneAcceso = _repRegistrosIntegra.TieneAccesoModulo(Url, IdPersonal) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta IP y cookie de Usuario
        /// </summary>
        /// <returns> Confirmación de Inserción </returns>
        /// <returns> Bool </returns>
        [Route("[Action]/{Usuario}/{CookieUsuario}/{IpUsuario}")]
        [HttpGet]
        public ActionResult InsertarIpCookieUsuario(string Usuario, string CookieUsuario, string IpUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                integraDBContext contexto = new integraDBContext();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(contexto);

                AccesosIntegraLogRepositorio _repaccesosIntegraLogRepositorio = new AccesosIntegraLogRepositorio(contexto);
                AccesosIntegraLogBO accesosIntegraLogBO = new AccesosIntegraLogBO();

                AccesosIntegraDetalleLogBO accesosIntegraLogDetalleBO = new AccesosIntegraDetalleLogBO();
                AccesosIntegraDetalleLogRepositorio _repccesosIntegraDetalleLogRepositorio = new AccesosIntegraDetalleLogRepositorio(contexto);

                var usuarioAcceso = _repaccesosIntegraLogRepositorio.ObtenerUsuarioAccesoIntegralog(Usuario);

                if (usuarioAcceso.Usuario == Usuario)
                {
                    if (usuarioAcceso.IpUsuario != IpUsuario)
                    {
                        var fechaActual = DateTime.Now.Date.ToString("dd/MM/yyyy");
                        //var format = "dd/MM/yyyy";

                        //DateTime dateTime = DateTime.ParseExact(Fechaactual,format, CultureInfo.InvariantCulture);

                        var cantidadIpUsuario = _repccesosIntegraDetalleLogRepositorio.ObtenerCantidadIpUsuario(usuarioAcceso.Id, fechaActual);

                        if (cantidadIpUsuario.Cantidad < 3) {
                            accesosIntegraLogBO = _repaccesosIntegraLogRepositorio.FirstById(usuarioAcceso.Id);
                            accesosIntegraLogBO.IpUsuario = IpUsuario;
                            accesosIntegraLogBO.FechaModificacion = DateTime.Now;
                            _repaccesosIntegraLogRepositorio.Update(accesosIntegraLogBO);

                            accesosIntegraLogDetalleBO.IdAccesosIntegraLog = accesosIntegraLogBO.Id;
                            accesosIntegraLogDetalleBO.Tipo = "Ip";
                            accesosIntegraLogDetalleBO.Valor = IpUsuario;
                            accesosIntegraLogDetalleBO.Fecha = DateTime.Now;
                            accesosIntegraLogDetalleBO.Estado = true;
                            accesosIntegraLogDetalleBO.UsuarioCreacion = "system";
                            accesosIntegraLogDetalleBO.UsuarioModificacion = "system";
                            accesosIntegraLogDetalleBO.FechaCreacion = DateTime.Now;
                            accesosIntegraLogDetalleBO.FechaModificacion = DateTime.Now;
                            _repccesosIntegraDetalleLogRepositorio.Insert(accesosIntegraLogDetalleBO);
                        }
                        else if (cantidadIpUsuario.Cantidad >= 3)
                        {
                            accesosIntegraLogBO = _repaccesosIntegraLogRepositorio.FirstById(usuarioAcceso.Id);
                            accesosIntegraLogBO.IpUsuario = IpUsuario;
                            accesosIntegraLogBO.Habilitado = false;
                            accesosIntegraLogBO.FechaModificacion = DateTime.Now;
                            _repaccesosIntegraLogRepositorio.Update(accesosIntegraLogBO);

                            accesosIntegraLogDetalleBO.IdAccesosIntegraLog = accesosIntegraLogBO.Id;
                            accesosIntegraLogDetalleBO.Tipo = "Ip";
                            accesosIntegraLogDetalleBO.Valor = IpUsuario;
                            accesosIntegraLogDetalleBO.Fecha = DateTime.Now;
                            accesosIntegraLogDetalleBO.Estado = true;
                            accesosIntegraLogDetalleBO.UsuarioCreacion = "system";
                            accesosIntegraLogDetalleBO.UsuarioModificacion = "system";
                            accesosIntegraLogDetalleBO.FechaCreacion = DateTime.Now;
                            accesosIntegraLogDetalleBO.FechaModificacion = DateTime.Now;
                            _repccesosIntegraDetalleLogRepositorio.Insert(accesosIntegraLogDetalleBO);
                        }

                        

                    }
                    if (usuarioAcceso.Cookie != CookieUsuario)
                    {
                        accesosIntegraLogBO = _repaccesosIntegraLogRepositorio.FirstById(usuarioAcceso.Id);
                        accesosIntegraLogBO.Cookie = CookieUsuario;
                        accesosIntegraLogBO.FechaModificacion = DateTime.Now;
                        _repaccesosIntegraLogRepositorio.Update(accesosIntegraLogBO);

                        accesosIntegraLogDetalleBO = new AccesosIntegraDetalleLogBO();
                        accesosIntegraLogDetalleBO.IdAccesosIntegraLog = accesosIntegraLogBO.Id;
                        accesosIntegraLogDetalleBO.Tipo = "Cookie";
                        accesosIntegraLogDetalleBO.Valor = CookieUsuario;
                        accesosIntegraLogDetalleBO.Fecha = DateTime.Now;
                        accesosIntegraLogDetalleBO.Estado = true;
                        accesosIntegraLogDetalleBO.UsuarioCreacion = "system";
                        accesosIntegraLogDetalleBO.UsuarioModificacion = "system";
                        accesosIntegraLogDetalleBO.FechaCreacion = DateTime.Now;
                        accesosIntegraLogDetalleBO.FechaModificacion = DateTime.Now;
                        _repccesosIntegraDetalleLogRepositorio.Insert(accesosIntegraLogDetalleBO);
                    }
                }
                else if (usuarioAcceso.Usuario == null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        accesosIntegraLogBO.Usuario = Usuario;
                        accesosIntegraLogBO.IpUsuario = IpUsuario;
                        accesosIntegraLogBO.Cookie = CookieUsuario;
                        accesosIntegraLogBO.Habilitado = true;
                        accesosIntegraLogBO.Estado = true;
                        accesosIntegraLogBO.UsuarioCreacion = "system";
                        accesosIntegraLogBO.UsuarioModificacion = "system";
                        accesosIntegraLogBO.FechaCreacion = DateTime.Now;
                        accesosIntegraLogBO.FechaModificacion = DateTime.Now;
                        _repaccesosIntegraLogRepositorio.Insert(accesosIntegraLogBO);

                        accesosIntegraLogDetalleBO.IdAccesosIntegraLog = accesosIntegraLogBO.Id;
                        accesosIntegraLogDetalleBO.Tipo = "Ip";
                        accesosIntegraLogDetalleBO.Valor = IpUsuario;
                        accesosIntegraLogDetalleBO.Fecha = DateTime.Now;
                        accesosIntegraLogDetalleBO.Estado = true;
                        accesosIntegraLogDetalleBO.UsuarioCreacion = "system";
                        accesosIntegraLogDetalleBO.UsuarioModificacion = "system";
                        accesosIntegraLogDetalleBO.FechaCreacion = DateTime.Now;
                        accesosIntegraLogDetalleBO.FechaModificacion = DateTime.Now;
                        _repccesosIntegraDetalleLogRepositorio.Insert(accesosIntegraLogDetalleBO);

                        accesosIntegraLogDetalleBO = new AccesosIntegraDetalleLogBO();
                        accesosIntegraLogDetalleBO.IdAccesosIntegraLog = accesosIntegraLogBO.Id;
                        accesosIntegraLogDetalleBO.Tipo = "Cookie";
                        accesosIntegraLogDetalleBO.Valor = CookieUsuario;
                        accesosIntegraLogDetalleBO.Fecha = DateTime.Now;
                        accesosIntegraLogDetalleBO.Estado = true;
                        accesosIntegraLogDetalleBO.UsuarioCreacion = "system";
                        accesosIntegraLogDetalleBO.UsuarioModificacion = "system";
                        accesosIntegraLogDetalleBO.FechaCreacion = DateTime.Now;
                        accesosIntegraLogDetalleBO.FechaModificacion = DateTime.Now;
                        _repccesosIntegraDetalleLogRepositorio.Insert(accesosIntegraLogDetalleBO);

                        scope.Complete();
                    }
                }          

                        
                
                //string rpta = "INSERTADO CORRECTAMENTE";
                return Ok(true);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }/// TipoFuncion: GET
         /// Autor: Edgar S.
         /// Fecha: 25/01/2021
         /// Versión: 1.0
         /// <summary>
         /// Inserta IP y cookie de Usuario
         /// </summary>
         /// <returns> Confirmación de Inserción </returns>
         /// <returns> Bool </returns>
        [Route("[Action]/{Usuario}")]
        [HttpGet]
        public ActionResult ObtenerNombre(string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PersonalRepositorio _repPersonal = new PersonalRepositorio(contexto);

                return Ok(_repPersonal.ObtenerPrimerNombreApellidoPaternoPorUserName(Usuario));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
    public class ValidadorIntegraAspNetUsersDTO : AbstractValidator<TIntegraAspNetUsers>
    {
        public static ValidadorIntegraAspNetUsersDTO Current = new ValidadorIntegraAspNetUsersDTO();
        public ValidadorIntegraAspNetUsersDTO()
        {

        }

    }
}